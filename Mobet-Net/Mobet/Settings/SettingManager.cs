using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

using Mobet.Extensions;
using Mobet.Domain.UnitOfWork;
using Mobet.Caching;
using Mobet.Runtime.Session;
using Mobet.Dependency;
using Mobet.Settings.Configuration;
using Mobet.Settings.Store;
using Mobet.Settings.Provider;

namespace Mobet.Settings
{
    /// <summary>
    /// This class implements <see cref="ISettingManager"/> to manage setting values in the database.
    /// </summary>
    public class SettingManager : ISettingManager
    {
        public const string ApplicationSettingsCacheKey = "SETTING:APPLICATION_SETTINGS";

        private ISettingStore _settingStore;
        private ICacheManager _settingCacheProvider;
        private Dictionary<string, ISetting> _settings;
        private ISettingsConfiguration _settingsConfiguration;

        public SettingManager(ICacheManager cacheManager, ISettingsConfiguration settingsConfiguration, ISettingStore settingStore)
        {
            _settingsConfiguration = settingsConfiguration;
            _settingCacheProvider = cacheManager;
            _settingStore = settingStore;
            _settings = new Dictionary<string, ISetting>();

            var context = new SettingProviderContext();
            foreach (var providerType in _settingsConfiguration.Providers)
            {
                var provider = CreateProvider(providerType);
                foreach (var settings in provider.GetSettings(context))
                {
                    _settings[settings.Name] = settings;
                }
            }
        }

        private SettingProvider CreateProvider(Type providerType)
        {
            IocManager.Instance.RegisterIfNot(providerType);
            return (SettingProvider)(IocManager.Instance.Resolve(providerType));
        }

        public async Task<Setting> GetSettingAsync(string name)
        {
            var settings = await GetApplicationSettingsAsync();
            return await Task.FromResult<Setting>(settings.GetOrDefault(name));
        }

        public async Task<string> GetSettingValueAsync(string name, SettingScopes scope = SettingScopes.Application)
        {
            var setting = await GetSettingAsync(name);
            if (setting != null)
            {
                _settings[name] = await _settingStore.GetSettingAsync(name);
            }
            return await Task.FromResult<string>(_settings[name].Value);
        }

        public async Task<IReadOnlyList<ISetting>> GetAllSettingsAsync()
        {
            foreach (var setting in await GetApplicationSettingsAsync())
            {
                if (_settings.Keys.Contains(setting.Key))
                {
                    _settings[setting.Key] = setting.Value;
                }
            }
            return await Task.FromResult<IReadOnlyList<ISetting>>(_settings.Values.ToImmutableList());
        }
        public async Task<Setting> AddOrUpdateSettingAsync(string name, string value)
        {
            return await _settingStore.AddOrUpdateSettingAsync(new Setting(name, value));
        }

        public async Task<Setting> DeleteSettingAsync(string name)
        {
            return await _settingStore.DeleteSettingAsync(name);
        }

        private async Task<Dictionary<string, Setting>> GetApplicationSettingsAsync()
        {
            var settings = await _settingCacheProvider.Retrive(ApplicationSettingsCacheKey, async () =>
            {
                var dictionary = new Dictionary<string, Setting>();
                foreach (var setting in await _settingStore.GetAllSettingsAsync())
                {
                    dictionary[setting.Name] = setting;
                }
                return dictionary;
            });
            return await Task.FromResult(settings);
        }
    }
}