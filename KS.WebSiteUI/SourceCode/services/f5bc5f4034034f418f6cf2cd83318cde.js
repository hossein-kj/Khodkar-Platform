     var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
        
 var pred = predicate
      .create('ParentTypeId','==',1028);
    
        entityQuery.from('MasterDataKeyValues')
      .where(pred)
      .select('Id,ParentId,Code,Order,Name,TypeId')
     .using(manager).execute()
      .then(log)['catch'](log);