
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KS.Core.CacheProvider;
using KS.Core.CodeManager;
using KS.Core.GlobalVarioable;
using KS.Core.Model.Develop;
using KS.Core.Security;
using KS.Core.Utility;
using Microsoft.Owin;

namespace KS.Core.Owin.Middleware
{
    public class ScriptsDebugePathMiddleware : OwinMiddleware
    {
        public ScriptsDebugePathMiddleware(OwinMiddleware next) : base(next)
        {
        }

        private string GetPath(string url)
        {
            var path = Helper.ProperPath(url).Replace("//", "/");
            path = path.IndexOf("?", StringComparison.OrdinalIgnoreCase) > -1 ? path.Remove(path.IndexOf("?", StringComparison.OrdinalIgnoreCase)) : path;

            if (path.IndexOf(Config.DebugIdSign, StringComparison.OrdinalIgnoreCase) > -1)
            {
                var debugId =
                    path.Substring(
                        path.IndexOf(Config.DebugIdSign, StringComparison.OrdinalIgnoreCase) +
                        Config.DebugIdSign.Length + 1, 32);
                var realPath = path.Remove(path.IndexOf(Config.DebugIdSign, StringComparison.OrdinalIgnoreCase),
                    Config.DebugIdSign.Length + 34);


               

                var debugUsersCache = CacheManager.GetForCurrentUserByKey<List<DebugUser>>(CacheManager.GetDebugUserKey(CacheKey.DebugUser.ToString(),
                    CurrentUserManager.Id, CurrentUserManager.Ip));

            

                var debug =
                    debugUsersCache.Value?.FirstOrDefault(
                        du => du.Guid == debugId && du.Ip == CurrentUserManager.Ip);
                if (debug != null)
                {

                    if (realPath.IndexOf(Config.ScriptDebugPagesPath, StringComparison.OrdinalIgnoreCase) > -1 &&
                        debugUsersCache.IsCached)
                        return realPath.Replace("~/", Config.ScriptDebugPagesPath);


                        return realPath.Replace("~/", Config.ScriptDebugPath.ToLower());

                }
                path = realPath;
            }

            return path.IndexOf(Config.ScriptDebugPagesPath, StringComparison.OrdinalIgnoreCase) > -1 
                ? path.Replace(Config.ScriptDebugPagesPath.ToLower(), Config.ScriptDistPagesPath) : 
                path.Replace(Config.ScriptDebugPath.ToLower(), Config.ScriptDistPath);
        }

        public override async Task Invoke(IOwinContext context)
        {
            var requestUrl = context.Request.Path.Value.ToLower();
            if (requestUrl.StartsWith("/scripts/debug/"))
            {
                context.Response.Redirect(GetPath(requestUrl).Replace("~/", "/"));
            }
            await Next.Invoke(context);
        }
    }
}
