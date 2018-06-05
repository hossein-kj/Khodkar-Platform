
using System.Web;
using System.Web.SessionState;




namespace KS.WebSiteUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //protected void Application_Start()
        //{

           
        //}

        protected void Application_PostAuthorizeRequest()
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }

        //void Session_Start(object sender, EventArgs e)
        //{
          
        //}

        //void Session_End(object sender, EventArgs e)
        //{

        //}
    }
}
