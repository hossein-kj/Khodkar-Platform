 var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
        
 var pred = predicate
      .create('TypeCode', '==', 1);
    
        entityQuery.from('Files')
      .where(pred)
      .orderBy('Name desc')
        .skip(2)
        .take(2)
         .inlineCount()
      .select('Id,Name,Description,TypeCode,Size,Status')
     .using(manager).execute()
      .then(log)['catch'](log);