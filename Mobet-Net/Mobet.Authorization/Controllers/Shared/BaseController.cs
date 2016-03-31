using System;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Mobet.Localization;
using Mobet.Runtime.Session;
using Mobet.GlobalSettings;
using Mobet.Localization.Sources;
using Mobet.Authorization.Models;

namespace Mobet.Authorization.Controllers.Shared
{
    public class BaseController : Controller
    {
        public IAppSession AppSession { get; set; }

        public ILocalizationManager LocalizationManager { get; set; }

        public IGlobalSettingManager SettingManager { get; set; }

        public BaseController()
        {
            LocalizationManager = NullLocalizationManager.Instance;
            AppSession = NullAppSession.Instance;
        }

        #region [    Localization    ]

        protected string LocalizationSourceName { get; set; }

        protected ILocalizationSource LocalizationSource
        {
            get
            {
                if (LocalizationSourceName == null)
                {
                    throw new Exception("Must set LocalizationSourceName before, in order to get LocalizationSource");
                }

                if (_localizationSource == null || _localizationSource.Name != LocalizationSourceName)
                {
                    _localizationSource = LocalizationManager.GetSource(LocalizationSourceName);
                }

                return _localizationSource;
            }
        }

        private ILocalizationSource _localizationSource;

        protected virtual string L(string name)
        {
            return LocalizationSource.GetString(name);
        }

        protected string L(string name, params object[] args)
        {
            return LocalizationSource.GetString(name, args);
        }

        protected virtual string L(string name, CultureInfo culture)
        {
            return LocalizationSource.GetString(name, culture);
        }

        protected string L(string name, CultureInfo culture, params object[] args)
        {
            return LocalizationSource.GetString(name, culture, args);
        }

        #endregion

        #region [    JsonResult    ]

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return new JsonResult { Data = data, ContentType = contentType, ContentEncoding = contentEncoding };
        }

        public new JsonResult Json(object data, JsonRequestBehavior jsonRequest)
        {
            return new NewtonJsonResult { Data = data, JsonRequestBehavior = jsonRequest };
        }

        public new JsonResult Json(object data)
        {
            return new NewtonJsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion
    }
}