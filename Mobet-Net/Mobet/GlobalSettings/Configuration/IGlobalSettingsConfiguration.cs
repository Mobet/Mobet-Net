using System.Collections.Generic;
using Mobet.GlobalSettings.Provider;
using Mobet.Collections;
using Mobet.Dependency;

namespace Mobet.GlobalSettings.Configuration
{
    /// <summary>
    /// Used to configure setting system.
    /// </summary>
    public interface IGlobalSettingsConfiguration
    {
        /// <summary>
        /// List of settings providers.
        /// </summary>
        ITypeList<GlobalSettingsProvider> Providers { get; }
    }
}
