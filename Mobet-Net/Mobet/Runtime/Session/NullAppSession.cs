using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Runtime.Session
{
    /// <summary>
    /// Implements null object pattern for <see cref="IAppSession"/>.
    /// </summary>
    public class NullAppSession : IAppSession
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullAppSession Instance { get { return SingletonInstance; } }
        private static readonly NullAppSession SingletonInstance = new NullAppSession();

        /// <summary>
        /// Gets current UserId or empty.
        /// </summary>
        public string UserAccount { get { return string.Empty; } }

        private NullAppSession()
        {

        }
    }
}
