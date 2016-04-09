using Mobet.Auditing.Attributes;
using Mobet.Authorization.Models;
using Mobet.Localization;
using Mobet.Localization.Language;
using Mobet.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mobet.Authorization.Controllers.Shared
{
    public class LocalizationController : BaseController
    {
        [DisableAuditing]
        public virtual ActionResult ChangeCulture(string cultureName, string returnUrl = "")
        {
            if (!LocalizationHelper.IsValidCultureCode(cultureName))
            {
                throw new Exception("Unknown language: " + cultureName + ". It must be a valid culture!");
            }

            Response.Cookies.Add(new HttpCookie("Mobet.Localization.CultureName", cultureName) { Expires = DateTime.Now.AddYears(2) });

            if (Request.IsAjaxRequest())
            {
                return Json(new AjaxResponse<object>(), JsonRequestBehavior.AllowGet);
            }

            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect(Request.ApplicationPath);
        }
    }
}