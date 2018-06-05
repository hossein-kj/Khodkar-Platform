     var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
        
 var pred = predicate
      .create('TypeId', '==', 1034)
      .and('ParentId','==',33);
    
        entityQuery.from('MasterDataKeyValues')
      .where(pred)
       .orderBy('Name desc')
        .skip(2)
        .take(2)
         .inlineCount()
      .select('Id,Name,Code,PathOrUrl,Key,Value,SecondCode,Description,ViewRoleId,AccessRoleId,ModifyRoleId,RowVersion')
     .using(manager).execute()
      .then(log)['catch'](log);