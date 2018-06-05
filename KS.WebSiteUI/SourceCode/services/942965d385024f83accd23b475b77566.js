 var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
        
 var pred = predicate
      .create('TypeCode', '==', 1)
      .and('Url', 'startsWith', '/content');
    
        entityQuery.from('FilePaths')
      .where(pred)
      .orderBy('Name desc')
        .skip(2)
        .take(2)
         .inlineCount()
      .select('Id,Name,Url,Status,Size')
     .using(manager).execute()
      .then(log)['catch'](log);