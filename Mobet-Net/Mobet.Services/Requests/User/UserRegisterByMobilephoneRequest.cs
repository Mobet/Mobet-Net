using Mobet.AutoMapper;
using Mobet.Domain.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Services.Requests.User
{
    [AutoMapTo(typeof(Domain.Entities.User),typeof(Requests.User.UserCreateRequest))]
    public class UserRegisterByTelphoneRequest : IRequest
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        [Required]
        public string Telphone { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }

        [Required]
        public string Sign { get; set; }
    }

    public class UserRegisterByTelphoneResponse : IResponse
    {
        public bool Result { get; set; }

        public string Message { get; set; }
    }
}
