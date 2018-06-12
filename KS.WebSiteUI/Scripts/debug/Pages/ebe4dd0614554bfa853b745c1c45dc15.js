 $('#iebe4dd0614554bfa853b745c1c45dc15').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('Source Compare');var asPageEvent = '#iebe4dd0614554bfa853b745c1c45dc15'; var asPageId = '.iebe4dd0614554bfa853b745c1c45dc15.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({cms_masterDataKeyValue_GetByOtherLanguagesAndTypeId:"/odata/cms/MasterDataLocalKeyValues?$filter=(MasterDataKeyValue%2FTypeId%20eq%20@typeIdd)%20and%20(Language%20eq%20'@lang')&$expand=MasterDataKeyValue&$select=MasterDataKeyValue%2FId%2CMasterDataKeyValue%2FParentId%2CMasterDataKeyValue%2FCode%2CMasterDataKeyValue%2FOrder%2CMasterDataKeyValue%2FName%2CMasterDataKeyValue%2FKey%2CMasterDataKeyValue%2FValue%2CMasterDataKeyValue%2FIsLeaf%2CMasterDataKeyValue%2FPathOrUrl%2CName"}, $.asUrls); var
     $win=$(asPageId),
    $fileEditor=as("#fileEditor"),
    $editorsCodeEditor=as("#editorsCodeEditor"),
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
    backOrForwrdFlag=false,
    fileEditorPositionIndex=0,
    fileEditorPosition =[],
    codeEditorPositionIndex=0,
    codeEditorPosition =[],
    currentEditorName ="",
    currentEditor=null;
         
 $winFind.asWindow({focusedId:"txtFind"});

$fileEditor.asCodeEditor();
$editorsCodeEditor.asCodeEditor();
$drpEditors.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'MasterDataKeyValue.Id' },
            parentDataField: { name: 'MasterDataKeyValue.ParentId' },
            removeChildLessParent: false
        },                   
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByOtherLanguagesAndTypeId, [{ name: '@typeId', value: 1030 },{name:'@lang', value:'en'}])
        , displayDataField: 'Name'
          , valueDataField: 'MasterDataKeyValue.Code',
        orderby: 'MasterDataKeyValue.Order'
    }
    // , selectParents: true
//  , parentMode: "uniq"

});

     
var changeEditorToolbar = function(editor){
        
        backOrForwrdFlag=false;
        currentEditorName = editor;
 
           $lblEditor.html(editor)
     
    
          
    }
var setUpWin = function(){
                    $txtFileName.val("");
                $fileEditor.asCodeEditor('setValue', '');
                $editorsCodeEditor.asCodeEditor('setValue', '');
                  
             var extention = asPageParams.fileName.toLowerCase().split(".");
            
           if(asPageParams.fileName.toLowerCase().indexOf(".js") > -1){
               $drpEditors.asDropdown('selectValue', "javascript");
                $fileEditor.asCodeEditor("setEditorMode","javascript")
                $editorsCodeEditor.asCodeEditor("setEditorMode","javascript")
           }else if(asPageParams.fileName.toLowerCase().indexOf(".ts") > -1){
                  $fileEditor.asCodeEditor("setEditorMode","typescript")
                  $editorsCodeEditor.asCodeEditor("setEditorMode","typescript")
                  $drpEditors.asDropdown('selectValue', "typescript");
           }else if(asPageParams.fileName.toLowerCase().indexOf(".txt") > -1){
                  $fileEditor.asCodeEditor("setEditorMode","text")
                  $editorsCodeEditor.asCodeEditor("setEditorMode","text")
                  $drpEditors.asDropdown('selectValue', "text");
           }else if(asPageParams.fileName.toLowerCase().indexOf(".config") > -1){
                  $fileEditor.asCodeEditor("setEditorMode","xml")
                  $editorsCodeEditor.asCodeEditor("setEditorMode","xml")
                  $drpEditors.asDropdown('selectValue', "xml");
           }else if(asPageParams.fileName.toLowerCase().indexOf(".cs") > -1 && asPageParams.fileName.toLowerCase().indexOf(".css") === -1){
                  $fileEditor.asCodeEditor("setEditorMode","csharp")
                  $editorsCodeEditor.asCodeEditor("setEditorMode","csharp")
                  $drpEditors.asDropdown('selectValue', "csharp");
           }else if(asPageParams.fileName.toLowerCase().indexOf(".vb") > -1){
                  $fileEditor.asCodeEditor("setEditorMode","vbscript")
                   $editorsCodeEditor.asCodeEditor("setEditorMode","vbscript")
                  $drpEditors.asDropdown('selectValue', "vbscript");
           }else{
               $drpEditors.asDropdown('selectValue', extention[extention.length -1]);
                    $fileEditor.asCodeEditor("setEditorMode",extention[extention.length -1]);
                    $editorsCodeEditor.asCodeEditor("setEditorMode",extention[extention.length -1]);
           }
                  
             $txtFileName.val(asPageParams.fileName);
                 $fileEditor.asCodeEditor('setValue', asPageParams.sourceControlCode);
                 $editorsCodeEditor.asCodeEditor('setValue', asPageParams.editorCode);
        
}

    
    var bindEvent = function(){
 
         $(asPageEvent).on($.asEvent.modal.reopen, function (event,params) {
            if(params.fileName !== asPageParams.fileName || params.editorCode !== asPageParams.editorCode || asPageParams.sourceControlCode !== params.sourceControlCode){
                asPageParams=params;
                setUpWin();
            }});
  
         $btnCancel.on('click', function () {
                $win.asModal('hide');
            });
            asOnPageDispose = function(){
                
            }
    
          $(asPageEvent).on($.asEvent.page.ready, function (event) {
            // $fileEditor.asCodeEditor("focus");
            //  $drpEditors.asDropdown('selectValue', "text");
            //   $fileEditor.asCodeEditor("setEditorMode","text");
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

         $editorsCodeEditor.asCodeEditor("editor").commands.addCommand({
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

    $editorsCodeEditor.asCodeEditor("editor").commands.addCommand({
    name: 'CommentToggel',
    bindKey: {win: 'Ctrl-K',  mac: 'Command-K'},
    exec: function(editor) {
       commentToggel()
    },
    readOnly: false // false if this command should not apply in readOnly mode
});



 $btnPrev.click(function () {
              backOrForwrdFlag=true;
              
              
               switch (currentEditorName) {
                case "Source Control":
                    fileEditorPositionIndex = fileEditorPositionIndex === 0 ? fileEditorPosition.length -1:fileEditorPositionIndex;
                     currentEditor.asCodeEditor("editor").gotoLine(fileEditorPosition[fileEditorPositionIndex]);
                     if(fileEditorPositionIndex > 1)
                     fileEditorPositionIndex--;
                    break;
                case "Editor":
                    codeEditorPositionIndex = codeEditorPositionIndex === 0 ? codeEditorPosition.length -1:codeEditorPositionIndex;
                     currentEditor.asCodeEditor("editor").gotoLine(codeEditorPosition[codeEditorPositionIndex]);
                     if(codeEditorPositionIndex > 1)
                     codeEditorPositionIndex--;
                    break;
            }
            
            



        });
        $btnNext.click(function () {
                
          backOrForwrdFlag=true;
          
          
           switch (currentEditorName) {
                case "Source Control":
                        fileEditorPositionIndex = fileEditorPositionIndex === 0 ? fileEditorPosition.length -1:fileEditorPositionIndex;
                         currentEditor.asCodeEditor("editor").gotoLine(fileEditorPosition[fileEditorPositionIndex]);
                         if(fileEditorPositionIndex < fileEditorPosition.length - 1)
                         fileEditorPositionIndex++;
                    break;
                case "Editor":
                        codeEditorPositionIndex = codeEditorPositionIndex === 0 ? codeEditorPosition.length -1:codeEditorPositionIndex;
                         currentEditor.asCodeEditor("editor").gotoLine(codeEditorPosition[codeEditorPositionIndex]);
                         if(codeEditorPositionIndex < codeEditorPosition.length - 1)
                         codeEditorPositionIndex++;
                    break;
            }



                
        });
        
         $fileEditor.asCodeEditor("editor").getSession().on('change', function(e) {
            fileEditorPositionIndex = fileEditorPosition.length -1
         });
         
          $editorsCodeEditor.asCodeEditor("editor").getSession().on('change', function(e) {
            codeEditorPositionIndex = codeEditorPosition.length -1
         });
    
   $fileEditor.asCodeEditor("editor").on("focus", function(){
        currentEditor =$fileEditor;
       changeEditorToolbar("Source Control");
        backOrForwrdFlag=false;
    });
    
    $editorsCodeEditor.asCodeEditor("editor").on("focus", function(){
        currentEditor =$editorsCodeEditor;
       changeEditorToolbar("Editor");
        backOrForwrdFlag=false;
    });
    
     $fileEditor.asCodeEditor("editor").getSession().selection
     .on('changeCursor', function(e) {
                fileEditorPosition.push($fileEditor.asCodeEditor("editor").selection.getCursor().row);
        });
        
      $editorsCodeEditor.asCodeEditor("editor").getSession().selection
     .on('changeCursor', function(e) {
                codeEditorPosition.push($editorsCodeEditor.asCodeEditor("editor").selection.getCursor().row);
        });
        
       as("#btnToggleComment").click(function () {
     commentToggel()
    })
     as("#btnFindWindow").click(function () {
      $winFind.asWindow("show")
    });
         $drpEditors.on("change", function (event, item) {
         $fileEditor.asCodeEditor("setEditorMode",item.value);
         $editorsCodeEditor.asCodeEditor("setEditorMode",item.value);
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