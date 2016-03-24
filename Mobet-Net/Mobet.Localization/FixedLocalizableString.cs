using System;
using System.Globalization;

namespace Mobet.Localization
{
    /// <summary>
    /// A class that gets the same string on every localization.
    /// </summary>
    [Serializable]
    public class LocalizableStringFixed : ILocalizableString
    {
        /// <summary>
        /// The fixed string.
        /// Whenever Localize methods called, this string is returned.
        /// </summary>
        public virtual string FixedString { get; private set; }

        /// <summary>
        /// Needed for serialization.
        /// </summary>
        private LocalizableStringFixed()
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="LocalizableStringFixed"/>.
        /// </summary>
        /// <param name="fixedString">
        /// The fixed string.
        /// Whenever Localize methods called, this string is returned.
        /// </param>
        public LocalizableStringFixed(string fixedString)
        {
            FixedString = fixedString;
        }

        public string Localize(ILocalizationContext context)
        {
            return FixedString;
        }

        public string Localize(ILocalizationContext context, CultureInfo culture)
        {
            return FixedString;
        }

        public override string ToString()
        {
            return FixedString;
        }
    }
}