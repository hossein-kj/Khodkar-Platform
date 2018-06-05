var 
    $frm = as("#frmMasterData"),
    $divRoles = as("#divRoles"),
    $drpViewRole= as("#drpViewRole"),
    $drpModifyRole= as("#drpModifyRole"),
    $drpAccessRole= as("#drpAccessRole"),
    $drpMasterDataType= as("#drpMasterDataType"),
    $drpMasterData = as("#drpMasterData"),
    $txtName= as("#txtName"),
    $btnSave= as("#btnSave"),
    $chkEditMode= as("#chkEditeMode"),
    $winTranslator= $.asModalManager.get({url:$.asModalManager.urls.translator}),
    $txtDescription=as("#txtDescription"),
    $txtData=as("#txtData"),
    $txtOrder=as("#txtOrder"),
    $txtId=as("#txtId"),
    $txtMasterDataId=as("#txtMasterDataId"),
    $txtUrl=as("#txtUrl"),
    $txtValue=as("#txtValue"),
    $txtKey=as("#txtKey"),
    $txtSecondCode=as("#txtSecondCode"),
    $txtSecondPathOrUrl=as("#txtSecondPathOrUrl"),
    $txtForeignKey1=as("#txtForeignKey1"),
    $txtForeignKey2=as("#txtForeignKey2"),
    $txtForeignKey3=as("#txtForeignKey3"),
    $chkIsLeaf = as("#chkIsLeaf"),
    $chkNew = as("#chkNew"),
    $txtCode=as("#txtCode"),
    $txtTypeId=as("#txtTypeId"),
    $chkCache= as("#chkCache"),
    $chkStatus= as("#chkStatus"),
    $txtVersion = as("#txtVersion"),
    $txtSlidingExpirationTimeInMinutes = as("#txtSlidingExpirationTimeInMinutes"),
    $txtParentTypeId = as("#txtParentTypeId"),
    $chkType = as("#chkType"),
    $txtParentId = as("#txtParentId"),
    $btnTranslator = as("#btnTranslator"),
    $btnDell=as("#btnDell"),
    $drpProtocolsPathOrUrl=as("#drpProtocolsPathOrUrl"),
    $drpProtocolsSecondPathOrUrl=as("#drpProtocolsSecondPathOrUrl"),
    pathOrUrlProtocolId,
    secondPathOrUrlProtocolId,
    viewRoleId= 0,
    modifyRoleId= 0,
    accessRoleId= 0,
    typeId= 0,
    url= "",
   masterDataId= 0,
    isLeaf=false,
    rowVersion= "",
    codeNewId,
    masterDataNewId,
    temp = {},
    selectedMsterData={},
    validate,
    newParents={};


$winTranslator.asModal({width:800});


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
    , multiple: true

});

$drpProtocolsPathOrUrl.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: false
        },
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId, [{ name: '@typeId', value: 1046 }])
        , displayDataField: 'Name'
          , valueDataField: 'Id',
        orderby: 'Order'
    }
    , selectParents: false
//  , parentMode: "uniq"

});

$drpProtocolsSecondPathOrUrl.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: false
        },
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId, [{ name: '@typeId', value: 1046 }])
        , displayDataField: 'Name'
          , valueDataField: 'Id',
        orderby: 'Order'
    }
    , selectParents: false
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
         txtCode:{
            required: true
        },
        txtMasterDataId:{
            required: true
        },
        txtTypeId:{
        required: true
        },
        txtName: {
            required: true,
            maxlength:128
        },
        txtDescription:{
            maxlength:256
        },
        txtData:{
            maxlength:512
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
        drpProtocolsSecondPathOrUrl: {
            asType: 'asDropdown',
            required: true
        },
        drpProtocolsPathOrUrl: {
            asType: 'asDropdown',
            required: true
        }
    }
       validate =$frm.asValidate({ rules: validateRule});



var bindEvent = function () {
    
            $(asPageEvent).on($.asEvent.page.ready, function (event) {
              
    });
 
   $chkCache.change(function () {
        as("#divSlidingExpirationTimeInMinutes").prop("disabled", !this.checked)
    });
    
     $chkType.change(function () {
          
        if(this.checked === true)
      {
             
             if($chkIsLeaf.is(':checked') === true)
         {
                 $.asShowMessage({ template: "error", message: "یک اطلاع پایه نمی تواند نوع اطلاع پایه هم باشد" })
             $chkType.prop('checked', false)
         }
      }
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

            temp.Id = masterDataId;
            
            masterDataId=0;
            
            temp.Isleaf = isLeaf
            
            temp.ParentId = $txtParentId.val();
            $txtParentId.val($txtMasterDataId.val());
            
            temp.RowVersion = rowVersion;
            rowVersion='';

    
            temp.Name = $txtName.val();
            $txtName.val("")
       
            temp.Description = $txtDescription.val()
            $txtDescription.val("")
            
            temp.Data = $txtData.val()
            $txtData.val("")
            
         
            temp.Code = $txtCode.val()
            $txtCode.val("")
            
            
            temp.PathOrUrl = $txtUrl.val()
            $txtUrl.val("")
            
            temp.Guid = $txtId.val()
            
            temp.NewGuid=codeNewId;
            $txtId.val(codeNewId)
            
           
            temp.NewId = masterDataNewId
            $txtMasterDataId.val(masterDataNewId)
            
            temp.Version = $txtVersion.val();
            $txtVersion.val("0");
            
 
            temp.Order = $txtOrder.val();
            $txtOrder.val("");
            
            temp.Key = $txtKey.val();
            $txtKey.val("");
            
            temp.Value = $txtValue.val();
            $txtValue.val("");
            
            temp.SecondPathOrUrl = $txtSecondPathOrUrl.val();
            $txtSecondPathOrUrl.val("");
            temp.SecondCode = $txtSecondCode.val();
            $txtSecondCode.val("");
            temp.ForeignKey1 = $txtForeignKey1.val();
            $txtForeignKey1.val("");
            temp.ForeignKey2 = $txtForeignKey2.val();
            $txtForeignKey2.val("");
            temp.ForeignKey3 = $txtForeignKey3.val();
            $txtForeignKey3.val("");
            
            temp.ParentTypeId = $txtParentTypeId.val()
            $txtParentTypeId.val($txtTypeId.val())
            temp.TypeId = $txtTypeId.val()
         
            
            temp.EnableCache = $chkCache.prop('checked')
            $chkCache.prop('checked', false)
             temp.EditMode = $chkEditMode.prop('checked')
            $chkEditMode.prop('checked', false)
            temp.Status = $chkStatus.prop('checked')
            $chkStatus.prop('checked', false)
            
              temp.IsType = $chkType.prop('checked')
            $chkType.prop('checked', false)
            
            $drpProtocolsSecondPathOrUrl.asDropdown('selectValue', [], true)
            $drpProtocolsPathOrUrl.asDropdown('selectValue', [], true)
          
                temp.ViewRoleId=viewRoleId
                $drpViewRole.asDropdown('selectValue', [], true)
                
          
            temp.AccessRoleId=accessRoleId
                $drpAccessRole.asDropdown('selectValue', [], true)
       
            temp.ModifyRoleId=modifyRoleId
                $drpModifyRole.asDropdown('selectValue', [], true)
                
            }
      }else{
          setMasterData(temp)
      }
    });
    
  $chkIsLeaf.change(function () {
      
      
        if(this.checked === true)
       {
             if($chkType.is(':checked') === true)
         {
                 $.asShowMessage({ template: "error", message: "یک نوع نمی تواند اطلاع پایه هم باشد" })
             $chkIsLeaf.prop('checked', false);
             return;
         }
         
            
       }
  });
  $btnDell.click(function () {
       if ($drpMasterData.asDropdown('selected')) {
              $frm.asAjax({
            url: $.asUrls.cms_masterDataKeyValue_delete,
            data: JSON.stringify({
                Id: $drpMasterData.asDropdown('selected').value
            }),
            success: function (masterData) {
                $drpMasterData.asDropdown("removeItem");
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration ,showTime:10000000 });
            }
        }, { $form: $frm });
       }else{
            $.asShowMessage({ template: "error", message: "برای حذف یک اطلاع پایه را انتخاب نمایید. " })
        }
  });
    $btnSave.click(function () {
         if($chkType.is(':checked') === true && $chkIsLeaf.is(':checked') === true)
         {
                 $.asShowMessage({ template: "error", message: "یک نوع نمی تواند اطلاع پایه هم باشد" })
       return;
         }
                 
 
        var newMasterData;

         
         var id,guid;
          if ($drpMasterData.asDropdown('selected')) {
              if($chkNew.is(':checked'))
           {
             guid= codeNewId
            id=masterDataNewId
           }
            else{
                guid= $txtId.val()
                id=$drpMasterData.asDropdown('selected').value
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

           if($chkIsLeaf.is(':checked') === false && $chkNew.is(':checked')){
            newParents[id]=true
        }
        
        $frm.asAjax({
            url: $.asUrls.cms_masterDataKeyValue_save,
            data: JSON.stringify({
                Id: id,
                ParentId:$txtParentId.val(),
                TypeId:$txtTypeId.val(),
                ParentTypeId:$txtParentTypeId.val(),
                PathOrUrl: $txtUrl.val(),
                ViewRoleId: viewRoleId,
                ModifyRoleId: modifyRoleId,
                AccessRoleId: accessRoleId,
                Code:$txtCode.val(),
                Name: $txtName.val(),
                Description:$txtDescription.val(),
                Data:$txtData.val(),
                Guid:guid,
                Order: $txtOrder.val(),
                Key:$txtKey.val(),
                Value:$txtValue.val(),
                ForeignKey3:$txtForeignKey3.val(),
                ForeignKey2:$txtForeignKey2.val(),
                ForeignKey1:$txtForeignKey1.val(),
                SecondPathOrUrl:$txtSecondPathOrUrl.val(),
                SecondCode:$txtSecondCode.val(),
                Status: $chkStatus.is(':checked'),
                EnableCache: $chkCache.is(':checked'),
                EditMode: $chkEditMode.is(':checked'),
                IsLeaf: $chkIsLeaf.is(':checked'),
                IsType: $chkType.is(':checked'),
                IsNew:$chkNew.is(':checked'),
                PathOrUrlProtocolId:pathOrUrlProtocolId,
                SecondPathOrUrlProtocolId:secondPathOrUrlProtocolId,
                SlidingExpirationTimeInMinutes:$txtSlidingExpirationTimeInMinutes.val(),
                RowVersion: rowVersion
            }),
            success: function (masterData) {
            if($chkNew.is(':checked')){
                   var newParent = false
                        if(newParents[selectedMsterData.value]){
                            newParent = true
                            delete newParents[selectedMsterData.value]
                        }
                          $drpMasterData.asDropdown("addItem",{text:$txtName.val(),value:id},selectedMsterData,newParent)
            }
          
                setMasterData(masterData)
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration ,showTime:10000000 });
            }
        }, { $form: $frm })
        

    });
    
        $drpMasterData.on("change", function (event, item) {
                 selectedMsterData = item;
                $btnTranslator.removeClass("disabled")
                $btnTranslator.prop("disabled",false);
                if (typeof (item.value) != "undefined") {
                    getMasterData(item.value)
                  
                }
    });
        
      $drpMasterDataType.on("change", function (event, item) {
        var url = $.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId
        var queryTypeIdTemp = "(TypeId%20eq%20@typeIdd)"
        var queryParentTypeIdTemp = "(ParentTypeId%20eq%20@typeIdd)"
        var query = []
        $.each($drpMasterDataType.asDropdown('selected'), function (i, v) {
                if (v.selected){
                    query.push(queryTypeIdTemp.replace("@typeId",v.value))
                    query.push(queryParentTypeIdTemp.replace("@typeId",v.value))
                }
                    
            });
            
        $drpMasterData.asDropdown("reload",{url: url.replace("TypeId%20eq%20@typeIdd","(" + query.join("%20or%20") + ")")});
        $drpMasterData.css({"margin-top":"0"})
      
    });
    
     $drpProtocolsPathOrUrl.on("change", function (event, item) {
                 pathOrUrlProtocolId = item.value;
                 console.log(pathOrUrlProtocolId);
    });
    
    $drpProtocolsSecondPathOrUrl.on("change", function (event, item) {
                 secondPathOrUrlProtocolId = item.value;
                 
    });
    
    $btnTranslator.click(function () {
        $winTranslator.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.translator)},{name:"@isModal",value:true}])
        ,{
            type:"masterDataKeyValue",
            id:masterDataId,
            name:$txtName.val(),
            description:$txtDescription.val()
            
        })
    });

    
        asOnPageDispose = function(){
        validate.destroy();
    }
}
bindEvent();
var notFound = function(){
 $.asNotFound("اطلاع پایه")
}

var getMasterData = function (id) {


                $drpMasterData.asDropdown('selectValue', [], true)
                $drpMasterData.asDropdown('selectValue', id)

            

    viewRoleId = 0
    url = ""
    ser = 0
    rowVersion = ""



    $frm.asAjax({
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_get, [{ name: '@id', value: id }]),
        type: "get",
        success: function (masterData) {
            if($.isArray(masterData)){
                  if(masterData.length > 0)
                    setMasterData(masterData)
                    else
                     notFound()
            }else{
                 if(masterData != null){
                        if(typeof(masterData) != "undefined")
                      setMasterData(masterData)
                        else
                     notFound()
                 } else
                     notFound()
            }
        }
    });
    
    
}

var getProtocole=function(pathOrUrl){
     
              if(pathOrUrl[0] === "~"){
                return 1004;
              }else if(pathOrUrl[0] === "/" && pathOrUrl[1] === "/"){
                return 1005;
              }else  if(pathOrUrl[0] === "/"){
                return 1000;
              }else if(pathOrUrl.toLowerCase().indexOf("ftp://") === 0){
                return 1003;
              }else if(pathOrUrl.toLowerCase().indexOf("http://") === 0){
                return 1001;
              }else if(pathOrUrl.toLowerCase().indexOf("https://") === 0){
                return 1002;
              }
              return null;
}
    
var setMasterData = function(masterData){
   
           $txtId.prop("disabled", true)
              $chkNew.prop('checked', false)
            masterDataId = masterData.Id
         
            $txtMasterDataId.val(masterDataId)
          
            $txtParentId.val(masterData.ParentId)
            rowVersion = masterData.RowVersion
            $chkIsLeaf.prop('checked', masterData.IsLeaf)
            $chkType.prop('checked', masterData.IsType)
        
            isLeaf = masterData.IsLeaf

            $txtName.val(masterData.Name)
            if (masterData.Description != null)
            $txtDescription.val(masterData.Description)
            else
            $txtDescription.val("")
            
            if (masterData.Data != null)
            $txtData.val(masterData.Data)
            else
            $txtData.val("")
            
            
            if (masterData.Code !== null)
            {
                $txtCode.val(masterData.Code)
            }
            else
            $txtCode.val("")
            if (masterData.PathOrUrl !== null){
              $txtUrl.val(masterData.PathOrUrl)
              var protocol = getProtocole(masterData.PathOrUrl);
              if(protocol !== null){
                $drpProtocolsPathOrUrl.asDropdown('selectValue', protocol);
                pathOrUrlProtocolId=protocol; 
              }else{
                     $drpProtocolsPathOrUrl.asDropdown('selectValue', [], true);
                }
            }else{
                $txtUrl.val("");
             $drpProtocolsPathOrUrl.asDropdown('selectValue', [], true);
            }
            
             
            $txtId.val(masterData.Guid)
            codeNewId = masterData.NewGuid;
            masterDataNewId = masterData.NewId;
            $txtVersion.val(masterData.Version)
   
            if (masterData.Order !== null)
            $txtOrder.val(masterData.Order)
            else
            $txtOrder.val("")
            
            if (masterData.Key !== null)
            $txtKey.val(masterData.Key)
            else
            $txtKey.val("")
            
            if (masterData.Value !== null)
            $txtValue.val(masterData.Value)
            else
            $txtValue.val("")
            
             if (masterData.SecondCode !== null)
            $txtSecondCode.val(masterData.SecondCode)
            else
            $txtSecondCode.val("")
            
             if (masterData.SecondPathOrUrl !== null){
               $txtSecondPathOrUrl.val(masterData.SecondPathOrUrl)
              var protocol = getProtocole(masterData.SecondPathOrUrl);
              if(protocol !== null){
                 $drpProtocolsSecondPathOrUrl.asDropdown('selectValue', protocol);
                secondPathOrUrlProtocolId=protocol;
              }else{
                     $drpProtocolsSecondPathOrUrl.asDropdown('selectValue', [], true);
                }
             } else{
                 $txtSecondPathOrUrl.val("");
                 $drpProtocolsSecondPathOrUrl.asDropdown('selectValue', [], true);
             }
            
            
             if (masterData.ForeignKey1 !== null)
            $txtForeignKey1.val(masterData.ForeignKey1)
            else
            $txtForeignKey1.val("")
            
             if (masterData.ForeignKey2 !== null)
            $txtForeignKey2.val(masterData.ForeignKey2)
            else
            $txtForeignKey2.val("")
            
             if (masterData.ForeignKey3 !== null)
            $txtForeignKey3.val(masterData.ForeignKey3)
            else
            $txtForeignKey3.val("")
            
            $txtTypeId.val(masterData.TypeId)
            if (masterData.ParentTypeId !== null)
            $txtParentTypeId.val(masterData.ParentTypeId)
            else
            $txtParentTypeId.val("")
            
            
            
 
            $chkCache.prop('checked', masterData.EnableCache)
            $chkEditMode.prop('checked', masterData.EditMode)
            $chkStatus.prop('checked', masterData.Status)

            if (masterData.ViewRoleId !== null)
                $drpViewRole.asDropdown('selectValue', masterData.ViewRoleId)
            else
                $drpViewRole.asDropdown('selectValue', [], true)
                
            if (masterData.AccessRoleId != null)
                $drpAccessRole.asDropdown('selectValue', masterData.AccessRoleId)
            else
                $drpAccessRole.asDropdown('selectValue', [], true)
                
            if (masterData.ModifyRoleId != null)
                $drpModifyRole.asDropdown('selectValue', masterData.ModifyRoleId)
            else
                $drpModifyRole.asDropdown('selectValue', [], true)
}


