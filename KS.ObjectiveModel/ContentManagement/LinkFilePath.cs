using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Core.GlobalVarioable;

namespace KS.ObjectiveModel.ContentManagement
{
    public sealed class LinkFilePath : EntityFilePathObjective
    {
        public new static int GetSelfEntityId()
        {
            return (int)EntityIdentity.DynamicEntityFilePath;
        }
    }
}
