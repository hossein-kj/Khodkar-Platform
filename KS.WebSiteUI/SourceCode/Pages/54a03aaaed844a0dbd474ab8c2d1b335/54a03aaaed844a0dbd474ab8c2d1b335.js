var
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
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' }
        }
          ,url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId, [{ name: '@typeId', value: 1015 }])
        , displayDataField: 'Name'
          , valueDataField: 'Id',
        orderby: 'Order'
    }
     , parentMode: "uniq"

});




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
    , multiple: true
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

});

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
bindEvent();