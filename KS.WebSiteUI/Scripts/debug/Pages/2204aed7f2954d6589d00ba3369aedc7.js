 $('#i2204aed7f2954d6589d00ba3369aedc7').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('مدیریت گزارشات');var asPageEvent = '#i2204aed7f2954d6589d00ba3369aedc7'; var asPageId = '.i2204aed7f2954d6589d00ba3369aedc7.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({develop_reports_ActionLog_ToggleEnableLog:"/develop/reports/ActionLog/ToggleEnableLog",develop_reports_ActionLog_BackUp:"/develop/reports/ActionLog/BackUp",develop_reports_ErrorLog_BackUp:"/develop/reports/ErrorLog/BackUp",develop_reports_ActionLog_LogStatus:"/develop/reports/ActionLog/LogStatus"}, $.asUrls);    var
   $btnCancel =as("#btnCancel"),
   $chkStatusLog=as("#chkStatusLog"),
   $btnToggleEnableLog=as("#btnToggleEnableLog"),
    $btnBackUpActionLog=as("#btnBackUpActionLog"),
    $btnBackUpErrorLog=as("#btnBackUpErrorLog"),
    $win=as(asPageId);
    
  
    $win.asAjax({
                url:$.asUrls.develop_reports_ActionLog_LogStatus ,
                type: "get",
                success: function (status) {
                    $chkStatusLog.prop('checked', status)
                }
            }, {overlayClass: 'as-overlay-absolute' });
            
  var bindEvent =function(){
          $(asPageEvent).on($.asEvent.page.ready, function (event) {
           
           });
           
             $btnCancel.on('click', function () {
                $win.asModal('hide');
            });
             $btnToggleEnableLog.on('click', function () {
                $win.asAjax({
                url:$.asUrls.develop_reports_ActionLog_ToggleEnableLog ,
                type: "get",
                success: function (status) {
                    $chkStatusLog.prop('checked', status)
                }
            }, {overlayClass: 'as-overlay-absolute' });
            });
            
               $btnBackUpErrorLog.on('click', function () {
                 $win.asAjax({
                url:$.asUrls.develop_reports_ErrorLog_BackUp,
                type: "get",
                success: function (status) {
                  $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
                }
            }, {overlayClass: 'as-overlay-absolute' });
            });
            
            $btnBackUpActionLog.on('click', function () {
                 $win.asAjax({
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