
using System.Data.Entity;
using KS.Core.GlobalVarioable;

using KS.ObjectiveModel.ContentManagement;


using WebPageType = KS.ObjectiveModel.ContentManagement.WebPageType;

namespace KS.ObjectiveDataAccess.Contexts.Config
{
    public static class SqlContentManagementObjectiveModelConfiguration
    {
        public static void Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ContentManagement");
            modelBuilder.Entity<MasterDataKeyValueObjective>().ToTable("MasterDataKeyValues");
            modelBuilder.Entity<MasterDataLocalKeyValueObjective>().ToTable("MasterDataLocalKeyValues");
            modelBuilder.Entity<EntityFilePathObjective>().ToTable("EntityFilePaths");
            modelBuilder.Entity<EntityFileObjective>().ToTable("EntityFiles");
            modelBuilder.Entity<EntityMasterDataKeyValueObjective>().ToTable("EntityMasterDataKeyValues");
            modelBuilder.Entity<EntityGroupObjective>().ToTable("EntityGroups");
            modelBuilder.Entity<WebPageObjective>().ToTable("WebPages");
        
            modelBuilder.Entity<LinkObjective>().ToTable("Links");
            modelBuilder.Entity<FileObjective>().ToTable("Files");
            modelBuilder.Entity<FilePathObjective>().ToTable("FilePaths");
            modelBuilder.Entity<LocalFileObjective>().ToTable("LocalFiles");
            //modelBuilder.Entity<LocalCodeObjective>().ToTable("LocalCodes");
            modelBuilder.Entity<LocalFilePathObjective>().ToTable("LocalFilePaths");
            modelBuilder.Entity<LanguageAndCultureObjective>().ToTable("LanguageAndCultures");
            modelBuilder.Entity<CommentObjective>().ToTable("Comments");
            modelBuilder.Entity<UserProfileObjective>().ToTable("Users");
            //modelBuilder.Entity<CodeObjective>().ToTable("Codes");
           
            //modelBuilder.Entity<MasterDataLocalKeyValueDynamic>().ToTable("MasterDataLocalKeyValue");
            //  modelBuilder.Entity<EntityMasterDataKeyValue>()
            //.HasKey(c => new { c.EntityTypeId, c.EntityId });

            //modelBuilder.Entity<WebForm>()

            //    .HasMany<EntityMasterDataKeyValue>((WebForm w) => w.Services)

            //    .WithRequired()
            //    //.HasForeignKey((EntityMasterDataKeyValue r) =>
            //    //  new { r.EntityTypeId, r.EntityId });
            //    .HasForeignKey<int>((EntityMasterDataKeyValue kv) => kv.EntityId)
            //.WillCascadeOnDelete(false);

            //SqlGeneralConfiguration.MapMasterDataKeyValue(modelBuilder);

            //modelBuilder.Entity<MasterDataKeyValue>().ToTable("Common.MasterDataKeyValues");
            //modelBuilder.Entity<MasterDataLocalKeyValue>().ToTable("Common.MasterDataLocalKeyValues");
            //modelBuilder.Entity<EntityMasterDataKeyValue>().ToTable("Common.EntityMasterDataKeyValues");


            SqlGeneralConfiguration.InheritanceOfMasterDataKeyValue<Service>(modelBuilder,EntityIdentity.Service);
            SqlGeneralConfiguration.InheritanceOfMasterDataKeyValue<Path>(modelBuilder, EntityIdentity.Path);
            SqlGeneralConfiguration.InheritanceOfMasterDataKeyValue<Tag>(modelBuilder, EntityIdentity.Tag);
            SqlGeneralConfiguration.InheritanceOfMasterDataKeyValue<Group>(modelBuilder, EntityIdentity.Group);
            SqlGeneralConfiguration.InheritanceOfMasterDataKeyValue<WebPageType>(modelBuilder, EntityIdentity.WebPageType);
            SqlGeneralConfiguration.InheritanceOfMasterDataKeyValue<LinkType>(modelBuilder, EntityIdentity.LinkType);
            SqlGeneralConfiguration.InheritanceOfMasterDataKeyValue<WebPageTemplate>(modelBuilder, EntityIdentity.WebPageTemplate);
            SqlGeneralConfiguration.InheritanceOfMasterDataKeyValue<WebPageFrameWork>(modelBuilder, EntityIdentity.WebPageFrameWork);
            SqlGeneralConfiguration.InheritanceOfMasterDataKeyValue<KhodkarException>(modelBuilder, EntityIdentity.KhodkarException);
            SqlGeneralConfiguration.InheritanceOfMasterDataKeyValue<FileType>(modelBuilder, EntityIdentity.FileType);
            SqlGeneralConfiguration.InheritanceOfMasterDataKeyValue<MasterDataKeyValueType>(modelBuilder, EntityIdentity.MasterDataKeyValueType);


            //modelBuilder.Entity<MasterDataKeyValue>()
            //    .Map<Service>(m => m.Requires("TypeId").HasValue(EntityIdentity.Service));


            SqlGeneralConfiguration.InheritanceOfEntityMasterDataKeyValue<FileTag>(modelBuilder,
                EntityIdentity.FileTag);
            SqlGeneralConfiguration.InheritanceOfEntityMasterDataKeyValue<LinkTag>(modelBuilder,
              EntityIdentity.LinkTag);
            //SqlGeneralConfiguration.InheritanceOfEntityMasterDataKeyValue<LinkGroup>(modelBuilder,
            //  EntityIdentity.LinkGroup);
            SqlGeneralConfiguration.InheritanceOfEntityMasterDataKeyValue<FilePathTag>(modelBuilder,
              EntityIdentity.FilePathTag);
            //SqlGeneralConfiguration.InheritanceOfEntityMasterDataKeyValue<MasterDataKeyValueGroup>(modelBuilder,
            // EntityIdentity.MasterDataKeyValueGroup);

            SqlGeneralConfiguration.InheritanceOfEntityGroup<MasterDataKeyValueGroup>(modelBuilder,
              EntityIdentity.MasterDataKeyValueGroup);

            SqlGeneralConfiguration.InheritanceOfEntityGroup<LinkGroup>(modelBuilder,
              EntityIdentity.LinkGroup);

            SqlGeneralConfiguration.InheritanceOfEntityGroup<CommentGroup>(modelBuilder,
          EntityIdentity.CommentGroup);


            SqlGeneralConfiguration.InheritanceOfEntityFile<WebPageFile>(modelBuilder,
               EntityIdentity.WebPageFile);

            SqlGeneralConfiguration.InheritanceOfEntityFile<LinkFile>(modelBuilder,
               EntityIdentity.LinkFile);

            SqlGeneralConfiguration.InheritanceOfEntityFilePath<WebPageFilePath>(modelBuilder,
              EntityIdentity.WebPageFilePath);

            SqlGeneralConfiguration.InheritanceOfEntityFilePath<LinkFilePath>(modelBuilder,
             EntityIdentity.LinkFilePath);

            //modelBuilder.Entity<EntityMasterDataKeyValue>()
            //   .Map<WebFormService>(m => m.Requires("EntityTypeId").HasValue(EntityIdentity.WebformService));


            //modelBuilder.Entity<WebPageObjective>()
            //    .HasMany<WebPageKeyValue>((WebPageObjective w) => w.KeyValues)
            //    //.WithRequired()
            //    .WithOptional()
            //.WillCascadeOnDelete(true);

            modelBuilder.Entity<MasterDataKeyValueObjective>()
              .HasMany<MasterDataKeyValueGroup>((MasterDataKeyValueObjective w) => w.Groups)
               //.WithRequired()
               .WithOptional()
          .WillCascadeOnDelete(true);

            modelBuilder.Entity<LinkObjective>()
             .HasMany<LinkGroup>((LinkObjective w) => w.Groups)
              //.WithRequired()
              .WithOptional()
         .WillCascadeOnDelete(true);

            modelBuilder.Entity<CommentObjective>()
           .HasMany<CommentGroup>((CommentObjective w) => w.Groups)
            //.WithRequired()
            .WithOptional()
       .WillCascadeOnDelete(true);

          


            modelBuilder.Entity<WebPageObjective>()
               .HasMany<WebPageFile>((WebPageObjective w) => w.Files)
               //.WithRequired()
                .WithOptional()
           .WillCascadeOnDelete(true);

            modelBuilder.Entity<WebPageObjective>()
              .HasMany<WebPageFilePath>((WebPageObjective w) => w.FilePaths)
              //.WithRequired()
          .WithOptional()
          .WillCascadeOnDelete(true);

          








            modelBuilder.Entity<LinkObjective>()
             .HasMany<LinkTag>((LinkObjective w) => w.Tags)
         //.WithRequired()
         .WithOptional()
         .WillCascadeOnDelete(true);

        //    modelBuilder.Entity<LinkObjective>()
        //    .HasMany<LinkGroup>((LinkObjective w) => w.Groups)
        ////.WithRequired()
        //.WithOptional()
        //.WillCascadeOnDelete(false);

            modelBuilder.Entity<LinkObjective>()
              .HasMany<LinkFile>((LinkObjective w) => w.Files)
              //.WithRequired()
          .WithOptional()
          .WillCascadeOnDelete(true);

            modelBuilder.Entity<LinkObjective>()
             .HasMany<LinkFilePath>((LinkObjective w) => w.FilePaths)
         //.WithRequired()
         .WithOptional()
         .WillCascadeOnDelete(true);

            modelBuilder.Entity<FileObjective>()
           .HasMany<FileTag>((FileObjective w) => w.Tags)
       //.WithRequired()
       .WithOptional()
       .WillCascadeOnDelete(true);

            modelBuilder.Entity<FilePathObjective>()
           .HasMany<FilePathTag>((FilePathObjective w) => w.Tags)
       //.WithRequired()
       .WithOptional()
       .WillCascadeOnDelete(true);

            //      modelBuilder.Entity<MasterDataKeyValueObjective>()
            //    .HasMany<MasterDataKeyValueGroup>((MasterDataKeyValueObjective w) => w.Groups)
            ////.WithRequired()
            //.WithOptional()
            //.WillCascadeOnDelete(false);


            #region Add Relation To UserProfile

            modelBuilder.Entity<CommentObjective>()
 .HasOptional(c => c.CreateUser)
 .WithMany()
 .WillCascadeOnDelete(false);

            modelBuilder.Entity<CommentObjective>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

          

            modelBuilder.Entity<LinkObjective>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<LinkObjective>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<FileObjective>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<FileObjective>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<FilePathObjective>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<FilePathObjective>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<WebPageObjective>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<WebPageObjective>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<MasterDataKeyValueObjective>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<MasterDataKeyValueObjective>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<LanguageAndCultureObjective>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<LanguageAndCultureObjective>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<UserProfileObjective>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<UserProfileObjective>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);


//            modelBuilder.Entity<CodeObjective>()
//.HasOptional(c => c.CreateUser)
//.WithMany()
//.WillCascadeOnDelete(false);

//            modelBuilder.Entity<CodeObjective>()
//.HasOptional(c => c.ModifieUser)
//.WithMany()
//.WillCascadeOnDelete(false);

            modelBuilder.Entity<LocalFileObjective>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<LocalFileObjective>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

//            modelBuilder.Entity<LocalCodeObjective>()
//.HasOptional(c => c.CreateUser)
//.WithMany()
//.WillCascadeOnDelete(false);

//            modelBuilder.Entity<LocalCodeObjective>()
//.HasOptional(c => c.ModifieUser)
//.WithMany()
//.WillCascadeOnDelete(false);

            modelBuilder.Entity<LocalFilePathObjective>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<LocalFilePathObjective>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<MasterDataLocalKeyValueObjective>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<MasterDataLocalKeyValueObjective>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

            #endregion
        }
    }
}
