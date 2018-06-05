var 
    $frm = as("#frmQuery"),
    $divRoles = as("#divRoles"),
    $edrJavascript= as("#edrJavascript"),
    $drpViewRole= as("#drpViewRole"),
    $drpModifyRole= as("#drpModifyRole"),
    $drpAccessRole= as("#drpAccessRole"),
    $drpService= as("#drpService"),
    $txtName= as("#txtName"),
    $btnSave= as("#btnSave"),
    $chkEditMode= as("#chkEditeMode"),
    $chkIsLogOnService=as("#chkIsLogOnService"),
    $winFind=as("#divFind"),
    $winTranslator= $.asModalManager.get({url:$.asModalManager.urls.translator}),
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
    $txtServiceId=as("#txtServiceId"),
    $txtUrl=as("#txtUrl"),
    $chkService = as("#chkService"),
    $chkNew = as("#chkNew"),
    $txtCode=as("#txtCode"),
    $chkCache= as("#chkCache"),
    $chkStatus= as("#chkStatus"),
    $txtVersion = as("#txtVersion"),
    $txtSlidingExpirationTimeInMinutes = as("#txtSlidingExpirationTimeInMinutes"),
    $txtCompiledUrl = as("#txtCompiledUrl"),
    $btnCompile = as("#btnCompile"),
    $btnDell=as("#btnDell"),
    $btnTranslator = as("#btnTranslator"),
    $btnNext=as("#btnNext"),
    $btnPrev=as("#btnPrev"),
    $drpContexts=as("#drpContexts"),
    $btnGetMetaData=as("#btnGetMetaData"),
    $divCache=as("#divCache"),
    $divIsLogOnService = as("#divIsLogOnService"),
    $chkCheckIn=as("#chkCheckIn"),
    $txtComment=as("#txtComment"),
    $winSourceManager=$.asModalManager.get({url:$.asModalManager.urls.sourceManager}),
    $winSourceComparator=$.asModalManager.get({url:$.asModalManager.urls.sourceComparator}),
    $divEditor=as("#divEditor"),
    viewRoleId= 0,
    modifyRoleId= 0,
    accessRoleId= 0,
    typeId= 0,
    url= "",
   serviceId= 0,
    serviceParentId= 0,
    isLeaf=false,
    rowVersion= "",
    codeNewId,
    serviceNewId,
    queryString= null,
    isLoadQueryString = false,
   
    backOrForwrdFlag=false,
    codeEditorPositionIndex=0,
    codeEditorPosition =[],
    currentEditor=null,
    interval=null,
    temp = {},
    selectedService={},
    validate,
    newParents={},
    isFirstGrid=true,
    tableTemplate = '<table id="grvData" class="table table-condensed table-hover table-striped table-responsive">'+
    '<thead><tr>'+
       
    '</tr></thead>'+
        '<tbody>'+
       
    '</tbody>'+
'</table>';




$winSourceComparator.asModal({width:1200}); 
$winSourceManager.asModal({width:800}); 

     $winTranslator.asModal({width:800})           
$winFind.asWindow({focusedId:"txtFind"})
$winRestore.asModal(
    {backdrop:'static', keyboard: false}
    )

$edrJavascript.asCodeEditor();







var loadRoles = function () {
    return $.asAjaxTask({
        url: $.asUrls.security_Role_getAllByDefaultsLanguage
    });
}
$drpContexts.asDropdown({
        source: {
            hierarchy:
            {
                type: 'flat',
                keyDataField: { name: 'Id' },
                parentDataField: { name: 'ParentId' },
                childrenDataField: 'Children',
                removeChildLessParent: true
            },
            valueDataField: 'PathOrUrl',
            displayDataField: 'Name',
            orderby: 'Order',
              url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId, [{ name: '@typeId', value: 1039 }])
        }
         , parentMode: "uniq"
    });
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

$drpService.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: false
        },
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId, [{ name: '@typeId', value: 1001 }])
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
        txtUrl:{
            required: true
        },
        txtServiceId:{
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
   var initRecovery = function(){
        queryString = $.asGetQueryString();
        if(queryString !== null){
            $.asTemp[queryString] = $.asTemp[queryString] || {};
            $.asTemp[queryString].serviceJavascriptQueryEditor = $.asTemp[queryString].serviceJavascriptQueryEditor || "";
    
            if($.asTemp[queryString].serviceJavascriptQueryEditor !== "")
                    $.asStorage.setItem("serviceJavascriptQueryEditor" + queryString,$.asTemp[queryString].serviceJavascriptQueryEditor);
        }
   }
     $(asPageEvent).on($.asEvent.page.beforeSendAjaxRequest, function (event,args) {
    
     if(args.url) $txtCompiledUrl.val(args.url)
  });
    
        $(asPageEvent).on($.asEvent.page.ready, function (event) {
            
            $edrJavascript.asCodeEditor("focus");
            $winRestore.asModal("show");
    });
    
     $(asPageEvent).on($.asEvent.page.queryStringChange, function (event,pageUrl,queryString) {
          initRecovery();
          if(interval === null)
             interval = setInterval(autoSave, 5000);
            loadQueryString();
    });
    
    var loadWinComparator = function(sourceControlCode,fileName){
                 
         
                      
            $winSourceComparator.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.sourceComparator)},{name:"@isModal",value:true}])
                                                                                  
            ,{editorCode:$edrJavascript.asCodeEditor("getValue"),sourceControlCode:sourceControlCode,fileName:fileName});
         
         
    }
    
    $(asPageEvent).on("compare",function(event,selectedId){
        $divEditor.asAjax({
            url: $.asInitService($.asUrls.develop_service_getWebPageChange,[
                {name:"@changeId",value:selectedId}
                ,{name:"@codeId",value:serviceId}
                ]),
            type:"get",
            success: function (sql) {
                 loadWinComparator(sql,$txtId.val()+".js");
            }
        }, { validate:false, overlayClass: 'as-overlay-absolute'});
     });
     
    $(asPageEvent).on("changeSetSelected",function(event,selectedId){

        $divEditor.asAjax({
            url: $.asInitService($.asUrls.develop_service_getChange,[
                {name:"@changeId",value:selectedId}
                ,{name:"@codeId",value:serviceId}
                ]),
            type:"get",
            success: function (serviceCode) {
                 $edrJavascript.asCodeEditor('setValue', serviceCode);
             
            }
        }, { validate:false, overlayClass: 'as-overlay-absolute'});
    });
    
    as("#btnSourceControl").click(function () {
              $winSourceManager.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.sourceManager)},{name:"@isModal",value:true}])
            ,{parent:asPageEvent,selectEvent:"changeSetSelected",compareEvent:"compare",getUrl: $.asInitService($.asUrls.develop_service_getChanges, [
             { name: '@codeGuid', value: $txtId.val()}
             ])});

    });
    
    $txtUrl.on('input', function() {
            if($txtUrl.val().toLowerCase().indexOf("odata/") === 0 || $txtUrl.val().toLowerCase().indexOf("odata/") === 1){
                $divCache.hide();
                if($txtUrl.val().toLowerCase().indexOf("?") > -1)
                  $divIsLogOnService.hide();
                else
                  $divIsLogOnService.show();
            }else{
                $divCache.show();
                $divIsLogOnService.show();
             }
        });
    
    $btnGetMetaData.click(function () {
        if ($drpContexts.asDropdown('selected')) {
           $edrJavascript.asAjax({
            url: $drpContexts.asDropdown('selected').value,
            type: "get",
             dataType: "xml",
            success: function (metaData) {
               
                 $edrJavascript.asCodeEditor('setValue',$edrJavascript.asCodeEditor('getValue') + " /*" + metaData.childNodes[0].innerHTML + "*/");
            }
        },{validate:false,overlayClass: "as-overlay-absolute"});
        }else{
            $.asShowMessage({ template: "error", message: "ابتدا یک متا دیتا را انتخاب نمایید  " });
        }
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
                if( $edrJavascript === currentEditor)
                    currentEditor.asCodeEditor('setValue',$.asStorage.getItem("serviceJavascriptQueryEditor" + queryString));
          }
    }
    
     var recoverAllEditor = function(){
          queryString = $.asGetQueryString();
          if(queryString !== null){
                $edrJavascript.asCodeEditor('setValue',$.asStorage.getItem("serviceJavascriptQueryEditor" + queryString));
          }
    }
   
         $edrJavascript.asCodeEditor("editor").commands.addCommand({
    name: 'Find',
    bindKey: {win: 'Ctrl-F',  mac: 'Command-F'},
    exec: function(editor) {
       $winFind.asWindow("show")
    },
    readOnly: true // false if this command should not apply in readOnly mode
});



    $edrJavascript.asCodeEditor("editor").commands.addCommand({
    name: 'CommentToggel',
    bindKey: {win: 'Ctrl-K',  mac: 'Command-K'},
    exec: function(editor) {
       commentToggel()
    },
    readOnly: false // false if this command should not apply in readOnly mode
});


 $edrJavascript.asCodeEditor("editor").commands.addCommand({
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
                     $edrJavascript.asCodeEditor("editor").gotoLine(codeEditorPosition[codeEditorPositionIndex]);
                     if(codeEditorPositionIndex > 1)
                     codeEditorPositionIndex--;

        });
        $btnNext.click(function () {
                
          backOrForwrdFlag=true;

                codeEditorPositionIndex = codeEditorPositionIndex === 0 ? codeEditorPosition.length -1:codeEditorPositionIndex;
                 $edrJavascript.asCodeEditor("editor").gotoLine(codeEditorPosition[codeEditorPositionIndex]);
                 if(codeEditorPositionIndex < codeEditorPosition.length - 1)
                 codeEditorPositionIndex++;

                
        });
         $edrJavascript.asCodeEditor("editor").getSession().on('change', function(e) {
            codeEditorPositionIndex = codeEditorPosition.length -1
         });
   $edrJavascript.asCodeEditor("editor").on("focus", function(){
        currentEditor =$edrJavascript;
       changeEditorToolbar("کوئری");
        backOrForwrdFlag=false;
    });
    
     $edrJavascript.asCodeEditor("editor").getSession().selection
     .on('changeCursor', function(e) {
                codeEditorPosition.push($edrJavascript.asCodeEditor("editor").selection.getCursor().row);
        });
 
   $chkCache.change(function () {
        as("#divSlidingExpirationTimeInMinutes").prop("disabled", !this.checked)
    });
    
      $chkNew.change(function () {
        if(this.checked === true)
       {
            if(isLeaf === true)
         {
                 $.asShowMessage({ template: "error", message: "امکان افزودن سرویس جدید به یک سرویس دیگر وجود ندارد" })
             $chkNew.prop('checked', false)
         }
            else{
            
            $txtId.prop("disabled", false)

            temp.Id = serviceId;
            
            serviceId=0;
            
            temp.Isleaf = isLeaf
            
            temp.ParentId = serviceParentId;
            serviceParentId=0;
            
            temp.RowVersion = rowVersion;
            rowVersion='';

    
              
            temp.JsCode = $edrJavascript.asCodeEditor('getValue');
            $edrJavascript.asCodeEditor('setValue', '');
            
            temp.Name = $txtName.val();
            $txtName.val("")
       
            temp.Description = $txtDescription.val()
            $txtDescription.val("")
            
         
            temp.Code = $txtCode.val()
            $txtCode.val("")
            
            
            temp.PathOrUrl = $txtUrl.val()
            $txtUrl.val("")
            $txtUrl.trigger( "input" );
            temp.Guid = $txtId.val()
            
            temp.NewGuid=codeNewId;
            $txtId.val(codeNewId)
            
           
            temp.NewId = serviceNewId
            $txtServiceId.val(serviceNewId)
            
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
           setService(temp)
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
    })
    $btnCompile.click(function () {
        //$.asOdataQueryBuilder
        $btnCompile.button('loading') ;
        var log =    " var log = function(data){ $('" + asPageId + "').find('#btnCompile').button('reset'); var isFirstGrid = " + isFirstGrid + ";"+
        "var $grvData;  if(!isFirstGrid){$grvData =$('" + asPageId + "').find('#grvData');$grvData.asBootGrid('destroy');} $('#divResult').html('" + tableTemplate + "'); " + 
        "$grvData =$('" + asPageId + "').find('#grvData');" + 
        " if(data){if(data.results){if(data.results[0]){if(data.results[0].value){$.asConsole.dir(data.results[0].value[0]);var index=0;$.each(data.results[0].value[0],function(i,v){ " + 
        " $grvData.find('thead tr').append('<th data-header-align=\"center\" data-align = \"center\" data-column-id=\"' + i + '\" data-order=\"desc\" data-css-class=\"ltr\" '" 
        + " + '>' + i + '</th>');"
        + "index++;});}}}}; if(data){if(data.results){if(data.results[0]){if(data.results[0].value){"
        + " var dataArr = []; for (var i = 0; i < data.results[0].value.length; i++) { dataArr.push(data.results[0].value[i])};"
        + "$grvData.asBootGrid({caseSensitive:false,rowCount:[-1],source:{localData:dataArr}});}}}}}; "
        eval(log +  $edrJavascript.asCodeEditor("getValue"))
    });
    
    $btnDell.click(function () {
        if ($drpService.asDropdown('selected')) {
         $frm.asAjax({
            url:$.asUrls.develop_service_delete,
            data: JSON.stringify({
                Id: $drpService.asDropdown('selected').value,
            }),
            success: function (result) {
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration ,showTime:10000000 });
              $drpService.asDropdown("removeItem");
            }
        }, { $form: $frm });
        }else{
                 $.asShowMessage({ template: "error", message: "برای حذف یک سرویس را انتخاب نمایید" })
        }
    });
    $btnSave.click(function () {
        
               
        var newService;
  
         
        //  $drpService.asDropdown("removeItem");
         
         var id,parentId,guid;
          if ($drpService.asDropdown('selected')) {
              if($chkNew.is(':checked'))
           {
             guid= codeNewId
            parentId = $drpService.asDropdown('selected').value
            id=serviceNewId
           }
            else{
                guid= $txtId.val()
                id=$drpService.asDropdown('selected').value
                parentId=serviceParentId
            }
            
        }
        
          
    
                      
   

         
            
        
     
        if($chkService.is(':checked') === false && $chkNew.is(':checked')){
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

 

        var jsCode = $edrJavascript.asCodeEditor("getValue")

        $.asTemp[queryString].serviceJavascriptQueryEditor = jsCode;
   

        $frm.asAjax({
            url: $.asUrls.develop_service_save,
            data: JSON.stringify({
                Id: id,
                ParentId:parentId,
                Url: $txtUrl.val(),
                Key:$chkIsLogOnService.is(':checked') ? 1:0,
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
                IsLeaf: $chkService.is(':checked'),
                IsNew:$chkNew.is(':checked'),
                CheckIn: $chkCheckIn.is(':checked'),
                Comment:$txtComment.val(),
                SlidingExpirationTimeInMinutes:$txtSlidingExpirationTimeInMinutes.val(),
                RowVersion: rowVersion
            }),
            success: function (service) {
                if($chkNew.is(':checked')){
                    var newParent = false
                        if(newParents[selectedService.value]){
                            newParent = true
                            delete newParents[selectedService.value]
                        }
                       $drpService.asDropdown("addItem",{text:$txtName.val(),value:id},selectedService,newParent)
                }
                 
                setService(service,false)
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration ,showTime:10000000 });
            }
        }, { $form: $frm })

    });

    $drpService.on("change", function (event, item) {
    
         selectedService = item;
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
            id:serviceId,
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

var getService = function (id) {

    isLoadQueryString = true;


                $drpService.asDropdown('selectValue', [], true)
                $drpService.asDropdown('selectValue', id)

            

    viewRoleId = 0
    url = ""
    rowVersion = ""



    $frm.asAjax({
        url: $.asInitService($.asUrls.develop_service_get,[{name:"@id",value:id}]),
        type: "get",
        error:function(){
             isLoadQueryString = false;
        },
        success: function (service) {
           
             if($.isArray(service)){
                  if(service.length > 0)
                    setService(service)
                    else
                     notFound()
            }else{
                 if(service != null){
                        if(typeof(service) != "undefined")
                       setService(service)
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
        $.asStorage.setItem("serviceJavascriptQueryEditor" + queryString,$edrJavascript.asCodeEditor("getValue"))
    }
var changeEditorToolbar = function(editor){
        
        
 
           $lblEditor.html(editor)
     
    
          
    }
    
var setService = function(service,changeEditor){
    console.dir(service);
    if(typeof(changeEditor) == "undefined")
        changeEditor=true
             $txtComment.val("");
             $chkCheckIn.prop('checked', false);
           $txtId.prop("disabled", true)
              $chkNew.prop('checked', false)
            serviceId = service.Id
            
            $txtServiceId.val(serviceId)
            serviceParentId=service.ParentId
            rowVersion = service.RowVersion
            $chkService.prop('checked', service.IsLeaf)

            isLeaf = service.IsLeaf
            if(changeEditor === true){
                if (service.JsCode != null)
                $edrJavascript.asCodeEditor('setValue',service.JsCode);
            else
                $edrJavascript.asCodeEditor('setValue', '');
            }
            as("#divLastModifiUser").html(service.LastModifieUser);
            as("#divLastModifiLocalDataTime").html(service.LastModifieLocalDateTime);
            $txtName.val(service.Name)
            if (service.Description != null)
            $txtDescription.val(service.Description)
            else
            $txtDescription.val("")
            if (service.Code !== null)
            $txtCode.val(service.Code)
            else
            $txtCode.val("")
            if (service.PathOrUrl !== null)
            $txtUrl.val(service.PathOrUrl)
            else
            $txtUrl.val("")
            $txtUrl.trigger( "input" );
            $txtId.val(service.Guid)
            codeNewId = service.NewGuid;
            serviceNewId = service.NewId;
            $txtVersion.val(service.Version)
   
            if (service.Order !== null)
            $txtOrder.val(service.Order)
            else
            $txtOrder.val("")
            $chkIsLogOnService.prop('checked', service.Key === 1 ? true:false)
            $chkCache.prop('checked', service.EnableCache)
            $chkEditMode.prop('checked', service.EditMode)
            $chkStatus.prop('checked', service.Status)

            if (service.ViewRoleId !== null)
                $drpViewRole.asDropdown('selectValue', service.ViewRoleId)
            else
                $drpViewRole.asDropdown('selectValue', [], true)
            if (service.AccessRoleId != null)
                $drpAccessRole.asDropdown('selectValue', service.AccessRoleId)
            else
                $drpAccessRole.asDropdown('selectValue', [], true)
            if (service.ModifyRoleId != null)
                $drpModifyRole.asDropdown('selectValue', service.ModifyRoleId)
            else
                $drpModifyRole.asDropdown('selectValue', [], true)
}
var loadQueryString = function () {
    queryString = $.asGetQueryString()
    if (queryString !== null) {
        var q = queryString.split("/")


        getService(q[0]);




    }
}


