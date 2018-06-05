     var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
   
 var pred = predicate
      .create('FileId', '==', 1)
      .and('Language','==','en');
        entityQuery.from('LocalFiles')
      .where(pred)
      .select('Id,Name,Description,RowVersion,Status')
     .using(manager).execute()
      .then(log)['catch'](log);