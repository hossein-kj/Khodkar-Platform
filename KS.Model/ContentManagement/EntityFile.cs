
using KS.Model.Base;

namespace KS.Model.ContentManagement
{
    public class EntityFile : BaseEntityRelation
    {
       
        //[ForeignKey("MasterDataKeyValueId")]
        public int FileId { get; set; }

        //public int EntityId { get; set; }

        //is defaults for content
        public bool IsDefaults { get; set; }

        public File File { get; set; }

        public int EntityTypeId { get; set; }
        public int? DynamicEntityTypeId { get; set; }
        public MasterDataKeyValue DynamicEntityType { get; set; }

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
