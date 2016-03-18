using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Auditing.Configuration;
using Mobet.Domain.UnitOfWork.Configuration;
using Mobet.EntityFramework.Configuration;
using Mobet.Events.Configuration;
using Mobet.Localization.Configuration;
using Mobet.Settings.Configuration;
using Mobet.Dependency;

namespace Mobet.Configuration.Startup
{
    public interface IStartupConfiguration
    {
        /// <summary>
        /// Used to set localization configuration.
        /// </summary>
        ILocalizationConfiguration LocalizationConfiguration { get; }
        /// <summary>
        /// Used to configure unit of work defaults.
        /// </summary>
        IUnitOfWorkDefaultOptionsConfiguration UnitOfWorkDefaultOptionsConfiguration { get; }
        /// <summary>
        /// Used to configure EntityFramework.
        /// </summary>
        IEntityFrameworkConfiguration EntityFrameworkConfiguration { get; }
        /// <summary>
        /// Used to configure auditing.
        /// </summary>
        IAuditingConfiguration AuditingConfiguration { get; }
        /// <summary>
        /// Used to configure setting system.
        /// </summary>
        ISettingsConfiguration SettingsConfiguration { get; }
        /// <summary>
        /// Used to configure <see cref="IEventBus"/>.
        /// </summary>
        IEventBusConfiguration EventBusConfiguration { get; }
    }
}
