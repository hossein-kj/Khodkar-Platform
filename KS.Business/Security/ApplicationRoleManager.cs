using KS.DataAccess.Contexts;
using KS.DataAccess.Contexts.Base;
using KS.Model.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace KS.Business.Security
{
    // PASS CUSTOM APPLICATION ROLE AND INT AS TYPE ARGUMENTS TO BASE:

    public class ApplicationRoleManager : RoleManager<ApplicationRole, int>
    {

        // PASS CUSTOM APPLICATION ROLE AND INT AS TYPE ARGUMENTS TO CONSTRUCTOR:

        public ApplicationRoleManager(IRoleStore<ApplicationRole, int> roleStore)
            : base(roleStore)
        {

        }


        // PASS CUSTOM APPLICATION ROLE AS TYPE ARGUMENT:

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {

            return new ApplicationRoleManager(

                new ApplicationRoleStore(context.Get<SecurityContext>()));

        }
    }
}
