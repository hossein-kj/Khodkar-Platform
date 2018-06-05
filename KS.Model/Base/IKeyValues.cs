using System.Collections.Generic;
using KS.Model.ContentManagement;

namespace KS.Model.Base
{
    public interface IKeyValues<T> where T:EntityMasterDataKeyValue
    {
        List<T> KeyValues { get; set; }
    }
}