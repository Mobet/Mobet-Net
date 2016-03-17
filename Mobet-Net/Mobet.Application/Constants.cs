using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Application
{
    public static class Constants
    {
        /// <summary>
        /// 用户服务
        /// </summary>
        public static class AccountService
        {
            /// <summary>
            /// 用户服务资源文件
            /// </summary>
            public const string LocalizationSourceName = "AccountService";
            /// <summary>
            /// 注册成功
            /// </summary>
            public const string RegisterByMobilephoneComplete = "RegisterByMobilephoneComplete";
            /// <summary>
            /// 无效的短信验证码
            /// </summary>
            public const string RegisterByMobilephoneInvalidMessageAuthCode = "RegisterByMobilephoneInvalidMessageAuthCode";
        }
        /// <summary>
        /// 缓存
        /// </summary>
        public static class Cache
        {
            /// <summary>
            /// 短信验证码
            /// </summary>
            public const string MessageAuthCode = "AUTHORIZATION:MESSAGE_AUTH_CODE:{0}_{1}_{2}";
        }
    }
}
