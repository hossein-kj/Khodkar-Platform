     var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
        
 var pred = predicate
      .create('TypeId', '==', 1033)
   .and('ParentId','==',245)
      .and('Value','!=',1);
    
        entityQuery.from('MasterDataKeyValues')
      .where(pred)
      .select('Id,ParentId,PathOrUrl')
     .using(manager).execute()
      .then(log)['catch'](log);