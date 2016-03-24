using Mobet.Configuration.Startup;
using Mobet.Dependency;
using Mobet.Events.Configuration;
using Mobet.Events.ConventionalRegistras;
using Mobet.Events.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Events.Startup
{
    public static class StartupConfigurationExtensions
    {

        public static StartupConfiguration UseEventBus(this StartupConfiguration bootstrap, Action<IEventBusConfiguration> invoke = null)
        {
            IocManager.Instance.RegisterIfNot<IEventBusConfiguration, EventBusConfiguration>();
            IocManager.Instance.AddConventionalRegistrar(new EventBusConventionalRegistras());

            if (invoke != null)
            {
                invoke(IocManager.Instance.Resolve<IEventBusConfiguration>());
            }

            IocManager.Instance.RegisterModule(new EventBusModule());


            return bootstrap;
        }
    }
}
