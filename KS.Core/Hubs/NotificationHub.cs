
using Microsoft.AspNet.SignalR;


namespace KS.Core.Hubs
{
    public class NotificationHub : Hub
    {
        public void BroadcastMessage(string templateMessage, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(templateMessage, message);
        }
    }
}
