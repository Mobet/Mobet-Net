﻿using System;
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

using Mobet.Domain.UnitOfWork.ConventionalRegistras;
using Mobet.EntityFramework.Startup;
using Mobet.Auditing.Startup;
using Mobet.AutoMapper.Startup;
using Mobet.Services.SettingProviders;
using Mobet.Services;
using Mobet.Localization;
using Mobet.GlobalSettings.Store;
using Autofac;
using Autofac.Extras.DynamicProxy2;

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

            StartupConfig.RegisterDependency(config =>
            {
                //应用程序配置
                config.GlobalSettingsConfiguration.Providers.Add<AuthorizationSettingProvider>();
                config.GlobalSettingsConfiguration.Providers.Add<LocalizationSettingProvider>();
                config.GlobalSettingsConfiguration.Providers.Add<ResourcesSettingProvider>();
                config.GlobalSettingsConfiguration.Providers.Add<MessageSettingProvider>();

                //本地化
                config.LocalizationConfiguration.Sources.Add(new DictionaryBasedLocalizationSource(Constants.Localization.SourceName.Messages, new XmlEmbeddedFileLocalizationDictionaryProvider(Assembly.GetExecutingAssembly(), Constants.Localization.RootNamespace.Messages)));
                config.LocalizationConfiguration.Sources.Add(new DictionaryBasedLocalizationSource(Constants.Localization.SourceName.Events, new XmlEmbeddedFileLocalizationDictionaryProvider(Assembly.GetExecutingAssembly(), Constants.Localization.RootNamespace.Events)));
                config.LocalizationConfiguration.Sources.Add(new DictionaryBasedLocalizationSource(Constants.Localization.SourceName.Scopes, new XmlEmbeddedFileLocalizationDictionaryProvider(Assembly.GetExecutingAssembly(), Constants.Localization.RootNamespace.Scopes)));
                config.LocalizationConfiguration.Sources.Add(new DictionaryBasedLocalizationSource(Constants.Localization.SourceName.Views, new XmlEmbeddedFileLocalizationDictionaryProvider(Assembly.GetExecutingAssembly(), Constants.Localization.RootNamespace.Views)));

                //EF 连接字符串
                config.UseDataAccessEntityFramework(cfg =>
                {
                    cfg.DefaultNameOrConnectionString = "Mobet.Authorization";
                });

                config
                   .UseAuditing()
                   .UseAutoMapper();

                config.RegisterWebMvcApplication(new ConventionalRegistrarConfig());
            });
        }

        public class ConventionalRegistrarConfig : Autofac.Module
        {
            protected override void Load(Autofac.ContainerBuilder builder)
            {
                builder.RegisterType<GlobalSettingStore>()
                    .AsImplementedInterfaces()
                    .EnableClassInterceptors()
                    .InterceptedBy(typeof(UnitOfWorkInterceptor))
                    .InstancePerDependency();
                base.Load(builder);
            }
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
