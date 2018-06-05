using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KS.ObjectiveModel.Base;
using KS.Core.Model;
using KS.Core.Model.Log;

namespace KS.ObjectiveModel.ContentManagement
{
    public class LinkObjective : BaseEntityObjective, ITree<LinkObjective>, ILogEntity,ITagObjective<LinkTag>,IGroupObjective<LinkGroup>
    {
         public LinkObjective()
        {

            this.Childrens = new List<LinkObjective>();

        }

        public LinkObjective(string text, string url, int order)
            : this()
        {
            this.Text = text;
            this.Url = url;
            this.Order = order;
        }

        public LinkObjective(string text, string url, string iconPath, int order, int parentId)
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
        public virtual LinkObjective Parent { get; set; }
        public virtual ICollection<LinkObjective> Childrens { get; set; }
        public bool ShowToSearchEngine { get; set; }

        //[ForeignKey("LinkId")]
        //public List<LinkKeyValue> KeyValues { get; set; }

        [ForeignKey("LinkId")]
        public List<LinkFile> Files { get; set; }

        [ForeignKey("LinkId")]
        public List<LinkFilePath> FilePaths { get; set; }
        public new static string GetSelfEntityTableName()
        {
            return "ContentManageMent.Links";
        }

        //public virtual ICollection<ApplicationTreeNode> Ancestors { get; set; }
        //public virtual ICollection<ApplicationTreeNode> Offspring { get; set; }

        [ForeignKey("LinkId")]
        public List<LinkTag> Tags { get; set; }

        //[ForeignKey("LinkId")]
        //public List<LinkGroup> Groups { get; set; }

        [ForeignKey("LinkId")]
        public List<LinkGroup> Groups { get; set; }
    }
}
