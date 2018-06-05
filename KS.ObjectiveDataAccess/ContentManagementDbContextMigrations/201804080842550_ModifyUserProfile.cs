namespace KS.ObjectiveDataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyUserProfile : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("ContentManagement.Users", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Files", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LocalFiles", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LocalFiles", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Files", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.FilePaths", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LocalFilePaths", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LocalFilePaths", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.FilePaths", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Links", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.MasterDataKeyValues", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.MasterDataLocalKeyValues", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.MasterDataLocalKeyValues", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.MasterDataKeyValues", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.WebPages", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.WebPages", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Links", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.EntityMasterDataKeyValues", "UserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Users", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Comments", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Comments", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LanguageAndCultures", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LanguageAndCultures", "ModifieUserId", "ContentManagement.Users");
            DropPrimaryKey("ContentManagement.Users");
            AlterColumn("ContentManagement.Users", "Id", c => c.Int(nullable: false, identity: false));
            AddPrimaryKey("ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.Users", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.Files", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.LocalFiles", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.LocalFiles", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.Files", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.FilePaths", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.LocalFilePaths", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.LocalFilePaths", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.FilePaths", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.Links", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.MasterDataKeyValues", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.MasterDataLocalKeyValues", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.MasterDataLocalKeyValues", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.MasterDataKeyValues", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.WebPages", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.WebPages", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.Links", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.EntityMasterDataKeyValues", "UserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.Users", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.Comments", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.Comments", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.LanguageAndCultures", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.LanguageAndCultures", "ModifieUserId", "ContentManagement.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("ContentManagement.LanguageAndCultures", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LanguageAndCultures", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Comments", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Comments", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Users", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.EntityMasterDataKeyValues", "UserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Links", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.WebPages", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.WebPages", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.MasterDataKeyValues", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.MasterDataLocalKeyValues", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.MasterDataLocalKeyValues", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.MasterDataKeyValues", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Links", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.FilePaths", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LocalFilePaths", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LocalFilePaths", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.FilePaths", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Files", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LocalFiles", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LocalFiles", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Files", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Users", "CreateUserId", "ContentManagement.Users");
            DropPrimaryKey("ContentManagement.Users");
            AlterColumn("ContentManagement.Users", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.LanguageAndCultures", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.LanguageAndCultures", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.Comments", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.Comments", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.Users", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.EntityMasterDataKeyValues", "UserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.Links", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.WebPages", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.WebPages", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.MasterDataKeyValues", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.MasterDataLocalKeyValues", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.MasterDataLocalKeyValues", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.MasterDataKeyValues", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.Links", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.FilePaths", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.LocalFilePaths", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.LocalFilePaths", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.FilePaths", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.Files", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.LocalFiles", "ModifieUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.LocalFiles", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.Files", "CreateUserId", "ContentManagement.Users", "Id");
            AddForeignKey("ContentManagement.Users", "CreateUserId", "ContentManagement.Users", "Id");
        }
    }
}
