 $('#i23cd2e374f1e4b189a2e5a9ad8c76da3').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('Add or Updatet languages and cultures');var asPageEvent = '#i23cd2e374f1e4b189a2e5a9ad8c76da3'; var asPageId = '.i23cd2e374f1e4b189a2e5a9ad8c76da3.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({cms_languageAndCulture_save:"/cms/languageAndCulture/save",cms_languageAndCulture_public_getAll:"/cms/languageAndCulture/public/getAll",security_Role_getAllByOtherLanguage:"/odata/security/LocalRoles?$filter=(Language%20eq%20'@lang')&$expand=Role&$select=Role%2FId%2CRole%2FParentId%2CRole%2FName%2CRole%2FOrder%2CRole%2FIsLeaf%2CName",cms_languageAndCulture_byJsFile_get:"/cms/languageAndCulture/Get/@id",cms_languageAndCulture_delete:"/cms/languageAndCulture/delete"}, $.asUrls); var 
 $frm = as("#frmLang"),
    $divRoles = as("#divRoles"),
    $edrJavascript= as("#edrJavascript"),
    $drpViewRole= as("#drpViewRole"),
    $drpModifyRole= as("#drpModifyRole"),
    $drpAccessRole= as("#drpAccessRole"),
    $btnPathSelector=as("#btnPathSelector"),
    $winPathSelector=$.asModalManager.get({url:$.asModalManager.urls.pathSelector}),
    $winRestore=as("#winRestorEditor"),
    $winFind=as("#divFind"),
    $drpLanguge= as("#drpLanguge"),
    $txtLang=as("#txtLang"),
    $txtCulture=as("#txtCulture"),
    $txtCountry=as("#txtCountry"),
    $txtFind=as("#txtFind"),
    $txtReplace=as("#txtReplace"),
    $chkCase=as("#chkFindCase"),
    $chkHole=as("#chkFindHole"),
    $chkExp=as("#chkFindExp"),
    $chkSelect=as("#chkFindSelect"),
    $chkStatus= as("#chkStatus"),
    $chkDefaultsLang= as("#chkDefaultsLang"),
    $chkIsRightToLeft= as("#chkIsRightToLeft"),
    $chkPublish=as("#chkPublish"),
    $chkNew=as("#chkNew"),
    $editorTools=as("#editorToolbar"),
    $lblEditor=as("#lblEditor"),
    $imgFlag=as("#imgFlag"),
    $btnSave=as("#btnSave"),
    $btnDell=as("#btnDell"),
    $btnNext=as("#btnNext"),
    $btnPrev=as("#btnPrev"),
    $btnDell=as("#btnDell"),
     backOrForwrdFlag=false,
    codeEditorPositionIndex=0,
    codeEditorPosition =[],
    currentEditor=null,
    interval,
    temp = {},
    selectedLanguageAndCulture,
    rowVersion= "",
    flagId=0,
    flagUrl,
    languageAndCultureId=0,
    viewRoleId= 0,
    modifyRoleId= 0,
    accessRoleId= 0,
     languageAndCultures,
     langParams;
    
    $.asTemp.langJavascriptQueryEditor = $.asTemp.langJavascriptQueryEditor || "";

     if($.asTemp.langJavascriptQueryEditor !== "")
         $.asStorage.setItem("langJavascriptQueryEditor",$.asTemp.langJavascriptQueryEditor)

  $winPathSelector.asModal({width:800})   
  $winFind.asWindow({focusedId:"txtFind"})
$winRestore.asModal(
    {backdrop:'static', keyboard: false}
    )

$edrJavascript.asCodeEditor();

var loadRoles = function () {
    return $.asAjaxTask({
         url: $.asInitService($.asUrls.security_Role_getAllByOtherLanguage, [{ name: '@lang', value: $.asLang }])
    });
}

as("#divRoles").asAfterTasks([
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

    var loadLanguages = function () {
    return $.asAjaxTask({
        url: $.asUrls.cms_languageAndCulture_public_getAll
    });
}


$drpLanguge.asAfterTasks([
    loadLanguages()
], function (languages) {
    languageAndCultures=languages;
    langParams = {
        source: {
            displayDataField: 'country',
            valueDataField: 'language',
            urlDataField: 'flagUrl',
            idDataField: 'id',
            exteraDataField: 'isRightToLeft',
            localData: languages
        }
    }

     var lang = $.asGetDefaultsLnguageAndCulter()
    if (lang) {
        langParams.selectedValue = lang.country
      
    }

    $drpLanguge.asFlexSelect(langParams);
},{overlayClass:"as-overlay-fixed"})

var autoSave = function(){
        $.asStorage.setItem("langJavascriptQueryEditor",$edrJavascript.asCodeEditor("getValue"))
    }
var changeEditorToolbar = function(editor){
        
        
 
           $lblEditor.html(editor)
     
    
          
    }
    var validateRule = {
        txtLang:{
            required: true
        },
        txtCountry: {
            required: true
        },
        txtLang: {
            required: true
        },
        txtCulture: {
            required: true
        },drpViewRole: {
            asType: 'asDropdown',
            required: true
        },
        drpAccessRole: {
            asType: 'asDropdown',
            required: true
        },drpModifyRole: {
            asType: 'asDropdown',
            required: true
        }

    }
       validate =$frm.asValidate({ rules: validateRule});
       
var notFound = function(){
 $.asNotFound("Language and culture")
}
var bindEvent = function(){
    
            asOnPageDispose = function(){
        window.clearInterval(interval);
        validate.destroy();
    }
    
          $(asPageEvent).on($.asEvent.page.ready, function (event) {
            $edrJavascript.asCodeEditor("focus");
            $winRestore.asModal("show");
    });
    


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
        if( $edrJavascript === currentEditor)
        currentEditor.asCodeEditor('setValue',$.asStorage.getItem("langJavascriptQueryEditor"));
    }
    
     var recoverAllEditor = function(){
        $edrJavascript.asCodeEditor('setValue',$.asStorage.getItem("langJavascriptQueryEditor"));
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
       changeEditorToolbar("ترجمه");
        backOrForwrdFlag=false;
    });
    
     $edrJavascript.asCodeEditor("editor").getSession().selection
     .on('changeCursor', function(e) {
                codeEditorPosition.push($edrJavascript.asCodeEditor("editor").selection.getCursor().row);
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
      $chkHole.change(function () {
       find()

    })
      $chkExp.change(function () {
       find()

    })
    $chkSelect.change(function () {
       find()

    })
         as("#btnEditorResize").click(function () {
      
        resize();
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
     
        interval = setInterval(autoSave, 5000);
    })
    as("#btnRestorPerviousEditors").click(function () {
       $winRestore.asModal('hide');
         recoverAllEditor();
    })
    
    
    $btnPathSelector.click(function () {
        $winPathSelector.asModal('load',$.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.pathSelector)},{name:"@isModal",value:true}])
        ,{parent:asPageEvent,event:"flagSelected"})
    });
      $(asPageEvent).on("flagSelected", function (event,selectedUrl,selectedId) {
          flagId=selectedId
            $imgFlag.html('<img src="' + ($.asThumbnailPath.replace("~","") + selectedUrl).replace("//","/") + '">')
    });
    var setLanguageAndCulture = function(languageAndCulture){
      
        $txtLang.val(languageAndCulture.Language)
        $txtCulture.val(languageAndCulture.Culture)
        $txtCountry.val(languageAndCulture.Country)
        $chkStatus.prop('checked', languageAndCulture.Status)
        $chkDefaultsLang.prop('checked', languageAndCulture.IsDefaults)
        $chkIsRightToLeft.prop('checked', languageAndCulture.IsRightToLeft)
        $edrJavascript.asCodeEditor('setValue', languageAndCulture.JsCode);
        rowVersion=languageAndCulture.RowVersion
        flagId=languageAndCulture.FlagId
        flagUrl=languageAndCulture.FlagUrl
        if(flagUrl !== "")
        $imgFlag.html('<img src="/files/thumbnail' + flagUrl + '">')
        else
        $imgFlag.html("")
            if (languageAndCulture.ViewRoleId !== null)
                $drpViewRole.asDropdown('selectValue', languageAndCulture.ViewRoleId)
            else
                $drpViewRole.asDropdown('selectValue', [], true)
                
            if (languageAndCulture.AccessRoleId != null)
                $drpAccessRole.asDropdown('selectValue', languageAndCulture.AccessRoleId)
            else
                $drpAccessRole.asDropdown('selectValue', [], true)
                
            if (languageAndCulture.ModifyRoleId != null)
                $drpModifyRole.asDropdown('selectValue', languageAndCulture.ModifyRoleId)
            else
                $drpModifyRole.asDropdown('selectValue', [], true)
    }
        $drpLanguge.on("selectedIndexChanged", function (event, id, value, text, url, extera) {
        if (url)
            $drpLanguge.asFlexSelect('setItem', '<img src="' + url + '">&nbsp;<span class="caret"></span>')
       
             $frm.asAjax({
                url:$.asInitService($.asUrls.cms_languageAndCulture_byJsFile_get, [{ name: '@id', value: id }]) ,
                type: "get",
                success: function (languageAndCulture) {
                selectedLanguageAndCulture= languageAndCulture
                languageAndCultureId=languageAndCulture.Id
                  if(languageAndCulture !== null){
                      setLanguageAndCulture(languageAndCulture)
            }else
            notFound()
                }
            }, {overlayClass: 'as-overlay-absolute' });
    
    });
    
        $chkNew.change(function () {
        if(this.checked === true)
       {
           setLanguageAndCulture({
               Language:"",
               Culture:"",
               Country:"",
               Status:false,
               IsDefaults:false,
               IsRightToLeft:false,
               JsCode:"",
               RowVersion:"",
               FlagId:0,
               FlagUrl:"",
               ModifyRoleId:null,
               ViewRoleId:null,
               AccessRoleId:null
           })
  
            
       }else{
           setLanguageAndCulture(selectedLanguageAndCulture)
       }
    });
    $btnDell.click(function () {
        if(languageAndCultureId === 0){
            $.asShowMessage({ template: "error", message: "To remove , Select a language and culture"})
            return;
        }
         $frm.asAjax({
            url: $.asUrls.cms_languageAndCulture_delete,
            data: JSON.stringify({
                Id: languageAndCultureId
            }),
            success: function (result) {
                var selected = $.asFindInJsonArray(languageAndCultures,"id",languageAndCultureId)
                var index = languageAndCultures.indexOf(selected);
                if (index > -1) {
                    languageAndCultures.splice(index, 1);
                }
                langParams.localData = languageAndCultures;
                $drpLanguge.asFlexSelect(langParams);
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
            }
        }, { $form: $frm });
    });
    $btnSave.click(function () {
        if(flagId===0){
                 $.asShowMessage({template: "error", message:  "Flag image not selected." });
                 return;
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
         $frm.asAjax({
            url: $.asUrls.cms_languageAndCulture_save,
            data: JSON.stringify({
                Id: languageAndCultureId,
                isNew:$chkNew.is(':checked'),
                FlagId:flagId,
                publish: $chkPublish.is(':checked'),
                ViewRoleId: viewRoleId,
                ModifyRoleId: modifyRoleId,
                AccessRoleId: accessRoleId,
                Language:$txtLang.val(),
                Country:$txtCountry.val(),
                Culture: $txtCulture.val(),
                IsDefaults: $chkDefaultsLang.is(':checked'),
                IsRightToLeft:$chkIsRightToLeft.is(':checked'),
                Status: $chkStatus.is(':checked'),
                JsCode: $edrJavascript.asCodeEditor("getValue"),
                RowVersion: rowVersion
            }),
            success: function (languageAndCulture) {
          
                flagUrl=selectedLanguageAndCulture.FlagUrl
            selectedLanguageAndCulture=languageAndCulture
            selectedLanguageAndCulture.JsCode = $edrJavascript.asCodeEditor("getValue")
                 selectedLanguageAndCulture.FlagUrl=flagUrl
                 
                setLanguageAndCulture(selectedLanguageAndCulture)
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
            }
        }, { $form: $frm })
    });
  
}
bindEvent()
  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent();
if (typeof (requestedUrl) != 'undefined')  {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });