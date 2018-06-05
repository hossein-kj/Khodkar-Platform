using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.ObjectiveModel.Base;

namespace KS.ObjectiveModel.ContentManagement
{
    public class EntityFilePathObjective : BaseEntityRelation
    {
        //public int EntityTypeId { get; set; }
        //[ForeignKey("MasterDataKeyValueId")]
        public int FilePathId { get; set; }

        //public int EntityId { get; set; }

        //is defaults for content
        public bool IsDefaults { get; set; }

        public FilePathObjective FilePath { get; set; }

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
