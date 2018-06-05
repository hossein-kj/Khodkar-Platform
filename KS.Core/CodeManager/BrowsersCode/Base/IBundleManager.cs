
using System.Collections.Generic;
using KS.Core.Model;
using KS.Core.Model.Develop;

namespace KS.Core.CodeManager.BrowsersCode.Base
{
    public interface IBundleManager
    {
        void AddBundle(List<BundleOption> bundleOptions);

        void RemoveBundle(string name);
    }
}
