namespace Mobet.Auditing.Configuration
{
    /// <summary>
    /// Defines MVC Controller auditing configurations
    /// </summary>
    internal class MvcControllersAuditingConfiguration : IMvcControllersAuditingConfiguration
    {
        /// <summary>
        /// Used to enable/disable auditing for MVC controllers.
        /// Default: true.
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// Used to enable/disable auditing for child MVC actions.
        /// Default: false.
        public bool IsEnabledForChildActions { get; set; }

        public MvcControllersAuditingConfiguration()
        {
            IsEnabled = true;
        }
    }
}