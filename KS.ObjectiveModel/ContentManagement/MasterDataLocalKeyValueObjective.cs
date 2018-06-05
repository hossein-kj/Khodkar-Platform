using System.ComponentModel.DataAnnotations;
using KS.ObjectiveModel.Base;

namespace KS.ObjectiveModel.ContentManagement
{
    public sealed class MasterDataLocalKeyValueObjective : BaseEntityWithoutRolesObjective
    {
        public int MasterDataKeyValueId { get; set; }

        public MasterDataKeyValueObjective MasterDataKeyValue { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }
    }
}
