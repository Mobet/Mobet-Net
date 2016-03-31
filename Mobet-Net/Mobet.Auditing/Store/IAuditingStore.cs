using System.Threading.Tasks;
using Mobet.Dependency;
namespace Mobet.Auditing.Store
{
    /// <summary>
    /// This interface should be implemented by vendors to
    /// make auditing working.
    /// Default implementation is <see cref="LoggingAuditingStore"/>.
    /// </summary>
    public interface IAuditingStore
    {
        /// <summary>
        /// Should save audits to a persistent store.
        /// </summary>
        /// <param name="auditInfo">Audit informations</param>
        Task SaveAsync(AuditingMessage auditInfo);
    }
}