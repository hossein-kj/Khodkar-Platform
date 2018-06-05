
using System.Threading.Tasks;
using Microsoft.Owin;
using KS.Core.Log.Base;

namespace KS.Core.Owin.Middleware
{
    public class ActionLogMiddleware : OwinMiddleware
    {
        private readonly IActionLogManager _actionLogManager;

        public ActionLogMiddleware(OwinMiddleware next, IActionLogManager actionLogManager) : base(next)
        {
            _actionLogManager = actionLogManager;
        }

        public override async Task Invoke(IOwinContext context)
        {

            _actionLogManager.StartLogRequest(context.Request);

            await Next.Invoke(context);

            _actionLogManager.EndLogRequest(context.Request, context.Response.StatusCode == 200);
            
            
        }
    }
}
