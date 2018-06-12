 $('#i514af161e6a34278b3b1aa019cb654d8').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('مدیریت مسیرها');var asPageEvent = '#i514af161e6a34278b3b1aa019cb654d8'; var asPageId = '.i514af161e6a34278b3b1aa019cb654d8.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId:"/odata/cms/MasterDataKeyValues?$filter=TypeId%20eq%20@typeIdd&$select=Id%2CParentId%2CCode%2CPathOrUrl%2COrder%2CName%2CIsLeaf%2CKey%2CValue",security_Role_getAllByDefaultsLanguage:"/odata/security/Roles?$select=Id%2CParentId%2CName%2COrder%2CIsLeaf%2CDescription",cms_filePath_GetByDefaultsLanguagesAndTypeCodeAndPathByPaging:"/odata/cms/FilePaths?$filter=(TypeCode%20eq%20@typeCoded)%20and%20(startswith(Url%2C'@path')%20eq%20true)&$orderby=@orderby&$skip=@skip&$top=@top&$select=Id%2CName%2CUrl%2CStatus%2CSize&$inlinecount=allpages",cms_filePath_save:"/cms/filepath/Save",cms_filePath_getById:"/odata/cms/FilePaths?$filter=Id%20eq%20@idd&$select=Id%2CName%2CDescription%2CUrl%2CGuid%2CStatus%2CSize%2CViewRoleId%2CTypeCode%2CAccessRoleId%2CModifyRoleId%2CRowVersion",cms_filePath_delete:"/cms/filepath/delete"}, $.asUrls); var
    $frm = as("#frm"),
    $winFileSelector=$.asModalManager.get({url:$.asModalManager.urls.fileSelector}),
    $winAddOrEdit=as("#winAddOrEdit"),
    $winTranslator=$.asModalManager.get({url:$.asModalManager.urls.translator}),
    $drpFileType= as("#drpFileType"),
    $drpPath= as("#drpPath"),
    $txtName=as("#txtName"),
    $txtPath=as("#txtPath"),
    $txtDescription=as("#txtDescription"),
    $txtGuId=as("#txtGuId"),
    $grvFile=as("#grvFile"),
   $btnNew=as("#btnNew"),
   $btnTranslator=as("#btnTranslator"),
   $btnEdit=as("#btnEdit"),
     $btnSelectFile=as("#btnSelectFile"),
     $btnDell=as("#btnDell"),
     $btnExecute=as("#btnExecute"),
     $chkStatus=as("#chkStatus"),
     $divRoles=as("#divRoles"),
     $divGuid=as("#divGuid"),
    $drpViewRole= as("#drpViewRole"),
    $drpModifyRole= as("#drpModifyRole"),
    $drpAccessRole= as("#drpAccessRole"),
    $drpFileTypeAddUpdate=as("#drpFileTypeAddUpdate"),
     selectedItems={
         hasFolder:function(){
            var result = false
             $.each(this.items,function(i,v){
               
                 if(v === true){
                       result= true;
                 }
               
             })
             return result;
         },
         path:"",
         items:{}
     },
     selectedId=0,
     rowVersion="",
      template =
        '<div class="as-material-switch container-fluid @class"><div><input name="chkStatusGrid" type="checkbox" @status /><label class="label-default as-label" for="chkStatusGrid" >  </label></div></div>',
     validate;
     var onSuccess = function(){
           $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
              selectedItems.items={};
              $grvFile.asBootGrid("deselect");
     }
     var validateRule = {
        drpFileTypeAddUpdate: {
            asType: 'asDropdown',
            required: true
        },
         drpViewRole: {
            asType: 'asDropdown',
            required: true
        },
        drpAccessRole: {
            asType: 'asDropdown',
            required: true
        },drpModifyRole: {
            asType: 'asDropdown',
            required: true
        },
         txtName:{
            required: true
        },
         txtPath:{
            required: true
        },
        txtGuId:{
            required: {
                depends: function (element) {
                return $(element).is(":visible");
            }
            }
        }
    };
    
    validate =$frm.asValidate({ rules: validateRule});
    
       $winTranslator.asModal({width:800});
     $winAddOrEdit.asModal(
    {backdrop:'static', keyboard: false}
    );
     $winFileSelector.asModal({width:800});
     
       var loadFileType = function () {
    return $.asAjaxTask({
        url:  $.asInitService($.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId, [{ name: '@typeId', value: 1009 }])
    });
}
        as("#divFileType").asAfterTasks([
    loadFileType()
], function (fileTypes) {
            $drpFileType.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: false
        },
       localData:fileTypes
        , displayDataField: 'Name'
          , valueDataField: 'Code',
        orderby: 'Order'
    }
    , selectParents: true
  , parentMode: "uniq"

});
        $drpFileTypeAddUpdate.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: false
        },
       localData:fileTypes
        , displayDataField: 'Name'
          , valueDataField: 'Code',
        orderby: 'Order'
    }
    , selectParents: true
  , parentMode: "uniq"

});
}, { overlayClass: 'as-overlay-relative' });
     
     

  $drpPath.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: false
        },
        url:$.asInitService($.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId, [{ name: '@typeId', value: 1025 }])
        , displayDataField: 'Name'
          , valueDataField: 'PathOrUrl',
        orderby: 'Order'
    }
    , selectParents: true
  , parentMode: "uniq"

});


var loadRoles = function () {
    return $.asAjaxTask({
        url: $.asUrls.security_Role_getAllByDefaultsLanguage
    });
}

$divRoles.asAfterTasks([
    loadRoles()
], function (roles) {

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
            localData: roles
            //url: '/cms/AllMenus'
            //url: 'Security/Access'
        }
        
    })

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
            localData: roles
            //url: '/cms/AllMenus'
            //url: 'Security/Access'
        }
       
    })

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
            localData: roles
            //url: '/cms/AllMenus'
            //url: 'Security/Access'
        }
    
    })

}, { overlayClass: 'as-overlay-relative' });
 
    var reloadGrid = function(){
               $grvFile.asBootGrid("reload");
    }

    var calculateSelectedFileAndFolder = function(event, rows){

        if(event.type==="selected"){
            selectedItems.items[rows[0].Name]=rows[0].Id
        }else{
            delete selectedItems.items[rows[0].Name]
        }
    }
     var bindEvent = function(){
         var notFound = function(){
         $.asNotFound(" مسیر")
        }
        var setFilePath = function(data){
            var filePath=data[0]
            selectedId=filePath.Id;
            if(filePath.Url !== null)
            $txtPath.val(filePath.Url);
            else
            $txtPath.val("");
            
            
            if(filePath.Name !== null)
            $txtName.val(filePath.Name);
            else
            $txtName.val("");
            
            if(filePath.Description !== null)
            $txtDescription.val(filePath.Description);
            else
            $txtDescription.val("");
            
            if(filePath.Guid !== null)
            $txtGuId.val(filePath.Guid);
            else
            $txtGuId.val("");
            
            $chkStatus.prop('checked', filePath.Status);
            
             if (filePath.TypeCode !== null)
                $drpFileTypeAddUpdate.asDropdown('selectValue', filePath.TypeCode);
            else
                $drpFileTypeAddUpdate.asDropdown('selectValue', [], true);
                
                
            if (filePath.ViewRoleId !== null)
                $drpViewRole.asDropdown('selectValue', filePath.ViewRoleId)
            else
                $drpViewRole.asDropdown('selectValue', [], true)
            if (filePath.AccessRoleId !== null)
                $drpAccessRole.asDropdown('selectValue', filePath.AccessRoleId)
            else
                $drpAccessRole.asDropdown('selectValue', [], true)
            if (filePath.ModifyRoleId !== null)
                $drpModifyRole.asDropdown('selectValue', filePath.ModifyRoleId)
            else
                $drpModifyRole.asDropdown('selectValue', [], true)
                
            if(filePath.RowVersion !== null)
            rowVersion=filePath.RowVersion;
            else
            rowVersion="";
                
        };
            $(asPageEvent).on("fileSelected",function(event,selectedFile,selectedId){
                $txtPath.val(selectedFile.replace("~",""));
            });
              $(asPageEvent).on($.asEvent.page.ready, function (event) {
           
                $grvFile.asBootGrid({
    rowCount:[10,25,50,100],
    source:{
        url:''
    },
    requestHandler:function(request){
         if ($drpPath.asDropdown('selected') && $drpFileType.asDropdown('selected')) {
        
             selectedItems.items={};
             selectedItems.path="";
             var pathUrl= $drpPath.asDropdown('selected').value.replace("~","");
        selectedItems.path=pathUrl;
        var orderbyValue = "Name desc"
        var skip = 0
        if(request.current > 1)
        skip=(request.current - 1) * request.rowCount
        $.each(request.sort, function(key, value) {
                 orderbyValue = key + " " + value
                });
        request.url = $.asInitService($.asUrls.cms_filePath_GetByDefaultsLanguagesAndTypeCodeAndPathByPaging, [
            { name: '@path', value: pathUrl }
            , { name: '@typeCode', value: $drpFileType.asDropdown('selected').value }
            ,{ name: '@orderby', value: orderbyValue }
            ,{ name: '@skip', value: skip }
             ,{ name: '@top', value: request.rowCount}]);
                  selectedItems.items={};
              $grvFile.asBootGrid("deselect");
             return request
         }
         return null;
    },
    formatters: {
        Status: function (column, row)
        {
            return row.Status ? template.replace('@status','checked="checked"').replace('@class','status'):template.replace('@status','').replace('@class','');
        }
    },
        selection: true,
        rowSelect:true,
        multiSelect:true
}).on("selected.rs.jquery.asBootGrid", function(e, rows)
{
    calculateSelectedFileAndFolder(e,rows)
    
}).on("deselected.rs.jquery.asBootGrid", function(e, rows)
{
    calculateSelectedFileAndFolder(e,rows)
});
            
        });
        
            $drpPath.on("change", function (event, item) {
            $grvFile.asBootGrid("reload");
        });
        
         $drpFileType.on("change", function (event, item) {
            $grvFile.asBootGrid("reload");
        });
        
     $btnSelectFile.click(function () {
          if ($drpPath.asDropdown('selected')) {
            $winFileSelector.asModal('load',$.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.fileSelector)},{name:"@isModal",value:true}])
            ,{path:$drpPath.asDropdown('selected').value,parent:asPageEvent,event:"fileSelected"})
          }else{
                 $.asShowMessage({template:"error", message: "هیچ مسیری انتخاب نشده است"});
          }
        });
        
        $btnNew.click(function () {
            $divGuid.hide();
            setFilePath([{
                Id:0,
                Guid:null,
                Name:null,
                Description:null,
                Url:null,
                Status:false,
                ViewRoleId:null,
                ModifyRoleId:null,
                AccessRoleId:null,
                TypeCode:null,
                RowVersion:null
            }]);
            $winAddOrEdit.asModal("show");
          
        });
        
        $btnDell.click(function () {
         var files=[]
              $.each(selectedItems.items,function(i,v){
                files.push(v)
            });
            
          if (files.length === 1) {
                
            $btnDell.asAjax({
            url:$.asUrls.cms_filePath_delete,
            data: JSON.stringify({
               Id:files[0]
            }),
            success: function (result) {
                $btnDell.button('reset')
                $grvFile.asBootGrid("remove");
             onSuccess()
            },error:function(){
                $btnDell.button('reset')
            }
        },{validate: false,overlayClass: "as-overlay-absolute"});
         $btnDell.button('loading');
            }else{
                       $.asShowMessage({template:"error", message: "مسیری برای حذف انتخاب نشده است."});
            }
        
        });
        
             $btnEdit.click(function () {
                 
                 var files=[]
              $.each(selectedItems.items,function(i,v){
                files.push(v)
            });
            
          if (files.length === 1) {
              $divGuid.show();
            $winAddOrEdit.asModal("show");
              $winAddOrEdit.asAjax({
        url:$.asInitService($.asUrls.cms_filePath_getById, [
            { name: '@id', value: files[0] }]) ,
        type: "get",
        success: function (filePath) {
            if($.isArray(filePath)){
                  if(filePath.length > 0)
                    setFilePath(filePath)
                    else
                     notFound()
            }else{
                 if(filePath != null){
                        if(typeof(filePath) != "undefined")
                      setFilePath(filePath)
                        else
                     notFound()
                 } else
                     notFound()
            }
        }
    },{overlayClass: "as-overlay-absolute"});
          }else{
                 $.asShowMessage({template:"error", message: "هیچ مسیری انتخاب نشده است"});
          }
        });
    
        
        as("#btnCancel").click(function () {
            
       $winAddOrEdit.asModal('hide');
        });
        
            $btnTranslator.click(function () {
                                 
                 var files=[]
              $.each(selectedItems.items,function(i,v){
                files.push(v)
            });
            
          if (files.length === 1) {
        $winTranslator.asModal('load',$.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.translator)},{name:"@isModal",value:true}])
        ,{
            type:"filePath",
            id:files[0],
            name:$txtName.val(),
            description:$txtDescription.val()
            
        });
          }else{
                 $.asShowMessage({template:"error", message: "هیچ مسیری انتخاب نشده است"});
          }
    });
        
        $btnExecute.click(function () {
            
         $winAddOrEdit.asAjax({
            url: $.asUrls.cms_filePath_save,
            data: JSON.stringify({
                Id:selectedId,
                Url: $txtPath.val(),
                Guid:$txtGuId.val(),
                Name:$txtName.val(),
                Description:$txtDescription.val(),
                TypeCode:$drpFileTypeAddUpdate.asDropdown('selected').value,
                Status:$chkStatus.is(':checked'),
                ViewRoleId: $drpViewRole.asDropdown('selected').value,
                ModifyRoleId: $drpModifyRole.asDropdown('selected').value,
                AccessRoleId: $drpAccessRole.asDropdown('selected').value,
                RowVersion:rowVersion
            }),
            success: function (result) {
                if(selectedId!==0){
                     $grvFile.asBootGrid("remove");
                }
                $grvFile.asBootGrid("append",[result])
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
            }
        },{$form: $frm,overlayClass: "as-overlay-absolute"});
              
        });

        asOnPageDispose = function(){
          $grvFile.asBootGrid("destroy");
           validate.destroy();
        }
     }
     bindEvent();  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });