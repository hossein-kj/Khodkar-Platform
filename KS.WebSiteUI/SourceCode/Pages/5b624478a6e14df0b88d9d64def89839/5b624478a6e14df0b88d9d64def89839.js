var
     $win=$(asPageId),
     $frm=as("#frmAddOrUpdateFile"),
    $fileEditor=as("#fileEditor"),
    $winFind=as("#winFind"),
     $txtFind=as("#txtFind"),
    $txtReplace=as("#txtReplace"),
    $chkCase=as("#chkFindCase"),
    $chkWhole=as("#chkFindWhole"),
    $chkExp=as("#chkFindExp"),
    $chkSelect=as("#chkFindSelect"),
    $chkStatus= as("#chkStatus"),
    $lblEditor=as("#lblEditor"),
    $btnNext=as("#btnNext"),
    $btnPrev=as("#btnPrev"),
    $drpEditors=as("#drpEditorNames"),
    $txtFileName=as("#txtFileName"),
    $btnCancel=as("#btnCancel"),
     $btnSave= as("#btnSave"),
      fileActionMode="add",
    backOrForwrdFlag=false,
    codeEditorPositionIndex=0,
    codeEditorPosition =[],
    currentEditor=null,
    validateFileAction;
         
 $winFind.asWindow({focusedId:"txtFind"});

$fileEditor.asCodeEditor();

$drpEditors.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: false
        },
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId, [{ name: '@typeId', value: 1030 }])
        , displayDataField: 'Name'
          , valueDataField: 'Code',
        orderby: 'Order'
    }
    // , selectParents: true
//  , parentMode: "uniq"

});

        var validateRuleFileAction = {
         txtFileName:{
            required: true
        }
    };
     validateFileAction =$frm.asValidate({ rules: validateRuleFileAction});
     
var autoSave = function(){
        $.asStorage.setItem("fileEditor",$fileEditor.asCodeEditor("getValue"))
    }
var changeEditorToolbar = function(editor){
        
        
 
           $lblEditor.html(editor)
     
    
          
    }
var setUpWin = function(){
                    $txtFileName.val("");
                $fileEditor.asCodeEditor('setValue', '');
              if(asPageParams.fileActionMode === "update") {
                  
             var extention = asPageParams.files[0].toLowerCase().split(".");
            
           if(asPageParams.files[0].toLowerCase().indexOf(".js") > -1){
               $drpEditors.asDropdown('selectValue', "javascript");
                $fileEditor.asCodeEditor("setEditorMode","javascript")
           }else if(asPageParams.files[0].toLowerCase().indexOf(".ts") > -1){
                  $fileEditor.asCodeEditor("setEditorMode","typescript")
                  $drpEditors.asDropdown('selectValue', "typescript");
           }else if(asPageParams.files[0].toLowerCase().indexOf(".txt") > -1){
                  $fileEditor.asCodeEditor("setEditorMode","text")
                  $drpEditors.asDropdown('selectValue', "text");
           }else if(asPageParams.files[0].toLowerCase().indexOf(".config") > -1){
                  $fileEditor.asCodeEditor("setEditorMode","xml")
                  $drpEditors.asDropdown('selectValue', "xml");
           }else if(asPageParams.files[0].toLowerCase().indexOf(".cs") > -1 && asPageParams.files[0].toLowerCase().indexOf(".css") === -1){
                  $fileEditor.asCodeEditor("setEditorMode","csharp")
                  $drpEditors.asDropdown('selectValue', "csharp");
           }else if(asPageParams.files[0].toLowerCase().indexOf(".vb") > -1){
                  $fileEditor.asCodeEditor("setEditorMode","vbscript")
                  $drpEditors.asDropdown('selectValue', "vbscript");
           }else if(asPageParams.files[0].toLowerCase().indexOf(".sql") > -1){
                  $fileEditor.asCodeEditor("setEditorMode","sqlserver")
                  $drpEditors.asDropdown('selectValue', "sqlserver");
           }else{
               $drpEditors.asDropdown('selectValue', extention[extention.length -1]);
                    $fileEditor.asCodeEditor("setEditorMode",extention[extention.length -1]);
           }
                  
             $txtFileName.val(asPageParams.files[0]);

          $win.asAjax({
            url:$.asInitService($.asUrls.fms_get,[{name:"@path",value:$.asUrlAsParameter((asPageParams.path + "/" + asPageParams.files[0]).replace(new RegExp($.asUrlDelimeter, "gi"),"/").replace(new RegExp("//", "gi"),"/")) + "/"}]),
             type:"get",
            success: function (result) {
                $fileEditor.asCodeEditor('setValue', result);
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
            }
        },{validate:false,overlayClass: "as-overlay-absolute"});
      
        } 
}

    
    var bindEvent = function(){
 
         $(asPageEvent).on($.asEvent.modal.reopen, function (event,params) {
            if(params.files !== asPageParams.files || params.fileActionMode !== asPageParams.fileActionMode || asPageParams.path !== params.path
            || params.parent !== asPageParams.parent || params.event !== asPageParams.event){
                asPageParams=params;
                setUpWin();
            }});
        $btnSave.on('click',function(){
       
         $win.asAjax({
            url: $.asUrls.fms_save,
            data: JSON.stringify({
                Path: asPageParams.path.replace(new RegExp($.asUrlDelimeter, "gi"),"/").replace(new RegExp("//", "gi"),"/"),
                Name:$txtFileName.val(),
                Content:$fileEditor.asCodeEditor("getValue")
            }),
            success: function (result) {
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
              if(asPageParams.fileActionMode === "add"){
                   {
                       $(asPageParams.parent).trigger(asPageParams.event, [{name:$txtFileName.val(),modifieLocalDateTime:result}]);
                       $win.asModal("hide");
                   }
             
              }
            }
        },{$form: $frm,overlayClass: "as-overlay-absolute"});
            
        });
         $btnCancel.on('click', function () {
                $win.asModal('hide');
            });
            asOnPageDispose = function(){
                  validateFileAction.destroy();
            }
    
          $(asPageEvent).on($.asEvent.page.ready, function (event) {
            $fileEditor.asCodeEditor("focus");
             $drpEditors.asDropdown('selectValue', "text");
               $fileEditor.asCodeEditor("setEditorMode","text");
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

    

   
         $fileEditor.asCodeEditor("editor").commands.addCommand({
    name: 'Find',
    bindKey: {win: 'Ctrl-F',  mac: 'Command-F'},
    exec: function(editor) {
       $winFind.asWindow("show")
    },
    readOnly: true // false if this command should not apply in readOnly mode
});



    $fileEditor.asCodeEditor("editor").commands.addCommand({
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
                     $fileEditor.asCodeEditor("editor").gotoLine(codeEditorPosition[codeEditorPositionIndex]);
                     if(codeEditorPositionIndex > 1)
                     codeEditorPositionIndex--;

        });
        $btnNext.click(function () {
                
          backOrForwrdFlag=true;

                codeEditorPositionIndex = codeEditorPositionIndex === 0 ? codeEditorPosition.length -1:codeEditorPositionIndex;
                 $fileEditor.asCodeEditor("editor").gotoLine(codeEditorPosition[codeEditorPositionIndex]);
                 if(codeEditorPositionIndex < codeEditorPosition.length - 1)
                 codeEditorPositionIndex++;

                
        });
        
         $fileEditor.asCodeEditor("editor").getSession().on('change', function(e) {
            codeEditorPositionIndex = codeEditorPosition.length -1
         });
    
   $fileEditor.asCodeEditor("editor").on("focus", function(){
        currentEditor =$fileEditor;
       changeEditorToolbar("ویرایشگر");
        backOrForwrdFlag=false;
    });
    
     $fileEditor.asCodeEditor("editor").getSession().selection
     .on('changeCursor', function(e) {
                codeEditorPosition.push($fileEditor.asCodeEditor("editor").selection.getCursor().row);
        });
       as("#btnToggleComment").click(function () {
     commentToggel()
    })
     as("#btnFindWindow").click(function () {
      $winFind.asWindow("show")
    });
         $drpEditors.on("change", function (event, item) {
         $fileEditor.asCodeEditor("setEditorMode",item.value);
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
    bindEvent();