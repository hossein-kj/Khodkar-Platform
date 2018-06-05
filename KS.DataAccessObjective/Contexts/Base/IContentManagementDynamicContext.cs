using System.Data.Entity;
using KS.Model.ContentManagement;
using System.Threading.Tasks;

namespace KS.DataAccess.Contexts.Base
{
    public interface IContentManagementDynamicContext
    {
        DbSet<Comment> Comments { get; set; }
        DbSet<DynamicEntity> DynamicEntities { get; set; }
        DbSet<EntityFilePath> EntityFilePaths { get; set; }
        DbSet<EntityFile> EntityFiles { get; set; }
        DbSet<EntityGroup> EntityGroups { get; set; }
        DbSet<EntityMasterDataKeyValue> EntityMasterDataKeyValues { get; set; }
        DbSet<FilePath> FilePaths { get; set; }
        DbSet<File> Files { get; set; }
        DbSet<LanguageAndCulture> LanguageAndCultures { get; set; }
        DbSet<Link> Links { get; set; }
        DbSet<LocalFilePath> LocalFilePaths { get; set; }
        DbSet<LocalFile> LocalFiles { get; set; }
        DbSet<MasterDataKeyValue> MasterDataKeyValues { get; set; }
        DbSet<MasterDataLocalKeyValue> MasterDataLocalKeyValues { get; set; }
        DbSet<UserProfile> Users { get; set; }
        DbSet<WebPage> WebPages { get; set; }
        Task<int> SaveChangesAsync();
    }
}