     var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")

 var pred = predicate
      .create('GroupId', '==', 71);
   
        entityQuery.from('EntityGroups')
       // .withParameters({materDataKeyValueType: "Service" })
      .where(pred)
      .select('Id,LinkId,MasterDataKeyValueId,CommentId,EntityTypeId')
     .using(manager).execute()
      .then(log)['catch'](log);