using System;
using KS.DataAccess.Contexts;
using KS.Model.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KS.Business.Security
{
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole,
ApplicationUserClaim>, IUserStore<ApplicationUser, int>, IDisposable
    {
        //public ApplicationUserStore(): this(new IdentityDbContext())
        //{
        //    base.DisposeContext = true;
        //}

        public ApplicationUserStore(SecurityContext context)
            : base(context)
        {

        }

    }
}
