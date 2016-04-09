using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Mobet.Dependency;

namespace Mobet.Runtime.Session
{
    /// <summary>
    /// Implements <see cref="IAppSession"/> to get session properties from claims of <see cref="Thread.CurrentPrincipal"/>.
    /// </summary>
    public class AppClaimsSession : IAppSession, IDependency
    {
        public virtual string Name
        {
            get
            {
                var claims = System.Security.Claims.ClaimsPrincipal.Current.Claims;
                if (claims == null)
                {
                    return null;
                }

                var userNameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                if (userNameClaim == null || string.IsNullOrEmpty(userNameClaim.Value))
                {
                    return null;
                }

                return userNameClaim.Value;
            }
        }
        public virtual string UserId
        {
            get
            {
                var claims = System.Security.Claims.ClaimsPrincipal.Current.Claims;
                if (claims == null)
                {
                    return null;
                }

                var userIdClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                {
                    return null;
                }

                return userIdClaim.Value;
            }
        }
    }
}
