using KS.Model.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace KS.Business.Security
{

    // Configure the application sign-in manager which is used in this application.
    // PASS INT AS TYPE ARGUMENT TO BASE INSTEAD OF STRING:
    public class ApplicationSignInManager : SignInManager<ApplicationUser, int>
    {

        public ApplicationSignInManager(

            ApplicationUserManager userManager, IAuthenticationManager authenticationManager) :

            base(userManager, authenticationManager) { }



        //public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        //{

        //    return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);

        //}



        public static ApplicationSignInManager Create(

         IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {

            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);

        }

    }
}
