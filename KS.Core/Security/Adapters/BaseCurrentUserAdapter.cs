using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using KS.Core.CacheProvider;
using KS.Core.CodeManager;
using KS.Core.SessionProvider.Base;

namespace KS.Core.Security.Adapters
{
    public abstract class BaseCurrentUserAdapter
    {
        public virtual int Id
        {
            get
            {
                try
                {


                    var identity = Thread.CurrentPrincipal.Identity.IsAuthenticated ? (ClaimsPrincipal)Thread.CurrentPrincipal
                        : (ClaimsPrincipal)HttpContext.Current.User ?? (ClaimsPrincipal)Thread.CurrentPrincipal;

                    return Convert.ToInt32(identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                                       .Select(c => c.Value).FirstOrDefault());
                }
                catch (Exception)
                {
                    //in external Login Mode May be User Id Not Int
                    return 0;
                }
            }
        }

        public virtual string UserName
        {
            get
            {
                var identity = Thread.CurrentPrincipal.Identity.IsAuthenticated ? (ClaimsPrincipal)Thread.CurrentPrincipal
                        : (ClaimsPrincipal)HttpContext.Current.User ?? (ClaimsPrincipal)Thread.CurrentPrincipal;

                return identity.Claims.Where(c => c.Type == ClaimTypes.Name)
                                   .Select(c => c.Value).FirstOrDefault();
            }
        }

        public virtual string Email
        {
            get
            {
                var identity = Thread.CurrentPrincipal.Identity.IsAuthenticated ? (ClaimsPrincipal)Thread.CurrentPrincipal
                        : (ClaimsPrincipal)HttpContext.Current.User ?? (ClaimsPrincipal)Thread.CurrentPrincipal;

                return identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                                   .Select(c => c.Value).FirstOrDefault();
            }
        }

        public virtual bool IsAuthenticated => HttpContext.Current.User == null ? Thread.CurrentPrincipal.Identity.IsAuthenticated : HttpContext.Current.User.Identity.IsAuthenticated;

        //public virtual List<string> Roles
        //{
        //    get
        //    {
        //        var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

        //        return identity.Claims.Where(c => c.Type == ClaimTypes.Role)
        //                           .Select(c => c.Value).ToList();
        //    }
        //}

        public virtual List<int> RolesId
        {
            get
            {
                var identity = Thread.CurrentPrincipal.Identity.IsAuthenticated ? (ClaimsPrincipal)Thread.CurrentPrincipal
                        : (ClaimsPrincipal)HttpContext.Current.User ?? (ClaimsPrincipal)Thread.CurrentPrincipal;

                var userGroups = identity.Claims.Where(c => c.Type == ClaimTypes.GroupSid)
                                   .Select(c => c.Value).ToList();

                var rolesId = new List<int>();
                foreach (var groupRole in AuthorizeManager.GetUserRoles(userGroups.Select(int.Parse).ToList()).Select(ur => ur.RolesId))
                {
                    rolesId.AddRange(groupRole);
                }

                return rolesId;
            }
        }


        public virtual string UserIdentity => HttpContext.Current.User == null ? (Thread.CurrentPrincipal.Identity.Name ?? "") : (HttpContext.Current.User.Identity.Name ?? "");


        public virtual string Ip => System.Web.HttpContext.Current == null ? "" : System.Web.HttpContext.Current.Request.UserHostAddress ?? "";

        public virtual bool LogOff(ISessionManager sessionManager)
        {
            CacheManager.ClearCurrentUserCache();
            //var debugUser = SourceControl.DebugUsers?.Find(du => du.UserId == CurrentUserManager.Id);



            //    if (debugUser != null)
            //        SourceControl.DebugUsers?.Remove(debugUser);



            sessionManager.Abandon();
            return true;



        }
    }
}