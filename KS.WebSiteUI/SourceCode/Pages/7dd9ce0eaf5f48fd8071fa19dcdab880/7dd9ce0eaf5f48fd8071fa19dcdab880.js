var 
    $frmCmsWebForm= as("#cmsWebForm_webForm"),
    $editorContent= as("#cmsWebForm_Html"),
    $codeEditorHtml= as("#cmsWebForm_htmlSource"),
    $codeEditorJavascript= as("#cmsWebForm_javaScript"),
    $codeEditorStyle= as("#cmsWebForm_style"),
    $drpMenu= as("#cmsWebForm_drp_menu"),
    $drpViewRole= as("#cmsWebForm_drp_viewRole"),
    $drpModifyRole= as("#cmsWebForm_drp_modifyRole"),
    $drpAccessRole= as("#cmsWebForm_drp_accessRole"),
    $drpService= as("#cmsWebForm_drp_service"),
    $drpFrameWork= as("#cmsWebForm_drp_frameWork"),
    $drpType= as("#cmsWebForm_drp_type"),
    $txtTitle= as("#cmsWebForm_Title"),
    $txtTemplatePatternUrl= as("#cmsWebForm_TemplatePatternUrl"),
    $btnSave= as("#cmsWebForm_save"),
    $btnDell=as("#btnDell"),
    $chkStatus= as("#cmsWebForm_Status"),
    $chkCache= as("#cmsWebForm_Cache"),
    $chkPublish= as("#cmsWebForm_Publish"),
    $chkCheckIn=as("#chkCheckIn"),
    $chkEditMode= as("#cmsWebForm_EditeMode"),
    $winFind=as("#cmsWebForm_find"),
    $txtFind=as("#cmsWebForm_txtFind"),
    $txtReplace=as("#cmsWebForm_txtReplace"),
    $chkCase=as("#cmsWebForm_findCase"),
    $chkWhole=as("#cmsWebForm_findWhole"),
    $chkExp=as("#cmsWebForm_findExp"),
    $chkSelect=as("#cmsWebForm_findSelect"),
    $editorTools=as("#editorToolbar"),
    $sourceEditorToolbar=as("#sourceEditorToolbar"),
    $lblEditor=as("#lblEditor"),
    $txtParams=as("#cmsWebForm_params"),
    $divParams=as("#divParams"),
    $winRestore=as("#cmsWebForm_restorEditor"),
    $txtPageId=as("#cmsWebForm_id"),
    $txtCacheTime = as("#cmsWebForm_SlidingExpirationTimeInMinutes"),
    $btnNext=as("#btnNext"),
    $btnPrev=as("#btnPrev"),
    $txtComment=as("#txtComment"),
    $winSourceManager=$.asModalManager.get({url:$.asModalManager.urls.sourceManager}),
    $winSourceComparator=$.asModalManager.get({url:$.asModalManager.urls.sourceComparator}),
    servicesId= [],
    viewRoleId= 0,
    modifyRoleId= 0,
    accessRoleId= 0,
    typeId= 0,
    templatePatternUrl= "",
    frameWorkUrl="",
    webFormId= 0,
    rowVersion= "",
    selectedMenu,
    queryString= null,
    isLoadQueryString = false,
    currentEditorName ="",
    backOrForwrdFlag=false,
    jsEditorPositionIndex=0,
    jsEditorPosition =[],
    cssEditorPositionIndex=0,
    cssEditorPosition =[],
    htmlEditorPositionIndex=0,
    htmlEditorPosition =[],
    currentEditor=null,
    interval = null;

$winSourceComparator.asModal({width:1200}); 
$winSourceManager.asModal({width:800});                
$winFind.asWindow({focusedId:"cmsWebForm_txtFind"})
// $winRestore.asWindow({focusedId:"cmsWebForm_restorPerviousEditors",sizeClass: "col-lg-3 col-md-3 col-sm-6 col-xs-8", draggable: false, backgroundColor: "white",closeByEscape:false})
$winRestore.asModal(
    {backdrop:'static', keyboard: false}
    )

$codeEditorJavascript.asCodeEditor();

$codeEditorStyle.asCodeEditor({ mode: 'css' });
$codeEditorHtml.asCodeEditor({ mode: 'html', wysiwygEditor: "cmsWebForm_Html" });
$editorContent.asEditor({ 
    htmlEditor: $codeEditorHtml, 
    init_instance_callback: function (editor) {
    editor.addShortcut('ctrl+l',"fullScreen", function() {resizeSourceEditor()});
    editor.on('focus', function (e) {
     changeEditorToolbar("محتوا")
    });
  }});
var loadMenu = function () {
    return $.asAjaxTask({
        url: $.asInitService($.asUrls.cms_link_getByLanguage, [{ name: '@lang', value: 'fa' }])
    });
}
as("#cmsWebForm_menu").asAfterTasks([
    loadMenu()
], function (menus) {

    $drpMenu.asDropdown({
        source: {
            hierarchy:
            {
                type: 'flat',
                keyDataField: { name: 'Id' },
                parentDataField: { name: 'ParentId' },
                childrenDataField: 'Children',
                removeChildLessParent: true
            },
            valueDataField: 'Url',
            displayDataField: 'Text',
            orderby: 'Order',
            localData: menus
            //url: '/cms/AllMenus'
            //url: 'Security/Access'
        }
    })
})



$drpService.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: true
        },
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId, [{ name: '@typeId', value: 1001 }])
        , displayDataField: 'Name'
          , valueDataField: 'Code',
        orderby: 'Order'
    }
 , multiple: true
    //, selectParents: false
 , parentMode: "uniq"

});




$drpType.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: true
        },
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId, [{ name: '@typeId', value: 1003 }])
        , displayDataField: 'Name'
          , valueDataField: 'Id',
        orderby: 'Order'
    }
   , parentMode: "uniq"

});






var loadRoles =function () {
    return $.asAjaxTask({
        url: $.asUrls.security_Role_getAllByDefaultsLanguage
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


$drpFrameWork.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: true
        },
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId, [{ name: '@typeId', value: 1005 }])
        , displayDataField: 'Name'
          , valueDataField: 'PathOrUrl',
        orderby: 'Order'
    }
   , parentMode: "uniq"

});





 $frmCmsWebForm.asValidate("validator").addMethod(
        "regex",
        function(value, element, regexp) {
            var re = new RegExp(regexp);
            return this.optional(element) || re.test(value);
        },
        "شناسه می تواند شامل اعدا حروف و . و _ و - باشد و با . یا - یا _ شروع نمی شود"
);

var validate =$frmCmsWebForm.asValidate({
    rules: {
        cmsWebForm_drp_viewRole: {
            asType: 'asDropdown',
            required: true
        },
        cmsWebForm_drp_modifyRole: {
            asType: 'asDropdown',
            required: true
        },
        cmsWebForm_drp_accessRole: {
            asType: 'asDropdown',
            required: true
        },
        cmsWebForm_drp_menu: {
            asType: 'asDropdown',
            required: true
        },
        cmsWebForm_Title: {
            required: true,
            maxlength:255
        },
        cmsWebForm_params:{
            maxlength:1024
        },
        cmsWebForm_TemplatePatternUrl:{
            maxlength:1024
        },
        cmsWebForm_id:{
            maxlength:32,
            regex: "^[A-Za-z0-9][A-Za-z0-9_\\-\\.]*$" 
        }

    }
}
);



var loadQueryString = function () {
    queryString = $.asGetQueryString()
    if (queryString !== null) {
        var q = queryString.split("/")

        // loadQueryStringByUrl = false

        selectedMenu = q[0].replace(new RegExp($.asUrlDelimeter, "gi"),"/")
        getwebform(q[0], q[1]);




    }
}
var bindEvent = function () {

    var initRecovery = function(){
        queryString = $.asGetQueryString();
        if(queryString !== null){
                       $.asTemp[queryString] = $.asTemp[queryString] || {};

            $.asTemp[queryString].htmlEditor = $.asTemp[queryString].htmlEditor || "";
            $.asTemp[queryString].javaScriptEditor = $.asTemp[queryString].javaScriptEditor || "";
            $.asTemp[queryString].styleEditor = $.asTemp[queryString].styleEditor || "";

            
             if($.asTemp[queryString].htmlEditor !== "")
                $.asStorage.setItem("htmlPage" + queryString,$.asTemp[queryString].htmlEditor);
             if($.asTemp[queryString].javaScriptEditor !== "")
                $.asStorage.setItem("javaScriptPage" + queryString,$.asTemp[queryString].javaScriptEditor);
             if($.asTemp[queryString].styleEditor !== "")
                $.asStorage.setItem("stylePage" + queryString,$.asTemp[queryString].styleEditor);
        }
            
                
    }
    
        $(asPageEvent).on($.asEvent.page.ready, function (event) {

            $codeEditorJavascript.asCodeEditor("focus");
            $winRestore.asModal("show");
    });
    
    $(asPageEvent).on($.asEvent.page.queryStringChange, function (event,pageUrl,queryString) {
          initRecovery();
          if(interval === null)
          interval = setInterval(autoSave, 5000);
            loadQueryString();
          
    });
    
    
    
    $chkCache.change(function () {
        as("#cmsWebForm_SlidingExpirationTimeInMinutes_fieldset").prop("disabled", !this.checked)
    });

  as("#cmsWebForm_html_changeMode").click(function () {
       resizeSourceEditor()
    })

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
                if( $codeEditorJavascript === currentEditor)
                currentEditor.asCodeEditor('setValue',$.asStorage.getItem("javaScriptPage" + queryString));
                 if( $codeEditorStyle === currentEditor)
                currentEditor.asCodeEditor('setValue',$.asStorage.getItem("stylePage" + queryString));
                 if( $codeEditorHtml === currentEditor)
                currentEditor.asCodeEditor('setValue',$.asStorage.getItem("htmlPage" + queryString));
          }
    }
    
     var recoverAllEditor = function(){
          queryString = $.asGetQueryString();
          if(queryString !== null){
              
                      $codeEditorJavascript.asCodeEditor('setValue',$.asStorage.getItem("javaScriptPage" + queryString));
                    $codeEditorStyle.asCodeEditor('setValue',$.asStorage.getItem("stylePage" + queryString));
                    $codeEditorHtml.asCodeEditor('setValue',$.asStorage.getItem("htmlPage" + queryString));
          }

    }
   
         $codeEditorJavascript.asCodeEditor("editor").commands.addCommand({
    name: 'Find',
    bindKey: {win: 'Ctrl-F',  mac: 'Command-F'},
    exec: function(editor) {
       $winFind.asWindow("show")
    },
    readOnly: true // false if this command should not apply in readOnly mode
});
         $codeEditorHtml.asCodeEditor("editor").commands.addCommand({
    name: 'Find',
    bindKey: {win: 'Ctrl-F',  mac: 'Command-F'},
    exec: function(editor) {
       $winFind.asWindow("show")
    },
    readOnly: true // false if this command should not apply in readOnly mode
});
         $codeEditorStyle.asCodeEditor("editor").commands.addCommand({
    name: 'Find',
    bindKey: {win: 'Ctrl-F',  mac: 'Command-F'},
    exec: function(editor) {
       $winFind.asWindow("show")
    },
    readOnly: true // false if this command should not apply in readOnly mode
});

    $codeEditorJavascript.asCodeEditor("editor").commands.addCommand({
    name: 'CommentToggel',
    bindKey: {win: 'Ctrl-K',  mac: 'Command-K'},
    exec: function(editor) {
       commentToggel()
    },
    readOnly: false // false if this command should not apply in readOnly mode
});
    $codeEditorStyle.asCodeEditor("editor").commands.addCommand({
    name: 'CommentToggel',
    bindKey: {win: 'Ctrl-K',  mac: 'Command-K'},
    exec: function(editor) {
       commentToggel()
    },
    readOnly: false // false if this command should not apply in readOnly mode
});
    $codeEditorHtml.asCodeEditor("editor").commands.addCommand({
    name: 'CommentToggel',
    bindKey: {win: 'Ctrl-K',  mac: 'Command-K'},
    exec: function(editor) {
       commentToggel()
    },
    readOnly: false // false if this command should not apply in readOnly mode
});
 $codeEditorJavascript.asCodeEditor("editor").commands.addCommand({
    name: 'fullScreen',
    bindKey: {win: 'Ctrl-L',  mac: 'Command-L'},
    exec: function(editor) {
        resize();
    },
    readOnly: false // false if this command should not apply in readOnly mode
});
 $codeEditorStyle.asCodeEditor("editor").commands.addCommand({
    name: 'fullScreen',
    bindKey: {win: 'Ctrl-L',  mac: 'Command-L'},
    exec: function(editor) {
        resize();
    },
    readOnly: false // false if this command should not apply in readOnly mode
});
 $codeEditorHtml.asCodeEditor("editor").commands.addCommand({
    name: 'fullScreen',
    bindKey: {win: 'Ctrl-L',  mac: 'Command-L'},
    exec: function(editor) {
        resize();
    },
    readOnly: false // false if this command should not apply in readOnly mode
});
            
      $btnPrev.click(function () {
              backOrForwrdFlag=true;
              switch (currentEditorName) {
                case "جاوا اسکریپت":
                    jsEditorPositionIndex = jsEditorPositionIndex === 0 ? jsEditorPosition.length -1:jsEditorPositionIndex;
                     $codeEditorJavascript.asCodeEditor("editor").gotoLine(jsEditorPosition[jsEditorPositionIndex]);
                     if(jsEditorPositionIndex > 1)
                     jsEditorPositionIndex--;
                    break;
                case "استایل":
                    cssEditorPositionIndex = cssEditorPositionIndex === 0 ? cssEditorPosition.length -1:cssEditorPositionIndex;
                     $codeEditorStyle.asCodeEditor("editor").gotoLine(cssEditorPosition[cssEditorPositionIndex]);
                     if(cssEditorPositionIndex > 1)
                     cssEditorPositionIndex--;
                    break;
                case "Html":
                    htmlEditorPositionIndex = htmlEditorPositionIndex === 0 ? htmlEditorPosition.length -1:htmlEditorPositionIndex;
                     $codeEditorHtml.asCodeEditor("editor").gotoLine(htmlEditorPosition[htmlEditorPositionIndex]);
                     if(htmlEditorPositionIndex >1)
                     htmlEditorPositionIndex--;
                    break;
            }
        });
        $btnNext.click(function () {
                
          backOrForwrdFlag=true;
          switch (currentEditorName) {
            case "جاوا اسکریپت":
                jsEditorPositionIndex = jsEditorPositionIndex === 0 ? jsEditorPosition.length -1:jsEditorPositionIndex;
                 $codeEditorJavascript.asCodeEditor("editor").gotoLine(jsEditorPosition[jsEditorPositionIndex]);
                 if(jsEditorPositionIndex < jsEditorPosition.length - 1)
                 jsEditorPositionIndex++;
                break;
            case "استایل":
                cssEditorPositionIndex = cssEditorPositionIndex === 0 ? cssEditorPosition.length -1:cssEditorPositionIndex;
                 $codeEditorStyle.asCodeEditor("editor").gotoLine(cssEditorPosition[cssEditorPositionIndex]);
                 if(cssEditorPositionIndex < cssEditorPosition.length - 1)
                 cssEditorPositionIndex++;
                break;
            case "Html":
                htmlEditorPositionIndex = htmlEditorPositionIndex === 0 ? htmlEditorPosition.length -1:htmlEditorPositionIndex;
                 $codeEditorHtml.asCodeEditor("editor").gotoLine(htmlEditorPosition[htmlEditorPositionIndex]);
                 if(htmlEditorPositionIndex < htmlEditorPosition.length)
                 htmlEditorPositionIndex++;
                break;
             }
                
        });
     $codeEditorJavascript.asCodeEditor("editor").getSession().on('change', function(e) {
            jsEditorPositionIndex = jsEditorPosition.length -1
         });
    $codeEditorHtml.asCodeEditor("editor").getSession().on('change', function(e) {
            htmlEditorPositionIndex = htmlEditorPosition.length -1
         });
         
    $codeEditorStyle.asCodeEditor("editor").getSession().on('change', function(e) {
            cssEditorPositionIndex = cssEditorPosition.length -1
         });
         
   $codeEditorJavascript.asCodeEditor("editor").on("focus", function(){
        currentEditor =$codeEditorJavascript;
       changeEditorToolbar("جاوا اسکریپت");
    });
     $codeEditorJavascript.asCodeEditor("editor").getSession().selection
     .on('changeCursor', function(e) {
                jsEditorPosition.push($codeEditorJavascript.asCodeEditor("editor").selection.getCursor().row);
        });

      $codeEditorStyle.asCodeEditor("editor").on("focus", function(){
        currentEditor =$codeEditorStyle;
        changeEditorToolbar("استایل")
    });
    
       $codeEditorStyle.asCodeEditor("editor").getSession().selection
     .on('changeCursor', function(e) {
                cssEditorPosition.push($codeEditorStyle.asCodeEditor("editor").selection.getCursor().row);
        });
        
         $codeEditorHtml.asCodeEditor("editor").on("focus", function(){
        currentEditor =$codeEditorHtml
         changeEditorToolbar("Html")
        });
        
         $codeEditorHtml.asCodeEditor("editor").getSession().selection
        .on('changeCursor', function(e) {
         htmlEditorPosition.push($codeEditorHtml.asCodeEditor("editor").selection.getCursor().row)
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

    });
    var loadWinComparator = function(sourceControlCode,fileName){
                 
         
                      
            $winSourceComparator.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.sourceComparator)},{name:"@isModal",value:true}])
                                                                                  
            ,{editorCode:currentEditor.asCodeEditor("getValue"),sourceControlCode:sourceControlCode,fileName:fileName});
         
         
    }
     $(asPageEvent).on("compare",function(event,selectedId){
       
         var sourceControlCode,fileName =asPageEvent.substring(2);
         
         
         
          if($lblEditor.html() === "Html"){
     as("#divHtml").asAjax({
            url: $.asInitService($.asUrls.cms_webPage_getWebPageChange,[{name:"@changeId",value:selectedId},{name:"@webPageGuid",value:$txtPageId.val()}]),
            type:"get",
            success: function (webForm) {
                sourceControlCode = webForm.Html;
                fileName+=".html";
                loadWinComparator(sourceControlCode,fileName);
            }
        }, { validate:false, overlayClass: 'as-overlay-absolute'});
        }else if($lblEditor.html()==="جاوا اسکریپت"){

        as("#divJs").asAjax({
            url: $.asInitService($.asUrls.cms_webPage_getWebPageResourcesChange,[
                {name:"@changeId",value:selectedId}
                ,{name:"@webPageGuid",value:$txtPageId.val()}
                ]),
            type:"get",
            success: function (js) {
                sourceControlCode = js;
                fileName+=".js";
                loadWinComparator(sourceControlCode,fileName);
            }
        }, { validate:false, overlayClass: 'as-overlay-absolute'});
        }else{
            
        as("#divCss").asAjax({
            url: $.asInitService($.asUrls.cms_webPage_getWebPageResourcesChange,[
                {name:"@changeId",value:selectedId}
                ,{name:"@webPageGuid",value:$txtPageId.val()}
                ]),
            type:"get",
            success: function (css) {
                 sourceControlCode = css;
                 fileName+=".css";
                 loadWinComparator(sourceControlCode,fileName);
            }
        }, { validate:false, overlayClass: 'as-overlay-absolute'});
        }

         
         
         
         
     });
    $(asPageEvent).on("changeSetSelected",function(event,selectedId){
       
        
        if($lblEditor.html() === "Html"){
     as("#divHtml").asAjax({
            url: $.asInitService($.asUrls.cms_webPage_getWebPageChange,[{name:"@changeId",value:selectedId},{name:"@webPageGuid",value:$txtPageId.val()}]),
            type:"get",
            success: function (webForm) {

            if (webForm.Html) {
                $editorContent.asEditor('setContent', webForm.Html);
                $codeEditorHtml.asCodeEditor('setValue', webForm.Html);
            } else {
                $editorContent.asEditor('setContent', '');
                $codeEditorHtml.asCodeEditor('setValue', '');
            }

                
            as("#divLastModifiUser").html(webForm.LastModifieUser);
            as("#divLastModifiLocalDataTime").html(webForm.LastModifieLocalDateTime);
            $txtTitle.val(webForm.Title)
            $txtParams.val(webForm.Params)
            $txtPageId.val(webForm.Guid)
            $("#cmsWebForm_version").val(webForm.Version)
            $chkStatus.prop('checked', webForm.Status)
            $chkCache.prop('checked', webForm.EnableCache)
            as("#chkMobile").prop('checked', webForm.IsMobileVersion)
            $("#cmsWebForm_SlidingExpirationTimeInMinutes_fieldset").prop("disabled", !webForm.EnableCache)
            $txtCacheTime.val(webForm.CacheSlidingExpirationTimeInMinutes)
            $chkEditMode.prop('checked', webForm.EditMode)

     
            $drpService.asDropdown('selectValue', [], true)
            if (webForm.Services != null) {
                if (webForm.Services.length != 0)
                    $drpService.asDropdown('selectValue', webForm.Services)

            }


            if (typeId == 15) {
                if (webForm.TemplatePatternUrl != null)
                    $drpFrameWork.asDropdown('selectValue', webForm.FrameWorkUrl)
                else
                    $drpFrameWork.asDropdown('selectValue', [], true)
          
                $txtTemplatePatternUrl.val(webForm.TemplatePatternUrl)
            }
            if (webForm.ViewRoleId != null)
                $drpViewRole.asDropdown('selectValue', webForm.ViewRoleId)
            else
                $drpViewRole.asDropdown('selectValue', [], true)
            if (webForm.AccessRoleId != null)
                $drpAccessRole.asDropdown('selectValue', webForm.AccessRoleId)
            else
                $drpAccessRole.asDropdown('selectValue', [], true)
            if (webForm.ModifyRoleId != null)
                $drpModifyRole.asDropdown('selectValue', webForm.ModifyRoleId)
            else
                $drpModifyRole.asDropdown('selectValue', [], true)
             
             
            }
        }, { validate:false, overlayClass: 'as-overlay-absolute'});
        }else if($lblEditor.html()==="جاوا اسکریپت"){

        as("#divJs").asAjax({
            url: $.asInitService($.asUrls.cms_webPage_getWebPageResourcesChange,[
                {name:"@changeId",value:selectedId}
                ,{name:"@webPageGuid",value:$txtPageId.val()}
                ]),
            type:"get",
            success: function (js) {
                 $codeEditorJavascript.asCodeEditor('setValue', js);
             
            }
        }, { validate:false, overlayClass: 'as-overlay-absolute'});
        }else{
            
        as("#divCss").asAjax({
            url: $.asInitService($.asUrls.cms_webPage_getWebPageResourcesChange,[
                {name:"@changeId",value:selectedId}
                ,{name:"@webPageGuid",value:$txtPageId.val()}
                ]),
            type:"get",
            success: function (css) {
                 $codeEditorStyle.asCodeEditor('setValue', css);
             
            }
        }, { validate:false, overlayClass: 'as-overlay-absolute'});
        }
             
    });
    as("#btnSourceControl").click(function () {
        var type = "css";
        if($lblEditor.html() === "Html"){
            type = "json";
        }else if($lblEditor.html()==="جاوا اسکریپت"){
                 type = "js";
             }
             
            $winSourceManager.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.sourceManager)},{name:"@isModal",value:true}])
            ,{parent:asPageEvent,selectEvent:"changeSetSelected",compareEvent:"compare",getUrl: $.asInitService($.asUrls.cms_webPage_getWebPageChanges, [
             { name: '@webPageGuid', value: $txtPageId.val()}
             ,{ name: '@type', value: type}])});
    });
    as("#btnRecovery").click(function () {
        recoverEditor()
    });
as("#btnReplace").click(function () {
       currentEditor.asCodeEditor("replace",$txtReplace.val());
    })
    as("#btnReplaceAll").click(function () {
       currentEditor.asCodeEditor("replaceAll",$txtReplace.val());
    })
    as("#btnToggleComment").click(function () {
     commentToggel()
    })
as("#cmsWebForm_findNext").click(function () {
       currentEditor.asCodeEditor("findNext");
    })
    
    as("#cmsWebForm_findPrev").click(function () {
       currentEditor.asCodeEditor("findPrevious");

    })
as("#cmsWebForm_cancelRestor").click(function () {
       $winRestore.asModal('hide');
        loadQueryString()
        queryString = $.asGetQueryString();
        if(queryString !== null){
                  initRecovery();
                interval = setInterval(autoSave, 5000);
        }
        
    })
    as("#cmsWebForm_restorPerviousEditors").click(function () {
       $winRestore.asModal('hide');
         recoverAllEditor();
    })
    $btnDell.click(function () {
         $frmCmsWebForm.asAjax({
            url: $.asUrls.cms_webPage_delete,
            data: JSON.stringify({
                Id: webFormId
            }),
            success: function (webForm) {
                webFormId = 0
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
            }
        }, { $form: $frmCmsWebForm });
    });
    
    $btnSave.click(function () {

        //$frmWebForm.asAfterTasks([

        //], function () {

        //}, { overlayClass: "as-overlay-absolute" })



        //if ($frmWebForm.asValidate('valid')) {
        templatePatternUrl = ""
        servicesId = []
        if ($drpService.asDropdown('selected')) {
            $.each($drpService.asDropdown('selected'), function (i, v) {
                if (v.selected)
                    servicesId.push(v.value)
            })
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
        if ($drpType.asDropdown('selected')) {
            typeId = $drpType.asDropdown('selected').value
        }


        if (typeId == 15) {
            if ($drpFrameWork.asDropdown('selected')) {
                var tempUrl = $drpFrameWork.asDropdown('selected')
                if (tempUrl.selected)
                    frameWorkUrl = tempUrl.value
            }
            templatePatternUrl = $txtTemplatePatternUrl.val()
        }

        var js = $codeEditorJavascript.asCodeEditor("getValue")

        var html = $codeEditorHtml.asCodeEditor("getValue")

        var css = $codeEditorStyle.asCodeEditor("getValue")

        // var tools = $.asGetDependentModules(js);
       
            
       
        //       var pageParams =eval("({" + $txtParams.val() + "})");
     
        // var urls =[]
        // if(typeof(pageParams.loadScriptAndStyle) != "undefined")
        //      urls = pageParams.loadScriptAndStyle.urls || [];
        
        $.asTemp[queryString].htmlEditor = html
        $.asTemp[queryString].javaScriptEditor = js;
        $.asTemp[queryString].styleEditor = css

        $frmCmsWebForm.asAjax({
            url: $.asUrls.cms_webPage_save,
            data: JSON.stringify({
                Id: webFormId,
                Url: selectedMenu,
                TemplatePatternUrl: templatePatternUrl,
               CacheSlidingExpirationTimeInMinutes:$txtCacheTime.val(),
                FrameWorkUrl: frameWorkUrl,
                Services: servicesId,
                ViewRoleId: viewRoleId,
                ModifyRoleId: modifyRoleId,
                AccessRoleId: accessRoleId,
                // Tools: tools.join(','),
                JavaScript: js,
                Html: html,
                Style: css,
                Publish: $chkPublish.is(':checked'),
                CheckIn:$chkCheckIn.is(':checked'),
                Title: $txtTitle.val(),
                Params:$txtParams.val(),
                DependentModules:$.asGetDependentModules(js),
                Guid:$txtPageId.val(),
                IsMobileVersion:as("#chkMobile").is(':checked'),
                Status: $chkStatus.is(':checked'),
                EnableCache: $chkCache.is(':checked'),
                EditMode: $chkEditMode.is(':checked'),
                Comment:$txtComment.val(),
                TypeId: typeId,
                RowVersion: rowVersion
            }),
            success: function (webForm) {
                webFormId = webForm.Id;
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration,showTime:10000000 });
            }
        }, { $form: $frmCmsWebForm })

        //}

        //$($drpService).asDropdown('selectValue', [8, 7]);


    });

    $drpService.on("change", function (event, item) {

    //   if (loadQueryStringByUserInput === false && loadQueryStringByUrl === true) {
    // if (loadQueryStringByUserInput === false) {
     if (isLoadQueryString === false) {
            if (item.selected) {
                $codeEditorJavascript.asCodeEditor("insert", item.value);
            } else {
                $codeEditorJavascript.asCodeEditor("find", item.value,{wholeWord: true});
                $codeEditorJavascript.asCodeEditor("replaceAll", '');
            }
        }

    });
    
        $drpMenu.on("change", function (event, item) {
            selectedMenu=item.value;
            if (item.selected) {
               if(item.value.toLowerCase().indexOf($.asMobileSign) > -1){
               as("#chkMobile").prop('checked', true)
            } else {
            as("#chkMobile").prop('checked', false)
            }
        }

    });

    $drpType.on("change", function (event, item) {
        if ($drpMenu.asDropdown('selected')) {
            // loadQueryStringByUserInput = true
            // if (loadQueryStringByUrl === true) {
             if (isLoadQueryString === false) {


          
                initPageType(item.value);
              
                //var pageUrl = url
                //if (url.indexOf($.asUrlDelimeter) > -1)
                var pageUrl = selectedMenu.replace(/\//g, $.asUrlDelimeter)
                if (typeof (item.value) != "undefined") {
                    $.asSetQueryString(pageUrl + "/" + item.value)
                    // getwebform(pageUrl,item.value)
                }

            }
            else {
                // loadQueryStringByUrl = true
                
                //getwebform(q[0],q[1])
            }
        }
    });
        asOnPageDispose = function(){
        window.clearInterval(interval);
        validate.destroy();
    }
    // $chkIsModal.change(function() {
    //  var $this = $(this);
    //  if ($drpMenu.asDropdown('selected')) {
    //         var url = $drpMenu.asDropdown('selected').value
    //            if ($this.is(':checked')) {
    //     getwebform(url,true)
    //  } else {
    //      getwebform(url)
    // }
    //   }
    //});
}
bindEvent();
var getwebform = function (formUrl, pageTypeId) {
    typeId=pageTypeId;
    isLoadQueryString = true;
    
           
                var q = queryString.split("/")
                initPageType(q[1])

                $drpMenu.asDropdown('selectValue', [], true)
                $drpMenu.asDropdown('selectValue', (decodeURI(q[0]).toLowerCase()).replace(new RegExp($.asUrlDelimeter, "g"), "/"))
                // console.log((q[0]).replace(new RegExp($.asUrlDelimeter, "g"), "/"))

                $drpType.asDropdown('selectValue', [], true)
                $drpType.asDropdown('selectValue', q[1])
            
          
    // form = form.replace(/#-/g, '');

    // if (typeof isModal === 'undefined')
    //url= '/cms/WebPage/Get/' + encodeURI(form)
    //else

    //services={}
    servicesId = []
    viewRoleId = 0
    frameWorkUrl = ""
    url = ""
    templatePatternUrl = ""
    webFormId = 0
    rowVersion = ""



    $frmCmsWebForm.asAjax({
        url: $.asInitService($.asUrls.cms_webPage_get ,[{name:"@url",value:$.asUrlAsParameter(formUrl)},{name:"@typeId",value:typeId}]),
        type: "get",
        error:function(){
             isLoadQueryString = false;
        },
        success: function (webForm) {
          
            // if (loadQueryStringByUrl === false) {
            //     var q = queryString.split("/")
            //     initPageType(q[1])

            //     $drpMenu.asDropdown('selectValue', [], true)
            //     $drpMenu.asDropdown('selectValue', (q[0].toLowerCase()).replace(new RegExp($.asUrlDelimeter, "g"), "/"))
            //     // console.log((q[0]).replace(new RegExp($.asUrlDelimeter, "g"), "/"))

            //     $drpType.asDropdown('selectValue', [], true)
            //     $drpType.asDropdown('selectValue', q[1])
                
            // }


            $txtComment.val("");
            //    setMode = true;
            if (webForm.Id == 0)
           {
                $.asShowMessage({ message: 'محتوای این منو هنوز تهیه نشده است' });
                $txtPageId.prop("disabled", false)
           }else
            $txtPageId.prop("disabled", true)

            webFormId = webForm.Id
            rowVersion = webForm.RowVersion
            if (webForm.JavaScript)
                $codeEditorJavascript.asCodeEditor('setValue', webForm.JavaScript);
            else
                $codeEditorJavascript.asCodeEditor('setValue', '');
            if (webForm.Html) {
                $editorContent.asEditor('setContent', webForm.Html);
                $codeEditorHtml.asCodeEditor('setValue', webForm.Html);
            } else {
                $editorContent.asEditor('setContent', '');
                $codeEditorHtml.asCodeEditor('setValue', '');
            }
            if (webForm.Style)
                $codeEditorStyle.asCodeEditor('setValue', webForm.Style)
            else
                $codeEditorStyle.asCodeEditor('setValue', '')
                
            as("#divLastModifiUser").html(webForm.LastModifieUser);
            as("#divLastModifiLocalDataTime").html(webForm.LastModifieLocalDateTime);
            $txtTitle.val(webForm.Title)
            $txtParams.val(webForm.Params)
            $txtPageId.val(webForm.Guid)
            $("#cmsWebForm_version").val(webForm.Version)
            $chkPublish.prop('checked', false)
            $chkCheckIn.prop('checked', false)
            $chkStatus.prop('checked', webForm.Status)
            $chkCache.prop('checked', webForm.EnableCache)
            as("#chkMobile").prop('checked', webForm.IsMobileVersion)
            $("#cmsWebForm_SlidingExpirationTimeInMinutes_fieldset").prop("disabled", !webForm.EnableCache)
            $txtCacheTime.val(webForm.CacheSlidingExpirationTimeInMinutes)
            $chkEditMode.prop('checked', webForm.EditMode)

            // $chkIsModal.prop('checked', webForm.IsModal)
            $drpService.asDropdown('selectValue', [], true)
            if (webForm.Services != null) {
                if (webForm.Services.length != 0)
                    $drpService.asDropdown('selectValue', webForm.Services)

            }


            if (typeId == 15) {
                if (webForm.TemplatePatternUrl != null)
                    $drpFrameWork.asDropdown('selectValue', webForm.FrameWorkUrl)
                else
                    $drpFrameWork.asDropdown('selectValue', [], true)
          
                $txtTemplatePatternUrl.val(webForm.TemplatePatternUrl)
            }
            if (webForm.ViewRoleId != null)
                $drpViewRole.asDropdown('selectValue', webForm.ViewRoleId)
            else
                $drpViewRole.asDropdown('selectValue', [], true)
            if (webForm.AccessRoleId != null)
                $drpAccessRole.asDropdown('selectValue', webForm.AccessRoleId)
            else
                $drpAccessRole.asDropdown('selectValue', [], true)
            if (webForm.ModifyRoleId != null)
                $drpModifyRole.asDropdown('selectValue', webForm.ModifyRoleId)
            else
                $drpModifyRole.asDropdown('selectValue', [], true)
            //if(webForm.TypeId != null)
            //$drpType.asDropdown('selectValue', webForm.TypeId)
            //else
            //$drpType.asDropdown('selectValue', [],true)
            //setMode = false;
            // loadQueryStringByUserInput = false
            isLoadQueryString = false;
        }
    });
}
var autoSave = function(){
        $.asStorage.setItem("javaScriptPage" + queryString,$codeEditorJavascript.asCodeEditor("getValue"))
        $.asStorage.setItem("stylePage" + queryString,$codeEditorStyle.asCodeEditor("getValue"))
        $.asStorage.setItem("htmlPage" + queryString,$codeEditorHtml.asCodeEditor("getValue"))
    }
var changeEditorToolbar = function(editor){
        backOrForwrdFlag=false;
        currentEditorName = editor;
        
        if(editor === "محتوا")
      {
           $editorTools.addClass("hide");
            $sourceEditorToolbar.removeClass("hide");
         
      }else{
           $lblEditor.html(editor)
           $sourceEditorToolbar.addClass("hide");
        $editorTools.removeClass("hide");
      }
          
    }
    var resizeSourceEditor = function(){
         $('#cmsWebForm_Html_container').toggleClass('editor-fullscreen');
    }
    var initPageType = function(type){
                        if (type == 15) {
                    $("#cmsWebFom_frameWork").show()
                    $("#cmsWebFom_template").show()
                    
                }
                else {
                    $("#cmsWebFom_frameWork").hide()
                    $("#cmsWebFom_template").hide()
                }
                 if (type == 14) {
                    $divParams.hide()
                }
                else {
                    $divParams.show()
                   
                }
    }

