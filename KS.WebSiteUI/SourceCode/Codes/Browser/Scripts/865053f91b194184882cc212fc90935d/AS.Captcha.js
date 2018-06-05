(function ($) {
    "use strict";
    $.fn.asCaptcha = function (params) {

        var $captcha =$.as(this),defaultsParam;
        var btnId = 'btnRefresh' + $captcha.prop('id');
        var imageHolderId = 'placeHolderImageCaptcha' + $captcha.prop('id');

        var refresh = function () {
            defaultsParam=$captcha.data("defaultsParam")
            $captcha.asAjax({
                url: $captcha.data("url"),
                type: "GET",
                success: function (data) {
                    $("#" + imageHolderId).html(' <img src="data:image/png;base64,' + data + ' " />')
                }
            }, { overlayClass: defaultsParam.waitingClass });
        }
        if ($.type(params) === "string") {
            // TODO: COMPLET CAPTCHA
            if (params === "refresh") {
                refresh();
            }
        } else {
            // merge default and user parameters
            defaultsParam = $.extend({ url: '/Captcha/Genrate/', btnClass: 'btn btn-primary',waitingClass:'as-overlay-absolute' }, params)
            
            var template = [
'<div style="display: inline-block" id="' + imageHolderId + '"></div>',
'<div style="margin-top: 8px;margin-right:3px;display: inline-block" id="' + btnId + '" class="' + defaultsParam.btnClass + '"><i class="glyphicon glyphicon-refresh"></i></div>'
            ];
            // traverse all nodes
            // this.each(function () {
                // express a single node as a jQuery object
                // $captcha = $.as(this);

                //$captcha.modal(defaultsParam);

                $captcha.addClass("row")
                $captcha.html(template.join(''))
                //$("#" + imageHolderId).css({ "width": 150, "height": 50 })
                $captcha.data("url", defaultsParam.url);
                $captcha.data("defaultsParam", defaultsParam);
                refresh();
                $("#" + btnId).on('click', function () {
                    $captcha.asCaptcha('refresh');
                });
            // });

        }
        // allow jQuery chaining
        return this;
    };
})(jQuery);
//$.ajax({
//    type: "GET",
//    url: 'Captcha/Genrate/',
//    dataType: "image/png",
//    success: function (data) {
//        //$('#mainImageCaptcha').attr('src', "data:image/png;base64,'" + data + "'");
//        $('#mainCaptcha').html('<img id="mainImageCaptcha" src="Content/images/loader.gif" />');
//    }
//});


//var ajaxOptions = {};
//ajaxOptions.cache = false;
//ajaxOptions.url = "Captcha/Genrate/";
//ajaxOptions.type = "GET";
//ajaxOptions.headers = {};
//ajaxOptions.headers.Accept = "application/octet-stream"
//ajaxOptions.success = function (result) {

//    $("#mainImageCaptcha").attr("src", "data:image/png;base64," + result);
//};
//ajaxOptions.error = function (jqXHR) {
//    console.log("found error");
//    console.log(jqXHR);
//};
//$.ajax(ajaxOptions);