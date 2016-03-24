using System;

using Mobet.Auditing.Attributes;
using Mobet.Domain.Services;
using Mobet.Dependency;
using Mobet.Configuration.Startup;
using Mobet.Extensions;
using Mobet.Auditing.Startup;

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
                       s.IsEnabledForAnonymousUsers = true;
                   });

                cfg.RegisterConsoleApplication();
            });

            var service = IocManager.Instance.Resolve<IService>();

            service.Mothod();

            Console.ReadKey();
        }
    }

    public class Service : IService
    {
        [AuditedAttribute]
        public void Mothod()
        {
            Console.WriteLine("service mothod do something ...");
        }
    }
    public interface IService : IApplicationService
    {
        void Mothod();
    }
}
