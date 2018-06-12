 $('#iaf26bbce21214cd6b3fd21eba4d0596a').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('Output Management');var asPageEvent = '#iaf26bbce21214cd6b3fd21eba4d0596a'; var asPageId = '.iaf26bbce21214cd6b3fd21eba4d0596a.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({develop_code_os_dotNet_publishDll:"/develop/code/os/dotnet/publishDll",develop_code_os_dotNet_dellOutputDll:"/develop/code/os/dotnet/dellOutputDll",develop_code_os_dotNet_getOutputs:"/develop/code/os/dotnet/GetetOutputs/@codeId/@orderBy/@skip/@take",develop_code_os_dotNet_addOutputDll:"/develop/code/os/dotnet/addOutputDll",develop_code_os_dotNet_viewDllBuildLog:"/develop/code/os/dotnet/ViewDllBuildLog/@name/@codeId"}, $.asUrls); var 
     
     $win=$(asPageId),
     $frmAddOrAupdate=as("#frmAddOrAupdate"),
     $txtPath=as("#txtPath"),
 
     $txtDescription=as("#txtDescription"),
     $btnCancel=as("#btnCancel"),
     $btnSelect=as("#btnSelect"),
     $btnPublish=as("#btnPublish"),
     $btnNew=as("#btnNew"),
    $btnDell=as("#btnDell"),
     $grvOutputs=as("#grvOutputs"),
      $winFileSelector=$.asModalManager.get({url:$.asModalManager.urls.fileSelector}),
      $winLog = as("#winLog"),
      $btnViewLog=as("#btnViewLog"),
      $txtLog= as("#txtLog"),
       selectedItems={
         items:{}
     },
    codeId="";
    $btnPublish.hide();
    $btnNew.hide();
    $winFileSelector.asModal({width:800}) ;
     $winLog.asModal({width:800});
   
 var calculateSelectedOutput = function(event, rows){

        if(event.type==="selected"){
            selectedItems.items[rows[0].Name]={Id:rows[0].Name}
        }else{
            delete selectedItems.items[rows[0].Name]
        }
    }
        if(asPageParams){
          codeId=asPageParams.codeId;
           
               $grvOutputs.asBootGrid({
    rowCount:[10,25,50,100],
    source:{
        url:''
    },
    requestHandler:function(request){
         if (codeId !== "") {
   
        var orderbyValue = "PathOrUrl desc"
        var skip = 0
        if(request.current > 1)
        skip=(request.current - 1) * request.rowCount
        $.each(request.sort, function(key, value) {
                 orderbyValue = key + " " + value
                });
        request.url = $.asInitService($.asUrls.develop_code_os_dotNet_getOutputs, [
            { name: '@codeId', value: codeId }
            ,{ name: '@take', value: request.rowCount }
            ,{ name: '@skip', value: skip }
            ,{ name: '@orderby', value: orderbyValue }]);
            selectedItems.items={};
             return request
         }
    },
        selection: true,
        rowSelect:true
}).on("selected.rs.jquery.asBootGrid", function(e, rows)
{
    calculateSelectedOutput(e,rows)
    
}).on("deselected.rs.jquery.asBootGrid", function(e, rows)
{
    calculateSelectedOutput(e,rows)
});
          }
           
       


      

var getSelectedOutputs = function(){
                
           var sources=[]
              $.each(selectedItems.items,function(i,v){
                sources.push(v.Id)
            });
            return sources;
}
                  var onSuccess = function(){
           $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
              selectedItems.items={};
              $grvOutputs.asBootGrid("deselect");
     }
 var bindEvent =function(){
     var setButtonState = function(){
          if(asPageParams.showPublishButton)
                    $btnPublish.show();
                    else
                    $btnPublish.hide();
                    
            if(asPageParams.showAddOutputButton)
                    $btnNew.show();
                    else
                    $btnNew.hide();
     }
       $(asPageEvent).on($.asEvent.modal.reopen, function (event,params) {
            if(params.codeId !== asPageParams.codeId || params.codePath !== asPageParams.codePath
            || params.showPublishButton !== asPageParams.showPublishButton
            || params.showAddOutputButton !== asPageParams.showAddOutputButton){
                asPageParams=params;
                     codeId=asPageParams.codeId;
                   setButtonState();
                      $grvOutputs.asBootGrid("reload");
            }
          
        });
        
     $(asPageEvent).on($.asEvent.page.ready, function (event) {
    
          setButtonState();
        });
         $(asPageEvent).on("fileSelected",function(event,selectedFile,selectedId,selectedFileName){
            $win.asAjax({
            url: $.asUrls.develop_code_os_dotNet_addOutputDll,
            data: JSON.stringify({
                Id:codeId,
                Path:selectedFile,
                Name: selectedFileName
               
            }),
            success: function (result) {
               $grvOutputs.asBootGrid("append",[result]);
             onSuccess();
            }
        }, { validate:false ,overlayClass: "as-overlay-absolute"});
        
         });
         
         $btnViewLog.click(function () {
              var outputs = getSelectedOutputs();
       if (outputs.length === 1)  {
           $txtLog.val("");
              $winLog.asModal("show");
              
              
               $winLog.asAjax({
            url: $.asInitService($.asUrls.develop_code_os_dotNet_viewDllBuildLog,[{name:"@codeId",value:codeId},{name:"@name",value:outputs[0]}]),
            type:"get",
            success: function (result) {
            $txtLog.val(result);
                
              onSuccess();
            }
        },{overlayClass: "as-overlay-absolute"});
              
              
       }else
           $.asShowMessage({template:"error", message: "  You must select an output to view the build report"});
         });
     as("#btnCancel").click(function () {
            
       $winLog.asModal('hide');
        });
    $btnPublish.click(function () {
            
    var outputs = getSelectedOutputs();
       if (outputs.length === 1)  {
            $win.asAjax({
            url: $.asUrls.develop_code_os_dotNet_publishDll,
            data: JSON.stringify({
                Id:codeId,
                Name: outputs[0]
               
            }),
            success: function (result) {
             onSuccess();
            }
            }, { validate:false ,overlayClass: "as-overlay-absolute"});
        
          }else
           $.asShowMessage({template:"error", message: "To publish only one output must be selected"});
           
        });
    $btnNew.click(function () {
         $winFileSelector.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.fileSelector)},{name:"@isModal",value:true}])
            ,{path:asPageParams.codePath,parent:asPageEvent,event:"fileSelected"});

           
        });

        
            $btnDell.click(function () {
        if (codeId !== "") {
          var outputs = getSelectedOutputs();
            if(outputs.length >= 1){
            $win.asAjax({
            url: $.asUrls.develop_code_os_dotNet_dellOutputDll,
            data: JSON.stringify({
                Id:codeId,
                SelectedOutputs: getSelectedOutputs()
               
            }),
            success: function (result) {
                $grvOutputs.asBootGrid("remove");
             onSuccess();
            }
        }, { validate:false ,overlayClass: "as-overlay-absolute"});
            }  else
            $.asShowMessage({template:"error", message: "At least one output must be selected"});
        }else
            $.asShowMessage({template:"error", message: " The code id is unclear"});
    });
     asOnPageDispose = function(){
          $grvOutputs.asBootGrid("destroy");
        }
            
 }
 bindEvent()  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });