using System;
using System.Globalization;
using System.Threading;
using System.Reflection;

using Mobet.Dependency;

using Mobet.Localization.Dictionaries.Xml;
using Mobet.Localization.Dictionaries;
using Mobet.Localization;
using Mobet.Localization.Sources;
using Mobet.Localization.Dictionaries.Json;
using Mobet.Localization.Sources.Resource;

using Mobet.Demo.Localization.Localization.ResourceSerouces;

using Mobet.Configuration.Startup;
using Mobet.Localization.Startup;

namespace Mobet.Demo.Localization
{
    class Program
    {

        static void Main(string[] args)
        {

            StartupConfig.RegisterDependency(cfg =>
            {
                cfg.UseLocalization(config =>
                {
                    config.Sources.Add(
                                new DictionaryBasedLocalizationSource(
                                    "Demo",
                                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                                        Assembly.GetExecutingAssembly(), "Mobet.Demo.Localization.Localization.XmlSources"
                            )));
                    config.Sources.Add(
                                new DictionaryBasedLocalizationSource(
                                    "Lang",
                                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                                        Assembly.GetExecutingAssembly(),
                                         "Mobet.Demo.Localization.Localization.JsonSources"
                            )));

                    config.Sources.Add(new ResourceFileLocalizationSource("Res", Res.ResourceManager));
                });

                cfg.RegisterConsoleApplication();
            });


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
