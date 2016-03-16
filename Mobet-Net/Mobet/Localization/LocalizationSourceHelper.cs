using Mobet.Localization.Configuration;
using Mobet.Logging;
using System;

namespace Mobet.Localization
{
    public static class LocalizationSourceHelper
    {
        public static string ReturnGivenNameOrThrowException(ILocalizationConfiguration configuration, string sourceName, string name)
        {
            var exceptionMessage = string.Format(
                "Can not find '{0}' in localization source '{1}'!",
                name, sourceName
                );

            if (!configuration.ReturnGivenTextIfNotFound)
            {
                throw new Exception(exceptionMessage);
            }

            LogHelper.Logger.Warn(exceptionMessage);

            return configuration.WrapGivenTextIfNotFound
                ? string.Format("{0}", name)
                : name;
        }
    }
}
