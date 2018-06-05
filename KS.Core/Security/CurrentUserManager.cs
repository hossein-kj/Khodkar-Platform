
using System.Collections.Generic;
using KS.Core.Security.Adapters;
using KS.Core.SessionProvider.Base;

namespace KS.Core.Security
{
    public static class CurrentUserManager
    {
        private static IDefaultCurrentUserAdapter Adapter =>  DependencyProvider.DependencyManager.Get<IDefaultCurrentUserAdapter>();
        public static int Id => Adapter.Id;

        public static string Email => Adapter.Email;

        public static string UserName => Adapter.UserName;

        public static bool IsAuthenticated => Adapter.IsAuthenticated;

        public static List<int> RolesId => Adapter.RolesId;


        public static string UserIdentity => Adapter.UserIdentity;

        public static string Ip => Adapter.Ip;

        public static bool LogOff(ISessionManager sessionManager)
        {
           return Adapter.LogOff(sessionManager);

        }
    }

    public static class CurrentUserManager<T> where T:ICurrentUserAdapter
    {
        private static ICurrentUserAdapter Adapter => DependencyProvider.DependencyManager.Get<T>();
        public static int Id => Adapter.Id;

        public static string Email => Adapter.Email;
        public static string UserName => Adapter.UserName;

        public static bool IsAuthenticated => Adapter.IsAuthenticated;

        public static List<int> RolesId => Adapter.RolesId;


        public static string UserIdentity => Adapter.UserIdentity;

        public static string Ip => Adapter.Ip;

        public static bool LogOff(ISessionManager sessionManager)
        {
            return Adapter.LogOff(sessionManager);

        }
    }
}