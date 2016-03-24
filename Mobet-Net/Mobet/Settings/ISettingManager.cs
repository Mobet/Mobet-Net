using Mobet.Dependency;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobet.Settings
{
    /// <summary>
    /// This is the main interface that must be implemented to be able to load/change values of settings.
    /// </summary>
    public interface ISettingManager
    {
        /// <summary>
        /// Gets the <see cref="Setting"/> object with given unique name.
        /// Throws exception if can not find the setting.
        /// </summary>
        /// <param name="name">Unique name of the setting</param>
        /// <returns>The <see cref="Setting"/> object.</returns>
        Task<Setting> GetSettingAsync(string name);
        /// <summary>
        /// Gets current value of a setting.
        /// It gets the setting value, overwritten by application if exists.
        /// </summary>
        /// <param name="name">Unique name of the setting</param>
        /// <returns>Current value of the setting</returns>
        Task<string> GetSettingValueAsync(string name, SettingScopes scope = SettingScopes.Application);
        /// <summary>
        /// Gets a list of all setting.
        /// </summary>
        /// <returns>All settings.</returns>
        Task<IReadOnlyList<ISetting>> GetAllSettingsAsync();
        /// <summary>
        /// Add Or Update setting for the application level.
        /// </summary>
        /// <param name="name">Unique name of the setting</param>
        /// <param name="value">Value of the setting</param>
        Task<Setting> AddOrUpdateSettingAsync(string name, string value);
        /// <summary>
        /// Delete setting by name.
        /// </summary>
        /// <param name="name">Unique name of the setting</param>
        /// <returns>Value of the setting</returns>
        Task<Setting> DeleteSettingAsync(string name);

    }
}
