using System;

namespace Mobet.Settings
{
    /// <summary>
    /// Defines a setting.
    /// A setting is used to configure and change behavior of the application.
    /// </summary>
    public class Setting : ISetting
    {
        /// <summary>
        /// Unique name of the setting.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Value of the setting.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Scopes of this setting.
        /// Default value: <see cref="SettingScopes.Application"/>.
        /// </summary>
        public SettingScopes Scopes { get; private set; }

        /// <summary>
        /// Group of this setting.
        /// </summary>
        public SettingGroup Group { get; private set; }

        /// <summary>
        /// Creates a new <see cref="Setting"/> object.
        /// </summary>
        /// <param name="name">Unique name of the setting</param>
        /// <param name="value">Value of the setting</param>
        /// <param name="settingGroup">Group of this setting</param>
        /// <param name="scopes">Scopes of this setting. Default value: <see cref="SettingScopes.Application"/>.</param>
        public Setting(string name,string value,SettingGroup settingGroup = null, SettingScopes scopes = SettingScopes.Application)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
            Value = value;
            Group = settingGroup;
            Scopes = scopes;
        }
    }
}
