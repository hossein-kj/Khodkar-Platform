 $('#i7cc477d5bdce4398bf0958ededada970').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('Data Translator');var asPageEvent = '#i7cc477d5bdce4398bf0958ededada970'; var asPageId = '.i7cc477d5bdce4398bf0958ededada970.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({cms_masterDataLocalKeyValue_get:"/odata/cms/MasterDataLocalKeyValues?$filter=(MasterDataKeyValueId%20eq%20@idd)%20and%20(Language%20eq%20'@lang')&$select=Id%2CName%2CDescription%2CRowVersion%2CStatus",cms_masterDataLocalKeyValue_save:"/cms/masterdatalocalkeyvalue/save",cms_languageAndCulture_public_getAll:"/cms/languageAndCulture/public/getAll",cms_localFilePath_save:"/cms/localfilepath/Save",cms_localFilePath_get:"/odata/cms/LocalFilePaths?$filter=(FilePathId%20eq%20@idd)%20and%20(Language%20eq%20'@lang')&$select=Id%2CName%2CDescription%2CRowVersion%2CStatus",cms_localFile_get:"/odata/cms/LocalFiles?$filter=(FileId%20eq%20@idd)%20and%20(Language%20eq%20'@lang')&$select=Id%2CName%2CDescription%2CRowVersion%2CStatus",cms_localFile_save:"/cms/localfile/Save",security_LocalRole_save:"/security/localrole/save",security_LocalRole_get:"/odata/security/LocalRoles?$filter=(RoleId%20eq%20@idd)%20and%20(Language%20eq%20'@lang')&$select=Id%2CName%2CDescription%2CRowVersion%2CStatus",security_LocalGroup_get:"/odata/security/LocalGroups?$filter=(GroupId%20eq%20@idd)%20and%20(Language%20eq%20'@lang')&$select=Id%2CName%2CDescription%2CRowVersion%2CStatus",security_LocalGroup_save:"/security/localgroup/save"}, $.asUrls); var 
    $drpLanguge= as("#drpLanguge"),
    $txtName=as("#txtName"),
    $txtDescription =as("#txtDescription"),
    $btnTranslate =as("#btnTranslate"),
    $btnCancel =as("#btnCancel"),
    $txtDescription=as("#txtDescription"),
    $txtLang=as("#txtLang"),
    $txtCulture=as("#txtCulture")
    $frm=as("#frmTranslator"),
    // $win=as("#win"),
    $win=$(asPageId),
    $chkStatus = as("#chkStatusTrans"),
    translateId=0,
    rowVersion= "";

    var loadLanguages = function () {
    return $.asAjaxTask({
        url: $.asUrls.cms_languageAndCulture_public_getAll
    });
}
var setLangAndCulture = function(lang,culture){
                        $txtLang.val(lang)
                        $txtCulture.val(culture)
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
        langParams.selectedValue = lang.country
        setLangAndCulture(lang.lang,lang.country)
    }

    $drpLanguge.asFlexSelect(langParams);
  
},{overlayClass:"as-overlay-fixed"})

var notFound = function(){
 $.asNotFound("Translation");
 translateId=0;
}
    var setTranslate = function(language){
          $txtName.val(language.Name)
        $txtDescription.val(language.Description)
        $chkStatus.prop('checked', language.Status)
        translateId = language.Id
        rowVersion=language.RowVersion
    }
    var setTextBox = function(){
        
            $txtName.val(asPageParams.name)
              $txtDescription.val(asPageParams.description)
    }
    var bindEvent =function(){
               $(asPageEvent).on($.asEvent.modal.reopen, function (event,params) {
                 
            if(params.type !== asPageParams.type || params.id !== asPageParams.id){
                asPageParams=params;
                setTextBox();
            }
        });
      $(asPageEvent).on($.asEvent.page.ready, function (event) {
    if(asPageParams){
       setTextBox()
        }
    
    });
    $btnTranslate.click(function(){
        var url=""
            if(asPageParams){
             if(asPageParams.type === "masterDataKeyValue"){
                  url= $.asUrls.cms_masterDataLocalKeyValue_save;
             }else if(asPageParams.type === "filePath"){
                  url= $.asUrls.cms_localFilePath_save;
             }else if(asPageParams.type === "file"){
                  url= $.asUrls.cms_localFile_save;
             }else if(asPageParams.type === "role"){
                  url= $.asUrls.security_LocalRole_save;
             }else if(asPageParams.type === "group"){
                  url= $.asUrls.security_LocalGroup_save;
             }
                       $frm.asAjax({
            url: url,
            data: JSON.stringify({
                Id: translateId,
                Language:$txtLang.val(),
                ItemId:asPageParams.id,
                Name: $txtName.val(),
                Description:$txtDescription.val(),
                Status: $chkStatus.is(':checked'),
                RowVersion: rowVersion
            }),
            success: function (translate) {
                translateId=translate.Id
                rowVersion=translate.RowVersion
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
            }
        }, { $form: $frm,overlayClass: 'as-overlay-absolute'  });
            }else{
                       $.asShowMessage({template:"error", message: "Pleas Open For Again"});
            }

        
    });
    
       $drpLanguge.on("selectedIndexChanged", function (event, id, value, text, url, extera) {
        if (url)
            $drpLanguge.asFlexSelect('setItem', '<img src="' + url + '">&nbsp;<span class="caret"></span>')
             setLangAndCulture(value,text)
                    var loadUrl=""
            if(asPageParams){
             if(asPageParams.type === "masterDataKeyValue"){
                  loadUrl= $.asUrls.cms_masterDataLocalKeyValue_get;
             }else if(asPageParams.type === "filePath"){
                  loadUrl= $.asUrls.cms_localFilePath_get;
             }else if(asPageParams.type === "file"){
                  loadUrl= $.asUrls.cms_localFile_get;
             }else if(asPageParams.type === "role"){
                  loadUrl=$.asUrls.security_LocalRole_get;
             }else if(asPageParams.type === "group"){
                  loadUrl= $.asUrls.security_LocalGroup_get;
             }
          
                  $win.asAjax({
                url:$.asInitService(loadUrl, [{ name: '@id', value: asPageParams.id },{ name: '@lang', value: value }]) ,
                type: "get",
                success: function (language) {
                    
                             if($.isArray(language)){
                  if(language.length > 0){
                      setTranslate(language[0])
                  }
               
                    else
                     notFound()
            }else
            notFound()
                }
            }, {overlayClass: 'as-overlay-absolute' });
            }else{
                       $.asShowMessage({template:"error", message: "Pleas Open For Again"});
            }
        
    
    });
              $btnCancel.on('click', function () {
                $win.asModal('hide');
            })

    }
        bindEvent()  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });