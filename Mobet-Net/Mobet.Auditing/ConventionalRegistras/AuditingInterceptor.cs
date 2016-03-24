using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Transactions;

using Castle.Core.Logging;
using Castle.DynamicProxy;

using Mobet.Extensions;
using Mobet.Domain.UnitOfWork;
using Mobet.Runtime.Session;
using Mobet.Threading;
using Mobet.Auditing.Store;
using Mobet.Auditing.Configuration;
using Mobet.Auditing.Provider;
using Mobet.Auditing.Extensions;

namespace Mobet.Auditing.ConventionalRegistras
{
    internal class AuditingInterceptor : IInterceptor
    {
        public IAppSession AppSession { get; set; }

        public ILogger Logger { get; set; }

        public IAuditingStore AuditingStore { get; set; }

        private readonly IAuditingConfiguration _configuration;

        private readonly IAuditModelProvider _auditInfoProvider;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AuditingInterceptor(IAuditingConfiguration configuration, IAuditModelProvider auditInfoProvider, IUnitOfWorkManager unitOfWorkManager,IAuditingStore auditingStore)
        {
            _configuration = configuration;
            _auditInfoProvider = auditInfoProvider;
            _unitOfWorkManager = unitOfWorkManager;

            AppSession = NullAppSession.Instance;
            Logger = NullLogger.Instance;
            AuditingStore = auditingStore;
        }

        public void Intercept(IInvocation invocation)
        {
            if (!AuditingHelper.ShouldSaveAudit(invocation.MethodInvocationTarget, _configuration, AppSession))
            {
                invocation.Proceed();
                return;
            }

            var auditInfo = CreateAuditInfo(invocation);

            if (AsyncHelper.IsAsyncMethod(invocation.Method))
            {
                PerformAsyncAuditing(invocation, auditInfo);
            }
            else
            {
                PerformSyncAuditing(invocation, auditInfo);
            }
        }

        private AuditModel CreateAuditInfo(IInvocation invocation)
        {
            var auditInfo = new AuditModel
            {
                UserAccount = AppSession.UserAccount,
                ServiceName = invocation.MethodInvocationTarget.DeclaringType != null
                    ? invocation.MethodInvocationTarget.DeclaringType.FullName
                    : "",
                MethodName = invocation.MethodInvocationTarget.Name,
                Parameters = ConvertArgumentsToJson(invocation),
                Time = DateTime.Now
            };

            _auditInfoProvider.Fill(auditInfo);

            return auditInfo;
        }

        private void PerformSyncAuditing(IInvocation invocation, AuditModel auditInfo)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                auditInfo.Exception = ex;
                throw;
            }
            finally
            {
                stopwatch.Stop();
                auditInfo.Duration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
                AuditingStore.Save(auditInfo);
            }
        }
        private void PerformAsyncAuditing(IInvocation invocation, AuditModel auditInfo)
        {
            var stopwatch = Stopwatch.StartNew();

            invocation.Proceed();

            if (invocation.Method.ReturnType == typeof(Task))
            {
                invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithFinally(
                    (Task) invocation.ReturnValue,
                    exception => SaveAuditInfo(auditInfo, stopwatch, exception)
                    );
            }
            else //Task<TResult>
            {
                invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithFinallyAndGetResult(
                    invocation.Method.ReturnType.GenericTypeArguments[0],
                    invocation.ReturnValue,
                    exception => SaveAuditInfo(auditInfo, stopwatch, exception)
                    );
            }
        }

        private string ConvertArgumentsToJson(IInvocation invocation)
        {
            try
            {
                var parameters = invocation.MethodInvocationTarget.GetParameters();
                if (parameters.IsNullOrEmpty())
                {
                    return "{}";
                }

                var dictionary = new Dictionary<string, object>();
                for (int i = 0; i < parameters.Length; i++)
                {
                    var parameter = parameters[i];
                    var argument = invocation.Arguments[i];
                    dictionary[parameter.Name] = argument;
                }

                return dictionary.ToJsonString(true);
            }
            catch (Exception ex)
            {
                Logger.Warn("Could not serialize arguments for method: " + invocation.MethodInvocationTarget.Name);
                Logger.Warn(ex.ToString(), ex);
                return "{}";
            }
        }

        private void SaveAuditInfo(AuditModel auditInfo, Stopwatch stopwatch, Exception exception)
        {
            stopwatch.Stop();
            auditInfo.Exception = exception;
            auditInfo.Duration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.Suppress))
            {
                AuditingStore.Save(auditInfo);
                uow.Complete();
            }
        }
    }
}