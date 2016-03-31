using System;
using System.Globalization;
using Mobet.Localization.Extensions;
using Mobet.Localization.Settings;
namespace Mobet.Localization
{
    /// <summary>
    /// Represents a string that can be localized.
    /// </summary>
    [Serializable]
    public class LocalizationString : ILocalizationString
    {
        /// <summary>
        /// Unique name of the localization source.
        /// </summary>
        public virtual string SourceName { get; private set; }

        /// <summary>
        /// Unique Name of the string to be localized.
        /// </summary>
        public virtual string Name { get; private set; }

        /// <summary>
        /// Needed for serialization.
        /// </summary>
        private LocalizationString()
        {
            
        }

        /// <param name="name">Unique name of the localization source</param>
        /// <param name="sourceName">Unique Name of the string to be localized</param>
        public LocalizationString(string name, string sourceName)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (sourceName == null)
            {
                throw new ArgumentNullException("sourceName");
            }

            Name = name;
            SourceName = sourceName;
        }

        public string Localize(ILocalizationContext context)
        {
            return context.LocalizationManager.GetString(SourceName, Name);
        }

        public string Localize(ILocalizationContext context, CultureInfo culture)
        {
            return context.LocalizationManager.GetString(SourceName, Name, culture);
        }

        public override string ToString()
        {
            return string.Format("[LocalizableString: {0}, {1}]", Name, SourceName);
        }
    }
}