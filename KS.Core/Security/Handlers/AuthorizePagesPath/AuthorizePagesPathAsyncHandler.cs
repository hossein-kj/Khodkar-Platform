using System;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using KS.Core.Log.Elmah.Base;
using KS.Core.Data.Contexts.Base;

namespace KS.Core.Security.Handlers.AuthorizePagesPath
{
    public class AuthorizePagesPathAsyncHandler : IHttpAsyncHandler
    {
        public bool IsReusable => false;

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, Object extraData)
        {
            //context.Response.Write("<p>Begin IsThreadPoolThread is " + Thread.CurrentThread.IsThreadPoolThread + "</p>\r\n");
            var errorLogManager = AutofacDependencyResolver.Current.RequestLifetimeScope.Resolve<IErrorLogManager>();
            var dataBaseContextManager = AutofacDependencyResolver.Current.RequestLifetimeScope.Resolve<IDataBaseContextManager>();
            context.Response.ContentType = "application/json";
            var asynch = new AuthorizePagesPathAsynchOperation
                (cb, context, errorLogManager, dataBaseContextManager, null);
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