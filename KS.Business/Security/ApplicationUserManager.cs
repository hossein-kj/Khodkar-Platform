using KS.Model.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace KS.Business.Security
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    // *** PASS IN TYPE ARGUMENT TO BASE CLASS:

    public class ApplicationUserManager : UserManager<ApplicationUser, int>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, int> store, IDataProtectionProvider dataProtectionProvider,
            EmailService emailService, SmsService smsService, IIdentityValidator<string> localizePasswordValidator, IPasswordHasher passwordHasher)
            : base(store)
        {
            
            //UserValidator = new LocalizeUserValidator<ApplicationUser, int>(this)
            //{
            //    AllowOnlyAlphanumericUserNames = false,
            //    RequireUniqueEmail = true
            //};

            //// Configure validation logic for passwords
            //PasswordValidator = new LocalizePasswordValidator
            //{
            //    RequiredLength = 6,
            //    RequireNonLetterOrDigit = true,
            //    RequireDigit = true,
            //    RequireLowercase = true,
            //    RequireUppercase = true,
            //};


            // Configure user lockout defaults
            UserLockoutEnabledByDefault = false;
            //DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.

            //RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser,int>
            //{
            //    MessageFormat = "Your security code is {0}"
            //});

            //RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser,int>
            //{
            //    Subject = "Security Code",
            //    BodyFormat = "Your security code is {0}"
            //});

            EmailService = emailService;
            SmsService = smsService;
            PasswordHasher = passwordHasher;

            UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, int>(dataProtectionProvider.Create("Khodkar"));
        }

        //    public override async Task<IList<string>> GetRolesAsync(int userId)
        //    {
        //         IList<string> roleNames = await base.GetRolesAsync(userId);
        //         var identityRoles = new List<string>();

        //         foreach (var roleName in roleNames)
        //         {
        //             identityRoles.Add(roleName);
        //         }

        //         return identityRoles; 
        //    }

    }
}
