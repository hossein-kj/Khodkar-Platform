
using KS.Model.Base;

namespace KS.Model.ContentManagement
{
    public class EntityFilePath : BaseEntityRelation
    {
        public int EntityTypeId { get; set; }
        //[ForeignKey("MasterDataKeyValueId")]

        public int? DynamicEntityTypeId { get; set; }
        public MasterDataKeyValue DynamicEntityType { get; set; }

        public int FilePathId { get; set; }

        //public int EntityId { get; set; }

        //is defaults for content
        public bool IsDefaults { get; set; }

        public FilePath FilePath { get; set; }
        public MasterDataKeyValue EntityType { get; set; }

        public int? LinkId { get; set; }
        public Link Link { get; set; }

        public int? WebPageId { get; set; }
        public WebPage WebPage { get; set; }

        public static int GetSelfEntityId()
        {
            return 0;
        }
    }
}
