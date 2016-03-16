using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Mobet.Localization.Configuration;
using Mobet.Dependency;
using Mobet.Localization.Dictionaries;
using Mobet.Localization.Sources;
using Castle.Core.Logging;
using Mobet.Localization.Language;

namespace Mobet.Localization
{
    public class LocalizationManager : ILocalizationManager
    {
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets current language for the application.
        /// </summary>
        [Obsolete("Inject ILanguageManager and use ILanguageManager.CurrentLanguage.")]
        public LanguageInfo CurrentLanguage { get { return _languageManager.CurrentLanguage; } }

        private readonly ILanguageManager _languageManager;
        private readonly ILocalizationConfiguration _configuration;
        private readonly IDictionary<string, ILocalizationSource> _sources;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LocalizationManager(
            ILanguageManager languageManager,
            ILocalizationConfiguration configuration)
        {
            Logger = NullLogger.Instance;
            _languageManager = languageManager;
            _configuration = configuration;
            _sources = new Dictionary<string, ILocalizationSource>();

            Initialize();
        }

        public void Initialize()
        {
            InitializeSources();
        }

        [Obsolete("Inject ILanguageManager and use ILanguageManager.GetLanguages().")]
        public IReadOnlyList<LanguageInfo> GetAllLanguages()
        {
            return _languageManager.GetLanguages();
        }

        private void InitializeSources()
        {
            if (!_configuration.IsEnabled)
            {
                Logger.Debug("Localization disabled.");
                return;
            }

            Logger.Debug(string.Format("Initializing {0} localization sources.", _configuration.Sources.Count));
            foreach (var source in _configuration.Sources)
            {
                if (_sources.ContainsKey(source.Name))
                {
                    throw new Exception("There are more than one source with name: " + source.Name + "! Source name must be unique!");
                }

                _sources[source.Name] = source;
                source.Initialize(_configuration);

                //Extending dictionaries
                if (source is IDictionaryBasedLocalizationSource)
                {
                    var dictionaryBasedSource = source as IDictionaryBasedLocalizationSource;
                    var extensions = _configuration.Sources.Extensions.Where(e => e.SourceName == source.Name).ToList();
                    foreach (var extension in extensions)
                    {
                        extension.DictionaryProvider.Initialize(source.Name);
                        foreach (var extensionDictionary in extension.DictionaryProvider.Dictionaries.Values)
                        {
                            dictionaryBasedSource.Extend(extensionDictionary);
                        }
                    }
                }

                Logger.Debug("Initialized localization source: " + source.Name);
            }
        }

        /// <summary>
        /// Gets a localization source with name.
        /// </summary>
        /// <param name="name">Unique name of the localization source</param>
        /// <returns>The localization source</returns>
        public ILocalizationSource GetSource(string name)
        {
            if (!_configuration.IsEnabled)
            {
                return NullLocalizationSource.Instance;
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            ILocalizationSource source;
            if (!_sources.TryGetValue(name, out source))
            {
                throw new Exception("Can not find a source with name: " + name);
            }

            return source;
        }

        /// <summary>
        /// Gets all registered localization sources.
        /// </summary>
        /// <returns>List of sources</returns>
        public IReadOnlyList<ILocalizationSource> GetAllSources()
        {
            return _sources.Values.ToImmutableList();
        }
    }
}