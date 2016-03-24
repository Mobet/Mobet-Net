using Mobet.Web.SignalR.Notifications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Web.SignalR.Notifications
{
    /// <summary>
    /// Interface to send real time notifications to users.
    /// </summary>
    public interface INotifier
    {
        /// <summary>
        /// This method tries to deliver real time notifications to specified users.
        /// If a user is not online, it should ignore him.
        /// </summary>
        Task SendNotificationsAsync(UserNotification[] userNotifications);
    }
}
