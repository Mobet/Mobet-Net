using Mobet.Domain.Entities.Audited;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Web.SignalR.Notifications.Models
{
    /// <summary>
    /// Represents a published notification.
    /// </summary>
    [Serializable]
    public class Notification : AuditedEntity<Guid>
    {
        public Notification()
        {
            UserNotifications = new HashSet<UserNotification>();
        }
        /// <summary>
        /// Unique notification name.
        /// </summary>
        [MaxLength(96)]
        public string NotificationName { get; set; }

        /// <summary>
        /// Notification data.
        /// </summary>
        public NotificationData Data { get; set; }

        /// <summary>
        /// Gets or sets the type of the entity.
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        /// Name of the entity type (including namespaces).
        /// </summary>

        [MaxLength(96)]
        public string EntityTypeName { get; set; }

        /// <summary>
        /// Entity id.
        /// </summary>
        public object EntityId { get; set; }

        /// <summary>
        /// Severity.
        /// </summary>
        public NotificationSeverity Severity { get; set; }


        public virtual ICollection<UserNotification> UserNotifications { get; set; }

    }
}
