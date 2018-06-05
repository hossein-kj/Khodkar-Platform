using System;
using System.Web;

namespace KS.Core.Security.Handlers.AuthorizeScriptsDebugePath
{
    public class AuthorizeScriptsDebugePathAsyncHandler : IHttpAsyncHandler
    {
        public bool IsReusable => false;

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, Object extraData)
        {
            //context.Response.Write("<p>Begin IsThreadPoolThread is " + Thread.CurrentThread.IsThreadPoolThread + "</p>\r\n");

            context.Response.ContentType = "application/javascript";
            AuthorizeScriptsDebugePathAsynchOperation asynch = new AuthorizeScriptsDebugePathAsynchOperation
                (cb, context, null);
            asynch.StartAsyncWork();
            return asynch;

        
        }

        public void EndProcessRequest(IAsyncResult result)
        {
        }

        public void ProcessRequest(HttpContext context)
        {
           
        }
    }

   
}