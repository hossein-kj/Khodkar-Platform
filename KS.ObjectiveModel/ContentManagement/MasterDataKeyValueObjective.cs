
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KS.ObjectiveModel.Base;

namespace KS.ObjectiveModel.ContentManagement
{
    public class MasterDataKeyValueObjective : BaseEntityWithoutAutoIdentityObjective, ITree<MasterDataKeyValueObjective>, IGroupObjective<MasterDataKeyValueGroup>
    {
        public int? ParentId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(512)]
        public string Code { get; set; }

        [StringLength(512)]
        public string SecondCode { get; set; }

        [StringLength(512)]
        public string Data { get; set; }

        public int? Key { get; set; }
        public int? Value { get; set; }

        //[Required]
        //public int TypeId { get; set; }

        public int? ParentTypeId { get; set; }

        [StringLength(1024)]
        public string PathOrUrl { get; set; }

        [StringLength(1024)]
        public string SecondPathOrUrl { get; set; }

        //[StringLength(1024)]
        //public string ForeignUrl { get; set; }
        // public int? DisplayOrder { get; set; }
        //public int? RoleId { get; set; }
        public bool IsLeaf { get; set; }
        public bool IsType { get; set; }
        public int? Order { get; set; }

        public int? ForeignKey1 { get; set; }
        public int? ForeignKey2 { get; set; }
        public int? ForeignKey3 { get; set; }
        public MasterDataKeyValueObjective Parent { get; set; }
        public bool EnableCache { get; set; }
        public int SlidingExpirationTimeInMinutes { get; set; }

        [StringLength(256)]
        public string Description { get; set; }
        [StringLength(32)]
        [Index(IsUnique = true)]
        public string Guid { get; set; }
        public int Version { get; set; }
        public bool EditMode { get; set; }
        public ICollection<MasterDataKeyValueObjective> Childrens { get; set; }
        public ICollection<MasterDataLocalKeyValueObjective> LocalValues { get; set; }

        public static int GetSelfEntityId()
        {
            return 0;
        }

        //public new static string GetSelfEntityTableName()
        //{
        //    return EntityIdentity.MasterDataKeyValueTable;
        //}

        //[ForeignKey("GroupId")]
        //public List<MasterDataKeyValueGroup> Groups { get; set; }

        [ForeignKey("GroupId")]
        public List<MasterDataKeyValueGroup> Groups { get; set; }
    }
}
