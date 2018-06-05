 var manager = $.asOdataQueryBuilder({url:"/odata/security/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
             //var Predicate = breeze.Predicate;
 var pred = predicate
      .create('IsGroup','==',false);
       // breeze.EntityQuery.from('MasterDataLocalKeyValues')
        entityQuery.from('RoleGroups')
       // .withParameters({materDataKeyValueType: "Service" })
      .where(pred)
      .select('Id,ParentId,Name,Order,IsLeaf')
     .using(manager).execute()
      .then(log)['catch'](log);