using Mobet.Authorization.Controllers.Shared;
using Mobet.Authorization.Models;
using Mobet.Authorization.Models.Passport;
using Mobet.Caching;
using Mobet.Extensions;
using Mobet.Net.Mail;
using Mobet.Runtime.Cookie;
using Mobet.Runtime.Security;
using Mobet.Services;
using Mobet.Services.Requests.Captcha;
using Mobet.Services.Requests.User;
using Mobet.Services.Services;
using Mobet.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mobet.Authorization.Controllers
{
    [Authorize]
    public class PassportController : BaseController
    {

        private readonly IUserService userService;
        private readonly ICaptchaService captchaService;
        private readonly ICacheManager cacheManager;
        private readonly IEmailSender emailSender;

        public PassportController(IUserService userService, ICacheManager cacheManager, ICaptchaService captchaService, IEmailSender emailSender)
        {
            this.cacheManager = cacheManager;
            this.captchaService = captchaService;
            this.userService = userService;
            this.emailSender = emailSender;
        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        #region [    Security    ]

        /// <summary>
        /// 账号安全
        /// </summary>
        /// <returns></returns>
        public ActionResult Security(PassportSecurityViewModel model)
        {
            var user = userService.GetUserProfileData(new UserGetProfileDataRequest { UserId = AppSession.UserId.TryInt(0) }).Model ?? new Mobet.Services.Models.User();
            model.Email = user.Email;
            model.Telphone = user.Telphone;
            return View(model);
        }
        /// <summary>
        /// 设置/修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult PasswordDetail()
        {
            return View();
        }
        /// <summary>
        /// 绑定/修改手机号码
        /// </summary>
        /// <returns></returns>
        public ActionResult TelphoneDetail()
        {
            return View();
        }
        /// <summary>
        /// 绑定/修改邮箱
        /// </summary>
        /// <returns></returns>
        public ActionResult EmailDetail()
        {
            return View();
        }
        /// <summary>
        /// 设置/修改密保问题
        /// </summary>
        /// <returns></returns>
        public ActionResult QuestionDetail()
        {
            return View();
        }
        /// <summary>
        /// 设置/修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult SetPassword(UserSetPasswordViewModel model)
        {
            var captcha = CryptoManager.DecryptDES(CookieManager.GetCookieValue(Constants.CookieNames.CaptchSetPassword));
            if (captcha != model.Captcha)
            {
                return Json(new MvcAjaxResponse(false, "错误的验证码"));
            }
            var response = userService.SetPassword(new UserSetPasswordRequest
            {
                Id = AppSession.UserId,
                OldPassword = model.OldPassword,
                Password = model.Password
            });
            return Json(new MvcAjaxResponse(response.Result, response.Message));
        }

        /// <summary>
        /// 发送邮件验证码
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> EmailCaptchaSendAsync(string email)
        {
            var response = await captchaService.EmailCaptchaSendAsync(new EmailCaptchaSendRequest
            {
                Captcha = EmailCaptcha.Bind,
                Email = email,
                ExpiredTime = 30
            });

            return Json(new MvcAjaxResponse(response.Result, response.Message));
        }
        /// <summary>
        /// 验证/绑定邮箱
        /// </summary>
        /// <param name="email"></param>
        /// <param name="captcha"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ValidateEmailCaptchaAsync(string email, string captcha)
        {
            if (cacheManager.Get<string>(string.Format(Constants.CacheNames.EmailCaptcha, EmailCaptcha.Bind, email, captcha)) != captcha)
            {
                return Json(new MvcAjaxResponse(false, "邮件验证码无效"));
            }
            var response = userService.SetEmail(new UserSetEmailRequest { Id = AppSession.UserId, Email = email });
            return Json(new MvcAjaxResponse(response.Result, response.Message));
        }

        #endregion

        /// <summary>
        /// 个人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Profile()
        {
            return View();
        }

        /// <summary>
        /// 获取设置密码图片验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCaptcha()
        {
            return File(Captcha.GetBytes(Constants.CookieNames.CaptchSetPassword), @"image/jpeg");
        }


    }
}