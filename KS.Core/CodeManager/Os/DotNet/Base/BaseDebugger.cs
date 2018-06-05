using KS.Core.FileSystemProvide.Base;
using KS.Core.Localization;
using KS.Core.Model.Develop;
using LiteDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using KS.Core.Exceptions;
using KS.Core.GlobalVarioable;

namespace KS.Core.CodeManager.Os.DotNet.Base
{
    public abstract class BaseDebugger
    {
        protected static readonly object Lock = new object();
        protected readonly IFileSystemManager FileSystemManager;
        private const string DebugDb = "DebugDb.db";
        private const string DebugCollection = "DebugDb";

        protected BaseDebugger(IFileSystemManager fileSystemManager)
        {
            FileSystemManager = fileSystemManager;
        }

        protected virtual void CreateDebugDataBase(string dbPath)
        {
            if (!FileSystemManager.FileExist((dbPath + "/" + DebugDb).Replace("//", "/")))
            {
                using (var db = new LiteDatabase((FileSystemManager
                 .CreatDirectoryIfNotExist(dbPath) + "/" + DebugDb).Replace("//", "/")))
                {
                    var debugDb = db.GetCollection<DebugInfo>(DebugCollection);
                    debugDb.EnsureIndex(dg => dg.CodeId);
                    //debugDb.EnsureIndex(dg => dg.Data);
                    debugDb.EnsureIndex(dg => dg.IsOk);
                    debugDb.EnsureIndex(dg => dg.IntegerValue);
                    debugDb.EnsureIndex(dg => dg.DecimalValue);
                    debugDb.EnsureIndex(dg => dg.DateTime);
                    debugDb.EnsureIndex(dg => dg.CreateDateTime);
                    debugDb.EnsureIndex(dg => dg.DllVersion);
                    debugDb.EnsureIndex(dg => dg.IsAppDebugInfo);
                }
            }
        }

        public virtual void DeleteDebugDataBase(string dbPath)
        {
            if (!FileSystemManager.FileExist((dbPath + "/" + DebugDb).Replace("//", "/")))
            {
                FileSystemManager.DeleteFile((dbPath + "/" + DebugDb).Replace("//", "/"));
            }
        }

        public string SerializeObjectToJobjectString(object data)
        {

            return JsonConvert.SerializeObject
            (data, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }
        public virtual DebugInfo AddOrUpdateDebugInfo(DebugInfo debugInfo, string dbPath)
        {
            var dateTime = DateTime.UtcNow;

            if (debugInfo.Id == 0)
                debugInfo.CreateDateTime = LanguageManager.ToLocalDateTime(dateTime);

            dbPath = (FileSystemManager
                 .CreatDirectoryIfNotExist(dbPath) + "/" + DebugDb).Replace("//", "/");
            lock (Lock)
            {


                using (var db = new LiteDatabase(dbPath))
                {

                    var sourceControl = db.GetCollection<DebugInfo>(DebugCollection);
                    if (debugInfo.Id == 0)
                        sourceControl.Insert(debugInfo);
                    else
                    {
                        var info = db.GetCollection<DebugInfo>(DebugCollection).FindOne(dg => dg.Id == debugInfo.Id);

                        if (info.IsAppDebugInfo)
                            throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidAccessToUpdateCodeMadeDebugInfo));


                        sourceControl.Update(debugInfo);
                    }
                }
            }

            return debugInfo;
        }

        public virtual void DeleteDebugInfo(List<int> debugInfoIds, string dbPath, int dllVersion)
        {
            dbPath = (FileSystemManager
                          .CreatDirectoryIfNotExist(dbPath) + "/" + DebugDb).Replace("//", "/");
            lock (Lock)
            {
                using (var db = new LiteDatabase(dbPath))
                {
                    foreach (var id in debugInfoIds)
                    {
                        db.GetCollection<DebugInfo>(DebugCollection).Delete(dg => dg.Id == id);
                    }


                }
            }
        }

        public virtual DebugInfo GetDebugInfo(int debugInfoId, string dbPath)
        {
            lock (Lock)
            {
                using (var db = new LiteDatabase((FileSystemManager
                 .CreatDirectoryIfNotExist(dbPath) + "/" + DebugDb).Replace("//", "/")))
                {
                    return db.GetCollection<DebugInfo>(DebugCollection).FindOne(dg => dg.Id == debugInfoId);
                }
            }
        }

        public List<DebugInfo> GetDebugInfosByPagination(
        string orderBy, int skip, int take
        , string dbPath
        , int codeId
        , int dllVersion
        , string data
        , int? integerValue
        , decimal? decimalValue
        , string fromDateTime
        , string toDateTime
        , out int count)
        {

            CreateDebugDataBase(dbPath);

            lock (Lock)
            {
                var orders = orderBy.Split(' ');
                Query query = Query.EQ("CodeId", codeId);
                if (data.Trim() != "!")
                {
                    var query2 = Query.Contains("Data", data);
                    query = query == null ? query2 : Query.And(query2, query);
                }
                if (integerValue != null)
                {
                    var query2 = Query.EQ("IntegerValue", integerValue);
                    query = query == null ? query2 : Query.And(query2, query);
                }
                if (decimalValue != null)
                {
                    var query2 = Query.EQ("DecimalValue", decimalValue);
                    query = query == null ? query2 : Query.And(query2, query);
                }
                if (fromDateTime.Trim() != "!")
                {
                    query = query == null ? Query.GTE("CreateDateTime", fromDateTime.Trim()) :
                        Query.And(Query.GTE("CreateDateTime", fromDateTime.Trim()), query);
                }

                if (toDateTime.Trim() != "!")
                {
                    query = query == null ? Query.LTE("CreateDateTime", toDateTime.Trim()) :
                        Query.And(Query.LTE("CreateDateTime", toDateTime.Trim()), query);
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

                using (var db = new LiteDatabase((FileSystemManager
                 .CreatDirectoryIfNotExist(dbPath) + "/" + DebugDb).Replace("//", "/")))
                {
                    count = db.GetCollection<DebugInfo>(DebugCollection).Count(query);
                    return
                        db.GetCollection<DebugInfo>(DebugCollection)
                            .Find(query, skip: skip, limit: take)
                            .ToList();
                }
            }
        }

        public List<DebugInfo> GetDebugInfos(string dbPath, int codeId)
        {

            CreateDebugDataBase(dbPath);

            lock (Lock)
            {

                Query query = Query.EQ("CodeId", codeId);


                using (var db = new LiteDatabase((FileSystemManager
                 .CreatDirectoryIfNotExist(dbPath) + "/" + DebugDb).Replace("//", "/")))
                {
                    return
                        db.GetCollection<DebugInfo>(DebugCollection)
                            .Find(query)
                            .ToList();
                }
            }
        }
    }
}
