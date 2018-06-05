
using System.Collections.Generic;
using KS.Core.SessionProvider.Base;


namespace KS.Core.Security.Adapters
{
    public interface ICurrentUserAdapter
    {
        int Id { get; }

        string UserName { get; }

        string Email { get; }

        bool IsAuthenticated { get; }

        //List<string> Roles { get; }

        List<int> RolesId { get; }


        string UserIdentity { get; }

        string Ip { get; }

        bool LogOff(ISessionManager sessionManager);
    }
}