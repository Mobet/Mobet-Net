namespace Mobet.Settings
{
    /// <summary>
    /// Represents value of a setting.
    /// </summary>
    public interface ISetting
    {
        /// <summary>
        /// Unique name of the setting.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Value of the setting.
        /// </summary>
        string Value { get; }
    }
}