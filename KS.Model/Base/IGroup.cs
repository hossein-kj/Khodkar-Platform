using System.Collections.Generic;
using KS.Model.ContentManagement;

namespace KS.Model.Base
{  
    public interface IGroup<T> where T:EntityGroup
    {
        List<T> Groups { get; set; }
    }
}