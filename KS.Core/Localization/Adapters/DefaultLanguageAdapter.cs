
using KS.Core.Data.Contexts.Base;

namespace KS.Core.Localization.Adapters
{
    public sealed class DefaultLanguageAdapter : BaseLanguageAdapter, ILanguageAdapter
    {
        public DefaultLanguageAdapter(IDataBaseContextManager dataBaseContextManager)
            :base(dataBaseContextManager)
        {

        }
    }
}
