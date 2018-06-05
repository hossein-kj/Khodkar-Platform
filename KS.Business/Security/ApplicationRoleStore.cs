using System;
using KS.DataAccess.Contexts;
using KS.Model.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using KS.DataAccess.Contexts.Base;

namespace KS.Business.Security
{
    public class ApplicationRoleStore : RoleStore<ApplicationRole, int, ApplicationUserRole>, IQueryableRoleStore<ApplicationRole, int>, IRoleStore<ApplicationRole, int>, IDisposable
    {

        //public ApplicationRoleStore()

        //    : base(new IdentityDbContext())
        //{

        //    base.DisposeContext = true;

        //}



        public ApplicationRoleStore(SecurityContext context)

            : base(context)
        {

        }

    }
}
