using Mobet.AutoMapper;
using Mobet.Domain.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Application.Requests.User
{
    [AutoMapTo(typeof(Domain.Models.User),typeof(Requests.User.UserCreateRequest))]
    public class UserRegisterByMobilephoneRequest : IRequest
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobilephone { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 短信验证码
        /// </summary>
        [Required]
        public string MessageAuthCode { get; set; }
    }

    public class UserRegisterByMobilephoneResponse : IResponse
    {
        public bool Result { get; set; }

        public string Message { get; set; }
    }
}
