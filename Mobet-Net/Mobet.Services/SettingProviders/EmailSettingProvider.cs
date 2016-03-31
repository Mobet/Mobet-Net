using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobet.GlobalSettings;
using Mobet.GlobalSettings.Provider;
using Mobet.GlobalSettings.Models;

namespace Mobet.Services.SettingProviders
{
    public class EmailSettingProvider : GlobalSettingsProvider
    {
        public override IEnumerable<GlobalSettings.Models.GlobalSetting> GetSettings(GlobalSettingsProviderContext context)
        {
            return new[]
                   {
                       new GlobalSettings.Models.GlobalSetting {Name = Constants.Settings.Email.SMTP,Value = "127.0.0.1",Group =  "Email", Scope = GlobalSettingScope.Application },
                   };
        }
    }
}
