using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Mobet.Logging;

namespace Mobet.Settings.Store
{
    /// <summary>
    /// Implements default behavior for ISettingStore.
    /// Only <see cref="GetSettingAsync"/> method is implemented and it gets setting's value
    /// from application's configuration file if exists, or returns null if not.
    /// </summary>
    public class SimpleSettingStore : ISettingStore
    {
        public SimpleSettingStore()
        {
        }

        public Task<Setting> GetSettingAsync(string name)
        {
            return Task.FromResult(new Setting(name, ConfigurationManager.AppSettings[name]));
        }
        public Task<Setting> DeleteSettingAsync(string name)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, SimpleSettingStore does not support DeleteSettingAsync.");
            throw new NotImplementedException();
        }

        public Task<Setting> AddOrUpdateSettingAsync(Setting setting)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, SimpleSettingStore does not support AddOrUpdateSettingAsync.");
            throw new NotImplementedException();
        }

        public Task<List<Setting>> GetAllSettingsAsync()
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, SimpleSettingStore does not support GetAllSettingsAsync.");
            return Task.FromResult(new List<Setting>());
        }
    }
}