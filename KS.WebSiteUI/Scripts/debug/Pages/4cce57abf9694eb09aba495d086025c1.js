﻿ $('#i4cce57abf9694eb09aba495d086025c1').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('Browser Codes Management');var asPageEvent = '#i4cce57abf9694eb09aba495d086025c1'; var asPageId = '.i4cce57abf9694eb09aba495d086025c1.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({cms_masterDataKeyValue_GetByOtherLanguagesAndTypeId:"/odata/cms/MasterDataLocalKeyValues?$filter=(MasterDataKeyValue%2FTypeId%20eq%20@typeIdd)%20and%20(Language%20eq%20'@lang')&$expand=MasterDataKeyValue&$select=MasterDataKeyValue%2FId%2CMasterDataKeyValue%2FParentId%2CMasterDataKeyValue%2FCode%2CMasterDataKeyValue%2FOrder%2CMasterDataKeyValue%2FName%2CMasterDataKeyValue%2FKey%2CMasterDataKeyValue%2FValue%2CMasterDataKeyValue%2FIsLeaf%2CMasterDataKeyValue%2FPathOrUrl%2CName",security_Role_getAllByOtherLanguage:"/odata/security/LocalRoles?$filter=(Language%20eq%20'@lang')&$expand=Role&$select=Role%2FId%2CRole%2FParentId%2CRole%2FName%2CRole%2FOrder%2CRole%2FIsLeaf%2CName",develop_code_browser_save:"/develop/code/browser/save",develop_code_browser_delete:"/develop/code/browser/delete",develop_code_browser_get:"/develop/code/browser/get/@id",develop_code_browser_getCodeContent:"/develop/code/browser/GetCodeContent/@path/@id",develop_code_browser_file_save:"/develop/code/browser/file/save",develop_code_browser_checkJavascriptCode:"/develop/code/browser/CheckJavascriptCode",develop_code_browser_getChanges:"/develop/code/browser/GetChanges/@codePath/@codeName/@orderBy/@skip/@take/@comment/@user/@fromDateTime/@toDateTime",develop_code_browser_getChange:"/develop/code/browser/GetChange/@changeId/@path/@codeId",cms_masterDataKeyValue_GetByOtherLanguageAndTypeIdAndParentId:"/odata/cms/MasterDataLocalKeyValues?$filter=((MasterDataKeyValue%2FTypeId%20eq%20@typeIdd)%20and%20(MasterDataKeyValue%2FParentId%20eq%20@idd))%20and%20(Language%20eq%20'@lang')&$expand=MasterDataKeyValue&$select=MasterDataKeyValue%2FId%2CMasterDataKeyValue%2FParentId%2CMasterDataKeyValue%2FCode%2CMasterDataKeyValue%2FOrder%2CName",cms_masterDataKeyValue_GetByOtherLanguageAndParentTypetId:"/odata/cms/MasterDataLocalKeyValues?$filter=(MasterDataKeyValue%2FParentTypeId%20eq%20@parentTypeIdd)%20and%20(Language%20eq%20'@lang')&$expand=MasterDataKeyValue&$select=MasterDataKeyValue%2FId%2CMasterDataKeyValue%2FParentId%2CMasterDataKeyValue%2FCode%2CMasterDataKeyValue%2FOrder%2CName%2CMasterDataKeyValue%2FTypeId"}, $.asUrls); var 
    $frm = as("#frmCode"),
    $divRoles = as("#divRoles"),
    $edrCode= as("#edrCode"),
    $drpViewRole= as("#drpViewRole"),
    $drpModifyRole= as("#drpModifyRole"),
    $drpAccessRole= as("#drpAccessRole"),
    $drpCodes= as("#drpCodes"),
    $drpCodeType=as("#drpCodeType"),
    $drpEditors=as("#drpEditors"),
    $txtName= as("#txtName"),
    $btnSave= as("#btnSave"),
    $btnSaveCode=as("#btnSaveCode"),
    $btnSelectFile=as("#btnSelectFile"),
    $btnManageBundle=as("#btnManageBundle"),
    $btnSelectDir=as("#btnSelectDir"),
    $chkEditMode= as("#chkEditeMode"),
    $winFind=as("#divFind"),
    $winTranslator= $.asModalManager.get({url:$.asModalManager.urls.translator}),
    $winFolderSelector=$.asModalManager.get({url:$.asModalManager.urls.directorySelector}),
    $winCodeManager=$.asModalManager.get({url:$.asModalManager.urls.fileManager}),
    $winFileSelector=$.asModalManager.get({url:$.asModalManager.urls.fileSelector}),
    $winBundleManager=as("#winBundleManager"),
    $txtFind=as("#txtFind"),
    $txtReplace=as("#txtReplace"),
    $chkCase=as("#chkFindCase"),
    $chkWhole=as("#chkFindWhole"),
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
    $chkCheckIn=as("#chkCheckIn"),
    $txtVersion = as("#txtVersion"),
    $txtSlidingExpirationTimeInMinutes = as("#txtSlidingExpirationTimeInMinutes"),
    $btnNext=as("#btnNext"),
    $btnPrev=as("#btnPrev"),
    $btnDell=as("#btnDell"),
    $btnTranslator = as("#btnTranslator"),
    $btnManageCode=as("#btnManageCode"),
    $divUrl=as("#divUrl"),
    $divSelectDir=as(".divSelectDir"),
    $divEditor=as("#divEditor"),
    $btnCheckJavaScriptCode=as("#btnCheckJavaScriptCode"),
    $txtComment=as("#txtComment"),
    $winSourceManager=$.asModalManager.get({url:$.asModalManager.urls.sourceManager}),
    $winSourceComparator=$.asModalManager.get({url:$.asModalManager.urls.sourceComparator}),
    selectedCodeFile,
    selectedCodeName,
    codeType,
    status=false,
    parentPath="",
    isType=false,
    parentTypeId=null,
    viewRoleId= 0,
    modifyRoleId= 0,
    accessRoleId= 0,
    typeId= 0,
    codeParentId= 0,
    isLeaf=false,
    rowVersion= "",
    codeNewGuid,
    codeNewId,
    queryString= null,
    isLoadQueryString = false,
    isFirstLoad=true,

    backOrForwrdFlag=false,
    codeEditorPositionIndex=0,
    codeEditorPosition =[],
    currentEditor=null,
    selectedCodeId,
    interval=null,
    temp = {},
    selectedcode={},
    validate,
    newParents={},
    rolesList;



$divUrl.hide();
$divEditor.hide();

$winSourceComparator.asModal({width:1200}); 
$winSourceManager.asModal({width:800}); 
  $winCodeManager.asModal({width:800});
 $winFolderSelector.asModal({width:800});
  $winFileSelector.asModal({width:800}) ;
     $winTranslator.asModal({width:800});
     $winBundleManager.asModal({width:800});
$winFind.asWindow({focusedId:"txtFind"})
$winRestore.asModal(
    {backdrop:'static', keyboard: false}
    )

$edrCode.asCodeEditor();







var loadData = function (url) {
    return $.asAjaxTask({
        url: url
    });
}

$divRoles.asAfterTasks([
    loadData($.asInitService($.asUrls.security_Role_getAllByOtherLanguage, [{ name: '@lang', value: $.asLang }]))
], function (roles) {
    rolesList=roles;
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
            localData: roles
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
            localData: roles
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
            localData: roles
        }

    })

}, { overlayClass: 'as-overlay-relative' });

$drpEditors.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'MasterDataKeyValue.Id' },
            parentDataField: { name: 'MasterDataKeyValue.ParentId' },
            removeChildLessParent: false
        },
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByOtherLanguageAndTypeIdAndParentId, [{ name: '@typeId', value: 1030 },{ name: '@id', value: 199 },{ name: '@lang', value: $.asLang }])
        , displayDataField: 'Name'
          , valueDataField: 'MasterDataKeyValue.Code',
        orderby: 'MasterDataKeyValue.Order'
    }
    , selectParents: true
//  , parentMode: "uniq"

});


$drpCodeType.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'MasterDataKeyValue.Id' },
            parentDataField: { name: 'MasterDataKeyValue.ParentId' },
            removeChildLessParent: false
        },
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByOtherLanguageAndParentTypetId, [{ name: '@parentTypeId', value: 1028 },{ name: '@lang', value: $.asLang }])
        , displayDataField: 'Name'
          , valueDataField: 'MasterDataKeyValue.TypeId',
        orderby: 'MasterDataKeyValue.Order'
    }
    // , selectParents: true
//  , parentMode: "uniq"

});
$drpCodes.asDropdown("init","Select Code Type",{
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


 $frm.asValidate("validator").addMethod(
        "regex",
        function(value, element, regexp) {
            var re = new RegExp(regexp);
            return this.optional(element) || re.test(value);
        },
     "The id can contain letters and numbers and _ and - and does not start with . or - or _ "
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


var loadCodeTypes = function(codeTypeId,typeChanged){
         
            if(codeTypeId === "1035")
            codeType="js";
            else
            codeType="css";
         
            if(isFirstLoad || typeChanged){
                isFirstLoad=false;
                            as("#divCode").asAfterTasks([
                loadData($.asInitService($.asUrls.cms_masterDataKeyValue_GetByOtherLanguagesAndTypeId, [{ name: '@typeId', value: codeTypeId },{name:'@lang',value:$.asLang}]))
            ], function (codes) {
                        $drpCodes.asDropdown("reload",{localData:codes });
                     $drpCodes.css({"margin-top":"0"});
                     if(selectedCodeId){
                       $drpCodes.asDropdown('selectValue', selectedCodeId)
                       getCode(selectedCodeId);
                     }
            }, { overlayClass: 'as-overlay-absolute-no-height' });
            }else{
                if(selectedCodeId){
                       getCode(selectedCodeId);
                    }
            }

}
var bindEvent =function () {
    
    var initRecovery = function(){
         queryString = $.asGetQueryString();
        if(queryString !== null){
            $.asTemp[queryString] = $.asTemp[queryString] || {};
            $.asTemp[queryString].codeJavascriptQueryEditor = $.asTemp[queryString].codeJavascriptQueryEditor || "";
    
            if($.asTemp[queryString].codeJavascriptQueryEditor !== "")
                    $.asStorage.setItem("codeJavascriptQueryEditor" + queryString,$.asTemp[queryString].codeJavascriptQueryEditor);
        }
    }
    
        $(asPageEvent).on($.asEvent.page.ready, function (event) {
              $btnManageCode.prop('disabled', true);
            $edrCode.asCodeEditor("focus");
            $winRestore.asModal("show");
            $drpEditors.asDropdown('selectValue', "javascript");
        $btnCheckJavaScriptCode.prop('disabled', false);
    });
    
     $(asPageEvent).on($.asEvent.page.queryStringChange, function (event,pageUrl,queryString) {
           initRecovery();
          if(interval === null)
          interval = setInterval(autoSave, 5000);
            loadQueryString();
    });
    
       $(asPageEvent).on("folderSelected",function(event,selectedFolder,selectedId){
                $txtUrl.val(selectedFolder)
            });
            
     $drpCodeType.on("change", function (event, item) {
              typeId=item.value;
      loadCodeTypes(typeId,true);
    });
    
            var loadWinComparator = function(sourceControlCode,fileName){
                 
         
                      
            $winSourceComparator.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.sourceComparator)},{name:"@isModal",value:true}])
                                                                                  
            ,{editorCode:$edrCode.asCodeEditor("getValue"),sourceControlCode:sourceControlCode,fileName:fileName});
         
         
    }
        $(asPageEvent).on("compare",function(event,selectedId){
        $divEditor.asAjax({
            url: $.asInitService($.asUrls.develop_code_browser_getChange,[
                {name:"@changeId",value:selectedId}
                ,{name:"@path",value:selectedCodeFile.replace(/\//g, $.asUrlDelimeter)}
                ,{name:"@codeId",value:selectedCodeId}
                ]),
            type:"get",
            success: function (brCode) {
                 loadWinComparator(brCode,selectedCodeName);
            }
        }, { validate:false, overlayClass: 'as-overlay-absolute'});
     });
             $(asPageEvent).on("changeSetSelected",function(event,selectedId){

        $divEditor.asAjax({
            url: $.asInitService($.asUrls.develop_code_browser_getChange,[
                {name:"@changeId",value:selectedId}
                ,{name:"@path",value:selectedCodeFile.replace(/\//g, $.asUrlDelimeter)}
                ,{name:"@codeId",value:selectedCodeId}
                ]),
            type:"get",
            success: function (brCode) {
                 $edrCode.asCodeEditor('setValue', brCode);
             
            }
        }, { validate:false, overlayClass: 'as-overlay-absolute'});
    });
            
    
       
   as("#btnSourceControl").click(function () {
             if(selectedCodeName && selectedCodeFile){
              $winSourceManager.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.sourceManager)},{name:"@isModal",value:true}])
            ,{parent:asPageEvent,selectEvent:"changeSetSelected",compareEvent:"compare",getUrl: $.asInitService($.asUrls.develop_code_browser_getChanges, [
             { name: '@codeName', value: selectedCodeName}
             ,{ name: '@codePath', value: selectedCodeFile.replace(selectedCodeName,"").replace(/\//g, $.asUrlDelimeter)}])});
             }else{
                 $.asShowMessage({template:"error", message: "no file selected"});
             }
    });
            
    $btnManageCode.click(function () {
          if ($drpCodes.asDropdown('selected')) {
            $winCodeManager.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.fileManager)},{name:"@isModal",value:true}])
            ,{path:parentPath,urlParentId:1044})
          }else{
                 $.asShowMessage({template:"error", message: "no code selected"});
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
    wholeWord: $chkWhole.is(':checked'),
    regExp: $chkExp.is(':checked')});

    }
    var resize = function(){
          as('#' + currentEditor.prop('id') + "_container").toggleClass('editor-fullscreen');

        currentEditor.toggleClass('editor-fullHeight');
        currentEditor.asCodeEditor('resize');
    }
         var recoverEditor = function(){
          queryString = $.asGetQueryString();
          if(queryString !== null){
               $divEditor.show();
                if( $edrCode === currentEditor)
                    currentEditor.asCodeEditor('setValue',$.asStorage.getItem("codeJavascriptQueryEditor" + queryString));
          }
    }
    
     var recoverAllEditor = function(){
          queryString = $.asGetQueryString();
          if(queryString !== null){
              $divEditor.show();
            $edrCode.asCodeEditor('setValue',$.asStorage.getItem("codeJavascriptQueryEditor" + queryString));
          }
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

 $btnPrev.click(function () {
              backOrForwrdFlag=true;

                    codeEditorPositionIndex = codeEditorPositionIndex === 0 ? codeEditorPosition.length -1:codeEditorPositionIndex;
                     $edrCode.asCodeEditor("editor").gotoLine(codeEditorPosition[codeEditorPositionIndex]);
                     if(codeEditorPositionIndex > 1)
                     codeEditorPositionIndex--;

        });
        $btnNext.click(function () {
                
          backOrForwrdFlag=true;

                codeEditorPositionIndex = codeEditorPositionIndex === 0 ? codeEditorPosition.length -1:codeEditorPositionIndex;
                 $edrCode.asCodeEditor("editor").gotoLine(codeEditorPosition[codeEditorPositionIndex]);
                 if(codeEditorPositionIndex < codeEditorPosition.length - 1)
                 codeEditorPositionIndex++;

                
        });
    
     $edrCode.asCodeEditor("editor").getSession().on('change', function(e) {
            codeEditorPositionIndex = codeEditorPosition.length -1
         });

   $edrCode.asCodeEditor("editor").on("focus", function(){
        currentEditor =$edrCode;
       changeEditorToolbar("کوئری");
       backOrForwrdFlag=false;
    });
    
     $edrCode.asCodeEditor("editor").getSession().selection
     .on('changeCursor', function(e) {
                codeEditorPosition.push($edrCode.asCodeEditor("editor").selection.getCursor().row);
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
         $divEditor.hide();
       }else{
            $divSelectDir.hide();
            $txtUrl.prop('disabled', true);
            $btnManageCode.prop('disabled', false);
            $divEditor.show();
       }
           
       });
      $chkNew.change(function () {
        if(this.checked === true)
       {
            if(isLeaf === true)
         {
                 $.asShowMessage({ template: "error", message: "You can not add a code to another code" })
             $chkNew.prop('checked', false)
         }
            else{
            
            $txtId.prop("disabled", false)

            temp.Id = selectedCodeId;
            
            selectedCodeId=0;
            
            temp.Isleaf = isLeaf
            temp.IsType =isType;
            isType=false;
            temp.ParentId = codeParentId;
            codeParentId=temp.Id;
            
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
            $winFolderSelector.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.directorySelector)},{name:"@isModal",value:true}])
            ,
            {path:parentPath,parent:asPageEvent,event:"folderSelected"})
          }else
           $.asShowMessage({template:"error", message: "No code selected"});
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
      $chkWhole.change(function () {
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
        queryString = $.asGetQueryString();
        if(queryString !== null){
                  initRecovery();
                interval = setInterval(autoSave, 5000);
        }
    })
    as("#btnRestorPerviousEditors").click(function () {
       $winRestore.asModal('hide');
         recoverAllEditor();
    })
   
    $btnManageBundle.click(function () {
      
       if (selectedCodeId) {
          
            $winBundleManager.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter("en/admin/develop/codes/browsers/bundle/")},{name:"@isModal",value:true}])
            ,
            {codeId:selectedCodeId,roles:rolesList,accessRoleId:accessRoleId,codePath:$txtUrl.val(),
            modifyRoleId:modifyRoleId,viewRoleId:viewRoleId,codeType:codeType,parent:asPageEvent})
          }else
           $.asShowMessage({template:"error", message: "No code selected"});
    });
    $btnDell.click(function () {
        if ($drpCodes.asDropdown('selected')) {
         $frm.asAjax({
            url: $.asUrls.develop_code_browser_delete,
            data: JSON.stringify({
                Id: selectedCodeId,
            }),
            success: function (result) {
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration ,showTime:10000000 });
              $drpCodes.asDropdown("removeItem");
            }
        }, { $form: $frm });
        }else{
                 $.asShowMessage({ template: "error", message: "Select a code to remove" })
        }
    });
    $btnSave.click(function () {
        
               
        var newcode;
  
         
        //  $drpCodes.asDropdown("removeItem");
         
         var id,guid;
          if ($drpCodes.asDropdown('selected')) {
              if($chkNew.is(':checked'))
           {
             guid= codeNewGuid
          
            id=codeNewId
           }
            else{
                guid= $txtId.val()
                id=selectedCodeId
         
            }
            
        }
        
          
    
                      
   

         
            
        
     
        if($chkIsLeaf.is(':checked') === false && $chkNew.is(':checked')){
            newParents[id]=true;
            $divEditor.show();
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

 

        // var jsCode = $edrCode.asCodeEditor("getValue")

        // $.asTemp[queryString].codeJavascriptQueryEditor = jsCode;
        
        
        console.log(viewRoleId);
         console.log(accessRoleId);
          console.log(modifyRoleId);
        modifyRoleId

        $frm.asAjax({
            url: $.asUrls.develop_code_browser_save,
            data: JSON.stringify({
                Id: id,
                TypeId:typeId,
                ParentId:codeParentId,
                PathOrUrl: $txtUrl.val(),
                IsPath:true,
                ViewRoleId: viewRoleId,
                ModifyRoleId: modifyRoleId,
                AccessRoleId: accessRoleId,
                Code:$txtCode.val(),
                Name: $txtName.val(),
                Description:$txtDescription.val(),
                Guid:guid,
                Order: $txtOrder.val(),
                Status: $chkStatus.is(':checked'),
                EnableCache: $chkCache.is(':checked'),
                EditMode: $chkEditMode.is(':checked'),
                IsLeaf: $chkIsLeaf.is(':checked'),
                IsType:isType,
                ParentTypeId:isType ? parentTypeId:null,
                IsNew:$chkNew.is(':checked'),
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
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration ,showTime:10000000 });
            }
        }, { $form: $frm })

    });

    
    $drpCodes.on("change", function (event, item) {
         selectedcode = item;
        $btnTranslator.removeClass("disabled");
        $btnTranslator.prop("disabled",false);
             if (isLoadQueryString === false) {

    
                if (typeof (item.value) != "undefined") {
                    $.asSetQueryString(item.value + "/" + typeId)
                  
                }

            }
         

    });
     $drpEditors.on("change", function (event, item) {
         if(item.value === "javascript")
         $btnCheckJavaScriptCode.prop('disabled', false);
         else
         $btnCheckJavaScriptCode.prop('disabled', true);
         $edrCode.asCodeEditor("setEditorMode",item.value);
     });
       $(asPageEvent).on("fileSelected",function(event,selectedFile,selectedId,selectedFileName){
           selectedCodeFile=selectedFile;
           selectedCodeName=selectedFileName;
           var extention = selectedFileName.toLowerCase().split(".");
             $btnCheckJavaScriptCode.prop('disabled', true);
           if(selectedFileName.toLowerCase().indexOf(".js") > -1){
               $drpEditors.asDropdown('selectValue', "javascript");
                $edrCode.asCodeEditor("setEditorMode","javascript");
                $btnCheckJavaScriptCode.prop('disabled', false);
           }else if(selectedFileName.toLowerCase().indexOf(".ts") > -1){
                  $edrCode.asCodeEditor("setEditorMode","typescript")
                  $drpEditors.asDropdown('selectValue', "typescript");
           }else{
               console.log(extention[extention.length -1]);
               $drpEditors.asDropdown('selectValue', extention[extention.length -1]);
                    $edrCode.asCodeEditor("setEditorMode",extention[extention.length -1]);
           }
          $divEditor.asAjax({
            url: $.asInitService($.asUrls.develop_code_browser_getCodeContent,[{name:"@id",value:selectedCodeId},{name:"@path",value:$.asUrlAsParameter(selectedFile)}]),
            type:"get",
            success: function (resultCode) {
             $edrCode.asCodeEditor('setValue',resultCode);
            }
        }, { validate:false, overlayClass: 'as-overlay-absolute'});
         });
        $btnSelectFile.click(function () {
                     $winFileSelector.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.fileSelector)},{name:"@isModal",value:true}])
            ,{path:parentPath,parent:asPageEvent,event:"fileSelected"})
        });
        $btnCheckJavaScriptCode.click(function () {
                     $divEditor.asAjax({
            url: $.asUrls.develop_code_browser_checkJavascriptCode,
            data: JSON.stringify({
                Id: selectedCodeId,
                Code:$edrCode.asCodeEditor("getValue")
            }),
            success: function (result) {
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration ,showTime:10000000 });
            }
        }, {validate:false , overlayClass: 'as-overlay-absolute'});
        });
            $btnSaveCode.click(function () {
                     $divEditor.asAjax({
            url: $.asUrls.develop_code_browser_file_save,
            data: JSON.stringify({
                Id: selectedCodeId,
                Code:$edrCode.asCodeEditor("getValue"),
                Path:selectedCodeFile,
                CheckIn: $chkCheckIn.is(':checked'),
                Comment:$txtComment.val()
            }),
            success: function (result) {
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration ,showTime:10000000 });
            }
        }, {validate:false , overlayClass: 'as-overlay-absolute'});
        });
       $btnTranslator.click(function () {
        $winTranslator.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.translator)},{name:"@isModal",value:true}])
        ,{
            type:"masterDataKeyValue",
            id:selectedCodeId,
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
 $.asNotFound("کد")
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
        $.asStorage.setItem("codeJavascriptQueryEditor" + queryString,$edrCode.asCodeEditor("getValue"))
    }
var changeEditorToolbar = function(editor){
        
        
 
           $lblEditor.html(editor)
     
    
          
    }
    
var setCode = function(code,changeEditor){
            if(typeof(changeEditor) == "undefined")
                 changeEditor=true;
           $txtId.prop("disabled", true)
              $chkNew.prop('checked', false)
            selectedCodeId = code.Id
             $txtComment.val("");
              $chkCheckIn.prop('checked', false);
            as("#divLastModifiUser").html(code.LastModifieUser);
            as("#divLastModifiLocalDataTime").html(code.LastModifieLocalDateTime);
            $txtCodeId.val(selectedCodeId)
            codeParentId=code.ParentId
            rowVersion = code.RowVersion
            $chkIsLeaf.prop('checked', code.IsLeaf)
            if(code.IsLeaf){
             $divEditor.show();
            $btnManageCode.prop('disabled', false);
            }
            else{
            $btnManageCode.prop('disabled', true);
            $divEditor.hide();
            }
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
            if (code.PathOrUrl !== null){
            parentPath=code.PathOrUrl;
            $txtUrl.val(code.PathOrUrl);
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


            if (code.ViewRoleId !== null){
                viewRoleId=code.ViewRoleId;
                $drpViewRole.asDropdown('selectValue', viewRoleId)
            }
            else{
                viewRoleId=[]
                 $drpViewRole.asDropdown('selectValue', viewRoleId, true)
            }
               
            if (code.AccessRoleId != null){
                accessRoleId=code.AccessRoleId;
                 $drpAccessRole.asDropdown('selectValue', accessRoleId)
            }
               
            else{
                accessRoleId=[];
                 $drpAccessRole.asDropdown('selectValue', accessRoleId, true)
            }
               
            if (code.ModifyRoleId != null){
                modifyRoleId=code.ModifyRoleId;
                $drpModifyRole.asDropdown('selectValue', modifyRoleId)
            }
                
            else{
                modifyRoleId=[];
                $drpModifyRole.asDropdown('selectValue', modifyRoleId, true)
            }
                
}
var loadQueryString = function () {
    queryString = $.asGetQueryString()
    if (queryString !== null) {
        var q = queryString.split("/")
            $drpCodeType.asDropdown('selectValue', q[1])
          selectedCodeId =q[0];
          typeId =q[1];
          loadCodeTypes(q[1]);
            




    }
}


  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });