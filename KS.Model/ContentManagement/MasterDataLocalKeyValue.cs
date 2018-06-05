using System.ComponentModel.DataAnnotations;
using KS.Model.Base;

namespace KS.Model.ContentManagement
{
    public sealed class MasterDataLocalKeyValue : BaseEntityWithoutRoles
    {
        public int MasterDataKeyValueId { get; set; }

        public MasterDataKeyValue MasterDataKeyValue { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }
    }
}
