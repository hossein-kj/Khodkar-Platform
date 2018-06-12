 $('#iafebde185a7440a2af6e7d118dcd42e7').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('Remote download manager');var asPageEvent = '#iafebde185a7440a2af6e7d118dcd42e7'; var asPageId = '.iafebde185a7440a2af6e7d118dcd42e7.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({cms_masterDataKeyValue_GetByOtherLanguagesAndTypeId:"/odata/cms/MasterDataLocalKeyValues?$filter=(MasterDataKeyValue%2FTypeId%20eq%20@typeIdd)%20and%20(Language%20eq%20'@lang')&$expand=MasterDataKeyValue&$select=MasterDataKeyValue%2FId%2CMasterDataKeyValue%2FParentId%2CMasterDataKeyValue%2FCode%2CMasterDataKeyValue%2FOrder%2CMasterDataKeyValue%2FName%2CMasterDataKeyValue%2FKey%2CMasterDataKeyValue%2FValue%2CMasterDataKeyValue%2FIsLeaf%2CMasterDataKeyValue%2FPathOrUrl%2CName",fms_downlodFromUrl:"/fms/DownlodFromUrl",cms_masterDataKeyValue_GetByOtherLanguageAndParentId:"/odata/cms/MasterDataLocalKeyValues?$filter=(MasterDataKeyValue%2FParentId%20eq%20@parentIdd)%20and%20(Language%20eq%20'@lang')&$expand=MasterDataKeyValue&$select=MasterDataKeyValue%2FId%2CMasterDataKeyValue%2FParentId%2CMasterDataKeyValue%2FCode%2CMasterDataKeyValue%2FOrder%2CName"}, $.asUrls); var
     $win=$(asPageId),
    $frm=as("#frmRemoteDownloadManager"),
    $txtUrl=as("#txtUrl"),
    $btnExecute= as("#btnExecute"),
    $drpBaseUrl=as("#drpBaseUrl"),
    $txtFileName=as("#txtFileName"),
    selectedDll=0,
    selectedVersion=0,
    selecteBaseUrlId=0,
    validateRunTest;


$drpBaseUrl.asDropdown({
        source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'MasterDataKeyValue.Id' },
            parentDataField: { name: 'MasterDataKeyValue.ParentId' },
            removeChildLessParent: false
        },
            url:asPageParams.urlTypeId ?
            $.asInitService($.asUrls.cms_masterDataKeyValue_GetByOtherLanguagesAndTypeId , [{ name: '@typeId', value: asPageParams.urlTypeId },{ name: '@lang', value: $.asLang }]) :
            $.asInitService( $.asUrls.cms_masterDataKeyValue_GetByOtherLanguageAndParentId, [{ name: '@parentId', value: asPageParams.urlParentId },{ name: '@lang', value: $.asLang }]) 
        , displayDataField: 'Name'
          , valueDataField: 'MasterDataKeyValue.Id',
        orderby: 'MasterDataKeyValue.Order'
    }
    , multiple: false
    , selectParents: false
//  , parentMode: "uniq"

});



        var validateRule = {
         drpBaseUrl:{
           asType: 'asDropdown',
            required: true
        },
        txtUrl:{
            required: true
        },
        txtFileName:{
            required: true
        }
    };
     validateRunTest =$frm.asValidate({ rules: validateRule});

var setUpWin = function(){
    
    $txtFileName.val("");
    $txtUrl.val("");

}

    
    var bindEvent = function(){
 
         $(asPageEvent).on($.asEvent.modal.reopen, function (event,params) {
               if(params.urlParentId !== asPageParams.urlParentId || params.path !== asPageParams.path  || params.urlTypeId !== asPageParams.urlTypeId){
                   asPageParams=params;
                    setUpWin();
               }
                
            });
            

            

            
        
        


        
         $drpBaseUrl.on("change", function (event, item) {
            if(item){
                selecteBaseUrlId=item.value; 
            
                if(item.text.toLowerCase().indexOf("nuget") > -1){
                    $txtUrl.val("/Microsoft.Web.Infrastructure/1.0.0.0")
                }
            }
        });
        

      
        $btnExecute.on('click',function(){
                $win.asAjax({
                        url:$.asUrls.fms_downlodFromUrl,
                        data: JSON.stringify({
                            FileName:$txtFileName.val(),
                            FilePath : asPageParams.path,
                            Url : $txtUrl.val(),
                            BaseUrlId:selecteBaseUrlId
                        }),
                        success: function (result) {
                          $.asShowMessage({ message: $.asRes[$.asLang].successOpration ,showTime:10000000 });
                        }
                         },{$form: $frm,validate:true,overlayClass: "as-overlay-absolute"});
            
        });
        //  $btnCancel.on('click', function () {
        //         $win.asModal('hide');
        //     });
            // asOnPageDispose = function(){
            //       validateRunTest.destroy();
            // }
    
          $(asPageEvent).on($.asEvent.page.ready, function (event) {
             setUpWin();
    });

    }
    bindEvent();  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });