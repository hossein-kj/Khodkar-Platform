
(function ($) {
    "use strict";
    $.fn.asWindow = function (params) {
        var $win = $.as(this);
        //        var template = [
        //' <div class="col-lg-3 col-md-4 col-sm-7 col-xs-9 as-draggable as-shadow hide" style="position:fixed;top:20%;right:5%;background-color: white;z-index: 99999;border: solid 1px;cursor: move;display:none">',
        //           ' <div class="modal-content">',
        //                '<div class="modal-header">',
        //                      '   <button type="button" class="close" aria-hidden="true">&times;</button>',
        //                    ' <h4 class="modal-title">پیدا و جایگزین کردن</h4>',         
        //              '  </div>',
        //                  '<div class="modal-body">',
        //                '</div>',
        //                '<div class="modal-footer">',
        //                 '</div>',
        //            '</div>',
        //    '</div>'
        //        ].join('');

        if ($.type(params) === "string") {
            if (params === "show") {
                $win.removeClass('hide')
                //alert(parseInt(($(window).scrollTop()))) -(parseInt(($(window).scrollTop()) - screen.height))
                $win.css({ "position": "fixed", "top": ($(window).height() / 2) - ($win.outerHeight() / 2), "left": ($(window).width() / 2) - ($win.outerWidth() / 2) })
                if (typeof ($win.data("focusedId")) != "undefined") {
                    $("#" + $win.data("focusedId")).focus();
                    $("#" + $win.data("focusedId")).select();
                }
            }
            if (params === "close") {
                $win.addClass('hide')
            }
        } else {
            // merge default and user parameters
            var defaultsParams = $.extend({ sizeClass: "col-lg-4 col-md-4 col-sm-7 col-xs-9", zIndex: "99999", draggable: true, backgroundColor: "white", closeByEscape: true }, params);
            //$pop.data("defaultsParams", defaultsParams)
            //// traverse all nodes
            this.each(function () {

                // express a single node as a jQuery object
                $win = $.as(this);
                $win.find(".close").click(function () {
                    $win.asWindow("close")
                });

                $win.addClass('as-shadow hide ' + defaultsParams.sizeClass)
                //"position": "relative", "top": defaultsParams.top, "right": defaultsParams.right, 
                $win.css({ "z-index": defaultsParams.zIndex, "border": "solid 1px", backgroundColor: defaultsParams.backgroundColor })
                if (defaultsParams.draggable === true) {
                    $win.find(".modal-header").css({ "cursor": "move" })


                    $win.asDraggabilly({
                        handle: '.as-handle'
                    })
                }
                if (defaultsParams.closeByEscape === true) {
                    $(document)
                        .keyup(function (e) {
                            if (e.keyCode == 27) {
                                $win.asWindow("close")
                            }
                        });
                }
                if (typeof (defaultsParams.focusedId) != "undefined") {
                    $win.data("focusedId", defaultsParams.focusedId)
                }

            });

        }
        // allow jQuery chaining
        return this;
    };
})(jQuery);