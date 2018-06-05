using System.ComponentModel.DataAnnotations.Schema;
using KS.ObjectiveModel.Base;

namespace KS.ObjectiveModel.ContentManagement
{
    public class EntityMasterDataKeyValueObjective : BaseEntityRelation
    {

        public int MasterDataKeyValueId { get; set; }

        //public int EntityId { get; set; }


        [ForeignKey("MasterDataKeyValueId")]
        public MasterDataKeyValueObjective MasterDataKeyValue { get; set; }

        //public int? GroupId { get; set; }

        //public int EntityId { get; set; }


        //[ForeignKey("GroupId")]
        //public Group Group { get; set; }

        //public int? DynamicContentEntityTypeId { get; set; }
        //public MasterDataKeyValueType DynamicContentEntityType { get; set; }

        public int? LinkId { get; set; }

        [ForeignKey("LinkId")]
        public LinkObjective Link { get; set; }

        //public int? WebPageId { get; set; }

        //[ForeignKey("WebPageId")]
        //public WebPageObjective WebPage { get; set; }

        public int? FileId { get; set; }

        [ForeignKey("FileId")]
        public FileObjective File { get; set; }

        public int? FilePathId { get; set; }

        [ForeignKey("FilePathId")]
        public FilePathObjective FilePath { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public UserProfileObjective User { get; set; }

        public static int GetSelfEntityId()
        {
            return 0;
        }
    }
}
