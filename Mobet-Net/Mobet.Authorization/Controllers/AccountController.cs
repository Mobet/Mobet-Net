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

namespace Mobet.Authorization.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
        {

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(string signin, LocalRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = new LocalRegistrationUserService.CustomUser
                //{
                //    Username = model.Username,
                //    Password = model.Password,
                //    Subject = Guid.NewGuid().ToString(),
                //    Claims = new List<Claim>()
                //};
                //LocalRegistrationUserService.Users.Add(user);
                //user.Claims.Add(new Claim(Constants.ClaimTypes.GivenName, model.First));
                //user.Claims.Add(new Claim(Constants.ClaimTypes.FamilyName, model.Last));

                return Redirect("/core/" + Constants.RoutePaths.Login + "?signin=" + signin);
            }

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

        public ActionResult Consent(ConsentViewModel model)
        {
            return this.View(model);
        }
        public ActionResult Permissions(ClientPermissionsViewModel model)
        {
            return this.View(model);
        }
        public virtual ActionResult Error(ErrorViewModel model)
        {
            return this.View(model);
        }

        [HttpGet]
        public ActionResult GetRadomCode()
        {
            var bytes = CreateRandomCode("__Mobet.Signup.CheckCode");
            return File(bytes, @"image/jpeg");
        }

        public static byte[] CreateRandomCode(string cookieName)
        {
            var img = new Bitmap(150, 50);
            var g = Graphics.FromImage(img);
            try
            {
                var r = new Random();
                var MyColor = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
                g.Clear(Color.White);
                for (var i = 0; i < 10; i++)
                {
                    MyColor = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
                    img.SetPixel(r.Next(150), r.Next(50), MyColor);  //产生10个随机颜色的杂点
                }
                var str = "";    //存储产生的5位验证码
                for (var i = 1; i <= 5; i++)
                {
                    var s = GetString()[r.Next(GetString().Length)];
                    MyColor = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
                    var myBrush = new SolidBrush(MyColor);
                    g.DrawString(s, new Font("Algerian", r.Next(20, 32),FontStyle.Italic), myBrush, 30 * (i - 1), r.Next(13));
                    str += s;
                }

                //设置cookie
                CookieHelper.CreateCookie(cookieName, str, DateTime.MaxValue);

                for (var i = 1; i <= 5; i++)
                {
                    MyColor = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
                    var p = new Pen(MyColor, r.Next(5));
                    g.DrawLine(p, r.Next(150), r.Next(50), r.Next(150), r.Next(50));
                    g.DrawLine(p, r.Next(170), r.Next(180), r.Next(150), r.Next(150));
                }

                //保存图片数据
                using (var stream = new MemoryStream())
                {
                    img.Save(stream, ImageFormat.Jpeg);
                    //输出图片流
                    return stream.ToArray();
                }
            }
            finally
            {
                g.Dispose();
                img.Dispose();
            }
        }

        /// <summary>
        /// 返回除字母O之外的25个字母和数字0之外的9个数字
        /// </summary>
        /// <returns></returns>
        private static string[] GetString()
        {
            string[] Arr = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            return Arr;
        }
    }
}