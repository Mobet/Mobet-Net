using System;
using System.Text;

namespace Mobet.Auditing
{
    /// <summary>
    /// This informations are collected for an <see cref="AuditedAttribute"/> method.
    /// </summary>
    public class AuditingMessage
    {
        /// <summary>
        /// output.
        /// </summary>
        public virtual string Output { get; set; }

        /// <summary>
        /// Calling parameters.
        /// </summary>
        public virtual string InputParameters { get; set; }

        /// <summary>
        /// Start time of the method execution.
        /// </summary>
        public virtual DateTime Time { get; set; }

        /// <summary>
        /// Total duration of the method call.
        /// </summary>
        public virtual int Duration { get; set; }

        /// <summary>
        /// IP address of the client.
        /// </summary>
        public virtual string Route { get; set; }

        /// <summary>
        /// Name (generally computer name) of the client.
        /// </summary>
        public virtual string Host { get; set; }

        /// <summary>
        /// Browser information if this method is called in a web request.
        /// </summary>
        public virtual string UserAgent { get; set; }

        /// <summary>
        /// User Data
        /// </summary>
        public virtual string UserData { get; set; }

        /// <summary>
        /// Exception object, if an exception occured during execution of the method.
        /// </summary>
        public Exception Exception { get; set; }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("DateTime: {0}", Time.ToString("yyyy-MM-dd hh:mm:ss")));
            sb.AppendLine(string.Format("Duration: {0} millisecond", Duration.ToString()));
            sb.AppendLine(string.Format("Route：{0}", Route));
            sb.AppendLine(string.Format("InputParameters：{0}", InputParameters));
            sb.AppendLine(string.Format("Output：{0}", Output));
            sb.AppendLine(string.Format("Host：{0}", Host));

            if (string.IsNullOrWhiteSpace(UserAgent))
            {
                sb.AppendLine(string.Format("UserAgent：{0}", UserAgent));
            }
            if (string.IsNullOrWhiteSpace(UserData))
            {
                sb.AppendLine(string.Format("UserData：{0}", UserData));
            }
            if (Exception != null)
            {
                sb.AppendLine(string.Format("Exception：{0}", Exception == null ? "" : Exception.Message));
                sb.AppendLine(string.Format("StackTrace：{0}", Exception == null ? "" : Exception.StackTrace));
            }

            sb.AppendLine();

            return sb.ToString();
        }
    }
}