using System.Collections.Generic;
using KS.ObjectiveModel.ContentManagement;

namespace KS.ObjectiveModel.Base
{
    public interface ITagObjective<T> where T:EntityMasterDataKeyValueObjective
    {
        List<T> Tags { get; set; }
    }
}