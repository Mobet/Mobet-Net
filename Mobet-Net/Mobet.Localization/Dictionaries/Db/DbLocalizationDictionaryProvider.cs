using Autofac.Core;
using Mobet.Dependency;
using Mobet.Extensions;
using Mobet.Localization.Language;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Localization.Dictionaries.Db
{
    /// <summary>
    /// Extends <see cref="ILocalizationDictionaryProvider"/> to add tenant and database based localization.
    /// </summary>
    public class DbLocalizationDictionaryProvider : ILocalizationDictionaryProvider
    {
        public ILocalizationDictionary DefaultDictionary
        {
            get { return GetDefaultDictionary(); }
        }

        public IDictionary<string, ILocalizationDictionary> Dictionaries
        {
            get { return GetDictionaries(); }
        }

        private readonly ConcurrentDictionary<string, ILocalizationDictionary> _dictionaries;

        private string _sourceName;

        private readonly ILocalizationDictionaryProvider _internalProvider;

        private ILanguageManager _languageManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbLocalizationDictionaryProvider"/> class.
        /// </summary>
        public DbLocalizationDictionaryProvider(ILocalizationDictionaryProvider internalProvider)
        {
            _internalProvider = internalProvider;
            _dictionaries = new ConcurrentDictionary<string, ILocalizationDictionary>();
        }

        public void Initialize(string sourceName)
        {
            _sourceName = sourceName;
            _languageManager = IocManager.Instance.Resolve<ILanguageManager>();
            _internalProvider.Initialize(_sourceName);
        }

        protected virtual IDictionary<string, ILocalizationDictionary> GetDictionaries()
        {
            var languages = _languageManager.GetLanguages();

            foreach (var language in languages)
            {
                _dictionaries.GetOrAdd(language.Name, s => CreateLocalizationDictionary(language));
            }

            return _dictionaries;
        }

        protected virtual ILocalizationDictionary GetDefaultDictionary()
        {
            var defaultLanguage = _languageManager.GetLanguages().FirstOrDefault(l => l.IsDefault);
            if (defaultLanguage == null)
            {
                throw new ApplicationException("Default language is not defined!");
            }

            return _dictionaries.GetOrAdd(defaultLanguage.Name, s => CreateLocalizationDictionary(defaultLanguage));
        }

        protected virtual ILocalizationDictionary CreateLocalizationDictionary(LanguageInfo language)
        {
            var internalDictionary =
                _internalProvider.Dictionaries.GetOrDefault(language.Name) ??
                new EmptyInternalDictionary(CultureInfo.GetCultureInfo(language.Name));

            var dictionary = IocManager.Instance.ResolveOptional<ILocalizationDictionary>(
                    new NamedPropertyParameter("sourceName", _sourceName),
                    new NamedPropertyParameter("internalDictionary", internalDictionary)
                );

            return dictionary;
        }

        public virtual void Extend(ILocalizationDictionary dictionary)
        {
            //Add
            ILocalizationDictionary existingDictionary;
            if (!_internalProvider.Dictionaries.TryGetValue(dictionary.CultureInfo.Name, out existingDictionary))
            {
                _internalProvider.Dictionaries[dictionary.CultureInfo.Name] = dictionary;
                return;
            }

            //Override
            var localizedStrings = dictionary.GetAllStrings();
            foreach (var localizedString in localizedStrings)
            {
                existingDictionary[localizedString.Name] = localizedString.Value;
            }
        }
    }

    internal class EmptyInternalDictionary : ILocalizationDictionary
    {
        public CultureInfo CultureInfo { get; private set; }

        public EmptyInternalDictionary(CultureInfo cultureInfo)
        {
            CultureInfo = cultureInfo;
        }

        public LocalizedString GetOrNull(string name)
        {
            return null;
        }

        public IReadOnlyList<LocalizedString> GetAllStrings()
        {
            return new LocalizedString[0];
        }

        public string this[string name]
        {
            get { return null; }
            set { }
        }
    }
}
