using System.Linq;
using System.Reflection;

using Mobet.Runtime.Session;
using Mobet.Auditing.Configuration;
using Mobet.Auditing.Attributes;

namespace Mobet.Auditing
{
    public static class AuditingHelper
    {
        public static bool ShouldSaveAudit(MethodInfo methodInfo, IAuditingConfiguration configuration, IAppSession appSession, bool defaultValue = false)
        {
            if ((configuration == null || !configuration.IsEnabled)
                || (methodInfo == null)
                || (!methodInfo.IsPublic)
                || (methodInfo.IsDefined(typeof(DisableAuditingAttribute)))
                || (methodInfo.DeclaringType != null && methodInfo.DeclaringType.IsDefined(typeof(DisableAuditingAttribute)))
                )
            {
                return false;
            }

            if ((methodInfo.IsDefined(typeof(AuditedAttribute)))
                || (methodInfo.DeclaringType != null && methodInfo.DeclaringType.IsDefined(typeof(AuditedAttribute)))
                || (methodInfo.DeclaringType != null &&configuration.Selectors.Any(selector => selector.Predicate(methodInfo.DeclaringType)))
                )
            {
                return true;
            }
            return defaultValue;
        }
    }
}