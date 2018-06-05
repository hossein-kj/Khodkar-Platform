using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Model.Base;

namespace KS.Model.ContentManagement
{
    public sealed class File : BaseEntity, IKeyValues<EntityMasterDataKeyValue>
    {
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        [StringLength(128)]
        public string Description { get; set; }

        public byte[] Content { get; set; }

        public int TypeCode { get; set; }
        public float Size { get; set; }

        [StringLength(1024)]
        public string Url { get; set; }

        //public int? ThumbnailId { get; set; }

        //[ForeignKey("ThumbnailId")]
        //public FilePath Thumbnail { get; set; }

        //public bool Public { get; set; }
        public Guid Guid { get; set; }
        public ICollection<LocalFile> LocalFiles { get; set; }

        //[ForeignKey("FileId")]
        //public List<EntityMasterDataKeyValue> KeyValues { get; set; }

        [ForeignKey("FileId")]
        public List<EntityMasterDataKeyValue> KeyValues { get; set; }
    }
}
