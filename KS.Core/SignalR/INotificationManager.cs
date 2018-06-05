namespace KS.Core.SignalR
{
    public interface INotificationManager
    {
        void BroadcastMessage(string templateMessage, string message);
    }
}