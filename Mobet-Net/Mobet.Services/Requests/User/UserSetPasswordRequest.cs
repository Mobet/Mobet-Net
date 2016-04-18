using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Services.Requests.User
{
    public class UserSetPasswordRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public string Id { get; set; }
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
    }


    public class UserSetPasswordResponse
    {

        public UserSetPasswordResponse()
        {

        }

        public UserSetPasswordResponse(bool result, string message)
        {
            Result = result;
            Message = message;
        }

        public bool Result { get; set; }

        public string Message { get; set; }
    }
}
