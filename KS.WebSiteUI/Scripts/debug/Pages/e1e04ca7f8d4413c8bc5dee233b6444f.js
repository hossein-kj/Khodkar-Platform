 $('#ie1e04ca7f8d4413c8bc5dee233b6444f').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('Run a Unit Test');var asPageEvent = '#ie1e04ca7f8d4413c8bc5dee233b6444f'; var asPageId = '.ie1e04ca7f8d4413c8bc5dee233b6444f.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({develop_code_os_dotNet_getOutputVersionNumbers:"/develop/code/os/dotnet/GetOutputVersionNumbers/@codeId",develop_code_os_dotNet_getDllByTypeId:"/odata/cms/MasterDataKeyValues?$filter=((ForeignKey1%20eq%20null)%20or%20(ForeignKey1%20eq%20@typeIdd))%20and%20(TypeId%20eq%201041d)&$select=Id%2CParentId%2CPathOrUrl%2CName%2CCode%2COrder",develop_code_os_dotNet_getNamespaceAndClassesAndMethodsByDllId:"/odata/cms/MasterDataKeyValues?$filter=((((ForeignKey1%20eq%20752d)%20or%20(ForeignKey1%20eq%20753d))%20or%20(ForeignKey1%20eq%20754d))%20and%20(TypeId%20eq%201041d))%20and%20(ForeignKey2%20eq%20@dllIdd)&$select=Id%2CParentId%2CPathOrUrl%2CName%2CCode%2COrder",develop_code_os_dotNet_runUnitTestMethod:"/develop/code/os/dotnet/RunUnitTestMethod",develop_code_os_dotNet_getUnitTestMethods:"/develop/code/os/dotnet/GetUnitTestMethods/@dllId/@dllVersion/@codeId"}, $.asUrls); var
     $win=$(asPageId),
    $frm=as("#frmUnitTest"),
    $txtParameterValue=as("#txtParameterValue"),
    $btnExecute= as("#btnExecute"),
    $drpUnitTestDll=as("#drpUnitTestDll"),
    $drpVersion=as("#drpDllVersion"),
    $drpDllCodes=as("#drpDllCodes"),
    $drpMethods=as("#drpMethods"),
    methodName="",
    className="",
    selectedDll=0,
    selectedVersion=0,
    selectedCod=0,
    validateRunTest;


$drpUnitTestDll.asDropdown({
        source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: false
        },
            url:  $.asInitService( $.asUrls.develop_code_os_dotNet_getDllByTypeId, [{ name: '@typeId', value: 979 }])
        , displayDataField: 'Name'
          , valueDataField: 'Id',
        orderby: 'Order'
    }
    , multiple: false
    , selectParents: false
//  , parentMode: "uniq"

});



        var validateRuleRunTest = {
         drpDllCodes:{
           asType: 'asDropdown',
            required: true
        },
        drpUnitTestDll:{
            asType: 'asDropdown',
            required: true
        },
        drpDllVersion:{
            asType: 'asDropdown',
            required: true
        }
    };
     validateRunTest =$frm.asValidate({ rules: validateRuleRunTest});

var setUpWin = function(){
    


$drpDllCodes.asDropdown("init","First, select an assembly",{
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

$drpMethods.asDropdown("init"," First, select an code",{
    source: {
         displayDataField: 'Name'
          , valueDataField: 'Name',
        orderby: 'Name'
    }
});

    $drpVersion.asDropdown("init","First, select an assembly",{
    source: {
         displayDataField: 'Value'
          , valueDataField: 'Id',
        orderby: 'Id'
    }
});

    $txtParameterValue.val("");

}

    
    var bindEvent = function(){
 
         $(asPageEvent).on($.asEvent.modal.reopen, function (event,params) {
                asPageParams=params;
                 setUpWin();
            });
            
    var loadData = function (url) {
        return $.asAjaxTask({
            url: url
        });
    }
      var loadDllCods = function(){
              if(selectedDll > 0){
                    as("#divDllCodes").asAfterTasks([
                       loadData($.asInitService($.asUrls.develop_code_os_dotNet_getNamespaceAndClassesAndMethodsByDllId,
                       [
                           { name: '@dllId', value: selectedDll }
                        ]))
                    ], function (dllCodes) {
                        $drpDllCodes.asDropdown("reload",{localData: dllCodes});
                    },{overlayClass: 'as-overlay-absolute-no-height'});
                }
            }
            
     var loadVersions = function(){
              if(selectedDll > 0){
                    as("#divDllVersions").asAfterTasks([
                       loadData($.asInitService($.asUrls.develop_code_os_dotNet_getOutputVersionNumbers,
                       [
                           { name: '@codeId', value: selectedDll }
                        ]))
                    ], function (versions) {
                        $drpVersion.asDropdown("reload",{localData: versions});
                    },{overlayClass: 'as-overlay-absolute-no-height'});
                }
            }
            
             var loadMethodNames = function(){
              if(selectedCod > 0){
                    as("#divMethods").asAfterTasks([
                       loadData($.asInitService($.asUrls.develop_code_os_dotNet_getUnitTestMethods,
                       [
                           { name: '@dllId', value: selectedDll }
                           ,{ name: '@dllVersion', value: selectedVersion }
                           ,{ name: '@codeId', value: selectedCod }
                        ]))
                    ], function (methodNames) {
                        var methods = [];
                     
                        $.each(methodNames,function(key,value){
                            methods.push({Name:value})
                        });
                        $drpMethods.asDropdown("reload",{localData: methods});
                    },{overlayClass: 'as-overlay-absolute-no-height'});
                }
            }
            
        
        
        $drpVersion.on("change", function (event, item) {
            selectedVersion=item.value;
        });
        
         $drpMethods.on("change", function (event, item) {
             if(item.value.indexOf("=>") > 0){
                  var methodClass = item.value.split("=>");
                  className=methodClass[0];
                  methodName=methodClass[1];
             }else{
                  className="";
                  methodName=item.value;
             }
           
        });
        
         $drpDllCodes.on("change", function (event, item) {
            if(item){
                selectedCod=item.value; 
                
            }
        });
        
         $drpVersion.on("change", function (event, item) {
            if(item){
                loadMethodNames();
            }
        });
            
      $drpUnitTestDll.on("change", function (event, item) {
           if(item){
             selectedDll=item.value;
             loadDllCods();
             loadVersions();
           }
      });
      
        $btnExecute.on('click',function(){
                $win.asAjax({
                        url:$.asUrls.develop_code_os_dotNet_runUnitTestMethod,
                        data: JSON.stringify({
                            CodeId: $drpDllCodes.asDropdown('selected').value,
                            DllVersion : selectedVersion,
                            Parameter : $txtParameterValue.val(),
                            MethodeName:methodName,
                            ClassName:className
                        }),
                        success: function (result) {
                          $.asShowMessage({ message: result});
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