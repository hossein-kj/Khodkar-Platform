
using KS.Core.CodeManager.DataBase.Base;
using KS.Core.Data.Contexts.SqlServer.Base;

namespace KS.Core.CodeManager.DataBase
{
    public sealed class DataBaseManager :BaseDataBaseManager,IDataBaseManager
    {
        public DataBaseManager(ISqlHelper sqlHelper):base(sqlHelper)
        {

        }
    }
}
