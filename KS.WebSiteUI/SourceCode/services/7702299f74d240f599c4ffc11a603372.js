     var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
   
 var pred = predicate
      .create('Id', '==', 1);
        entityQuery.from('Links')
      .where(pred)
      .select('Id,Text,Html,TypeId,IconPath,IsLeaf,Url,Order,ParentId,ShowToSearchEngine,ViewRoleId,ModifyRoleId,AccessRoleId,Action,TransactionCode,IsMobile,RowVersion,Status')
     .using(manager).execute()
      .then(log)['catch'](log);