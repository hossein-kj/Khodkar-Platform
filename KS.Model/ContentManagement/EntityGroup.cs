
using System.ComponentModel.DataAnnotations.Schema;
using KS.Model.Base;

namespace KS.Model.ContentManagement
{
    public class EntityGroup : BaseEntityRelation
    {
       
        //[ForeignKey("MasterDataKeyValueId")]
        public int GroupId { get; set; }

        //public int EntityId { get; set; }

        public MasterDataKeyValue Group { get; set; }

        public int EntityTypeId { get; set; }

        public int? LinkId { get; set; }

        [ForeignKey("LinkId")]
        public Link Link { get; set; }

        public int? CommentId { get; set; }

        [ForeignKey("CommentId")]
        public Comment Comment { get; set; }

        public int? MasterDataKeyValueId { get; set; }
        public MasterDataKeyValue MasterDataKeyValue { get; set; }



        public static int GetSelfEntityId()
        {
            return 0;
        }
    }
}
