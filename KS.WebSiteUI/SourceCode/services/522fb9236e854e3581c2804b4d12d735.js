     var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
        
 var pred = predicate
 
      .create('ForeignKey1', '==', 752)
      .or('ForeignKey1', '==', 753)
       .or('ForeignKey1', '==', 754);
       var pred2 = pred.and('TypeId','==',1041).and('ForeignKey2','==',941);

    
        entityQuery.from('MasterDataKeyValues')
      .where(pred2)
      .select('Id,ParentId,PathOrUrl,Name,Code,Order')
     .using(manager).execute()
      .then(log)['catch'](log);