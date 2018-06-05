using System.Web;
using System.Web.SessionState;

namespace KS.Core.SessionProvider.Base
{
    public abstract class BaseSessionManager
    {
        protected readonly HttpSessionState Session;

        protected BaseSessionManager()
        {
            Session = HttpContext.Current.Session;
        }

        public virtual object Get(string key)
        {
            return Session[key];
        }

        public virtual T Get<T>(string key) where T : class
        {
            return Session[key] as T;
        }

        public virtual void Remove(string key)
        {
            Session.Remove(key);
        }

        public virtual void RemoveAll()
        {
            Session.RemoveAll();
        }

        public virtual void Store(string key, object value)
        {
            Session[key] = value;
        }

        public virtual void Abandon()
        {
            Session.Abandon();
        }

    }
}