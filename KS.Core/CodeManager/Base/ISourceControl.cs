using System.Collections.Generic;
using System.Threading.Tasks;
using KS.Core.Model.Develop;
using KS.Core.Model.Log;

namespace KS.Core.CodeManager.Base
{
    public interface ISourceControl
    {
        Task AddChange(string codePath, string codeName, string newCode, int version, string comment);
        Task<bool> AddOrUpdateDependencyEngineAsync(BundleDependency bundleDependency);
        void CheckCodeCheckOute(ILogEntity entitty);
        List<SourceControlChange> GeChangesByPagination(
            string orderBy, int skip, int take
            , string codePath
            , string codeName         
            , string comment
            ,string user
            ,string fromDateTime
            , string toDateTime
            , out int count);

        SourceControlChange GeChangeById(int changeId, string codePath);
        SourceControlChange GeChangeByNameAndVersion(int version, string codePath, string name);
        Task<string> GetJavascriptOfDebugPath(string path);
        bool RecycleBin(string codePath, string codeName, string zipName = null, bool codeNameIsFolder = true);
    }
}