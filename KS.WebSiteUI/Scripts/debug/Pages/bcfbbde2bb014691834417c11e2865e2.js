 $('#ibcfbbde2bb014691834417c11e2865e2').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('تاریخچه رویدادها');var asPageEvent = '#ibcfbbde2bb014691834417c11e2865e2'; var asPageId = '.ibcfbbde2bb014691834417c11e2865e2.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId:"/odata/cms/MasterDataKeyValues?$filter=TypeId%20eq%20@typeIdd&$select=Id%2CParentId%2CCode%2CPathOrUrl%2COrder%2CName%2CIsLeaf%2CKey%2CValue",develop_reports_ActionLog_GetByPagination:"/develop/reports/ActionLog/GetByPagination/@orderBy/@skip/@take/@serviceUrl/@nameOrUrlOrUser/@fromDateTime/@toDateTime",develop_reports_ActionLog_GetById:"/develop/reports/ActionLog/GetById/@id",develop_reports_ActionLog_Delete:"/develop/reports/ActionLog/delete"}, $.asUrls);     var 
   $btnDell=as("#btnDell"),
   $btnDetail=as("#btnDetail"),
   $winDetail=as("#winDetail"),
   $grvLog =as("#grvLog"),
         selectedItems={
         items:{}
     },
     $divIsSuccessed=as("#divIsSuccessed"),
     $divName=as("#divName"),
     $divDataTime=as("#divDataTime"),
     $divParameters=as("#divParameters"),
     $divType=as("#divType"),
     $divUrlReferrer=as("#divUrlReferrer"),
     $divRequest=as("#divRequest"),
     $divCookies=as("#divCookies"),
     $divIsMobileMode=as("#divIsMobileMode"),
     $divIp=as("#divIp"),
     $divIsDebugMode=as("#divIsDebugMode"),
     $fromDateInput=as("#fromDateInput"),
     $toDateInput=as("#toDateInput"),
     $fromTimeInput=as("#fromTimeInput"),
     $toTimeInput=as("#toTimeInput"),
     $drpService=as("#drpService"),
     $txtNameOrUrlOrUser=as("#txtNameOrUrlOrUser"),
     $btnSearch=as("#btnSearch"),
     serviceUrl="!",
     toDateTime="!",
     fromDateTime="!";
     
    $fromDateInput.asDateTimeInput({ type: 'calendar', calendar: { params: { height: '30px', width: '190px' } }, theme: 'public' });
    $toDateInput.asDateTimeInput({ type: 'calendar', calendar: { params: { height: '30px', width: '190px' } }, theme: 'public' });
    
     $fromTimeInput.asDateTimeInput({ type: 'time', theme: 'public' });
    $toTimeInput.asDateTimeInput({ type: 'time', theme: 'public' });
    
    
     $winDetail.asModal({width:800});
     
     $drpService.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'Id' },
            parentDataField: { name: 'ParentId' },
            removeChildLessParent: true
        },                  
        url: $.asInitService($.asUrls.cms_masterDataKeyValue_GetByDefaultsLanguageAndTypeId, [{ name: '@typeId', value: 1001 }])
        , displayDataField: 'Name'
          , valueDataField: 'PathOrUrl',
        orderby: 'Order'
    }
 , selectParents: true


});
     
        var calculateSelectedLog = function(event, rows){
        if(event.type==="selected"){
            selectedItems.items[rows[0].Id]={Id:rows[0].Id,Type:rows[0].Type,
            Name:rows[0].Name,ServiceUrl:rows[0].ServiceUrl,
            User:rows[0].User,LocalDateTime:rows[0].LocalDateTime,
            Url:rows[0].Url,ExecutionTimeInMilliseconds:rows[0].ExecutionTimeInMilliseconds}
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
      
        request.url = $.asInitService($.asUrls.develop_reports_ActionLog_GetByPagination, [
            { name: '@orderby', value: orderbyValue }
            ,{ name: '@skip', value: skip }
             ,{ name: '@take', value: request.rowCount}
             ,{ name: '@serviceUrl', value: $.asUrlAsParameter(serviceUrl)}
             ,{ name: '@nameOrUrlOrUser', value: $.asUrlAsParameter($txtNameOrUrlOrUser.val() === "" ? "!":$txtNameOrUrlOrUser.val())}
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
 $.asNotFound(" رویداد")
}

        var bindEvent = function () {
            
       $(asPageEvent).on($.asEvent.page.ready, function (event) {

            });
        
             var clearModal = function(){
                    $divName.empty();
                    $divDataTime.empty();
                    $divParameters.empty();
                    $divCookies.empty();
                    $divRequest.empty();
                      $divUrlReferrer.empty();
                        $divType.empty();
                         $divIsSuccessed.empty();
                         $divIsDebugMode.empty();
                         $divIp.empty();
                         $divIsMobileMode.empty();
               
             }
                  var onSuccess = function(){
           $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
              selectedItems.items={};
              $grvLog.asBootGrid("deselect");
     }

        $drpService.on("change", function (event, item) {
            if (item.selected) {
                serviceUrl = item.value;
               
            }
    });
    as("#btnRemoveFilte").click(function () {
          $drpService.asDropdown('selectValue', [], true);
          serviceUrl="!";
     
          $txtNameOrUrlOrUser.val("");
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
            url: $.asInitService($.asUrls.develop_reports_ActionLog_GetById,[{name:"@id",value:logs[0].Id}]),
            type:"get",
            success: function (actionLog) {
        
               
                    $divName.html(actionLog.Name);
                    $divDataTime.html(actionLog.DateTime);
                    $divParameters.html(actionLog.Parameters);
                    var coockies="";
                    if(actionLog.Coockies != null)
                    $.each(actionLog.Coockies.split(';'),function(i,v){
                        coockies += i + " : " + v + " <br>";
                    });
                    $divCookies.html(coockies);
                     var data = "";
                     
                     if(actionLog.Request !== null && actionLog.Request){
                            $.each(actionLog.Request.split(';'),function(i,v){
                             data += i + " : " + v + " <br>";
                        });
                     }
                 
                    $divRequest.html(data);
                      $divUrlReferrer.html(actionLog.UrlReferrer);
                        $divType.html(actionLog.Type);
                         $divIp.html(actionLog.Ip);
                            $divIsDebugMode.html(actionLog.IsDebugMode ? "بله":"خیر");
                         $divIsMobileMode.html(actionLog.IsMobileMode ? "بله":"خیر");
                         $divIsSuccessed.html(actionLog.IsSuccessed ? "بله":"خیر");
             
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
            url: $.asUrls.develop_reports_ActionLog_Delete,
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