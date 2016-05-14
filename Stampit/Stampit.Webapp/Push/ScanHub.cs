using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Stampit.Logic.Interface;

namespace Stampit.Webapp.Push
{
    [HubName("scan")]
    public class ScanHub : Hub, IPushNotifier
    {
        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public void OnScan(string code, bool valid)
        {
            this.Clients.All.onScan(code, valid);
        }

        void IPushNotifier.OnScan(string code, bool valid)
        {
            GlobalHost.ConnectionManager.GetHubContext<ScanHub>().Clients.All.OnScan(code, valid);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
    }
}