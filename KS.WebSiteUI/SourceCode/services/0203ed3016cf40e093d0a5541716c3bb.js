     var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
        
 var pred = predicate
      .create('MasterDataKeyValue.TypeId', '==', 1034)
      .and('MasterDataKeyValue.ParentId','==',33)
       .and('Language','==','en');
    
        entityQuery.from('MasterDataLocalKeyValues')
      .where(pred)
      .expand('MasterDataKeyValue')
       .orderBy('Name desc')
        .skip(2)
        .take(2)
         .inlineCount()
      .select('MasterDataKeyValue.Id,Name,MasterDataKeyValue.Name,MasterDataKeyValue.Code,MasterDataKeyValue.PathOrUrl,MasterDataKeyValue.Key,MasterDataKeyValue.Value,MasterDataKeyValue.SecondCode,Description,MasterDataKeyValue.Description,MasterDataKeyValue.ViewRoleId,MasterDataKeyValue.AccessRoleId,MasterDataKeyValue.ModifyRoleId,MasterDataKeyValue.RowVersion')
     .using(manager).execute()
      .then(log)['catch'](log);