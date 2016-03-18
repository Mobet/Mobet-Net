using System;
using System.Reflection;
using Mobet.Domain.Repositories;
using Mobet.Domain.Services;
using Autofac.Extras.DynamicProxy2;

namespace Mobet.Domain.UnitOfWork
{
    /// <summary>
    /// A helper class to simplify unit of work process.
    /// </summary>
    public static class UnitOfWorkHelper
    {
        /// <summary>
        /// Returns true if UOW must be used for given type as convention.
        /// </summary>
        /// <param name="type">Type to check</param>
        public static bool IsConventionalUowClass(Type type)
        {
            return typeof(IRepository).IsAssignableFrom(type) || typeof(IApplicationService).IsAssignableFrom(type);
        }

        /// <summary>
        /// Returns true if given method has UnitOfWorkAttribute attribute.
        /// </summary>
        /// <param name="methodInfo">Method info to check</param>
        public static bool HasUnitOfWorkAttribute(MemberInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(UnitOfWorkAttribute), true);
        }

        /// <summary>
        /// Returns UnitOfWorkAttribute it exists.
        /// </summary>
        /// <param name="methodInfo">Method info to check</param>
        public static UnitOfWorkAttribute GetUnitOfWorkAttributeOrNull(MemberInfo methodInfo)
        {
            var attrs = methodInfo.GetCustomAttributes(typeof (UnitOfWorkAttribute), false);
            if (attrs.Length <= 0)
            {
                return null;
            }

            return (UnitOfWorkAttribute) attrs[0];
        }
    }
}