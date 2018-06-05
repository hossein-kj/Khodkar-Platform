 var manager = $.asOdataQueryBuilder({url:"/odata/security/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
            
 var pred = predicate
      .create('Language','==','en');
        entityQuery.from('LocalGroups')
      .where(pred)
     .expand('Group')
      .select('Group.Id,Group.ParentId,Group.Name,Group.Order,Group.IsLeaf,Name')
     .using(manager).execute()
      .then(log)['catch'](log);