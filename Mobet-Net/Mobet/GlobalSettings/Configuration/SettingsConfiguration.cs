using Mobet.Collections;
using Mobet.GlobalSettings.Provider;
using System.Collections.Generic;

namespace Mobet.GlobalSettings.Configuration
{
    /// <summary>
    /// Used to configure setting system.
    /// </summary>
    public class GlobalSettingsConfiguration : IGlobalSettingsConfiguration
    {
        public ITypeList<GlobalSettingsProvider> Providers { get; private set; }

        public GlobalSettingsConfiguration()
        {
            Providers = new TypeList<GlobalSettingsProvider>();
        }
    }
}