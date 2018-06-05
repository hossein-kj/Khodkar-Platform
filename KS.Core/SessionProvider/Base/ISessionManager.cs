namespace KS.Core.SessionProvider.Base
{
    public interface ISessionManager
    {
        object Get(string key);
        T Get<T>(string key) where T : class;
        void Remove(string key);
        void RemoveAll();
        void Store(string key, object value);
        void Abandon();
    }
}