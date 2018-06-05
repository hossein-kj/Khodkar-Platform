using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Model.Base;
using KS.Core.Model;
using KS.Core.Model.ContentManagement;
using KS.Core.Model.Log;

namespace KS.Model.ContentManagement
{
    public class Link : BaseEntity, ITree<Link>, ILogEntity, IKeyValues<EntityMasterDataKeyValue>,IGroup<EntityGroup>
    {
         public Link()
        {

            this.Childrens = new List<Link>();

        }

        public Link(string text, string url, int order)
            : this()
        {
            this.Text = text;
            this.Url = url;
            this.Order = order;
        }

        public Link(string text, string url, string iconPath, int order, int parentId)
            : this(text, url, order)
        {
            this.ParentId = parentId;
            this.IconPath = iconPath;
        }

        [StringLength(255)]
        public string Text { get; set; }

        [StringLength(512)]
        public string Html { get; set; }

        [StringLength(255)]
        public string Action { get; set; }
        public int TypeId { get; set; }

        [StringLength(255)]
        public string IconPath { get; set; }

        public Boolean IsLeaf { get; set; }

        [StringLength(4)]
        public string TransactionCode { get; set; }

        [StringLength(1024)]
        public string Url { get; set; }
        public bool IsMobile { get; set; }
        public int? Order { get; set; }
        public int? ParentId { get; set; }
        //public int? RoleId { get; set; }
        public virtual Link Parent { get; set; }
        public virtual ICollection<Link> Childrens { get; set; }
        public bool ShowToSearchEngine { get; set; }

        //[ForeignKey("LinkId")]
        //public List<EntityMasterDataKeyValue> KeyValues { get; set; }

        [ForeignKey("LinkId")]
        public List<EntityFile> Files { get; set; }

        [ForeignKey("LinkId")]
        public List<EntityFilePath> FilePaths { get; set; }


        public new static string GetSelfEntityTableName()
        {
            return "ContentManageMent.Links";
        }

        //public virtual ICollection<ApplicationTreeNode> Ancestors { get; set; }
        //public virtual ICollection<ApplicationTreeNode> Offspring { get; set; }

        [ForeignKey("LinkId")]
        public List<EntityMasterDataKeyValue> KeyValues { get; set; }

        //[ForeignKey("LinkId")]
        //public List<EntityMasterDataKeyValue> Groups { get; set; }

        [ForeignKey("LinkId")]
        public List<EntityGroup> Groups { get; set; }
    }
}
