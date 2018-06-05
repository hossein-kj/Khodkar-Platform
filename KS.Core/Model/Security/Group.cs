

using System.Collections.Generic;

namespace KS.Core.Model.Security
{
    public class Group
    {
        public Group()
        {
            RolesId=new List<int>();
        }
        public int Id { get; set; }
        public List<int> RolesId { get; set; }
    }
}
