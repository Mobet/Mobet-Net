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
using Mobet.Runtime.Security;
using Mobet.Services.Services;
using Mobet.Services.Requests.Message;
using System.Threading.Tasks;
using Mobet.Caching;
using Mobet.Services;
using Mobet.Services.Requests.User;
using Mobet.AutoMapper;
using Mobet.Runtime.Session;
using System.Threading;
using Newtonsoft.Json;

namespace Mobet.Authorization.Controllers
{
    public class AccountController : BaseController
    {
        public ICacheManager CacheManager { get; set; }
        public IMessageService MessageService { get; set; }
        public IUserService UserService { get; set; }

        public AccountController(IMessageService messageService, ICacheManager cacheManager, IUserService userService)
        {
            MessageService = messageService;
            CacheManager = cacheManager;
            UserService = userService;
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
        public ActionResult Permissions(ClientPermissionsViewModel model)
        {
            return this.View(model);
        }
        public virtual ActionResult Error(Web.Models.ErrorViewModel model)
        {
            return this.View(model);
        }

        [HttpGet]
        public ActionResult GetCaptcha()
        {
            return File(Captcha.GetBytes(Constants.CookieNames.Captcha), @"image/jpeg");
        }

        [HttpPost]
        public async Task<JsonResult> ValidateCaptchaAndSendMessageCode(string telphone, string captcha)
        {
            if (string.IsNullOrEmpty(telphone))
            {
                return Json(new MvcAjaxResponse(false, "无效的手机号码"));
            }
            if (CryptoManager.DecryptDES(CookieManager.GetCookieValue(Constants.CookieNames.Captcha)) != captcha)
            {
                return Json(new MvcAjaxResponse(false, "错误的验证码"));
            }

            var response = await MessageService.MessageCaptchaSendAsync(new MessageCaptchaSendRequest
            {
                MessageCaptcha = Mobet.Services.MessageCaptcha.Register,
                Telphone = telphone
            });

            return Json(new MvcAjaxResponse(response.Result, response.Message));
        }

        [HttpPost]
        public JsonResult ValidateMessageCaptcha(string captcha, string telphone)
        {
            if (CacheManager.Get<string>(string.Format(Constants.CacheNames.MessageCaptcha, MessageCaptcha.Register, telphone, captcha)) != captcha)
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
                var response = UserService.RegisterByTelphone(model.MapTo<UserRegisterByTelphoneRequest>());
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
            var model = UserService.GetUserProfileData(new UserGetProfileDataRequest { UserId = AppSession.UserId });
            return Json(new MvcAjaxResponse(model));
        }
    }
}