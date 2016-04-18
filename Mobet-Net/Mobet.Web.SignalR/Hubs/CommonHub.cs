using Castle.Core.Logging;
using Microsoft.AspNet.SignalR;
using Mobet.Dependency;
using Mobet.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Web.SignalR.Hubs
{
    public class CommonHub : Hub, IDependency
    {
        private readonly IOnlineClientManager _onlineClientManager;

        public IAppSession AppSession { get; set; }

        public ILogger Logger { get; set; }

        public CommonHub(IOnlineClientManager onlineClientManager)
        {
            _onlineClientManager = onlineClientManager;
            AppSession = NullAppSession.Instance;
            Logger = NullLogger.Instance;
        }

        public async override Task OnConnected()
        {
            await base.OnConnected();

            var client = new OnlineClient(
                Context.ConnectionId,
                GetIpAddressOfClient(),
                AppSession.UserId
                );

            Logger.Debug("A client is connected: " + client);

            _onlineClientManager.Add(client);
        }

        public async override Task OnDisconnected(bool stopCalled)
        {
            await base.OnDisconnected(stopCalled);

            Logger.Debug("A client is disconnected: " + Context.ConnectionId);

            try
            {
                _onlineClientManager.Remove(Context.ConnectionId);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
            }
        }

        private string GetIpAddressOfClient()
        {
            try
            {
                return Context.Request.Environment["server.RemoteIpAddress"].ToString();
            }
            catch (Exception ex)
            {
                Logger.Error("Can not find IP address of the client! connectionId: " + Context.ConnectionId);
                Logger.Error(ex.Message, ex);
                return "";
            }
        }

    }
}
