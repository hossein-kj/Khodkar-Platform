
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KS.ObjectiveModel.Base;

namespace KS.ObjectiveModel.Security
{
    public sealed class ApplicationLocalRole : BaseEntityWithoutUserProfile
    {
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public ApplicationRole Role { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }
    }
}
