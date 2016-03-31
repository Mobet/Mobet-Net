namespace Mobet.GlobalSettings
{
    /// <summary>
    /// Represents value of a setting.
    /// </summary>
    public interface GlobalSetting
    {
        /// <summary>
        /// Unique name of the setting.
        /// </summary>
        string Name { get; set; }
        
        /// <summary>
        /// Value of the setting.
        /// </summary>
        string Value { get; set; }
    }
}