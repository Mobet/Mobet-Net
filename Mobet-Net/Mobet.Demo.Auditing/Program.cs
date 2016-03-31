using System;
using System.Threading.Tasks;

using Mobet.Auditing.Attributes;
using Mobet.Domain.Services;
using Mobet.Dependency;
using Mobet.Configuration.Startup;
using Mobet.Extensions;
using Mobet.Auditing.Startup;
using Mobet.Auditing.Store;
using Mobet.Auditing.Provider;
using Mobet.Auditing;
using Newtonsoft.Json;
using Castle.Core.Logging;

namespace Mobet.Demo.Auditing
{
    class Program
    {
        static void Main(string[] args)
        {
            StartupConfig.RegisterDependency(cfg =>
            {
                cfg.UseLoggingLog4net()
                   .UseAuditing(s =>
                   {
                       s.IsEnabled = true;
                   });

                cfg.RegisterConsoleApplication();
            });

            var service = IocManager.Instance.Resolve<IService>();

            service.Mothod();
            service.Mothod(new ServiceMethodRequest { ID = 10001, Name = "XiaoMing" });
            service.ExceptionMothod(new ServiceMethodRequest { ID = 10001, Name = "Exception" });


            Console.ReadKey();
        }
    }
    public static class ConsoleHelper
    {

        public static void WriteLine(string msg = "", ConsoleColor forecolor = ConsoleColor.Green, ConsoleColor backcolor = ConsoleColor.Black)
        {
            Console.ForegroundColor = forecolor;
            Console.BackgroundColor = backcolor;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }

    public class ConsoleAuditingStore : IAuditingStore, IDependency
    {
        public ILogger Logger { get; set; }

        public ConsoleAuditingStore()
        {
            Logger = NullLogger.Instance;
        }

        public Task SaveAsync(AuditingMessage auditInfo)
        {
            ConsoleHelper.WriteLine();
            ConsoleHelper.WriteLine(JsonConvert.SerializeObject(auditInfo, Formatting.Indented), ConsoleColor.Red);
            ConsoleHelper.WriteLine();
            return Task.FromResult(0);
        }

    }

    public class AuditModelProvider : IAuditingModelProvider, IDependency
    {
        public void Fill(AuditingMessage auditInfo)
        {
            auditInfo.Host = "SONGCHAOX-QUARKFINANCE";
            auditInfo.Route = "https://xxx.com/service/method";
            auditInfo.UserAgent = "Mozilla/5.0 (Linux;Android 5.1;MX5)";
            auditInfo.UserData = JsonConvert.SerializeObject(new  { UserAccount = "mobet", UserId = 201037, Role = "administrator" });
        }
    }

    [Audited]
    public class Service : IService
    {
        public async Task<ServiceMethodResponse> ExceptionMothod(ServiceMethodRequest request)
        {
            ConsoleHelper.WriteLine("exception mothod running ...");

            throw new Exception("some exception throw", new Exception("inner excepetion..."));

            return await Task.FromResult(new ServiceMethodResponse { Result = false, Message = "this is some response" });
        }

        public void Mothod()
        {
            ConsoleHelper.WriteLine("no parameters method for service mothod do something ...");
        }
        public async Task Mothod(ServiceMethodRequest request)
        {
            ConsoleHelper.WriteLine("service async mothod do something ...");

            await Task.FromResult(new ServiceMethodResponse { Result = false, Message = "this is some response" });
        }

        public async Task<ServiceMethodResponse> Mothod2(ServiceMethodRequest request)
        {
            ConsoleHelper.WriteLine("service asnyc mothod with response parameters something ...");

            return await Task.FromResult(new ServiceMethodResponse { Result = false, Message = "this is some response" });
        }

    }
    public interface IService : IApplicationService
    {
        void Mothod();

        Task Mothod(ServiceMethodRequest request);

        Task<ServiceMethodResponse> Mothod2(ServiceMethodRequest request);

        Task<ServiceMethodResponse> ExceptionMothod(ServiceMethodRequest request);
    }


    public class ServiceMethodRequest
    {
        public int ID { get; set; }
        public string Name { get; set; }

    }

    public class ServiceMethodResponse
    {
        public bool Result { get; set; }
        public string Message { get; set; }
    }


}
