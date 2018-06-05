using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Core.GlobalVarioable;
using KS.ObjectiveModel.Base;

namespace KS.ObjectiveModel.ContentManagement
{
    public class Group : MasterDataKeyValueObjective
    {
        public new static int GetSelfEntityId()
        {
            return (int)EntityIdentity.Group;
        }
    }
}
