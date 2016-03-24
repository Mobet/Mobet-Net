namespace Mobet.Auditing.Provider
{
    /// <summary>
    /// Null implementation of <see cref="IAuditModelProvider"/>.
    /// </summary>
    internal class NullAuditModelProvider : IAuditModelProvider
    {
        public void Fill(AuditModel auditInfo)
        {
            
        }
    }
}