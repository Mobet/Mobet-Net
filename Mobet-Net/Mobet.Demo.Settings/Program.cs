using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Dependency;
using Mobet.Settings;
using Mobet.Settings.Extensions;
using Mobet.Settings.Provider;
using Mobet.Settings.Configuration;
using Mobet.Domain.Services;
using Mobet.Configuration.Startup;

namespace Mobet.Demo.Settings
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper boot = new Bootstrapper();
            boot.RegisterConsoleApplication();
            boot.StartupConfiguration.SettingsConfiguration.Providers.Add<EmailSettingProvider>();

            boot.UseLoggingLog4net()
                .UseCacheProviderInMemory()
                ;

            var settingConfiguration = IocManager.Instance.Resolve<ISettingsConfiguration>();
            var settingManager = IocManager.Instance.Resolve<ISettingManager>();
            var emailSmtpHost = settingManager.GetSettingValue("Email.Smtp.Host");

            Console.WriteLine(string.Format("Default email smtp host is {0}", emailSmtpHost));

            var service = IocManager.Instance.Resolve<IService>();
            service.Mothed();

            Console.ReadKey();
        }
    }

    /// <summary>
    /// Defines settings to send emails.
    /// <see cref="EmailSettingNames"/> for all available configurations.
    /// </summary>
    internal class EmailSettingProvider : SettingProvider
    {
        public override IEnumerable<Setting> GetSettings(SettingProviderContext context)
        {
            return new[]
                   {
                       new Setting("Email.Smtp.Host", "127.0.0.1", new SettingGroup("Email"), scopes: SettingScopes.Application),
                   };
        }
    }

    public interface IService : IApplicationService
    {
        void Mothed();
    }

    public class Service : IService
    {
        public ISettingManager SettingManager { get; set; }

        public Service()
        {
        }
        public void Mothed()
        {
            Console.WriteLine(string.Format("Service : Email smtp host is {0}", SettingManager.GetSettingValue("Email.Smtp.Host")));
        }
    }
}
