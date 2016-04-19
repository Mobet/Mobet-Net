using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Services.Requests.User
{
    public class UserGetProfileDataRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public int UserId { get; set; }
    }

    public class UserGetProfileDataResponse
    {
        public Models.User Model { get; set; }
    }

}
