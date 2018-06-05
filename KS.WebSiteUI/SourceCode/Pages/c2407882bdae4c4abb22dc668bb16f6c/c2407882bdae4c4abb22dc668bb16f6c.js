var
     $win=$(asPageId),
     $frm=as("#frmMigration"),
    $migCodeEditor=as("#migCodeEditor"),
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
    $txtDbMigrationsConfiguration=as("#txtDbMigrationsConfiguration"),
    $txtRootNamespace=as("#txtRootNamespace"),
    $txtMigration=as("#txtMigration"),
    $btnExecute= as("#btnExecute"),
    $btnUpdateMigration=as("#btnUpdateMigration"),
    $migEditorDiv = as("#migCodeEditor_container"),
    $drpConnection=as("#drpConnection"),
    $drpVersion=as("#drpDllVersion"),
    $drpClass=as("#drpClass"),
    $drpMigrations=as("#drpMigrations"),
    $divRunMigrations=as("#divRunMigrations"),
    $divNewMigration=as("#divNewMigration"),
    $drpAction=as("#drpAction"),
    $divScript=as("#divScript"),
    $drpSourceMigration=as("#drpSourceMigration"),
    $drpTargetMigration=as("#drpTargetMigration"),
    $chkForce=as("#chkForce"),
    selectedAction=1,
    selectedVersion=0,
    selectedConnectionId=0,
    selectedLanguage,
    backOrForwrdFlag=false,
    codeEditorPositionIndex=0,
    codeEditorPosition =[],
    currentEditor=null,
    validateMigration;
$divNewMigration.show();
$divRunMigrations.hide();
$divScript.hide();
 $winFind.asWindow({focusedId:"txtFind"});

$migCodeEditor.asCodeEditor();
$drpMigrations.asDropdown("init","First, select the Configuration class and assembly version",{
    source: {
         displayDataField: 'Name'
          , valueDataField: 'Name',
        orderby: 'Name'
    }
});

$drpTargetMigration.asDropdown("init"," First, select the Configuration class and assembly version",{
    source: {
         displayDataField: 'Name'
          , valueDataField: 'Name',
        orderby: 'Name'
    }
});

$drpSourceMigration.asDropdown("init"," First, select the Configuration class and assembly version",{
    source: {
         displayDataField: 'Name'
          , valueDataField: 'Name',
        orderby: 'Name'
    }
});


$drpConnection.asDropdown({
    source: {
         url: $.asInitService( $.asUrls.develop_code_database_sqlserver_connectionsByOtherLanguage, [{ name: '@lang', value: $.asLang }])
        , displayDataField: 'Value'
          , valueDataField: 'Key',
        orderby: 'Value'
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
    // , selectParents: true
//  , parentMode: "uniq"

});

        var validateRuleMigration = {
         drpClass:{
           asType: 'asDropdown',
            required: true
        },
         drpDllVersion:{
           asType: 'asDropdown',
            required: true
        },
        txtRootNamespace:{
         required: {
                depends: function (element) {
                return $divNewMigration.is(":visible");
                }
            }
        },
          txtMigration:{
         required: {
                depends: function (element) {
                return $divNewMigration.is(":visible");
                }
            }
        }
    };
     validateMigration =$frm.asValidate({ rules: validateRuleMigration});
     
var autoSave = function(){
        $.asStorage.setItem("migCodeEditor",$migCodeEditor.asCodeEditor("getValue"))
    }

var setUpWin = function(){
    


$drpClass.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: false
        },
            url:  $.asInitService( $.asUrls.develop_code_os_dotNet_getNamespaceAndClassesByDllId, [{ name: '@dllId', value: asPageParams.codeId }])
        , displayDataField: 'Name'
          , valueDataField: 'Id',
        orderby: 'Order'
    }
    , multiple: false
    , selectParents: false
//  , parentMode: "uniq"

});

    $drpVersion.asDropdown({
    source: {
        url:  $.asInitService( $.asUrls.develop_code_os_dotNet_getOutputVersionNumbers, [{ name: '@codeId', value: asPageParams.codeId }])
        , displayDataField: 'Value'
          , valueDataField: 'Id',
        orderby: 'Id'
    }

});
$drpAction.asDropdown({
    source: {
        localData:[{Value:"Run Migration",Key:1},{Value:"Add New Migration ",Key:2},{Value:"Genrate Script Between 2 Migration  ",Key:3}]
        , displayDataField: 'Value'
          , valueDataField: 'Key',
        orderby: 'Value'
    }
});
selectedAction=1;
$drpAction.asDropdown('selectValue', 1);
$drpAction.trigger("change");

    $migEditorDiv.hide();
             $txtMigration.val("");
                    $txtRootNamespace.val("");
                $migCodeEditor.asCodeEditor('setValue', '');
          
                  selectedLanguage="csharp";
                  $drpEditors.asDropdown('selectValue', "csharp");
                  $migCodeEditor.asCodeEditor("setEditorMode","csharp");

                  //$drpEditors.asDropdown('selectValue', "vbscript");
                 // $migCodeEditor.asCodeEditor("setEditorMode","vbscript");
}

    
    var bindEvent = function(){
 
         $(asPageEvent).on($.asEvent.modal.reopen, function (event,params) {
             
            if(params.codeId !== asPageParams.codeId){
                asPageParams=params;
                    setUpWin();
            }
            
               
            });
    var loadData = function (url) {
        return $.asAjaxTask({
            url: url
        });
    }
            var loadMigrationClasses = function(){
              if(selectedVersion > 0 && $drpClass.asDropdown('selected') && selectedAction !== 2){
                    as("#divMigrations").asAfterTasks([
                       loadData($.asInitService($.asUrls.develop_code_os_dotNet_getDbMigrationClasses,
                       [
                           { name: '@dllVersion', value: selectedVersion },
                           { name: '@configurationClassId', value: $drpClass.asDropdown('selected').value }
                        ]))
                    ], function (migrations) {
                        var classes = [];
                     
                        $.each(migrations,function(key,value){
                            classes.push({Name:value})
                        });
                        $drpMigrations.asDropdown("reload",{localData: classes});
                        $drpTargetMigration.asDropdown("reload",{localData: classes});
                        classes.push({Name:"Empty Database"});
                        $drpSourceMigration.asDropdown("reload",{localData: classes});
                    },{overlayClass: 'as-overlay-absolute-no-height'});
                }
            }
            

       $drpAction.on("change", function (event, item) {
           if(item)
           selectedAction=item.value;
           if(selectedAction === 2)
           {
               $divNewMigration.show();
               $divRunMigrations.hide();
               $divScript.hide();
           }else if(selectedAction === 1){
               $divNewMigration.hide();
                $divRunMigrations.show();
                $divScript.hide();
           }else{
               $divNewMigration.hide();
                $divRunMigrations.hide();
                $divScript.show();
           }
        });
        
        
        $drpVersion.on("change", function (event, item) {
            selectedVersion=item.value;
            loadMigrationClasses();
        });
        
         $drpClass.on("change", function (event, item) {
            loadMigrationClasses();
        });
            
      $drpConnection.on("change", function (event, item) {
          selectedConnectionId = item.value;
      });
      
        $btnExecute.on('click',function(){
       
         if(selectedConnectionId===0){
            $.asShowMessage({ template: "error", message: " Choose a connection to run" })
        }else{
          
                if(selectedAction===2){
                     $win.asAjax({
                        url:$.asUrls.develop_code_os_dotNet_generateMigration,
                        data: JSON.stringify({
                            Name:$txtMigration.val(),
                            Language:selectedLanguage,
                            ConnectionId:selectedConnectionId,
                            ConfigurationCodeId: $drpClass.asDropdown('selected').value,
                            DllVersion : selectedVersion,
                            RootNamespace : $txtRootNamespace.val(),
                            Force:$chkForce.is(':checked')
                        }),
                        success: function (result) {
                          $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
                          $migEditorDiv.show();
                          $migCodeEditor.asCodeEditor("setEditorMode",selectedLanguage);
                           $migCodeEditor.asCodeEditor('setValue', result.UserCode + "\n" + result.DesignerCode);
                        }
                         },{$form: $frm,validate:true,overlayClass: "as-overlay-absolute"});
                }else if(selectedAction===1){
                  
                     $win.asAjax({
                        url:$.asUrls.develop_code_os_dotNet_runMigration,
                        data: JSON.stringify({
                            Name:$drpMigrations.asDropdown('selected').value,
                            Language:selectedLanguage,
                            ConnectionId:selectedConnectionId,
                            ConfigurationCodeId: $drpClass.asDropdown('selected').value,
                            DllVersion : selectedVersion,
                            RootNamespace : $txtRootNamespace.val(),
                            Force:$chkForce.is(':checked')
                        }),
                        success: function (result) {
                          $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
                        }
                         },{$form: $frm,validate:true,overlayClass: "as-overlay-absolute"});
                }else{
                     $win.asAjax({
                        url:$.asUrls.develop_code_os_dotNet_getMigrationScript,
                        data: JSON.stringify({
                            TargetName:$drpTargetMigration.asDropdown('selected').value,
                            SourceName:$drpSourceMigration.asDropdown('selected').value === "Empty Database"  ? "": $drpSourceMigration.asDropdown('selected').value,
                            Language:selectedLanguage,
                            ConnectionId:selectedConnectionId,
                            ConfigurationCodeId: $drpClass.asDropdown('selected').value,
                            DllVersion : selectedVersion,
                            RootNamespace : $txtRootNamespace.val(),
                            Force:$chkForce.is(':checked')
                        }),
                        success: function (result) {
                          $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
                           $migEditorDiv.show();
                          $migCodeEditor.asCodeEditor("setEditorMode","sqlserver");
                           $migCodeEditor.asCodeEditor('setValue', result);
                        }
                         },{$form: $frm,validate:true,overlayClass: "as-overlay-absolute"});
                }
        }
            
        });
        //  $btnCancel.on('click', function () {
        //         $win.asModal('hide');
        //     });
            asOnPageDispose = function(){
                  validateMigration.destroy();
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

    

   
         $migCodeEditor.asCodeEditor("editor").commands.addCommand({
    name: 'Find',
    bindKey: {win: 'Ctrl-F',  mac: 'Command-F'},
    exec: function(editor) {
       $winFind.asWindow("show")
    },
    readOnly: true // false if this command should not apply in readOnly mode
});



    $migCodeEditor.asCodeEditor("editor").commands.addCommand({
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
                     $migCodeEditor.asCodeEditor("editor").gotoLine(codeEditorPosition[codeEditorPositionIndex]);
                     if(codeEditorPositionIndex > 1)
                     codeEditorPositionIndex--;

        });
        $btnNext.click(function () {
                
          backOrForwrdFlag=true;

                codeEditorPositionIndex = codeEditorPositionIndex === 0 ? codeEditorPosition.length -1:codeEditorPositionIndex;
                 $migCodeEditor.asCodeEditor("editor").gotoLine(codeEditorPosition[codeEditorPositionIndex]);
                 if(codeEditorPositionIndex < codeEditorPosition.length - 1)
                 codeEditorPositionIndex++;

                
        });
        
         $migCodeEditor.asCodeEditor("editor").getSession().on('change', function(e) {
            codeEditorPositionIndex = codeEditorPosition.length -1
         });
    
   $migCodeEditor.asCodeEditor("editor").on("focus", function(){
        currentEditor =$migCodeEditor;
        backOrForwrdFlag=false;
    });
    
     $migCodeEditor.asCodeEditor("editor").getSession().selection
     .on('changeCursor', function(e) {
                codeEditorPosition.push($migCodeEditor.asCodeEditor("editor").selection.getCursor().row);
        });
       as("#btnToggleComment").click(function () {
     commentToggel()
    })
     as("#btnFindWindow").click(function () {
      $winFind.asWindow("show")
    });
         $drpEditors.on("change", function (event, item) {
             selectedLanguage=item.value;
         $migCodeEditor.asCodeEditor("setEditorMode",selectedLanguage);
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