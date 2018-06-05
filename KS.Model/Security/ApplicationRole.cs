using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KS.Core.Security;
using KS.Model.Base;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using KS.Core.Model;
using KS.Core.Model.ContentManagement;
using KS.Core.Model.Log;

namespace KS.Model.Security
{
    public class ApplicationRole : IdentityRole<int, ApplicationUserRole>, IRole<int>, ITree<ApplicationRole>, ILogEntity,IAccessRole
    {
        [StringLength(256)]
        public string Description { get; set; }

        public ApplicationRole()
        {
            this.Childrens = new List<ApplicationRole>();
            this.LocalRoles = new List<ApplicationLocalRole>();
        }

        public ApplicationRole(string name)
            : this()
        {
            this.Name = name;
            this.Childrens = new List<ApplicationRole>();
        }

        public ApplicationRole(string name, string description)
            : this(name)
        {
            this.Description = description;
        }
        public virtual ApplicationRole Parent { get; set; }
        //public int? Value { get; set; }
       // public int? RoleId { get; set; }
        public bool IsFree { get; set; }
        public int? ParentId { get; set; }
        public bool IsLeaf { get; set; }
        public int? Order { get; set; }
        public int Status { get; set; }
        public virtual ICollection<ApplicationRole> Childrens { get; set; }
        public static string GetSelfEntityTableName()
        {

            return "[Security].[AspNetRoles]";
        }

        //public bool EnableCache { get; set; }
        //public int SlidingExpirationTimeInMinutes { get; set; }

        [ForeignKey("RoleId")]
        public ICollection<ApplicationLocalRole> LocalRoles { get; set; }
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
    }

  


}
