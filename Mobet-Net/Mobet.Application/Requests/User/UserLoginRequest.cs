using Mobet.AutoMapper;
using Mobet.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Application.Requests.User
{
    [AutoMap(typeof(Domain.Models.User))]
    public class UserLoginRequest : IRequest
    {
        /// <summary>
        /// 移动号码
        /// </summary>
        public string Mobilephone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 登录提供程序
        /// </summary>
        public string LoginProvider { get; set; }
    }
}
