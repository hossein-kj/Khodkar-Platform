 $('#if8732d663c3d4cdf863af491153d6813').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('Test Page');var asPageEvent = '#if8732d663c3d4cdf863af491153d6813'; var asPageId = '.if8732d663c3d4cdf863af491153d6813.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({}, $.asUrls);         // throw "fake error!"
        //$("#developTestPagePartial1").asAjax({
         //   url: $.asUrls.cms_getServices,
         //   type:'get',
         //   data: {queryOption:JSON.stringify({
          //      query: 'Id==70'
          //  })},
          //  success: function (webForm) {
          //   console.dir(webForm)
         //   }
        //});
          //breeze.config.initializeAdapterInstance('dataService', 'webApiOData', true);
          
          
          
          
          
          
       // var dataService = new breeze.DataService({
        //serviceName: "/odata/cms/",
       // hasServerMetadata: false
       // });
        
       // var manager = new breeze.EntityManager({
       // dataService: dataService
       // });
        
       
          
        var log2 = function(data){
            //console.dir(data)
            //$("#developTestPagePartial1").html(data.results[0].value[0].MasterDataKeyValue.Name)
             
            // console.dir($.asGetPropertybyName({name:{family:'reza'}},'name.family'))
            // console.dir($.asTreeify({ list: data.results[0].value, keyDataField: {name:'this.Id'}, parentDataField: {name:'this.ParentId'} }));
             
               as("#drp_service").asDropdown({
        source: {
            hierarchy:
            {
                type: 'flat',
                keyDataField:{name:'[MasterDataKeyValue][Id]'},
                parentDataField:{name:'[MasterDataKeyValue][ParentId]'}
            },
            localData: data.results
            , displayDataField: 'MasterDataKeyValue.Name'
              ,valueDataField: 'MasterDataKeyValue.Code',
              orderby:'MasterDataKeyValue.Order'
        }
   , multiple: true
  //, selectParents: false
   ,parentMode: "uniq"
  
    });
    
        }
          var log = function(data){
            //console.dir(data)
            //$("#developTestPagePartial1").html(data.results[0].value[0].MasterDataKeyValue.Name)
             
            // console.dir($.asGetPropertybyName({name:{family:'reza'}},'name.family'))
            // console.dir($.asTreeify({ list: data.results[0].value, keyDataField: {name:'this.Id'}, parentDataField: {name:'this.ParentId'} }));
             
               as("#drp_service").asDropdown({
        source: {
            hierarchy:
            {
                type: 'flat',
                keyDataField:{name:'Id'},
                parentDataField:{name:'ParentId'}
            },
            localData: data.results
            , displayDataField: 'Name'
              ,valueDataField: 'Code',
              orderby:'Order'
        }
   , multiple: true
  //, selectParents: false
   ,parentMode: "uniq"
  
    });
    
        }
        var i = 0

  
   var test2 = function(){
       
     var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
             //var Predicate = breeze.Predicate;
 var pred = predicate
      .create('MasterDataKeyValue.TypeId', '==', 1001)
      .and('Language','==','en');
       // breeze.EntityQuery.from('MasterDataLocalKeyValues')
        entityQuery.from('MasterDataLocalKeyValues')
       // .withParameters({materDataKeyValueType: "Service" })
      .where(pred)
     .expand('MasterDataKeyValue')
      .select('MasterDataKeyValue.Id,MasterDataKeyValue.ParentId,MasterDataKeyValue.Code,MasterDataKeyValue.Order,MasterDataKeyValue.Name,Name')
     .using(manager).execute()
      .then(log2)['catch'](log2);
             
      
      // Yet another way to ask the same question
//var pred = Predicate
 //      .create('Freight', '>;', 100)
 //      .and('OrderDate', '>;', new Date(1998, 3, 1));
//var query1c = baseQuery.where(pred);

      //var query = EntityQuery.from('Products')
    //.where('Category.CategoryName', 'startswith', 'S')
    //.expand('Category');
      
   }
   
var temp = function(){
                   $("#cmsWebForm_drp_service2").asDropdown({
        source: {
            hierarchy:
            {
                type: 'flat',
                keyDataField:{name:'[MasterDataKeyValue][Id]'},
                parentDataField:{name:'[MasterDataKeyValue][ParentId]'}
            },
            url: "/odata/cms/MasterDataLocalKeyValues?$filter=(MasterDataKeyValue%2FTypeId%20eq%201001d)%20and%20(Language%20eq%20'en')&$expand=MasterDataKeyValue&$select=MasterDataKeyValue%2FId%2CMasterDataKeyValue%2FParentId%2CMasterDataKeyValue%2FCode%2CMasterDataKeyValue%2FOrder%2CMasterDataKeyValue%2FName%2CName"
            , displayDataField: 'MasterDataKeyValue.Name'
              ,valueDataField: 'MasterDataKeyValue.Code',
              orderby:'MasterDataKeyValue.Order'
        }
   , multiple: true
  //, selectParents: false
   ,parentMode: "uniq"
  
    });
}

var test = function(){
       
     var manager = $.asOdataQueryBuilder({url:"/odata/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
             //var Predicate = breeze.Predicate;
 var pred = predicate
      .create('TypeId', '==', 1001);
       // breeze.EntityQuery.from('MasterDataLocalKeyValues')
        entityQuery.from('MasterDataKeyValues')
       // .withParameters({materDataKeyValueType: "Service" })
      .where(pred)
      .select('Id,ParentId,Code,Order,Name')
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
      
   }
   
   var testEntityGroup = function(){
       
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
      .then(logEntityGroup)['catch'](logEntityGroup);
             
      
      // Yet another way to ask the same question
//var pred = Predicate
 //      .create('Freight', '>;', 100)
 //      .and('OrderDate', '>;', new Date(1998, 3, 1));
//var query1c = baseQuery.where(pred);

      //var query = EntityQuery.from('Products')
    //.where('Category.CategoryName', 'startswith', 'S')
    //.expand('Category');
    
 
      
   }
   
     var logEntityGroup = function(data){
            //console.dir(data)
    
       as("#cmsWebForm_menu").asAfterTasks([
    loadLinks()
], function (links) {
    
            $.asEach(links, function (link) {
          if(link.Link.Url === '#Admin')
          link.Link.Text = $.asStorage.getItem($.asUserName);
           
        });
    //console.dir(links)
    
       //$("#testService").asDropdown({
      //  source: {
       //     hierarchy:
      //      {
      //          type: 'flat',
      //          keyDataField: { name: '[Link][Id]' },
      //          parentDataField: { name: '[Link][ParentId]' },
     //           childrenDataField: 'children'
     //       },
     //       valueDataField: 'Link.Url',
     //       orderby: 'Link.Order',
    //        displayDataField: 'Link.Text',
    //       localData:links
    //    },
    //    link: true,
    //    parentMode: "uniq",
    //    selectParents: false,
    //    moveByFixedNav: {
    //        initialTop: 86
     //   }
    //})
    
    
     as("#testService").asDropdown({
        source: {
          hierarchy:
            {
                type: 'flat',
                keyDataField: { name: '[Link][Id]' },
                parentDataField: { name: '[Link][ParentId]' },
                childrenDataField: 'children'
            },
            valueDataField: 'Link.Url',
            orderby: 'Link.Order',
            displayDataField: 'Link.Text',
           localData:links
        },
        link: true,
        parentMode: "uniq",
        selectParents: false,
        moveByFixedNav: {
            initialTop: 86
        }
    })
    
});

    
     //$("#testService").asDropdown({
      //  source: {
      //      hierarchy:
     //       {
     //           type: 'flat',
      //          keyDataField: { name: '[Link][Id]' },
     //           parentDataField: { name: '[Link][ParentId]' },
     //           childrenDataField: 'children'
     //       },
     //       valueDataField: 'Link.Url',
     //       orderby: 'Link.Order',
    //        displayDataField: 'Link.Text',
    //        url:$.asInitService("/odata/cms/EntityGroups?$filter=((EntityTypeId%20eq%20101d)%20and%20(GroupId%20eq%2071d))%20and%20(Link%2FLanguage%20eq%20'@lang')&$expand=Link&$select=Link%2FId%2CLink%2FParentId%2CLink%2FText%2CLink%2FHtml%2CLink%2FUrl%2CLink%2FOrder",[{name:'@lang',value:'en'}])
   //     },
   //     link: true,
   //     parentMode: "uniq",
   //     selectParents: false,
   //     moveByFixedNav: {
   //         initialTop: 86
   //     }
  //  })
    
    
  //  $("#testService").asAjax({
  //     type:'get',
  //  url:$.asInitService("/odata/cms/EntityGroups?$filter=((EntityTypeId%20eq%20101d)%20and%20(GroupId%20eq%2071d))%20and%20(Link%2FLanguage%20eq%20'@lang')&$expand=Link&$select=Link%2FId%2CLink%2FParentId%2CLink%2FText%2CLink%2FHtml%2CLink%2FUrl",[{name:'@lang',value:'en'}])
//,success: function (links) {
  //  console.log(links);
//}});
        }
        
        function loadLinks(url){
      return $.asAjaxTask({
     url:$.asInitService("/odata/cms/EntityGroups?$filter=((EntityTypeId%20eq%20101d)%20and%20(GroupId%20eq%2071d))%20and%20(Link%2FLanguage%20eq%20'@lang')&$expand=Link&$select=Link%2FId%2CLink%2FParentId%2CLink%2FText%2CLink%2FHtml%2CLink%2FUrl%2CLink%2FOrder",[{name:'@lang',value:'fa'}])
      });
}
   
//test();
//testEntityGroup();
//console.log($.asStorage.getItem($.asUserName))

 var getDefaultsLink = function(){
       
     var manager = $.asOdataQueryBuilder({url:"/odata/public/cms/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
             //var Predicate = breeze.Predicate;
 var pred = predicate
      .create('TypeId', '==', 1)
      .or('TypeId','==',2)
      .and('Language','==','fa');
       // breeze.EntityQuery.from('MasterDataLocalKeyValues')
        entityQuery.from('LinksPublic')
       // .withParameters({materDataKeyValueType: "Service" })
      .where(pred)
      .select('Id,ParentId,Text,Html,Url,Order,IsLeaf')
     .using(manager).execute()
      .then(logGetDefaultsLink)['catch'](logGetDefaultsLink);
      
   }
   
    var getAllLink = function(){
       
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
      .then(logGetDefaultsLink)['catch'](logGetDefaultsLink);
      
   }
   
    var logGetDefaultsLink = function(data){
       
       // console.dir($.asTreeify({ list: data.results, keyDataField: {name:'Id'}, parentDataField: {name:'ParentId'} }));
        console.dir($.asTreeify({ list: data.results, keyDataField: {name:'Id'}, parentDataField: {name:'ParentId'} ,removeChildLessParent:true}));
            //console.dir(data)
 
    }
    
      var getAllRole = function(){
       
     var manager = $.asOdataQueryBuilder({url:"/odata/security/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
             //var Predicate = breeze.Predicate;
 var pred = predicate
      .create('IsGroup','==',false);
       // breeze.EntityQuery.from('MasterDataLocalKeyValues')
        entityQuery.from('RoleGroups')
       // .withParameters({materDataKeyValueType: "Service" })
      .where(pred)
      .select('Id,ParentId,Name,Order,IsLeaf')
     .using(manager).execute()
      .then(logGetAllRolek)['catch'](logGetAllRolek);
      
   }
   
    var logGetAllRolek = function(data){
       
       // console.dir($.asTreeify({ list: data.results, keyDataField: {name:'Id'}, parentDataField: {name:'ParentId'} }));
       console.dir($.asTreeify({ list: data.results, keyDataField: {name:'Id'}, parentDataField: {name:'ParentId'} ,removeChildLessParent:true}));
            //console.dir(data)
 
    }
  
    
      var getAllRoleByOtherLang = function(){
       
     var manager = $.asOdataQueryBuilder({url:"/odata/security/"});
   var predicate =  $.asOdataQueryBuilder("Predicate")
   var entityQuery = $.asOdataQueryBuilder("EntityQuery")
         
             //var Predicate = breeze.Predicate;
 var pred = predicate
      .create('Role.IsGroup','==',false)
      .and('Language','==','en');
        entityQuery.from('LocalRoleGroups')
      .where(pred)
     .expand('Role')
      .select('Role.Id,Role.ParentId,Role.Name,Role.Order,Role.IsLeaf,Name')
     .using(manager).execute()
      .then(logGetAllRoleByOtherLang)['catch'](logGetAllRoleByOtherLang);
      
   }
    var logGetAllRoleByOtherLang = function(data){
       
       // console.dir($.asTreeify({ list: data.results, keyDataField: {name:'Id'}, parentDataField: {name:'ParentId'} }));
       console.dir($.asTreeify({ list: data.results, keyDataField: {name:'[Role][Id]'}, parentDataField: {name:'[Role][ParentId]'}, isLeafDataField:'[Role][IsLeaf]' ,removeChildLessParent:true}));
            //console.dir(data)
 
    }
    //getAllRoleByOtherLang()
    //cmsWebForm.$editorContent.asEditor({ htmlEditor: "cmsWebForm_HtmlSource" });
               
        //       tinymce.remove();
        //   tinymce.init({
        //             selector: "#cmsWebForm_Html",
        //             rtl_ui: true,
        //             directionality: 'rtl',
        //             skin_url: '/Content/editor/skins/lightgray',
        //             theme_url: '/scripts/editor/themes/modern/theme.js',
        //             language_url: '/scripts/editor/langs/' + $.asLang  + '.js',
        //             setup: function (editor) {
        //                 editor.on('change', function (e) {
        //                     //var codeEditor = ace.edit(params.htmlEditor)                       
        //                     //codeEditor.setValue(tinymce.get($editor.prop('id')).getContent())
        //                 });
        //             }
        //         });
      // tinyMCE.execCommand('mceAddControl', false, '#cmsWebForm_Html');
              
  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });