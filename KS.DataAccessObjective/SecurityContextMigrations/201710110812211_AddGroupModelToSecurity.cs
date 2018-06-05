namespace KS.DataAccess.SecurityContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupModelToSecurity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Security.AspNetGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ParentId = c.Int(),
                        IsLeaf = c.Boolean(nullable: false),
                        Order = c.Int(),
                        Status = c.Int(nullable: false),
                        CreateUserId = c.Int(),
                        ModifieUserId = c.Int(),
                        CreateLocalDateTime = c.String(),
                        ModifieLocalDateTime = c.String(),
                        AccessLocalDateTime = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                        ModifieDateTime = c.DateTime(nullable: false),
                        AccessDateTime = c.DateTime(nullable: false),
                        ViewRoleId = c.Int(),
                        ModifyRoleId = c.Int(),
                        AccessRoleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Security.AspNetGroups", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "Security.AspNetGroupRoles",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.GroupId })
                .ForeignKey("Security.AspNetGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("Security.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "Security.AspNetUserGroups",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.GroupId })
                .ForeignKey("Security.AspNetGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("Security.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Security.AspNetGroups", "ParentId", "Security.AspNetGroups");
            DropForeignKey("Security.AspNetGroupRoles", "RoleId", "Security.AspNetRoles");
            DropForeignKey("Security.AspNetUserGroups", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserGroups", "GroupId", "Security.AspNetGroups");
            DropForeignKey("Security.AspNetGroupRoles", "GroupId", "Security.AspNetGroups");
            DropIndex("Security.AspNetUserGroups", new[] { "GroupId" });
            DropIndex("Security.AspNetUserGroups", new[] { "UserId" });
            DropIndex("Security.AspNetGroupRoles", new[] { "GroupId" });
            DropIndex("Security.AspNetGroupRoles", new[] { "RoleId" });
            DropIndex("Security.AspNetGroups", new[] { "ParentId" });
            DropTable("Security.AspNetUserGroups");
            DropTable("Security.AspNetGroupRoles");
            DropTable("Security.AspNetGroups");
        }
    }
}
