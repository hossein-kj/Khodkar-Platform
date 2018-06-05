     var manager = $.asOdataQueryBuilder({url:"/odata/security/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
   
 var pred = predicate
      .create('GroupId', '==', 1)
      .and('Language','==','en');
        entityQuery.from('LocalGroups')
       // .withParameters({materDataKeyValueType: "Service" })
      .where(pred)
      .select('Id,Name,Description,RowVersion,Status')
     .using(manager).execute()
      .then(log)['catch'](log);