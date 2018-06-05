using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.ObjectiveModel.Base;

namespace KS.ObjectiveModel.ContentManagement
{
    public sealed class FilePathObjective : BaseEntityObjective,ITagObjective<FilePathTag>
    {
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(128)]
        public string Description { get; set; }

        public int TypeCode { get; set; }

        [StringLength(1024)]
        public string Url { get; set; }
        public float Size { get; set; }

        //public int? ThumbnailId { get; set; }

        //[ForeignKey("ThumbnailId")]
        //public FilePathObjective Thumbnail { get; set; }
        //public bool Public { get; set; }
        public Guid Guid { get; set; }
        public ICollection<LocalFilePathObjective> LocalFilePaths { get; set; }

        //[ForeignKey("FilePathId")]
        //public List<FilePathKeyValue> KeyValues { get; set; }

        [ForeignKey("FilePathId")]
        public List<FilePathTag> Tags { get; set; }
    }
}
