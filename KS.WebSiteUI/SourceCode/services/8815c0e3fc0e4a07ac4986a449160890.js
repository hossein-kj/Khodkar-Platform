     var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
   
 var pred = predicate
      .create('File.TypeCode', '==', 1)
      .and('Language','==','en');
        entityQuery.from('LocalFiles')
      .where(pred)
      .orderBy('Name desc')
        .skip(2)
        .take(2)
         .inlineCount()
      .expand('File')
      .select('File.Id,Name,Description,File.Status,File.TypeCode,File.Size')
     .using(manager).execute()
      .then(log)['catch'](log);