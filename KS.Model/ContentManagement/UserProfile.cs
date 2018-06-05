
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KS.Model.Base;

namespace KS.Model.ContentManagement
{
    public class UserProfile: BaseEntityWithoutAutoIdentity, IKeyValues<EntityMasterDataKeyValue>
    {
        [StringLength(256)]
        public string AliasName { get; set; }

        [ForeignKey("UserId")]
        public List<EntityMasterDataKeyValue> KeyValues { get; set; }
    }
}
