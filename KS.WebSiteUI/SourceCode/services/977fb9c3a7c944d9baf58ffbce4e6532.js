     var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
   
 var pred = predicate
      .create('FilePath.TypeCode', '==', 1)
      .and('Language','==','en');
        entityQuery.from('LocalFilePaths')
       // .withParameters({materDataKeyValueType: "Service" })
      .where(pred)
      .orderBy('Name desc')
        .skip(2)
        .take(2)
         .inlineCount()
      .expand('FilePath')
       .select('FilePath.Id,Name,Description,FilePath.Url,FilePath.Guid,FilePath.Status,FilePath.Size')
     .using(manager).execute()
      .then(log)['catch'](log);