using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Compilation;

using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;

using Castle.Core.Logging;
using Castle.Services.Logging.Log4netIntegration;

using Mobet.Dependency;
using Mobet.Dependency.ConventionalRegistrars;
using Mobet.Domain.UnitOfWork.ConventionalRegistras;
using Mobet.EntityFramework.ConventionalRegistras;
using Mobet.Configuration.Startup;
using Mobet.Logging;
using Mobet.AutoMapper;
using Mobet.Auditing.ConventionalRegistras;
using Mobet.Modules.Logging;
using Mobet.Runtime.Session.Modules;
using Mobet.Caching;
using Mobet.Events.Modules;
using Mobet.Events.ConventionalRegistras;
using Mobet.EntityFramework.Configuration;
using Mobet.Auditing.Configuration;
using Mobet.Settings.Configuration;
using Mobet.Events.Configuration;
using Mobet.Domain.UnitOfWork.Configuration;
using Mobet.Localization.Configuration;

namespace Mobet.Configuration.Startup
{
    public class StartupConfiguration : IStartupConfiguration
    {
        /// <summary>
        /// Used to set localization configuration.
        /// </summary>
        public ILocalizationConfiguration LocalizationConfiguration { get; private set; }
        /// <summary>
        /// Used to configure unit of work defaults.
        /// </summary>
        public IUnitOfWorkDefaultOptionsConfiguration UnitOfWorkDefaultOptionsConfiguration { get; private set; }
        /// <summary>
        /// Used to configure EntityFramework.
        /// </summary>
        public IEntityFrameworkConfiguration EntityFrameworkConfiguration { get; private set; }
        /// <summary>
        /// Used to configure auditing.
        /// </summary>
        public IAuditingConfiguration AuditingConfiguration { get; private set; }
        /// <summary>
        /// Used to configure setting system.
        /// </summary>
        public ISettingsConfiguration SettingsConfiguration { get; private set; }
        /// <summary>
        /// Used to configure <see cref="IEventBus"/>.
        /// </summary>
        public IEventBusConfiguration EventBusConfiguration { get; private set; }

        public StartupConfiguration()
        {

            AuditingConfiguration = IocManager.Instance.Resolve<IAuditingConfiguration>();
            UnitOfWorkDefaultOptionsConfiguration = IocManager.Instance.Resolve<IUnitOfWorkDefaultOptionsConfiguration>();
            EntityFrameworkConfiguration = IocManager.Instance.Resolve<IEntityFrameworkConfiguration>();
            SettingsConfiguration = IocManager.Instance.Resolve<ISettingsConfiguration>();
            EventBusConfiguration = IocManager.Instance.Resolve<IEventBusConfiguration>();
            LocalizationConfiguration = IocManager.Instance.Resolve<ILocalizationConfiguration>();
        }
    }
}
