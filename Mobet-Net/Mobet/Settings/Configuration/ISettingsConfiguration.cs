using System.Collections.Generic;
using Mobet.Settings.Provider;
using Mobet.Collections;
using Mobet.Dependency;
namespace Mobet.Settings.Configuration
{
    /// <summary>
    /// Used to configure setting system.
    /// </summary>
    public interface ISettingsConfiguration :ISingletonDependency
    {
        /// <summary>
        /// List of settings providers.
        /// </summary>
        ITypeList<SettingProvider> Providers { get; }
    }
}
