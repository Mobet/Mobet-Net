using Mobet.Domain.Services;
using Mobet.Services.Requests.Captcha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Services.Services
{
    public interface ICaptchaService : IApplicationService
    {
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<MessageCaptchaSendResponse> MessageCaptchaSendAsync(MessageCaptchaSendRequest request);

        /// <summary>
        /// 发送邮件验证码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmailCaptchaSendResponse> EmailCaptchaSendAsync(EmailCaptchaSendRequest request);
    }
}
