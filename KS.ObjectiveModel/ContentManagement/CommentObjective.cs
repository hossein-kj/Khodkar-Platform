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
    public sealed class CommentObjective : BaseEntityObjective,ITree<CommentObjective>,IGroupObjective<CommentGroup>
    {
        public int? ParentId { get; set; }
        public CommentObjective Parent { get; set; }
        public bool IsLeaf { get; set; }
        public int? Order { get; set; }

        [EmailAddress]
        [StringLength(32)]
        public string Email { get; set; }

        public int WebPageId { get; set; }
        public WebPageObjective WebPage { get; set; }

        [StringLength(32)]
        public string Name { get; set; }

        [StringLength(2048)]
        public string Content { get; set; }

        public bool Public { get; set; }

        public int Like { get; set; }
        public int DisLike { get; set; }

        [ForeignKey("ParentId")]
        public ICollection<CommentObjective> Childrens { get; set; }

        [ForeignKey("CommentId")]
        public List<CommentGroup> Groups { get; set; }
    }
}
