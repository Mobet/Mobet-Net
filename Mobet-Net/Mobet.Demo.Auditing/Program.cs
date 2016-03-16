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
            Bootstrapper boot = new Bootstrapper();
            boot.RegisterConsoleApplication();

            boot.StartupConfiguration.AuditingConfiguration.IsEnabled = true;
            boot.StartupConfiguration.AuditingConfiguration.IsEnabledForAnonymousUsers = true;


            boot
                .UseDataAccessMicrosoftDataAccess()
                .UseLoggingLog4net()
                .UseAuditing();


            //var config = IocManager.Instance.Resolve<IAuditingConfiguration>();
            //Console.WriteLine(config.IsEnabledForAnonymousUsers);
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
