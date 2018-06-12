 $('#iaa1028a02226443b9b25cb0672a418e7').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('Group Manager');var asPageEvent = '#iaa1028a02226443b9b25cb0672a418e7'; var asPageId = '.iaa1028a02226443b9b25cb0672a418e7.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({cms_link_getByLanguage:"/odata/cms/Links?$filter=Language%20eq%20'@lang'&$select=Id%2CParentId%2CText%2CHtml%2CUrl%2COrder%2CIsLeaf",cms_masterDataKeyValue_GetByOtherLanguagesAndTypeId:"/odata/cms/MasterDataLocalKeyValues?$filter=(MasterDataKeyValue%2FTypeId%20eq%20@typeIdd)%20and%20(Language%20eq%20'@lang')&$expand=MasterDataKeyValue&$select=MasterDataKeyValue%2FId%2CMasterDataKeyValue%2FParentId%2CMasterDataKeyValue%2FCode%2CMasterDataKeyValue%2FOrder%2CMasterDataKeyValue%2FName%2CMasterDataKeyValue%2FKey%2CMasterDataKeyValue%2FValue%2CMasterDataKeyValue%2FIsLeaf%2CMasterDataKeyValue%2FPathOrUrl%2CName",cms_languageAndCulture_public_getAll:"/cms/languageAndCulture/public/getAll",cms_entityGroup_get:"/odata/cms/EntityGroups?$filter=GroupId%20eq%20@groupIdd&$select=Id%2CLinkId%2CMasterDataKeyValueId%2CCommentId%2CEntityTypeId",cms_entityGroup_save:"/cms/entityGroup/save",cms_masterDataKeyValue_getTypeByOtherLanguages:"/odata/cms/MasterDataLocalKeyValues?$filter=(MasterDataKeyValue%2FIsType%20eq%20true)%20and%20(Language%20eq%20'@lang')&$expand=MasterDataKeyValue&$select=MasterDataKeyValue%2FId%2CMasterDataKeyValue%2FParentTypeId%2CMasterDataKeyValue%2FCode%2CMasterDataKeyValue%2FOrder%2CMasterDataKeyValue%2FName%2CMasterDataKeyValue%2FKey%2CMasterDataKeyValue%2FValue%2CMasterDataKeyValue%2FIsLeaf%2CMasterDataKeyValue%2FTypeId%2CName"}, $.asUrls); var
$frm =as("#frmGroups"),
$drpGroup=as("#drpGroup"),
$drpMasterDataType = as("#drpMasterDataType"),
$drpMasterData=as("#drpMasterData"),
$drpLink=as("#drpLink"),
$drpLanguge=as("#drpLanguge"),
$btnLinkToGroup=as("#btnLinkToGroup"),
$btnMasterDataToGroup=as("#btnMasterDataToGroup"),
masterdataList=[],
addedMasterDataList=[],
removedMasterDataList=[],
linkList=[],
addedLinkList=[],
removedLinkList=[],
selectedLang,
    validate,
    url= "";
    
    var validateRule = {
        drpGroup: {
        asType: 'asDropdown',
          required: true
        }

    }
       validate =$frm.asValidate({ rules: validateRule});
       
    $drpGroup.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'MasterDataKeyValue.Id' },
            parentDataField: { name: 'MasterDataKeyValue.ParentId' }
        }
          ,url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByOtherLanguagesAndTypeId, [{ name: '@typeId', value: 1015 }, { name: '@lang', value: $.asLang }])
        , displayDataField: 'Name'
          , valueDataField: 'MasterDataKeyValue.Id',
        orderby: 'MasterDataKeyValue.Order'
    }
     , parentMode: "uniq"

});




$drpMasterData.asDropdown("init"," Please Select Master Data Type",{
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
    , multiple: true
    , selectParents: true

});

$drpMasterDataType.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
             keyDataField: { name: 'MasterDataKeyValue.TypeId' },
            parentDataField: { name: 'MasterDataKeyValue.ParentTypeId' },
            removeChildLessParent: false
        },
        
        url:$.asInitService($.asUrls.cms_masterDataKeyValue_getTypeByOtherLanguages, [{ name: '@lang', value: $.asLang }])
                , displayDataField: 'Name'
          , valueDataField: 'MasterDataKeyValue.TypeId',
        orderby: 'MasterDataKeyValue.Order'
    }
    , selectParents: true

});

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
            , multiple: true
    , selectParents: true
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

var addToGroup=function(groupId,entityTypeId,removedList,addedList){
    
    
        
      $frm.asAjax({
            url: $.asUrls.cms_entityGroup_save,
            data: JSON.stringify({
                GroupId: groupId,
                EntityTypeId:entityTypeId,
                RemovedList:removedList,
                AddedList:addedList,
                Name:$drpGroup.asDropdown('selected').text
            }),
            success: function (result) {
                if(result === "masterData"){
                    masterdataList = removedMasterDataList = addedMasterDataList=[]
                if ($drpMasterData.asDropdown('selected')) {
                    $.each($drpMasterData.asDropdown('selected'), function (i, v) {
                        if (v.selected)
                            masterdataList.push(v.value)
                    })
                }
        
        }
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
            }
        }, { $form: $frm })
}
         var load = function (url) {
        return $.asAjaxTask({
            url: url
            });
        }
var getMasterData = function(masterdataTypeId,selectedMasterData){
     var url = $.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId
        var queryTypeIdTemp = "(TypeId%20eq%20@typeIdd)"
        var queryParentTypeIdTemp = "(ParentTypeId%20eq%20@typeIdd)"
        var query = []
        
     
            
        // $.each($drpMasterDataType.asDropdown('selected'), function (i, v) {
        //         if (v.selected){
                    query.push(queryTypeIdTemp.replace("@typeId",masterdataTypeId))
                    query.push(queryParentTypeIdTemp.replace("@typeId",masterdataTypeId))
            //     }
                    
            // })
                  as("#masterdata").asAfterTasks([
            load( url.replace("TypeId%20eq%20@typeIdd","(" + query.join("%20or%20") + ")"))
        ], function (masterdatas) {
            masterdataList=masterdatas
        
            addedMasterDataList = []
            removedMasterDataList=[]
           $drpMasterData.asDropdown("reload",{localData:masterdatas});
         if(typeof(selectedMasterData) != "undefined")
             $drpMasterData.asDropdown('selectValue', selectedMasterData)
        },{overlayClass:"as-overlay-fixed"})
   
        $drpMasterData.css({"margin-top":"0"})
       
}
var getGroups = function (groupId) {

   
    
    $drpLink.asDropdown('selectValue', [], true)
    $drpMasterData.asDropdown('selectValue', [], true)
             linkList =[]
                                        removedLinkList =[]
                                        addedLinkList=[]
                    
                              masterdataList =[]
                                        removedMasterDataList =[]
                                        addedMasterDataList=[]
  



    $frm.asAjax({
        url: $.asInitService($.asUrls.cms_entityGroup_get, [{ name: '@groupId', value: groupId }]),
        type: "get",
        success: function (groups) {
       
        var entityTypeId
            
             if($.isArray(groups)){
                  if(groups.length > 0){
                          $.each(groups, function (i, v) {
                                if(v.EntityTypeId == 101){
                        
                                    linkList.push(v.LinkId)
                                }else{
                                    entityTypeId = v.EntityTypeId
                             
                                    masterdataList.push(v.MasterDataKeyValueId)
                                }
                                
                                
                            })
                  }
                    
                  
                      
                  
             }
              if (linkList.length != 0)
                  $drpLink.asDropdown('selectValue', linkList)
               if (masterdataList.length != 0){
                    $drpMasterDataType.asDropdown('selectValue', entityTypeId)
                     getMasterData(entityTypeId,masterdataList)
                    
               }
                 
            
            

        }
    });
}
var bindEvent = function () {
    
            $(asPageEvent).on($.asEvent.page.ready, function (event) {
            
    });

  $drpLanguge.on("selectedIndexChanged", function (event, id, value, text, url, extera) {
        if (url)
            $drpLanguge.asFlexSelect('setItem', '<img src="' + url + '">&nbsp;<span class="caret"></span>')
            
                          as("#link").asAfterTasks([
            load($.asInitService($.asUrls.cms_link_getByLanguage, [{ name: '@lang', value: value }]))
        ], function (links) {
            linkList=links;
            addedLinkList=removedLinkList=[]
            $drpLink.asDropdown("reload",{localData:links});
        }, { overlayClass: 'as-overlay-absolute-no-height' });
                 
                 selectedLang = value
        })

 $drpGroup.on("change", function (event, item) {
        
          
                if (typeof (item.value) != "undefined") {
                    getGroups(item.value)
                  
                }
    });
    


    
        $drpMasterData.on("change", function (event, item) {
        
             if (typeof (item.value) != "undefined") {
                      
                        if( $.asFindInJsonArray(masterdataList, "Id", item.value) != null){
                            if(item.selected === false)
                                removedMasterDataList.push(item.value)
                                else{
                                  
                                     if ( removedMasterDataList.indexOf(item.value) > -1) {
                                        removedMasterDataList.splice(index, 1);
                                    }
                                }
                        }else{
                                   if(item.selected === true)
                                addedMasterDataList.push(item.value)
                                else{
                                   
                                     if (addedMasterDataList.indexOf(item.value) > -1) {
                                        addedMasterDataList.splice(index, 1);
                                    }
                                }
                        }
                       
                            // getMasterData(item.value)
                  
                }
    });

  

      $drpMasterDataType.on("change", function (event, item) {
             if (typeof (item.value) != "undefined") {
                  getMasterData(item.value)
             }
    });
    
      $drpLink.on("change", function (event, item) {
        
             if (typeof (item.value) != "undefined") {
   
                   
                        if(linkList.indexOf(item.value) > -1){
                            if(item.selected === false)
                                removedLinkList.push(item.value)
                                else{
                                   
                                     if (removedLinkList.indexOf(item.value) > -1) {
                                        removedLinkList.splice(index, 1);
                                    }
                                }
                        }else{
                                   if(item.selected === true)
                                addedLinkList.push(item.value)
                                else{
                                  
                                     if (addedLinkList.indexOf(item.value) > -1) {
                                        addedLinkList.splice(index, 1);
                                    }
                                }
                        }
                  
                }
    });
    
    $btnLinkToGroup.click(function () {
        if ($frm.asValidate('valid')) {
            //  linkId = []
            // if ($drpLink.asDropdown('selected')) {
            //     $.each($drpLink.asDropdown('selected'), function (i, v) {
            //         if (v.selected)
            //             linkId.push(v.value)
            //     })
                
                addToGroup($drpGroup.asDropdown('selected').value,"101",removedLinkList,addedLinkList)
            }
        }
    )
       $btnMasterDataToGroup.click(function () {
        if ($frm.asValidate('valid')) {
            //  ids = []
            // if ($drpMasterData.asDropdown('selected')) {
            //     $.each($drpMasterData.asDropdown('selected'), function (i, v) {
            //         if (v.selected)
            //             ids.push(v.value)
            //     })
                
                addToGroup($drpGroup.asDropdown('selected').value,$drpMasterDataType.asDropdown('selected').value,removedMasterDataList,addedMasterDataList)
            // }
        }
    })
        asOnPageDispose = function(){
        validate.destroy();
    }

    }
bindEvent();  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });