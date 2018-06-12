 $('#ibf36ac466ffa4f60af777dade9a0d175').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('مدیریت وبسرویس های WCF');var asPageEvent = '#ibf36ac466ffa4f60af777dade9a0d175'; var asPageId = '.ibf36ac466ffa4f60af777dade9a0d175.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeIdAndParentId:"/odata/cms/MasterDataKeyValues?$filter=(TypeId%20eq%20@typeIdd)%20and%20(ParentId%20eq%20@idd)&$select=Id%2CParentId%2CCode%2COrder%2CName",develop_code_os_dotNet_getWcfGenreatedCode:"/develop/code/os/dotnet/GetWcfGenreatedCode"}, $.asUrls); var
     $win=$(asPageId),
     $frm=as("#frmWcf"),
    $wcfCodeEditor=as("#wcfCodeEditor"),
    $winFind=as("#winFind"),
     $txtFind=as("#txtFind"),
    $txtReplace=as("#txtReplace"),
    $chkCase=as("#chkFindCase"),
    $chkWhole=as("#chkFindWhole"),
    $chkExp=as("#chkFindExp"),
    $chkSelect=as("#chkFindSelect"),
    $chkStatus= as("#chkStatus"),
    $btnNext=as("#btnNext"),
    $btnPrev=as("#btnPrev"),
    $drpEditors=as("#drpEditorNames"),
    $txtUrl=as("#txtUrl"),
    $txtWcfUsername=as("#txtWcfUsername"),
    $txtWcfPassword=as("#txtWcfPassword"),
    $txtWcfDomain=as("#txtWcfDomain"),
    $txtWcfProxy=as("#txtWcfProxy"),
     $btnSave= as("#btnSave"),
     $wcfEditorDiv = as("#wcfCodeEditor_container"),
    backOrForwrdFlag=false,
    codeEditorPositionIndex=0,
    codeEditorPosition =[],
    currentEditor=null,
    validateFileAction;
         
 $winFind.asWindow({focusedId:"txtFind"});

$wcfCodeEditor.asCodeEditor();

$drpEditors.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: false
        },
          url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeIdAndParentId, [{ name: '@typeId', value: 1030 },{ name: '@id', value: 196 }])
        , displayDataField: 'Name'
          , valueDataField: 'Code',
        orderby: 'Order'
    }
    // , selectParents: true
//  , parentMode: "uniq"

});

        var validateRuleFileAction = {
         txtUrl:{
            required: true
        }
    };
     validateFileAction =$frm.asValidate({ rules: validateRuleFileAction});
     
var autoSave = function(){
        $.asStorage.setItem("wcfCodeEditor",$wcfCodeEditor.asCodeEditor("getValue"))
    }

var setUpWin = function(){
    $wcfEditorDiv.hide();
                    $txtUrl.val("http://");
                    $txtWcfPassword.val("")
                    $txtWcfUsername.val("");
                $wcfCodeEditor.asCodeEditor('setValue', '');
          
                  
                  $drpEditors.asDropdown('selectValue', "csharp");
                  $wcfCodeEditor.asCodeEditor("setEditorMode","csharp");

                  //$drpEditors.asDropdown('selectValue', "vbscript");
                 // $wcfCodeEditor.asCodeEditor("setEditorMode","vbscript");
}

    
    var bindEvent = function(){
 
         $(asPageEvent).on($.asEvent.modal.reopen, function (event,params) {
                setUpWin();
            });
        $btnSave.on('click',function(){
       
         $win.asAjax({
            url: $.asUrls.develop_code_os_dotNet_getWcfGenreatedCode,
            data: JSON.stringify({
                Url:$txtUrl.val(),
                Username:$txtWcfUsername.val(),
                Password:$txtWcfPassword.val(),
                Domain:$txtWcfDomain.val(),
                Proxy:$txtWcfProxy.val(),
                Language:$drpEditors.asDropdown('selected').value
            }),
            success: function (result) {
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
              $wcfEditorDiv.show();
               $wcfCodeEditor.asCodeEditor('setValue', result);
            }
        },{$form: $frm,overlayClass: "as-overlay-absolute"});
            
        });
        //  $btnCancel.on('click', function () {
        //         $win.asModal('hide');
        //     });
            asOnPageDispose = function(){
                  validateFileAction.destroy();
            }
    
          $(asPageEvent).on($.asEvent.page.ready, function (event) {
             setUpWin();
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

    

   
         $wcfCodeEditor.asCodeEditor("editor").commands.addCommand({
    name: 'Find',
    bindKey: {win: 'Ctrl-F',  mac: 'Command-F'},
    exec: function(editor) {
       $winFind.asWindow("show")
    },
    readOnly: true // false if this command should not apply in readOnly mode
});



    $wcfCodeEditor.asCodeEditor("editor").commands.addCommand({
    name: 'CommentToggel',
    bindKey: {win: 'Ctrl-K',  mac: 'Command-K'},
    exec: function(editor) {
       commentToggel()
    },
    readOnly: false // false if this command should not apply in readOnly mode
});



 $btnPrev.click(function () {
              backOrForwrdFlag=true;

                    codeEditorPositionIndex = codeEditorPositionIndex === 0 ? codeEditorPosition.length -1:codeEditorPositionIndex;
                     $wcfCodeEditor.asCodeEditor("editor").gotoLine(codeEditorPosition[codeEditorPositionIndex]);
                     if(codeEditorPositionIndex > 1)
                     codeEditorPositionIndex--;

        });
        $btnNext.click(function () {
                
          backOrForwrdFlag=true;

                codeEditorPositionIndex = codeEditorPositionIndex === 0 ? codeEditorPosition.length -1:codeEditorPositionIndex;
                 $wcfCodeEditor.asCodeEditor("editor").gotoLine(codeEditorPosition[codeEditorPositionIndex]);
                 if(codeEditorPositionIndex < codeEditorPosition.length - 1)
                 codeEditorPositionIndex++;

                
        });
        
         $wcfCodeEditor.asCodeEditor("editor").getSession().on('change', function(e) {
            codeEditorPositionIndex = codeEditorPosition.length -1
         });
    
   $wcfCodeEditor.asCodeEditor("editor").on("focus", function(){
        currentEditor =$wcfCodeEditor;
        backOrForwrdFlag=false;
    });
    
     $wcfCodeEditor.asCodeEditor("editor").getSession().selection
     .on('changeCursor', function(e) {
                codeEditorPosition.push($wcfCodeEditor.asCodeEditor("editor").selection.getCursor().row);
        });
       as("#btnToggleComment").click(function () {
     commentToggel()
    })
     as("#btnFindWindow").click(function () {
      $winFind.asWindow("show")
    });
         $drpEditors.on("change", function (event, item) {
         $wcfCodeEditor.asCodeEditor("setEditorMode",item.value);
     });
    
    
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

    });
    as("#btnRecovery").click(function () {
        recoverEditor()
    })
as("#btnReplace").click(function () {
       currentEditor.asCodeEditor("replace",$txtReplace.val());
    })
    as("#btnReplaceAll").click(function () {
       currentEditor.asCodeEditor("replaceAll",$txtReplace.val());
    })

as("#btnFindNext").click(function () {
       currentEditor.asCodeEditor("findNext");
    })
    
    as("#btnFindPrev").click(function () {
       currentEditor.asCodeEditor("findPrevious");

    })

    
    }
    bindEvent();  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });