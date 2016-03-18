using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobet.Settings;
using Mobet.Settings.Provider;

namespace Mobet.Services.SettingProviders
{
    public class EmailSettingProvider : SettingProvider
    {
        public override IEnumerable<Setting> GetSettings(SettingProviderContext context)
        {
            return new[]
                   {
                       new Setting("Email.Smtp.Host", "127.0.0.1", new SettingGroup("Email"), scopes: SettingScopes.Application),
                   };
        }
    }
}
