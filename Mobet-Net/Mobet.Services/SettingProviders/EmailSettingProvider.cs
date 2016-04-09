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
        public override IEnumerable<GlobalSetting> GetSettings(GlobalSettingsProviderContext context)
        {
            return new[]
                   {
                       new GlobalSetting {Name = Constants.Settings.Email.MailSMTP,DisplayName = "消息通知邮箱SMTP",Value = "127.0.0.1",Group =  "邮箱设置", Scope = GlobalSettingScope.Application ,Description = "消息通知邮箱SMTP"},
                       new GlobalSetting {Name = Constants.Settings.Email.MailUserName ,DisplayName = "发送邮箱账户",Value = "example@microsoft.com",Group =  "邮箱设置", Scope = GlobalSettingScope.Application ,Description = "消息通知邮箱发送邮箱账户"},
                       new GlobalSetting {Name = Constants.Settings.Email.MailPassword  ,DisplayName = "发送邮箱账户密码",Value = "password",Group =  "邮箱设置", Scope = GlobalSettingScope.Application ,Description = "发送邮箱账户密码"},
                       new GlobalSetting {Name = Constants.Settings.Email.MailPort,DisplayName = "邮件端口",Value = "25",Group =  "邮箱设置", Scope = GlobalSettingScope.Application ,Description = "邮件端口"},
                   };
        }
    }
}
