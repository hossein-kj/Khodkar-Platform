var 
    $frm = as("#frmQuery"),
    $divRoles = as("#divRoles"),
    $drpViewRole= as("#drpViewRole"),
    $drpModifyRole= as("#drpModifyRole"),
    $drpRole=as("#drpRole"),
    $drpAccessRole= as("#drpAccessRole"),
    $drpMasterDataType= as("#drpMasterDataType"),
    $drpMasterData = as("#drpMasterData"),
    $drpPermission=as("#drpPermission"),
    $drpActions=as("#drpActions"),
    $txtName= as("#txtName"),
    $btnSave= as("#btnSave"),
    $chkEditMode= as("#chkEditeMode"),
    $winFind=as("#divFind"),
    $winTranslator= $.asModalManager.get({url:$.asModalManager.urls.translator}),
    $txtFind=as("#txtFind"),
    $txtReplace=as("#txtReplace"),
    $chkCase=as("#chkFindCase"),
    $chkHole=as("#chkFindHole"),
    $chkExp=as("#chkFindExp"),
    $chkSelect=as("#chkFindSelect"),
    $editorTools=as("#editorToolbar"),
    $lblEditor=as("#lblEditor"),
    $txtDescription=as("#txtDescription"),
    $txtOrder=as("#txtOrder"),
    $winRestore=as("#winRestorEditor"),
    $txtId=as("#txtId"),
    $txtPermissionId=as("#txtPermissionId"),
    $txtValue=as("#txtValue"),
    $chkPermission = as("#chkPermission"),
    $chkNew = as("#chkNew"),
    $txtCode=as("#txtCode"),
    $txtMasterDataCode=as("#txtMasterDataCode"),
    $txtMasterDataUrl=as("#txtMasterDataUrl"),
    $chkCache= as("#chkCache"),
    $chkStatus= as("#chkStatus"),
    $txtVersion = as("#txtVersion"),
    $txtSlidingExpirationTimeInMinutes = as("#txtSlidingExpirationTimeInMinutes"),
    $txtParentId = as("#txtParentId"),
    $btnTranslator = as("#btnTranslator"),
    $btnDell=as("#btnDell"),
    $divPermission=as("#divPermission"),
    $divForeign=as("#divForeign"),
    $divMasterdata=as("#divMasterdata"),
    $divLink=as("#divLink"),
    $drpLink=as("#drpLink"),
    $drpLanguge=as("#drpLanguge"),
    $divVersion=as("#divVersion"),
    $drpVersion=as("#drpVersion"),
    selectedVersion=0,
    currentLang = $.asGetDefaultsLnguageAndCulter().lang,
    isFirstLoadOfLink = true,
    viewRoleId= 0,
    modifyRoleId= 0,
    accessRoleId= 0,
    typeId= 0,
    url= "",
   permissionId= 0,
    isLeaf=false,
    rowVersion= "",
    codeNewId,
    permissionNewId,
    currentEditor=null,
    interval,
    temp = {},
    selectedPermission={},
    validate,
    newParents={},
    langParams = {
                source: {
                    displayDataField: 'country',
                    valueDataField: 'language',
                    urlDataField: 'flagUrl',
                    idDataField: 'culture',
                    exteraDataField: 'isRightToLeft'
                },
                selectedSearchKey:'language'
            };

$divVersion.hide();
$divForeign.hide();
$divPermission.hide();

$.asTemp.serviceJavascriptQueryEditor = $.asTemp.serviceJavascriptQueryEditor || "";

             if($.asTemp.serviceJavascriptQueryEditor !== "")
                $.asStorage.setItem("serviceJavascriptQueryEditor",$.asTemp.serviceJavascriptQueryEditor)

$winTranslator.asModal({width:800})
$winFind.asWindow({focusedId:"txtFind"})
$winRestore.asModal(
    {backdrop:'static', keyboard: false}
    )







var loadRoles = function () {
    return $.asAjaxTask({
        url: $.asUrls.security_Role_getAllByDefaultsLanguage
    });
}

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
      
    });

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
      
    });
    
    $drpRole.asDropdown({
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

}, { overlayClass: 'as-overlay-relative' });

$drpMasterData.asDropdown("init","نوع اطلاع پایه را انتخاب نمایید",{
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: false
        }
        , displayDataField: 'Name'
          , valueDataField: 'Id',
        orderby: 'Order'
    }
    , selectParents: true

});

$drpVersion.asDropdown("init"," ابتدا کد را انتخاب نمایید",{
    source: {
         displayDataField: 'Value'
          , valueDataField: 'Id',
        orderby: 'Id'
    }
});
$drpPermission.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: false
        },
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId, [{ name: '@typeId', value: 1032 }])
        , displayDataField: 'Name'
        , valueDataField: 'Id',
        orderby: 'Order'
    }
     , selectParents: true
//  , parentMode: "uniq"

});
$drpActions.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: false
        },
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId, [{ name: '@typeId', value: 1031 }])
        , displayDataField: 'Name'
          , valueDataField: 'Id',
        orderby: 'Order'
    }
    // , selectParents: true
//  , parentMode: "uniq"

});
$drpMasterDataType.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'TypeId' },
            parentDataField: { name: 'ParentTypeId' },
            removeChildLessParent: false
        },
        url:$.asUrls.cms_masterDataKeyValue_getType
        , displayDataField: 'Name'
          , valueDataField: 'TypeId',
        orderby: 'Order'
    }
    , selectParents: true
    // , multiple: true

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
         txtCode:{
            required: true
        },
        txtPermissionId:{
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
        },
        drpModifyRole: {
            asType: 'asDropdown',
            required: true
        },
        drpRole: {
            asType: 'asDropdown',
            required: {
                depends: function (element) {
                return $chkPermission.is(':checked') === true;
                }
            }
        },
        drpActions: {
            asType: 'asDropdown',
            required: {
                depends: function (element) {
                return $chkPermission.is(':checked') === true;
                }
            }
        },
        drpMasterData: {
            asType: 'asDropdown',
            required: {
                depends: function (element) {
                return $chkPermission.is(':checked') === true;
                }
            }
        }
    }
       validate =$frm.asValidate({ rules: validateRule});
    var loadData = function (url) {
    return $.asAjaxTask({
        url: url
    });
}
    var getTypeMasterData = function(selectedId){
                var url = $.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId
        var queryTypeIdTemp = "(TypeId%20eq%20@typeIdd)"
        var queryParentTypeIdTemp = "(ParentTypeId%20eq%20@typeIdd)"
        var query = []
        // $.each($drpMasterDataType.asDropdown('selected'), function (i, v) {
                // if (v.selected){
                    query.push(queryTypeIdTemp.replace("@typeId",$drpMasterDataType.asDropdown('selected').value))
                    query.push(queryParentTypeIdTemp.replace("@typeId",$drpMasterDataType.asDropdown('selected').value))
                // }
                    
            // })
            
        $divMasterdata.asAfterTasks([
            loadData(url.replace("TypeId%20eq%20@typeIdd","(" + query.join("%20or%20") + ")"))
        ], function (masterdatas) {
            $drpMasterData.asDropdown("reload",{localData: masterdatas});
            $drpMasterData.css({"margin-top":"0"});
            if(selectedId !== null)
            $drpMasterData.asDropdown('selectValue', selectedId)
            getMasterData(selectedId)
        },{overlayClass: 'as-overlay-absolute'});

    }
    
          $drpLink.asDropdown("init","کشور را انتخاب نمایید",{
        source: {
            hierarchy:
            {
                type: 'flat',
                keyDataField: { name: 'Id' },
                parentDataField: { name: 'ParentId' },
                childrenDataField: 'Children',
                removeChildLessParent: false
            },
            valueDataField: 'Id',
            displayDataField: 'Text',
            orderby: 'Order'
        }
        , selectParents: true
        ,removeChildLessParent:false
    });


    
    var callLoadLanguages = function(linkId,linkUrl){
             
           
                 if(linkUrl){
                     var urlLang = linkUrl.split('/');
                  
                    langParams.selectedValue = urlLang[1];
       
                }else{
                    langParams.selectedValue = $.asGetDefaultsLnguageAndCulter().lang;
                }
                
                if(isFirstLoadOfLink ){
                                             
                        $drpLanguge.asAfterTasks([
                        loadData($.asUrls.cms_languageAndCulture_public_getAll)
                    ], function (languages) {
                      
                        langParams.source.localData= languages;
                        $drpLanguge.asFlexSelect(langParams);
                    },{overlayClass:"as-overlay-fixed"});
                }
          
         if(isFirstLoadOfLink || currentLang !== langParams.selectedValue){
                 isFirstLoadOfLink=false;
                 currentLang=langParams.selectedValue;
                 if(langParams.source.localData){
                      $drpLanguge.asFlexSelect(langParams);
                 }
                 
             $divLink.asAfterTasks([
               loadData($.asInitService($.asUrls.cms_link_getByLanguage, [{ name: '@lang', value: langParams.selectedValue}]))
            ], function (links) {
                $drpLink.asDropdown("reload",{localData: links});
               
                if(linkId)
                $drpLink.asDropdown('selectValue', linkId)
            
            },{overlayClass: 'as-overlay-absolute'});
                 

         }

        
    }
    var getLink = function(linkId,fillLinkAndLanguageDropDown){
         $frm.asAjax({
        url: $.asInitService($.asUrls.cms_link_get, [{ name: '@id', value: linkId}]),
        type: "get",
        success: function (link) {
        setLink(link,fillLinkAndLanguageDropDown);

        }
    });
    }
        
    var setLinkEvent = function(){
                $drpLink.on("change", function (event, item) {
                  
                if (typeof (item.value) != "undefined") {
                   
                    getLink(item.value)
                  
                }
     
          });
          
        $drpLanguge.on("selectedIndexChanged", function (event, id, value, text, url, extera) {
            if (url){
                        currentLang = value;
                      $drpLanguge.asFlexSelect('setItem', '<img src="' + url + '">&nbsp;<span class="caret"></span>');
                     $drpLink.asDropdown("reload",{url: $.asInitService($.asUrls.cms_link_getByLanguage, [{ name: '@lang', value: value }])});
                }
        });
    }
    var initLinks = function(linkId,linkUrl){
                     if(isFirstLoadOfLink){
                            
                               
                        
                               setLinkEvent();
                        
                            }
                             callLoadLanguages(linkId,linkUrl);
    }
    var loadLinks = function(linkId)
        {
    $divMasterdata.hide();
    $divLink.show();
    
     if(linkId){
         getLink(linkId,true);
         
     }else{
            initLinks();
     }
   
    
   
}

    var setLink = function(link,fillLinkAndLanguageDropDown){
                     if($.isArray(link)){
                  if(link.length > 0){
                     setMasterData({PathOrUrl:link[0].Url,Code:''});
                     if(fillLinkAndLanguageDropDown){
                           initLinks(link[0].Id,link[0].Url);
                     }
                  }
                
                    else
                     notFound("لینک");
            }else{
                 if(link != null){
                        if(typeof(link) != "undefined"){
                          setMasterData({PathOrUrl:link.Url,Code:''});
                             if(fillLinkAndLanguageDropDown){
                           initLinks(link.Id,link.Url);
                     }
                        }
                     
                        else
                     notFound("لینک");
                 } else
                     notFound("لینک");
            }
    }


var bindEvent = function () {
     $drpVersion.on("change", function (event, item) {
    selectedVersion=item.value === null ? 0:item.value;
 });
      $chkPermission.change(function () {
             
        if(this.checked === true)
       {
           $divForeign.hide();
           $divPermission.show();
       }else{
           $divPermission.hide();
           $divForeign.show();
       }
      });



            $(asPageEvent).on($.asEvent.page.ready, function (event) {
              
    });
 
   $chkCache.change(function () {
        as("#divSlidingExpirationTimeInMinutes").prop("disabled", !this.checked)
    });
    
      $chkNew.change(function () {
          
        if(this.checked === true)
      {
             
            if(isLeaf === true)
         {
                 $.asShowMessage({ template: "error", message: "امکان افزودن یک اطلاع پایه به یک اطلاع پایه دیگر وجود ندارد" })
             $chkNew.prop('checked', false)
         }
            else{
            
            $txtId.prop("disabled", false)

            temp.Id = permissionId;
            
            permissionId=0;
            
            temp.Isleaf = isLeaf
            
            temp.ParentId = $txtParentId.val();
            $txtParentId.val($txtPermissionId.val());
            
            temp.RowVersion = rowVersion;
            rowVersion='';

    
            temp.Name = $txtName.val();
            $txtName.val("")
       
            temp.Description = $txtDescription.val()
            $txtDescription.val("")
            
         
            temp.Code = $txtCode.val()
            $txtCode.val("")
            
          
            temp.Guid = $txtId.val()
            
            temp.NewGuid=codeNewId;
            $txtId.val(codeNewId)
            
           
            temp.NewId = permissionNewId
            $txtPermissionId.val(permissionNewId)
            
            temp.Version = $txtVersion.val()
            $txtVersion.val("0")
            
 
            temp.Order = $txtOrder.val()
            $txtOrder.val("")
            
            temp.Key = $drpMasterDataType.asDropdown('selected').value;
             $drpMasterDataType.asDropdown('selectValue', [], true)
             
             
            temp.ForeignKey3=$drpMasterData.asDropdown('selected').value
            $drpMasterData.asDropdown('selectValue', [], true)
                
            temp.ForeignKey2=$drpRole.asDropdown('selected').value
           $drpRole.asDropdown('selectValue', [], true)
           
            temp.ForeignKey1=$drpActions.asDropdown('selected').value
           $drpActions.asDropdown('selectValue', [], true)
            
            temp.Value = $txtValue.val();
            $txtValue.val("");
         
            
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
                
                
            temp.ForeignUrl = $txtMasterDataUrl.val();
            $txtMasterDataUrl.val("");
            temp.ForeignCode = $txtMasterDataCode.val();
            $txtMasterDataCode.val("");
                
            }
      }else{
          setPermission(temp)
      }
    });
    
  $btnDell.click(function () {
       if ($drpPermission.asDropdown('selected')) {
              $frm.asAjax({
            url: $.asUrls.security_Permission_delete,
            data: JSON.stringify({
                Id: $drpPermission.asDropdown('selected').value
            }),
            success: function (masterData) {
                $drpPermission.asDropdown("removeItem");
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
            }
        }, { $form: $frm });
       }else{
            $.asShowMessage({ template: "error", message: "برای حذف یک اطلاع پایه را انتخاب نمایید. " })
        }
  });
    $btnSave.click(function () {
                 
 
        var newPermission;

         
         var id,guid;
          if ($drpPermission.asDropdown('selected')) {
              if($chkNew.is(':checked'))
           {
             guid= codeNewId
            id=permissionNewId
           }
            else{
                guid= $txtId.val()
                id=$drpPermission.asDropdown('selected').value
            }
            
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

           if($chkPermission.is(':checked') === false && $chkNew.is(':checked')){
            newParents[id]=true
        }
 
        $frm.asAjax({
            url: $.asUrls.security_Permission_save,
            data: JSON.stringify({
                Id: id,
                ParentId:$txtParentId.val(),
                TypeId:1032,
                PathOrUrl:"/",
                ViewRoleId: viewRoleId,
                ModifyRoleId: modifyRoleId,
                AccessRoleId: accessRoleId,
                Code:$txtCode.val(),
                Name: $txtName.val(),
                Description:$txtDescription.val(),
                Guid:guid,
                Order: $txtOrder.val(),
                Key:$drpMasterDataType.asDropdown('selected').value,
                Value:selectedVersion,
                ForeignKey3:$drpMasterDataType.asDropdown('selected').value === 101 ?$drpLink.asDropdown('selected').value : $drpMasterData.asDropdown('selected').value,
                ForeignKey2:$drpRole.asDropdown('selected').value,
                ForeignKey1:$drpActions.asDropdown('selected').value,
                Status: $chkStatus.is(':checked'),
                EnableCache: $chkCache.is(':checked'),
                EditMode: $chkEditMode.is(':checked'),
                IsLeaf: $chkPermission.is(':checked'),
                IsType: false,
                IsNew:$chkNew.is(':checked'),
                SlidingExpirationTimeInMinutes:$txtSlidingExpirationTimeInMinutes.val(),
                RowVersion: rowVersion
            }),
            success: function (permission) {
            if($chkNew.is(':checked')){
                   var newParent = false
                        if(newParents[selectedPermission.value]){
                            newParent = true
                            delete newParents[selectedPermission.value]
                        }
                          $drpPermission.asDropdown("addItem",{text:$txtName.val(),value:id},selectedPermission,newParent)
            }
          
                setPermission(permission)
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
            }
        }, { $form: $frm })
        

    });
          $drpPermission.on("change", function (event, item) {
               $btnTranslator.removeClass("disabled");
               $btnTranslator.prop("disabled",false);
              $divMasterdata.show();
                $divLink.hide();
               selectedPermission = item;
               
                if (typeof (item.value) != "undefined") {
                    getPermission(item.value)
                  
                }
          });
        $drpMasterData.on("change", function (event, item) {
             if ($drpMasterData.asDropdown('selected')) {
        getMasterData(item.value)
             }
    });
        
      $drpMasterDataType.on("change", function (event, item) {
          if(item.value === 101)
            loadLinks();
          else
            getTypeMasterData(null);
      
    });
    
    $btnTranslator.click(function () {
        $winTranslator.asModal('load',$.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.translator)},{name:"@isModal",value:true}])
        ,{        
            type:"masterDataKeyValue",
            id:permissionId,
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
var notFound = function(entity){
 $.asNotFound(entity)
}
var setMasterData = function(masterData){
            if (masterData.PathOrUrl !== null)
            $txtMasterDataUrl.val(masterData.PathOrUrl)
            else
            $txtMasterDataUrl.val("")
            
             if (masterData.Code !== null)
            {
                $txtMasterDataCode.val(masterData.Code)
            }
            else
            $txtMasterDataCode.val("")
            
             if (masterData.ForeignKey1){
           
             
                    if(masterData.ForeignKey1 === 757 
                    || masterData.ForeignKey1 === 753 
                    || masterData.ForeignKey1 === 752 
                    || masterData.ForeignKey1 === 754 
                    || masterData.ForeignKey1 === 755 
                    || masterData.ForeignKey1 === 979){
                          $divVersion.show();
                       
                          if(masterData.ForeignKey1 === 757 || masterData.ForeignKey1 === 979){
                                 $divVersion.asAfterTasks([
                                   loadData($.asInitService($.asUrls.develop_code_os_dotNet_getOutputVersions, [{ name: '@codeId', value: masterData.Id }]))
                                ], function (versions) {
                                    $drpVersion.asDropdown("reload",{localData: versions});
                              
                                    $drpVersion.asDropdown('selectValue', [selectedVersion === 0 ? null:selectedVersion])
                                
                                },{overlayClass: 'as-overlay-absolute'});
                          }else{
                             $divVersion.asAfterTasks([
                                   loadData($.asInitService($.asUrls.develop_code_os_dotNet_getDotNetCodesVersionsById, [{ name: '@id', value: masterData.Id }]))
                                ], function (versions) {
                                    var classes = [];
                                 
                                    $.each(versions,function(key,value){
                                        classes.push({Id:value.Version,Value:value.Version})
                                    });
                                     classes.push({Id:0,Value:"آخرین نگارشی که کامپایل شده یا خواهد شد"});
                                    $drpVersion.asDropdown("reload",{localData: classes});
                              
                                    $drpVersion.asDropdown('selectValue', [selectedVersion === 0 ? null:selectedVersion])
                                
                                },{overlayClass: 'as-overlay-absolute'});
                          }
                    }
             
             }else{
                 $divVersion.hide();
             }
                    
}
  var getMasterData = function (id) {
    $divMasterdata.show();
    $divLink.hide();
    if(id === null) return;
    $divForeign.asAjax({
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_get, [{ name: '@id', value: id }]),
        type: "get",
        success: function (masterData) {
            if($.isArray(masterData)){
                  if(masterData.length > 0)
                    setMasterData(masterData)
                    else
                     notFound("اطلاع پایه")
            }else{
                 if(masterData != null){
                        if(typeof(masterData) != "undefined")
                      setMasterData(masterData)
                        else
                     notFound("اطلاع پایه")
                 } else
                     notFound("اطلاع پایه")
            }
        }
    },{overlayClass: 'as-overlay-absolute'});
    
    
}
  var getPermission = function (id) {


                $drpPermission.asDropdown('selectValue', [], true)
                $drpPermission.asDropdown('selectValue', id)

            

    viewRoleId = 0
    url = ""
    ser = 0
    rowVersion = ""



    $frm.asAjax({
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_get, [{ name: '@id', value: id }]),
        type: "get",
        success: function (permission) {
            if($.isArray(permission)){
                  if(permission.length > 0)
                    setPermission(permission,true)
                    else
                     notFound("مجوز")
            }else{
                 if(permission != null){
                        if(typeof(permission) != "undefined")
                      setPermission(permission,true)
                        else
                     notFound("مجوز")
                 } else
                     notFound("مجوز")
            }
        }
    });

    
}
  var setPermission = function(permission,loadMasterData){
            if (permission.Key !== null){
                $drpMasterDataType.asDropdown('selectValue', permission.Key);
                if(loadMasterData){
                  
                    if(permission.Key === 101)
                    loadLinks(permission.ForeignKey3);
                    else
                    getTypeMasterData(permission.ForeignKey3);
                }
           
            }
            else
                $drpMasterDataType.asDropdown('selectValue', [], true);
                
            
            
            as("#divLastModifiUser").html(permission.LastModifieUser);
            as("#divLastModifiLocalDataTime").html(permission.LastModifieLocalDateTime);
            
             if (permission.ForeignKey2 !== null)
                $drpRole.asDropdown('selectValue', permission.ForeignKey2)
            else
                $drpRole.asDropdown('selectValue', [], true)
                
             if (permission.ForeignKey1 !== null)
                $drpActions.asDropdown('selectValue', permission.ForeignKey1)
            else
                $drpActions.asDropdown('selectValue', [], true)
                
                
           $txtId.prop("disabled", true)
              $chkNew.prop('checked', false)
            permissionId = permission.Id
         
            $txtPermissionId.val(permissionId)
          
            $txtParentId.val(permission.ParentId)
            rowVersion = permission.RowVersion
            $chkPermission.prop('checked', permission.IsLeaf)
            if(permission.IsLeaf)
            $divPermission.show();
            else
            $divPermission.hide();
        
            isLeaf = permission.IsLeaf

            $txtName.val(permission.Name)
            if (permission.Description != null)
            $txtDescription.val(permission.Description)
            else
            $txtDescription.val("")
            if (permission.Code !== null)
            {
                $txtCode.val(permission.Code)
            }
            else
            $txtCode.val("")
     
            $txtId.val(permission.Guid)
            codeNewId = permission.NewGuid;
            permissionNewId = permission.NewId;
            $txtVersion.val(permission.Version)
   
            if (permission.Order !== null)
            $txtOrder.val(permission.Order)
            else
            $txtOrder.val("")
            
            
            
            
            if (permission.Key !== null)
                $drpMasterDataType.asDropdown('selectValue', permission.Key);
            else
                $drpMasterDataType.asDropdown('selectValue', [], true);
            
            
            // if (permission.ForeignCode !== null)
            // $txtMasterDataCode.val(permission.ForeignCode)
            // else
            // $txtMasterDataCode.val("")
            
            //  if (permission.ForeignUrl !== null)
            // $txtMasterDataUrl.val(permission.ForeignUrl)
            // else
            // $txtMasterDataUrl.val("")
            
            
            if (permission.Value !== null)
            selectedVersion=permission.Value;
            else
            selectedVersion=0;
            
            
            
 
            $chkCache.prop('checked', permission.EnableCache)
            $chkEditMode.prop('checked', permission.EditMode)
            $chkStatus.prop('checked', permission.Status)

            if (permission.ViewRoleId !== null)
                $drpViewRole.asDropdown('selectValue', permission.ViewRoleId)
            else
                $drpViewRole.asDropdown('selectValue', [], true)
                
            if (permission.AccessRoleId != null)
                $drpAccessRole.asDropdown('selectValue', permission.AccessRoleId)
            else
                $drpAccessRole.asDropdown('selectValue', [], true)
                
            if (permission.ModifyRoleId != null)
                $drpModifyRole.asDropdown('selectValue', permission.ModifyRoleId)
            else
                $drpModifyRole.asDropdown('selectValue', [], true)
}  
    



