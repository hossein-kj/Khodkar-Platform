using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Core.DependencyProvider.Adapters
{
    public interface  IDependencyAdapter
    {
        T Get<T>();
    }
}
