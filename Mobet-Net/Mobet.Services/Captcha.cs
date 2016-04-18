using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Services
{
    /// <summary>
    /// 短信验证码类型
    /// </summary>
    public enum MessageCaptcha
    {
        /// <summary>
        /// 注册
        /// </summary>
        [Description("注册")]
        Register = 1,
        /// <summary>
        /// 找回密码
        /// </summary>
        [Description("找回密码")]
        RetrievePassword = 2,
    }

    /// <summary>
    /// 邮件验证码类型
    /// </summary>
    public enum EmailCaptcha
    {
        /// <summary>
        /// 绑定
        /// </summary>
        Bind = 1,
        /// <summary>
        /// 变更
        /// </summary>
        Change = 2,
    }

}
