     var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
             //var Predicate = breeze.Predicate;
 var pred = predicate
      .create('EntityTypeId', '==', 101)
      .and('GroupId','==',71)
      .and('Link.Language','==','en');
       // breeze.EntityQuery.from('MasterDataLocalKeyValues')
        entityQuery.from('EntityGroups')
       // .withParameters({materDataKeyValueType: "Service" })
      .where(pred)
     .expand('Link')
      .select('Link.Id,Link.ParentId,Link.Text,Link.Html,Link.Url')
     .using(manager).execute()
      .then(log)['catch'](log);