using System;
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
using KS.Core.Log.Base;
using KS.Core.Log.Elmah.Base;
using KS.Core.Model.ContentManagement;
using KS.Core.Model.Core;
using KS.Core.Model.Log;
using KS.Core.Security;
using KS.Core.UI.Setting;
using KS.Model.Base;

namespace KS.DataAccess.Contexts.Base
{
    public abstract class BaseContext<TContext> : DbContext where TContext : DbContext
    {
        protected readonly IErrorLogManager ErrorLogManager;
        protected readonly IActionLogManager ActionLogManager;
        private string _query = "";
        protected BaseContext(IErrorLogManager errorLogManager
            , IActionLogManager actionLogManager) : base(ConnectionKey.DefaultsSqlServerConnection.ToString())
        {
            Database.Log += Log;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            ErrorLogManager = errorLogManager;
            ActionLogManager = actionLogManager;
        }

        protected BaseContext() : base(ConnectionKey.DefaultsSqlServerConnection.ToString())
        {

        }

        static BaseContext()
        {
            Database.SetInitializer<TContext>(null);
        }

        private void Log(string s)
        {
            if (Settings.IsDebugMode)
            {
                _query += s;
                if(Database.Connection.State == System.Data.ConnectionState.Closed)
                {
                    var dataTime = DateTime.UtcNow;

                    ActionLogManager.Log(new ActionLog()
                    {
                        DateTime = dataTime,
                        Ip = CurrentUserManager.Ip,
                        IsDebugMode = true,
                        IsMobileMode = Settings.IsMobileMode,
                        LocalDateTime = LanguageManager.ToLocalDateTime(dataTime),
                        Name = "DataBaseQueryLog",
                        ServiceUrl = "/DataBaseQueryLog",
                        Url = "/DataBaseQueryLog",
                        User = CurrentUserManager.UserName,
                        IsSuccessed = true,
                        Request = _query
                    });
                    _query = "";
                }
            }
            else
            {
#if (DEBUG == true)
                Debug.WriteLine(s);
#endif
            }
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
                        Key = dbValidationError.PropertyName, Value = dbValidationError.ErrorMessage
                    }));
                ErrorLogManager.LogException(new ExceptionLog()
                {
                    Detail = string.Join(" - " ,errors.Select(er=>er.Key + " : " + er.Value)),
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

        //public void UpdateKeyValueCollection<T>(List<T> collection, List<int> newCollection)
        //    where  T:EntityMasterDataKeyValueObjective, new()

        //{
        //    while (collection.Count > 0)
        //    {
        //        Entry(collection.First()).State = EntityState.Deleted;
        //    }

        //    collection.AddRange(newCollection.Select(item => new T() {MasterDataKeyValueId = item}));
        //}
    }
}
