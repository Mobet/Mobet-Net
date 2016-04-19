using IdentityServer3.Core.Models;
using IdentityServer3.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core;
using Mobet.Authorization.Models;
using System.Drawing;
using System.IO;
using Mobet.Runtime.Cookie;
using System.Drawing.Imaging;
using Mobet.Authorization.Controllers.Shared;
using Mobet.Web.Models;

using Constants = Mobet.Services.Constants;
using IUserService = Mobet.Services.Services.IUserService;
using Mobet.Runtime.Security;
using Mobet.Services.Services;
using System.Threading.Tasks;
using Mobet.Caching;
using Mobet.Services;
using Mobet.Services.Requests.User;
using Mobet.AutoMapper;
using Mobet.Runtime.Session;
using System.Threading;
using Newtonsoft.Json;
using IdentityServer3.Core.Services;

using System.Web.Http.Owin;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Configuration.Hosting;
using Mobet.Services.Requests.Captcha;
using Mobet.Extensions;

namespace Mobet.Authorization.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ICacheManager cacheManager;
        private readonly ICaptchaService captchaService;
        private readonly IUserService userService;

        public AccountController(ICaptchaService messageService, ICacheManager cacheManager, IUserService userService)
        {
            this.captchaService = messageService;
            this.cacheManager = cacheManager;
            this.userService = userService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginViewModel model, SignInMessage message)
        {
            return this.View(model);
        }
        public ActionResult Registration(string signin)
        {
            return View();
        }

        public ActionResult Logout(LogoutViewModel model)
        {
            return this.View(model);
        }
        public ActionResult LoggedOut(LoggedOutViewModel model)
        {
            return this.View(model);
        }
        public ActionResult SignOut()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return View();
        }
        public ActionResult Consent(ConsentViewModel model)
        {
            return this.View(model);
        }
        /// <summary>
        /// 绑定授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Permissions(ClientPermissionsViewModel model)
        {
            return View(model);
        }
        public virtual ActionResult Error(Web.Models.ErrorViewModel model)
        {
            return this.View(model);
        }

        [HttpGet]
        public ActionResult GetCaptcha()
        {
            return File(Runtime.Cookie.Captcha.GetBytes(Constants.CookieNames.CaptchaSignup), @"image/jpeg");
        }



        [HttpPost]
        public async Task<JsonResult> ValidateCaptchaAndSendMessageCode(string telphone, string captcha)
        {
            if (string.IsNullOrEmpty(telphone))
            {
                return Json(new MvcAjaxResponse(false, "无效的手机号码"));
            }

            if (CryptoManager.DecryptDES(CookieManager.GetCookieValue(Constants.CookieNames.CaptchaSignup)) != captcha)
            {
                return Json(new MvcAjaxResponse(false, "错误的验证码"));
            }

            var response = await captchaService.MessageCaptchaSendAsync(new MessageCaptchaSendRequest
            {
                Captcha = Mobet.Services.MessageCaptcha.Register,
                Telphone = telphone,
                ExpiredTime = 30
            });

            return Json(new MvcAjaxResponse(response.Result, response.Message));
        }

        [HttpPost]
        public JsonResult ValidateMessageCaptcha(string captcha, string telphone)
        {
            if (this.cacheManager.Get<string>(string.Format(Constants.CacheNames.MessageCaptcha, Mobet.Services.MessageCaptcha.Register, telphone, captcha)) != captcha)
            {
                return Json(new MvcAjaxResponse(false, "短信验证码无效"));
            }

            return Json(new MvcAjaxResponse("短信验证码验证成功"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(string signin, LocalRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = userService.RegisterByTelphone(model.MapTo<UserRegisterByTelphoneRequest>());
                if (response.Result)
                {
                    return Redirect("/core/" + IdentityServer3.Core.Constants.RoutePaths.Login + "?signin=" + signin);
                }

                model.ErrorMessage = response.Message;
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult GetUserProfileData()
        {
            var model = userService.GetUserProfileData(new UserGetProfileDataRequest { UserId = AppSession.UserId.TryInt(0) });
            return Json(new MvcAjaxResponse(model));
        }
    }
}