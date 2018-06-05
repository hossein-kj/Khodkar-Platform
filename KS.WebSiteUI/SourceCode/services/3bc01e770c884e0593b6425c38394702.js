     var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
        
 var pred = predicate
 
      .create('TypeId', '==', 1041)
       .and('Id', '==', 943);
 

    
        entityQuery.from('MasterDataKeyValues')
      .where(pred)
      .select('Id,Version,Name,Code,Order')
     .using(manager).execute()
      .then(log)['catch'](log);