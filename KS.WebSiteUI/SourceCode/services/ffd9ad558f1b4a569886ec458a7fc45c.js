     var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
        
 var pred = predicate
      .create('ParentId','==',33);
    
        entityQuery.from('MasterDataKeyValues')
      .where(pred)
      .select('Id,ParentId,Code,Order,Name')
     .using(manager).execute()
      .then(log)['catch'](log);