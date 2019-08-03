//(function ($) {
//    // jQuery plugin definition
//    $.fn.asModal = function (params) {
//        var $pop = $(this);
//        if ($.type(params) === "string") {
//            return $pop.modal(params);
//        } else {
//            // merge default and user parameters
//            var defaultsParam = $.extend({ keyboard: true }, params);

//            // traverse all nodes
//            this.each(function () {
//                // express a single node as a jQuery object
//                $pop = $(this);

//                $pop.modal(defaultsParam);

//            });

//        }
//        // allow jQuery chaining
//        return this;
//    };
//})(jQuery);

(function ($) {
    "use strict";
    $.fn.asModal = function (params,url,pageParams) {
        var $pop = $.as(this);
        var template = [
'<div class="modal-header">',
'</div>',
'<div class="modal-body">',
'</div>',
'<div class="modal-footer">',
'</div>'
        ].join('');

        if ($.type(params) === "string") {
            if (params === "show")
                $pop.removeClass('hide')
            if (params === "loadHtml") {
                $pop.asModal('show')
                $pop.asAjax({
                    url: $.Khodkar.formHtmlPath + url + ".html",
                    type: "get",
                    success: function(data) {
                        $pop.empty();
                        $pop.html(data);
                        $pop.modal('handleUpdate');
                    }
                }, { overlayClass: 'as-overlay-relative' });
            } else if (params === "load") {
               
                var modalParams = $.asModalManager.modalsParams[$pop.attr("id")] //$pop.data("defaultsParams")
                   if (pageParams)
                    modalParams.pageParams = pageParams
                  
                    if(modalParams === undefined){
                        modalParams = {}
                         modalParams.isFirstLoad =true
                    }
                    
                if(modalParams.isFirstLoad){
               
                var loadModal =function (data) {
                        modalParams.isFirstLoad=false;
                        modalParams.pageId = data.pageId
                        $.asModalManager.modalsParams[$pop.attr("id")] = modalParams // $pop.data("defaultsParams", modalParams);
                        //$.asSetWebPageData({ $holder: $pop, isModal: true, data: data })
                        $.asSetupPage(data, $pop, true, true, modalParams.pageParams)
                        //$pop.html(data.page);
                    }
                $pop.asModal('show')
                $pop.empty();
                $pop.html(template)
                $pop.asAjax({
                    url: url,
                    type: "get",
                    success: function (data) {
                     loadModal(data)
                    },
                     error:function(data){
                         loadModal(data.responseJSON)
                    }
                }, { overlayClass: 'as-overlay-relative' });
                }else{
                    
                    $.asModalManager.modalsParams[$pop.attr("id")] = modalParams //$pop.data("defaultsParams", modalParams);
                   $pop.asModal('show')
                    $("#" + modalParams.pageId).trigger($.asEvent.modal.reopen, [modalParams.pageParams])
                }

            } else {
                 $pop.modal($.asModalManager.modalsParams[$pop.attr("id")])
                return $pop.modal(params)
            }
        } else {
            // merge default and user parameters
            var defaultsParams = $.extend({ keyboard: true,width:400, tabindex: -1, show: false,isFirstLoad:true }, params);
           
            //// traverse all nodes
            this.each(function () {
                $pop = $.as(this);
                 $.asModalManager.modalsParams[$pop.attr("id")] = defaultsParams//$pop.data("defaultsParams", defaultsParams)
            //$.asLoadScriptAndStyle({
            //    urls: [
            //        { url: 'asModal.css', kind: 'css' }
            //    ],
            //    loadedCallback: function() {
                    // express a single node as a jQuery object
                    //$pop = $(this);
                    $pop.addClass('modal hide fade')
                    //$pop.data("width", defaultsParams.width)
                    $pop.prop("tabindex", defaultsParams.tabindex)
                    $pop.data("width", defaultsParams.width)
                    //$pop.html(defaultsParams.waitingView)
                    //delete defaultsParams.tabindex
                    //delete defaultsParams.waitingView
                    $pop.modal(defaultsParams);

            //    }
            })

        }
        // allow jQuery chaining
        return this;
    };
})(jQuery);