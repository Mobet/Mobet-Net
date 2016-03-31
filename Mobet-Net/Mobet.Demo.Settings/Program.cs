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
using Mobet.Settings.Store;

namespace Mobet.Demo.Settings
{
    class Program
    {
        static void Main(string[] args)
        {
            StartupConfig.RegisterDependency(cfg =>
            {
                cfg.UseSettings(config =>
                {
                    config.Providers.Add<EmailSettingProvider>();
                });

                cfg.RegisterConsoleApplication();
            });

            var settingConfiguration = IocManager.Instance.Resolve<IGlobalSettingsConfiguration>();
            var settingManager = IocManager.Instance.Resolve<IGlobalSettingManager>();
            var emailSmtpHost = settingManager.GetSettingValue("Email.Smtp.Host");

            Console.WriteLine(string.Format("Default email smtp host is {0}", emailSmtpHost));

            var service = IocManager.Instance.Resolve<IService>();
            service.Mothed();

            Console.ReadKey();
            var service2 = IocManager.Instance.Resolve<IService>();
            service2.Mothed();

            Console.ReadKey();
        }
    }

    /// <summary>
    /// Defines settings to send emails.
    /// <see cref="EmailSettingNames"/> for all available configurations.
    /// </summary>
    internal class EmailSettingProvider : SettingProvider
    {
        public override IEnumerable<GlobalSetting> GetSettings(GlobalSettingsProviderContext context)
        {
            return new[]
                   {
                       new GlobalSetting("Email.Smtp.Host", "127.0.0.1", new GlobalSettingGroup("Email"), scopes: GlobalSettingScope.Application),
                   };
        }
    }

    public interface IService : IApplicationService
    {
        void Mothed();
    }

    public class Service : IService
    {
        public IGlobalSettingManager SettingManager { get; set; }

        public Service()
        {
        }
        public void Mothed()
        {
            Console.WriteLine(string.Format("Service : Email smtp host is {0}", SettingManager.GetSettingValue("Email.Smtp.Host")));
        }
    }

    public class Simple2SettingStore : IGlobalSettingStore, IDependency
    {
        private Dictionary<string, string> _dictionary = new Dictionary<string, string>() { };
        public Simple2SettingStore()
        {
            _dictionary.Add("Email.Smtp.Host", "Simple2SettingStore");
        }

        public Task<GlobalSetting> GetSettingAsync(string name)
        {
            return Task.FromResult(new GlobalSetting(name, _dictionary[name]));
        }
        public Task<GlobalSetting> DeleteSettingAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<GlobalSetting> AddOrUpdateSettingAsync(GlobalSetting setting)
        {
            throw new NotImplementedException();
        }

        public Task<List<GlobalSetting>> GetAllSettingsAsync()
        {
            return Task.FromResult(new List<GlobalSetting>() {
                new GlobalSetting("Email.Smtp.Host","Simple2SettingStore")
            });
        }
    }
}
