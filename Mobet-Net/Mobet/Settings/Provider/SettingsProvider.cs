using System.Collections.Generic;
using Mobet.Dependency;

namespace Mobet.Settings.Provider
{
    /// <summary>
    /// Inherit this class to define settings for a module/application.
    /// </summary>
    public abstract class SettingProvider : IDependency
    {
        /// <summary>
        /// Gets all setting definitions provided by this provider.
        /// </summary>
        /// <returns>List of settings</returns>
        public abstract IEnumerable<Setting> GetSettings(SettingProviderContext context);
    }
}