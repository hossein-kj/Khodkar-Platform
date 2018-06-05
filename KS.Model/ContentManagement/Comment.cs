
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KS.Core.Model.ContentManagement;
using KS.Model.Base;

namespace KS.Model.ContentManagement
{
    public sealed class Comment : BaseEntity,ITree<Comment>,IGroup<EntityGroup>
    {
        public int? ParentId { get; set; }
        public Comment Parent { get; set; }
        public bool IsLeaf { get; set; }
        public int? Order { get; set; }

        [EmailAddress]
        [StringLength(32)]
        public string Email { get; set; }

        public int WebPageId { get; set; }
        public WebPage WebPage { get; set; }

        [StringLength(32)]
        public string Name { get; set; }

        [StringLength(2048)]
        public string Content { get; set; }

        public bool Public { get; set; }

        public int Like { get; set; }
        public int DisLike { get; set; }

        [ForeignKey("ParentId")]
        public ICollection<Comment> Childrens { get; set; }

        [ForeignKey("CommentId")]
        public List<EntityGroup> Groups { get; set; }
    }
}
