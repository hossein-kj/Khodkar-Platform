using System;
using System.Threading;
using System.Web;
using KS.Core.Exceptions;
using KS.Core.FileSystemProvide;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;

namespace KS.Core.Security.Handlers.AuthorizeScriptsDebugePath
{
    public class AuthorizeScriptsDebugePathAsynchOperation : IAsyncResult
    {
        private bool _completed;
        private readonly Object _state;
        private readonly AsyncCallback _callback;
        private readonly HttpContext _context;

        bool IAsyncResult.IsCompleted => _completed;
        WaitHandle IAsyncResult.AsyncWaitHandle => null;
        Object IAsyncResult.AsyncState => _state;
        bool IAsyncResult.CompletedSynchronously => false;

        public AuthorizeScriptsDebugePathAsynchOperation(AsyncCallback callback, HttpContext context, Object state = null)
        {
            _callback = callback;
            _context = context;
            _state = state;
            _completed = false;
        }

        public void StartAsyncWork()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartAsyncTask), null);
        }

        private void StartAsyncTask(Object workItemState)
        {


            var path = 
                AuthorizeManager.AuthorizeDebugJavascriptPath(_context.Request.RawUrl);
            if(path != null)
            _context.Response.WriteFile(path.Replace("~", "").Replace("//", "/"));
            else
                throw new KhodkarInvalidException(string.Format(LanguageManager.ToAsErrorMessage(ExceptionKey.NotFound),
             _context.Request.RawUrl));

            _completed = true;
            _callback(this);
        }
    }
}