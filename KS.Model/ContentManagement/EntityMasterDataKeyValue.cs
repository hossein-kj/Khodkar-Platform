using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using KS.Model.Base;

namespace KS.Model.ContentManagement
{
    public class EntityMasterDataKeyValue : BaseEntityRelation
    {
        public int MasterDataKeyValueId { get; set; }

        public int EntityTypeId { get; set; }

        //public MasterDataKeyValueObjective MasterDataKeyValue { get; set; }
        [ForeignKey("MasterDataKeyValueId")]
        public MasterDataKeyValue MasterDataKeyValue { get; set; }

        //public int? DynamicContentEntityTypeId { get; set; }
        //public MasterDataKeyValue DynamicContentEntityType { get; set; }

        //public int? GroupId { get; set; }

        //public int EntityId { get; set; }


        //[ForeignKey("GroupId")]
        //public MasterDataKeyValue Group { get; set; }

        public int? LinkId { get; set; }

        [ForeignKey("LinkId")]
        public Link Link { get; set; }

        //public int? WebPageId { get; set; }
        //public WebPage WebPage { get; set; }

        public int? FileId { get; set; }

        [ForeignKey("FileId")]
        public File File { get; set; }

        public int? FilePathId { get; set; }

        [ForeignKey("FilePathId")]
        public FilePath FilePath { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public UserProfile User { get; set; }

        public static int GetSelfEntityId()
        {
            return 0;
        }
    }
}
