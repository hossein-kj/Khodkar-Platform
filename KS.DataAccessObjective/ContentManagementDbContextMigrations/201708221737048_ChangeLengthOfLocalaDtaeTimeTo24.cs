namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeLengthOfLocalaDtaeTimeTo24 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("ContentManagement.Comments", "CreateLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.Comments", "ModifieLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.Comments", "AccessLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.Users", "CreateLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.Users", "ModifieLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.Users", "AccessLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.DynamicEntities", "CreateLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.DynamicEntities", "ModifieLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.DynamicEntities", "AccessLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.MasterDataKeyValues", "CreateLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.MasterDataKeyValues", "ModifieLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.MasterDataKeyValues", "AccessLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.MasterDataLocalKeyValues", "CreateLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.MasterDataLocalKeyValues", "ModifieLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.MasterDataLocalKeyValues", "AccessLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.Links", "CreateLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.Links", "ModifieLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.Links", "AccessLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.FilePaths", "CreateLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.FilePaths", "ModifieLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.FilePaths", "AccessLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.LocalFilePaths", "CreateLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.LocalFilePaths", "ModifieLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.LocalFilePaths", "AccessLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.Files", "CreateLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.Files", "ModifieLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.Files", "AccessLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.LocalFiles", "CreateLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.LocalFiles", "ModifieLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.LocalFiles", "AccessLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.WebPages", "CreateLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.WebPages", "ModifieLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.WebPages", "AccessLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.LanguageAndCultures", "CreateLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.LanguageAndCultures", "ModifieLocalDateTime", c => c.String(maxLength: 24));
            AlterColumn("ContentManagement.LanguageAndCultures", "AccessLocalDateTime", c => c.String(maxLength: 24));
        }
        
        public override void Down()
        {
            AlterColumn("ContentManagement.LanguageAndCultures", "AccessLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.LanguageAndCultures", "ModifieLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.LanguageAndCultures", "CreateLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.WebPages", "AccessLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.WebPages", "ModifieLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.WebPages", "CreateLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.LocalFiles", "AccessLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.LocalFiles", "ModifieLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.LocalFiles", "CreateLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.Files", "AccessLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.Files", "ModifieLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.Files", "CreateLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.LocalFilePaths", "AccessLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.LocalFilePaths", "ModifieLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.LocalFilePaths", "CreateLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.FilePaths", "AccessLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.FilePaths", "ModifieLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.FilePaths", "CreateLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.Links", "AccessLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.Links", "ModifieLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.Links", "CreateLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.MasterDataLocalKeyValues", "AccessLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.MasterDataLocalKeyValues", "ModifieLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.MasterDataLocalKeyValues", "CreateLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.MasterDataKeyValues", "AccessLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.MasterDataKeyValues", "ModifieLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.MasterDataKeyValues", "CreateLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.DynamicEntities", "AccessLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.DynamicEntities", "ModifieLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.DynamicEntities", "CreateLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.Users", "AccessLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.Users", "ModifieLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.Users", "CreateLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.Comments", "AccessLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.Comments", "ModifieLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("ContentManagement.Comments", "CreateLocalDateTime", c => c.String(maxLength: 22));
        }
    }
}
