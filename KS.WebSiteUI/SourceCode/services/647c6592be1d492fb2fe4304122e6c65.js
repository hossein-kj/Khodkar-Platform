 var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
        
 var pred = predicate
      .create('Id', '==', 1);
    
        entityQuery.from('Files')
      .where(pred)
      .select('Id,Name,Description,Url,Guid,Status,Size,ViewRoleId,TypeCode,AccessRoleId,ModifyRoleId,RowVersion')
     .using(manager).execute()
      .then(log)['catch'](log);