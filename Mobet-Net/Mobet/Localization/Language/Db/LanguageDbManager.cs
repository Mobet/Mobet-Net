using Mobet.Caching;
using Mobet.Dependency;
using Mobet.Events.Handlers;
using Mobet.Localization.Store;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobet.Localization.Models;
using Mobet.GlobalSettings;

namespace Mobet.Localization.Language.Db
{
    public class LanguageDbManager : ILanguageDbManager, ISingletonDependency
    {
        private readonly ILanguageStore _languageStore;
        private readonly ICacheManager _cacheManager;
        private readonly IGlobalSettingManager _globalSettingManager;

        public LanguageDbManager(ILanguageStore languageStore, ICacheManager cacheManager, IGlobalSettingManager globalSettingManager)
        {
            _languageStore = languageStore;
            _cacheManager = cacheManager;
            _globalSettingManager = globalSettingManager;
        }

        public async Task<IReadOnlyList<Models.Language>> GetLanguagesAsync()
        {
            var languages = _cacheManager.Retrive<IReadOnlyList<Models.Language>>(IConstants.Localization.CacheNames.Language, () =>
            {
                return _languageStore.GetAllLanguagesAsync().Result.ToImmutableList();
            });

            return await Task.FromResult(languages);
        }

        public virtual async Task<Models.Language> GetDefaultLanguageOrNullAsync()
        {
            var defaultLanguageName = await _globalSettingManager.GetSettingValueAsync(IConstants.Localization.DefaultLanguageName);
            var language = (await GetLanguagesAsync()).FirstOrDefault(x => x.Name == defaultLanguageName);
            return language;
        }

        public virtual async Task AddAsync(Models.Language language)
        {
            await _languageStore.AddOrUpdateLanguageAsync(language);
        }

        public virtual async Task RemoveAsync(string languageName)
        {
            await _languageStore.DeleteLanguageAsync(languageName);
        }

        public virtual async Task UpdateAsync(Models.Language language)
        {
            await _languageStore.AddOrUpdateLanguageAsync(language);
        }

        public virtual async Task SetDefaultLanguageAsync(string languageName)
        {
            var cultureInfo = CultureInfo.GetCultureInfo(languageName);
            await _globalSettingManager.AddOrUpdateSettingAsync(languageName, cultureInfo.Name);
            await _globalSettingManager.ClearGlobalSettingCacheAsync();
        }
    }
}
