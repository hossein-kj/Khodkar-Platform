using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.ObjectiveModel.Base;

namespace KS.ObjectiveModel.ContentManagement
{
    public class UserProfileObjective : BaseEntityWithoutAutoIdentityObjective,IKeyValuesObjective<EntityMasterDataKeyValueObjective>
    {
        [StringLength(256)]
        public string AliasName { get; set; }

        [ForeignKey("UserId")]
        public List<EntityMasterDataKeyValueObjective> KeyValues { get; set; }
    }
}
