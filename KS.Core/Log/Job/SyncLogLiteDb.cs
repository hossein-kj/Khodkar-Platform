using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Web.Hosting;
using KS.Core.FileSystemProvide;
using KS.Core.GlobalVarioable;

namespace KS.Core.Log.Job
{
    internal static class SyncLogLiteDb
    {
        private static readonly object Lock = new object();
        private static bool _running = false;
        //private static bool _firstRun = true;
        internal static void Start()
        {
            lock (Lock)
            {
                if (_running) return;
                //if(_firstRun)
                //{
                //    var temp = new Elmah.Base.ElmahErrorLogManager(null);
                //    var temp2 = new ActionLogManager(new FileSystemManager());
                //    _firstRun = false;
                //}
                HostingEnvironment.QueueBackgroundWorkItem(cancellationToken =>
                {

                    while (!cancellationToken.IsCancellationRequested)
                    {
                        ActionLogManager.BackGroundLogOperation();
                        Elmah.Base.ElmahErrorLogManager.BackGroundLogOperation();
                        Thread.Sleep(Config.LogBackgroundJobIntervalsInMilliseconds);
                    }
                });
                _running = true;
            }
        }
       
    }
}
