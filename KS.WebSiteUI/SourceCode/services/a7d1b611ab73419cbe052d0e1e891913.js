     var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
             //var Predicate = breeze.Predicate;
 var pred = predicate
 .create('IsType', '==', true)
    //   .create('TypeId', '==', 1012)
    //   .or('ParentTypeId','==',1012)
     .and('TypeId', '==', 1012);
       // breeze.EntityQuery.from('MasterDataLocalKeyValues')
        entityQuery.from('MasterDataKeyValues')
       // .withParameters({materDataKeyValueType: "Service" })
      .where(pred)
      .select('Id,ParentTypeId,TypeId,Code,Order,Name,Key,Value')
     .using(manager).execute()
      .then(log)['catch'](log);