using System.Collections.Generic;
using Mobet.Localization.Configuration;
using Mobet.Settings.Provider;
using Mobet.Settings;

namespace Mobet.Localization.Settings
{
    public class LocalizationSettingProvider : SettingProvider
    {
        public override IEnumerable<Setting> GetSettings(SettingProviderContext context)
        {
            return new[]
            {
                new Setting("Localization.Default.Language.Name","zh-CN")
            };
        }
    }
}