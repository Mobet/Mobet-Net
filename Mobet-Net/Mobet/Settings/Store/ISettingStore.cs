using System.Collections.Generic;
using System.Threading.Tasks;

using Mobet.Dependency;

namespace Mobet.Settings.Store
{
    /// <summary>
    /// This interface is used to get/set settings from/to a data source (database).
    /// </summary>
    public interface ISettingStore
    {
        /// <summary>
        /// Gets a setting or null.
        /// </summary>
        /// <param name="name">Name of the setting</param>
        /// <returns>Setting object</returns>
        Task<Setting> GetSettingAsync(string name);

        /// <summary>
        /// Adds a setting.
        /// </summary>
        /// <param name="setting">Setting to add</param>
        Task<Setting> AddOrUpdateSettingAsync(Setting setting);

        /// <summary>
        /// Deletes a setting.
        /// </summary>
        /// <param name="name">Name of the setting</param>
        Task<Setting> DeleteSettingAsync(string name);

        /// <summary>
        /// Gets a list of setting.
        /// </summary>
        /// <returns>List of settings</returns>
        Task<List<Setting>> GetAllSettingsAsync();
    }
}