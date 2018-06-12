 $('#i6ec310a6f5e6431590c4d79083aa8d65').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('Report Management');var asPageEvent = '#i6ec310a6f5e6431590c4d79083aa8d65'; var asPageId = '.i6ec310a6f5e6431590c4d79083aa8d65.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({develop_reports_ActionLog_ToggleEnableLog:"/develop/reports/ActionLog/ToggleEnableLog",develop_reports_ActionLog_BackUp:"/develop/reports/ActionLog/BackUp",develop_reports_ErrorLog_BackUp:"/develop/reports/ErrorLog/BackUp",develop_reports_ActionLog_LogStatus:"/develop/reports/ActionLog/LogStatus"}, $.asUrls);  var 
    $chkStatusLog=as("#chkStatus"),
    $btnToggleEnableLog=as("#btnToggleEnableLog"),
    $btnBackUpActionLog=as("#btnBackUpActionLog"),
    $btnBackUpErrorLog=as("#btnBackUpErrorLog");



  
    $(asPageId).asAjax({
                url:$.asUrls.develop_reports_ActionLog_LogStatus ,
                type: "get",
                success: function (status) {
                      $chkStatusLog.prop('checked', status)
                }
            }, {overlayClass: 'as-overlay-absolute' });
            
        var bindEvent =function(){
         
            $btnToggleEnableLog.on('click', function () {
                  $(asPageId).asAjax({
                url:$.asUrls.develop_reports_ActionLog_ToggleEnableLog ,
                type: "get",
                success: function (status) {
                    $chkStatusLog.prop('checked', status)
                }
            }, {overlayClass: 'as-overlay-absolute' });
            });
             $btnBackUpErrorLog.on('click', function () {
                  $(asPageId).asAjax({
                url:$.asUrls.develop_reports_ErrorLog_BackUp,
                type: "get",
                success: function (status) {
                  $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
                }
            }, {overlayClass: 'as-overlay-absolute' });
            });
            
            $btnBackUpActionLog.on('click', function () {
                  $(asPageId).asAjax({
                url:$.asUrls.develop_reports_ActionLog_BackUp,
                type: "get",
                success: function (status) {
                  $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
                }
            }, {overlayClass: 'as-overlay-absolute' });
            });
            
        }
        
        bindEvent();  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });