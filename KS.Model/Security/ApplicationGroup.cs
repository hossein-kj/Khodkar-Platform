using KS.Core.Model;
using KS.Core.Security;
using KS.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KS.Core.Model.ContentManagement;
using KS.Core.Model.Log;

namespace KS.Model.Security
{
    public class ApplicationGroup: ITree<ApplicationGroup>, ILogEntity, IAccessRole
    {
        public ApplicationGroup()
        {
            this.Childrens = new List<ApplicationGroup>();

            this.ApplicationRoles = new List<ApplicationGroupRole>();

            this.ApplicationUsers = new List<ApplicationUserGroup>();
            this.LocalGroups = new List<ApplicationLocalGroup>();

        }

        public ApplicationGroup(string name) : this()
        {
            this.Name = name;
        }

        public ApplicationGroup(string name, string description) : this(name)
        {
            this.Description = description;
        }

        public int Id { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public virtual ICollection<ApplicationGroupRole> ApplicationRoles { get; set; }

        public virtual ICollection<ApplicationUserGroup> ApplicationUsers { get; set; }

        public int? ParentId
        {
            get;

            set;
        }

        public ApplicationGroup Parent
        {
            get;

            set;
        }

        public bool IsLeaf
        {
            get;

            set;
        }

        public int? Order
        {
            get;

            set;
        }

        public ICollection<ApplicationGroup> Childrens
        {
            get;

            set;
        }

        public int Status
        {
            get;

            set;
        }

        public int? CreateUserId
        {
            get;

            set;
        }

        public int? ModifieUserId
        {
            get;

            set;
        }
        [StringLength(19)]
        public string CreateLocalDateTime
        {
            get;

            set;
        }
        [StringLength(19)]
        public string ModifieLocalDateTime
        {
            get;

            set;
        }
        [StringLength(19)]
        public string AccessLocalDateTime
        {
            get;

            set;
        }

        public DateTime CreateDateTime
        {
            get;

            set;
        }

        public DateTime ModifieDateTime
        {
            get;

            set;
        }

        public DateTime AccessDateTime
        {
            get;

            set;
        }

        public int? ViewRoleId
        {
            get;

            set;
        }

        public int? ModifyRoleId
        {
            get;

            set;
        }

        public int? AccessRoleId
        {
            get;

            set;
        }

        [ForeignKey("GroupId")]
        public ICollection<ApplicationLocalGroup> LocalGroups { get; set; }
    }
}
