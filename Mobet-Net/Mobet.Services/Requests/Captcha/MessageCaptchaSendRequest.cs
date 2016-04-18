using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Services.Requests.Captcha
{
    public class MessageCaptchaSendRequest
    {
        /// <summary>
        /// 短信验证码类型
        /// </summary>
        public MessageCaptcha Captcha { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [Required]
        public string Telphone { get; set; }
        /// <summary>
        /// 失效时间（分钟）
        /// </summary>
        public int ExpiredTime { get; set; }
    }

    public class MessageCaptchaSendResponse : BooleanResponse
    {
        public MessageCaptchaSendResponse(bool result, string message)
           : base(result, message)
        {
        }
    }
}
