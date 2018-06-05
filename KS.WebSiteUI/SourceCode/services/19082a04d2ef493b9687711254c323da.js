     var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
   
 var pred = predicate
      .create('MasterDataKeyValueId', '==', 1)
      .and('Language','==','en');
        entityQuery.from('MasterDataLocalKeyValues')
       // .withParameters({materDataKeyValueType: "Service" })
      .where(pred)
      .select('Id,Name,Description,RowVersion,Status')
     .using(manager).execute()
      .then(log)['catch'](log);