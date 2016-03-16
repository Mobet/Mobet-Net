using System.Collections.Generic;
using System.Threading.Tasks;
using Mobet.Extensions;
using Mobet.Threading;

namespace Mobet.Settings.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="ISettingManager"/>.
    /// </summary>
    public static class SettingManagerExtensions
    {
        /// <summary>
        /// Gets current value of a setting.
        /// It gets the setting value, overwritten by application and the current user if exists.
        /// </summary>
        /// <param name="settingManager">Setting manager</param>
        /// <param name="name">Unique name of the setting</param>
        /// <returns>Current value of the setting</returns>
        public static string GetSettingValue(this ISettingManager settingManager, string name)
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueAsync(name));
        }
    }
}