var
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
    bindEvent();