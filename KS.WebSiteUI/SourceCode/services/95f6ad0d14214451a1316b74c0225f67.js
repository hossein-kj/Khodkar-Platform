     var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
   
 var pred = predicate
      .create('Id', '==', 1);
        entityQuery.from('LanguageAndCultures')
      .where(pred)
      .select('Id,Culture,Country,IsRightToLeft,IsDefaults,FlagId,RowVersion,Status')
     .using(manager).execute()
      .then(log)['catch'](log);