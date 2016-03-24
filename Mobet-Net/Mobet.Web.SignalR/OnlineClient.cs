﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Web.SignalR
{
    /// <summary>
    /// Implements <see cref="IOnlineClient"/>.
    /// </summary>
    [Serializable]
    public class OnlineClient : IOnlineClient
    {
        /// <summary>
        /// Unique connection Id for this client.
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// IP address of this client.
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// User Account.
        /// </summary>
        public string UserAccount { get; set; }

        /// <summary>
        /// Connection establishment time for this client.
        /// </summary>
        public DateTime ConnectTime { get; set; }

        /// <summary>
        /// Shortcut to set/get <see cref="Properties"/>.
        /// </summary>
        public object this[string key]
        {
            get { return Properties[key]; }
            set { Properties[key] = value; }
        }

        /// <summary>
        /// Can be used to add custom properties for this client.
        /// </summary>
        public Dictionary<string, object> Properties
        {
            get { return _properties; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                _properties = value;
            }
        }
        private Dictionary<string, object> _properties;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnlineClient"/> class.
        /// </summary>
        public OnlineClient()
        {
            ConnectTime = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OnlineClient"/> class.
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="userAccount">The user identifier.</param>
        public OnlineClient(string connectionId, string ipAddress, string userAccount)
            : this()
        {
            ConnectionId = connectionId;
            IpAddress = ipAddress;
            UserAccount = userAccount;

            Properties = new Dictionary<string, object>();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
