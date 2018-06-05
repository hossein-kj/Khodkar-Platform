using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Model;
using KS.Model.ContentManagement;


namespace KS.DataAccess.Contexts.Config
{
    public static class SqlContentManagementModelConfiguration
    {
        public static void Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ContentManagement");
            modelBuilder.Entity<UserProfile>().ToTable("Users");
            //modelBuilder.Entity<MasterDataKeyValue>().ToTable("MasterDataKeyValues");
            //modelBuilder.Entity<MasterDataLocalKeyValue>().ToTable("MasterDataLocalKeyValues");
            //modelBuilder.Entity<EntityFilePath>().ToTable("EntityFilePaths");
            //modelBuilder.Entity<EntityFile>().ToTable("EntityFiles");
            //modelBuilder.Entity<EntityMasterDataKeyValue>().ToTable("EntityMasterDataKeyValues");
            //modelBuilder.Entity<WebPage>().ToTable("WebPages");
            //modelBuilder.Entity<WebContent>().ToTable("WebContents");


            modelBuilder.Entity<MasterDataKeyValue>()
            .HasMany<EntityGroup>((MasterDataKeyValue w) => w.Groups)
             //.WithRequired()
             .WithOptional()
        .WillCascadeOnDelete(true);

            modelBuilder.Entity<Link>()
             .HasMany<EntityGroup>((Link w) => w.Groups)
              //.WithRequired()
              .WithOptional()
         .WillCascadeOnDelete(true);

            modelBuilder.Entity<Comment>()
           .HasMany<EntityGroup>((Comment w) => w.Groups)
            //.WithRequired()
            .WithOptional()
       .WillCascadeOnDelete(true);

            modelBuilder.Entity<WebPage>()
               .HasMany<EntityFile>((WebPage w) => w.Files)
                //.WithRequired()
                .WithOptional()
           .WillCascadeOnDelete(true);

            modelBuilder.Entity<DynamicEntity>()
         .HasMany<EntityGroup>((DynamicEntity w) => w.Groups)
          //.WithRequired()
          .WithOptional()
     .WillCascadeOnDelete(false);

            modelBuilder.Entity<EntityGroup>()
  .HasOptional(c => c.DynamicEntity)
  .WithMany()
  .WillCascadeOnDelete(false);

            modelBuilder.Entity<WebPage>()
              .HasMany<EntityFilePath>((WebPage w) => w.FilePaths)
          //.WithRequired()
          .WithOptional()
          .WillCascadeOnDelete(true);

            modelBuilder.Entity<DynamicEntity>()
                .HasMany<EntityDynamicEntity>((DynamicEntity w) => w.MasterEntities)
                //.WithRequired()
                .WithOptional()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DynamicEntity>()
         .HasMany<EntityDynamicEntity>((DynamicEntity w) => w.DetailEntities)
     //.WithRequired()
     .WithOptional()
     .WillCascadeOnDelete(true);

            modelBuilder.Entity<EntityDynamicEntity>()
    .HasRequired(c => c.DetailEntity)
    .WithMany()
    .WillCascadeOnDelete(false);


            modelBuilder.Entity<EntityDynamicEntity>()
    .HasRequired(c => c.DetailEntityType)
    .WithMany()
    .WillCascadeOnDelete(false);

            modelBuilder.Entity<DynamicEntity>()
              .HasMany<EntityMasterDataKeyValue>((DynamicEntity w) => w.KeyValues)
          //.WithRequired()
          .WithOptional()
          .WillCascadeOnDelete(true);

            modelBuilder.Entity<DynamicEntity>()
              .HasMany<EntityFile>((DynamicEntity w) => w.Files)
                //.WithRequired()
                .WithOptional()
          .WillCascadeOnDelete(true);

            modelBuilder.Entity<DynamicEntity>()
             .HasMany<EntityFilePath>((DynamicEntity w) => w.FilePaths)
         //.WithRequired()
         .WithOptional()
         .WillCascadeOnDelete(true);

            modelBuilder.Entity<Link>()
             .HasMany<EntityMasterDataKeyValue>((Link w) => w.KeyValues)
         //.WithRequired()
         .WithOptional()
         .WillCascadeOnDelete(true);

            //    modelBuilder.Entity<Link>()
            //    .HasMany<LinkGroup>((Link w) => w.Groups)
            ////.WithRequired()
            //.WithOptional()
            //.WillCascadeOnDelete(false);

            modelBuilder.Entity<Link>()
              .HasMany<EntityFile>((Link w) => w.Files)
          //.WithRequired()
          .WithOptional()
          .WillCascadeOnDelete(true);

            modelBuilder.Entity<Link>()
             .HasMany<EntityFilePath>((Link w) => w.FilePaths)
         //.WithRequired()
         .WithOptional()
         .WillCascadeOnDelete(true);

            modelBuilder.Entity<File>()
           .HasMany<EntityMasterDataKeyValue>((File w) => w.KeyValues)
       //.WithRequired()
       .WithOptional()
       .WillCascadeOnDelete(true);

            modelBuilder.Entity<FilePath>()
           .HasMany<EntityMasterDataKeyValue>((FilePath w) => w.KeyValues)
       //.WithRequired()
       .WithOptional()
       .WillCascadeOnDelete(true);

            //      modelBuilder.Entity<MasterDataKeyValue>()
            //    .HasMany<MasterDataKeyValueGroup>((MasterDataKeyValue w) => w.Groups)
            ////.WithRequired()
            //.WithOptional()
            //.WillCascadeOnDelete(false);

 #region Add Relation To UserProfile

            modelBuilder.Entity<Comment>()
 .HasOptional(c => c.CreateUser)
 .WithMany()
 .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comment>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<DynamicEntity>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<DynamicEntity>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<Link>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<Link>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<File>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<File>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<FilePath>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<FilePath>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<WebPage>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<WebPage>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<MasterDataKeyValue>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<MasterDataKeyValue>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<LanguageAndCulture>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<LanguageAndCulture>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<UserProfile>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<UserProfile>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

//            modelBuilder.Entity<Code>()
//.HasOptional(c => c.CreateUser)
//.WithMany()
//.WillCascadeOnDelete(false);

//            modelBuilder.Entity<Code>()
//.HasOptional(c => c.ModifieUser)
//.WithMany()
//.WillCascadeOnDelete(false);


            modelBuilder.Entity<LocalFile>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<LocalFile>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

//            modelBuilder.Entity<LocalCode>()
//.HasOptional(c => c.CreateUser)
//.WithMany()
//.WillCascadeOnDelete(false);

//            modelBuilder.Entity<LocalCode>()
//.HasOptional(c => c.ModifieUser)
//.WithMany()
//.WillCascadeOnDelete(false);

            modelBuilder.Entity<LocalFilePath>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<LocalFilePath>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<MasterDataLocalKeyValue>()
.HasOptional(c => c.CreateUser)
.WithMany()
.WillCascadeOnDelete(false);

            modelBuilder.Entity<MasterDataLocalKeyValue>()
.HasOptional(c => c.ModifieUser)
.WithMany()
.WillCascadeOnDelete(false);
            #endregion
        }
    }
}
