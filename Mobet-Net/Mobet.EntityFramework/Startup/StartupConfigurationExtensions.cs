using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Mobet.Configuration.Startup;
using Mobet.Dependency;
using Mobet.Domain.UnitOfWork.ConventionalRegistras;
using Mobet.EntityFramework.ConventionalRegistras;
using Mobet.EntityFramework.Configuration;

namespace Mobet.EntityFramework.Startup
{
    public static class StartupConfigurationExtensions
    {
        public static StartupConfiguration UseDataAccessEntityFramework(this StartupConfiguration bootstrap, Action<IEntityFrameworkConfiguration> invoke = null)
        {
            IocManager.Instance.RegisterIfNot<IEntityFrameworkConfiguration, EntityFrameworkConfiguration>();
            IocManager.Instance.AddConventionalRegistrar(new UnitOfWorkConventionalRegistrar());
            IocManager.Instance.AddConventionalRegistrar(new EntityFrameworkConventionalRegistrar());

            if (invoke != null)
            {
                invoke(IocManager.Instance.Resolve<IEntityFrameworkConfiguration>());
            }

            return bootstrap;
        }

    }
}
