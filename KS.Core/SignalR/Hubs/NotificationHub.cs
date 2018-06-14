using KS.Core.GlobalVarioable;
using KS.Core.SignalR.Attribute;
using Microsoft.AspNet.SignalR;

namespace KS.Core.SignalR.Hubs
{
    
    public class NotificationHub : Hub
    {
        [AuthorizeClaims(Roles.Admin, Roles.Developers)]
        public void BroadcastMessage(string templateMessage, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(templateMessage, message);
        }
    }
}
