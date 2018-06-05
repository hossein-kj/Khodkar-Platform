
using System.Data.Entity;
using KS.Core.Log.Elmah.Base;
using KS.DataAccess.Config;
using KS.DataAccess.Contexts.Config;
using KS.Model.ContentManagement;
using KS.Core.Log.Base;
using KS.DataAccess.Contexts.Base;

namespace KS.DataAccess.Contexts
{
    //For Oracle Migration
    [DbConfigurationType(typeof(OracleModelConfiguration))]
    public sealed class ContentManagementContext : BaseContext<ContentManagementContext>, IContentManagementContext
    {
        public ContentManagementContext(IErrorLogManager errorLogManager
            ,IActionLogManager actionLogManager):base(errorLogManager, actionLogManager)
        {
            
        }

        //public ContentManagementContext()
        //{

        //}

        public DbSet<DynamicEntityObjective> DynamicEntities { get; set; }
        public DbSet<WebPageObjective> WebPages { get; set; }
        public DbSet<CommentObjective> Comments { get; set; }
        //public DbSet<HtmlTemplate> HtmlTemplates { get; set; }

        //public DbSet<WebPageService> WebPageServices { get; set; }
        public DbSet<EntityMasterDataKeyValueObjective> EntityMasterDataKeyValues { get; set; }
        public DbSet<MasterDataKeyValueObjective> MasterDataKeyValues { get; set; }
        public DbSet<LanguageAndCultureObjective> LanguageAndCultures { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Path> Paths { get; set; }
        public DbSet<MasterDataKeyValueType> MasterDataKeyValueTypes { get; set; }
        public DbSet<LinkObjective> Links { get; set; }
        public DbSet<FileObjective> Files { get; set; }
        public DbSet<FilePathObjective> FilePaths { get; set; }
        public DbSet<LocalFilePathObjective> LocalFilePaths { get; set; }
        public DbSet<LocalFileObjective> LocalFiles { get; set; }
        public DbSet<UserProfileObjective> Users { get; set; }
        public DbSet<EntityGroupObjective> EntityGroups { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           SqlContentManagementObjectiveModelConfiguration.Config(modelBuilder);
        }

        public System.Data.Entity.DbSet<KS.Model.ContentManagement.MasterDataLocalKeyValueObjective> MasterDataLocalKeyValues { get; set; }
    }
}
