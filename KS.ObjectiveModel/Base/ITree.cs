using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.ObjectiveModel.Base
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
