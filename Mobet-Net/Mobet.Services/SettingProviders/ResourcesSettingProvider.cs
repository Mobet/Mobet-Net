using Mobet.Services;
using Mobet.GlobalSettings;
using Mobet.GlobalSettings.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mobet.GlobalSettings.Models;

namespace Mobet.Authorization.Configuration
{
    public class ResourcesSettingProvider : GlobalSettingsProvider
    {
        public override IEnumerable<GlobalSettings.Models.GlobalSetting> GetSettings(GlobalSettingsProviderContext context)
        {
            return new[]
            {
                new GlobalSettings.Models.GlobalSetting { Name = Constants.Settings.Resources.Domain, Value = "https://120.25.244.254:44300/",Group = "Resources" }
            };
        }

    }
}