using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KS.Core.CodeManager.BrowsersCode.Base;
using KS.Core.Exceptions;
using KS.Core.FileSystemProvide.Base;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;
using KS.Core.Model.Core;
using KS.Core.Model.Develop;
using KS.Core.Model.FileSystem;
using KS.Core.Model.Log;
using KS.Core.Security;
using LiteDB;

namespace KS.Core.CodeManager.Base
{
    public abstract  class BaseSourceControl 
    {
        protected static readonly object Lock = new object();
        protected readonly string DependencyEngineDisPath = Config.ScriptDistPath + "asGetDependentModules.js";
        protected  readonly string DependencyEngineDebugPath = Config.ScriptDebugPath + "asGetDependentModules.js";
        protected readonly IFileSystemManager FileSystemManager;
        protected readonly ICodeTemplate CodeTemplate;
        protected readonly IZipManager ZipManager;

        protected BaseSourceControl(IFileSystemManager fileSystemManager,
            ICodeTemplate codeTemplate
            , IZipManager zipManager)
        {
            FileSystemManager = fileSystemManager;
            CodeTemplate = codeTemplate;
            ZipManager = zipManager;
        }
        public virtual void CheckCodeCheckOute(ILogEntity entitty)
        {
            if (entitty.ModifieUserId != CurrentUserManager.Id)
            {


                var modifyUser = AuthorizeManager.GetUserInfoById(entitty.ModifieUserId ?? 0);
                if (modifyUser != null)
                    throw new CheckOutRecordException(modifyUser.Key);
                else
                    throw new CheckOutRecordException("XXXXXX");
            }
        }

        

        public virtual async Task<string> GetJavascriptOfDebugPath(string path)
        {
            return await FileSystemManager.ReadAsync(
                AuthorizeManager.AuthorizeDebugJavascriptPath(path).Replace("//", "/"));
        }

        

        public virtual async Task<bool> AddOrUpdateDependencyEngineAsync(BundleDependency bundleDependency)
        {
            var engine = await ProperJavaScriptDependencyEngine(bundleDependency);
            await WriteJavaScriptDependencyEngine(Config.BrowserCodeDependencyEngineSourcePath, engine);
            await WriteJavaScriptDependencyEngine(DependencyEngineDebugPath, engine);
            if (bundleDependency.IsPublish)
                await WriteJavaScriptDependencyEngine(DependencyEngineDisPath, engine);
            return true;
        }

        protected virtual async Task<string> ProperJavaScriptDependencyEngine(BundleDependency bundleDependency)
        {
            var engine = await FileSystemManager.ReadAsync(Config.BrowserCodeDependencyEngineSourcePath);
            var startToken = CodeTemplate.DependencyKeyStart.Replace(CodeTemplate.DependencyKey,
                   bundleDependency.DependencyKey);
            var startIndex = engine.IndexOf(startToken, StringComparison.Ordinal);

            var endToken = CodeTemplate.DependencyKeyEnd.Replace(CodeTemplate.DependencyKey,
                       bundleDependency.DependencyKey);
            var endIndex = engine.IndexOf(endToken, StringComparison.Ordinal);
            var dependencyArray = "";
            var dependencyKeyArray = "";
            if (!bundleDependency.IsDelete)
            {
                foreach (var keyValue in bundleDependency.Dependency)
                {
                    if (keyValue.Key == null)
                    {
                        dependencyArray += "{ url: '" + keyValue.Value.Replace("~/", "") + "?minversion=" + bundleDependency.Version + "' },";
                    }
                    else
                    {
                        var depFunc = keyValue.Value.Replace("~/", "");
                        if (dependencyKeyArray != "")
                            dependencyKeyArray += ",";
                        dependencyKeyArray += depFunc.Remove(depFunc.IndexOf(".", StringComparison.Ordinal)) + "()";
                    }
                }

                dependencyArray += "{ url:'" + bundleDependency.Path.Replace("~/", "") + "?minversion=" + bundleDependency.Version + "' }";
            }
            if(startIndex > -1)
            engine = engine.Remove(startIndex + startToken.Length, endIndex - (startIndex + startToken.Length));
            if (bundleDependency.IsDelete)
            {
                return engine.Replace(startToken + endToken, "");
            }
            var dependency = CodeTemplate.DependencyTemplate.Replace(CodeTemplate.DependencyKey,
                    bundleDependency.DependencyKey)
                .Replace(CodeTemplate.DependencyKeyStart, startToken)
                .Replace(CodeTemplate.DependencyKeyEnd, endToken).
                Replace(CodeTemplate.DependencyArray, dependencyArray).
                Replace(CodeTemplate.DependencyKeyArray, dependencyKeyArray);
            engine = startIndex > -1 ? engine.Replace(startToken + endToken, dependency):
                engine.Replace(CodeTemplate.NewModule, dependency + " " + System.Environment.NewLine + CodeTemplate.NewModule);
            return engine;
        }

        protected virtual async Task<bool> WriteJavaScriptDependencyEngine(string path, string engine)
        {
            return await FileSystemManager.WriteAsync(path, engine);
        }

        
        protected virtual KeyValue CreateSourceControlDataBase(string codePath)
        {
            var codePathSegment = codePath.Split('/');
            var dbName = codePathSegment[codePathSegment.Length - 2];
            //  var dbName = codeName.Remove(codeName.IndexOf(".", StringComparison.Ordinal));
            //var dbPath = "";
            //for (var i = 0; i < codePathSegment.Length - 1; i++)
            //    dbPath += codePathSegment[i] ;

            //AuthorizeManager.AuthorizePath(codePath, ActionKey.WriteToDisk);
            using (var db = new LiteDatabase((FileSystemManager.CreatDirectoryIfNotExist(codePath) + "/" + dbName + ".db").Replace("//", "/")))
            {
                var logs = db.GetCollection<SourceControlChange>(dbName);
                logs.EnsureIndex(lg => lg.Name);
                logs.EnsureIndex(lg => lg.Comment);
                logs.EnsureIndex(lg => lg.Version);
                logs.EnsureIndex(lg => lg.LocalDateTime);
            }

            return new KeyValue() {Key = dbName, Value = codePath };
        }

        public virtual  bool RecycleBin(string codePath,string codeName,string zipName = null,bool codeNameIsFolder = true)
        {
            if (zipName == null)
                zipName = codeName;
            if (codePath[0] != '~' && codePath[0] == '/')
                codePath = "~" + codePath;
            else if (codePath[0] != '~' && codePath[0] != '/')
                codePath = "~/" + codePath;

            var disPath = FileSystemManager.RelativeToAbsolutePath((codePath.Replace("~", Config.SourceCodeDeletedPath)
                + "/" + zipName.Replace(".", "-") + "-" + LanguageManager.ToLocalDateTime(DateTime.UtcNow).Replace(" ", "-").Replace(":", "_").Replace("/", "-") + ".zip").Replace("//","/"));
            if (disPath == null)
                throw new KhodkarInvalidException(string.Format(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound),
              codePath.Replace("~", Config.SourceCodeDeletedPath)
                + zipName + "-" + LanguageManager.ToLocalDateTime(DateTime.UtcNow).Replace(" ", "-").Replace(":", "_").Replace("/", "-") + ".zip"));

            var sourcePath = FileSystemManager.RelativeToAbsolutePath(codePath);


            var path = disPath.Remove(disPath.ToLower()
                     .LastIndexOf("\\", StringComparison.Ordinal));

            FileSystemManager.CreatDirectoryIfNotExist(path);

            return codeNameIsFolder
                ? ZipManager.Zip(new ZipOprationInfo()
                {
                    Folders = new List<string> {codeName},
                    DestinationPath = disPath,
                    IsNew = true,
                    SourcePath = sourcePath
                })
                : ZipManager.Zip(new ZipOprationInfo()
                {
                    Files = new List<string> {codeName},
                    DestinationPath = disPath,
                    IsNew = true,
                    SourcePath = sourcePath
                });
        }

        public virtual async Task AddChange(string codePath,string codeName, string newCode, int version, string comment)
        {
            var dbInfo = CreateSourceControlDataBase(codePath);


            var dateTime = DateTime.UtcNow;

            var oldCode = "";
            try
            {
                oldCode = await FileSystemManager.ReadAsync((codePath + "/" + codeName).Replace("//","/"));
            }
            catch (Exception)
            {
                // ignored when new code create and old code not exist.
            }

            if (oldCode != newCode && newCode != "")
            {
                InsertChange(new SourceControlChange()
                {
                    Name = codeName,
                    DateTime = dateTime,
                    LocalDateTime = LanguageManager.ToLocalDateTime(dateTime),
                    Code = newCode,
                    Comment = comment,
                    User = CurrentUserManager.UserName,
                    Version = version
                }, dbInfo);
            }
        }

        protected virtual void InsertChange(SourceControlChange change,KeyValue databaseInfo)
        {
            lock (Lock)
            {
                using (
                    var db =
                        new LiteDatabase((FileSystemManager.CreatDirectoryIfNotExist(databaseInfo.Value) + "/" +
                                         databaseInfo.Key + ".db").Replace("//", "/")))
                {

                    var sourceControl = db.GetCollection<SourceControlChange>(databaseInfo.Key);

                    sourceControl.Insert(change);
                }
            }
        }

        public virtual List<SourceControlChange> GeChangesByPagination(
              string orderBy, int skip, int take
            , string codePath
            , string codeName
            , string comment
            ,string user
            , string fromDateTime
            , string toDateTime
            , out int count)
        {
            var databaseInfo = CreateSourceControlDataBase(codePath);
 
            lock (Lock)
            {
                var orders = orderBy.Split(' ');
                Query query = Query.EQ("Name", codeName);
                if (user.Trim() != "!")
                {
                    var query2 = Query.Contains("User", user);
                    query = query == null ? query2 : Query.And(query2, query);
                }
                if (comment.Trim() != "!")
                {
                    var query2 = Query.Contains("Comment", comment);
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

                using (var db = new LiteDatabase((FileSystemManager.CreatDirectoryIfNotExist(databaseInfo.Value) + "/" +
                                         databaseInfo.Key + ".db").Replace("//", "/")))
                {
                    count = db.GetCollection<SourceControlChange>(databaseInfo.Key).Count(query);
                    return
                        db.GetCollection<SourceControlChange>(databaseInfo.Key)
                            .Find(query, skip: skip, limit: take)
                            .ToList();
                }
            }

        }

        public virtual SourceControlChange GeChangeById(int changeId, string codePath)
        {
            var databaseInfo = CreateSourceControlDataBase(codePath);

            lock (Lock)
            {

                using (var db = new LiteDatabase((FileSystemManager.CreatDirectoryIfNotExist(databaseInfo.Value) + "/" +
                                         databaseInfo.Key + ".db").Replace("//", "/")))
                {
                    return
                        db.GetCollection<SourceControlChange>(databaseInfo.Key)
                            .FindById(changeId);
                }
            }

        }

        public virtual SourceControlChange GeChangeByNameAndVersion(int version, string codePath,string name)
        {
            var databaseInfo = CreateSourceControlDataBase(codePath);

            lock (Lock)
            {

                using (var db = new LiteDatabase((FileSystemManager.CreatDirectoryIfNotExist(databaseInfo.Value) + "/" +
                                         databaseInfo.Key + ".db").Replace("//", "/")))
                {
                    return
                        db.GetCollection<SourceControlChange>(databaseInfo.Key).FindOne(sc => sc.Version == version
                        && sc.Name== name);
                }
            }

        }
    }
}
