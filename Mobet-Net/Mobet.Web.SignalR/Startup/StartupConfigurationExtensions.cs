using Microsoft.AspNet.SignalR;
using Mobet.Configuration.Startup;
using Mobet.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Web.SignalR.Startup
{
    public static class StartupConfigurationExtensions
    {
        public static StartupConfiguration UseLocalization(this StartupConfiguration bootstrap)
        {
            GlobalHost.DependencyResolver = new SignalRDependencyResolver();
            return bootstrap;
        }
    }
}
