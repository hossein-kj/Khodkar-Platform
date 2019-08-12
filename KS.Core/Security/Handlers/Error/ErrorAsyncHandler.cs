using System;
using System.Net;
using System.Web;


namespace KS.Core.Security.Handlers.Error
{
    public class ErrorAsyncHandler : IHttpHandler
    {
        public bool IsReusable => false;

        //public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, Object extraData)
        //{
        //    //context.Response.Write("<p>Begin IsThreadPoolThread is " + Thread.CurrentThread.IsThreadPoolThread + "</p>\r\n");
        //    context.Response.ContentType = "application/json";
        //    var asynch = new ErrorAsynchOperation
        //        (cb, context,  null);
        //    asynch.StartAsyncWork();
        //    return asynch;


        //}

        public void EndProcessRequest(IAsyncResult result)
        {
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated == false)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.Write("error");
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.Write("error");
            }
        }
    }
}
