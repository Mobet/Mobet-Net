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
    public class MessageSettingProvider : GlobalSettingsProvider
    {
        public override IEnumerable<GlobalSettings.Models.GlobalSetting> GetSettings(GlobalSettingsProviderContext context)
        {
            return new[]
            {
                new GlobalSetting { Name = Constants.Settings.Message.MessageContentTemplate,DisplayName = "短信验证码内容模板", Value = "您于{0}进行手机验证操作，验证码为{1}，10分钟内有效，尽快操作，千万不要告诉别人哦！",Group = "短信平台" ,Description = "短信验证码内容模板"},
                new GlobalSetting { Name = Constants.Settings.Message.MessageSeverUrl,DisplayName = "短信平台接口地址", Value = "http://qf-coreuat-01:8148/sms-frontal/MessageController/sendMsg",Group = "短信平台" ,Description = "短信平台接口地址"},
                new GlobalSetting { Name = Constants.Settings.Message.MessageDeptType,DisplayName = "短信部门代码", Value = "001",Group = "短信平台" ,Description = "短信部门代码"},
                new GlobalSetting { Name = Constants.Settings.Message.MessageBesType,DisplayName = "短信业务代码", Value = "005",Group = "短信平台" ,Description = "短信业务代码"},
                new GlobalSetting { Name = Constants.Settings.Message.MessageExpiredTime,DisplayName = "短信失效时间(分钟)", Value = "30",Group = "短信平台" ,Description = "短信失效时间(分钟)"},
            };
        }

    }
}