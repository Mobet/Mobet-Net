using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Mobet;
using Mobet.Dependency;
using Mobet.Configuration.Startup;
using System.Configuration;
using Mobet.Localization.Dictionaries;
using Mobet.Localization.Dictionaries.Xml;
using System.Reflection;
using Mobet.Application.SettingProviders;
using Mobet.Localization.Settings;

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

            Bootstrapper cfg = new Bootstrapper();
            cfg.RegisterWebMvcApplication();

            cfg.StartupConfiguration.LocalizationConfiguration.Sources.Add(
             new DictionaryBasedLocalizationSource(
                 "Demo",
                 new XmlEmbeddedFileLocalizationDictionaryProvider(
                     Assembly.GetExecutingAssembly(), "Mobet.Authorization.Localization.XmlSources"
                     )));

            cfg.StartupConfiguration.SettingsConfiguration.Providers.Add<EmailSettingProvider>();
            cfg.StartupConfiguration.SettingsConfiguration.Providers.Add<LocalizationSettingProvider>();



            cfg.UseDataAccessEntityFramework()
                .UseEventBus()
                .UseCacheProviderRedis("172.30.30.190:6379,allowAdmin=true")
                .UseAuditing()
                .UseAutoMapper()
                .UseAppSession()
             ;


        }
    }
}
