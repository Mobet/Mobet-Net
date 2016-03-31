using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Compilation;
using System.Configuration;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;
using System.Threading;

using Mobet;
using Mobet.Infrastructure;
using Mobet.Authorization.Configuration;
using Mobet.Dependency;
using Mobet.Configuration.Startup;
using Mobet.Localization.Dictionaries;
using Mobet.Localization.Dictionaries.Xml;
using Mobet.Localization.Settings;
using Mobet.Localization.Language;
using Mobet.Extensions;

using Mobet.EntityFramework.Startup;
using Mobet.Auditing.Startup;
using Mobet.AutoMapper.Startup;
using Mobet.Services.SettingProviders;
using Mobet.Services;
using Mobet.Localization;

namespace Mobet.Authorization
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            StartupConfig.RegisterDependency(cfg =>
            {
                //应用程序配置
                cfg.UseGlobalSettings(config =>
                {
                    config.Providers.Add<EmailSettingProvider>();
                    config.Providers.Add<LocalizationSettingProvider>();
                    config.Providers.Add<ResourcesSettingProvider>();
                });
                //本地化
                cfg.UseLocalization(config =>
                {
                    config.Sources.Add(new DictionaryBasedLocalizationSource(Constants.Localization.SourceName.Messages, new XmlEmbeddedFileLocalizationDictionaryProvider(Assembly.GetExecutingAssembly(), Constants.Localization.RootNamespace.Messages)));
                    config.Sources.Add(new DictionaryBasedLocalizationSource(Constants.Localization.SourceName.Events, new XmlEmbeddedFileLocalizationDictionaryProvider(Assembly.GetExecutingAssembly(), Constants.Localization.RootNamespace.Events)));
                    config.Sources.Add(new DictionaryBasedLocalizationSource(Constants.Localization.SourceName.Scopes, new XmlEmbeddedFileLocalizationDictionaryProvider(Assembly.GetExecutingAssembly(), Constants.Localization.RootNamespace.Scopes)));
                    config.Sources.Add(new DictionaryBasedLocalizationSource(Constants.Localization.SourceName.Views, new XmlEmbeddedFileLocalizationDictionaryProvider(Assembly.GetExecutingAssembly(), Constants.Localization.RootNamespace.Views)));

                });
                //EF 连接字符串
                cfg.UseDataAccessEntityFramework(config =>
                {
                    config.DefaultNameOrConnectionString = "Mobet.Authorization";
                });

                cfg
                   .UseAppSession()
                   .UseAuditing()
                   .UseAutoMapper();

                cfg.RegisterWebMvcApplication();

            });
        }


        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
            var langCookie = Request.Cookies["Mobet.Localization.CultureName"];
            if (langCookie != null && LocalizationHelper.IsValidCultureCode(langCookie.Value))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(langCookie.Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(langCookie.Value);
            }
            else if (!Request.UserLanguages.IsNullOrEmpty())
            {
                var firstValidLanguage = Request
                    .UserLanguages
                    .FirstOrDefault(LocalizationHelper.IsValidCultureCode);

                if (firstValidLanguage != null)
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(firstValidLanguage);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(firstValidLanguage);
                }
            }
        }
    }
}
