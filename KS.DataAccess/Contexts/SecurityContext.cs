﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using KS.Core.Exceptions;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;
using KS.Core.Log;
using KS.Core.Log.Elmah.Base;
using KS.Core.Model.ContentManagement;
using KS.Core.Model.Core;
using KS.Core.Security;
using KS.DataAccess.Config;
using KS.DataAccess.Contexts.Config;
using KS.Model.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using KS.DataAccess.Contexts.Base;
using KS.Core.Model.Log;

namespace KS.DataAccess.Contexts
{

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //For Oracle Migration
    [DbConfigurationType(typeof(KS.DataAccess.Config.OracleSecurityModelConfiguration))]
    public class SecurityContext :
        IdentityDbContext
            <ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, ISecurityContext
    {
        protected readonly IErrorLogManager ErrorLogManager;
        public SecurityContext(IErrorLogManager errorLogManager)
            : base(ConnectionKey.DefaultsSqlServerConnection.ToString())
        {
            Database.Log += Log;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            ErrorLogManager = errorLogManager;
        }

        static SecurityContext()
        {
            Database.SetInitializer<SecurityContext>(new ApplicationDbInitializer());
        }
        public DbSet<ApplicationLocalGroup> ApplicationLocalGroups { get; set; }
        public DbSet<ApplicationLocalRole> ApplicationLocalRoles { get; set; }
        public DbSet<ApplicationQueryAuthrize> ApplicationQueryAuthrizes { get; set; }

        private static void Log(string s)
        {
            Debug.WriteLine(s);
        }

        public async Task<List<T>> GetTreeByAllOffspringAsync<T>(string entityTable, string where) where T : ITree<T>
        {
            return await Database.SqlQuery<T>(@"
;WITH cte AS 
 (
  SELECT tree.*
  FROM " + entityTable + @" tree
  WHERE " + where + @" 
  UNION ALL
  SELECT tree.*
  FROM " + entityTable + @" tree JOIN cte c ON tree.parentId = c.Id
  )
  SELECT  *
  FROM cte
").ToListAsync();
        }

        //public static ApplicationDbContext Create()
        //{
        //    return new ApplicationDbContext();
        //}

        // Add the ApplicationGroups property:

        public virtual DbSet<ApplicationGroup> Groups { get; set; }

        //public virtual DbSet<ApplicationTree> ApplicationTrees { get; set; }

        // Override OnModelsCreating:

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Make sure to call the base method first:

            base.OnModelCreating(modelBuilder);


            // Map Roles to Role:

            //    modelBuilder.Entity<ApplicationRole>()

            //.HasMany(x => x.Childrens)
            //.WithOptional()
            //    .HasForeignKey(x => x.ParentId)
            //    .WillCascadeOnDelete(false);

            //    modelBuilder.Entity<ApplicationTreeNode>()
            //        .HasKey(p => new {p.AncestorId, p.OffspringId});

            //    modelBuilder.Entity<ApplicationTree>()
            //        .HasOptional(d => d.Parent)
            //        .WithMany(p => p.Childrens)
            //        .HasForeignKey(d => d.ParentId)
            //        .WillCascadeOnDelete(false);

            //    modelBuilder.Entity<ApplicationTree>()
            //        .HasMany(p => p.Ancestors)
            //        .WithRequired(d => d.Offspring)
            //        .HasForeignKey(d => d.OffspringId)
            //        .WillCascadeOnDelete(false);

            //    // has many offspring
            //    modelBuilder.Entity<ApplicationTree>()
            //        .HasMany(p => p.Offspring)
            //        .WithRequired(d => d.Ancestor)
            //        .HasForeignKey(d => d.AncestorId)
            //        .WillCascadeOnDelete(false);

            // Map Users to Groups:

            modelBuilder.Entity<ApplicationGroup>()

                .HasMany<ApplicationUserGroup>((ApplicationGroup g) => g.ApplicationUsers)

                .WithRequired()

                .HasForeignKey<int>((ApplicationUserGroup ag) => ag.GroupId);


            modelBuilder.Entity<ApplicationUserGroup>()

                .HasKey((ApplicationUserGroup r) =>
                    new
                    {
                        UserId = r.UserId,

                        GroupId = r.GroupId

                    });




            // Map Roles to Groups:

            modelBuilder.Entity<ApplicationGroup>()

                .HasMany<ApplicationGroupRole>((ApplicationGroup g) => g.ApplicationRoles)

                .WithRequired()

                .HasForeignKey<int>((ApplicationGroupRole ap) => ap.GroupId);

            modelBuilder.Entity<ApplicationGroupRole>().HasKey((ApplicationGroupRole gr) =>
                new
                {
                    RoleId = gr.RoleId,

                    GroupId = gr.GroupId

                });

            SqlSecurityModelConfiguration.Config(modelBuilder);
        }

        public override int SaveChanges()
        {
            FillLogProperty();
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //var entry = ex.Entries.Single();
                ////The MSDN examples use Single so I think there will be only one
                ////but if you prefer - do it for all entries
                ////foreach(var entry in ex.Entries)
                ////{
                //if (entry.State == EntityState.Deleted)
                //    //When EF deletes an item its state is set to Detached
                //    //http://msdn.microsoft.com/en-us/data/jj592676.aspx
                //    entry.State = EntityState.Detached;
                //else
                //    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                ////throw; //You may prefer not to resolve when updating
                ////}
                throw new DataConcurrencyException();
            }
            catch (DbEntityValidationException ex)
            {
                var errors = new List<KeyValue>();
                foreach (var dbValidationErrors in ex.EntityValidationErrors.Select(er => er.ValidationErrors))
                    errors.AddRange(dbValidationErrors.Select(dbValidationError => new KeyValue()
                    {
                        Key = dbValidationError.PropertyName,
                        Value = dbValidationError.ErrorMessage
                    }));
                ErrorLogManager.LogException(new ExceptionLog()
                {
                    Detail = string.Join(" - ", errors.Select(er => er.Key + " : " + er.Value)),
                    Message = ex.Message,
                    Source = ex.GetType().FullName
                });
                throw;
            }
        }

        public override async Task<int> SaveChangesAsync()
        {
            FillLogProperty();
            try
            {
                return await base.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //var entry = ex.Entries.Single();
                ////The MSDN examples use Single so I think there will be only one
                ////but if you prefer - do it for all entries
                ////foreach(var entry in ex.Entries)
                ////{
                //if (entry.State == EntityState.Deleted)
                //    //When EF deletes an item its state is set to Detached
                //    //http://msdn.microsoft.com/en-us/data/jj592676.aspx
                //    entry.State = EntityState.Detached;
                //else
                //    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                ////throw; //You may prefer not to resolve when updating
                ////}
                throw new DataConcurrencyException();
            }
            catch (DbEntityValidationException ex)
            {
                var errors = new List<KeyValue>();
                foreach (var dbValidationErrors in ex.EntityValidationErrors.Select(er => er.ValidationErrors))
                    errors.AddRange(dbValidationErrors.Select(dbValidationError => new KeyValue()
                    {
                        Key = dbValidationError.PropertyName,
                        Value = dbValidationError.ErrorMessage
                    }));
                ErrorLogManager.LogException(new ExceptionLog()
                {
                    Detail = string.Join(" - ", errors.Select(er => er.Key + " : " + er.Value)),
                    Message = ex.Message,
                    Source = ex.GetType().FullName
                });
                throw;
            }
        }

        private void FillLogProperty()
        {
            var dateTime = DateTime.UtcNow;
            var localeDateTime = LanguageManager.ToLocalDateTime(dateTime);


            var entities =
                ChangeTracker.Entries()
                    .Where(
                        x => x.Entity is ILogEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            //        var currentUsername = !string.IsNullOrEmpty(System.Web.HttpContext.Current ? .
            //        User? .
            //        Identity? .
            //        Name)
            //?
            //        HttpContext.Current.User.Identity.Name
            //        :
            //        "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((ILogEntity)entity.Entity).Status = 1;
                    ((ILogEntity)entity.Entity).CreateDateTime = dateTime;
                    ((ILogEntity)entity.Entity).CreateLocalDateTime = localeDateTime;
                    if (CurrentUserManager.Id != 0)
                        ((ILogEntity)entity.Entity).CreateUserId = CurrentUserManager.Id;
                }
                ((ILogEntity)entity.Entity).ModifieDateTime = dateTime;
                ((ILogEntity)entity.Entity).ModifieLocalDateTime = localeDateTime;
                if (CurrentUserManager.Id != 0)
                    ((ILogEntity)entity.Entity).ModifieUserId = CurrentUserManager.Id;

                ((ILogEntity)entity.Entity).AccessDateTime = dateTime;
                ((ILogEntity)entity.Entity).AccessLocalDateTime = localeDateTime;
            }
        }

        //public System.Data.Entity.DbSet<KS.Model.Security.ApplicationRole> ApplicationRoles { get; set; }

        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { get; set; }

        public DbSet<ApplicationGroupRole> ApplicationGroupRoles { get; set; }

        //public System.Data.Entity.DbSet<KS.Model.Security.ApplicationRole> ApplicationRoles { get; set; }
    }


}
