var 
    $frm = as("#frmRoles"),
    $txtRoleId=as("#txtRoleId"),
    $divRoles = as("#divRoles"),
    $drpViewRole= as("#drpViewRole"),
    $drpModifyRole= as("#drpModifyRole"),
    $drpAccessRole= as("#drpAccessRole"),
    $drpRoles = as("#drpRoles"),
    $txtName= as("#txtName"),
    $btnSave= as("#btnSave"),
    $winTranslator= $.asModalManager.get({url:$.asModalManager.urls.translator}),
    $txtDescription=as("#txtDescription"),
    $txtOrder=as("#txtOrder"),
    $chkIsLeaf = as("#chkIsLeaf"),
    $chkNew = as("#chkNew"),
    $chkStatus= as("#chkStatus"),
    $txtParentId = as("#txtParentId"),
    $btnTranslator = as("#btnTranslator"),
    $btnDell=as("#btnDell"),
    viewRoleId= 0,
    modifyRoleId= 0,
    accessRoleId= 0,
   roleId= 0,
    isLeaf=false,
    temp = {},
    selectedRole={},
    validate,
    newParents={};

$winTranslator.asModal({width:800});

var loadRoles = function () {
    return $.asAjaxTask({
       url: $.asInitService($.asUrls.security_Role_getAllByOtherLanguage, [{ name: '@lang', value: $.asLang }])
    });
}

$divRoles.asAfterTasks([
    loadRoles()
], function (roles) {

     $drpRoles.asDropdown({
        source: {
            hierarchy:
            {
                type: 'flat',
                keyDataField: { name: 'Role.Id' },
                parentDataField: { name: 'Role.ParentId' },
                childrenDataField: 'Children',
                removeChildLessParent: false
            },
            valueDataField: 'Role.Id',
            displayDataField: 'Name',
            orderby: 'Role.Order',
            localData: roles
            //url: '/cms/AllMenus'
            //url: 'Security/Access'
        }
     , selectParents: true
    });
    
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




var validateRule = {
        txtName: {
            required: true,
            maxlength:128
        },
        txtDescription:{
            required: true,
            maxlength:256
        },
         txtOrder:{
            required:true
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
        }
    }
       validate =$frm.asValidate({ rules: validateRule});



var bindEvent = function () {
    
            $(asPageEvent).on($.asEvent.page.ready, function (event) {
              
    });
 
    
      $chkNew.change(function () {
          
        if(this.checked === true)
      {
             
            if(isLeaf === true)
         {
                 $.asShowMessage({ template: "error", message: "There is no way to add a role to another role" })
             $chkNew.prop('checked', false)
         }
            else{
            
   

            temp.Id = roleId;
            
            roleId=0;
            
            temp.Isleaf = isLeaf
            
            temp.ParentId = $txtParentId.val();
            $txtParentId.val($txtRoleId.val());

    
            temp.Name = $txtName.val();
            $txtName.val("")
       
            temp.Description = $txtDescription.val()
            $txtDescription.val("")
            
            
 
            temp.Order = $txtOrder.val();
            $txtOrder.val("");
          

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
          setRoleData(temp)
      }
    });
    
  $chkIsLeaf.change(function () {
      
      
        if(this.checked === true)
       {
         
            
       }
  });
  $btnDell.click(function () {
       if ($drpRoles.asDropdown('selected')) {
              $frm.asAjax({
            url: $.asUrls.security_Role_delete,
            data: JSON.stringify({
                Id: $drpRoles.asDropdown('selected').value
            }),
            success: function (masterData) {
                // $drpRoles.asDropdown("removeItem");
                  $drpRoles.asDropdown("reload",{url:  $.asInitService($.asUrls.security_Role_getAllByOtherLanguage, [{ name: '@lang', value: $.asLang }])});
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration ,showTime:10000000 });
            }
        }, { $form: $frm });
       }else{
            $.asShowMessage({ template: "error", message: "To remove , select a role" })
        }
  });
    $btnSave.click(function () {
 
        var newRole;

         
         var id;
          if ($drpRoles.asDropdown('selected')) {
              if($chkNew.is(':checked'))
           {
            id=0
           }
            else{
                id=$drpRoles.asDropdown('selected').value
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
            url: $.asUrls.security_Role_save,
            data: JSON.stringify({
                Id: id,
                ParentId:$txtParentId.val(),
                ViewRoleId: viewRoleId,
                ModifyRoleId: modifyRoleId,
                AccessRoleId: accessRoleId,
                Name: $txtName.val(),
                Description:$txtDescription.val(),
                Order: $txtOrder.val(),
                Status: $chkStatus.is(':checked'),
                IsLeaf: $chkIsLeaf.is(':checked'),
                IsNew:$chkNew.is(':checked')
            }),
            success: function (role) {
            if($chkNew.is(':checked')){
                //   var newParent = false
                //         if(newParents[selectedRole.value]){
                      
                //             newParent = true
                //             delete newParents[selectedRole.value]
                //         }
                //           $drpRoles.asDropdown("addItem",{text:role.Description,value:role.Id},selectedRole,newParent);
                          
                           $drpRoles.asDropdown("reload",{url:$.asInitService($.asUrls.security_Role_getAllByOtherLanguage, [{ name: '@lang', value: $.asLang }]) });
            }
          
                setRoleData(role)
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration ,showTime:10000000 });
            }
        }, { $form: $frm })
        

    });
    
        $drpRoles.on("change", function (event, item) {
                 selectedRole = item;
                $btnTranslator.removeClass("disabled")
                $btnTranslator.prop("disabled",false);
                if (typeof (item.value) != "undefined") {
                    getRole(item.value)
                  
                }
    });
    
    
    $btnTranslator.click(function () {
        $winTranslator.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.translator)},{name:"@isModal",value:true}])
        ,{
            type:"role",
            id:roleId,
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
 $.asNotFound("Role")
}

var getRole = function (id) {


                $drpRoles.asDropdown('selectValue', [], true)
                $drpRoles.asDropdown('selectValue', id)


    $frm.asAjax({
        url: $.asInitService($.asUrls.security_Role_get, [{ name: '@id', value: id }]),
        type: "get",
        success: function (role) {
            if($.isArray(role)){
                  if(role.length > 0)
                    setRoleData(role[0])
                    else
                     notFound()
            }else{
                 if(role != null){
                        if(typeof(role) != "undefined")
                      setRoleData(role)
                        else
                     notFound()
                 } else
                     notFound()
            }
        }
    });
    
    
}

    
var setRoleData = function(role){
                console.dir(role)
              $chkNew.prop('checked', false)
            roleId = role.Id
         
            $txtRoleId.val(roleId)
          
            $txtParentId.val(role.ParentId)
         
            $chkIsLeaf.prop('checked', role.IsLeaf)
        
            isLeaf = role.IsLeaf

            $txtName.val(role.Name)
            if (role.Description != null)
            $txtDescription.val(role.Description)
            else
            $txtDescription.val("")

   
 
   
            if (role.Order !== null)
            $txtOrder.val(role.Order)
            else
            $txtOrder.val("")
            
          
            
            
           
            $chkStatus.prop('checked', role.Status)

            if (role.ViewRoleId !== null)
                $drpViewRole.asDropdown('selectValue', role.ViewRoleId)
            else
                $drpViewRole.asDropdown('selectValue', [], true)
                
            if (role.AccessRoleId != null)
                $drpAccessRole.asDropdown('selectValue', role.AccessRoleId)
            else
                $drpAccessRole.asDropdown('selectValue', [], true)
                
            if (role.ModifyRoleId != null)
                $drpModifyRole.asDropdown('selectValue', role.ModifyRoleId)
            else
                $drpModifyRole.asDropdown('selectValue', [], true)
}


