using System;
using KS.Business.Security;
using KS.Core.GlobalVarioable;
using KS.Model.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace KS.WebSiteUI
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the application for OAuth based flow
            PublicClientId = "web";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthAuthorizationServerProvider(PublicClientId),
                //AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                //AuthorizeEndpointPath = new PathString("/api/Account/Authorize"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(Config.LoginExpireTimeSpanInHours),
                //AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(45),
                AuthenticationType = OAuthDefaults.AuthenticationType,
                AllowInsecureHttp = true,

            };

            //Enabling Cross-Origin 
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);



            // Configure the db context, user manager and signin manager to use a single instance per request
            //app.CreatePerOwinContext(DependencyManager.Get<SecurityContext>); 
            //app.CreatePerOwinContext(DependencyManager.Get<ApplicationUserManager>);
            //app.CreatePerOwinContext(DependencyManager.Get<ApplicationSignInManager>);
            //app.CreatePerOwinContext(DependencyManager.Get<ApplicationRoleManager>);
            //app.CreatePerOwinContext(ApplicationDbContext.Create);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            //app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            //app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);



            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString(Config.LoginUrl),
                ExpireTimeSpan = TimeSpan.FromHours(Config.LoginExpireTimeSpanInHours),
                //ExpireTimeSpan = TimeSpan.FromSeconds(45),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator
                        // ADD AN INT AS A THIRD TYPE ARGUMENT:

            .OnValidateIdentity<ApplicationUserManager, ApplicationUser, int>(

                validateInterval: TimeSpan.FromHours(Config.LoginExpireTimeSpanInHours),

                // THE NAMED ARGUMENT IS DIFFERENT:

                regenerateIdentityCallback: (manager, user)

                    => user.GenerateUserIdentityAsync(manager),

                    // Need to add THIS line because we added the third type argument (int) above:

                    getUserIdCallback: (claim) => int.Parse(claim.GetUserId()))

                }
            });

        

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);



      
           
            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});

            //app.UseYahooAuthentication("<YOUR CONSUMER KEY>", "<YOUR CONSUMER SECRET>");
        }
    }
}