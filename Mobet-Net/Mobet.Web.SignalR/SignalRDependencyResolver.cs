using Microsoft.AspNet.SignalR;
using Mobet.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Web.SignalR
{
    public class SignalRDependencyResolver : DefaultDependencyResolver
    {

        public override object GetService(Type serviceType)
        {

            return IocManager.Instance.IsRegistered(serviceType)
                ? IocManager.Instance.Resolve(serviceType)
                : base.GetService(serviceType);
        }
    }
}
