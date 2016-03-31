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

namespace Mobet.Localization.Language.Db
{
    public class LanguageDbManager : ILanguageManager,
        ISingletonDependency
    {
        private readonly ILanguageStore _languageStore;
        private readonly ICacheManager _cacheManager;

        public LanguageDbManager(ILanguageStore languageStore, ICacheManager cacheManager)
        {
            _languageStore = languageStore;
            _cacheManager = cacheManager;
        }

        public async Task<IReadOnlyList<Models.Language>> GetLanguagesAsync(int? tenantId)
        {
            return (await GetLanguageDictionary(tenantId)).Values.ToImmutableList();
        }

        public virtual async Task AddAsync(ApplicationLanguage language)
        {
            if ((await GetLanguagesAsync(language.TenantId)).Any(l => l.Name == language.Name))
            {
                throw new AbpException("There is already a language with name = " + language.Name); //TODO: LOCALIZE?
            }

            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                await _languageRepository.InsertAsync(language);
                await _unitOfWorkManager.Current.SaveChangesAsync();
            }
        }

        public virtual async Task RemoveAsync(int? tenantId, string languageName)
        {
            var currentLanguage = (await GetLanguagesAsync(tenantId)).FirstOrDefault(l => l.Name == languageName);
            if (currentLanguage == null)
            {
                return;
            }

            if (currentLanguage.TenantId == null && tenantId != null)
            {
                throw new AbpException("Can not delete a host language from tenant!"); //TODO: LOCALIZE?
            }

            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                await _languageRepository.DeleteAsync(currentLanguage.Id);
                await _unitOfWorkManager.Current.SaveChangesAsync();
            }
        }

        public virtual async Task UpdateAsync(int? tenantId, ApplicationLanguage language)
        {
            var existingLanguageWithSameName = (await GetLanguagesAsync(language.TenantId)).FirstOrDefault(l => l.Name == language.Name);
            if (existingLanguageWithSameName != null)
            {
                if (existingLanguageWithSameName.Id != language.Id)
                {
                    throw new AbpException("There is already a language with name = " + language.Name); //TODO: LOCALIZE
                }
            }

            if (language.TenantId == null && tenantId != null)
            {
                throw new AbpException("Can not update a host language from tenant"); //TODO: LOCALIZE
            }

            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                await _languageRepository.UpdateAsync(language);
                await _unitOfWorkManager.Current.SaveChangesAsync();
            }
        }

        public async Task<ApplicationLanguage> GetDefaultLanguageOrNullAsync(int? tenantId)
        {
            var defaultLanguageName = tenantId.HasValue
                ? await _settingManager.GetSettingValueForTenantAsync(LocalizationSettingNames.DefaultLanguage, tenantId.Value)
                : await _settingManager.GetSettingValueForApplicationAsync(LocalizationSettingNames.DefaultLanguage);

            return (await GetLanguagesAsync(tenantId)).FirstOrDefault(l => l.Name == defaultLanguageName);
        }

        public async Task SetDefaultLanguageAsync(int? tenantId, string languageName)
        {
            var cultureInfo = CultureInfo.GetCultureInfo(languageName);
            if (tenantId.HasValue)
            {
                await _settingManager.ChangeSettingForTenantAsync(tenantId.Value, LocalizationSettingNames.DefaultLanguage, cultureInfo.Name);
            }
            else
            {
                await _settingManager.ChangeSettingForApplicationAsync(LocalizationSettingNames.DefaultLanguage, cultureInfo.Name);
            }
        }

        private async Task<Dictionary<string, Models.Language>> GetLanguageDictionary(int? tenantId)
        {

            var languageDictionary = _cacheManager.Retrive<Dictionary<string, Models.Language>>(LocalizationConstansts.CacheKeys.Language, () =>
            {
                return (_languageStore.GetAllLanguagesAsync().Result).ToDictionary(l => l.Name);
            });

            return languageDictionary;
        }
    }
}
