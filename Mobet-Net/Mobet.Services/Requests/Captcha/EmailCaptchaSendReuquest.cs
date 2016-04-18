using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Services.Requests.Captcha
{
    /// <summary>
    /// 发送邮箱验证码
    /// </summary>
    public class EmailCaptchaSendRequest
    {
        /// <summary>
        /// 邮件验证码类型
        /// </summary>
        public EmailCaptcha Captcha { get; set; }
        /// <summary>
        /// 邮件地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 失效时间（分钟）
        /// </summary>
        public int ExpiredTime { get; set; }
    }

    public class EmailCaptchaSendResponse : BooleanResponse
    {
        public EmailCaptchaSendResponse(bool result, string message)
           : base(result, message)
        {
        }
    }
}
