namespace KS.ObjectiveDataAccess.SecurityContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstTime : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "Security.AspNetGroups",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(maxLength: 256),
            //            Description = c.String(maxLength: 256),
            //            ParentId = c.Int(),
            //            IsLeaf = c.Boolean(nullable: false),
            //            Order = c.Int(),
            //            Status = c.Int(nullable: false),
            //            CreateUserId = c.Int(),
            //            ModifieUserId = c.Int(),
            //            CreateLocalDateTime = c.String(maxLength: 19),
            //            ModifieLocalDateTime = c.String(maxLength: 19),
            //            AccessLocalDateTime = c.String(maxLength: 19),
            //            CreateDateTime = c.DateTime(nullable: false),
            //            ModifieDateTime = c.DateTime(nullable: false),
            //            AccessDateTime = c.DateTime(nullable: false),
            //            ViewRoleId = c.Int(),
            //            ModifyRoleId = c.Int(),
            //            AccessRoleId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("Security.AspNetGroups", t => t.ParentId)
            //    .Index(t => t.ParentId);
            
            //CreateTable(
            //    "Security.AspNetGroupRoles",
            //    c => new
            //        {
            //            RoleId = c.Int(nullable: false),
            //            GroupId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.RoleId, t.GroupId })
            //    .ForeignKey("Security.AspNetGroups", t => t.GroupId, cascadeDelete: true)
            //    .ForeignKey("Security.AspNetRoles", t => t.RoleId, cascadeDelete: true)
            //    .Index(t => t.RoleId)
            //    .Index(t => t.GroupId);
            
            //CreateTable(
            //    "Security.AspNetRoles",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Description = c.String(maxLength: 256),
            //            IsFree = c.Boolean(nullable: false),
            //            ParentId = c.Int(),
            //            IsLeaf = c.Boolean(nullable: false),
            //            Order = c.Int(),
            //            Status = c.Int(nullable: false),
            //            CreateUserId = c.Int(),
            //            ModifieUserId = c.Int(),
            //            ViewCount = c.Int(nullable: false),
            //            ViewRoleId = c.Int(),
            //            ModifyRoleId = c.Int(),
            //            AccessRoleId = c.Int(),
            //            CreateLocalDateTime = c.String(maxLength: 19),
            //            ModifieLocalDateTime = c.String(maxLength: 19),
            //            AccessLocalDateTime = c.String(maxLength: 19),
            //            CreateDateTime = c.DateTime(nullable: false),
            //            ModifieDateTime = c.DateTime(nullable: false),
            //            AccessDateTime = c.DateTime(nullable: false),
            //            Name = c.String(nullable: false, maxLength: 256),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("Security.AspNetRoles", t => t.ParentId)
            //    .Index(t => t.ParentId)
            //    .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            //CreateTable(
            //    "Security.AspNetLocalRole",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            RoleId = c.Int(nullable: false),
            //            Name = c.String(maxLength: 256),
            //            Description = c.String(maxLength: 256),
            //            Language = c.String(maxLength: 8),
            //            CreateUserId = c.Int(),
            //            ModifieUserId = c.Int(),
            //            CreateLocalDateTime = c.String(maxLength: 19),
            //            ModifieLocalDateTime = c.String(maxLength: 19),
            //            AccessLocalDateTime = c.String(maxLength: 19),
            //            CreateDateTime = c.DateTime(nullable: false),
            //            ModifieDateTime = c.DateTime(nullable: false),
            //            AccessDateTime = c.DateTime(nullable: false),
            //            Status = c.Int(nullable: false),
            //            RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("Security.AspNetRoles", t => t.RoleId, cascadeDelete: true)
            //    .Index(t => t.RoleId);
            
            //CreateTable(
            //    "Security.AspNetUserRoles",
            //    c => new
            //        {
            //            UserId = c.Int(nullable: false),
            //            RoleId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.UserId, t.RoleId })
            //    .ForeignKey("Security.AspNetRoles", t => t.RoleId, cascadeDelete: true)
            //    .ForeignKey("Security.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.RoleId);
            
            //CreateTable(
            //    "Security.AspNetUsers",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            FirstName = c.String(maxLength: 32),
            //            LastName = c.String(maxLength: 64),
            //            NationalCode = c.String(maxLength: 10),
            //            IdentityNumber = c.String(maxLength: 16),
            //            Serial = c.String(maxLength: 16),
            //            PostalCode = c.String(maxLength: 16),
            //            HomeAddress = c.String(maxLength: 512),
            //            WorkAddress = c.String(maxLength: 512),
            //            Job = c.String(maxLength: 64),
            //            LocalBirthDate = c.String(maxLength: 19),
            //            BirthDate = c.DateTime(nullable: false),
            //            FatherName = c.String(maxLength: 32),
            //            HomeTell1 = c.String(maxLength: 16),
            //            HomeTell2 = c.String(maxLength: 16),
            //            Mobile = c.String(maxLength: 16),
            //            IsMale = c.Boolean(nullable: false),
            //            IsMarried = c.Boolean(),
            //            Children = c.Int(),
            //            Online = c.Boolean(nullable: false),
            //            UserProfileId = c.Int(nullable: false),
            //            Status = c.Int(nullable: false),
            //            CreateUserId = c.Int(),
            //            ModifieUserId = c.Int(),
            //            ViewCount = c.Int(nullable: false),
            //            ViewRoleId = c.Int(),
            //            ModifyRoleId = c.Int(),
            //            AccessRoleId = c.Int(),
            //            CreateLocalDateTime = c.String(maxLength: 19),
            //            ModifieLocalDateTime = c.String(maxLength: 19),
            //            AccessLocalDateTime = c.String(maxLength: 19),
            //            CreateDateTime = c.DateTime(nullable: false),
            //            ModifieDateTime = c.DateTime(nullable: false),
            //            AccessDateTime = c.DateTime(nullable: false),
            //            Email = c.String(maxLength: 256),
            //            EmailConfirmed = c.Boolean(nullable: false),
            //            PasswordHash = c.String(),
            //            SecurityStamp = c.String(),
            //            PhoneNumber = c.String(),
            //            PhoneNumberConfirmed = c.Boolean(nullable: false),
            //            TwoFactorEnabled = c.Boolean(nullable: false),
            //            LockoutEndDateUtc = c.DateTime(),
            //            LockoutEnabled = c.Boolean(nullable: false),
            //            AccessFailedCount = c.Int(nullable: false),
            //            UserName = c.String(nullable: false, maxLength: 256),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .Index(t => t.UserProfileId, unique: true)
            //    .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            //CreateTable(
            //    "Security.AspNetUserClaims",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            UserId = c.Int(nullable: false),
            //            ClaimType = c.String(),
            //            ClaimValue = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("Security.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "Security.AspNetUserGroups",
            //    c => new
            //        {
            //            UserId = c.Int(nullable: false),
            //            GroupId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.UserId, t.GroupId })
            //    .ForeignKey("Security.AspNetGroups", t => t.GroupId, cascadeDelete: true)
            //    .ForeignKey("Security.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.GroupId);
            
            //CreateTable(
            //    "Security.AspNetUserLogins",
            //    c => new
            //        {
            //            LoginProvider = c.String(nullable: false, maxLength: 128),
            //            ProviderKey = c.String(nullable: false, maxLength: 128),
            //            UserId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
            //    .ForeignKey("Security.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "Security.AspNetLocalGroup",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            GroupId = c.Int(nullable: false),
            //            Name = c.String(maxLength: 256),
            //            Description = c.String(maxLength: 256),
            //            Language = c.String(maxLength: 8),
            //            CreateUserId = c.Int(),
            //            ModifieUserId = c.Int(),
            //            CreateLocalDateTime = c.String(maxLength: 19),
            //            ModifieLocalDateTime = c.String(maxLength: 19),
            //            AccessLocalDateTime = c.String(maxLength: 19),
            //            CreateDateTime = c.DateTime(nullable: false),
            //            ModifieDateTime = c.DateTime(nullable: false),
            //            AccessDateTime = c.DateTime(nullable: false),
            //            Status = c.Int(nullable: false),
            //            RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("Security.AspNetGroups", t => t.GroupId, cascadeDelete: true)
            //    .Index(t => t.GroupId);
            
            //CreateTable(
            //    "Security.ApplicationQueryAuthrizes",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            GroupId = c.Int(),
            //            RoleId = c.Int(),
            //            Entity = c.String(),
            //            ResourceTypeId = c.Int(nullable: false),
            //            Resource = c.String(),
            //            Grant = c.Boolean(nullable: false),
            //            ViewRoleId = c.Int(),
            //            ModifyRoleId = c.Int(),
            //            AccessRoleId = c.Int(),
            //            Language = c.String(maxLength: 8),
            //            CreateUserId = c.Int(),
            //            ModifieUserId = c.Int(),
            //            CreateLocalDateTime = c.String(maxLength: 19),
            //            ModifieLocalDateTime = c.String(maxLength: 19),
            //            AccessLocalDateTime = c.String(maxLength: 19),
            //            CreateDateTime = c.DateTime(nullable: false),
            //            ModifieDateTime = c.DateTime(nullable: false),
            //            AccessDateTime = c.DateTime(nullable: false),
            //            Status = c.Int(nullable: false),
            //            RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
            //        })
            //    .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Security.AspNetLocalGroup", "GroupId", "Security.AspNetGroups");
            DropForeignKey("Security.AspNetGroups", "ParentId", "Security.AspNetGroups");
            DropForeignKey("Security.AspNetGroupRoles", "RoleId", "Security.AspNetRoles");
            DropForeignKey("Security.AspNetUserRoles", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserLogins", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserGroups", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserGroups", "GroupId", "Security.AspNetGroups");
            DropForeignKey("Security.AspNetUserClaims", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserRoles", "RoleId", "Security.AspNetRoles");
            DropForeignKey("Security.AspNetLocalRole", "RoleId", "Security.AspNetRoles");
            DropForeignKey("Security.AspNetRoles", "ParentId", "Security.AspNetRoles");
            DropForeignKey("Security.AspNetGroupRoles", "GroupId", "Security.AspNetGroups");
            DropIndex("Security.AspNetLocalGroup", new[] { "GroupId" });
            DropIndex("Security.AspNetUserLogins", new[] { "UserId" });
            DropIndex("Security.AspNetUserGroups", new[] { "GroupId" });
            DropIndex("Security.AspNetUserGroups", new[] { "UserId" });
            DropIndex("Security.AspNetUserClaims", new[] { "UserId" });
            DropIndex("Security.AspNetUsers", "UserNameIndex");
            DropIndex("Security.AspNetUsers", new[] { "UserProfileId" });
            DropIndex("Security.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("Security.AspNetUserRoles", new[] { "UserId" });
            DropIndex("Security.AspNetLocalRole", new[] { "RoleId" });
            DropIndex("Security.AspNetRoles", "RoleNameIndex");
            DropIndex("Security.AspNetRoles", new[] { "ParentId" });
            DropIndex("Security.AspNetGroupRoles", new[] { "GroupId" });
            DropIndex("Security.AspNetGroupRoles", new[] { "RoleId" });
            DropIndex("Security.AspNetGroups", new[] { "ParentId" });
            DropTable("Security.ApplicationQueryAuthrizes");
            DropTable("Security.AspNetLocalGroup");
            DropTable("Security.AspNetUserLogins");
            DropTable("Security.AspNetUserGroups");
            DropTable("Security.AspNetUserClaims");
            DropTable("Security.AspNetUsers");
            DropTable("Security.AspNetUserRoles");
            DropTable("Security.AspNetLocalRole");
            DropTable("Security.AspNetRoles");
            DropTable("Security.AspNetGroupRoles");
            DropTable("Security.AspNetGroups");
        }
    }
}
