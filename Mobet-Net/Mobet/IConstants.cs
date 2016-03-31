using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet
{
    internal class IConstants
    {
        public static class Localization
        {
            public const string DefaultLanguageName = "Localization.DefaultLanguageName";

            public const string DefaultLocalizationXmlSources = "Mobet.Localization.Sources.XmlSource";

            public static class CacheNames
            {
                public const string LocalizationText = "LOCALIZATION:LOCALIZATION_TEXT:";

                public const string Language = "LOCALIZATION:LANGUAGE:";
            }
        }

        public static class GlobalSettings
        {
            public static class CacheNames
            {
                public const string GlobalSettings = "GLOBALSETTINGS:APPLICATION_SETTINGS";
            }
        }
    }
}
