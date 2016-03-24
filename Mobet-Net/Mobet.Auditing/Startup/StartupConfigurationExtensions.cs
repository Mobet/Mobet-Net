using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobet.Configuration.Startup;
using Mobet.Dependency;
using Mobet.Auditing.ConventionalRegistras;
using Mobet.Auditing.Configuration;

namespace Mobet.Auditing.Startup
{
    public static class StartupConfigurationExtensions
    {
        public static StartupConfiguration UseAuditing(this StartupConfiguration bootstrap, Action<IAuditingConfiguration> invoke = null)
        {
            IocManager.Instance.RegisterIfNot<IAuditingConfiguration, AuditingConfiguration>();
            IocManager.Instance.AddConventionalRegistrar(new AuditingRegistrar());

            if (invoke != null)
            {
                invoke(IocManager.Instance.Resolve<IAuditingConfiguration>());
            }

            return bootstrap;
        }
    }
}
