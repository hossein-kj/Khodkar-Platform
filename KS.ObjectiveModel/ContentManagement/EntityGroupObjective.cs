using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.ObjectiveModel.Base;

namespace KS.ObjectiveModel.ContentManagement
{
    public class EntityGroupObjective : BaseEntityRelation
    {
       
        //[ForeignKey("MasterDataKeyValueId")]
        public int GroupId { get; set; }

        //public int EntityId { get; set; }

        public Group Group { get; set; }

        //public int EntityTypeId { get; set; }
        public int? DynamicEntityTypeId { get; set; }
        public MasterDataKeyValueType DynamicEntityType { get; set; }

        public int? LinkId { get; set; }

        [ForeignKey("LinkId")]
        public LinkObjective Link { get; set; }

        public int? CommentId { get; set; }

        [ForeignKey("CommentId")]
        public CommentObjective Comment { get; set; }

        public int? DynamicEntityId { get; set; }

        public int? MasterDataKeyValueId { get; set; }
        public MasterDataKeyValueObjective MasterDataKeyValue { get; set; }



        public static int GetSelfEntityId()
        {
            return 0;
        }
    }
}
