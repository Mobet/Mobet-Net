using Mobet.Dependency;

namespace Mobet.Auditing.Provider
{
    /// <summary>
    /// Provides an interface to provide audit informations in the upper layers.
    /// </summary>
    public interface IAuditModelProvider : ISingletonDependency
    {
        /// <summary>
        /// Called to fill needed properties.
        /// </summary>
        /// <param name="auditInfo">Audit info that is partially filled</param>
        void Fill(AuditModel auditInfo);
    }
}