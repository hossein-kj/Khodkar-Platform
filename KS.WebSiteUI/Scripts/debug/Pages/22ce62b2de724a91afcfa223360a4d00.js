 $('#i22ce62b2de724a91afcfa223360a4d00').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('مدیریت منابع Bundle ها');var asPageEvent = '#i22ce62b2de724a91afcfa223360a4d00'; var asPageId = '.i22ce62b2de724a91afcfa223360a4d00.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({develop_code_browser_GetBundlesOfCodeByDefaultsLanguageByPaging:"/odata/cms/MasterDataKeyValues?$filter=(TypeId%20eq%20@typeIdd)%20and%20(ParentId%20eq%20@parentIdd)&$orderby=@orderby&$skip=@skip&$top=@top&$select=Id%2CName%2CCode%2CPathOrUrl%2CKey%2CValue%2CSecondCode%2CDescription%2CViewRoleId%2CAccessRoleId%2CModifyRoleId%2CRowVersion&$inlinecount=allpages",develop_code_browser_bundle_source_save:"/develop/code/browser/bundle/source/Save",develop_code_browser_bundle_source_delete:"/develop/code/browser/bundle/source/delete"}, $.asUrls); var 
     
     $win=$(asPageId),
     $frmAddOrAupdate=as("#frmAddOrAupdate"),
     $txtPath=as("#txtPath"),
    $btnTranslator=as("#btnTranslator"),
     $txtDescription=as("#txtDescription"),
     $btnCancel=as("#btnCancel"),
     $btnSelect=as("#btnSelect"),
     $btnEdit=as("#btnEdit"),
     $btnNew=as("#btnNew"),
     $drpViewRole= as("#drpViewRole3"),
    $drpModifyRole= as("#drpModifyRole3"),
    $drpAccessRole= as("#drpAccessRole3"),
    $btnSelectPath=as("#btnSelectPath"),
    $btnDell=as("#btnDell"),
     $grvBundleSources=as("#grvBundleSources"),
      $winAddOrEdit=as("#winAddOrEdit"),
      $winFileSelector=$.asModalManager.get({url:$.asModalManager.urls.fileSelector}),
      $winTranslator=$.asModalManager.get({url:$.asModalManager.urls.translator}),
        $divRoles=as("#divRoles"),
        viewRoleId,
        accessRoleId,
        modifyRoleId,
        validate,
      runGetRoles = 0,
       isNew=false,
       sourceName,
       selectedItems={
         items:{}
     },
    bundleId="";
     
     $winAddOrEdit.asModal(
    {backdrop:'static', keyboard: false}
    );
     $winTranslator.asModal({width:800});
    $winFileSelector.asModal({width:800}) ;
    
    var validateRule = {
        txtPath:{
            required: true,
            maxlength:1024
        },
        txtDescription:{
            maxlength:256
        },
        drpViewRole: {
            asType: 'asDropdown',
            required: true
        },
        drpAccessRole: {
            asType: 'asDropdown',
            required: true
        },
        drpModifyRole: {
        asType: 'asDropdown',
          required: true
        }

    }
       validate =$frmAddOrAupdate.asValidate({ rules: validateRule});
       var setBundleSourceWin = function(source){
           if(source===null){
            $txtPath.val(asPageParams.codePath);
            $txtDescription.val("");
            $drpViewRole.asDropdown('selectValue', asPageParams.viewRoleId);
            viewRoleId=asPageParams.viewRoleId;
            $drpAccessRole.asDropdown('selectValue', asPageParams.accessRoleId);
            accessRoleId=asPageParams.accessRoleId;
            $drpModifyRole.asDropdown('selectValue', asPageParams.modifyRoleId);
            modifyRoleId=asPageParams.modifyRoleId;
           }else{
            $txtPath.val(source.PathOrUrl);
            $txtDescription.val(source.Description);
        
           
            $drpViewRole.asDropdown('selectValue', source.ViewRoleId);
           viewRoleId=source.ViewRoleId;
            $drpAccessRole.asDropdown('selectValue', source.AccessRoleId);
          accessRoleId=source.AccessRoleId;
            $drpModifyRole.asDropdown('selectValue', source.ModifyRoleId);
            modifyRoleId=source.ModifyRoleId;
           }

                
    }
 var calculateSelectedBundleSource = function(event, rows){

        if(event.type==="selected"){
            selectedItems.items[rows[0].Id]={Id:rows[0].Id,Key:rows[0].Key,Name:rows[0].Name,
            Description:rows[0].Description,PathOrUrl:rows[0].PathOrUrl,RowVersion:rows[0].RowVersion
                ,ViewRoleId:rows[0].ViewRoleId,AccessRoleId:rows[0].AccessRoleId,ModifyRoleId:rows[0].ModifyRoleId
            }
        }else{
            delete selectedItems.items[rows[0].Id]
        }
    }
        if(asPageParams){
          bundleId=asPageParams.bundleId;
          
              $drpViewRole.asDropdown({
        source: {
            hierarchy:
            {
                type: 'flat',
                keyDataField: { name: 'Id' },
                parentDataField: { name: 'ParentId' },
                childrenDataField: 'Children',
                removeChildLessParent: true
            },
            valueDataField: 'Id',
            displayDataField: 'Description',
            orderby: 'Order',
            localData: asPageParams.roles
        }
      
    });

    $drpAccessRole.asDropdown({
        source: {

            hierarchy:
            {
                type: 'flat',
                keyDataField: { name: 'Id' },
                parentDataField: { name: 'ParentId' },
                childrenDataField: 'Children',
                removeChildLessParent: true
            },
            valueDataField: 'Id',
            displayDataField: 'Description',
            orderby: 'Order',
            localData: asPageParams.roles
        }
     
    });

    $drpModifyRole.asDropdown({
        source: {

            hierarchy:
            {
                type: 'flat',
                keyDataField: { name: 'Id' },
                parentDataField: { name: 'ParentId' },
                childrenDataField: 'Children',
                removeChildLessParent: true
            },
            valueDataField: 'Id',
            displayDataField: 'Description',
            orderby: 'Order',
            localData: asPageParams.roles
        }
  
    });
    
           setBundleSourceWin(null);
           
               $grvBundleSources.asBootGrid({
    rowCount:[10,25,50,100],
    source:{
        url:''
    },
    requestHandler:function(request){
         if (bundleId !== "") {
   
        var orderbyValue = "PathOrUrl desc"
        var skip = 0
        if(request.current > 1)
        skip=(request.current - 1) * request.rowCount
        $.each(request.sort, function(key, value) {
                 orderbyValue = key + " " + value
                });
        request.url = $.asInitService($.asUrls.develop_code_browser_GetBundlesOfCodeByDefaultsLanguageByPaging, [
            { name: '@typeId', value: 1034 }
            ,{ name: '@parentId', value: bundleId }
            ,{ name: '@top', value: request.rowCount }
            ,{ name: '@skip', value: skip }
            ,{ name: '@orderby', value: orderbyValue }]);
            selectedItems.items={};
             return request
         }
    },
        selection: true,
        rowSelect:true
}).on("selected.rs.jquery.asBootGrid", function(e, rows)
{
    calculateSelectedBundleSource(e,rows)
    
}).on("deselected.rs.jquery.asBootGrid", function(e, rows)
{
    calculateSelectedBundleSource(e,rows)
});
          }
           
       


      

var getSelectedsources = function(){
                
           var sources=[]
              $.each(selectedItems.items,function(i,v){
                sources.push(v)
            });
            return sources;
}
                  var onSuccess = function(){
           $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
              selectedItems.items={};
              $grvBundleSources.asBootGrid("deselect");
     }
 var bindEvent =function(){
       $(asPageEvent).on($.asEvent.modal.reopen, function (event,params) {
       
            if(params.bundleId !== asPageParams.bundleId){
                asPageParams=params;
                 bundleId=asPageParams.bundleId;
                setBundleSourceWin(null);
                $grvBundleSources.asBootGrid("reload");
            }
          
        });
        
     $(asPageEvent).on($.asEvent.page.ready, function (event) {
        
         
        });
        
         $(asPageEvent).on("fileSelected",function(event,selectedFile,selectedId,selectedFileName){
                $txtPath.val(selectedFile);
                sourceName=selectedFileName;
         });
    $btnEdit.click(function () {
            
    var sources = getSelectedsources();
       if (sources.length === 1)  {
            isNew=false;
            

            setBundleSourceWin(            {
            PathOrUrl:sources[0].PathOrUrl,
            Description:sources[0].Description,
            RowVersion:sources[0].RowVersion,
           ViewRoleId:asPageParams.viewRoleId,
           AccessRoleId:asPageParams.accessRoleId,
          ModifyRoleId:asPageParams.modifyRoleId,
            });
            
            $winAddOrEdit.asModal("show");
          }else
           $.asShowMessage({template:"error", message: "   باید یک buneSourcele انتخاب شود"});
           
        });
    $btnNew.click(function () {
           
            isNew=true;
            setBundleSourceWin(null);
            $winAddOrEdit.asModal("show");
           
        });
        
     $btnTranslator.click(function () {
         var sources = getSelectedsources();
       if (sources.length === 1)  {
        $winTranslator.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.translator)},{name:"@isModal",value:true}])
        ,{
            type:"masterDataKeyValue",
            id:sources[0].Id,
            name:sources[0].Name,
            description:sources[0].Description
            
        });
       }else
           $.asShowMessage({template:"error", message: "   باید یک buneSourcele انتخاب شود"});
    });
        
         $drpViewRole.on("change", function (event, item) {
                if (typeof (item.value) != "undefined") {
                    viewRoleId=item.value;
                }
        });
        $drpAccessRole.on("change", function (event, item) {
                if (typeof (item.value) != "undefined") {
                    accessRoleId=item.value;
                }
        });
        $drpModifyRole.on("change", function (event, item) {
                if (typeof (item.value) != "undefined") {
                    modifyRoleId=item.value;
                }
        });
        
        as("#btnExecute").click(function(){
             var sources = getSelectedsources();
             $winAddOrEdit.asAjax({
                    url: $.asUrls.develop_code_browser_bundle_source_save,
                    data: JSON.stringify({
                Id: isNew === true ? 0:sources[0].Id,
                Name:sourceName,
                ParentId:asPageParams.bundleId,
                TypeId:1033,
                ParentTypeId:"",
                IsPath:true,
                PathOrUrl: $txtPath.val(),
                ViewRoleId: viewRoleId,
                ModifyRoleId: modifyRoleId,
                AccessRoleId: accessRoleId,
                Description:$txtDescription.val(),
                Status: true,
                EnableCache: false,
                EditMode: false,
                IsLeaf: true,
                IsType: false,
                IsNew:isNew,
                RowVersion: isNew === true ? "":sources[0].RowVersion,
                    }),
                    success: function (result) {
                        if( isNew===true){
                           
                            isNew=false;
                        }else{
                             
                               $grvBundleSources.asBootGrid("remove");
                        }
                         $grvBundleSources.asBootGrid("append",[result]);
                          
                      onSuccess();
                      $winAddOrEdit.asModal('hide');
                    }
                },{$form: $frmAddOrAupdate,overlayClass: "as-overlay-absolute"});
        });
       as("#btnCancel").click(function () {
            
       $winAddOrEdit.asModal('hide');
        });
        
        $btnSelectPath.click(function () {
         $winFileSelector.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.fileSelector)},{name:"@isModal",value:true}])
            ,{path:asPageParams.codePath,parent:asPageEvent,event:"fileSelected"})
        });
        
            $btnDell.click(function () {
    
            var sources = getSelectedsources();
       if (sources.length === 1)  {
            $win.asAjax({
            url: $.asUrls.develop_code_browser_bundle_source_delete,
            data: JSON.stringify({
                Id: sources[0].Id
               
            }),
            success: function (result) {
                 $btnDell.button('reset')
                $grvBundleSources.asBootGrid("remove");
             onSuccess();
            }
        }, { validate:false ,overlayClass: "as-overlay-absolute"});
         $btnDell.button('loading') ;
          }else
           $.asShowMessage({template:"error", message: "   باید یک buneSourcele انتخاب شود"});
    });
     asOnPageDispose = function(){
          $grvBundleSources.asBootGrid("destroy");
          validate.destroy();
        }
            
 }
 bindEvent()  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });