using Mobet.Collections;
using Mobet.Settings.Provider;
using System.Collections.Generic;

namespace Mobet.Settings.Configuration
{
    /// <summary>
    /// Used to configure setting system.
    /// </summary>
    public class SettingsConfiguration : ISettingsConfiguration
    {
        public ITypeList<SettingProvider> Providers { get; private set; }

        public SettingsConfiguration()
        {
            Providers = new TypeList<SettingProvider>();
        }
    }
}