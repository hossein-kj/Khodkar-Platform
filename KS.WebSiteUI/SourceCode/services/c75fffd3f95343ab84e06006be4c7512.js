 var manager = $.asOdataQueryBuilder({url:"/odata/security/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
     var pred = predicate
      .create('UserId', '==', 1);
      
        entityQuery.from('UserGroups')
       // .withParameters({materDataKeyValueType: "Service" })
        .where(pred)
      .select('GroupId')
     .using(manager).execute()
      .then(log)['catch'](log);