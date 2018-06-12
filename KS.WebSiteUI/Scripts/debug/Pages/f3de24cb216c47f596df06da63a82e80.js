 $('#if3de24cb216c47f596df06da63a82e80').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('Links Management');var asPageEvent = '#if3de24cb216c47f596df06da63a82e80'; var asPageId = '.if3de24cb216c47f596df06da63a82e80.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({cms_link_getByLanguage:"/odata/cms/Links?$filter=Language%20eq%20'@lang'&$select=Id%2CParentId%2CText%2CHtml%2CUrl%2COrder%2CIsLeaf",cms_link_save:"/cms/link/save",cms_masterDataKeyValue_GetByOtherLanguagesAndTypeId:"/odata/cms/MasterDataLocalKeyValues?$filter=(MasterDataKeyValue%2FTypeId%20eq%20@typeIdd)%20and%20(Language%20eq%20'@lang')&$expand=MasterDataKeyValue&$select=MasterDataKeyValue%2FId%2CMasterDataKeyValue%2FParentId%2CMasterDataKeyValue%2FCode%2CMasterDataKeyValue%2FOrder%2CMasterDataKeyValue%2FName%2CMasterDataKeyValue%2FKey%2CMasterDataKeyValue%2FValue%2CMasterDataKeyValue%2FIsLeaf%2CMasterDataKeyValue%2FPathOrUrl%2CName",cms_languageAndCulture_public_getAll:"/cms/languageAndCulture/public/getAll",security_Role_getAllByOtherLanguage:"/odata/security/LocalRoles?$filter=(Language%20eq%20'@lang')&$expand=Role&$select=Role%2FId%2CRole%2FParentId%2CRole%2FName%2CRole%2FOrder%2CRole%2FIsLeaf%2CName",cms_link_get:"/odata/cms/Links?$filter=Id%20eq%20@idd&$select=Id%2CText%2CHtml%2CTypeId%2CIconPath%2CIsLeaf%2CUrl%2COrder%2CParentId%2CShowToSearchEngine%2CViewRoleId%2CModifyRoleId%2CAccessRoleId%2CAction%2CTransactionCode%2CIsMobile%2CRowVersion%2CStatus",cms_link_delete:"/cms/link/delete"}, $.asUrls); var
$frm =as("#frmLink"),
$drpLink=as("#drpLink"),
$drpLinkType=as("#drpLinkType"),
$chkIsLeaf=as("#chkIsLeaf"),
$drpLanguge=as("#drpLanguge"),
$chkMobile=as("#chkMobile"),
$txtName=as("#txtName"),
$txtIconPath=as("#txtIconPath"),
$txtHtml=as("#txtHtml"),
$txtCode=as("#txtCode"),
$txtAction=as("#txtAction"),
$txtUrl=as("#txtUrl"),
$txtOrder=as("#txtOrder"),
$drpViewRole=as("#drpViewRole"),
$drpAccessRole=as("#drpAccessRole"),
$drpModifyRole=as("#drpModifyRole"),
$chkStatus=as("#chkStatus"),
$chkShowToSerachEngin=as("#chkShowToSerachEngin"),
$btnSave=as("#btnSave"),
$btnDell=as("#btnDell"),
$chkNew=as("#chkNew"),
linkId,
rowVersion,
isLeaf=false,
viewRoleId= 0,
    modifyRoleId= 0,
    accessRoleId= 0,
    typeId= 0,
    selectedLink,
    selectedMenue,
    validate,
    newParents={},
    selectedLang;
    

 $drpLink.asDropdown("init","Chosse Country",{
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
    
     
    $drpLinkType.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'MasterDataKeyValue.Id' },
            parentDataField: { name: 'MasterDataKeyValue.ParentId' }
            //removeChildLessParent: true
        },
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByOtherLanguagesAndTypeId, [{ name: '@typeId', value: 1024 }, { name: '@lang', value: $.asLang }])
        , displayDataField: 'Name'
          , valueDataField: 'MasterDataKeyValue.Code',
        orderby: 'MasterDataKeyValue.Order'
    }
   , parentMode: "uniq"

});
    var loadLanguages = function () {
    return $.asAjaxTask({
        url: $.asUrls.cms_languageAndCulture_public_getAll
    });
    }
$drpLanguge.asAfterTasks([
    loadLanguages()
], function (languages) {
    var langParams = {
        source: {
            displayDataField: 'country',
            valueDataField: 'language',
            urlDataField: 'flagUrl',
            idDataField: 'culture',
            exteraDataField: 'isRightToLeft',
            localData: languages
        }
    }

     var lang = $.asGetDefaultsLnguageAndCulter()
    if (lang) {
           $drpLink.asDropdown("reload",{url: $.asInitService($.asUrls.cms_link_getByLanguage, [{ name: '@lang', value: lang.lang }])});
           selectedLang= lang.lang;
        langParams.selectedValue = lang.country
    }

    $drpLanguge.asFlexSelect(langParams);
},{overlayClass:"as-overlay-fixed"})
    var loadRoles = function () {
    return $.asAjaxTask({
       url: $.asInitService($.asUrls.security_Role_getAllByOtherLanguage, [{ name: '@lang', value: $.asLang}])
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
var notFound = function(){
 $.asNotFound("لینک")
}
var setLink = function(link){
     $chkNew.prop('checked', false)
            linkId = link.Id
            rowVersion = link.RowVersion
            $chkIsLeaf.prop('checked', link.IsLeaf)
            $chkMobile.prop('checked', link.IsMobile)
          
            isLeaf = link.IsLeaf

            $txtName.val(link.Text)

            if (link.TransactionCode !== null)
            $txtCode.val(link.TransactionCode)
            else
            $txtCode.val("")
            
             if (link.Html !== null)
            $txtHtml.val(link.Html)
            else
            $txtHtml.val("");
            
             var txtUrl = link.Url.toLowerCase();
        if(txtUrl[txtUrl.length - 1] != "/")
            txtUrl += "/";
        if(txtUrl[0] != "/")
            txtUrl = "/" + txtUrl;
            
       // txtUrl = txtUrl.substring( 0, txtUrl.length - 1)
       
         if (txtUrl.indexOf($.asMobileSign) > -1)
                txtUrl = txtUrl.replace( $.asMobileSign, "/");
        if (txtUrl.indexOf(selectedLang) > -1)
                txtUrl = txtUrl.replace( "/" + selectedLang + "/", "/");
   
            $txtUrl.val(txtUrl);

             if (link.Action !== null)
            $txtAction.val(link.Action)
            else
            $txtAction.val("")
            
             if (link.IconPath !== null)
            $txtIconPath.val(link.IconPath)
            else
            $txtIconPath.val("")
   
            if (link.Order !== null)
            $txtOrder.val(link.Order)
            else
            $txtOrder.val("")
 
          
            $chkShowToSerachEngin.prop('checked', link.ShowToSearchEngine)
            $chkStatus.prop('checked', link.Status)
            
             if (link.TypeId !== null)
                $drpLinkType.asDropdown('selectValue', link.TypeId);
            else
                $drpLinkType.asDropdown('selectValue', [], true);

            if (link.ViewRoleId !== null)
                $drpViewRole.asDropdown('selectValue', link.ViewRoleId);
            else
                $drpViewRole.asDropdown('selectValue', [], true);
            if (link.AccessRoleId !== null)
                $drpAccessRole.asDropdown('selectValue', link.AccessRoleId);
            else
                $drpAccessRole.asDropdown('selectValue', [], true);
            if (link.ModifyRoleId !== null)
                $drpModifyRole.asDropdown('selectValue', link.ModifyRoleId);
            else
                $drpModifyRole.asDropdown('selectValue', [], true);
}
     

var validateRule = {
        txtUrl:{
            required: true
        },
        txtName: {
            required: true,
            maxlength:128
        }
        ,
        drpViewRole: {
            asType: 'asDropdown',
            required: true
        },
        drpAccessRole: {
            asType: 'asDropdown',
            required: true
        },drpModifyRole: {
            asType: 'asDropdown',
            required: true
        },drpLinkType: {
            asType: 'asDropdown',
            required: true
        }

    }
       validate =$frm.asValidate({ rules: validateRule});
       
 var bindEvent =function(){
        $drpLanguge.on("selectedIndexChanged", function (event, id, value, text, url, extera) {
        if (url)
            $drpLanguge.asFlexSelect('setItem', '<img src="' + url + '">&nbsp;<span class="caret"></span>')
                 $drpLink.asDropdown("reload",{url: $.asInitService($.asUrls.cms_link_getByLanguage, [{ name: '@lang', value: value }])});
                 selectedLang = value
        })
        
          $drpLink.on("change", function (event, item) {
                  
                if (typeof (item.value) != "undefined") {
                    selectedMenue = item;
                       $frm.asAjax({
        url: $.asInitService($.asUrls.cms_link_get, [{ name: '@id', value: item.value }]),
        type: "get",
        success: function (link) {
        
             if($.isArray(link)){
                  if(link.length > 0){
                         selectedLink=link[0]
                    setLink(link[0])
                  }
                
                    else
                     notFound()
            }else{
                 if(link != null){
                        if(typeof(link) != "undefined"){
                                selectedLink=link
                       setLink(link)
                        }
                     
                        else
                     notFound()
                 } else
                     notFound()
            }
        }
    });
                  
                }
     
          });
          
           $chkNew.change(function () {
        if(this.checked === true)
       {
            if(isLeaf === true)
         {
                 $.asShowMessage({ template: "error", message:"Can not Add Leaf To Leaf" })
             $chkNew.prop('checked', false)
         }
            else{
            
            
            linkId=null;
            

            

            rowVersion='';

    
            
     
            $txtName.val("")
       
    
            $txtHtml.val("")
            
 
            $txtAction.val("")
         
            $txtCode.val("")
            
            
  
            $txtUrl.val("")
            
   
            $txtOrder.val("")
            
        
            $chkMobile.prop('checked', false)
            
     
        
            $chkStatus.prop('checked', false)
            
       
                $drpLinkType.asDropdown('selectValue', [], true)
       
                $drpViewRole.asDropdown('selectValue', [], true)
                
          
          
                $drpAccessRole.asDropdown('selectValue', [], true)
       
        
                $drpModifyRole.asDropdown('selectValue', [], true)
                
            }
       }else{
           setLink(selectedLink)
       }
    });
 
         $btnDell.click(function () {
             

        
             if(selectedLink){
                                    $frm.asAjax({
            url: $.asUrls.cms_link_delete,
            data: JSON.stringify({
                Id: selectedLink.Id
            }),
            success: function (result) {
            $drpLink.asDropdown("removeItem");
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
            }
        }, { $form: $frm });
             }else{
            $.asShowMessage({ template: "error", message: "For Delete Select One Link" })
        }
   
         });
         $btnSave.click(function () {

        var newService;
         
         var id,parentId;
       
              if($chkNew.is(':checked'))
           {
            if(selectedLink)
            parentId = selectedLink.Id
            else
            parentId = null
            id=null
           }
            else{
                if(selectedLink){
                     id=selectedLink.Id
                parentId=selectedLink.ParentId
                }
               
            }
            
       
        

        if($chkIsLeaf.is(':checked') === false && $chkNew.is(':checked')){
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
        var txtUrl = $txtUrl.val().toLowerCase();
        if(txtUrl[txtUrl.length - 1] != "/")
            txtUrl += "/";
        if(txtUrl[0] != "/")
            txtUrl = "/" + txtUrl;
            
       // txtUrl = txtUrl.substring( 0, txtUrl.length - 1)
       
         if (txtUrl.indexOf($.asMobileSign) > -1)
                txtUrl = txtUrl.replace( $.asMobileSign, "/");
        if (txtUrl.indexOf(selectedLang) > -1)
                txtUrl = txtUrl.replace( "/" + selectedLang + "/", "/");
                
         if($chkMobile.is(':checked') === true)
            txtUrl = "/" +  ($.asMobileSign).replace(/\//g, "") + txtUrl;
                    
            txtUrl = "/" + selectedLang + txtUrl;
               
        $frm.asAjax({
            url: $.asUrls.cms_link_save,
            data: JSON.stringify({
                Id: id,
                ParentId:parentId,
                Url: txtUrl,
                ViewRoleId: viewRoleId,
                ModifyRoleId: modifyRoleId,
                AccessRoleId: accessRoleId,
                Language:selectedLang,
                TypeId:$drpLinkType.asDropdown('selected').value,
                Action: $txtAction.val(),
                Text: $txtName.val(),
                Html:$txtHtml.val(),
                Order: $txtOrder.val(),
                Status: $chkStatus.is(':checked'),
                IsMobile: $chkMobile.is(':checked'),
                IsLeaf: $chkIsLeaf.is(':checked'),
                RowVersion: rowVersion
            }),
            success: function (link) {
                if($chkNew.is(':checked')){
                    var newParent = false
                     if(link.IsLeaf === false)
                              newParents[link.Id] = true
                    if(selectedMenue){
                           if(newParents[selectedMenue.value]){
                            newParent = true
                            delete newParents[selectedMenue.value]
                        }
                         $drpLink.asDropdown("addItem",{text:$txtName.val(),value:link.Id},selectedMenue,newParent)
                    }else{
                       
                        $drpLink.asDropdown("addItem",{text:$txtName.val(),value:link.Id},null,newParent)
                    }
                     
                       
                }
                 
                setLink(link)
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
            }
        }, { $form: $frm })

    });
            asOnPageDispose = function(){
        validate.destroy();
            }
 }
 bindEvent();



  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });