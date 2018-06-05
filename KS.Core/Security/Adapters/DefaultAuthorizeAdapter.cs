
using KS.Core.Data.Contexts.Base;

namespace KS.Core.Security.Adapters
{
    public sealed class DefaultAuthorizeAdapter:BaseAuthorizeAdapter,IDefaultAuthorizeAdapter,IAuthorizeAdapter
    {
        public DefaultAuthorizeAdapter(IDataBaseContextManager dataBaseContextManager):base(dataBaseContextManager)
        {

        }
    }
}
