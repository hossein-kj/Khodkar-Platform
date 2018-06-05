var 
    $frm = as("#frmRoles"),
    $txtGroupId=as("#txtGroupId"),
    $divRoles = as("#divRoles"),
    $drpRoles=as("#drpRoles"),
    $drpViewRole= as("#drpViewRole"),
    $drpModifyRole= as("#drpModifyRole"),
    $drpAccessRole= as("#drpAccessRole"),
    $drpGroups = as("#drpGroups"),
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
    $divGroupRoles=as("#divGroupRoles"),
    $btnDell=as("#btnDell"),
    roleList=[],
    addedRoleList=[],
    removedRoleList=[],
    ViewRoleId= 0,
    ModifyRoleId= 0,
    AccessRoleId= 0,
   groupId= 0,
    isLeaf=false,
    temp = {},
    selectedGroup={},
    validate,
    newParents={};
$divGroupRoles.hide();

$winTranslator.asModal({width:800});


 $drpGroups.asDropdown({
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
            displayDataField: 'Description',
            orderby: 'Order',
            url: $.asUrls.security_Group_getAllByDefaultsLanguage
        }
     , selectParents: true
    });
    
 var getGroupRoles = function (groupId) {

    $drpRoles.asDropdown('selectValue', [], true)

    roleList =[]
    removedRoleList =[]
    addedRoleList=[]

    $drpRoles.asAjax({
        url: $.asInitService($.asUrls.security_GroupRoles_getByGroupId, [{ name: '@groupId', value: groupId }]),
        type: "get",
        success: function (roles) {
               
             if($.isArray(roles)){
                  if(roles.length > 0){
                     $.each(roles, function (i, v) {
                         roleList.push(v.RoleId);
                    })
                  }
             }
              if (roleList.length != 0)
                  $drpRoles.asDropdown('selectValue', roleList);
        }
    });
}

var loadRoles = function () {
    return $.asAjaxTask({
        url:$.asUrls.security_Role_getAllByDefaultsLanguage
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
                keyDataField: { name: 'Id' },
                parentDataField: { name: 'ParentId' },
                childrenDataField: 'Children',
                removeChildLessParent: true
            },
            valueDataField: 'Id',
            displayDataField: 'Description',
            orderby: 'Order',
            localData: roles
        },
        multiple: true
     
    });
    
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
         drpRoles: {
            asType: 'asDropdown',
            required: {
                depends: function (element) {
                return $chkIsLeaf.is(':checked')===true;
                }
            }
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
                 $.asShowMessage({ template: "error", message: "امکان افزودن یک گروه به گروه دیگر وجود ندارد" })
             $chkNew.prop('checked', false)
         }
            else{
            
   

            temp.Id = groupId;
            
            groupId=0;
            
            temp.Isleaf = isLeaf
            
            temp.ParentId = $txtParentId.val();
            $txtParentId.val($txtGroupId.val());
            

    
            temp.Name = $txtName.val();
            $txtName.val("")
       
            temp.Description = $txtDescription.val()
            $txtDescription.val("")
            
            
 
            temp.Order = $txtOrder.val();
            $txtOrder.val("");
          

            temp.Status = $chkStatus.prop('checked')
            $chkStatus.prop('checked', false)

          
                temp.ViewRoleId=ViewRoleId
                $drpViewRole.asDropdown('selectValue', [], true)
                
          
            temp.AccessRoleId=AccessRoleId
                $drpAccessRole.asDropdown('selectValue', [], true)
       
            temp.ModifyRoleId=ModifyRoleId
                $drpModifyRole.asDropdown('selectValue', [], true)
                
            }
      }else{
          setGroupData(temp)
      }
    });
    
  $chkIsLeaf.change(function () {

        if(this.checked === true)
       {
         $divGroupRoles.show();
            
       }else{
           $divGroupRoles.hide();
       }
  });
  $btnDell.click(function () {
       if ($drpGroups.asDropdown('selected')) {
              $frm.asAjax({
            url: $.asUrls.security_Group_delete,
            data: JSON.stringify({
                Id: $drpGroups.asDropdown('selected').value
            }),
            success: function (masterData) {
                // $drpGroups.asDropdown("removeItem");
                  $drpGroups.asDropdown("reload",{url: $.asUrls.security_Group_getAllByDefaultsLanguage});
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration ,showTime:10000000 });
            }
        }, { $form: $frm });
       }else{
            $.asShowMessage({ template: "error", message: "برای حذف یک گروه را انتخاب نمایید" })
        }
  });
    $btnSave.click(function () {
 
        var newRole;

         
         var id;
          if ($drpGroups.asDropdown('selected')) {
              if($chkNew.is(':checked'))
           {
            id=0
           }
            else{
                id=$drpGroups.asDropdown('selected').value
            }
            
        }

            
        if ($drpViewRole.asDropdown('selected')) {
            ViewRoleId = $drpViewRole.asDropdown('selected').value
        }
        if ($drpModifyRole.asDropdown('selected')) {
            ModifyRoleId = $drpModifyRole.asDropdown('selected').value
        }
        if ($drpAccessRole.asDropdown('selected')) {
            AccessRoleId = $drpAccessRole.asDropdown('selected').value
        }

           if($chkIsLeaf.is(':checked') === false && $chkNew.is(':checked')){
            newParents[id]=true
        }
        
        $frm.asAjax({
            url: $.asUrls.security_Group_save,
            data: JSON.stringify({
                Id: id,
                ParentId:$txtParentId.val(),
                ViewRoleId: ViewRoleId,
                ModifyRoleId: ModifyRoleId,
                AccessRoleId: AccessRoleId,
                Name: $txtName.val(),
                RemovedList:removedRoleList,
                AddedList:addedRoleList,
                Description:$txtDescription.val(),
                Order: $txtOrder.val(),
                Status: $chkStatus.is(':checked'),
                IsLeaf: $chkIsLeaf.is(':checked'),
                IsNew:$chkNew.is(':checked')
            }),
            success: function (group) {
            if($chkNew.is(':checked')){
                //   var newParent = false
                //         if(newParents[selectedGroup.value]){
                      
                //             newParent = true
                //             delete newParents[selectedGroup.value]
                //         }
                //           $drpGroups.asDropdown("addItem",{text:group.Description,value:group.Id},selectedGroup,newParent);
                          
                           $drpGroups.asDropdown("reload",{url:  $.asUrls.security_Group_getAllByDefaultsLanguage});
            }
          
                setGroupData(group)
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration ,showTime:10000000 });
            }
        }, { $form: $frm })
        

    });
    
        
         $drpRoles.on("change", function (event, item) {
             
                     
             if (typeof (item.value) != "undefined") {
   
                   
                        if(roleList.indexOf(item.value) > -1){
                            if(item.selected === false)
                                removedRoleList.push(item.value)
                                else{
                                   
                                     if (removedRoleList.indexOf(item.value) > -1) {
                                        removedRoleList.splice(index, 1);
                                    }
                                }
                        }else{
                                   if(item.selected === true)
                                addedRoleList.push(item.value)
                                else{
                                  
                                     if (addedRoleList.indexOf(item.value) > -1) {
                                        addedRoleList.splice(index, 1);
                                    }
                                }
                        }
                  
                }
             
         });
        
        $drpGroups.on("change", function (event, item) {
            addedRoleList=removedRoleList=[];
                 selectedGroup = item;
                $btnTranslator.removeClass("disabled")
                $btnTranslator.prop("disabled",false);
                if (typeof (item.value) != "undefined") {
                    getGroup(item.value)
                  
                }
        });
    
    
    $btnTranslator.click(function () {
        $winTranslator.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.translator)},{name:"@isModal",value:true}])
        ,{
            type:"group",
            id:groupId,
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
 $.asNotFound("گروه")
}

var getGroup = function (id) {
    
    getGroupRoles(id);

    $drpGroups.asDropdown('selectValue', [], true)
     $drpGroups.asDropdown('selectValue', id)

    $frm.asAjax({
        url: $.asInitService($.asUrls.security_Group_get, [{ name: '@id', value: id }]),
        type: "get",
        success: function (group) {
            if($.isArray(group)){
                  if(group.length > 0)
                    setGroupData(group[0])
                    else
                     notFound()
            }else{
                 if(group != null){
                        if(typeof(group) != "undefined")
                      setGroupData(group)
                        else
                     notFound()
                 } else
                     notFound()
            }
        }
    });
    
    
}

    
var setGroupData = function(group){
             
              $chkNew.prop('checked', false)
            groupId = group.Id
         
            $txtGroupId.val(groupId)
          
            $txtParentId.val(group.ParentId)
         
            $chkIsLeaf.prop('checked', group.IsLeaf)
            $chkIsLeaf.trigger("change");
            isLeaf = group.IsLeaf

            $txtName.val(group.Name)
            if (group.Description != null)
            $txtDescription.val(group.Description)
            else
            $txtDescription.val("")

   
 
   
            if (group.Order !== null)
            $txtOrder.val(group.Order)
            else
            $txtOrder.val("")
            
          
            
            
           
            $chkStatus.prop('checked', group.Status)

            if (group.ViewRoleId !== null)
                $drpViewRole.asDropdown('selectValue', group.ViewRoleId)
            else
                $drpViewRole.asDropdown('selectValue', [], true)
                
            if (group.AccessRoleId != null)
                $drpAccessRole.asDropdown('selectValue', group.AccessRoleId)
            else
                $drpAccessRole.asDropdown('selectValue', [], true)
                
            if (group.ModifyRoleId != null)
                $drpModifyRole.asDropdown('selectValue', group.ModifyRoleId)
            else
                $drpModifyRole.asDropdown('selectValue', [], true)
}


