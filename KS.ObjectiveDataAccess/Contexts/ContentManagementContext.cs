
using System.Data.Entity;
using KS.Core.Log.Elmah.Base;
using KS.ObjectiveDataAccess.Config;
using KS.ObjectiveDataAccess.Contexts.Config;
using KS.ObjectiveModel.ContentManagement;
using KS.Core.Log.Base;
using KS.ObjectiveDataAccess.Contexts.Base;

namespace KS.ObjectiveDataAccess.Contexts
{
    //For Oracle Migration
    [DbConfigurationType(typeof(OracleContentManagementModelConfiguration))]
    public sealed class ContentManagementContext : BaseContext<ContentManagementContext>, IContentManagementContext
    {
        public ContentManagementContext(IErrorLogManager errorLogManager
            ,IActionLogManager actionLogManager):base(errorLogManager, actionLogManager)
        {
            
        }

        public ContentManagementContext()
        {

        }


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

        public System.Data.Entity.DbSet<KS.ObjectiveModel.ContentManagement.MasterDataLocalKeyValueObjective> MasterDataLocalKeyValues { get; set; }
    }
}
