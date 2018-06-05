  var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
   
 var pred = predicate
      .create('MasterDataKeyValue.TypeId', '==', 1001)
      .and('MasterDataKeyValue.ParentId','==',33)
      .and('Language','==','en');
 
        entityQuery.from('MasterDataLocalKeyValues')
  
      .where(pred)
     .expand('MasterDataKeyValue')
      .select('MasterDataKeyValue.Id,MasterDataKeyValue.ParentId,MasterDataKeyValue.Code,MasterDataKeyValue.Order,Name')
     .using(manager).execute()
      .then(log)['catch'](log);
             
      
      // Yet another way to ask the same question
//var pred = Predicate
 //      .create('Freight', '>;', 100)
 //      .and('OrderDate', '>;', new Date(1998, 3, 1));
//var query1c = baseQuery.where(pred);

      //var query = EntityQuery.from('Products')
    //.where('Category.CategoryName', 'startswith', 'S')
    //.expand('Category');
      