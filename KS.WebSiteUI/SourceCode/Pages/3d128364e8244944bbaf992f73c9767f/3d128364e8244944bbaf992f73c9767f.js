
var 
     
     $win=$(asPageId),
     $frmAddOrAupdate=as("#frmAddOrAupdate"),
     $txtDependencyKey=as("#txtDependencyKey"),
     $txtPath=as("#txtPath"),
     $txtCdnUrl=as("#txtCdnUrl"),
     $txtName=as("#txtName"),
     $txtDescription=as("#txtDescription"),
     $chkCdn=as("#chkCdn"),
     $chkOneByOneBundle=as("#chkOneByOneBundle"),
     $chkPublishBundle=as("#chkPublishBundle"),
     $btnCancel=as("#btnCancel"),
     $btnSelect=as("#btnSelect"),
     $btnTranslator=as("#btnTranslator"),
     $btnEdit=as("#btnEdit"),
     $btnNew=as("#btnNew"),
     $btnCompile = as("#btnCompile"),
     $btnAddDependency=as("#btnAddDependency"),
     $drpViewRole= as("#drpViewRole2"),
    $drpModifyRole= as("#drpModifyRole2"),
    $drpAccessRole= as("#drpAccessRole2"),
    $drpCodeType=as("#drpCodeTypeForDependency"),
    $drpCodes=as("#drpCodesDependency"),
    $drpBundlesDependency=as("#drpBundlesDependency"),
    $drpDependency=as("#drpDependency"),
    $btnManageSource=as("#btnManageSource"),
    $btnSelectPath=as("#btnSelectPath"),
    $btnDell=as("#btnDell"),
     $grvBundles=as("#grvBundles"),
     $winTranslator=$.asModalManager.get({url:$.asModalManager.urls.translator}),
      $winAddOrEdit=as("#winAddOrEdit"),
      $winPathSelector=$.asModalManager.get({url:$.asModalManager.urls.directorySelector}),
      $winBundleSourceManager=as("#winBundleSourceManager"),
        $divRoles=as("#divRoles"),
        $divCdn=as("#divCdn"),
        $divDependency=as("#divDependency"),
        $divDependencyPanel=as("#divDependencyPanel"),
        winAddOrEditFirstShow=true,
        validate,
      runGetRoles = 0,
       isNew=false,
       debugPath,
       selectedItems={
         items:{}
     },
          template =
        '<div class="as-material-switch container-fluid @class"><div><input name="chkCdn" type="checkbox" @IsCdn /><label class="label-default as-label" for="chkCdn" >  </label></div></div>',
    codeId="";
    $divCdn.hide();
    $winAddOrEdit.asModal(
    {backdrop:'static', keyboard: false}
    );
    $winTranslator.asModal({width:800})
    $winPathSelector.asModal({width:800}) ;
    $winBundleSourceManager.asModal({width:800}) ;
    var validateRule = {
         txtCdnUrl:{
             required: {
                depends: function (element) {
                return $(element).is(":visible");
                }
            },
            maxlength:1024
        },
         txtDependencyKey:{
             required: {
                depends: function (element) {
                return (getSelectedDependency()).length > 0;
                }
            }
        },
         txtName:{
            required: true,
            maxlength:255
        },
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
       var setBundleWin = function(bundle){
           if(bundle===null){
            //  debugPath=asPageParams.codeType === "js" ? $.asDebugBaceScriptUrl:$.asDebugBaceStyleUrl;
            debugPath="/";
            $txtPath.val(debugPath);
            $txtName.val("");
            $txtDescription.val("");
            $txtDependencyKey.val("");
            $chkCdn.prop('checked', false);
           $chkOneByOneBundle.prop('checked', false);
          $divDependencyPanel.show();
            $drpViewRole.asDropdown('selectValue', asPageParams.viewRoleId);
           
            $drpAccessRole.asDropdown('selectValue', asPageParams.accessRoleId);
          
            $drpModifyRole.asDropdown('selectValue', asPageParams.modifyRoleId);
           }else{
               var bundles = getSelectedBundles();
            $txtPath.val(bundle.PathOrUrl);
            $txtDependencyKey.val(bundle.Code === "bundle_" + bundles[0].Id ? "":bundle.Code);
            $txtName.val(bundle.Name);
            $txtDescription.val(bundle.Description);
            $chkCdn.prop('checked', bundle.IsCdn);
            $chkOneByOneBundle.prop('checked', bundle.OneByOneBundle);
            
            bundle.OneByOneBundle ? $divDependencyPanel.hide():$divDependencyPanel.show();
            $drpViewRole.asDropdown('selectValue', bundle.ViewRoleId);
           
            $drpAccessRole.asDropdown('selectValue', bundle.AccessRoleId);
          
            $drpModifyRole.asDropdown('selectValue', bundle.ModifyRoleId);
           }

                
    }
     var calculateSelectedBundle = function(event, rows){

        if(event.type==="selected"){               
            selectedItems.items[rows[0].Id]={Id:rows[0].MasterDataKeyValue.Id,Key:rows[0].MasterDataKeyValue.Key,Value:rows[0].MasterDataKeyValue.Value,Name:rows[0].MasterDataKeyValue.Name
            ,Code:rows[0].MasterDataKeyValue.Code,
            SecondCode:rows[0].MasterDataKeyValue.SecondCode,Description:rows[0].MasterDataKeyValue.Description,PathOrUrl:rows[0].MasterDataKeyValue.PathOrUrl,RowVersion:rows[0].MasterDataKeyValue.RowVersion
                ,ViewRoleId:rows[0].MasterDataKeyValue.ViewRoleId,AccessRoleId:rows[0].MasterDataKeyValue.AccessRoleId,ModifyRoleId:rows[0].MasterDataKeyValue.ModifyRoleId
            }
        }else{
            delete selectedItems.items[rows[0].MasterDataKeyValue.Id]
        }
    }
    
   
    

        if(asPageParams){
          codeId=asPageParams.codeId;
          
    $drpViewRole.asDropdown({
        source: {
            hierarchy:
            {
                type: 'flat',
                keyDataField: { name: 'Role.Id' },
                parentDataField: { name: 'Role.ParentId' },
                childrenDataField: 'Children',
                isLeafDataField: 'Role.IsLeaf',
                removeChildLessParent: true
            },
            valueDataField: 'Role.Id',
            displayDataField: 'Name',
            orderby: 'Role.Order',
            localData: asPageParams.roles
        }

    })
    $drpAccessRole.asDropdown({
        source: {
            hierarchy:
            {
                type: 'flat',
                keyDataField: { name: 'Role.Id' },
                parentDataField: { name: 'Role.ParentId' },
                childrenDataField: 'Children',
                isLeafDataField: 'Role.IsLeaf',
                removeChildLessParent: true
            },
            valueDataField: 'Role.Id',
            displayDataField: 'Name',
            orderby: 'Role.Order',
            localData: asPageParams.roles
        }

    })

    $drpModifyRole.asDropdown({
        source: {
            hierarchy:
            {
                type: 'flat',
                keyDataField: { name: 'Role.Id' },
                parentDataField: { name: 'Role.ParentId' },
                childrenDataField: 'Children',
                isLeafDataField: 'Role.IsLeaf',
                removeChildLessParent: true
            },
            valueDataField: 'Role.Id',
            displayDataField: 'Name',
            orderby: 'Role.Order',
            localData: asPageParams.roles
        }

    })
    
           setBundleWin(null);
    $grvBundles.asBootGrid({
    rowCount:[10,25,50,100],
    source:{
        url:''
    },
    requestHandler:function(request){
         if (codeId !== "") {
   
        var orderbyValue = "Name desc"
        var skip = 0
        if(request.current > 1)
        skip=(request.current - 1) * request.rowCount
        $.each(request.sort, function(key, value) {
                 orderbyValue = key.replace(".","/") + " " + value
                });                   
        request.url = $.asInitService($.asUrls.develop_code_browser_GetBundlesOfCodeByOtherLanguagesByPaging, [
            { name: '@typeId', value: 1033 }
            ,{ name: '@lang', value: $.asLang }
            ,{ name: '@parentId', value: codeId }
            ,{ name: '@top', value: request.rowCount }
            ,{ name: '@skip', value: skip }
            ,{ name: '@orderby', value: orderbyValue }]);
            selectedItems.items={};
             return request
         }
    },
    formatters: {
        Id: function (column, row)
        {
         return row.MasterDataKeyValue ? row.MasterDataKeyValue.Id:row.Id
        },
         PathOrUrl: function (column, row)
        {
           
         return row.MasterDataKeyValue ?  row.MasterDataKeyValue.PathOrUrl:row.PathOrUrl;
        },
         IsCdn: function (column, row)
        {
         return (row.MasterDataKeyValue ? row.MasterDataKeyValue.Key === 1:row.Key === 1) ? template.replace('@IsCdn','checked="checked"').replace('@class',''):template.replace('@IsCdn','').replace('@class','rtl-text');
        }
    },
        selection: true,
        rowSelect:true
}).on("selected.rs.jquery.asBootGrid", function(e, rows)
{
    calculateSelectedBundle(e,rows)
    
}).on("deselected.rs.jquery.asBootGrid", function(e, rows)
{
    calculateSelectedBundle(e,rows)
});
          }
           
  
  

var getSelectedDependency = function(){
                
    if ($drpBundlesDependency.asDropdown('selected')) {
             var selected = [];
              $.each($drpDependency.asDropdown('selected'), function (i, v) {
                if (v.selected){
                     selected.push(v.value)
                }
            });
            return selected;
        }
         return [];
}

var getSelectedBundles = function(){
                
           var bundles=[]
              $.each(selectedItems.items,function(i,v){
                bundles.push(v)
            });
            return bundles;
}
                  var onSuccess = function(){
           $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
              selectedItems.items={};
              $grvBundles.asBootGrid("deselect");
              $winAddOrEdit.asModal("hide")
     }
 var bindEvent =function(){
         $(asPageEvent).on($.asEvent.modal.reopen, function (event,params) {
            if(params.codeId !== asPageParams.codeId){
                asPageParams=params;
                     codeId=asPageParams.codeId;
                    setBundleWin(null);
                      $grvBundles.asBootGrid("reload");
            }
          
        });
     $(asPageEvent).on($.asEvent.page.ready, function (event) {
    
          
        });
         $(asPageEvent).on("pathSelected",function(event,selectedFolder,selectedId){
                $txtPath.val(selectedFolder.toLowerCase().replace(new RegExp(selectedFolder.toLowerCase().indexOf($.asDebugBaceStyleUrl.toLowerCase()) > -1 
                ? $.asDebugBaceStyleUrl.toLowerCase():$.asDebugBaceScriptUrl.toLowerCase(), "gi"), ""));
         });
         
      $btnTranslator.click(function () {
          var bundles = getSelectedBundles();
       if (bundles.length === 1)  {
        $winTranslator.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.translator)},{name:"@isModal",value:true}])
        ,{
            type:"masterDataKeyValue",
            id:bundles[0].Id,
            name:bundles[0].Name,
            description:bundles[0].Description
            
        });
       }else
           $.asShowMessage({template:"error", message: "You must select a bundle"});
    });
    var loadData = function (url) {
    return $.asAjaxTask({
        url: url
    });
}
    var fillDependencyDropDown = function(bundles){
         if(winAddOrEditFirstShow){
        winAddOrEditFirstShow=false;
        $drpCodeType.asDropdown({
                source: {
                    hierarchy:
                    {
                        type: 'flat',
                        keyDataField: { name: 'MasterDataKeyValue.Id' },
                        parentDataField: { name: 'MasterDataKeyValue.ParentId' },
                        removeChildLessParent: false
                    },                   
                    url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByOtherLanguageAndParentTypetId, [{ name: '@parentTypeId', value: 1028 },{name:'@lang',value:$.asLang}])
                    , displayDataField: 'Name'
                      , valueDataField: 'MasterDataKeyValue.TypeId',
                    orderby: 'MasterDataKeyValue.Order'
                }
                // , selectParents: true
            //  , parentMode: "uniq"
            
            });
        $drpCodes.asDropdown("init","Choose the type of code",{
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'MasterDataKeyValue.Id' },
            parentDataField: { name: 'MasterDataKeyValue.ParentId' },
            removeChildLessParent: false
        }
        , displayDataField: 'Name'
          , valueDataField: 'MasterDataKeyValue.Id',
        orderby: 'MasterDataKeyValue.Order'
    }
    , selectParents: true


        });
        $drpBundlesDependency.asDropdown("init","Select the code",{
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: false
        }
        , displayDataField: 'PathOrUrl'
          , valueDataField: 'Id',
        orderby: 'PathOrUrl'
    }


        });
        $drpDependency.asDropdown("init"," ",{
            source: {
                 displayDataField: 'path'
                  , valueDataField: 'id',
                orderby: 'path'
            }
             , multiple: true
        });
        }
        if(bundles && isNew === false){
         $divDependency.asAfterTasks([
           
                loadData($.asInitService($.asUrls.develop_code_browser_getBundledependency, [{ name: '@bundleId', value: bundles[0].Id }]))
            ], function (dependency) {
              
                var selected = [];
                       $.each(dependency,function(i,v){
                           selected.push(v.id);
                        });
                
             
                    $drpDependency.asDropdown({
                source: {
                   localData:dependency
                    , displayDataField: 'path'
                      , valueDataField: 'id',
                    orderby: 'path'
                }
                , multiple: true
            });
                  $drpDependency.asDropdown('selectValue', selected);
            }, { overlayClass: 'as-overlay-absolute-no-height' });
       
        }
    }
    $btnEdit.click(function () {
            
    var bundles = getSelectedBundles();
       if (bundles.length === 1)  {
            isNew=false;
            

            setBundleWin({
            Code:bundles[0].Code,
            PathOrUrl:bundles[0].PathOrUrl,
            Name:bundles[0].Name,
            Description:bundles[0].Description,
            IsCdn:bundles[0].Key === 1,
            OneByOneBundle :bundles[0].Value === 1,
            RowVersion:bundles[0].RowVersion,
           ViewRoleId:asPageParams.viewRoleId,
           AccessRoleId:asPageParams.accessRoleId,
          ModifyRoleId:asPageParams.modifyRoleId,
            });
            
            $winAddOrEdit.asModal("show");
            fillDependencyDropDown(bundles);
          }else
           $.asShowMessage({template:"error", message: "You must select a bundle"});
           
        });
    $btnNew.click(function () {
           
            isNew=true;
            setBundleWin(null);
            $drpDependency.asDropdown('selectValue', [], true)
            $winAddOrEdit.asModal("show");
            fillDependencyDropDown();
           
        });
        $chkOneByOneBundle.change(function () {
            if(this.checked)
                $divDependencyPanel.hide()
            else
                $divDependencyPanel.show();
         });
        as("#btnExecute").click(function(){
             
            if($chkOneByOneBundle.is(':checked') && $chkCdn.is(':checked')){
                $.asShowMessage({template:"error", message: "A single bundle can not be used in dependencies"});
                return;
            }
                
             var bundles = getSelectedBundles();
          
             $winAddOrEdit.asAjax({
                    url: $.asUrls.develop_code_browser_bundle_save,
                    data: JSON.stringify({
                Id: isNew === true ? 0:bundles[0].Id,
                ParentId:asPageParams.codeId,
                TypeId:1033,
                ParentTypeId:"",
                IsPath:true,
                PathOrUrl: $txtPath.val(),
                SecondPathOrUrl:$txtCdnUrl.val(),
                ViewRoleId: $drpViewRole.asDropdown('selected').value,
                ModifyRoleId: $drpModifyRole.asDropdown('selected').value,
                AccessRoleId: $drpAccessRole.asDropdown('selected').value,
                Name: $txtName.val(),
                Description:$txtDescription.val(),
                Key:$chkCdn.is(':checked') ? 1:0,
                Value:$chkOneByOneBundle.is(':checked') ? 1:0,
                Status: true,
                EnableCache: false,
                EditMode: false,
                IsLeaf: true,
                IsType: false,
                IsNew:isNew,
                DependencyKey:$chkOneByOneBundle.is(':checked') ? "":$txtDependencyKey.val(),
                Dependency:$chkOneByOneBundle.is(':checked') ? []:getSelectedDependency(),
                RowVersion: isNew === true ? "":bundles[0].RowVersion,
                    }),
                    success: function (result) {
                        if( isNew===true){
                           
                            isNew=false;
                        }else{
                             
                               $grvBundles.asBootGrid("remove");
                        }
                         $grvBundles.asBootGrid("append",[result]);
                          
                      onSuccess();
                    }
                },{$form: $frmAddOrAupdate,overlayClass: "as-overlay-absolute"});
        });
        $btnAddDependency.click(function(){
            if ($drpBundlesDependency.asDropdown('selected')) {
             var isNew = true;
             var newDependency = $drpBundlesDependency.asDropdown('selected');
             var localData =[];
             var selected = [];
             if ($drpDependency.asDropdown('selected')) {
              $.each($drpDependency.asDropdown('selected'), function (i, v) {
                if (v.selected){
                    if(v.value === newDependency.value)
                        isNew=false;
                     selected.push(v.value)
                     localData.push({id:v.value,path:v.text});
                     }
                });
             }
             if(isNew){
                  localData.push({id:newDependency.value,path:newDependency.text});
                  selected.push(newDependency.value)
             }
                
             $drpDependency.asDropdown("reload",{localData:localData });

             $drpDependency.asDropdown('selectValue', selected)
            }else{
                    $.asShowMessage({template:"error", message: "You must select a bundle"});
            }
        });
       as("#btnCancel").click(function () {
            
       $winAddOrEdit.asModal('hide');
        });
        
        $btnSelectPath.click(function () {
         
         $winPathSelector.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.directorySelector)},{name:"@isModal",value:true}])
            ,{path:asPageParams.codeType === "js" ? $.asDebugBaceScriptUrl:$.asDebugBaceStyleUrl,parent:asPageEvent,event:"pathSelected"})
        });
        
        $btnManageSource.click(function () {
    
            var bundles = getSelectedBundles();
       if (bundles.length === 1)  {
            $winBundleSourceManager.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter("en/admin/develop/codes/browsers/bundle/sources")},{name:"@isModal",value:true}])
            ,
            {bundleId:bundles[0].Id,codePath:asPageParams.codePath,roles:asPageParams.roles,accessRoleId:asPageParams.accessRoleId,
            modifyRoleId:asPageParams.modifyRoleId,viewRoleId:asPageParams.viewRoleId,parent:asPageEvent})
          }else
           $.asShowMessage({template:"error", message: "You must select a bundle"});
    });
            $btnDell.click(function () {
    
            var bundles = getSelectedBundles();
       if (bundles.length === 1)  {
            $win.asAjax({
            url: $.asUrls.develop_code_browser_bundle_Delete,
            data: JSON.stringify({
                Id: bundles[0].Id
               
            }),
                error:function(){
                  $btnDell.button('reset')
            },
            success: function (result) {
                 $btnDell.button('reset')
                $grvBundles.asBootGrid("remove");
             onSuccess();
            }
        }, { validate:false ,overlayClass: "as-overlay-absolute"});
         $btnDell.button('loading') ;
          }else
           $.asShowMessage({template:"error", message: "You must select a bundle"});
    });
     $drpCodeType.on("change", function (event, item) {
     
      $drpCodes.asDropdown("reload",{url:$.asInitService($.asUrls.cms_masterDataKeyValue_GetByOtherLanguagesAndTypeId, [{ name: '@typeId', value: item.value },{name:'@lang',value:$.asLang}]) });
    });
    
    $drpCodes.on("change", function (event, item) {                
      $drpBundlesDependency.asDropdown("reload",{url:$.asInitService( $.asUrls.develop_code_browser_GetBundlesOfCodeExceptOneByOneBundle, [{ name: '@codeId', value: item.value }]) });
    });
    
    $chkCdn.change(function () {
        if(this.checked === true)
       {
           $divCdn.show();
       }else{
            $divCdn.hide();
       }
    });

    
     $btnCompile.click(function () {
      var bundles = getSelectedBundles();
       if (bundles.length === 1)  {
         $win.asAjax({
            url: $.asUrls.develop_code_browser_bundle_compile,
            data: JSON.stringify({
                Id: bundles[0].Id,
                IsPublish:$chkPublishBundle.is(':checked')
            }),
            success: function (result) {
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
            }
        }, {validate:false,overlayClass: "as-overlay-absolute"});
        }else{
               $.asShowMessage({template:"error", message: "You must select a bundle"});
        }
    });
     asOnPageDispose = function(){
          $grvBundles.asBootGrid("destroy");
          validate.destroy();
        }
            
 }
 bindEvent()