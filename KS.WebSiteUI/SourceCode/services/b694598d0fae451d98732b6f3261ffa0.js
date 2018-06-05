 var manager = $.asOdataQueryBuilder({url:"/odata/security/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
            
 var pred = predicate
      .create('Language','==','en');
        entityQuery.from('LocalRoles')
      .where(pred)
     .expand('Role')
      .select('Role.Id,Role.ParentId,Role.Name,Role.Order,Role.IsLeaf,Name')
     .using(manager).execute()
      .then(log)['catch'](log);