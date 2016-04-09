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
        public override IEnumerable<GlobalSetting> GetSettings(GlobalSettingsProviderContext context)
        {
            return new[]
            {
                new GlobalSetting { Name = Constants.Settings.Resources.Domain,DisplayName = "资源站点根目录", Value = "https://120.25.244.254:44300/",Group = "资源设置" ,Description = "资源站点根目录"}
            };
        }

    }
}