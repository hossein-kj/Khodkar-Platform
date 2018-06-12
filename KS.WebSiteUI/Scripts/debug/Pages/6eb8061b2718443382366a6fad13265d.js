 $('#i6eb8061b2718443382366a6fad13265d').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('Settings');var asPageEvent = '#i6eb8061b2718443382366a6fad13265d'; var asPageId = '.i6eb8061b2718443382366a6fad13265d.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({cms_masterDataKeyValue_GetByOtherLanguagesAndTypeId:"/odata/cms/MasterDataLocalKeyValues?$filter=(MasterDataKeyValue%2FTypeId%20eq%20@typeIdd)%20and%20(Language%20eq%20'@lang')&$expand=MasterDataKeyValue&$select=MasterDataKeyValue%2FId%2CMasterDataKeyValue%2FParentId%2CMasterDataKeyValue%2FCode%2CMasterDataKeyValue%2FOrder%2CMasterDataKeyValue%2FName%2CMasterDataKeyValue%2FKey%2CMasterDataKeyValue%2FValue%2CMasterDataKeyValue%2FIsLeaf%2CMasterDataKeyValue%2FPathOrUrl%2CName",cms_masterDataKeyValue_get:"/cms/masterdatakeyvalue/get/@id",develop_webConfig_settings_save:"/develop/webconfig/settings/save/",develop_webConfig_settings_dell:"/develop/webconfig/settings/delete/",develop_webConfig_settings_getSettingsByPagination:"/develop/webconfig/settings/GetSettingsByPagination/@orderBy/@skip/@take",develop_webConfig_settings_getMasterDataKeyValuePropertyName:"/develop/webconfig/settings/GetMasterDataKeyValuePropertyName/",cms_masterDataKeyValue_getTypeByOtherLanguages:"/odata/cms/MasterDataLocalKeyValues?$filter=(MasterDataKeyValue%2FIsType%20eq%20true)%20and%20(Language%20eq%20'@lang')&$expand=MasterDataKeyValue&$select=MasterDataKeyValue%2FId%2CMasterDataKeyValue%2FParentTypeId%2CMasterDataKeyValue%2FCode%2CMasterDataKeyValue%2FOrder%2CMasterDataKeyValue%2FName%2CMasterDataKeyValue%2FKey%2CMasterDataKeyValue%2FValue%2CMasterDataKeyValue%2FIsLeaf%2CMasterDataKeyValue%2FTypeId%2CName"}, $.asUrls);     var   $frm = as("#frmConfig"),
   $btnDell=as("#btnDell"),
   $btnEdit=as("#btnEdit"),
   $btnNew=as("#btnNew"),
   $btnExecute=as("#btnExecute"),
   $winAddOrEdit=as("#winAddOrEdit"),
   $grvSettings =as("#grvSettings"),
   $drpMasterDataType= as("#drpMasterDataType"),
    $drpMasterData = as("#drpMasterData"),
    $drpMasterDataProperty=as("#drpMasterDataProperty"),
    $drpJavaScriptType=as("#drpJavaScriptType"),
    $divMasterData=as("#divMasterData"),
    $divSetting=as("#divSetting"),
    $divJavaScriptType=as("#divJavaScriptType"),
    $chkIsMasterData=as("#chkIsMasterData"),
    $chkInjectToJavaScript=as("#chkInjectToJavaScript"),
    $txtKey=as("#txtKey"),
    $txtValue=as("#txtValue"),
    $txtDesc=as("#txtDesc"),
    validate,
    isNew=false,
         selectedItems={
         items:{}
     },
     masterDataProperty="Id",
     javaScriptType="None",
    templateInjectToJavaScript =
        '<div class="as-material-switch container-fluid @class"><div><input name="chkInjectToJavaScriptGrid" type="checkbox" @injectToJavaScript /><label class="label-default as-label" for="chkInjectToJavaScriptGrid" >  </label></div></div>';
        $winAddOrEdit.asModal(
    {backdrop:'static', keyboard: false}
    );
        var calculateSelectedConfig = function(event, rows){

        if(event.type==="selected"){
            selectedItems.items[rows[0].Key]={Key:rows[0].Key,Value:rows[0].Value,
            Description:rows[0].Description,MasterDataKeyValuePropertyName:rows[0].MasterDataKeyValuePropertyName,
            MasterDataKeyValueId:rows[0].MasterDataKeyValueId,JavaScriptType:rows[0].JavaScriptType,
            InjectToJavaScript:rows[0].InjectToJavaScript,MasterDataKeyValueTypeId:rows[0].MasterDataKeyValueTypeId}
        }else{
            delete selectedItems.items[rows[0].Key]
        }
    }

    $grvSettings.asBootGrid({
     
    rowCount:[10,25,50,100,-1],
    source:{
        url:''
    },
    requestHandler:function(request){

        var orderbyValue = "Name desc"
        var skip = 0
        if(request.current > 1)
        skip=(request.current - 1) * request.rowCount
        $.each(request.sort, function(key, value) {
                 orderbyValue = key + " " + value
                });
        request.url = $.asInitService($.asUrls.develop_webConfig_settings_getSettingsByPagination, [
            { name: '@orderby', value: orderbyValue }
            ,{ name: '@skip', value: skip }
             ,{ name: '@take', value: request.rowCount}]);
             
              selectedItems.items={};
              $grvSettings.asBootGrid("deselect");
             return request

    },
    formatters: {
        InjectToJavaScript: function (column, row)
        {
            return row.InjectToJavaScript ? templateInjectToJavaScript.replace('@injectToJavaScript','checked="checked"').replace('@class',''):templateInjectToJavaScript.replace('@injectToJavaScript','').replace('@class','inject-to-javascript');
        }
    },
        selection: true,
        rowSelect:true,
        multiSelect:true
}).on("selected.rs.jquery.asBootGrid", function(e, rows)
{
    calculateSelectedConfig(e,rows)
    
}).on("deselected.rs.jquery.asBootGrid", function(e, rows)
{
    calculateSelectedConfig(e,rows)
});
            
$drpMasterData.asDropdown("init","Choose the type of MasterDataKeyValue",{
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'MasterDataKeyValue.Id' },
            parentDataField: { name: 'MasterDataKeyValue.ParentId' },
            removeChildLessParent: false
        }
        , displayDataField: 'Name'
          , valueDataField: 'MasterDataKeyValue.Id',
        orderby: 'MasterDataKeyValue.Order'
    }
    , selectParents: true

});
    var loadeMasterDataType = function () {
    return $.asAjaxTask({
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_getTypeByOtherLanguages, [{ name: '@lang', value: $.asLang }])
    });
    }
        var loadeMasterDataProperty = function () {
    return $.asAjaxTask({
        url: $.asUrls.develop_webConfig_settings_getMasterDataKeyValuePropertyName
    });
    }
  as("#divButtons").asAfterTasks([
            loadeMasterDataType(),loadeMasterDataProperty()
        ], function (masterdataTypes,masterdataProperties) {
            $drpMasterDataType.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'MasterDataKeyValue.TypeId' },
            parentDataField: { name: 'MasterDataKeyValue.ParentTypeId' },
            removeChildLessParent: false
        },
        localData:masterdataTypes
        , displayDataField: 'Name'
          , valueDataField: 'MasterDataKeyValue.TypeId',
        orderby: 'MasterDataKeyValue.Order'
    }
    , selectParents: true
    // , multiple: true

}, { overlayClass: 'as-overlay-relative' });

     var flexParam = {
        source: {
         displayDataField: 'Key'
          , valueDataField: 'Value'
            ,idDataField: 'Key'
            ,localData: masterdataProperties
        },
        selectedValue:masterDataProperty,
        selectedSearchKey:'Key', 
        selectedItemKey:'Key',
        selectedItemtemplate: '<a style="color:#000 !important" class="btn btn-defaults dropdown-toggle" href="#" data-toggle="dropdown">@selectedText &nbsp; <span class="caret"></span></a>',
        itemTemplate: '<div class="as-flex-select-link" data-extera="@extera" data-text="@text" data-id="@id" data-value="@value" data-url="@url">@text</div>',
    };
        $drpMasterDataProperty.asFlexSelect(flexParam);
        flexParam.source.localData = [
            {Key:"String",Value:"String"},
            {Key:"Array",Value:"Array"},
            {Key:"Object",Value:"Object"},
            {Key:"Number",Value:"Number"},
            {Key:"Bool",Value:"Bool"},
            {Key:"None",Value:"None"}];
        $drpJavaScriptType.asFlexSelect(flexParam);
        });


    var loadTypeMasterData = function (url) {
    return $.asAjaxTask({
        url: url
    });
}
var notFound = function(){
 $.asNotFound(" MasterDataKeyValue")
}

 

    var getTypeMasterData = function(selectedId){ 
                var url = $.asInitService($.asUrls.cms_masterDataKeyValue_GetByOtherLanguagesAndTypeId, [{ name: '@lang', value: $.asLang }])
        var queryTypeIdTemp = "(MasterDataKeyValue%2FTypeId%20eq%20@typeIdd)"
        var queryParentTypeIdTemp = "(MasterDataKeyValue%2FParentTypeId%20eq%20@typeIdd)"
        var query = []
        // $.each($drpMasterDataType.asDropdown('selected'), function (i, v) {
                // if (v.selected){
                    query.push(queryTypeIdTemp.replace("@typeId",$drpMasterDataType.asDropdown('selected').value))
                    query.push(queryParentTypeIdTemp.replace("@typeId",$drpMasterDataType.asDropdown('selected').value))
                // }
                    
            // })
       
        $winAddOrEdit.asAfterTasks([
            loadTypeMasterData(url.replace("MasterDataKeyValue%2FTypeId%20eq%20@typeIdd","(" + query.join("%20or%20") + ")"))
        ], function (masterdatas) {
            $drpMasterData.asDropdown("reload",{localData: masterdatas});
            $drpMasterData.css({"margin-top":"0"});
            if(selectedId !== null)
            $drpMasterData.asDropdown('selectValue', selectedId)

        },{overlayClass: 'as-overlay-module'});

    }
    var setMasterDataProperty = function(text){
        $drpMasterDataProperty.asFlexSelect('setItem', text + '&nbsp;<span class="caret"></span>');
        masterDataProperty=text;
    }
    
        var setJavaScriptType = function(text){
        $drpJavaScriptType.asFlexSelect('setItem', text + '&nbsp;<span class="caret"></span>');
        javaScriptType=text;
    }

        var bindEvent = function () {
            
       $(asPageEvent).on($.asEvent.page.ready, function (event) {
              var validateRule = {
         txtKey:{
            required:  {
                depends: function (element) {
                return $chkIsMasterData.is(':checked') === false;
                }
            }
        },
        txtValue:{
            required: {
                depends: function (element) {
                return $chkIsMasterData.is(':checked') === false;
                }
            }
        },
        
        drpMasterData: {
            asType: 'asDropdown',
            required: {
                depends: function (element) {
                return $chkIsMasterData.is(':checked') === true;
                }
            }
        },
        drpMasterDataType: {
            asType: 'asDropdown',
            required: {
                depends: function (element) {
                return $chkIsMasterData.is(':checked') === true;
                }
            }
        }
    }
       validate =$frm.asValidate({ rules: validateRule});
            });
    
        $drpMasterDataProperty.on("selectedIndexChanged", function (event, id, value, text, url, extera) {
        if (text){
            setMasterDataProperty(text);
             }
        });
        
        $drpJavaScriptType.on("selectedIndexChanged", function (event, id, value, text, url, extera) {
        if (text){
            setJavaScriptType(text);
             }
        });
        
             $chkIsMasterData.change(function () {
                if(this.checked === true){
                    $divMasterData.show();
                    $divSetting.hide()
                }else{
                    $divSetting.show()
                     $divMasterData.hide();
                }
             });
             var clearModal = function(){
                   $txtValue.val("");
                  $txtKey.val("");
                  $txtDesc.val("");
                  $chkInjectToJavaScript.prop('checked', false)
                  $chkIsMasterData.prop('checked', false)
             }
                  var onSuccess = function(){
           $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
              selectedItems.items={};
              $grvSettings.asBootGrid("deselect");
     }
     $chkInjectToJavaScript.change(function () {
                if(this.checked === true){
                    $divJavaScriptType.show();
                }else{
                     $divJavaScriptType.hide();
                }
             });
   $btnNew.click(function () {
       clearModal();
        isNew=true;
         $divMasterData.hide();
         $divSetting.show();
        $winAddOrEdit.asModal("show");
   });
   
       $btnExecute.click(function () {
        
            
                     $winAddOrEdit.asAjax({
            url: $.asUrls.develop_webConfig_settings_save,
            data: JSON.stringify({
                                    Value:$txtValue.val(),
                Key:$txtKey.val(),
                MasterDataKeyValueId:$drpMasterData.asDropdown('selected').value,
                MasterDataKeyValuePropertyName:masterDataProperty,
                Description:$txtDesc.val(),
                InjectToJavaScript:$chkInjectToJavaScript.is(':checked'),
                IsMasterDataSetting:$chkIsMasterData.is(':checked'),
                JavaScriptType:javaScriptType
            }),
            success: function (result) {
                if( isNew===true){
                   
                    isNew=false;
                }else{
                     
                       $grvSettings.asBootGrid("remove");
                }
                 $grvSettings.asBootGrid("append",[result]);
                  
              onSuccess();
            }
        },{$form: $frm,overlayClass: "as-overlay-absolute"});
       });
       $btnEdit.click(function () {
                    clearModal();
                 var settings=[]
              $.each(selectedItems.items,function(i,v){
                settings.push(v)
            });
            
          if (settings.length === 1) {
              isNew=false;
              $divMasterData.hide();
              $divSetting.show();
                $winAddOrEdit.asModal("show");
              if(settings[0].MasterDataKeyValueTypeId !== 0){
                  $divMasterData.show();
                  $divSetting.hide();
                   $chkIsMasterData.prop('checked', true)
               $drpMasterDataType.asDropdown('selectValue', settings[0].MasterDataKeyValueTypeId)
              getTypeMasterData(settings[0].MasterDataKeyValueTypeId === 0 ? null:settings[0].MasterDataKeyValueId)
              }else{
                  $txtValue.val(settings[0].Value);
                  $txtKey.val(settings[0].Key);
              }
              setMasterDataProperty(settings[0].MasterDataKeyValuePropertyName);
                    $txtDesc.val(settings[0].Description);
                 $chkInjectToJavaScript.prop('checked', settings[0].InjectToJavaScript)
                 if(settings[0].InjectToJavaScript){
                       $divJavaScriptType.show();
                    setJavaScriptType((settings[0].JavaScriptType === null ||  typeof(settings[0].JavaScriptType) == "undefined" ) ? "None":settings[0].JavaScriptType);
                 }else
                        $divJavaScriptType.hide();
          }else{
                 $.asShowMessage({template:"error", message: "To edit, you must select a setting"});
          }
        });
   $btnDell.click(function () {
                 var settings=[]
              $.each(selectedItems.items,function(i,v){
                settings.push(v)
            });
            
          if (settings.length === 1) {
               $frm.asAjax({
            url: $.asUrls.develop_webConfig_settings_dell,
            data: JSON.stringify({
                Key: settings[0].Key
               
            }),
            error:function(){
                  $btnDell.button('reset')
            },
            success: function (result) {
                 $btnDell.button('reset')
                $grvSettings.asBootGrid("remove");
             onSuccess();
            }
        }, { $form: $frm,validate:false });
         $btnDell.button('loading') ;
          }else{
                $.asShowMessage({template:"error", message: "  To delete, you must select a setting"});
          }
    });
            
        as("#btnCancel").click(function () {
            
       $winAddOrEdit.asModal('hide');
        });

          $drpMasterDataType.on("change", function (event, item) {
            getTypeMasterData(null)
      
    });
    
    asOnPageDispose = function(){
          $grvSettings.asBootGrid("destroy");
          validate.destroy();
        }
}
bindEvent();  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });