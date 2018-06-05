using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Core.GlobalVarioable;

namespace KS.ObjectiveModel.ContentManagement
{
    public class MasterDataKeyValueType: MasterDataKeyValueObjective
    {
        public new static int GetSelfEntityId()
        {
            return (int)EntityIdentity.MasterDataKeyValueType;
        }
    }
}
