using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Compilation;

using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;

using Castle.Core.Logging;
using Castle.Services.Logging.Log4netIntegration;

using Mobet.Dependency;
using Mobet.Dependency.ConventionalRegistrars;
using Mobet.Modules.Logging;
using Mobet.Runtime.Session.Modules;
using Mobet.Caching;
using Mobet.Domain.UnitOfWork.Configuration;
using Mobet.Settings.Configuration;
using Mobet.Settings.Modules;
using Mobet.Settings;
using Mobet.Settings.Store;

namespace Mobet.Configuration.Startup
{

    public static class StartupConfig
    {
        public static void RegisterDependency(Action<StartupConfiguration> invoke)
        {
            invoke(new StartupConfiguration());
        }
    }
    public class StartupConfiguration
    {
        /// <summary>
        /// Used to configure unit of work defaults.
        /// </summary>
        public IUnitOfWorkDefaultOptionsConfiguration UnitOfWorkDefaultOptionsConfiguration { get { return IocManager.Instance.Resolve<IUnitOfWorkDefaultOptionsConfiguration>(); } }
        /// <summary>
        /// Used to configure setting system.
        /// </summary>
        public ISettingsConfiguration SettingsConfiguration { get { return IocManager.Instance.Resolve<ISettingsConfiguration>(); } }

        public StartupConfiguration()
        {
            IocManager.Instance.AddConventionalRegistrar(new BasicConventionalRegistrar());

            IocManager.Instance.RegisterIfNot<IUnitOfWorkDefaultOptionsConfiguration, UnitOfWorkDefaultOptionsConfiguration>();
            IocManager.Instance.RegisterIfNot<ISettingsConfiguration, SettingsConfiguration>();
        }

    }

    public static class BootstrapperExtensions
    {
        private const string AssemblySkipLoadingPattern = "^System|^vshost32|^Nito.AsyncEx|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^NSubstitute|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Telerik|^Iesi|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease";

        public static StartupConfiguration UseCacheProviderRedis(this StartupConfiguration bootstrap, string configuration)
        {
            IocManager.Instance.RegisterWithParameters<ICacheManager, RedisCacheManager>("configuration", configuration);
            return bootstrap;
        }
        public static StartupConfiguration UseCacheProviderNetCache(this StartupConfiguration bootstrap)
        {
            IocManager.Instance.Register<ICacheManager, Caching.NetCacheManager>();
            return bootstrap;
        }
        public static StartupConfiguration UseLoggingLog4net(this StartupConfiguration bootstrap, string configFile = "log4net.config")
        {
            IocManager.Instance.RegisterWithParameters<ILoggerFactory, Log4netFactory>("configFile", configFile);
            IocManager.Instance.RegisterModule(new LoggingModule());
            return bootstrap;
        }
        public static StartupConfiguration UseAppSession(this StartupConfiguration bootstrap)
        {
            IocManager.Instance.RegisterModule(new AppSessionModule());
            return bootstrap;
        }

        public static StartupConfiguration UseSettings(this StartupConfiguration bootstrap, Action<ISettingsConfiguration> invoke)
        {
            IocManager.Instance.RegisterIfNot<ICacheManager, NetCacheManager>();
            IocManager.Instance.RegisterIfNot<ISettingManager, SettingManager>();
            IocManager.Instance.RegisterIfNot<ISettingStore, SimpleSettingStore>(DependencyLifeStyle.Transient);

            invoke(IocManager.Instance.Resolve<ISettingsConfiguration>());

            IocManager.Instance.RegisterModule(new SettingManagerModule());

            return bootstrap;
        }

        public static StartupConfiguration RegisterWebMvcApplication(this StartupConfiguration bootstrap, params IModule[] modules)
        {
            var assemblies = FilterSystemAssembly(BuildManager.GetReferencedAssemblies().Cast<Assembly>());

            IocManager.Instance.AddConventionalRegistrar(new ControllerConventionalRegistrar());
            IocManager.Instance.RegisterAssemblyByConvention(assemblies, modules);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(IocManager.Instance.IocContainer));

            return bootstrap;
        }
        public static StartupConfiguration RegisterWebApiApplication(this StartupConfiguration bootstrap, params IModule[] modules)
        {
            var assemblies = FilterSystemAssembly(BuildManager.GetReferencedAssemblies().Cast<Assembly>());
            HttpConfiguration configuration = GlobalConfiguration.Configuration;

            IocManager.Instance.AddConventionalRegistrar(new ControllerConventionalRegistrar());
            IocManager.Instance.AddConventionalRegistrar(new ApiControllerConventionalRegistrar());
            IocManager.Instance.RegisterAssemblyByConvention(assemblies, modules);

            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(IocManager.Instance.IocContainer);
            return bootstrap;
        }
        public static StartupConfiguration RegisterConsoleApplication(this StartupConfiguration bootstrap, params IModule[] modules)
        {
            IocManager.Instance.RegisterAssemblyByConvention(AppDomain.CurrentDomain.GetAssemblies());
            return bootstrap;
        }

        private static Assembly[] GetAssemblies()
        {
            var path = GetPhysicalPath(AppDomain.CurrentDomain.BaseDirectory);
            return FilterSystemAssembly(GetAssemblies(path)).ToArray();
        }
        private static List<string> GetAllFiles(string directoryPath)
        {
            return Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories).ToList();
        }
        private static List<Assembly> GetAssemblies(string directoryPath)
        {
            var filePaths = GetAllFiles(directoryPath).Where(t => t.EndsWith(".exe") || t.EndsWith(".dll"));
            return filePaths.Select(Assembly.LoadFile).ToList();
        }
        private static string GetPhysicalPath(string relativePath)
        {
            if (HttpContext.Current == null)
            {
                if (relativePath.StartsWith("~"))
                {
                    relativePath = relativePath.Remove(0, 2);
                }
                return Path.GetFullPath(relativePath);
            }
            if (relativePath.StartsWith("~"))
            {
                return HttpContext.Current.Server.MapPath(relativePath);
            }

            if (relativePath.StartsWith("/") || relativePath.StartsWith("\\"))
            {
                return HttpContext.Current.Server.MapPath("~" + relativePath);
            }
            if (HttpContext.Current != null)
            {
                return relativePath + "bin";
            }
            return HttpContext.Current.Server.MapPath("~/" + relativePath);
        }
        private static Assembly[] FilterSystemAssembly(IEnumerable<Assembly> assemblies)
        {
            return assemblies
                .Where(assembly => !Regex.IsMatch(assembly.FullName, AssemblySkipLoadingPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled))
                .ToArray();
        }

    }
}
