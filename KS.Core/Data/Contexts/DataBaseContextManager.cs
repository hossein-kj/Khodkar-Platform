
using KS.Core.CodeManager.BrowsersCode.Base;
using KS.Core.Data.Contexts.Base;
using KS.Core.Data.Contexts.SqlServer.Base;

namespace KS.Core.Data.Contexts
{
    public sealed class DataBaseContextManager : BaseDataBaseContextManager, IDataBaseContextManager
    {
        public DataBaseContextManager(ISqlHelper sqlHelper, ICodeTemplate codeTemplate) : base(sqlHelper, codeTemplate)
        {

        }
    }
}
