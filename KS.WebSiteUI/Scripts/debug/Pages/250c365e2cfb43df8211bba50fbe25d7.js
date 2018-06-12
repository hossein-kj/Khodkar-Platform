 $('#i250c365e2cfb43df8211bba50fbe25d7').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('مشاهده محتویات زیپ');var asPageEvent = '#i250c365e2cfb43df8211bba50fbe25d7'; var asPageId = '.i250c365e2cfb43df8211bba50fbe25d7.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({fms_zip_openByPaging:"/fms/openzip/@zipFullName/@orderBy/@skip/@take"}, $.asUrls); var 
     $win=$(asPageId),
     $btnCancel=as("#btnCancel"),
     $btnSelect=as("#btnSelect"),
     $grvZip=as("#grvZip"),
 
    requestedPath="",
     rootUrl;

    
 if(asPageParams){
                 requestedPath=asPageParams.zipFullName
               
           }
                $grvZip.asBootGrid({
    rowCount:[10,25,50,100],
    source:{
        url:''
    },
    requestHandler:function(request){
         if (requestedPath !== "") {
     
            rootUrl = requestedPath
            var zipUrl = rootUrl.replace(/\//g, $.asUrlDelimeter)

        
        
        var orderbyValue = "FileName desc"
        var skip = 0
        if(request.current > 1)
        skip=(request.current - 1) * request.rowCount
        $.each(request.sort, function(key, value) {
                 orderbyValue = key + " " + value
                });
        request.url = $.asInitService($.asUrls.fms_zip_openByPaging, [
            { name: '@zipFullName', value: zipUrl }
            ,{ name: '@orderby', value: orderbyValue }
            ,{ name: '@skip', value: skip }
             ,{ name: '@take', value: request.rowCount}])
             return request
         }
    },
    formatters: {
        UsesEncryption: function (column, row)
        {
            /* "this" is mapped to the grid instance */
            if(row.UsesEncryption)
            return "دارد" ;
            else
            return "ندارد";
        }
    },
        selection: false,
        rowSelect:false
});

 var bindEvent =function(){
         $(asPageEvent).on($.asEvent.modal.reopen, function (event,params) {
            if(params.zipFullName !== asPageParams.zipFullName){
                asPageParams=params;
                     requestedPath=asPageParams.zipFullName;
                 
                      $grvZip.asBootGrid("reload");
            }
          
        });
     $(asPageEvent).on($.asEvent.page.ready, function (event) {
          
            
        });
            

            
     asOnPageDispose = function(){
          $grvZip.asBootGrid("destroy");
        }
            
 }
 bindEvent()  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });