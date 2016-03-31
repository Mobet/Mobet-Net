using System.Collections.Generic;
using Mobet.Dependency;
using Mobet.GlobalSettings.Models;

namespace Mobet.GlobalSettings.Provider
{
    /// <summary>
    /// Inherit this class to define settings for a module/application.
    /// </summary>
    public abstract class GlobalSettingsProvider : IDependency
    {
        /// <summary>
        /// Gets all setting definitions provided by this provider.
        /// </summary>
        /// <returns>List of settings</returns>
        public abstract IEnumerable<GlobalSetting> GetSettings(GlobalSettingsProviderContext context);
    }
}