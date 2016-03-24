using System;
using System.Globalization;
using Mobet.Dependency;
using Mobet.Localization.Sources;
using Mobet.Localization.Extensions;

namespace Mobet.Localization
{
    /// <summary>
    /// This static class is used to simplify getting localized strings.
    /// </summary>
    public static class LocalizationHelper
    {

        public static ILocalizationManager LocalizationManager;

        static LocalizationHelper()
        {
            LocalizationManager = IocManager.Instance.IsRegistered<ILocalizationManager>()
                    ? IocManager.Instance.Resolve<ILocalizationManager>()
                    : NullLocalizationManager.Instance;
        }
        /// <summary>
        /// Gets a pre-registered localization source.
        /// </summary>
        public static ILocalizationSource GetSource(string name)
        {
            return LocalizationManager.GetSource(name);
        }

        /// <summary>
        /// Gets a localized string in current language.
        /// </summary>
        /// <param name="sourceName">Name of the localization source</param>
        /// <param name="name">Key name to get localized string</param>
        /// <returns>Localized string</returns>
        public static string GetString(string sourceName, string name)
        {
            return LocalizationManager.GetString(sourceName, name);
        }

        /// <summary>
        /// Gets a localized string in specified language.
        /// </summary>
        /// <param name="sourceName">Name of the localization source</param>
        /// <param name="name">Key name to get localized string</param>
        /// <param name="culture">culture</param>
        /// <returns>Localized string</returns>
        public static string GetString(string sourceName, string name, CultureInfo culture)
        {
            return LocalizationManager.GetString(sourceName, name, culture);
        }
    }
}