using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Mobet.Extensions;
using Mobet.Settings;
using Mobet.Localization;
using Mobet.Localization.Sources;
using Mobet.Dependency;
using System.Globalization;

namespace Mobet.Authorization.Controllers.Shared
{
    /// <summary>
    /// Base class for all views in system.
    /// </summary>
    public abstract class AbstractWebViewPage : AbstractWebViewPage<dynamic>
    {

    }
    public abstract class AbstractWebViewPage<TModel> : WebViewPage<TModel>
    {
        /// <summary>
        /// Gets the root path of the application.
        /// </summary>
        public string ApplicationPath
        {
            get
            {
                var appPath = HttpContext.Current.Request.ApplicationPath;
                if (appPath == null)
                {
                    return "/";
                }

                appPath = appPath.EnsureEndsWith('/');

                return appPath;
            }
        }

        /// <summary>
        /// Reference to the setting manager.
        /// </summary>
        public ISettingManager SettingManager { get; set; }

        /// <summary>
        /// Gets/sets name of the localization source that is used in this controller.
        /// It must be set in order to use <see cref="L(string)"/> and <see cref="L(string,CultureInfo)"/> methods.
        /// </summary>
        protected string LocalizationSourceName
        {
            get { return _localizationSource.Name; }
            set { _localizationSource = LocalizationHelper.GetSource(value); }
        }
        private ILocalizationSource _localizationSource;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected AbstractWebViewPage()
        {
            _localizationSource = NullLocalizationSource.Instance;

            SettingManager = IocManager.Instance.Resolve<ISettingManager>();
        }

        /// <summary>
        /// Gets localized string for given key name and current language.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <returns>Localized string</returns>
        protected virtual string L(string name)
        {
            return _localizationSource.GetString(name);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Localized string</returns>
        protected virtual string L(string name, params object[] args)
        {
            return _localizationSource.GetString(name, args);
        }

        /// <summary>
        /// Gets localized string for given key name and specified culture information.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <returns>Localized string</returns>
        protected virtual string L(string name, CultureInfo culture)
        {
            return _localizationSource.GetString(name, culture);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Localized string</returns>
        protected string L(string name, CultureInfo culture, params object[] args)
        {
            return _localizationSource.GetString(name, culture, args);
        }

        /// <summary>
        /// Gets localized string from given source for given key name and current language.
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="name">Key name</param>
        /// <returns>Localized string</returns>
        protected virtual string Ls(string sourceName, string name)
        {
            return LocalizationHelper.GetSource(sourceName).GetString(name);
        }

        /// <summary>
        /// Gets localized string from given source  for given key name and current language with formatting strings.
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="name">Key name</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Localized string</returns>
        protected virtual string Ls(string sourceName, string name, params object[] args)
        {
            return LocalizationHelper.GetSource(sourceName).GetString(name, args);
        }

        /// <summary>
        /// Gets localized string from given source  for given key name and specified culture information.
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <returns>Localized string</returns>
        protected virtual string Ls(string sourceName, string name, CultureInfo culture)
        {
            return LocalizationHelper.GetSource(sourceName).GetString(name, culture);
        }

        /// <summary>
        /// Gets localized string from given source  for given key name and current language with formatting strings.
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Localized string</returns>
        protected virtual string Ls(string sourceName, string name, CultureInfo culture, params object[] args)
        {
            return LocalizationHelper.GetSource(sourceName).GetString(name, culture, args);
        }
    }
}