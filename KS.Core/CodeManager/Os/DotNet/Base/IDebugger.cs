using KS.Core.Model.Develop;
using System.Collections.Generic;

namespace KS.Core.CodeManager.Os.DotNet.Base
{
    public interface IDebugger
    {
        void DeleteDebugDataBase(string dbPath);
        string SerializeObjectToJobjectString(object data);

        DebugInfo AddOrUpdateDebugInfo(DebugInfo debugInfo, string dbPath);

        void DeleteDebugInfo(List<int> debugInfoIds, string dbPath, int dllVersion);

        DebugInfo GetDebugInfo(int debugInfoId, string dbPath);

        List<DebugInfo> GetDebugInfosByPagination(
        string orderBy, int skip, int take
        , string dbPath
        , int codeId
        , int dllVersion
        , string data
        , int? integerValue
        , decimal? decimalValue
        , string fromDateTime
        , string toDateTime
        , out int count);

        List<DebugInfo> GetDebugInfos(string dbPath, int codeId);
    }
}
