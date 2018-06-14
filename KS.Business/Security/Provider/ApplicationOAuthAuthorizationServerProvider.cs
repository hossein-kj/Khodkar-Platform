using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Owin;
using KS.Core.CacheProvider;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;
using KS.Core.Model.Develop;
using KS.Core.Security;
using KS.Core.UI.Setting;
using KS.DataAccess.Contexts.Base;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;

namespace KS.Business.Security.Provider
{
    public class ApplicationOAuthAuthorizationServerProvider: OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private string _debugId;
        public ApplicationOAuthAuthorizationServerProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException(nameof(publicClientId));
            }

            _publicClientId = publicClientId;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {


            context.AdditionalResponseParameters.Add("debugId", _debugId);
            //return Task.FromResult<object>(null);
            return base.TokenEndpoint(context);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
                //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });


                //var userManager = new ApplicationUserManager(new ApplicationUserStore(new ApplicationDbContext()), new DpapiDataProtectionProvider(), new EmailService(), new SmsService(), null);

                //var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();


                using (var scope = context.OwinContext.GetAutofacLifetimeScope())
                {
                    var claims = new List<Claim>();
                    var userManager = scope.Resolve<ApplicationUserManager>();
                    var securityContext = scope.Resolve<ISecurityContext>();
                    var user = await userManager.FindAsync(context.UserName, context.Password);
                    if (user == null)
                    {
                        context.SetError(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidLogin));

                        return;
                    }
                    if (user.Status == 0)
                    {
                        context.SetError(LanguageManager.ToAsErrorMessage(ExceptionKey.DisabledUser));
                        return;
                    }

                // TODO: Ajax Login 
                //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                //var coockieIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationType);
                claims.Add(new Claim(ClaimTypes.Name, context.UserName));
                    ////identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));



                    //var userRoles = await userManager.GetRolesAsync(user.Id);
                    //var userByRoles = await securityContext.Users.Where(us => us.Id == user.Id).Include(u => u.Roles).SingleOrDefaultAsync();
                    //foreach (var role in userRoles)
                    //{
                    //    claims.Add(new Claim(ClaimTypes.Role, role));
                    //}

                    //foreach (var role in userByRoles.Roles)
                    //{
                    //    claims.Add(new Claim(ClaimTypes.SerialNumber, role.RoleId.ToString()));
                    //    claims.Add(new Claim(ClaimTypes.Role, role.RoleId.ToString()));
                    //}




                    var usersGroups = await securityContext.ApplicationUserGroups.Where(ug => ug.UserId == user.Id).ToListAsync();

                    claims.AddRange(usersGroups.Select(@group => new Claim(ClaimTypes.GroupSid, @group.GroupId.ToString())));

                    AuthorizeManager.CacheUserRoles(usersGroups.Select(gr => gr.GroupId).ToList());




                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                    //identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                    //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

                    var identity = new ClaimsIdentity(claims, context.Options.AuthenticationType);


                    //AuthenticationProperties properties = CreateProperties(context.UserName);
                    //AuthenticationTicket ticket = new AuthenticationTicket(coockieIdentity, properties);
                    //context.Validated(ticket);


                    //var props = new AuthenticationProperties(new Dictionary<string, string>
                    //{
                    //    {
                    //        "userName",context.UserName
                    //    }
                    //});



                    //var ticket = new AuthenticationTicket(identity, props);
                    //context.Validated(ticket);


                    //var tiket = new AuthenticationTicket(identity, new AuthenticationProperties(
                    //    new Dictionary<string, string>
                    //    {
                    //        {
                    //            "debugId", debugId
                    //        }
                    //    }));
                    //tiket.
                    //context.Validated();


                    _debugId = SecureGuid.NewGuid().ToString("N");
                    if (Settings.IsDebugMode)
                    {
                        var groupsRoles = AuthorizeManager.GetUserRoles(usersGroups.Select(gr => gr.GroupId).ToList());
                        if (
                            !groupsRoles.Exists(
                                gr => gr.RolesId.Contains((int) Roles.Debug) || gr.RolesId.Contains((int) Roles.Admin)))
                        {

                            context.SetError(LanguageManager.ToAsErrorMessage(code: ExceptionKey.InvalidDebugGrant));
                            return;
                        }
                        // SourceControl.DebugUsers = SourceControl.DebugUsers ?? new List<DebugUser>();


                        //var debugUsers = new List<DebugUser>();

                        //var debugUsersCache =
                        //    CacheManager.GetForCurrentUserByKey<List<DebugUser>>(
                        //        CacheManager.GetDebugUserKey(CacheKey.DebugUser.ToString(),
                        //            CurrentUserManager.Id, CurrentUserManager.Ip));

                        //if (debugUsersCache.IsCached)
                        //{
                        //    debugUsers = debugUsersCache.Value;
                        //}



                        ////var debugUser =
                        ////      SourceControl.DebugUsers.FirstOrDefault(du => du.Ip == CurrentUserManager.Ip && du.UserId == user.Id);

                        //var debugUser =
                        //    debugUsers.FirstOrDefault(du => du.Ip == CurrentUserManager.Ip && du.UserId == user.Id);

                        //if (debugUser != null)
                        //{
                        //    debugUser.Guid = _debugId;
                        //    debugUser.LoginDateTime = DateTime.UtcNow;
                        //}
                        //else
                        //{
                            //SourceControl.DebugUsers.Add(
                            //debugUsers.Add(
                            //    new DebugUser()
                            //    {
                            //        Ip = CurrentUserManager.Ip,
                            //        UserId = user.Id,
                            //        Guid = _debugId,
                            //        LoginDateTime = DateTime.UtcNow
                            //    });
                        //}

                        CacheManager.Store(
                            context.UserName + CacheManager.GetDebugUserKey(CacheKey.DebugUser.ToString(),
                                user.Id, CurrentUserManager.Ip), new List<DebugUser>()
                            {
                                new DebugUser()
                                {
                                    Ip = CurrentUserManager.Ip,
                                    UserId = user.Id,
                                    Guid = _debugId,
                                    LoginDateTime = DateTime.UtcNow
                                }
                            }, slidingExpiration: TimeSpan.FromMinutes(60));

                    }

                    var id = new ClaimsIdentity(claims,
                                DefaultAuthenticationTypes.ApplicationCookie);

                    context.Validated(id);
                    context.Validated(identity);
                    //var ctx = Request.GetOwinContext();
                    var applicationSignInManager = scope.Resolve<ApplicationSignInManager>();
                    applicationSignInManager.AuthenticationManager.SignIn(id, identity);


                }

                // var passwordHasher = new ApplicationPasswordHasher();

                // var dbContext = new Context();
                // var passwordHash = await dbContext.GetUsersHashePasswordAsync(context.UserName);
                // if (passwordHash == null)
                // {
                //     context.SetError(Resources.ErrorMessages.InvalidGrant, Resources.ErrorMessages.InvalidLogin);
                //     return;
                // }

                // if (passwordHasher.VerifyHashedPassword(passwordHash.ToString(), context.Password) == PasswordVerificationResult.Failed)
                //{
                //    context.SetError(Resources.ErrorMessages.InvalidGrant, Resources.ErrorMessages.InvalidLogin);
                //    return;
                //}





                //coockieIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                //context.Request.Context.Authentication.SignIn(properties, coockieIdentity);
        }
        //public static AuthenticationProperties CreateProperties(string userName)
        //{
        //    IDictionary<string, string> data = new Dictionary<string, string>
        //    {
        //        { "userName", userName }
        //    };
        //    return new AuthenticationProperties(data);
        //}
    }
}