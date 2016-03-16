using IdentityServer3.Core.Models;
using IdentityServer3.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdentityServer3.Core.Extensions;

namespace Mobet.Authorization.Controllers
{
    public class AccountController : AuthorizationController
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

    }
}