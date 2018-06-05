using System;
using System.Threading;
using System.Threading.Tasks;
using KS.Core.Log.Elmah.Base;
using KS.Core.Model;
using KS.Core.Model.Log;
using Microsoft.Owin;
using KS.Core.UI.Setting;

namespace KS.Core.Owin.Middleware
{
    public class LanguageAndCultureMiddleware : OwinMiddleware
    {
        private readonly IErrorLogManager _errorLogManager;

        public LanguageAndCultureMiddleware(OwinMiddleware next, IErrorLogManager errorLogManager) : base(next)
        {
            _errorLogManager = errorLogManager;
        }

        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                if (Settings.Culture != null)
                {
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Settings.Language);
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Settings.Language + "-" + Settings.Culture);
                }

            }
            catch (Exception ex)
            {
                _errorLogManager.LogException(new ExceptionLog()
                {
                    Detail = ex.ToString(),
                    Message = ex.Message,
                    Source = ex.GetType().FullName
                });
            }

            await Next.Invoke(context);


        }
    }
}
