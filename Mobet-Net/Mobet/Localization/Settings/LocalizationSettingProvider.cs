using System.Collections.Generic;
using Mobet.Localization.Configuration;
using Mobet.GlobalSettings.Provider;
using Mobet.GlobalSettings;
using Mobet.GlobalSettings.Models;

namespace Mobet.Localization.Settings
{
    public class LocalizationSettingProvider : GlobalSettingsProvider
    {
        public override IEnumerable<GlobalSettings.Models.GlobalSetting> GetSettings(GlobalSettingsProviderContext context)
        {
            return new[]
            {
                new GlobalSettings.Models.GlobalSetting { Name = "Localization.Default.Language.Name", Value= "zh-CN" , Group = "Localization"  }
            };
        }
    }
}