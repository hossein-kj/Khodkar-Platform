 $('#i92008639798c453b88bdeaa78f399472').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('کدهای تحت مرورگر');var asPageEvent = '#i92008639798c453b88bdeaa78f399472'; var asPageId = '.i92008639798c453b88bdeaa78f399472.' + $.asPageClass; var as = function(id){ return $(asPageId).find(id);};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId:"/odata/cms/MasterDataKeyValues?$filter=TypeId%20eq%20@typeIdd&$select=Id%2CParentId%2CCode%2CUrl%2COrder%2CName%2CIsLeaf%2CKey%2CValue",security_Role_getAllByDefaultsLanguage:"/odata/security/Roles?$select=Id%2CParentId%2CName%2COrder%2CIsLeaf%2CDescription",cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeIdAndParentId:"/odata/cms/MasterDataKeyValues?$filter=(TypeId%20eq%20@typeIdd)%20and%20(ParentId%20eq%20@idd)&$select=Id%2CParentId%2CCode%2COrder%2CName",develop_code_browser_save:"/develop/code/browser/save",develop_code_browser_delete:"/develop/code/browser/delete",develop_code_browser_get:"/develop/code/browser/get/@id",develop_code_browser_compile:"/develop/code/browser/compile"}, $.asUrls); var 
    $frm = as("#frmCode"),
    $divRoles = as("#divRoles"),
    $edrCode= as("#edrCode"),
    $drpViewRole= as("#drpViewRole"),
    $drpModifyRole= as("#drpModifyRole"),
    $drpAccessRole= as("#drpAccessRole"),
    $drpCodes= as("#drpCodes"),
    $drpEditors=as("#drpEditors"),
    $txtName= as("#txtName"),
    $btnSave= as("#btnSave"),
    $btnSelectDir=as("#btnSelectDir"),
    $chkEditMode= as("#chkEditeMode"),
    $winFind=as("#divFind"),
    $winTranslator= as("#winTranslator"),
    $winFolderSelector=as("#winFolderSelector"),
    $winCodeManager=as("#winCodeManager"),
    $txtFind=as("#txtFind"),
    $txtReplace=as("#txtReplace"),
    $chkCase=as("#chkFindCase"),
    $chkHole=as("#chkFindHole"),
    $chkExp=as("#chkFindExp"),
    $chkSelect=as("#chkFindSelect"),
    $editorTools=as("#editorToolbar"),
    $lblEditor=as("#lblEditor"),
    $txtDescription=as("#txtDescription"),
    $txtOrder=as("#txtOrder"),
    $winRestore=as("#winRestorEditor"),
    $txtId=as("#txtId"),
    $txtCodeId=as("#txtCodeId"),
    $txtUrl=as("#txtUrl"),
    $chkIsLeaf = as("#chkIsLeaf"),
    $chkNew = as("#chkNew"),
    $txtCode=as("#txtCode"),
    $chkCache= as("#chkCache"),
    $chkStatus= as("#chkStatus"),
    $chkDependency=as("#chkDependency"),
    $chkPublish=as("#chkPublish"),
    $txtVersion = as("#txtVersion"),
    $txtSlidingExpirationTimeInMinutes = as("#txtSlidingExpirationTimeInMinutes"),
    $btnCompile = as("#btnCompile"),
    $btnDell=as("#btnDell"),
    $btnTranslator = as("#btnTranslator"),
    $btnManageCode=as("#btnManageCode"),
    $divUrl=as("#divUrl"),
    $divSelectDir=as(".divSelectDir"),
    status=false,
    parentPath="",
    isType=false,
    parentTypeId=null,
    viewRoleId= 0,
    modifyRoleId= 0,
    accessRoleId= 0,
    typeId= 0,
    url= "",
   codeId= 0,
    codeParentId= 0,
    isLeaf=false,
    rowVersion= "",
    codeNewGuid,
    codeNewId,
    queryString= null,
    isLoadQueryString = false,
   
    currentEditor=null,
    interval,
    temp = {},
    selectedcode={},
    validate,
    newParents={};



$divUrl.hide();


$.asTemp.codeJavascriptQueryEditor = $.asTemp.codeJavascriptQueryEditor || "";

             if($.asTemp.codeJavascriptQueryEditor !== "")
                $.asStorage.setItem("codeJavascriptQueryEditor",$.asTemp.codeJavascriptQueryEditor);
  $winCodeManager.asModal({width:800})   
 $winFolderSelector.asModal({width:800});
     $winTranslator.asModal({width:800})           
$winFind.asWindow({focusedId:"txtFind"})
$winRestore.asModal(
    {backdrop:'static', keyboard: false}
    )

$edrCode.asCodeEditor();







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
         , parentMode: "uniq"
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
        , parentMode: "uniq"
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
       , parentMode: "uniq"
    })

}, { overlayClass: 'as-overlay-relative' });

$drpEditors.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: false
        },
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeIdAndParentId, [{ name: '@typeId', value: 1030 },{ name: '@id', value: 199 }])
        , displayDataField: 'Name'
          , valueDataField: 'Code',
        orderby: 'Order'
    }
    , selectParents: true
//  , parentMode: "uniq"

});
$drpCodes.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: false
        },
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId, [{ name: '@typeId', value: 1028 }])
        , displayDataField: 'Name'
          , valueDataField: 'Id',
        orderby: 'Order'
    }
    , selectParents: true
//  , parentMode: "uniq"

});


 $frm.asValidate("validator").addMethod(
        "regex",
        function(value, element, regexp) {
            var re = new RegExp(regexp);
            return this.optional(element) || re.test(value);
        },
        "شناسه می تواند شامل اعدا حروف و . و _ و - باشد و با . یا - یا _ شروع نمی شود"
);


var validateRule = {
        // drpModifyRole: {
        // asType: 'asDropdown',
        //   required: true
        // },
         txtCode:{
            required: true
        },
        txtCodeId:{
            required: true
        },
        txtName: {
            required: true,
            maxlength:128
        },
        txtDescription:{
            maxlength:256
        },
        txtId:{
            maxlength:32,
            regex: "^[A-Za-z0-9][A-Za-z0-9_\\-\\.]*$" 
        },
        drpViewRole: {
            asType: 'asDropdown',
            required: true
        },
        drpAccessRole: {
            asType: 'asDropdown',
            required: true
        }, drpModifyRole: {
        asType: 'asDropdown',
          required: true
        }

    }
       validate =$frm.asValidate({ rules: validateRule});



var bindEvent =function () {
    
        $(asPageEvent).on($.asEvent.page.ready, function (event) {
              $btnManageCode.prop('disabled', true);
            $edrCode.asCodeEditor("focus");
            $winRestore.asModal("show");
            $drpEditors.asDropdown('selectValue', "javascript")
    });
    
     $(asPageEvent).on($.asEvent.page.queryStringChange, function (event,pageUrl,queryString) {
            loadQueryString()
    });
    
       $(asPageEvent).on("folderSelected",function(event,selectedFolder,selectedId){
                $txtUrl.val(selectedFolder)
            })
            
    $btnManageCode.click(function () {
          if ($drpCodes.asDropdown('selected')) {
            $winCodeManager.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter("fa/fms/files-folders-manager-for-a-path/")},{name:"@isModal",value:true}])
            ,{path:parentPath})
          }else{
                 $.asShowMessage({template:"error", message: "هیچ کدی انتخاب نشده است"});
          }
          

    });

    as("#btnEditorResize").click(function () {
        resize();
    })

    var commentToggel = function(){
         currentEditor.asCodeEditor("toggleCommentLines");
    }
    var find = function(){ 
        currentEditor.asCodeEditor("find", $txtFind.val(),{ backwards: false,
    wrap: false,
    range:$chkSelect.is(':checked') === true ?  currentEditor.asCodeEditor("getSelectionRange"):null,
    caseSensitive: $chkCase.is(':checked'),
    wholeWord: $chkHole.is(':checked'),
    regExp: $chkExp.is(':checked')});

    }
    var resize = function(){
          as('#' + currentEditor.prop('id') + "_container").toggleClass('editor-fullscreen');

        currentEditor.toggleClass('editor-fullHeight');
        currentEditor.asCodeEditor('resize');
    }
         var recoverEditor = function(){
        if( $edrCode === currentEditor)
        currentEditor.asCodeEditor('setValue',$.asStorage.getItem("codeJavascriptQueryEditor"));
    }
    
     var recoverAllEditor = function(){
        $edrCode.asCodeEditor('setValue',$.asStorage.getItem("codeJavascriptQueryEditor"));
    }
   
         $edrCode.asCodeEditor("editor").commands.addCommand({
    name: 'Find',
    bindKey: {win: 'Ctrl-F',  mac: 'Command-F'},
    exec: function(editor) {
       $winFind.asWindow("show")
    },
    readOnly: true // false if this command should not apply in readOnly mode
});



    $edrCode.asCodeEditor("editor").commands.addCommand({
    name: 'CommentToggel',
    bindKey: {win: 'Ctrl-K',  mac: 'Command-K'},
    exec: function(editor) {
       commentToggel()
    },
    readOnly: false // false if this command should not apply in readOnly mode
});


 $edrCode.asCodeEditor("editor").commands.addCommand({
    name: 'fullScreen',
    bindKey: {win: 'Ctrl-L',  mac: 'Command-L'},
    exec: function(editor) {
        resize();
    },
    readOnly: false // false if this command should not apply in readOnly mode
});


    
   $edrCode.asCodeEditor("editor").on("focus", function(){
        currentEditor =$edrCode
       changeEditorToolbar("کوئری")
    });
 
   $chkCache.change(function () {
        as("#divSlidingExpirationTimeInMinutes").prop("disabled", !this.checked)
    });
    
       $chkIsLeaf.change(function () {
        if(this.checked === false)
       {
         $divSelectDir.show();
         $txtUrl.prop('disabled', false);
         $btnManageCode.prop('disabled', true);
       }else{
            $divSelectDir.hide();
            $txtUrl.prop('disabled', true);
            $btnManageCode.prop('disabled', false);
       }
           
       });
      $chkNew.change(function () {
        if(this.checked === true)
       {
            if(isLeaf === true)
         {
                 $.asShowMessage({ template: "error", message: "  امکان افزودن یک کد به کد دیگر وجود ندارد" })
             $chkNew.prop('checked', false)
         }
            else{
            
            $txtId.prop("disabled", false)

            temp.Id = codeId;
            
            codeId=0;
            
            temp.Isleaf = isLeaf
            
            temp.ParentId = codeParentId;
            codeParentId=0;
            
            temp.RowVersion = rowVersion;
            rowVersion='';

    
              
            temp.JsCode = $edrCode.asCodeEditor('getValue');
            $edrCode.asCodeEditor('setValue', '');
            
            temp.Name = $txtName.val();
            $txtName.val("")
       
            temp.Description = $txtDescription.val()
            $txtDescription.val("")
            
         
            temp.Code = $txtCode.val()
            $txtCode.val("")
            
            
            temp.Url = $txtUrl.val();
            $txtUrl.val(parentPath+"/"+codeNewGuid);
            
            temp.Guid = $txtId.val()
            
            temp.NewGuid=codeNewGuid;
            $txtId.val(codeNewGuid)
            
           
            temp.NewId = codeNewId
            $txtCodeId.val(codeNewId)
            
            temp.Version = $txtVersion.val()
            $txtVersion.val("0")
            
 
            temp.Order = $txtOrder.val()
            $txtOrder.val("")
            
            temp.EnableCache = $chkCache.prop('checked')
            $chkCache.prop('checked', false)
             temp.EditMode = $chkEditMode.prop('checked')
            $chkEditMode.prop('checked', false)
            temp.Status = $chkStatus.prop('checked')
            $chkStatus.prop('checked', false)

          
                temp.ViewRoleId=viewRoleId
                $drpViewRole.asDropdown('selectValue', [], true)
                
          
            temp.AccessRoleId=accessRoleId
                $drpAccessRole.asDropdown('selectValue', [], true)
       
            temp.ModifyRoleId=modifyRoleId
                $drpModifyRole.asDropdown('selectValue', [], true)
                
            }
       }else{
           setCode(temp)
       }
    });
    
    $btnSelectDir.click(function(){
                  if ($drpCodes.asDropdown('selected')) {
            $winFolderSelector.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter("fa/fms/folder-selector/")},{name:"@isModal",value:true}])
            ,
            {path:parentPath,parent:asPageEvent,event:"folderSelected"})
          }else
           $.asShowMessage({template:"error", message: "کدی انتخاب نشده است"});
    })
    
    as("#btnFindWindow").click(function () {
      $winFind.asWindow("show")
    })
    
    
        $txtFind.on("input",function(){
 
          find()
    });
    
    $chkCase.change(function () {
        find()

    })
      $chkHole.change(function () {
       find()

    })
      $chkExp.change(function () {
       find()

    })
    $chkSelect.change(function () {
       find()

    })
    
    as("#btnRecovery").click(function () {
        recoverEditor()
    })
as("#btnReplace").click(function () {
       currentEditor.asCodeEditor("replace",$txtReplace.val());
    })
    as("#btnReplaceAll").click(function () {
       currentEditor.asCodeEditor("replaceAll",$txtReplace.val());
    })
    as("#btnToggleComment").click(function () {
     commentToggel()
    })
as("#btnFindNext").click(function () {
       currentEditor.asCodeEditor("findNext");
    })
    
    as("#btnFindPrev").click(function () {
       currentEditor.asCodeEditor("findPrevious");

    })
as("#btnCancelRestor").click(function () {
       $winRestore.asModal('hide');
        loadQueryString()
        interval = setInterval(autoSave, 5000);
    })
    as("#btnRestorPerviousEditors").click(function () {
       $winRestore.asModal('hide');
         recoverAllEditor();
    })
    $btnCompile.click(function () {
     if ($drpCodes.asDropdown('selected')) {
         if(!status)
             $.asShowMessage({ template: "error", message: " کد غیر فعال می باشد و فقط به حالت دیباگ منتشر می شود" })
         $frm.asAjax({
            url: $.asUrls.develop_code_browser_compile,
            data: JSON.stringify({
                Id: $drpCodes.asDropdown('selected').value,
                IsPublish:$chkPublish.is(':checked'),
                Dependency:$chkDependency.is(':checked'),
            }),
            success: function (result) {
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
            }
        }, {validate:false});
        }else{
                 $.asShowMessage({ template: "error", message: "برای حذف یک سرویس را انتخاب نمایید" })
        }
    });
    
    $btnDell.click(function () {
        if ($drpCodes.asDropdown('selected')) {
         $frm.asAjax({
            url: $.asUrls.develop_code_browser_delete,
            data: JSON.stringify({
                Id: $drpCodes.asDropdown('selected').value,
            }),
            success: function (result) {
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
              $drpCodes.asDropdown("removeItem");
            }
        }, { $form: $frm });
        }else{
                 $.asShowMessage({ template: "error", message: "برای حذف یک سرویس را انتخاب نمایید" })
        }
    });
    $btnSave.click(function () {
        
               
        var newcode;
  
         
        //  $drpCodes.asDropdown("removeItem");
         
         var id,parentId,guid;
          if ($drpCodes.asDropdown('selected')) {
              if($chkNew.is(':checked'))
           {
             guid= codeNewGuid
            parentId = $drpCodes.asDropdown('selected').value
            id=codeNewId
           }
            else{
                guid= $txtId.val()
                id=$drpCodes.asDropdown('selected').value
                parentId=codeParentId
            }
            
        }
        
          
    
                      
   

         
            
        
     
        if($chkIsLeaf.is(':checked') === false && $chkNew.is(':checked')){
            newParents[id]=true
        }
            
        if ($drpViewRole.asDropdown('selected')) {
            viewRoleId = $drpViewRole.asDropdown('selected').value
        }
        if ($drpModifyRole.asDropdown('selected')) {
            modifyRoleId = $drpModifyRole.asDropdown('selected').value
        }
        if ($drpAccessRole.asDropdown('selected')) {
            accessRoleId = $drpAccessRole.asDropdown('selected').value
        }

 

        var jsCode = $edrCode.asCodeEditor("getValue")

        $.asTemp.codeJavascriptQueryEditor = jsCode;
   

        $frm.asAjax({
            url: $.asUrls.develop_code_browser_save,
            data: JSON.stringify({
                Id: id,
                TypeId:1028,
                ParentId:parentId,
                Url: $txtUrl.val(),
                ViewRoleId: viewRoleId,
                ModifyRoleId: modifyRoleId,
                AccessRoleId: accessRoleId,
                Code:$txtCode.val(),
                JsCode: jsCode,
                Name: $txtName.val(),
                Description:$txtDescription.val(),
                Guid:guid,
                Order: $txtOrder.val(),
                Status: $chkStatus.is(':checked'),
                EnableCache: $chkCache.is(':checked'),
                EditMode: $chkEditMode.is(':checked'),
                IsLeaf: $chkIsLeaf.is(':checked'),
                IsType:isType,
                ParentTypeId:parentTypeId,
                isNew:$chkNew.is(':checked'),
                BuildJs:$edrCode.asCodeEditor('getValue'),
                SlidingExpirationTimeInMinutes:$txtSlidingExpirationTimeInMinutes.val(),
                RowVersion: rowVersion
            }),
            success: function (code) {
                if($chkNew.is(':checked')){
                    var newParent = false
                        if(newParents[selectedcode.value]){
                            newParent = true
                            delete newParents[selectedcode.value]
                        }
                       $drpCodes.asDropdown("addItem",{text:$txtName.val(),value:id},selectedcode,newParent)
                }
                 
                setCode(code,false)
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
            }
        }, { $form: $frm })

    });

    
    $drpCodes.on("change", function (event, item) {
    
         selectedcode = item;
        $btnTranslator.removeClass("disabled")
             if (isLoadQueryString === false) {

    
                if (typeof (item.value) != "undefined") {
                    $.asSetQueryString(item.value)
                  
                }

            }


    });
     $drpEditors.on("change", function (event, item) {
         $edrCode.asCodeEditor("setEditorMode",item.value);
     });
        
       $btnTranslator.click(function () {
        $winTranslator.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter("fa/cms/languageandculture-manager/data-translator/")},{name:"@isModal",value:true}])
        ,{
            type:"masterDataKeyValue",
            id:codeId,
            name:$txtName.val(),
            description:$txtDescription.val()
            
        })
    });

    
        asOnPageDispose = function(){
        window.clearInterval(interval);
        validate.destroy();
    }
}
bindEvent();
var notFound = function(){
 $.asNotFound("سرویس")
}

var getCode = function (id) {

    isLoadQueryString = true;


                $drpCodes.asDropdown('selectValue', [], true)
                $drpCodes.asDropdown('selectValue', id)

            

    viewRoleId = 0
    url = ""
    ser = 0
    rowVersion = ""



    $frm.asAjax({
        url:$.asInitService($.asUrls.develop_code_browser_get, [{ name: '@id', value: id }]),
        type: "get",
        error:function(){
             isLoadQueryString = false;
        },
        success: function (code) {
           
             if($.isArray(code)){
                  if(code.length > 0)
                    setCode(code)
                    else
                     notFound()
            }else{
                 if(code != null){
                        if(typeof(code) != "undefined")
                       setCode(code)
                        else
                     notFound()
                 } else
                     notFound()
            }
            isLoadQueryString = false;
        }
    });
    
    
}
var autoSave = function(){
        $.asStorage.setItem("codeJavascriptQueryEditor",$edrCode.asCodeEditor("getValue"))
    }
var changeEditorToolbar = function(editor){
        
        
 
           $lblEditor.html(editor)
     
    
          
    }
    
var setCode = function(code,changeEditor){
            if(typeof(changeEditor) == "undefined")
                 changeEditor=true;
           $txtId.prop("disabled", true)
              $chkNew.prop('checked', false)
            codeId = code.Id
            
            $txtCodeId.val(codeId)
            codeParentId=code.ParentId
            rowVersion = code.RowVersion
            $chkIsLeaf.prop('checked', code.IsLeaf)
            if(code.IsLeaf)
            $btnManageCode.prop('disabled', false);
            else
            $btnManageCode.prop('disabled', true);
            isLeaf = code.IsLeaf;
            isType = code.IsType;
            parentTypeId=code.ParentTypeId
            $txtName.val(code.Name)
            if (code.Description != null)
            $txtDescription.val(code.Description)
            else
            $txtDescription.val("")
            if (code.Code !== null)
            $txtCode.val(code.Code)
            else
            $txtCode.val("")
            if (code.Url !== null){
            parentPath=code.Url;
            $txtUrl.val(code.Url);
            }
            else{
            parentPath="";
            $txtUrl.val("");
            }
            $txtId.val(code.Guid)
            codeNewGuid = code.NewGuid;
            codeNewId = code.NewId;
            $txtVersion.val(code.Version)
   
            if (code.Order !== null)
            $txtOrder.val(code.Order)
            else
            $txtOrder.val("")
 
            $chkCache.prop('checked', code.EnableCache)
            $chkEditMode.prop('checked', code.EditMode)
            $chkStatus.prop('checked', code.Status);
            status= code.Status;
            if(changeEditor){
                if (code.BuildJs != null)
                    $edrCode.asCodeEditor('setValue',code.BuildJs);
                else
                    $edrCode.asCodeEditor('setValue', '');
            }


            if (code.ViewRoleId !== null)
                $drpViewRole.asDropdown('selectValue', code.ViewRoleId)
            else
                $drpViewRole.asDropdown('selectValue', [], true)
            if (code.AccessRoleId != null)
                $drpAccessRole.asDropdown('selectValue', code.AccessRoleId)
            else
                $drpAccessRole.asDropdown('selectValue', [], true)
            if (code.ModifyRoleId != null)
                $drpModifyRole.asDropdown('selectValue', code.ModifyRoleId)
            else
                $drpModifyRole.asDropdown('selectValue', [], true)
}
var loadQueryString = function () {
    queryString = $.asGetQueryString()
    if (queryString !== null) {
        var q = queryString.split("/")


        getCode(q[0]);




    }
}


 ; if (typeof (requestedUrl) != 'undefined')  {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });