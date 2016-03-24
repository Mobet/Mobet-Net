using System;
using System.Linq;

using Castle.Core;

using Mobet.Dependency;
using Mobet.Auditing.Configuration;
using Mobet.Auditing.Attributes;

using Autofac;
using Autofac.Extras.DynamicProxy2;
using Mobet.Auditing.Store;
using Castle.Core.Logging;

namespace Mobet.Auditing.ConventionalRegistras
{
    public class AuditingRegistrar : IConventionalDependencyRegistrar
    {
        private IAuditingConfiguration _config;
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            _config = context.IocManager.Resolve<IAuditingConfiguration>();
            if (_config.IsEnabled)
            {
                var builder = new ContainerBuilder();

                builder.RegisterAssemblyTypes(context.Assembly)
                   .Where(t => ShouldIntercept(t))
                   .AsImplementedInterfaces()
                   .EnableInterfaceInterceptors()
                   .InterceptedBy(typeof(AuditingInterceptor))
                   .InstancePerDependency();

                builder.RegisterType<AuditingInterceptor>();

                builder.Update(context.IocManager.IocContainer);
            }
        }

        private bool ShouldIntercept(Type type)
        {
            if ((_config.Selectors.Any(selector => selector.Predicate(type)))
                || (type.IsDefined(typeof(AuditedAttribute), true))
                || (type.GetMethods().Any(m => m.IsDefined(typeof(AuditedAttribute), true)))
                )
            {
                return true;
            }
            return false;
        }
    }
}