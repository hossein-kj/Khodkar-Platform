 var manager = $.asOdataQueryBuilder({url:"/odata/security/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
        
    var pred = predicate
      .create('Id', '==', 55);
      
        entityQuery.from('Roles')
       // .withParameters({materDataKeyValueType: "Service" })
        .where(pred)
      .select('Id,ParentId,Name,Order,IsLeaf,Description,Status,ViewRoleId,ModifyRoleId,AccessRoleId')
     .using(manager).execute()
      .then(log)['catch'](log);