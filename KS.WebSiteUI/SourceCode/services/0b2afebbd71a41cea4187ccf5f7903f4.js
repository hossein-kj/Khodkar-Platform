  var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
             //var Predicate = breeze.Predicate;
 var pred = predicate
      .create('Language','==','fa');
       // breeze.EntityQuery.from('MasterDataLocalKeyValues')
        entityQuery.from('Links')
       // .withParameters({materDataKeyValueType: "Service" })
      .where(pred)
      .select('Id,ParentId,Text,Html,Url,Order,IsLeaf')
     .using(manager).execute()
      .then(log)['catch'](log);