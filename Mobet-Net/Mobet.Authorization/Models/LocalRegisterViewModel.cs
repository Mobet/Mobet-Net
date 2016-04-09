using IdentityServer3.Core.ViewModels;
using Mobet.Services.Requests.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mobet.Authorization.Models
{
    [AutoMapper.AutoMap(typeof(UserRegisterByTelphoneRequest))]
    public class LocalRegisterViewModel : ErrorViewModel
    {

        [Required]
        public string Telphone { get; set; }

        [Required]
        public string Password { get; set; }
    }
}