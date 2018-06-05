using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Hosting;
using KS.Core.CoockieProvider;
using KS.Core.GlobalVarioable;
using KS.Core.Model;
using KS.Core.Security;
using KS.Core.Utility;
using LiteDB;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq.Dynamic;
using KS.Core.Localization;
using KS.Core.Model.Log;
using KS.Core.UI.Setting;

namespace KS.Core.Log.Base
{
    public abstract class BaseActionLogManager
    {
        protected static string DbPath;
        protected static string LogCollectionName;
        protected static ConcurrentDictionary<string, IActionLog> AddedLogDictionary;
        protected static List<int> DeletedLogDictionary;
        protected static readonly object Lock = new object();
        protected static bool LogInMemoryOnly = false;
        protected static bool EnableLog = true;

        public string UrlReferrer { get; set; }
        public string StartTime { get; set; }
        public string Body { get; set; }
        public string ServiceName { get; set; }
        public string ServiceUrl { get; set; }

        static BaseActionLogManager()
        {
            AddedLogDictionary = new ConcurrentDictionary<string, IActionLog>();
            DeletedLogDictionary = new List<int>();
        }

        public virtual bool GetLogStatus()
        {
            return EnableLog;
        }

        public virtual bool ToggleEnableLogUntilNextApplicationRestart()
        {
            EnableLog = !EnableLog;
            return EnableLog;
        }

        protected bool ToggleLogInMemoryOnly()
        {
            LogInMemoryOnly = !LogInMemoryOnly;
            return LogInMemoryOnly;
        }


        public virtual List<IActionLog> GetByPagination(string orderBy, int skip, int take, string serviceUrl, string nameOrUrlOrUser, string fromDateTime, string toDateTime, out int count)
        {

            lock (Lock)
            {
                var orders = orderBy.Split(' ');
                Query query = null;
                if (serviceUrl.Trim() != "!")
                    query = Query.EQ("ServiceUrl", serviceUrl);
                if (nameOrUrlOrUser.Trim() != "!")
                {
                    var query2 = Query.Or(Query.Contains("Url", nameOrUrlOrUser), Query.Contains("Name", nameOrUrlOrUser), Query.Contains("User", nameOrUrlOrUser));
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

                using (var db = new LiteDatabase(DbPath))
                {
                    count = db.GetCollection<IActionLog>(LogCollectionName).Count(query);





                    return db.GetCollection<IActionLog>(LogCollectionName)
                            .Find(query, skip, take)
                            .ToList(); 

                }
            }

            //lock (Lock)
            //{
            //    var orders = orderBy.Split(' ');
            //    using (var db = new LiteDatabase(DbPath))
            //    {
            //        count = db.GetCollection<IActionLog>(LogCollectionName).Count(Query.All(Query.Descending));
            //        return orders[orders.Length - 1] == "desc" ?
            //            db.GetCollection<IActionLog>(LogCollectionName).Find(Query.All(orders[0], Query.Descending), skip: skip, limit: take).ToList()
            //            : db.GetCollection<IActionLog>(LogCollectionName).Find(Query.All(orders[0], Query.Ascending), skip: skip, limit: take).ToList();
            //    }
            //}

        }

        public virtual List<IActionLog> GetByServiceUrlAndPagination(string orderBy, int skip, int take, string serviceUrl, string user, string fromDateTime, string toDateTime, out int count)
        {

            lock (Lock)
            {
                var orders = orderBy.Split(' ');
                Query query =  Query.EQ("ServiceUrl", serviceUrl);
                if (user.Trim() != "!")
                {
                    var query2 = Query.Contains("User", user);
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

                using (var db = new LiteDatabase(DbPath))
                {
                    count = db.GetCollection<IActionLog>(LogCollectionName).Count(query);





                    return db.GetCollection<IActionLog>(LogCollectionName)
                            .Find(query, skip, take)
                            .ToList();

                }
            }


        }

        //public virtual List<IActionLog> GetByServiceUrlAndPagination(string serviceUrl, string orderBy, int skip, int take, out int count)
        //{


        //    lock (Lock)
        //    {
        //        var orders = orderBy.Split(' ');
        //        using (var db = new LiteDatabase(DbPath))
        //        {
        //            var query = Query.EQ("ServiceUrl", serviceUrl);
        //            count = db.GetCollection<IActionLog>(LogCollectionName).Count(query);

        //            var query2 = orders[orders.Length - 1] == "desc" ? Query.All(orders[0], Query.Descending) : Query.All(orders[0], Query.Ascending);
        //            query = Query.And(query2, query);

        //            return
        //                db.GetCollection<IActionLog>(LogCollectionName)
        //                    .Find(query, skip: skip, limit: take)
        //                    .ToList();
        //        }
        //    }

        //}

        public virtual IActionLog GetActionById(int id)
        {
            lock (Lock)
            {
                using (var db = new LiteDatabase(DbPath))
                {
                    return db.GetCollection<IActionLog>(LogCollectionName).FindOne(lg => lg.Id == id);
                } 
            }
        }

        public void StartLogRequest(IOwinRequest request)
        {
            try
            {
                if (!Config.EnableActionLog || !EnableLog) return;
                StartTime = DateTime.Now.TimeOfDay.ToString();
                if (request.Method.ToLower() == "post")
                {
                    Body = new StreamReader(request.Body).ReadToEnd();
                    byte[] requestData = Encoding.UTF8.GetBytes(Body);
                    var indexPassword = Body.IndexOf("password=", StringComparison.OrdinalIgnoreCase);
                    if (indexPassword > -1)
                    {
                        if (Body.Substring(indexPassword).IndexOf("&", StringComparison.OrdinalIgnoreCase) > -1)
                            Body= Body.Replace(
                                Body.Substring(indexPassword,
                                    Body.Substring(indexPassword).IndexOf("&", StringComparison.OrdinalIgnoreCase)), "");
                        else
                        {
                            Body = Body.Replace(
                                Body.Substring(indexPassword), "");
                        }
                    }

               
                    request.Body = new MemoryStream(requestData);
                }

                request.Headers.Add("asRequestId", new[] { SecureGuid.NewGuid().ToString() });
            }
            catch (Exception ex)
            {
                LogException(ex.ToString());
            }
        }

        private string ReadableStringCollectionToString(IReadableStringCollection collection)
        {
            var result = "";
            foreach (var item in collection)
            {
                result += item.Key + " : [" + string.Join(",", item.Value) + "];";
            }

            return result.TrimEnd(';');
        }


        public void EndLogRequest(IOwinRequest request, bool isSuccessed)
        {
            try
            {
                if (!Config.EnableActionLog || !EnableLog) return;
                if (ServiceName == null) return;
                double duration = 0;
                if (StartTime != null)
                {
                    duration = (DateTime.Now.TimeOfDay - TimeSpan.Parse(StartTime) ).TotalMilliseconds;
                    
                }


            

                    AddedLogDictionary.TryAdd(SecureGuid.NewGuid().ToString(), new ActionLog()
                {
                    ServiceUrl = ServiceUrl ?? "?",
                    Ip = request.RemoteIpAddress,
                    Url = request.Path.Value,
                    Parameters = Helper.UrlDecode(request.Method.ToLower() == "get" ? ReadableStringCollectionToString(request.Query) : Body),
                           //ReadableStringCollectionToString(request.ReadFormAsync().Result)),
                    Type = request.Method,
                    Coockies = JsonConvert.SerializeObject(CookieManager.GetAll()),
                    Request = Helper.UrlDecode(ReadableStringCollectionToString(request.Headers)),
                    DateTime = DateTime.UtcNow,
                    IsDebugMode = Settings.IsDebugMode,
                    IsMobileMode = Settings.IsMobileMode,
                    LocalDateTime = LanguageManager.ToLocalDateTime(DateTime.UtcNow),
                    Name = ServiceName ?? "?",
                    User = CurrentUserManager.UserName,
                    ExecutionTimeInMilliseconds= duration,
                    IsSuccessed=isSuccessed,
                    UrlReferrer =
                            Helper.UrlDecode(
                                (UrlReferrer ?? request.Uri.AbsoluteUri ?? new Uri("http://unknown").AbsoluteUri).ToString())

                });
            }
            catch (Exception ex)
            {

                LogException(ex.ToString());
            }
        }

        public virtual void LogMvcService(string name, HttpRequestBase request, string serviceUrl)
        {
            try
            {
                if (!Config.EnableActionLog || !EnableLog) return;
                ServiceName = name;

                ServiceUrl= serviceUrl;
                UrlReferrer=Helper.UrlDecode((request.UrlReferrer ?? request.Url ?? new Uri("http://unknown")).ToString());

                //AddedLogDictionary.Add(Guid.NewGuid().ToString(), new ActionLog()
                //{
                //    ServiceUrl = serviceUrl,
                //    Url = request.Path,
                //    Parameters = Helper.UrlDecode(request.Params.ToString()),
                //    Type = request.HttpMethod,
                //    Coockies = JsonConvert.SerializeObject(CookieManager.GetAll()),
                //    Request = Helper.UrlDecode(request.Headers.ToString()),
                //    DateTime = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
                //    IsDebugMode = Setting.IsDebugMode,
                //    IsMobileMode = Setting.IsMobileMode,
                //    LocalDateTime = Helper.ToLocalDateTime(DateTime.UtcNow),
                //    Name = name,
                //    User = CurrentUserManager.Email,
                //    UrlReferrer =
                //            Helper.UrlDecode(
                //                (request.UrlReferrer ?? request.Url ?? new Uri("http://unknown")).ToString())



                //});
            }
            catch (Exception ex)
            {

                 LogException(ex.ToString());
            }

        }

        public virtual void LogHttpService(string name,HttpRequestMessage request,string serviceUrl)
        {
            try
            {
                if (!Config.EnableActionLog || !EnableLog) return;
                ServiceName = name;

                ServiceUrl = serviceUrl;

                //AddedLogDictionary.Add(Guid.NewGuid().ToString(), new ActionLog()
                //{
                //    ServiceUrl = serviceUrl,
                //    Url = Helper.UrlDecode(request.RequestUri.AbsoluteUri),
                //    Parameters = Helper.UrlDecode(request.Method.Method.ToLower() == "get" ? request.RequestUri.Query :
                //           request.Content.ReadAsStringAsync().Result),
                //    Type = request.Method.Method,
                //    Coockies = JsonConvert.SerializeObject(request.Headers.GetCookies().Select(c => c.Cookies)),
                //    Request = Helper.UrlDecode(request.ToString()),
                //    DateTime = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
                //    IsDebugMode = Setting.IsDebugMode,
                //    IsMobileMode = Setting.IsMobileMode,
                //    LocalDateTime = Helper.ToLocalDateTime(DateTime.UtcNow),
                //    Name = name,
                //    User = CurrentUserManager.Email



                //});
            }
            catch (Exception ex)
            {

                LogException(ex.ToString());
            }
        }
        public virtual void LogOdataService(string name, HttpRequestMessage request)
        {


            try
            {
                if (!Config.EnableActionLog || !EnableLog) return;
                ServiceName = name;

                ServiceUrl = request.RequestUri.AbsolutePath;

                //AddedLogDictionary.Add(Guid.NewGuid().ToString(), new ActionLog()
                //{
                //    ServiceUrl = request.RequestUri.AbsolutePath,
                //    Url = Helper.UrlDecode(request.RequestUri.AbsoluteUri),
                //    Parameters = Helper.UrlDecode(request.Method.Method.ToLower() == "get" ? request.RequestUri.Query :
                //               request.Content.ReadAsStringAsync().Result),
                //    Type = request.Method.Method,
                //    Coockies = JsonConvert.SerializeObject(request.Headers.GetCookies().Select(c => c.Cookies)),
                //    Request = Helper.UrlDecode(request.ToString()),
                //    DateTime = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
                //    IsDebugMode = Setting.IsDebugMode,
                //    IsMobileMode = Setting.IsMobileMode,
                //    LocalDateTime = Helper.ToLocalDateTime(DateTime.UtcNow),
                //    Name = name,
                //    User = CurrentUserManager.Email



                //});
            }
            catch (Exception ex)
            {

                LogException(ex.ToString());
            }
        }

        public virtual void LogSpecialServices(string name, IOwinRequest request)
        {
            try
            {
                if (!Config.EnableActionLog || !EnableLog) return;
                ServiceName = name;

                ServiceUrl = request.Path.Value;
            }
            catch (Exception ex)
            {

                LogException(ex.ToString());
            }
        }
        public virtual void Log(IActionLog action)
        {
            try
            {
                if (!Config.EnableActionLog || !EnableLog) return;

                action.DateTime = DateTime.UtcNow;
                action.IsDebugMode = Settings.IsDebugMode;
                action.IsMobileMode = Settings.IsMobileMode;
                action.LocalDateTime = LanguageManager.ToLocalDateTime(DateTime.UtcNow);
                action.User = CurrentUserManager.UserName;
                AddedLogDictionary.TryAdd(Guid.NewGuid().ToString(), action);
            }
            catch (Exception ex)
            {

                LogException(ex.ToString());
            }
        }

        public virtual bool Delete(JObject data)
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

        protected static void LogException(string exception)
        {
            var path = HostingEnvironment.MapPath(Config.LogPath + LanguageManager.ToLocalDateTime(DateTime.UtcNow)
                                                      .Substring(0, 20)
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
