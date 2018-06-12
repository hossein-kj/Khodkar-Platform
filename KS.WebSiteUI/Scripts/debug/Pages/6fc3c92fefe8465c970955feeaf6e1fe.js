 $('#i6fc3c92fefe8465c970955feeaf6e1fe').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('مدیریت دانلودها از آدرس راه دور');var asPageEvent = '#i6fc3c92fefe8465c970955feeaf6e1fe'; var asPageId = '.i6fc3c92fefe8465c970955feeaf6e1fe.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId:"/odata/cms/MasterDataKeyValues?$filter=TypeId%20eq%20@typeIdd&$select=Id%2CParentId%2CCode%2CPathOrUrl%2COrder%2CName%2CIsLeaf%2CKey%2CValue",cms_masterDataKeyValue_GetByDefaultsLanguageAndParentId:"/odata/cms/MasterDataKeyValues?$filter=ParentId%20eq%20@parentIdd&$select=Id%2CParentId%2CCode%2COrder%2CName",fms_downlodFromUrl:"/fms/DownlodFromUrl"}, $.asUrls); var
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
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: false
        },
            url:asPageParams.urlTypeId ?
            $.asInitService($.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId , [{ name: '@typeId', value: asPageParams.urlTypeId }]) :
            $.asInitService( $.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndParentId, [{ name: '@parentId', value: asPageParams.urlParentId }]) 
        , displayDataField: 'Name'
          , valueDataField: 'Id',
        orderby: 'Order'
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