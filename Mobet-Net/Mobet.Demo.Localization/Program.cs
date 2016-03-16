using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using Mobet.Localization.Dictionaries.Xml;
using Mobet.Dependency;
using Mobet.Localization.Configuration;
using Mobet.Localization.Dictionaries;
using Mobet.Localization.Settings;
using Mobet.Localization;
using Mobet.Localization.Sources;

using Mobet.Localization.Extensions;
using Mobet.Localization.Dictionaries.Json;
using System.Globalization;
using System.Threading;
using Mobet.Localization.Sources.Resource;
using Mobet.Demo.Localization.Localization.ResourceSerouces;
using Mobet.Settings.Configuration;
using Mobet.Configuration.Startup;

namespace Mobet.Demo.Localization
{
    class Program
    {

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
            Bootstrapper boot = new Bootstrapper();
            boot.RegisterConsoleApplication();

            boot.StartupConfiguration.SettingsConfiguration.Providers.Add<LocalizationSettingProvider>();
            boot.StartupConfiguration.LocalizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    "Demo",
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(), "Mobet.Demo.Localization.Localization.XmlSources"
                        )));
            boot.StartupConfiguration.LocalizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    "Lang",
                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                         "Mobet.Demo.Localization.Localization.JsonSources"
                        )));
            boot.StartupConfiguration.LocalizationConfiguration.Sources.Add(new ResourceFileLocalizationSource("Res", Res.ResourceManager));

            ILocalizationManager localizationManager = IocManager.Instance.Resolve<ILocalizationManager>();

            ILocalizationSource _localizationSource = localizationManager.GetSource("Demo");
            ILocalizationSource _localizationSource2 = localizationManager.GetSource("Lang");
            ILocalizationSource _localizationSource3 = localizationManager.GetSource("Res");

            var helloWorld = _localizationSource.GetString(LocalizationNameConsts.HelloWorld);
            var helloWorld2 = _localizationSource2.GetString(LocalizationNameConsts.HelloWorld);
            var helloWorld3 = _localizationSource3.GetString(LocalizationNameConsts.HelloWorld);

            Console.WriteLine(helloWorld);
            Console.WriteLine(helloWorld2);
            Console.WriteLine(helloWorld3);

            Console.ReadKey();
        }
    }


    public class LocalizationNameConsts
    {

        public const string HelloWorld = "HelloWorld";
    }
}
