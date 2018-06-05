 var manager = $.asOdataQueryBuilder({url:"/odata/security/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         

        entityQuery.from('Roles')
       // .withParameters({materDataKeyValueType: "Service" })
  
      .select('Id,ParentId,Name,Order,IsLeaf,Description')
     .using(manager).execute()
      .then(log)['catch'](log);