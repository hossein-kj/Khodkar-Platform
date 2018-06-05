namespace KS.DataAccess.SecurityContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstTime : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Security.AspNetLocalRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleGroupId = c.Int(nullable: false),
                        Name = c.String(maxLength: 256),
                        Language = c.String(maxLength: 8),
                        CreateUserId = c.Int(),
                        ModifieUserId = c.Int(),
                        CreateLocalDateTime = c.String(maxLength: 22),
                        ModifieLocalDateTime = c.String(maxLength: 22),
                        AccessLocalDateTime = c.String(maxLength: 22),
                        CreateDateTime = c.DateTime(nullable: false),
                        ModifieDateTime = c.DateTime(nullable: false),
                        AccessDateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Security.AspNetRoles", t => t.RoleGroupId, cascadeDelete: true)
                .Index(t => t.RoleGroupId);
            
            CreateTable(
                "Security.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        RoleId = c.Int(),
                        IsFree = c.Boolean(nullable: false),
                        ParentId = c.Int(),
                        IsLeaf = c.Boolean(nullable: false),
                        IsGroup = c.Boolean(nullable: false),
                        Order = c.Int(),
                        Status = c.Int(nullable: false),
                        EnableCache = c.Boolean(nullable: false),
                        SlidingExpirationTimeInMinutes = c.Int(nullable: false),
                        CreateUserId = c.Int(),
                        ModifieUserId = c.Int(),
                        ViewCount = c.Int(nullable: false),
                        ViewRoleId = c.Int(),
                        ModifyRoleId = c.Int(),
                        AccessRoleId = c.Int(),
                        CreateLocalDateTime = c.String(maxLength: 22),
                        ModifieLocalDateTime = c.String(maxLength: 22),
                        AccessLocalDateTime = c.String(maxLength: 22),
                        CreateDateTime = c.DateTime(nullable: false),
                        ModifieDateTime = c.DateTime(nullable: false),
                        AccessDateTime = c.DateTime(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Security.AspNetRoles", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "Security.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("Security.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("Security.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "Security.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 32),
                        LastName = c.String(maxLength: 64),
                        NationalCode = c.String(maxLength: 10),
                        IdentityNumber = c.String(maxLength: 16),
                        Serial = c.String(maxLength: 16),
                        PostalCode = c.String(maxLength: 16),
                        HomeAddress = c.String(maxLength: 512),
                        WorkAddress = c.String(maxLength: 512),
                        Job = c.String(maxLength: 64),
                        LocalBirthDate = c.String(maxLength: 22),
                        BirthDate = c.DateTime(nullable: false),
                        FatherName = c.String(maxLength: 32),
                        HomeTell1 = c.String(maxLength: 16),
                        HomeTell2 = c.String(maxLength: 16),
                        Mobile = c.String(maxLength: 16),
                        IsMale = c.Boolean(nullable: false),
                        IsMarried = c.Boolean(),
                        Children = c.Int(),
                        Online = c.Boolean(nullable: false),
                        UserProfileId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreateUserId = c.Int(),
                        ModifieUserId = c.Int(),
                        ViewCount = c.Int(nullable: false),
                        ViewRoleId = c.Int(),
                        ModifyRoleId = c.Int(),
                        AccessRoleId = c.Int(),
                        CreateLocalDateTime = c.String(maxLength: 22),
                        ModifieLocalDateTime = c.String(maxLength: 22),
                        AccessLocalDateTime = c.String(maxLength: 22),
                        CreateDateTime = c.DateTime(nullable: false),
                        ModifieDateTime = c.DateTime(nullable: false),
                        AccessDateTime = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserProfileId, unique: true)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "Security.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Security.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "Security.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("Security.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "Security.ApplicationQueryAuthrizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(),
                        RoleId = c.Int(),
                        Entity = c.String(),
                        ResourceTypeId = c.Int(nullable: false),
                        Resource = c.String(),
                        Grant = c.Boolean(nullable: false),
                        ViewRoleId = c.Int(),
                        ModifyRoleId = c.Int(),
                        AccessRoleId = c.Int(),
                        Language = c.String(maxLength: 8),
                        CreateUserId = c.Int(),
                        ModifieUserId = c.Int(),
                        CreateLocalDateTime = c.String(maxLength: 22),
                        ModifieLocalDateTime = c.String(maxLength: 22),
                        AccessLocalDateTime = c.String(maxLength: 22),
                        CreateDateTime = c.DateTime(nullable: false),
                        ModifieDateTime = c.DateTime(nullable: false),
                        AccessDateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Security.AspNetUserRoles", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserLogins", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserClaims", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserRoles", "RoleId", "Security.AspNetRoles");
            DropForeignKey("Security.AspNetLocalRole", "RoleGroupId", "Security.AspNetRoles");
            DropForeignKey("Security.AspNetRoles", "ParentId", "Security.AspNetRoles");
            DropIndex("Security.AspNetUserLogins", new[] { "UserId" });
            DropIndex("Security.AspNetUserClaims", new[] { "UserId" });
            DropIndex("Security.AspNetUsers", "UserNameIndex");
            DropIndex("Security.AspNetUsers", new[] { "UserProfileId" });
            DropIndex("Security.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("Security.AspNetUserRoles", new[] { "UserId" });
            DropIndex("Security.AspNetRoles", "RoleNameIndex");
            DropIndex("Security.AspNetRoles", new[] { "ParentId" });
            DropIndex("Security.AspNetLocalRole", new[] { "RoleGroupId" });
            DropTable("Security.ApplicationQueryAuthrizes");
            DropTable("Security.AspNetUserLogins");
            DropTable("Security.AspNetUserClaims");
            DropTable("Security.AspNetUsers");
            DropTable("Security.AspNetUserRoles");
            DropTable("Security.AspNetRoles");
            DropTable("Security.AspNetLocalRole");
        }
    }
}
