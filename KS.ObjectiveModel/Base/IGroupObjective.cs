using System.Collections.Generic;
using KS.ObjectiveModel.ContentManagement;

namespace KS.ObjectiveModel.Base
{  
    public interface IGroupObjective<T> where T:EntityGroupObjective
    {
        List<T> Groups { get; set; }
    }
}