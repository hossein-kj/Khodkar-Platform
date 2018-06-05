
using System.ComponentModel.DataAnnotations.Schema;

namespace KS.Model.Security
{
    public class ApplicationGroupRole
    {
        public int GroupId { get; set; }

        public int RoleId { get; set; }

        [ForeignKey("GroupId")]
        public ApplicationGroup Group { get; set; }

        [ForeignKey("RoleId")]
        public ApplicationRole Role { get; set; }

    }
}
