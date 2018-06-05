using KS.Core.GlobalVarioable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.ObjectiveModel.ContentManagement
{
    public sealed class CommentGroup : EntityGroupObjective
    {
        public new static int GetSelfEntityId()
        {
            return (int)EntityIdentity.CommentGroup;
        }
    }
}
