using Mobet.Runtime.Cookie;
using Mobet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mobet.Authorization.Controllers.Shared
{
    public class CaptchaController : Controller
    {
        public ActionResult GetCaptcha(string type)
        {
            switch (type)
            {
                case "SIGNUP":
                    return File(Captcha.GetBytes(Constants.CookieNames.CaptchaSignup), @"image/jpeg");
                case "SET_PASSWORD":
                    return File(Captcha.GetBytes(Constants.CookieNames.CaptchSetPassword), @"image/jpeg");
                default:
                    return File(Captcha.GetBytes(Constants.CookieNames.CaptchaSignup), @"image/jpeg");
            }
        }
    }
}