 var manager = $.asOdataQueryBuilder({url:"/odata/security/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
             //var Predicate = breeze.Predicate;
 var pred = predicate
      .create('Role.IsGroup','==',false)
      .and('Language','==','en');
        entityQuery.from('LocalRoleGroups')
      .where(pred)
     .expand('Role')
      .select('Role.Id,Role.ParentId,Role.Name,Role.Order,Role.IsLeaf,Name')
     .using(manager).execute()
      .then(log)['catch'](log);