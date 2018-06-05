using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Core.GlobalVarioable;

namespace KS.ObjectiveModel.ContentManagement
{
    public sealed class MasterDataKeyValueGroup : EntityGroupObjective
    {
        public new static int GetSelfEntityId()
        {
            return (int)EntityIdentity.MasterDataKeyValueGroup;
        }
    }
}
