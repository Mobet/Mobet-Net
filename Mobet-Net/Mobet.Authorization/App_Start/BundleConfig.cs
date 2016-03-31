using Mobet.Authorization.Configuration;
using Mobet.Dependency;
using Mobet.GlobalSettings;
using System.Web;
using System.Web.Optimization;

namespace Mobet.Authorization
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/biz/scripts").IncludeDirectory("~/scripts", "*.js", true));
        }

    }
}
