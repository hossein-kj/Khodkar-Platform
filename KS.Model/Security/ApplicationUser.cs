using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using KS.Core.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using KS.Core.Model.Log;

namespace KS.Model.Security
{
    public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IUser<int>, ILogEntity,IAccessRole
    {
        public ApplicationUser()
        {
            Status = 1;
        }

        [StringLength(32)]
        public string FirstName { get; set; }

        [StringLength(64)]
        public string LastName { get; set; }

        [StringLength(10)]
        public string NationalCode { get; set; }

        [StringLength(16)]
        public string IdentityNumber { get; set; }

        [StringLength(16)]
        public string Serial { get; set; }

        [StringLength(16)]
        public string PostalCode { get; set; }

        [StringLength(512)]
        public string HomeAddress { get; set; }

        [StringLength(512)]
        public string WorkAddress { get; set; }

        [StringLength(64)]
        public string Job { get; set; }

        //[StringLength(64)]
        //public string EduCation { get; set; }

        [StringLength(19)]
        public string LocalBirthDate { get; set; }
        public DateTime BirthDate { get; set; }

        [StringLength(32)]
        public string FatherName { get; set; }

        [StringLength(16)]
        public string HomeTell1 { get; set; }

        [StringLength(16)]
        public string HomeTell2 { get; set; }

        [StringLength(16)]
        public string Mobile { get; set; }

        public bool IsMale { get; set; }

        public bool? IsMarried { get; set; }

        public int? Children { get; set; }

        public bool Online { get; set; }
        //public int Status { get; set; }
        //public int OwnerId { get; set; }
        //public string ShamsiDateTimeCreateDateTime { get; set; }
        //public string ShamsiLasteUpdateTime { get; set; }
        //public DateTime CreateDateTime { get; set; }
        //public DateTime LasteUpdateTime { get; set; }
        //public string ShamsiLasteActivityTime { get; set; }
        //public DateTime LasteActivityTime { get; set; }

        //[Index(IsUnique = true)]
        //public int UserProfileId { get; set; }
        public int Status { get; set; }
        public int? CreateUserId { get; set; }
        public int? ModifieUserId { get; set; }
        public int ViewCount { get; set; }
        public int? ViewRoleId { get; set; }
        public int? ModifyRoleId { get; set; }
        public int? AccessRoleId { get; set; }

         [StringLength(19)]
        public string CreateLocalDateTime { get; set; }

         [StringLength(19)]
        public string ModifieLocalDateTime { get; set; }

         [StringLength(19)]
        public string AccessLocalDateTime { get; set; }

        public DateTime CreateDateTime { get; set; }
        public DateTime ModifieDateTime { get; set; }
        public DateTime AccessDateTime { get; set; }

        public virtual ICollection<ApplicationUserGroup> Groups { get; set; }

         public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            var userIdentity = await manager
                .CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager, string authenticationType)
        {
            var userIdentity = await manager
                .CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }
}
