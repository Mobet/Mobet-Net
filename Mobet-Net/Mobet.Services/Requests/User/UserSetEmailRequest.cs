using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Services.Requests.User
{

    public class UserSetEmailRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public string Id { get; set; }
        /// <summary>
        /// 新邮箱
        /// </summary>
        [Required]
        public string Email { get; set; }
    }

    public class UserSetEmailResponse : BooleanResponse
    {
        public UserSetEmailResponse(bool result, string message)
           : base(result, message)
        {
        }
    }
}
