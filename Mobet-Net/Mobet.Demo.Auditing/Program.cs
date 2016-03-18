using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Auditing.Attributes;
using Mobet.Domain.Services;
using Mobet.Dependency;
using Mobet.Auditing.Configuration;
using Castle.Core.Logging;
using Mobet.Auditing.Store;
using Mobet.Configuration.Startup;

namespace Mobet.Demo.Auditing
{
    class Program
    {
        static void Main(string[] args)
        {
            StartupConfig.RegisterDependency(cfg =>
            {
                cfg.Configuration(s =>
                {
                    s.AuditingConfiguration.IsEnabled = true;
                    s.AuditingConfiguration.IsEnabledForAnonymousUsers = true;
                });

                cfg.UseLoggingLog4net()
                   .UseAuditing();

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
