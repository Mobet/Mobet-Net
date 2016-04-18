using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mobet.Authorization.Models.Passport
{
    public class UserSetPasswordViewModel
    {
        /// <summary>
        /// 原密码
        /// </summary>
        [Required]
        public string OldPassword { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required]
        public string Captcha { get; set; }
    }
}