
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KS.Model.Base;

namespace KS.Model.ContentManagement
{
    public sealed class WebPage : BaseEntity
    {
        public WebPage()
        {
            //KeyValues = new List<EntityMasterDataKeyValue>();
        }
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(1024)]
        public string Url { get; set; }

        public int? CategoryId { get; set; }

        [StringLength(1024)]
        public string TemplatePatternUrl { get; set; }
        public int? FrameWorkId { get; set; }

        [StringLength(1024)]
        public string FrameWorkUrl { get; set; }
        public bool CommentOff { get; set; }
        public string Html { get; set; }
        public bool HaveScript { get; set; }
        public bool HaveStyle { get; set; }
        [StringLength(255)]
        public string Tools { get; set; }

        [StringLength(2048)]
        public string DependentModules { get; set; }
        public string Services { get; set; }

        [StringLength(1024)]
        public string Params { get; set; }
        public bool EditMode { get; set; }
        public bool EnableCache { get; set; }
        public bool IsMobileVersion { get; set; }
        public int TypeId { get; set; }
        public int Like { get; set; }
        public int DisLike { get; set; }
        public int CacheSlidingExpirationTimeInMinutes { get; set; }

        [StringLength(32)]
        [Index(IsUnique = true)]
        public string Guid { get; set; }
        public int Version { get; set; }

        //[ForeignKey("WebPageId")]
        //public List<EntityMasterDataKeyValue> KeyValues { get; set; }

        [ForeignKey("WebPageId")]
        public List<EntityFile> Files { get; set; }

        [ForeignKey("WebPageId")]
        public List<EntityFilePath> FilePaths { get; set; }
    }
}
