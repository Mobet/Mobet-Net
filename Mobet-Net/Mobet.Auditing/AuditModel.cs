using System;

namespace Mobet.Auditing
{
    /// <summary>
    /// This informations are collected for an <see cref="AuditedAttribute"/> method.
    /// </summary>
    public class AuditModel
    {
        /// <summary>
        /// UserAccount.
        /// </summary>
        public string UserAccount { get; set; }

        /// <summary>
        /// Service (class/interface) name.
        /// </summary>
        public string ServiceName { get; set; }
        
        /// <summary>
        /// Executed method name.
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Calling parameters.
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// Start time of the method execution.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Total duration of the method call.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// IP address of the client.
        /// </summary>
        public string ClientIpAddress { get; set; }
        
        /// <summary>
        /// Name (generally computer name) of the client.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Browser information if this method is called in a web request.
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        /// Optional custom data that can be filled and used.
        /// </summary>
        public string CustomData { get; set; }
        
        /// <summary>
        /// Exception object, if an exception occured during execution of the method.
        /// </summary>
        public Exception Exception { get; set; }

        public override string ToString()
        {
            return string.Format(
                "AUDIT LOG: {0}.{1} is executed by user {2} in {3} ms from {4} IP address.",
                ServiceName, MethodName, UserAccount, Duration, ClientIpAddress
                );
        }
    }
}