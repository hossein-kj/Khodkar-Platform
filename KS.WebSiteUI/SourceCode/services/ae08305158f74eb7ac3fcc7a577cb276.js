     var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
        
 var pred = predicate
      .create('TypeId', '==', 1001);
    
        entityQuery.from('MasterDataKeyValues')
       // .withParameters({materDataKeyValueType: "Service" })
      .where(pred)
      .select('Id,ParentId,Code,Order,Name,IsLeaf,Key,Value,PathOrUrl')
     .using(manager).execute()
      .then(log)['catch'](log);