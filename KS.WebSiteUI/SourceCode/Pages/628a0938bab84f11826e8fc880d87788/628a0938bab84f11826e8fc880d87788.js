var 
    $frm = as("#frmCode"),
    $divRoles = as("#divRoles"),
    $edrCode= as("#edrCode"),
    $drpViewRole= as("#drpViewRole"),
    $drpModifyRole= as("#drpModifyRole"),
    $drpAccessRole= as("#drpAccessRole"),
    $drpCodes= as("#drpCodes"),
    $txtName= as("#txtName"),
    $btnSave= as("#btnSave"),
    $btnSaveCode=as("#btnSaveCode"),
    $btnSelectFile=as("#btnSelectFile"),
    $btnSelectDir=as("#btnSelectDir"),
    $chkEditMode= as("#chkEditeMode"),
    $winFind=as("#divFind"),
    $winTranslator= $.asModalManager.get({url:$.asModalManager.urls.translator}),
    $winFolderSelector=$.asModalManager.get({url:$.asModalManager.urls.directorySelector}),
    $winCodeManager=$.asModalManager.get({url:$.asModalManager.urls.fileManager}),
    $winFileSelector=$.asModalManager.get({url:$.asModalManager.urls.fileSelector}),
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
    $drpConnection=as("#drpConnection"),
    $btnExecuteQuery = as("#btnExecuteQuery"),
    $btnExecuteNoneQuery=as("#btnExecuteNoneQuery"),
    $btnExec=as("#btnExec"),
    $txtComment=as("#txtComment"),
    $winSourceManager=$.asModalManager.get({url:$.asModalManager.urls.sourceManager}),
    $winSourceComparator=$.asModalManager.get({url:$.asModalManager.urls.sourceComparator}),
    selectedTextOfCode,
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
    typeId= 1040,
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
    selectedConnectionId=0,
    interval=null,
    temp = {},
    selectedcode={},
    validate,
    newParents={},
    rolesList,
    isFirstGrid=true,
    resultGrids=[],
    tableTemplate = '<table id="@id" class="table table-condensed table-hover table-striped table-responsive">'+
    '<thead><tr>'+
       
    '</tr></thead>'+
        '<tbody>'+
       
    '</tbody>'+
'</table>';



$divUrl.hide();
$divEditor.hide();
$winSourceComparator.asModal({width:1200}); 
$winSourceManager.asModal({width:800});   
  $winCodeManager.asModal({width:800});
 $winFolderSelector.asModal({width:800});
  $winFileSelector.asModal({width:800}) ;
     $winTranslator.asModal({width:800});
$winFind.asWindow({focusedId:"txtFind"});
$winRestore.asModal(
    {backdrop:'static', keyboard: false}
    );

$edrCode.asCodeEditor({ mode: 'sqlserver' });

var loadData = function (url) {
    return $.asAjaxTask({
        url: url
    });
}

$divRoles.asAfterTasks([
    loadData($.asUrls.security_Role_getAllByDefaultsLanguage)
], function (roles) {
    rolesList=roles;
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



$drpConnection.asDropdown({
    source: {
        url: $.asUrls.develop_code_database_sqlserver_connections
        , displayDataField: 'Value'
          , valueDataField: 'Key',
        orderby: 'Value'
    }
    // , selectParents: true
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
        }
        ,url:$.asInitService($.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId, [{ name: '@typeId', value: 1040 }])
        , displayDataField: 'Name'
          , valueDataField: 'Id',
        orderby: 'Order'
    }
    , selectParents: true

});


//  $frm.asValidate("validator").addMethod(
//         "regex",
//         function(value, element, regexp) {
//             var re = new RegExp(regexp);
//             return this.optional(element) || re.test(value);
//         },
//         "شناسه می تواند شامل اعدا حروف و . و _ و - باشد و با . یا - یا _ شروع نمی شود"
// );


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
            maxlength:32
            // regex: "^[A-Za-z0-9][A-Za-z0-9_\\-\\.]*$" 
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
    var initRecovery = function(){
        queryString = $.asGetQueryString();
        if(queryString !== null){
        $.asTemp[queryString] = $.asTemp[queryString] || {};
        $.asTemp[queryString].sqlServeCodeQueryEditor = $.asTemp[queryString].sqlServeCodeQueryEditor || "";

        if($.asTemp[queryString].sqlServeCodeQueryEditor !== "")
                $.asStorage.setItem("sqlServeCodeQueryEditor" + queryString,$.asTemp[queryString].sqlServeCodeQueryEditor);
        }
    }
        $(asPageEvent).on($.asEvent.page.ready, function (event) {
              $btnManageCode.prop('disabled', true);
            $edrCode.asCodeEditor("focus");
            $winRestore.asModal("show");
              $drpCodes.css({"margin-top":"0"});
    });
    
     $(asPageEvent).on($.asEvent.page.queryStringChange, function (event,pageUrl,queryString) {
           initRecovery();
          if(interval === null)
          interval = setInterval(autoSave, 5000);
            loadQueryString()
    });
    
       $(asPageEvent).on("folderSelected",function(event,selectedFolder,selectedId){
                $txtUrl.val(selectedFolder)
            });
            
        var loadWinComparator = function(sourceControlCode,fileName){
                 
         
                      
            $winSourceComparator.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.sourceComparator)},{name:"@isModal",value:true}])
                                                                                  
            ,{editorCode:$edrCode.asCodeEditor("getValue"),sourceControlCode:sourceControlCode,fileName:fileName});
         
         
    }
    
    $(asPageEvent).on("compare",function(event,selectedId){
        $divEditor.asAjax({
            url: $.asInitService($.asUrls.develop_code_database_sqlserver_getChange,[
                {name:"@changeId",value:selectedId}
                ,{name:"@path",value:selectedCodeFile.replace(/\//g, $.asUrlDelimeter)}
                ,{name:"@codeId",value:selectedCodeId}
                ]),
            type:"get",
            success: function (sql) {
                 loadWinComparator(sql,selectedCodeName);
            }
        }, { validate:false, overlayClass: 'as-overlay-absolute'});
     });
             $(asPageEvent).on("changeSetSelected",function(event,selectedId){

        $divEditor.asAjax({
            url: $.asInitService($.asUrls.develop_code_database_sqlserver_getChange,[
                {name:"@changeId",value:selectedId}
                ,{name:"@path",value:selectedCodeFile.replace(/\//g, $.asUrlDelimeter)}
                ,{name:"@codeId",value:selectedCodeId}
                ]),
            type:"get",
            success: function (sql) {
                 $edrCode.asCodeEditor('setValue', sql);
             
            }
        }, { validate:false, overlayClass: 'as-overlay-absolute'});
    });
            
            
        as("#btnSourceControl").click(function () {
             if(selectedCodeName && selectedCodeFile){
              $winSourceManager.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.sourceManager)},{name:"@isModal",value:true}])
            ,{parent:asPageEvent,selectEvent:"changeSetSelected",compareEvent:"compare",getUrl: $.asInitService($.asUrls.develop_code_database_sqlserver_getChanges, [
             { name: '@codeName', value: selectedCodeName}
             ,{ name: '@codePath', value: selectedCodeFile.replace(selectedCodeName,"").replace(/\//g, $.asUrlDelimeter)}])});
             }else{
                 $.asShowMessage({template:"error", message: "هیچ فایلی انتخاب نشده است "});
             }
    });
    
    $btnManageCode.click(function () {
          if ($drpCodes.asDropdown('selected')) {
            $winCodeManager.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.fileManager)},{name:"@isModal",value:true}])
            ,{path:parentPath})
          }else{
                 $.asShowMessage({template:"error", message: "هیچ کدی انتخاب نشده است"});
          }
          

    });


    as("#btnEditorResize").click(function () {
        resize();
    })
    
var executeQuery = function(){
         if(selectedConnectionId===0){
            $.asShowMessage({ template: "error", message: " برای اجرا یک ارتباط را انتخاب نمایید" })
        }else{
            if($edrCode.asCodeEditor("getValue"))
               $btnExec.button('loading'); 
           $divEditor.asAjax({
            url: $.asUrls.develop_code_database_sqlserver_exec,
            data: JSON.stringify({
                ConnectionId: selectedConnectionId,
                Code:selectedTextOfCode !== "" ? selectedTextOfCode : $edrCode.asCodeEditor("getValue"),
                IsQuery: true
            }),
            success: function (result) {
           if(!isFirstGrid){
                $.each(resultGrids,function(i,v){ 
                     var $grvData =as('#' + v);
                     $grvData.asBootGrid('destroy');
                     
                 });
                 resultGrids=[];
           }
           as("#divResult").empty();
             $.each(result,function(i,v){ 
                 isFirstGrid=false;
                     gridfyResult(v);
                     
                 });
                
              
                
                
                
                
                
                
                
                  $btnExec.button('reset'); 
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration,showTime:10000000  });
            },
            error:function(){
                 $btnExec.button('reset')
            }
        }, {validate:false , overlayClass: 'as-overlay-absolute'});
        }
    }
        var executeNoneQuery = function(){
         if(selectedConnectionId===0){
            $.asShowMessage({ template: "error", message: " برای اجرا یک ارتباط را انتخاب نمایید" })
        }else{
            if($edrCode.asCodeEditor("getValue"))
               $btnExec.button('loading'); 
           $divEditor.asAjax({
            url: $.asUrls.develop_code_database_sqlserver_exec,
            data: JSON.stringify({
                ConnectionId: selectedConnectionId,
                Code:selectedTextOfCode !== "" ? selectedTextOfCode : $edrCode.asCodeEditor("getValue"),
                IsQuery: false
            }),
            success: function (result) {
                as("#divResult").empty("")
                as("#divResult").html("سطرهای تحت تاثیر : "+result[0]);
                  $btnExec.button('reset'); 
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration,showTime:10000000  });
            },
            error:function(){
                 $btnExec.button('reset')
            }
        }, {validate:false , overlayClass: 'as-overlay-absolute'});
        }
}

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
                 currentEditor.asCodeEditor('setValue',$.asStorage.getItem("sqlServeCodeQueryEditor" + queryString));
          }
    }
    
     var recoverAllEditor = function(){
          queryString = $.asGetQueryString();
          if(queryString !== null){
              $divEditor.show();
              $edrCode.asCodeEditor('setValue',$.asStorage.getItem("sqlServeCodeQueryEditor" + queryString));
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

 $edrCode.asCodeEditor("editor").commands.addCommand({
    name: 'executeQuery',
    bindKey: {win: 'Ctrl-E',  mac: 'Command-E'},
    exec: function(editor) {
        executeQuery();
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
        
        $edrCode.asCodeEditor("editor").getSession().selection.on('changeSelection', function(e) {
            selectedTextOfCode = $edrCode.asCodeEditor("editor").session.getTextRange($edrCode.asCodeEditor("editor").getSelectionRange());
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
                 $.asShowMessage({ template: "error", message: "  امکان افزودن یک کد به کد دیگر وجود ندارد" })
             $chkNew.prop('checked', false)
         }
            else{
            as("#divResult").empty();
            $txtId.prop("disabled", false);

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
            $txtName.val("");
       
            temp.Description = $txtDescription.val();
            $txtDescription.val("");
            
         
            temp.Code = $txtCode.val();
            $txtCode.val("");
            
            
            temp.Url = $txtUrl.val();
            $txtUrl.val(parentPath+"/"+codeNewGuid);
            
            temp.Guid = $txtId.val();
            
            temp.NewGuid=codeNewGuid;
            $txtId.val(codeNewGuid);
            
           
            temp.NewId = codeNewId;
            $txtCodeId.val(codeNewId);
            
            temp.Version = $txtVersion.val();
            $txtVersion.val("0");
            
 
            temp.Order = $txtOrder.val();
            $txtOrder.val("");
            
            temp.EnableCache = $chkCache.prop('checked');
            $chkCache.prop('checked', false);
             temp.EditMode = $chkEditMode.prop('checked');
            $chkEditMode.prop('checked', false);
            temp.Status = $chkStatus.prop('checked');
            $chkStatus.prop('checked', false);

          
                temp.ViewRoleId=viewRoleId;
                $drpViewRole.asDropdown('selectValue', [], true);
                
          
            temp.AccessRoleId=accessRoleId;
                $drpAccessRole.asDropdown('selectValue', [], true);
       
            temp.ModifyRoleId=modifyRoleId;
                $drpModifyRole.asDropdown('selectValue', [], true);
            
                
                
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
    });
    $btnDell.click(function () {
        if ($drpCodes.asDropdown('selected')) {
         $frm.asAjax({
            url:$.asUrls.develop_code_database_sqlserver_delete ,
            data: JSON.stringify({
                Id: selectedCodeId,
            }),
            success: function (result) {
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration ,showTime:10000000 });
              $drpCodes.asDropdown("removeItem");
            }
        }, { $form: $frm });
        }else{
                 $.asShowMessage({ template: "error", message: "برای حذف باید یک کد را انتخاب نمایید" })
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

        // $.asTemp[queryString].sqlServeCodeQueryEditor = jsCode;
   

        $frm.asAjax({
            url:$.asUrls.develop_code_database_sqlserver_save ,
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
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration,showTime:10000000 });
            }
        }, { $form: $frm })

    });
    
    var gridfyResult = function(result){
        var id = $.asuniqueId();
            resultGrids.push(id);
            as('#divResult').append(tableTemplate.replace('@id',id));
            var $grvData= as('#' + id);
        if(result){
            var index=0;
            $.each(result[0],function(i,v){ 
            $grvData.find('thead tr').append('<th data-header-align=\"center\" data-align = \"center\" data-column-id=\"' + i + '\" data-order=\"desc\" data-css-class=\"ltr\" >' + i + '</th>');
            index++;
            });
        };
         if(result){
             var dataArr = [];
             for (i = 0; i < result.length; i++) {
                 var obj ={};
                  $.each(result[i],function(k,v){ 
                        obj[k]= (v === null ? "" : v[0]);
                  });
                
                 dataArr.push(obj);
                 
             };
            
            $grvData.asBootGrid({caseSensitive:false,rowCount:[-1],source:{localData:dataArr}});
         }
    }
    
    $btnExecuteQuery.click(function () {
        executeQuery();
    });
    $btnExecuteNoneQuery.click(function () {
        executeNoneQuery();
    });

      $drpConnection.on("change", function (event, item) {
          selectedConnectionId = item.value;
      });
    $drpCodes.on("change", function (event, item) {
         selectedcode = item;
        selectedCodeId=item.value;
    //   if(selectedCodeId){
    //       getCode(selectedCodeId);
    //   }
        $btnTranslator.removeClass("disabled")
             if (isLoadQueryString === false) {

    
                if (typeof (item.value) != "undefined") {
                    $.asSetQueryString(item.value)
                  
                }

            }
         

    });

       $(asPageEvent).on("fileSelected",function(event,selectedFile,selectedId,selectedFileName){
           selectedCodeName=selectedFileName;
           selectedCodeFile=selectedFile;
           var extention = selectedFileName.toLowerCase().split(".");
          
          $divEditor.asAjax({
            url: $.asInitService($.asUrls.develop_code_database_sqlserver_getCodeContent,[{name:"@id",value:selectedCodeId},{name:"@path",value:$.asUrlAsParameter(selectedFile)}]),
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
            $btnSaveCode.click(function () {
                     $divEditor.asAjax({
            url: $.asUrls.develop_code_database_sqlserver_file_save,
            data: JSON.stringify({
                Id: selectedCodeId,
                Code:$edrCode.asCodeEditor("getValue"),
                Path:selectedCodeFile,
                CheckIn: $chkCheckIn.is(':checked'),
                Comment:$txtComment.val()
            }),
            success: function (result) {
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration,showTime:10000000 });
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
        url:$.asInitService($.asUrls.develop_code_database_sqlserver_get, [{ name: '@id', value: id }]),
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
        $.asStorage.setItem("sqlServeCodeQueryEditor" + queryString,$edrCode.asCodeEditor("getValue"))
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
        $drpCodes.asDropdown('selectValue',queryString)
         selectedCodeId =queryString;
         getCode(selectedCodeId);
    }
}


