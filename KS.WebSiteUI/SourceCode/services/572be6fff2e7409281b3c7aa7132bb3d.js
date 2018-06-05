     var manager = $.asOdataQueryBuilder({url:"/odata/security/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
   
 var pred = predicate
      .create('RoleId', '==', 1)
      .and('Language','==','en');
        entityQuery.from('LocalRoles')
       // .withParameters({materDataKeyValueType: "Service" })
      .where(pred)
      .select('Id,Name,Description,RowVersion,Status')
     .using(manager).execute()
      .then(log)['catch'](log);