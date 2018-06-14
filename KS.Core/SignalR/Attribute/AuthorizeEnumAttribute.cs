using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using KS.Core.GlobalVarioable;
using KS.Core.Security;

namespace KS.Core.SignalR.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeClaimsAttribute : AuthorizeAttribute
    {
        public AuthorizeClaimsAttribute(params Roles[] roles)
        {


            this.Roles = string.Join(",", roles.Select(r => (int)r));
        }

        protected override bool UserAuthorized(System.Security.Principal.IPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            //var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var identity = user as ClaimsPrincipal;
            var authenticated = identity?.Identity;
            if (authenticated != null && authenticated.IsAuthenticated)
            {
                var userGroups = identity?.Claims.Where(c => c.Type == ClaimTypes.GroupSid)
                    .Select(c => c.Value).ToList();

                var rolesId = new List<int>();
                foreach (
                    var groupRole in
                    AuthorizeManager.GetUserRoles(userGroups?.Select(int.Parse).ToList()).Select(ur => ur.RolesId))
                {
                    rolesId.AddRange(groupRole);
                }
                if (Roles.Split(',').Any(b => rolesId.Any(a => a.ToString() == b)))
                    return true;

                return false;
            }
            return false;

        }
    }
}
