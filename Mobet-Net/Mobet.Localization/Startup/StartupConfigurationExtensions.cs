using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Mobet.Configuration.Startup;
using Mobet.Dependency;
using Mobet.Localization.Configuration;
using Mobet.Localization.Modules;

namespace Mobet.Localization.Startup
{
    public static class StartupConfigurationExtensions
    {
        public static StartupConfiguration UseLocalization(this StartupConfiguration bootstrap, Action<ILocalizationConfiguration> invoke = null)
        {
            IocManager.Instance.RegisterIfNot<ILocalizationConfiguration, LocalizationConfiguration>();
            invoke(IocManager.Instance.Resolve<ILocalizationConfiguration>());

            if (invoke != null)
            {
                IocManager.Instance.RegisterModule(new LocalizationManagerModule());
            }
            return bootstrap;
        }
    }
}
