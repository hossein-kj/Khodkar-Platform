 $('#i0965065f233845d39f1bccfda8a6ac41').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('تاریخچه خطاها');var asPageEvent = '#i0965065f233845d39f1bccfda8a6ac41'; var asPageId = '.i0965065f233845d39f1bccfda8a6ac41.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({develop_reports_ErrorLog_GetByPagination:"/develop/reports/ErrorLog/GetByPagination/@orderBy/@skip/@take/@typeOrSourceOrMessage/@user/@fromDateTime/@toDateTime",develop_reports_ErrorLog_GetById:"/develop/reports/ErrorLog/GetById/@id",develop_reports_ErrorLog_Delete:"/develop/reports/ErrorLog/delete"}, $.asUrls);     var 
   $btnDell=as("#btnDell"),
   $btnDetail=as("#btnDetail"),
   $winDetail=as("#winDetail"),
   $grvLog =as("#grvLog"),
         selectedItems={
         items:{}
     },
     $divException=as("#divException"),
     $divAppDomain=as("#divAppDomain"),
     $divHostName=as("#divHostName"),
     $divDataTime=as("#divDataTime"),
     $divWebHostHtmlMessage=as("#divWebHostHtmlMessage"),
     $divQueryString=as("#divQueryString"),
     $divType=as("#divType"),
     $divMessage=as("#divMessage"),
     $divDetail=as("#divDetail"),
     $divServerVariables=as("#divServerVariables"),
     $divForm=as("#divForm"),
     $divCookies=as("#divCookies"),
     $divIsMobileMode=as("#divIsMobileMode"),
     $divIsDebugMode=as("#divIsDebugMode"),
     $fromDateInput=as("#fromDateInput"),
     $toDateInput=as("#toDateInput"),
     $fromTimeInput=as("#fromTimeInput"),
     $toTimeInput=as("#toTimeInput"),
     $txtUser=as("#txtUser"),
     $txtTypeOrSourceOrMessage=as("#txtTypeOrSourceOrMessage"),
     $btnSearch=as("#btnSearch"),
     toDateTime="!",
     fromDateTime="!";
     
         $fromDateInput.asDateTimeInput({ type: 'calendar', calendar: { params: { height: '30px', width: '190px' } }, theme: 'public' });
    $toDateInput.asDateTimeInput({ type: 'calendar', calendar: { params: { height: '30px', width: '190px' } }, theme: 'public' });
    
     $fromTimeInput.asDateTimeInput({ type: 'time', theme: 'public' });
    $toTimeInput.asDateTimeInput({ type: 'time', theme: 'public' });
    
     $winDetail.asModal({width:800});
     
        var calculateSelectedLog = function(event, rows){
        if(event.type==="selected"){
            selectedItems.items[rows[0].Id]={Id:rows[0].Id,Type:rows[0].Type,
            Source:rows[0].Source,Message:rows[0].Message,
            User:rows[0].User,LocalDateTime:rows[0].LocalDateTime,
            StatusCode:rows[0].StatusCode}
        }else{
            delete selectedItems.items[rows[0].Id]
        }
    }

    $grvLog.asBootGrid({
     
    rowCount:[10,25,50,100,-1],
    source:{
        url:''
    },
    requestHandler:function(request){
      
        var orderbyValue = "Id desc"
        var skip = 0
        if(request.current > 1)
        skip=(request.current - 1) * request.rowCount
        $.each(request.sort, function(key, value) {
                 orderbyValue = key + " " + value
                });

        request.url = $.asInitService($.asUrls.develop_reports_ErrorLog_GetByPagination, [
            { name: '@orderby', value: orderbyValue }
            ,{ name: '@skip', value: skip }
             ,{ name: '@take', value: request.rowCount}
             ,{ name: '@typeOrSourceOrMessage', value: $txtTypeOrSourceOrMessage.val() === "" ? "!":$txtTypeOrSourceOrMessage.val()}
             ,{ name: '@user', value: $txtUser.val() === "" ? "!":$txtUser.val()}
             ,{ name: '@fromDateTime', value: fromDateTime === "!" ? "!":fromDateTime.replace(":","_")}
             ,{ name: '@toDateTime', value: toDateTime === "!" ? "!" :toDateTime.replace(":","_")}]);
             
              selectedItems.items={};
              $grvLog.asBootGrid("deselect");
             return request

    },
        selection: true,
        rowSelect:true,
        multiSelect:true
}).on("selected.rs.jquery.asBootGrid", function(e, rows)
{
    calculateSelectedLog(e,rows)
    
}).on("deselected.rs.jquery.asBootGrid", function(e, rows)
{
    calculateSelectedLog(e,rows)
});
            

var notFound = function(){
 $.asNotFound(" خطا")
}

        var bindEvent = function () {
            
       $(asPageEvent).on($.asEvent.page.ready, function (event) {

            });
        
             var clearModal = function(){
                    $divAppDomain.empty();
                    $divHostName.empty();
                    $divDataTime.empty();
                    $divCookies.empty();
                    $divForm.empty();
                     $divServerVariables.empty();
                      $divDetail.empty();
                       $divMessage.empty();
                        $divQueryString.empty();
                        $divType.empty();
                         $divWebHostHtmlMessage.empty();
                         $divException.empty();
                         $divIsDebugMode.empty();
                         $divIsMobileMode.empty();
               
             }
                  var onSuccess = function(){
           $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
              selectedItems.items={};
              $grvLog.asBootGrid("deselect");
     }

    as("#btnRemoveFilte").click(function () {
     
          $txtTypeOrSourceOrMessage.val("");
          $txtUser.val("");
          toDateTime="!";
          $toTimeInput.asDateTimeInput("setTime","");
          fromDateTime="!";
           $fromTimeInput.asDateTimeInput("setTime","");
           toDateInput="!";
           $toDateInput.asDateTimeInput("setDate","");
           fromTimeInput="!"
           $fromDateInput.asDateTimeInput("setDate","");
    });
    $btnSearch.click(function () {
      
        if($toDateInput.asDateTimeInput('getDate').length > 0){
            toDateTime=$toDateInput.asDateTimeInput('getDate');
            if($toTimeInput.asDateTimeInput('getTime') !== "")
             toDateTime += " " + $toTimeInput.asDateTimeInput('getTime') + "_00";
            else
                  toDateTime += " " + $toTimeInput.asDateTimeInput('getTime') + "23_59_59";
        }
        
    
        if($fromDateInput.asDateTimeInput('getDate').length > 0){
            fromDateTime=$fromDateInput.asDateTimeInput('getDate');
            if($fromTimeInput.asDateTimeInput('getTime') !== "")
             fromDateTime += " " + $fromTimeInput.asDateTimeInput('getTime') + "_00";
            else
                fromDateTime += " " + $fromTimeInput.asDateTimeInput('getTime') + "00_00_00";
        }

        $grvLog.asBootGrid("reload");
    });
       $btnDetail.click(function () {
           
            var logs=[]
              $.each(selectedItems.items,function(i,v){
                logs.push(v)
            });
            
          if (logs.length === 1) {
         clearModal();
        $winDetail.asModal("show");
            
         $winDetail.asAjax({
            url: $.asInitService($.asUrls.develop_reports_ErrorLog_GetById,[{name:"@id",value:logs[0].Id}]),
            type:"get",
            success: function (exception) {
            
                var error = JSON.parse(exception.Error);
            
                    $divAppDomain.html(error.application);
                    $divHostName.html(error.host);
                    $divDataTime.html(error.time);
                    var coockies="";
                    $.each(error.cookies,function(i,v){
                        coockies += i + " : " + v + " <br>";
                    });
                    $divCookies.html(coockies);
                     var forms = "";
                    $.each(error.form,function(i,v){
                        forms += i + " : " + v + " <br>";
                    });
                    $divForm.html(forms);
                     var serverVariables = "";
                    $.each(error.serverVariables,function(i,v){
                        serverVariables += i + " : " + v + " <br>";
                    });
                     $divServerVariables.html(serverVariables);
                      $divDetail.html(error.detail);
                       $divMessage.html(error.message);
                        $divQueryString.html(error.queryString);
                        $divType.html(error.type);
                         $divWebHostHtmlMessage.html(error.webHostHtmlMessage);
                            $divIsDebugMode.html(exception.IsDebugMode ? "بله":"خیر");
                         $divIsMobileMode.html(exception.IsMobileMode ? "بله":"خیر");
                         $divException.html(JSON.stringify(error.exception));
             
            //   $grvLog.asBootGrid("remove");
              onSuccess();
            }
        },{overlayClass: "as-overlay-absolute"});
          }else{
                $.asShowMessage({template:"error", message: "  برای مشاهده یک لاگ انتخاب شود"});
          }
       });
   
   $btnDell.click(function () {
                var logs=[]
              $.each(selectedItems.items,function(i,v){
                logs.push(v.Id)
            });
            
          if (logs.length >= 1) {
               $grvLog.asAjax({
            url: $.asUrls.develop_reports_ErrorLog_Delete,
            data: JSON.stringify({
                Ids: logs
               
            }),
            error:function(){
                  $btnDell.button('reset')
            },
            success: function (result) {
                 $btnDell.button('reset')
                $grvLog.asBootGrid("remove");
             onSuccess();
            }
        }, { validate:false });
         $btnDell.button('loading') ;
          }else{
                $.asShowMessage({template:"error", message: "    برای حذف حداقل یک لاگ باید انتخاب شود  "});
          }
    });
            
        as("#btnCancel").click(function () {
            
       $winDetail.asModal('hide');
        });

    
    asOnPageDispose = function(){
          $grvLog.asBootGrid("destroy");
        }
}
bindEvent();  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });