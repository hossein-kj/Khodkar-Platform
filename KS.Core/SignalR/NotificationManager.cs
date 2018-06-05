
using KS.Core.Hubs;
using Microsoft.AspNet.SignalR;

namespace KS.Core.SignalR
{
    public class NotificationManager : INotificationManager
    {
        public void BroadcastMessage(string templateMessage, string message)
        {
            GlobalHost.ConnectionManager.GetHubContext<NotificationHub>().Clients.All.broadcastMessage(templateMessage,message);
        }
    }
}
