namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEditModeToMasterDataKeyValueModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("ContentManagement.Codes", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LocalCodes", "CodeId", "ContentManagement.Codes");
            DropForeignKey("ContentManagement.Codes", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Codes", "TypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.LocalCodes", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LocalCodes", "ModifieUserId", "ContentManagement.Users");
            DropIndex("ContentManagement.LocalCodes", new[] { "CodeId" });
            DropIndex("ContentManagement.LocalCodes", new[] { "CreateUserId" });
            DropIndex("ContentManagement.LocalCodes", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.Codes", new[] { "TypeId" });
            DropIndex("ContentManagement.Codes", new[] { "Guid" });
            DropIndex("ContentManagement.Codes", new[] { "CreateUserId" });
            DropIndex("ContentManagement.Codes", new[] { "ModifieUserId" });
            AddColumn("ContentManagement.MasterDataKeyValues", "EditMode", c => c.Boolean(nullable: false));
            DropTable("ContentManagement.LocalCodes");
            DropTable("ContentManagement.Codes");
        }
        
        public override void Down()
        {
            CreateTable(
                "ContentManagement.Codes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        TypeId = c.Int(nullable: false),
                        Description = c.String(maxLength: 256),
                        Guid = c.String(maxLength: 32),
                        Version = c.Int(nullable: false),
                        Language = c.String(maxLength: 8),
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
                        Status = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            DropColumn("ContentManagement.MasterDataKeyValues", "EditMode");
            CreateIndex("ContentManagement.Codes", "ModifieUserId");
            CreateIndex("ContentManagement.Codes", "CreateUserId");
            CreateIndex("ContentManagement.Codes", "Guid", unique: true);
            CreateIndex("ContentManagement.Codes", "TypeId");
            CreateIndex("ContentManagement.LocalCodes", "ModifieUserId");
            CreateIndex("ContentManagement.LocalCodes", "CreateUserId");
            CreateIndex("ContentManagement.LocalCodes", "CodeId");
            AddForeignKey("ContentManagement.LocalCodes", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.LocalCodes", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.Codes", "TypeId", "ContentManagement.MasterDataKeyValues", "Id", cascadeDelete: true);
            AddForeignKey("ContentManagement.Codes", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.LocalCodes", "CodeId", "ContentManagement.Codes", "Id", cascadeDelete: true);
            AddForeignKey("ContentManagement.Codes", "CreateUserId", "ContentManagement.Users", "Id");
        }
    }
}
