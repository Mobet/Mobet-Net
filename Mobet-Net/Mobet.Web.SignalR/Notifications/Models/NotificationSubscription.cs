using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using Mobet.Domain.Models;
using Mobet.Domain.Models.Audited;

namespace Mobet.Web.SignalR.Notifications.Models
{
    /// <summary>
    /// Used to store a notification subscription.
    /// </summary>
    public class NotificationSubscription : CreateAuditedEntity<Guid>
    {
        /// <summary>
        /// User Account.
        /// </summary>
        public virtual string UserAccount { get; set; }

        /// <summary>
        /// Notification unique name.
        /// </summary>
        [MaxLength(96)]
        public virtual string NotificationName { get; set; }

        /// <summary>
        /// Gets/sets entity type name, if this is an entity level notification.
        /// It's FullName of the entity type.
        /// </summary>
        [MaxLength(250)]
        public virtual string EntityTypeName { get; set; }

        /// <summary>
        /// AssemblyQualifiedName of the entity type.
        /// </summary>
        [MaxLength(512)]
        public virtual string EntityTypeAssemblyQualifiedName { get; set; }

        /// <summary>
        /// Gets/sets primary key of the entity, if this is an entity level notification.
        /// </summary>
        [MaxLength(96)]
        public virtual string EntityId { get; set; }

    }
}
