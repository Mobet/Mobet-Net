using System.Collections.Generic;
using System.Collections.Immutable;

using Mobet.Localization.Configuration;
using Mobet.Dependency;
using Mobet.Localization.Language;

namespace Mobet.Localization
{
    public class DefaultLanguageProvider : ILanguageProvider, IDependency
    {
        private readonly ILocalizationConfiguration _configuration;

        public DefaultLanguageProvider(ILocalizationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IReadOnlyList<LanguageInfo> GetLanguages()
        {
            return _configuration.Languages.ToImmutableList();
        }
    }
}