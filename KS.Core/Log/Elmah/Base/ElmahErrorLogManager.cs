using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using Elmah;
using KS.Core.FileSystemProvide;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;
using KS.Core.Log.Job;
using KS.Core.Model;
using KS.Core.Model.Log;
using LiteDB;
using Newtonsoft.Json.Linq;
using KS.Core.UI.Setting;

namespace KS.Core.Log.Elmah.Base
{
    public class ElmahErrorLogManager : ErrorLog, IElmahErrorLogManager
    {
        private static string _logCollectionName;
        private static string _dbPath;
        protected static readonly object Lock = new object();
        protected static ConcurrentDictionary<string, ExceptionLog> AddedLogDictionary;
        protected static List<int> DeletedLogDictionary;
        protected static bool LogInMemoryOnly = false;

        static ElmahErrorLogManager()
        {
            CreateDb();

            AddedLogDictionary = new ConcurrentDictionary<string, ExceptionLog>();
            DeletedLogDictionary = new List<int>();

            SyncLogLiteDb.Start();
        }

        private static void CreateDb()
        {
            _dbPath = new FileSystemManager().CreatDirectoryIfNotExist(Config.LogPath) + "ErrorLog.db";
            _logCollectionName = "ExceptionLog";

            using (var db = new LiteDatabase(_dbPath))
            {
                var logs = db.GetCollection<ExceptionLog>(_logCollectionName);
                //logs.EnsureIndex(lg => lg.Id);
                //logs.EnsureIndex(lg => lg.User);
                //logs.EnsureIndex(lg => lg.Message);
                //logs.EnsureIndex(lg => lg.Type);
                //logs.EnsureIndex(lg => lg.StatusCode);
                //logs.EnsureIndex(lg => lg.Source);
                //logs.EnsureIndex(lg => lg.Time);
                logs.EnsureIndex(lg => lg.LocalDateTime);
                //logs.EnsureIndex(lg => lg.WebHostHtmlMessage);
            }
        }

        public virtual void BackUp(string backUpName)
        {
            var fileSystemManager = new FileSystemManager();
            lock (Lock)
            {
                GetErrorById("1");

                if (!ToggleLogInMemoryOnly())
                    ToggleLogInMemoryOnly();

                fileSystemManager.CopyFile(_dbPath,
                    fileSystemManager.CreatDirectoryIfNotExist(Config.LogPath) + "ErrorLog_" + backUpName + ".db");
                fileSystemManager.DeleteFile(_dbPath);

               

                CreateDb();

                if (ToggleLogInMemoryOnly())
                    ToggleLogInMemoryOnly();

            }
        }

        protected bool ToggleLogInMemoryOnly()
        {
            LogInMemoryOnly = !LogInMemoryOnly;
            return LogInMemoryOnly;
        }

        public List<ExceptionLog> GetByPagination(string orderBy, int skip, int take, string typeOrSourceOrMessage, string user, string fromDateTime, string toDateTime, out int count)
        {


            lock (Lock)
            {
                var orders = orderBy.Split(' ');
                Query query = null;
                if (user.Trim() != "!")
                    query = Query.Contains("User", user);
                if (typeOrSourceOrMessage.Trim() != "!")
                {
                    var query2 = Query.Or(Query.Contains("Type", typeOrSourceOrMessage),
                        Query.Contains("Source", typeOrSourceOrMessage),
                        Query.Contains("Message", typeOrSourceOrMessage));

                    query = query == null ? query2 : Query.And(query2, query);
                }
                if (fromDateTime.Trim() != "!")
                {
                    query = query == null ? Query.GTE("LocalDateTime", fromDateTime.Trim()) :
                        Query.And(Query.GTE("LocalDateTime", fromDateTime.Trim()), query);
                }

                if (toDateTime.Trim() != "!")
                {
                    query = query == null ? Query.LTE("LocalDateTime", toDateTime.Trim()) :
                        Query.And(Query.LTE("LocalDateTime", toDateTime.Trim()), query);
                }



                if (query == null)
                {

                    query = orders[orders.Length - 1] == "desc" ? Query.All(orders[0], Query.Descending) : Query.All(orders[0], Query.Ascending);
                }
                else
                {
                    var query2 = orders[orders.Length - 1] == "desc" ? Query.All(orders[0], Query.Descending) : Query.All(orders[0], Query.Ascending);
                    query = Query.And(query2, query);
                }

                using (var db = new LiteDatabase(_dbPath))
                {
                    count = db.GetCollection<ExceptionLog>(_logCollectionName).Count(query);





                    return db.GetCollection<ExceptionLog>(_logCollectionName)
                            .Find(query, skip, take)
                            .ToList();

                }
            }
        }

        public ElmahErrorLogManager(IDictionary config)
        {

        }

        public override ErrorLogEntry GetError(string id)
        {
            lock (Lock)
            {
                var logId = Convert.ToInt32(id);
                using (var db = new LiteDatabase(_dbPath))
                {

                    //var elmahError = db.GetCollection<ElmahError>(_logCollectionName).FindOne(Query.Where("User", user => roles.Contains(user) ));
                    var elmahError = db.GetCollection<ExceptionLog>(_logCollectionName).FindOne(lg => lg.Id == logId);
                    return new ErrorLogEntry(this, id, ErrorXml.DecodeString(elmahError.Error));
                }
            }
        }

        public ExceptionLog GetErrorById(string id)
        {
            lock (Lock)
            {
                var logId = Convert.ToInt32(id);
                using (var db = new LiteDatabase(_dbPath))
                {
                    return db.GetCollection<ExceptionLog>(_logCollectionName).FindOne(lg => lg.Id == logId);
                }
            }
        }

        public override int GetErrors(int pageIndex, int pageSize, IList errorEntryList)
        {
            lock (Lock)
            {
                using (var db = new LiteDatabase(_dbPath))
                {

                    var logs = db.GetCollection<ExceptionLog>(_logCollectionName)
                        .Find(Query.All(Query.Descending), skip: (pageIndex - 1)*pageSize, limit: pageSize);
                    foreach (var log in logs)
                    {
                        errorEntryList.Add(new ErrorLogEntry(this, log.Id.ToString(), ErrorXml.DecodeString(log.Error)));
                    }
                    return db.GetCollection<ExceptionLog>(_logCollectionName).Count(Query.All(Query.Descending));
                }
            }
        }

        public void LogException(ExceptionLog exceptionLog)
        {
            Log(new Error()
            {
                Message = exceptionLog.Message,
                Detail = exceptionLog.Detail,
                Type = exceptionLog.Type,
                Time = DateTime.Now
            });
        }



        public override string Log(Error error)
        {
            try
            {
                var elmahError = new ExceptionLog()
                {
                    Error = ErrorXml.EncodeString(error),
                    //ApplicationName = error.ApplicationName,
                    //Cookies = error.Cookies,
                    Detail = error.Detail,
                    //Exception = error.Exception,
                    //Form = error.Form,
                    //HostName = error.HostName,
                    LocalDateTime = LanguageManager.ToLocalDateTime(error.Time.ToUniversalTime()),
                    Time = error.Time.ToUniversalTime(),
                    Message = error.Message,
                    //QueryString = error.QueryString,
                    //ServerVariables = error.ServerVariables,
                    Source = error.Source,
                    StatusCode = error.StatusCode,
                    Type = error.Type,
                    User = error.User,
                    WebHostHtmlMessage = error.WebHostHtmlMessage,
                    IsDebugMode = Settings.IsDebugMode,
                    IsMobileMode = Settings.IsMobileMode
                };
                AddedLogDictionary.TryAdd(Guid.NewGuid().ToString(), elmahError);
              
            }
            catch (Exception ex)
            {
                LogException(ex.ToString());
            }
            return "1";
        }


        public bool Delete(JObject data)
        {
            dynamic dataDto = data;
   

            JArray idArray = dataDto.Ids;
            var ids = idArray.ToObject<int[]>();

                    foreach (var id in ids)
                    {
                        DeletedLogDictionary.Add(id);
                    } 

            return true;
        }


        private static void BackGroundInsertToDataBase()
        {
            if(AddedLogDictionary == null)
                AddedLogDictionary = new ConcurrentDictionary<string, ExceptionLog>();
            try
            {
                var addedLog = AddedLogDictionary.Select(item => item).ToList();
               
                    using (var db = new LiteDatabase(_dbPath))
                    {

                        var logs = db.GetCollection<ExceptionLog>(_logCollectionName);
                        foreach (var log in addedLog)
                        {
                            log.Value.Id = 0;
                            //using (var tran = db.BeginTrans())
                            //{
                                logs.Insert(log.Value);
                            //    tran.Commit();
                            //}
                            ExceptionLog removedLog;
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

                
                    using (var db = new LiteDatabase(_dbPath))
                    {
                        foreach (var id in deletedLog)
                        {
                            //using (var tran = db.BeginTrans())
                            //{
                                db.GetCollection<ExceptionLog>(_logCollectionName)
                                    .Delete(lg => lg.Id == id);
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
            if (LogInMemoryOnly) return;
            lock (Lock)
            {
                BackGroundDeleteFromDataBase();
                BackGroundInsertToDataBase();
            }
        }

        private static void LogException(string exception)
        {
            
                var path = HostingEnvironment.MapPath(Config.LogPath + LanguageManager.ToLocalDateTime(DateTime.UtcNow)
                                                  .Replace("/", "-")
                                                  .Replace(":", "-")
                                                  .Replace(" ", "-") + ".txt");
                if (path == null) return;
                using (var fs = new FileStream(path, System.IO.FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite | FileShare.Delete))
                {
                    using (var strWriter = new StreamWriter(fs, Encoding.UTF8))
                    {
                        strWriter.Write(exception);
                    }
                }

        }
    }
}
