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
using Mobet.Services.SettingProviders;
using Mobet.Localization.Settings;
using Mobet.Authorization.Configuration;
using Mobet.Infrastructure;
using System.Web.Compilation;
using System.Threading.Tasks;

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
                cfg.Configuration(c =>
                {
                    c.EntityFrameworkConfiguration.DefaultNameOrConnectionString = "Mobet.Authorization";
                    c.LocalizationConfiguration.Sources.Add(
                        new DictionaryBasedLocalizationSource(
                            "AccountService", 
                            new XmlEmbeddedFileLocalizationDictionaryProvider(
                                Assembly.GetExecutingAssembly(), 
                                "Mobet.Configuration.Authorization.Localization.AccountService"
                            )
                        )
                     );

                    c.SettingsConfiguration.Providers.Add<EmailSettingProvider>();
                    c.SettingsConfiguration.Providers.Add<LocalizationSettingProvider>();
                    c.SettingsConfiguration.Providers.Add<ResourcesSettingProvider>();

                });

                cfg.UseDataAccessEntityFramework()
                   .UseEventBus()
                   .UseCacheProviderNetCache()
                   .UseAuditing()
                   .UseAutoMapper()
                   .UseAppSession();


                cfg.RegisterWebMvcApplication();

            });
        }
    }
}
