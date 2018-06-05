using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Core.GlobalVarioable;

namespace KS.ObjectiveModel.ContentManagement
{
    public sealed class LinkTag : EntityMasterDataKeyValueObjective
    {
        public new static int GetSelfEntityId()
        {
            return (int)EntityIdentity.LinkTag;
        }

        //[ForeignKey("MasterDataKeyValueId")]
        //public Service Service { get; set; }
    }
}
