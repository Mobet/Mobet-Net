using Mobet.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Runtime.Session
{
    /// <summary>
    /// Defines some session information that can be useful for applications.
    /// </summary>
    public interface IAppSession
    {
        /// <summary>
        /// Gets current UserId or empty.
        /// </summary>
        string UserAccount { get; }
    }
}
