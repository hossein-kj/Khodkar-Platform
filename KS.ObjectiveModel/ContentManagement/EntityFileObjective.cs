using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.ObjectiveModel.Base;

namespace KS.ObjectiveModel.ContentManagement
{
    public class EntityFileObjective : BaseEntityRelation
    {
        //public int EntityTypeId { get; set; }
        //[ForeignKey("MasterDataKeyValueId")]
        public int FileId { get; set; }

        //public int EntityId { get; set; }

        //is defaults for content
        public bool IsDefaults { get; set; }

        public FileObjective File { get; set; }

        public int? DynamicEntityTypeId { get; set; }
        public MasterDataKeyValueType DynamicEntityType { get; set; }

        public int? LinkId { get; set; }
        public LinkObjective Link { get; set; }

        public int? WebPageId { get; set; }
        public WebPageObjective WebPage { get; set; }

        public static int GetSelfEntityId()
        {
            return 0;
        }
    }
}
