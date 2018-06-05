     var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
        
 var pred = predicate
 
      .create('ForeignKey1', '==', null)
       .or('ForeignKey1', '==', 756)
       .or('ForeignKey1', '==', 757)
       .or('ForeignKey1', '==', 758);
       var pred2 = pred.and('TypeId','==',1041);

    
        entityQuery.from('MasterDataKeyValues')
      .where(pred2)
      .select('Id,ParentId,PathOrUrl,Name,Code,Order')
     .using(manager).execute()
      .then(log)['catch'](log);