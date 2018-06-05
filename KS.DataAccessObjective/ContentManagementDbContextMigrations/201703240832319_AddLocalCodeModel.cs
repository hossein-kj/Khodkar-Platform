namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLocalCodeModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ContentManagement.LocalCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodeId = c.Int(nullable: false),
                        Name = c.String(maxLength: 128),
                        Description = c.String(maxLength: 256),
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
                .ForeignKey("ContentManagement.Codes", t => t.CodeId, cascadeDelete: true)
                .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
                .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
                .Index(t => t.CodeId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifieUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("ContentManagement.LocalCodes", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LocalCodes", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LocalCodes", "CodeId", "ContentManagement.Codes");
            DropIndex("ContentManagement.LocalCodes", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.LocalCodes", new[] { "CreateUserId" });
            DropIndex("ContentManagement.LocalCodes", new[] { "CodeId" });
            DropTable("ContentManagement.LocalCodes");
        }
    }
}
