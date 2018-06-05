using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using KS.Core.FileSystemProvide;
using KS.Core.FileSystemProvide.Base;
using KS.Core.GlobalVarioable;
using KS.Core.Log.Base;
using KS.Core.Log.Job;
using KS.Core.Model.Log;
using LiteDB;

namespace KS.Core.Log
{
    public sealed class ActionLogManager : BaseActionLogManager, IActionLogManager
    {
        private readonly IFileSystemManager _fileSystemManager;
        static ActionLogManager()
        {

            CreateDb();

            SyncLogLiteDb.Start();
        }

        public ActionLogManager(IFileSystemManager fileSystemManager)
        {
            _fileSystemManager = fileSystemManager;
        }

        private static void CreateDb()
        {
            DbPath = new FileSystemManager().CreatDirectoryIfNotExist(Config.LogPath) + "ActionLog.db";
            LogCollectionName = "ActionLog";
            using (var db = new LiteDatabase(DbPath))
            {
                var logs = db.GetCollection<IActionLog>(LogCollectionName);
                logs.EnsureIndex(lg => lg.ExecutionTimeInMilliseconds);
                //logs.EnsureIndex(lg => lg.User);
                logs.EnsureIndex(lg => lg.Name);
                //logs.EnsureIndex(lg => lg.Type);
                //logs.EnsureIndex(lg => lg.Url);
                logs.EnsureIndex(lg => lg.ServiceUrl);
                //logs.EnsureIndex(lg => lg.IsMobileMode);
                logs.EnsureIndex(lg => lg.LocalDateTime);
                //logs.EnsureIndex(lg => lg.IsDebugMode);
                //logs.EnsureIndex(lg => lg.IsSuccessed);
                //logs.EnsureIndex(lg => lg.Parameters);
            }

       
        }

        public void BackUp(string backUpName)
        {
            lock (Lock)
            {
                GetActionById(1);

                if (!ToggleLogInMemoryOnly())
                    ToggleLogInMemoryOnly();

                _fileSystemManager.CopyFile(DbPath,
                    _fileSystemManager.CreatDirectoryIfNotExist(Config.LogPath)+ "ActionLog_" + backUpName + ".db");
                _fileSystemManager.DeleteFile(DbPath);

              

                CreateDb();

                if (ToggleLogInMemoryOnly())
                    ToggleLogInMemoryOnly();

                
            }

           
        }

        private static void BackGroundInsertToDataBase()
        {
            if(AddedLogDictionary == null)
                AddedLogDictionary = new ConcurrentDictionary<string, IActionLog>();
            try
            {

               
                    var addedLog = AddedLogDictionary.Select(item => item).ToList();
                    using (var db = new LiteDatabase(DbPath))
                    {

                        var logs = db.GetCollection<IActionLog>(LogCollectionName);
                        foreach (var log in addedLog)
                        {
                            log.Value.Id = 0;
                            //using (var tran = db.BeginTrans())
                            //{
                                logs.Insert(log.Value);
                                //tran.Commit();
                            //}
                            IActionLog removedLog;
                            AddedLogDictionary.TryRemove(log.Key,out removedLog);
                        }


                    }
                
            }
            catch (Exception ex)
            {
                LogException(ex.ToString());
            }
        }

        private static void BackGroundDeleteFromDataBase()
        {
            if(DeletedLogDictionary == null)
                DeletedLogDictionary = new List<int>();
            try
            {
                var deletedLog = DeletedLogDictionary.Select(item => item).ToList();


                    using (var db = new LiteDatabase(DbPath))
                    {
                        foreach (var id in deletedLog)
                        {

                            //using (var tran = db.BeginTrans())
                            //{
                                db.GetCollection<IActionLog>(LogCollectionName).Delete(lg => lg.Id == id);
                            //    tran.Commit();
                            //}
                        }


                    }
                    DeletedLogDictionary.RemoveAll(dl => deletedLog.Contains(dl));

            }
            catch (Exception ex)
            {

                LogException(ex.ToString());
            }

        }

        internal static void BackGroundLogOperation()
        {
            lock (Lock)
            {
                if (!LogInMemoryOnly)
                {
                    BackGroundDeleteFromDataBase();
                    BackGroundInsertToDataBase();
                }
            }
        }

    }
}
