using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace Mobet.Web
{
    public static class ActionDescriptorHelper
    {
        public static MethodInfo GetMethodInfo(ActionDescriptor actionDescriptor)
        {
            if (actionDescriptor is ReflectedActionDescriptor)
            {
                return ((ReflectedActionDescriptor)actionDescriptor).MethodInfo;
            }

            if (actionDescriptor is ReflectedAsyncActionDescriptor)
            {
                return ((ReflectedAsyncActionDescriptor)actionDescriptor).MethodInfo;
            }

            if (actionDescriptor is TaskAsyncActionDescriptor)
            {
                return ((TaskAsyncActionDescriptor)actionDescriptor).MethodInfo;
            }

            throw new Exception("Could not get MethodInfo for the action '" + actionDescriptor.ActionName + "' of controller '" + actionDescriptor.ControllerDescriptor.ControllerName + "'.");
        }
    }
}
