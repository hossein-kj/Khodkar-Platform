using System;
using System.Collections.Generic;

namespace KS.Core.Model.ContentManagement
{
    public interface ITree<T>
    {
        int Id { get; set; }
        int? ParentId { get; set; }
        T Parent { get; set; }
        Boolean IsLeaf { get; set; }
        int? Order { get; set; }
        ICollection<T> Childrens { get; set; }
        int Status { get; set; }
    }
}
