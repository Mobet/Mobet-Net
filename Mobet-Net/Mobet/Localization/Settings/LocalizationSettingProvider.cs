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
                new GlobalSettings.Models.GlobalSetting { Name = "Settings.Localization.DefaultLanguageName", Value= "zh-CN" , Group = "本地化设置" ,Description = "默认语言" }
            };
        }
    }
}