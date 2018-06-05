using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Core.GlobalVarioable;
using KS.Model;

using KS.Model.ContentManagement;

namespace KS.DataAccess.Contexts.Config
{
    internal static class SqlGeneralConfiguration
    {
        public static void InheritanceOfMasterDataKeyValue<T>(DbModelBuilder modelBuilder,EntityIdentity entitySelfId) where T:MasterDataKeyValueObjective 
        {
            //modelBuilder.Entity<MasterDataKeyValue>()
            //    .Map<T>(m => m.Requires("TypeId").HasValue((int)typeof(T).GetMethod("GetSelfEntityId").Invoke(null, new object[] { })));
            modelBuilder.Entity<MasterDataKeyValueObjective>()
               .Map<T>(m => m.Requires("TypeId").HasValue((int)entitySelfId));
        }

        public static void InheritanceOfEntityMasterDataKeyValue<T>(DbModelBuilder modelBuilder,
            EntityIdentity entitySelfId) where T : EntityMasterDataKeyValueObjective
        {
            modelBuilder.Entity<EntityMasterDataKeyValueObjective>()
                .Map<T>(m => m.Requires("EntityTypeId").HasValue((int)entitySelfId));
        }

        public static void InheritanceOfEntityFile<T>(DbModelBuilder modelBuilder,
            EntityIdentity entitySelfId) where T : EntityFileObjective
        {
            modelBuilder.Entity<EntityFileObjective>()
                .Map<T>(m => m.Requires("EntityTypeId").HasValue((int)entitySelfId));
        }

        public static void InheritanceOfEntityGroup<T>(DbModelBuilder modelBuilder,
            EntityIdentity entitySelfId) where T : EntityGroupObjective
        {
            modelBuilder.Entity<EntityGroupObjective>()
                .Map<T>(m => m.Requires("EntityTypeId").HasValue((int)entitySelfId));
        }

        public static void InheritanceOfEntityFilePath<T>(DbModelBuilder modelBuilder,
           EntityIdentity entitySelfId) where T : EntityFilePathObjective
        {
            modelBuilder.Entity<EntityFilePathObjective>()
                .Map<T>(m => m.Requires("EntityTypeId").HasValue((int)entitySelfId));
        }

        //public static void MapMasterDataKeyValue(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<MasterDataKeyValue>().ToTable("Common.MasterDataKeyValues");
        //    modelBuilder.Entity<MasterDataLocalKeyValue>().ToTable("Common.MasterDataLocalKeyValues");
        //    modelBuilder.Entity<EntityMasterDataKeyValue>().ToTable("Common.EntityMasterDataKeyValues");
        //}
    }
}
