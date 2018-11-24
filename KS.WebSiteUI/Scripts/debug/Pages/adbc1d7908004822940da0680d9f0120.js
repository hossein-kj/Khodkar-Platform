 $('#iadbc1d7908004822940da0680d9f0120').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('Assembly Management');var asPageEvent = '#iadbc1d7908004822940da0680d9f0120'; var asPageId = '.iadbc1d7908004822940da0680d9f0120.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({cms_masterDataKeyValue_GetByOtherLanguagesAndTypeId:"/odata/cms/MasterDataLocalKeyValues?$filter=(MasterDataKeyValue%2FTypeId%20eq%20@typeIdd)%20and%20(Language%20eq%20'@lang')&$expand=MasterDataKeyValue&$select=MasterDataKeyValue%2FId%2CMasterDataKeyValue%2FParentId%2CMasterDataKeyValue%2FCode%2CMasterDataKeyValue%2FOrder%2CMasterDataKeyValue%2FName%2CMasterDataKeyValue%2FKey%2CMasterDataKeyValue%2FValue%2CMasterDataKeyValue%2FIsLeaf%2CMasterDataKeyValue%2FPathOrUrl%2CName",security_Role_getAllByOtherLanguage:"/odata/security/LocalRoles?$filter=(Language%20eq%20'@lang')&$expand=Role&$select=Role%2FId%2CRole%2FParentId%2CRole%2FName%2CRole%2FOrder%2CRole%2FIsLeaf%2CName",develop_code_os_dotNet_getDll:"/odata/cms/MasterDataKeyValues?$filter=((((ForeignKey1%20eq%20null)%20or%20(ForeignKey1%20eq%20756d))%20or%20(ForeignKey1%20eq%20757d))%20or%20(ForeignKey1%20eq%20758d))%20and%20(TypeId%20eq%201041d)&$select=Id%2CParentId%2CPathOrUrl%2CName%2CCode%2COrder",develop_code_os_dotNet_get:"/develop/code/os/dotnet/Get/@id",develop_code_os_dotNet_save:"/develop/code/os/dotnet/Save",develop_code_os_dotNet_dell:"/develop/code/os/dotnet/delete",develop_code_os_dotNet_getChanges:"/develop/code/os/dotnet/GetChanges/@codeId/@orderBy/@skip/@take/@comment/@user/@fromDateTime/@toDateTime",develop_code_os_dotNet_getChange:"/develop/code/os/dotnet/GetChange/@changeId/@codeId",develop_code_os_dotNet_dllCompile:"/develop/code/os/dotnet/dllCcompile",develop_code_os_dotNet_getOutputVersions:"/develop/code/os/dotnet/GetOutputVersions/@codeId",develop_code_os_dotNet_debugCode:"/develop/code/os/dotnet/debugCode",cms_masterDataKeyValue_GetByOtherLanguageAndTypeIdAndParentId:"/odata/cms/MasterDataLocalKeyValues?$filter=((MasterDataKeyValue%2FTypeId%20eq%20@typeIdd)%20and%20(MasterDataKeyValue%2FParentId%20eq%20@idd))%20and%20(Language%20eq%20'@lang')&$expand=MasterDataKeyValue&$select=MasterDataKeyValue%2FId%2CMasterDataKeyValue%2FParentId%2CMasterDataKeyValue%2FCode%2CMasterDataKeyValue%2FOrder%2CName"}, $.asUrls); var 
    $frm = as("#frmDotNet"),
    $divRoles = as("#divRoles"),
    $edrDotNet= as("#edrDotNet"),
    $drpViewRole= as("#drpViewRole"),
    $drpModifyRole= as("#drpModifyRole"),
    $drpAccessRole= as("#drpAccessRole"),
    $drpCodes= as("#drpCodes"),
    $drpDependency=as("#drpDependency"),
    $txtName= as("#txtName"),
    $btnSave= as("#btnSave"),
    $chkEditMode= as("#chkEditeMode"),
    $winFind=as("#divFind"),
    $winTranslator= $.asModalManager.get({url:$.asModalManager.urls.translator}),
    $winCodeManager=$.asModalManager.get({url:$.asModalManager.urls.fileManager}),
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
    //$chkIsLeaf = as("#chkIsLeaf"),
    $chkNew = as("#chkNew"),
    $txtCode=as("#txtCode"),
    $chkStatus= as("#chkStatus"),
    $txtVersion = as("#txtVersion"),
    $btnCompileMain = as("#btnCompileMain"),
    $btnCompileWhitoutDependemcy=as("#btnCompileWhitoutDependemcy"),
    $btnCompileByDependemcy=as("#btnCompileByDependemcy"),
    $btnDell=as("#btnDell"),
    $btnOutputManager=as("#btnOutputManager"),
    $winOutputManager=as("#winOutputManager"),
    $winWcfManager=as("#winWcfManager"),
    $winEfMigrationGenerator=as("#winEfMigrationGenerator"),
    $winDebugManager=as("#winDebugManager"),
    $btnTranslator = as("#btnTranslator"),
    $btnNext=as("#btnNext"),
    $btnPrev=as("#btnPrev"),
    $drpEditors=as("#drpEditors"),
    $divCache=as("#divCache"),
    $chkCheckIn=as("#chkCheckIn"),
    $txtComment=as("#txtComment"),
    $winSourceManager=$.asModalManager.get({url:$.asModalManager.urls.sourceManager}),
    $winSourceComparator=$.asModalManager.get({url:$.asModalManager.urls.sourceComparator}),
    $divEditor=as("#divEditor"),
    $drpType=as("#drpType"),
    $divDll=as("#divDll"),
    $txtPlaceHolder=as("#txtPlaceHolder"),
    $divPlaceHolder=as("#divPlaceHolder"),
    $chkIsDirectory=as("#chkIsDirectory"),
    $drpDllStorPlace=as("#drpDllStorPlace"),
    $btnManageCode=as("#btnManageCode"),
    $drpVersion=as("#drpVersion"),
    $divVersion=as("#divVersion"),
    $chkChangeClose=as("#chkChangeClose"),
    $btnDebug=as("#btnDebug"),
    $btnKhodkarBreakpoint=as("#btnKhodkarBreakpoint"),
    $btnVsBreakpoint=as("#btnVsBreakpoint"),
    $btnDebugManager=as("#btnDebugManager"),
    $btnWcfManager=as("#btnWcfManager"),
    $btnEfMigrationGenerator=as("#btnEfMigrationGenerator"),
    $btnSnippetInstanceWcfService=as("#btnSnippetInstanceWcfService"),
    $btnSnippetConfigurationMigration=as("#btnSnippetConfigurationMigration"),
    selectedVersion=0,
    selectedCodeId=0,
    dependencyId = [],
    codeFileName,
    dotNetType,
    selectedEditorId,
    selectedDllStorePlace,
    viewRoleId= 0,
    modifyRoleId= 0,
    accessRoleId= 0,
    typeId= 0,
    path= "",
   codeId= 0,
    serviceParentId= 0,
    //isLeaf=false,
    rowVersion= "",
    codeNewGuId,
    codeNewId,
    queryString= null,
    isLoadQueryString = false,
   
    backOrForwrdFlag=false,
    codeEditorPositionIndex=0,
    codeEditorPosition =[],
    currentEditor=null,
    interval=null,
    temp = {},
    selectedCode={},
    validate,
    newParents={},
    isFirstGrid=true,
    tableTemplate = '<table id="grvData" class="table table-condensed table-hover table-striped table-responsive">'+
    '<thead><tr>'+
       
    '</tr></thead>'+
        '<tbody>'+
       
    '</tbody>'+
'</table>';
$btnManageCode.hide();
$btnOutputManager.hide();
$btnCompileMain.hide();
$divDll.hide();
$divPlaceHolder.hide();
$divEditor.hide();
 $winCodeManager.asModal({width:800});
  $winWcfManager.asModal({width:800}) ;
   $winEfMigrationGenerator.asModal({width:800}) ;
 $winOutputManager.asModal({width:800}) ;
 $winDebugManager.asModal({width:800}) ;
$winSourceComparator.asModal({width:1200}); 
$winSourceManager.asModal({width:800}); 

$winTranslator.asModal({width:800})           
$winFind.asWindow({focusedId:"txtFind"})
$winRestore.asModal(
    {backdrop:'static', keyboard: false}
    )

$edrDotNet.asCodeEditor({ mode: "csharp"});







var loadRoles = function () {
    return $.asAjaxTask({
         url: $.asInitService($.asUrls.security_Role_getAllByOtherLanguage, [{ name: '@lang', value: $.asLang }])
    });
}
$drpType.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'MasterDataKeyValue.Id' },
            parentDataField: { name: 'MasterDataKeyValue.ParentId' },
            removeChildLessParent: false
        },
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByOtherLanguagesAndTypeId, [{ name: '@typeId', value: 1043 },{ name: '@lang', value: $.asLang }])
        , displayDataField: 'Name'
          , valueDataField: 'MasterDataKeyValue.Id',
        orderby: 'MasterDataKeyValue.Order'
    }
    , selectParents: false
//  , parentMode: "uniq"

});
$drpVersion.asDropdown("init","Please select code",{
    source: {
         displayDataField: 'Value'
          , valueDataField: 'Id',
        orderby: 'Id'
    }
});


 
$drpEditors.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'MasterDataKeyValue.Id' },
            parentDataField: { name: 'MasterDataKeyValue.ParentId' },
            removeChildLessParent: false
        },         
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByOtherLanguageAndTypeIdAndParentId, [{ name: '@typeId', value: 1030 },{ name: '@id', value: 196 },{ name: '@lang', value: $.asLang }])
        , displayDataField: 'Name'
          , valueDataField: 'MasterDataKeyValue.Code',
        orderby: 'MasterDataKeyValue.Order'
    }
    , selectParents: false
//  , parentMode: "uniq"

});
$divRoles.asAfterTasks([
    loadRoles()
], function (roles) {

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

$drpCodes.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'MasterDataKeyValue.Id' },
            parentDataField: { name: 'MasterDataKeyValue.ParentId' },
            removeChildLessParent: false
        },
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByOtherLanguagesAndTypeId, [{ name: '@typeId', value: 1041 },{ name: '@lang', value: $.asLang }])
        , displayDataField: 'Name'
          , valueDataField: 'MasterDataKeyValue.Id',
        orderby: 'MasterDataKeyValue.Order'
    }
    , selectParents: true
//  , parentMode: "uniq"

});

$drpDependency.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: false
        },
        url: $.asInitService($.asUrls.develop_code_os_dotNet_getDll, [])
        , displayDataField: 'Name'
          , valueDataField: 'Id',
        orderby: 'Order'
    }
    , multiple: true
    , selectParents: false
//  , parentMode: "uniq"

});

$drpDllStorPlace.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'MasterDataKeyValue.Id' },
            parentDataField: { name: 'MasterDataKeyValue.ParentId' },
            removeChildLessParent: false
        },
            url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByOtherLanguagesAndTypeId, [{ name: '@typeId', value: 1044 },{ name: '@lang', value: $.asLang }])
        , displayDataField: 'Name'
          , valueDataField: 'MasterDataKeyValue.Id',
        orderby: 'MasterDataKeyValue.Order'
    }
    , multiple: false
    , selectParents: false
//  , parentMode: "uniq"

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
        txtPlaceHolder:{
             required: {
                depends: function (element) {
                return $(element).is(":visible");
                }
            }
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
        drpType: {
            asType: 'asDropdown',
            required: {
                depends: function (element) {
                return as("#divType").is(":visible");
                }
            }
        },
        drpEditors: {
            asType: 'asDropdown',
            required: {
                depends: function (element) {
                return $divDll.is(":visible");
                }
            }
        },
        drpDllStorPlace: {
            asType: 'asDropdown',
            required: {
                depends: function (element) {
                return $divDll.is(":visible");
                }
            }
        },
         drpVersion: {
            asType: 'asDropdown',
            required: {
                depends: function (element) {
                return $divVersion.is(":visible");
                }
            }
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

        var setEnviroment = function(){
            if(dotNetType === 756 || dotNetType === 757 || dotNetType === 758 || dotNetType === 979){
               $divDll.show();
               
             if(selectedDllStorePlace === 811)
             $btnOutputManager.hide();
             else
             $btnOutputManager.show();
             
              if(selectedDllStorePlace === 812)
             $divVersion.show();
             else
             $divVersion.hide();
               
               
               if(dotNetType === 757 || dotNetType === 979){
                   $btnCompileMain.show();
                   $btnManageCode.hide();
               }else{
                   $btnCompileMain.hide();
                   $btnManageCode.show();
               }
               
               
               $divPlaceHolder.hide();
               if(dotNetType !== 757 && dotNetType !== 979){
                    $divEditor.hide();
               }else{
                   $divEditor.show();
               }
            } else{
                $divDll.hide();
                $btnOutputManager.hide();
                $btnCompileMain.hide();
                $divVersion.hide();
                if(dotNetType===null){
                $divPlaceHolder.hide();
                }else{
                      $divPlaceHolder.show();
                  }
                
            }


        }

var bindEvent =function () {
   var initRecovery = function(){
        queryString = $.asGetQueryString();
        if(queryString !== null){
            $.asTemp[queryString] = $.asTemp[queryString] || {};
            $.asTemp[queryString].dotNetEditor = $.asTemp[queryString].dotNetEditor || "";
    
            if($.asTemp[queryString].dotNetEditor !== "")
                    $.asStorage.setItem("dotNetEditor" + queryString,$.asTemp[queryString].dotNetEditor);
        }
   }

    
        $(asPageEvent).on($.asEvent.page.ready, function (event) {
            $drpEditors.asDropdown('selectValue', "csharp");
            $edrDotNet.asCodeEditor("focus");
            $winRestore.asModal("show");
    });
    
     $(asPageEvent).on($.asEvent.page.queryStringChange, function (event,pageUrl,queryString) {
          initRecovery();
          if(interval === null)
             interval = setInterval(autoSave, 5000);
            loadQueryString();
    });
    
     $drpEditors.on("change", function (event, item) {
         $edrDotNet.asCodeEditor("setEditorMode",item.value);
         selectedEditorId=item.value === "csharp" ? 197:198;
     });
     
      $drpDllStorPlace.on("change", function (event, item) {
         selectedDllStorePlace=item.value;
         setEnviroment();
     });
     
        $drpType.on("change", function (event, item) {
        if ($drpType.asDropdown('selected')) {
            dotNetType=item.value;
            setEnviroment();
        }});
        

     
    var loadWinComparator = function(sourceControlCode,fileName){
                 
         
                      
            $winSourceComparator.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.sourceComparator)},{name:"@isModal",value:true}])
                                                                                  
            ,{editorCode:$edrDotNet.asCodeEditor("getValue"),sourceControlCode:sourceControlCode,fileName:fileName});
         
         
    }
    
    $(asPageEvent).on("compare",function(event,selectedId){
        $divEditor.asAjax({
            url: $.asInitService($.asUrls.develop_code_os_dotNet_getChange,[
                {name:"@changeId",value:selectedId}
                ,{name:"@codeId",value:codeId}
                ]),
            type:"get",
            success: function (dotnetCode) {
                 loadWinComparator(dotnetCode,$txtId.val()+ (selectedEditorId === 197 ? ".cs":".vb"));
            }
        }, { validate:false, overlayClass: 'as-overlay-absolute'});
     });
     
    $(asPageEvent).on("changeSetSelected",function(event,selectedId){

        $divEditor.asAjax({
            url: $.asInitService($.asUrls.develop_code_os_dotNet_getChange,[
                {name:"@changeId",value:selectedId}
                ,{name:"@codeId",value:codeId}
                ]),
            type:"get",
            success: function (serviceCode) {
                 $edrDotNet.asCodeEditor('setValue', serviceCode);
             
            }
        }, { validate:false, overlayClass: 'as-overlay-absolute'});
    });
    
        $btnManageCode.click(function () {
          if ($drpCodes.asDropdown('selected')) {
            $winCodeManager.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.fileManager)},{name:"@isModal",value:true}])
            ,{path:path,urlParentId:1008});
          }else{
                 $.asShowMessage({template:"error", message: "No code selected"});
          }
          

    });
    
    as("#btnSourceControl").click(function () {
              $winSourceManager.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.sourceManager)},{name:"@isModal",value:true}])
            ,{parent:asPageEvent,selectEvent:"changeSetSelected",compareEvent:"compare",getUrl: $.asInitService($.asUrls.develop_code_os_dotNet_getChanges, [
             {name:'@codeId',value:codeId}
             ])});

    });
    

    as("#btnEditorResize").click(function () {
        resize();
    });

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
                if( $edrDotNet === currentEditor)
                 {
                         $divEditor.show();
                    currentEditor.asCodeEditor('setValue',$.asStorage.getItem("dotNetEditor" + queryString));
                 }
          }
    }
    
     var recoverAllEditor = function(){
          queryString = $.asGetQueryString();
          if(queryString !== null){
               $divEditor.show();
                $edrDotNet.asCodeEditor('setValue',$.asStorage.getItem("dotNetEditor" + queryString));
          }
    }
   
         $edrDotNet.asCodeEditor("editor").commands.addCommand({
    name: 'Find',
    bindKey: {win: 'Ctrl-F',  mac: 'Command-F'},
    exec: function(editor) {
       $winFind.asWindow("show")
    },
    readOnly: true // false if this command should not apply in readOnly mode
});



    $edrDotNet.asCodeEditor("editor").commands.addCommand({
    name: 'CommentToggel',
    bindKey: {win: 'Ctrl-K',  mac: 'Command-K'},
    exec: function(editor) {
       commentToggel()
    },
    readOnly: false // false if this command should not apply in readOnly mode
});


 $edrDotNet.asCodeEditor("editor").commands.addCommand({
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
                     $edrDotNet.asCodeEditor("editor").gotoLine(codeEditorPosition[codeEditorPositionIndex]);
                     if(codeEditorPositionIndex > 1)
                     codeEditorPositionIndex--;

        });
        $btnNext.click(function () {
                
          backOrForwrdFlag=true;

                codeEditorPositionIndex = codeEditorPositionIndex === 0 ? codeEditorPosition.length -1:codeEditorPositionIndex;
                 $edrDotNet.asCodeEditor("editor").gotoLine(codeEditorPosition[codeEditorPositionIndex]);
                 if(codeEditorPositionIndex < codeEditorPosition.length - 1)
                 codeEditorPositionIndex++;

                
        });
         $edrDotNet.asCodeEditor("editor").getSession().on('change', function(e) {
            codeEditorPositionIndex = codeEditorPosition.length -1
         });
   $edrDotNet.asCodeEditor("editor").on("focus", function(){
        currentEditor =$edrDotNet;
       changeEditorToolbar(".Net");
        backOrForwrdFlag=false;
    });
    
     $edrDotNet.asCodeEditor("editor").getSession().selection
     .on('changeCursor', function(e) {
                codeEditorPosition.push($edrDotNet.asCodeEditor("editor").selection.getCursor().row);
        });
 
    $chkIsDirectory.change(function () {
        if(this.checked === true) {
            as("#divType").hide();
            //as("#divIsLeaf").hide();
            $divEditor.hide();
        }else{
             as("#divType").show();
            // as("#divIsLeaf").show();
             if(dotNetType !== 756 && dotNetType !== 758)
             $divEditor.show();
             else
             $divEditor.hide();
        }
     });
    
      $chkNew.change(function () {
        if(this.checked === true)
       {
            $chkIsDirectory.prop('checked', false);
            $chkIsDirectory.trigger('change');
            $txtId.prop("disabled", false)

            temp.Id = codeId;
            
            codeId=0;
            
           // temp.Isleaf = isLeaf
            
            temp.ParentId = serviceParentId;
            serviceParentId=0;
            
            temp.RowVersion = rowVersion;
            rowVersion='';

    
              
            temp.DotNetCode = $edrDotNet.asCodeEditor('getValue');
            $edrDotNet.asCodeEditor('setValue', '');
            
            temp.Name = $txtName.val();
            $txtName.val("")
       
            temp.Description = $txtDescription.val()
            $txtDescription.val("")
            
         
            temp.Code = $txtCode.val()
            $txtCode.val("")
            
            
            temp.SecondCode = $txtPlaceHolder.val()
            $txtPlaceHolder.val("")
         
            temp.Guid = $txtId.val()
            
            temp.NewGuid=codeNewGuId;
            $txtId.val(codeNewGuId)
            
           
            temp.NewId = codeNewId
            $txtCodeId.val(codeNewId)
            
            temp.Version = $txtVersion.val()
            $txtVersion.val("0")
            
            temp.ForeignKey1 = dotNetType;
            dotNetType=null;
            $drpType.asDropdown('selectValue', [], true);
            setEnviroment(null);
            temp.ForeignKey3=selectedEditorId;
            temp.Key = selectedDllStorePlace;
            temp.Order = $txtOrder.val()
            $txtOrder.val("")

             temp.EditMode = $chkEditMode.prop('checked')
            $chkEditMode.prop('checked', false)
            temp.Status = $chkStatus.prop('checked')
            $chkStatus.prop('checked', false)
            
            temp.EnableCache = $chkChangeClose.prop('checked')
            $chkChangeClose.prop('checked', false)
            
            selectedVersion=0;
            temp.SlidingExpirationTimeInMinutes = $drpVersion.asDropdown('selected').value;
          $drpVersion.asDropdown('selectValue', [], true);
          
                temp.ViewRoleId=viewRoleId
                $drpViewRole.asDropdown('selectValue', [], true)
                
          
            temp.AccessRoleId=accessRoleId
                $drpAccessRole.asDropdown('selectValue', [], true)
       
            temp.ModifyRoleId=modifyRoleId
                $drpModifyRole.asDropdown('selectValue', [], true)
                
            
       }else{
           setCode(temp)
       }
    });
    
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
    
    var compile = function(byDependency){
        
      if(dotNetType === 756 || dotNetType === 757 || dotNetType === 758 || dotNetType === 979){
           $frm.asAjax({
            url:$.asUrls.develop_code_os_dotNet_dllCompile,
            data: JSON.stringify({
                Id: selectedCodeId,
                ByDependency:byDependency
            }),
            success: function (result) {
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration ,showTime:10000000 });
              
            }
        }, { $form: $frm });
          }else{
              $.asShowMessage({ template: "error", message: " Ability to compile only for assembly type" });
          }
        
        
    }
    $btnCompileWhitoutDependemcy.click(function () {
            compile(false);
    });
    
    $btnCompileByDependemcy.click(function () {
            compile(true);
    });
    
    $btnDebug.click(function () {
          $frm.asAjax({
            url:$.asUrls.develop_code_os_dotNet_debugCode,
            data: JSON.stringify({
                Id: selectedCodeId
            }),
            success: function (result) {
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration ,showTime:10000000 });
              
            }
        }, { $form: $frm });
    });
    
    
     $btnSnippetConfigurationMigration.click(function () {
         if(selectedEditorId === 197){
             $edrDotNet.asCodeEditor("insert",
             "\n" +"//you must refrence EntityFramework.dll and EntityFramework.SqlServer.dll" + "\n"
            + "\n" +"//you must use System.Data.Entity.Migrations" + "\n"
            + "\n" +"internal sealed class Configuration : DbMigrationsConfiguration<full namespace qualified dbContext Name>" + "\n"
            + "\n" +"{" + "\n"
            + "\n" +" public Configuration()" + "\n"
            + "\n" +" {" + "\n"
            + "\n" +" AutomaticMigrationsEnabled = false;" + "\n"
            + "\n" +" }" + "\n"
            + "\n" +" protected override void Seed(full namespace qualified dbContext Name context)" + "\n"
            + "\n" +" {" + "\n"
            + "\n" +" }" + "\n"
            + "\n" +"}" + "\n"
            );}else{
                  $edrDotNet.asCodeEditor("insert",
             "\n" +"''you must refrence EntityFramework.dll and EntityFramework.SqlServer.dll" + "\n"
            + "\n" +"'//you must use System.Data.Entity.Migrations" + "\n"
            + "\n" +"NotInheritable Class Configuration Inherits DbMigrationsConfiguration(Of full namespace qualified dbContext Name)" + "\n"
            + "\n" +"Public Sub New()" + "\n"
            + "\n" +"MyBase.New" + "\n"
            + "\n" +"AutomaticMigrationsEnabled = false" + "\n"
            + "\n" +"End Sub" + "\n"
            + "\n" +"Protected Overrides Sub Seed(ByVal context As full namespace qualified dbContext Name)" + "\n"
            + "\n" +"End Sub" + "\n"
            + "\n" +"End Class" + "\n"
            );
            }
     });
    
    
     $btnSnippetInstanceWcfService.click(function () {
         if(selectedEditorId === 197){
             $edrDotNet.asCodeEditor("insert",
             "\n" +"//you must refrence System.dll and System.ServiceModel.dll and System.Runtime.Serialization.dll" + "\n"
            + "\n" +"//you must use System.Net; and System.ServiceModel; and System.ServiceModel.Description;" + "\n"
            + "\n" + "//replace WCFInterface by your wcf Interface" + "\n"
            + "\n" +"public WCFInterface CreateWCFInterface()" + "\n"
            + "\n" +"{" + "\n"
            + "\n" +"//create the binding" + "\n"
            + "\n" +" var binding = new BasicHttpBinding();" + "\n"
            + "\n" +"//configure the binding" + "\n"
             + "\n" +"//if your wcf service is windows authenticate uncomment below line" + "\n"
            + "\n" +"//binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;" + "\n"
            + "\n" +"//binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;" + "\n"
            + "\n" +"binding.Security.Mode = BasicHttpSecurityMode.None;" + "\n"
            + "\n" +"binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;" + "\n"
            + "\n" +"//if (you want set proxy for connection) uncomment below line" + "\n"
            + "\n" +'//binding.ProxyAddress = new Uri(string.Format("http://{0}:{1}", "samplePoroxy.yourDomain.com", "port"));' + "\n"
            + "\n" +"//binding.BypassProxyOnLocal = true;" + "\n"
            + "\n" +"//binding.UseDefaultWebProxy = false;" + "\n"
            + "\n" +'binding.CloseTimeout = TimeSpan.Parse("00:05:00");' + "\n"
            + "\n" +'binding.SendTimeout = TimeSpan.Parse("00:05:00");' + "\n"
            + "\n" +"binding.MaxBufferSize = 999999999;" + "\n"
            + "\n" +"binding.MaxReceivedMessageSize = 999999999;" + "\n"
            + "\n" +"binding.ReaderQuotas.MaxArrayLength = 2147483647;" + "\n"
            + "\n" +"binding.ReaderQuotas.MaxBytesPerRead = 2147483647;" + "\n"
            + "\n" +"binding.ReaderQuotas.MaxDepth = 2147483647;" + "\n"
            + "\n" +"binding.ReaderQuotas.MaxNameTableCharCount = 2147483647;" + "\n"
            + "\n" +"binding.ReaderQuotas.MaxStringContentLength = 2147483647;" + "\n"
            + "\n" +'var endpointAddress = new EndpointAddress("WCF webService Url");' + "\n"
            + "\n" +"var channelFactory = new ChannelFactory<WCFInterface>(binding, endpointAddress);" + "\n"
             + "\n" +"// if you need to windows auth by another user uncomment below line " + "\n"
            + "\n" +"// if (channelFactory.Credentials != null)" + "\n"
            + "\n" +"//  channelFactory.Credentials.Windows.ClientCredential =" + "\n"
            + "\n" +'//   new NetworkCredential("username", "password", "domain");' + "\n"
            + "\n" +"foreach (var operation in channelFactory.Endpoint.Contract.Operations)" + "\n"
            + "\n" +"{" + "\n"
            + "\n" +"var behavior = operation.Behaviors.Find<DataContractSerializerOperationBehavior>();" + "\n"
            + "\n" +"if (behavior != null)" + "\n"
            + "\n" +"{" + "\n"
            + "\n" +" behavior.MaxItemsInObjectGraph = 2147483647;" + "\n"
            + "\n" +"}" + "\n"
            + "\n" +"}" + "\n"
            + "\n" +"//create the channel" + "\n"
            + "\n" +"return channelFactory.CreateChannel();" + "\n"
            + "\n" +"}" + "\n"
             );
         }else{
              $edrDotNet.asCodeEditor("insert",
             "\n" +"''you must refrence System.dll and System.ServiceModel.dll and System.Runtime.Serialization.dll" + "\n"
            + "\n" +"'you must use System.Net; and System.ServiceModel; and System.ServiceModel.Description;" + "\n"
            + "\n" + "'replace WCFInterface by your wcf Interface" + "\n"
             + "\n" + "Public Function CreateWCFInterface() As WCFInterface" + "\n"
             + "\n" + "'create the binding" + "\n"
             + "\n" + "Dim binding = New BasicHttpBinding" + "\n"
             + "\n" + "'configure the binding" + "\n"
             + "\n" + "'if your wcf service is windows authenticate uncomment below line" + "\n"
             + "\n" + "'binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly" + "\n"
             + "\n" + "'binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm" + "\n"
             + "\n" + "binding.Security.Mode = BasicHttpSecurityMode.None" + "\n"
             + "\n" + "binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None" + "\n"
             + "\n" + "'if you want set proxy for connection uncomment below line" + "\n"
             + "\n" + "'" + 'binding.ProxyAddress = New Uri(String.Format("http://{0}:{1}", "samplePoroxy.yourDomain.com", "port"))' + "\n"
             + "\n" + "'binding.BypassProxyOnLocal = true" + "\n"
             + "\n" + "'binding.UseDefaultWebProxy = false" + "\n"
             + "\n" + "" + "\n"
             + "\n" + 'binding.CloseTimeout = TimeSpan.Parse("00:05:00")' + "\n"
             + "\n" + 'binding.SendTimeout = TimeSpan.Parse("00:05:00")' + "\n"
             + "\n" + "binding.MaxBufferSize = 999999999" + "\n"
             + "\n" + "binding.MaxReceivedMessageSize = 999999999" + "\n"
             + "\n" + "binding.ReaderQuotas.MaxArrayLength = 2147483647" + "\n"
             + "\n" + "binding.ReaderQuotas.MaxBytesPerRead = 2147483647" + "\n"
             + "\n" + " binding.ReaderQuotas.MaxDepth = 2147483647" + "\n"
             + "\n" + "binding.ReaderQuotas.MaxNameTableCharCount = 2147483647" + "\n"
             + "\n" + "binding.ReaderQuotas.MaxStringContentLength = 2147483647" + "\n"
             + "\n" + 'Dim endpointAddress = New EndpointAddress("WCF webService Url")' + "\n"
             + "\n" + "Dim channelFactory = New ChannelFactory(Of WCFInterface)(binding, endpointAddress)" + "\n"
             + "\n" + "' if you need to windows auth by another user uncomment below line " + "\n"
             + "\n" + "' If (Not (channelFactory.Credentials) Is Nothing) Then" + "\n"
             + "\n" + "'" + 'channelFactory.Credentials.Windows.ClientCredential = New NetworkCredential("username", "password", "domain")' + "\n"
             + "\n" + "'End If" + "\n"
             + "\n" + "For Each operation In channelFactory.Endpoint.Contract.Operations" + "\n"
             + "\n" + " Dim behavior = operation.Behaviors.Find(Of DataContractSerializerOperationBehavior)" + "\n"
             + "\n" + "If (Not (behavior) Is Nothing) Then" + "\n"
             + "\n" + "behavior.MaxItemsInObjectGraph = 2147483647" + "\n"
             + "\n" + "End If" + "\n"
             + "\n" + "Next" + "\n"
             + "\n" + "'create the channel" + "\n"
             + "\n" + "Return channelFactory.CreateChannel" + "\n"
             + "\n" + " End Function" + "\n");
         }
     });
     
     $btnVsBreakpoint.click(function () {
         if(selectedEditorId === 197){
             $edrDotNet.asCodeEditor("insert",
             "\n" + "//you Must Refrence KS.Core.dll"+ "\n"
              +"#if DEBUG" + "\n" +  "//check client login by debug mode"+ "\n"
             +"if (KS.Core.UI.Setting.Settings.IsDebugMode)" + "\n" +  "{"+ "\n"
             +"System.Diagnostics.Debugger.Break();" + "\n"
             + "}" + "\n" +  "#endif"
             + "\n"
             );
         }else{
             $edrDotNet.asCodeEditor("insert",
             "\n" + "'you Must Refrence KS.Core.dll"+ "\n"
             +"#If DEBUG Then" + "\n" +  "'check client login by debug mode"+ "\n"
             +"If KS.Core.UI.Setting.Settings.IsDebugMode Then" + "\n"
            + "System.Diagnostics.Debugger.Break()" + "\n" +  "End If"+ "\n"
            + "#End If" + "\n" 
             );
         }
          
     });
     
     $btnKhodkarBreakpoint.click(function () {
         
               
            if(selectedCodeId !== 0){

                
                         
         if(selectedEditorId === 197){
             $edrDotNet.asCodeEditor("insert",
             "\n" + "//you Must Refrence KS.Core.dll"+ "\n"
             + "#if DEBUG" + "\n" +  "//check client login by debug mode"+ "\n"
             +"if (KS.Core.UI.Setting.Settings.IsDebugMode)" + "\n" +  "{"+ "\n"
             +"//new Debugger" + "\n" +  "var debugger ="+ "\n"
             +"new KS.Core.CodeManager.Os.DotNet.Debugger(new KS.Core.FileSystemProvide.FileSystemManager());" + "\n" +  "//log sample Object"+ "\n"
             + "var addedDebugInfo = debugger.AddOrUpdateDebugInfo(new KS.Core.Model.Develop.DebugInfo()" + "\n" +  '{ CodeId = ' + selectedCodeId 
             + ', Data = debugger.SerializeObjectToJobjectString(new Object()), IntegerValue = 1 }, "@asCodePath");'+ "\n"
             + "//read debug by debugId" + "\n" +  'var readedDebugInfo = debugger.GetDebugInfo(addedDebugInfo.Id, "@asCodePath");'+ "\n"
             + "//get all debugs" + "\n" +  'var allDebugsOfCodeId = debugger.GetDebugInfos("@asCodePath", ' + selectedCodeId + ');'+ "\n"
             + "//you can use linq query on allDebugsOfCodeId" + "\n" +  " var selectedDebug = allDebugsOfCodeId.Where(dbg => dbg.IntegerValue == 1).FirstOrDefault();"+ "\n"
             + "}" + "\n" +  "#endif"
             + "\n"
             );
         }else{
             $edrDotNet.asCodeEditor("insert",
             "\n" + "'you Must Refrence KS.Core.dll"+ "\n"
             +"#If DEBUG Then" + "\n" +  "'check client login by debug mode"+ "\n"
             +"If KS.Core.UI.Setting.Settings.IsDebugMode Then" + "\n" +  "'new Debugger"+ "\n"
             +"Dim debugger As KS.Core.CodeManager.Os.DotNet.Base.IDebugger =" + "\n" +  "New KS.Core.CodeManager.Os.DotNet.Debugger(New KS.Core.FileSystemProvide.FileSystemManager())"+ "\n"
            + "'log sample Object" + "\n" +  "Dim addedDebugInfo = debugger.AddOrUpdateDebugInfo(New KS.Core.Model.Develop.DebugInfo() With"+ "\n"
             +'{.CodeId = ' + selectedCodeId + ', .Data = debugger.SerializeObjectToJobjectString(New Object()), .IntegerValue = 1}, "@asCodePath")' + "\n" +  "'read debug by debugId"+ "\n"
            + 'Dim readedDebugInfo = debugger.GetDebugInfo(addedDebugInfo.Id, "@asCodePath")' + "\n" +  "'get all debugs"+ "\n"
            + 'Dim allDebugsOfCodeId = debugger.GetDebugInfos("@asCodePath", ' + selectedCodeId + ')' + "\n" +  "'you can use linq query on allDebugsOfCodeId"+ "\n"
            + "Dim selectedDebug = allDebugsOfCodeId.Where(Function(dbg) dbg.IntegerValue = 1).FirstOrDefault()" + "\n" +  "End If"+ "\n"
            + "#End If" + "\n" 
             );
         }
                
            }else{
                 $.asShowMessage({ template: "error", message: " Please select a code" })
        }
         

          
     });
     
       $btnWcfManager.click(function () {

            $winWcfManager.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter("en/admin/develop/codes/os/dotnet/wcf-manager")},{name:"@isModal",value:true}]));
    });
    
    
    $btnEfMigrationGenerator.click(function () {
            if(selectedCodeId !== 0){
            $winEfMigrationGenerator.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter("en/admin/develop/codes/os/dotnet/ef-migration-generator")},{name:"@isModal",value:true}])
            ,{codeId:selectedCodeId});
            }else{
                 $.asShowMessage({ template: "error", message: "Please select a code" })
        }

    });
    
  $btnOutputManager.click(function () {
            
            
            if(selectedCodeId !== 0){
                            
            $winOutputManager.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter("en/admin/develop/codes/os/dotnet/output-manager")},{name:"@isModal",value:true}])
            ,{parent:asPageEvent,codeId:selectedCodeId,codePath:path,showPublishButton:selectedDllStorePlace === 813,showAddOutputButton:dotNetType !== 757 && dotNetType !== 979});
            }else{
                 $.asShowMessage({ template: "error", message: "Please select a code" })
        }

    });
    
      $btnDebugManager.click(function () {
            
            if(selectedCodeId !== 0){
            
            $winDebugManager.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter("en/admin/develop/codes/os/dotnet/debug-manager/")},{name:"@isModal",value:true}])
            ,{codeId:selectedCodeId});
            }else{
                 $.asShowMessage({ template: "error", message: "Please select a code" })
        }

    });
    
    $btnDell.click(function () {
        if ($drpCodes.asDropdown('selected')) {
         $frm.asAjax({
            url:$.asUrls.develop_code_os_dotNet_dell,
            data: JSON.stringify({
                Id: selectedCodeId,
            }),
            success: function (result) {
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration ,showTime:10000000 });
              $drpCodes.asDropdown("removeItem");
            }
        }, { $form: $frm });
        }else{
                 $.asShowMessage({ template: "error", message: "To remove a code, select" })
        }
    });
    $btnSave.click(function () {

         var id,parentId,guid;
          if ($drpCodes.asDropdown('selected')) {
              if($chkNew.is(':checked'))
           {
             guid= codeNewGuId;
            parentId = selectedCodeId;
            id=codeNewId;
           }
            else{
                guid= $txtId.val();
                id=selectedCodeId;
                parentId=serviceParentId;
            }
            
        }
        
          
    
                      
   

         
            
        
     //$chkIsLeaf.is(':checked') === false && 
        if($chkNew.is(':checked')){
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

 

        var jsCode = $edrDotNet.asCodeEditor("getValue")

        $.asTemp[queryString].dotNetEditor = jsCode;
   
        
        dependencyId = [];
        if ($drpDependency.asDropdown('selected')) {
            $.each($drpDependency.asDropdown('selected'), function (i, v) {
                if (v.selected)
                    dependencyId.push(v.value)
            });
        }
        
        $frm.asAjax({
            url: $.asUrls.develop_code_os_dotNet_save,
            data: JSON.stringify({
                Id: id,
                ParentId:parentId,
                SecondCode: $txtPlaceHolder.val(),
                ViewRoleId: viewRoleId,
                ModifyRoleId: modifyRoleId,
                AccessRoleId: accessRoleId,
                Code:$txtCode.val(),
                DotNetCode: jsCode,
                Name: $txtName.val(),
                Description:$txtDescription.val(),
                Guid:guid,
                DependentDlls:dependencyId,
                Order: $txtOrder.val(),
                Status: $chkStatus.is(':checked'),
                EditMode: $chkEditMode.is(':checked'),
                IsLeaf: false,
                IsNew:$chkNew.is(':checked'),
                CheckIn: $chkCheckIn.is(':checked'),
                Comment:$txtComment.val(),
                ForeignKey1:dotNetType,
                ForeignKey3:selectedEditorId,
                Key:selectedDllStorePlace,
                SlidingExpirationTimeInMinutes:selectedVersion,
                EnableCache:$chkChangeClose.is(':checked'),
                RowVersion: rowVersion
            }),
            success: function (code) {
                if($chkNew.is(':checked')){
                    var newParent = false
                        if(newParents[selectedCode.value]){
                            newParent = true
                            delete newParents[selectedCode.value]
                        }
                       $drpCodes.asDropdown("addItem",{text:$txtName.val(),value:id},selectedCode,newParent)
                }
                 
                setCode(code,false)
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration ,showTime:10000000 });
            }
        }, { $form: $frm })

    });

 $drpVersion.on("change", function (event, item) {
    selectedVersion=item.value;
 });
    $drpCodes.on("change", function (event, item) {
         selectedCode = item;
         selectedCodeId = item.value;
        $btnTranslator.removeClass("disabled");
        $btnTranslator.prop("disabled",false);
             if (isLoadQueryString === false) {

    
                if (typeof (item.value) != "undefined") {
                    $.asSetQueryString(item.value)
                  
                }

            }


    });

       $btnTranslator.click(function () {
        $winTranslator.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.translator)},{name:"@isModal",value:true}])
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
 $.asNotFound("Code")
}
    var loadData = function (url) {
        return $.asAjaxTask({
            url: url
        });
    }
var getCode = function (id) {

    isLoadQueryString = true;

                selectedCodeId=id;
                $drpCodes.asDropdown('selectValue', [], true);
                $drpCodes.asDropdown('selectValue', id);
                

            

            

    viewRoleId = 0;
    rowVersion = "";
    


    $frm.asAjax({
        url: $.asInitService($.asUrls.develop_code_os_dotNet_get,[{name:"@id",value:id}]),
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
        $.asStorage.setItem("dotNetEditor" + queryString,$edrDotNet.asCodeEditor("getValue"))
    }
var changeEditorToolbar = function(editor){
        
        
 
           $lblEditor.html(editor)
     
    
          
    }
    
var setCode = function(code,changeEditor){
    if(typeof(changeEditor) == "undefined")
        changeEditor=true;
            $txtComment.val("");
            $chkCheckIn.prop('checked', false);
            $txtId.prop("disabled", true);
            $chkNew.prop('checked', false);
            codeId = code.Id;
            path = code.Path;
            $txtCodeId.val(codeId)
            serviceParentId=code.ParentId
            rowVersion = code.RowVersion
            //$chkIsLeaf.prop('checked', code.IsLeaf)
            codeFileName=code.FileName;
            //isLeaf = code.IsLeaf
            if(changeEditor === true){
                if (code.DotNetCode != null)
                $edrDotNet.asCodeEditor('setValue',code.DotNetCode);
            else
                $edrDotNet.asCodeEditor('setValue', '');
            }
            as("#divLastModifiUser").html(code.LastModifieUser);
            as("#divLastModifiLocalDataTime").html(code.LastModifieLocalDateTime);
            $txtName.val(code.Name)
            if (code.Description != null)
            $txtDescription.val(code.Description)
            else
            $txtDescription.val("")
            if (code.Code !== null)
            $txtCode.val(code.Code)
            else
            $txtCode.val("")
            if (code.SecondCode !== null)
            $txtPlaceHolder.val(code.SecondCode)
            else
            $txtPlaceHolder.val("")
            $txtPlaceHolder.trigger( "input" );
            $txtId.val(code.Guid)
            codeNewGuId = code.NewGuid;
            codeNewId = code.NewId;
            $txtVersion.val(code.Version)
            selectedVersion=code.SlidingExpirationTimeInMinutes;
            

   
            if (code.Order !== null)
            $txtOrder.val(code.Order)
            else
            $txtOrder.val("")
            $chkEditMode.prop('checked', code.EditMode)
            $chkStatus.prop('checked', code.Status)
            $chkChangeClose.prop('checked', code.EnableCache)
            dotNetType=code.ForeignKey1;
             if (code.ForeignKey1 !== null){
                  $drpType.asDropdown('selectValue', code.ForeignKey1);
                  
                 setEnviroment();
                  $chkIsDirectory.prop('checked', false);
                 as("#divType").show();
             }else{
                  $drpType.asDropdown('selectValue', [], true);
             
                 setEnviroment();
                 $chkIsDirectory.prop('checked', true);
                 as("#divType").hide();
             }
               
                dependencyId=[];
            $drpDependency.asDropdown('selectValue', [], true)
            if (code.DependentDlls != null) {
                if (code.DependentDlls.length != 0)
                    $drpDependency.asDropdown('selectValue', code.DependentDlls)
                dependencyId=code.DependentDlls;
            }
               
                selectedEditorId=code.ForeignKey3;
               
             if (code.ForeignKey3 !== null){
               
                  if(code.ForeignKey3 === 197)
                  $drpEditors.asDropdown('selectValue', "csharp");
                  else
                  $drpEditors.asDropdown('selectValue', "vbscript");
             }else
                $drpEditors.asDropdown('selectValue', [], true);
    
             selectedDllStorePlace=code.Key;
             
           
            if((dotNetType === 756 || dotNetType === 757 || dotNetType === 758 || dotNetType === 979) && selectedDllStorePlace === 812){
                $divVersion.asAfterTasks([
                   loadData($.asInitService($.asUrls.develop_code_os_dotNet_getOutputVersions, [{ name: '@codeId', value: selectedCodeId }]))
                ], function (versions) {
                    $drpVersion.asDropdown("reload",{localData: versions});
              
                    $drpVersion.asDropdown('selectValue', [selectedVersion === 0 ? null:selectedVersion])
                
                },{overlayClass: 'as-overlay-absolute'});
            }
   

             if (code.Key !== null){
              $drpDllStorPlace.asDropdown('selectValue', code.Key);
             }else
                $drpDllStorPlace.asDropdown('selectValue', [], true);
             setEnviroment();
             
           
            if (code.ViewRoleId !== null){
                 $drpViewRole.asDropdown('selectValue', code.ViewRoleId);
                 viewRoleId=code.ViewRoleId;
            }else
                $drpViewRole.asDropdown('selectValue', [], true)
                
            if (code.AccessRoleId != null){
                 $drpAccessRole.asDropdown('selectValue', code.AccessRoleId);
                  accessRoleId=code.AccessRoleId;
            } else
                $drpAccessRole.asDropdown('selectValue', [], true);
                
            if (code.ModifyRoleId != null){
                 $drpModifyRole.asDropdown('selectValue', code.ModifyRoleId);
                 modifyRoleId=code.ModifyRoleId;
            }else
                $drpModifyRole.asDropdown('selectValue', [], true);
                
                  $chkIsDirectory.trigger('change');
}
var loadQueryString = function () {
    queryString = $.asGetQueryString()
    if (queryString !== null) {
        var q = queryString.split("/")


        getCode(q[0]);




    }
}



  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent();
if (typeof (requestedUrl) != 'undefined')  {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });