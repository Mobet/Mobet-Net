using System.Collections.Generic;
using Mobet.Localization;
using Mobet.Localization.Language;

namespace Mobet.Localization.Configuration
{
    /// <summary>
    /// Used for localization configurations.
    /// </summary>
    internal class LocalizationConfiguration : ILocalizationConfiguration
    {
        /// <inheritdoc/>
        public IList<LanguageInfo> Languages { get; private set; }

        /// <inheritdoc/>
        public ILocalizationSourceList Sources { get; private set; }

        /// <inheritdoc/>
        public bool IsEnabled { get; set; }

        /// <inheritdoc/>
        public bool ReturnGivenTextIfNotFound { get; set; }

        /// <inheritdoc/>
        public bool WrapGivenTextIfNotFound { get; set; }

        public LocalizationConfiguration()
        {
            Languages = new List<LanguageInfo>();
            Sources = new LocalizationSourceList();

            IsEnabled = true;
            ReturnGivenTextIfNotFound = true;
            WrapGivenTextIfNotFound = true;
        }
    }
}