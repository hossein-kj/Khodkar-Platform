using System.Collections.Generic;
using KS.ObjectiveModel.ContentManagement;

namespace KS.ObjectiveModel.Base
{
    public interface IKeyValuesObjective<T> where T:EntityMasterDataKeyValueObjective
    {
        List<T> KeyValues { get; set; }
    }
}