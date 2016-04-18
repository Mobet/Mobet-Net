using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Domain.Entities.Audited;

namespace Mobet.Web.SignalR.Notifications.Models
{
    /// <summary>
    /// Used to store a user notification.
    /// </summary>
    [Serializable]
    public class UserNotification : CreateAuditedEntity<Guid>
    {
        /// <summary>
        /// User Account.
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// Notification Id.
        /// </summary>
        [Required]
        public virtual Guid NotificationId { get; set; }

        /// <summary>
        /// Current state of the user notification.
        /// </summary>
        public virtual UserNotificationState State { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotificationInfo"/> class.
        /// </summary>
        public UserNotification()
        {
            State = UserNotificationState.Unread;
        }
        [ForeignKey("NotificationId")]
        public virtual Notification Notification { get; set; }
    }
}
