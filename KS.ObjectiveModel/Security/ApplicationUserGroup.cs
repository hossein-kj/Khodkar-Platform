using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.ObjectiveModel.Security
{
    public class ApplicationUserGroup
    {
     
        public int UserId { get; set; }

      
        public int GroupId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [ForeignKey("GroupId")]
        public ApplicationGroup Group{ get; set; }

    }
}
