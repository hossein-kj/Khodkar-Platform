using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using KS.ObjectiveModel.Base;

namespace KS.ObjectiveDataAccess.Util
{
    public static class EntityExtensions
    {
        //public static List<T> GetTreeByAllOffspring<T>(this ITree<T> tree) where T : class
        //{

        //   return new List<T>();
        //}

        //public async static Task<int> UpdateByLogAsync<T>(this IQueryable<T> source, Expression<Func<T, T>> changes)
        //    where T : class, ILogEntity,new ()
        //{
        //    var dateTime = DateTime.UtcNow;
        //    var localeDateTime = Helper.ToLocalDateTime(dateTime.AddHours(Common.Settings.Config.LocalTimeZone));

           
        //    Expression<Func<T, T>> logChanges = wf => new T()
        //    {
        //        ModifieDateTime = dateTime,
        //        ModifieLocalDateTime = localeDateTime,
        //        ModifieUserId = LoginUser.Id,
        //        AccessDateTime = dateTime,
        //        AccessLocalDateTime = localeDateTime
        //    };


        //    //changes.ModifieDateTime = dateTime;
        //    //changes.ModifieLocalDateTime = localeDateTime;
        //    //changes.ModifieUserId = LoginUser.Id;

        //    //changes.AccessDateTime = dateTime;
        //    //changes.AccessLocalDateTime = localeDateTime;

        //    return await source.UpdateAsync(Expression.Lambda<Func<T, T>>
        //        (Expression.AndAlso(changes.Body, logChanges.Body), changes.Parameters[0]));
        //}

//        public async static Task<List<T>> GetTreeByAllOffspringAsync2<T>(this DbSet<T> source, string entityTable, string where) where T : class 
//        {
//            var temp = @"
//;WITH cte AS 
// (
//  SELECT tree.*
//  FROM " + entityTable + @" tree
//  WHERE " + where + @" 
//  UNION ALL
//  SELECT tree.*
//  FROM " + entityTable + @" tree JOIN cte c ON tree.parentId = c.Id
//  )
//  SELECT  *
//  FROM cte
//";
//            temp = temp;
//            return await source.SqlQuery(temp).ToListAsync();
//        }
    }
}
