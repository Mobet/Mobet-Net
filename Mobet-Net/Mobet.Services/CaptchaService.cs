﻿using Mobet.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Mobet.GlobalSettings;
using Castle.Core.Logging;
using Mobet.Utils;
using Mobet.Caching;
using Mobet.Services.Requests.Captcha;
using Mobet.Net.Mail;

namespace Mobet.Services
{
    public class CaptchaService : ICaptchaService
    {

        private readonly ICacheManager cacheManager;
        private readonly IGlobalSettingManager globalSettingManager;
        private readonly IEmailSender emailSender;


        public ILogger Logger { get; set; }

        public CaptchaService(IGlobalSettingManager globalSettingManager, ICacheManager cacheManager, IEmailSender emailSender)
        {
            this.globalSettingManager = globalSettingManager;
            this.cacheManager = cacheManager;
            this.emailSender = emailSender;
        }

        public async Task<MessageCaptchaSendResponse> MessageCaptchaSendAsync(MessageCaptchaSendRequest request)
        {
            try
            {
                var captcha = StringHelper.GenerateNumber6Random();
                var client = new HttpClient();
                var content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    {"phoneNumber", request.Telphone},
                    {"content", string.Format( await globalSettingManager.GetSettingValueAsync(Constants.Settings.Message.MessageContentTemplate),DateTime.Now.ToString(), captcha)},
                    {"deptType", await globalSettingManager.GetSettingValueAsync(Constants.Settings.Message.MessageDeptType)},
                    {"besType", await globalSettingManager.GetSettingValueAsync(Constants.Settings.Message.MessageBesType)}
                });
                var response = client.PostAsync(await globalSettingManager.GetSettingValueAsync(Constants.Settings.Message.MessageSeverUrl), content);
                var result = await response.Result.Content.ReadAsStringAsync();
                cacheManager.Set(
                    string.Format(Constants.CacheNames.MessageCaptcha, request.Captcha, request.Telphone, captcha),
                    captcha,
                    DateTime.Now.AddMinutes(request.ExpiredTime)
                    );

                return new MessageCaptchaSendResponse(result.ToUpper() == "OK", result.ToUpper() == "OK" ? "短信发送成功" : "短信发送失败");
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return new MessageCaptchaSendResponse(false, "发送失败，短信平台网络异常。");
            }
        }

        public async Task<EmailCaptchaSendResponse> EmailCaptchaSendAsync(EmailCaptchaSendRequest request)
        {
            var captcha = StringHelper.GenerateNumber6Random();
            try
            {
                cacheManager.Set(
                    string.Format(Constants.CacheNames.EmailCaptcha, request.Captcha, request.Email, captcha),
                    captcha,
                    DateTime.Now.AddMinutes(request.ExpiredTime)
                    );
                await emailSender.SendAsync(request.Email, "账号绑定邮箱安全通知", @"
                    <p>亲爱的用户，您好</p>
                    <p>您的验证码是：" + captcha + @"</p>
                    <p>此验证码将用于验证身份，修改密码密保等。请勿将验证码透露给其他人。</p>
                    <p>本邮件由系统自动发送，请勿直接回复！</p>
                    <p>感谢您的访问，祝您使用愉快！</p>
                    <p>此致</p>
                    <p>IT应用支持</p>
                ");
                return new EmailCaptchaSendResponse(true, "邮件发送成功");
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return new EmailCaptchaSendResponse(false, "邮件验证码发送失败，邮件服务器网络异常。");
            }
        }
    }
}
