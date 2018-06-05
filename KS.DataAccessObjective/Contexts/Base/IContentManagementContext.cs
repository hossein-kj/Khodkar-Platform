using System.Data.Entity;
using KS.Model.ContentManagement;
using System.Threading.Tasks;

namespace KS.DataAccess.Contexts.Base
{
    public interface IContentManagementContext
    {
        DbSet<CommentObjective> Comments { get; set; }
        DbSet<DynamicEntityObjective> DynamicEntities { get; set; }
        DbSet<EntityGroupObjective> EntityGroups { get; set; }
        DbSet<EntityMasterDataKeyValueObjective> EntityMasterDataKeyValues { get; set; }
        DbSet<FilePathObjective> FilePaths { get; set; }
        DbSet<FileObjective> Files { get; set; }
        DbSet<LanguageAndCultureObjective> LanguageAndCultures { get; set; }
        DbSet<LinkObjective> Links { get; set; }
        DbSet<LocalFilePathObjective> LocalFilePaths { get; set; }
        DbSet<LocalFileObjective> LocalFiles { get; set; }
        DbSet<MasterDataKeyValueObjective> MasterDataKeyValues { get; set; }
        DbSet<MasterDataKeyValueType> MasterDataKeyValueTypes { get; set; }
        DbSet<MasterDataLocalKeyValueObjective> MasterDataLocalKeyValues { get; set; }
        DbSet<Path> Paths { get; set; }
        DbSet<Service> Services { get; set; }
        DbSet<UserProfileObjective> Users { get; set; }
        DbSet<WebPageObjective> WebPages { get; set; }
        Task<int> SaveChangesAsync();
    }
}