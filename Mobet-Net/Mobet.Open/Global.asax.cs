using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Mobet.Open
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            new Bootstrapper()
                .RegisterDataAccessEntityFramework()
                .RegisterWebApiControllers()
                .RegisterAutoMapper()
                .RegisterLoggingLog4net()
                .RegisterCacheProviderRedis();
                //.Configuration(x => x.DefaultNameOrConnectionString, "Mobet.Authorization");
                //.Configuration(x => x.EnableAuditing,true);
        }
    }
}
