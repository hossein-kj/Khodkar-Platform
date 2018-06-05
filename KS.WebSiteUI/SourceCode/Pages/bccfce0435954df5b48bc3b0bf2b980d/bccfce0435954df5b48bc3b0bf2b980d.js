 var 
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
        
        bindEvent();