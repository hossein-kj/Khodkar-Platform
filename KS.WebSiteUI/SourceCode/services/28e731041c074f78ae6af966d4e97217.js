 var manager = $.asOdataQueryBuilder({url:"/odata/security/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
     var pred = predicate
      .create('GroupId', '==', 1);
      
        entityQuery.from('GroupRoles')
       // .withParameters({materDataKeyValueType: "Service" })
        .where(pred)
      .select('RoleId')
     .using(manager).execute()
      .then(log)['catch'](log);