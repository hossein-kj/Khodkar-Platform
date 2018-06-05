(function ($) {
    // jQuery plugin definition
    $.fn.asFixedNav = function (params) {
        var $nav = $.as(this);
        if ($.type(params) === "string") {

        } else {

            //$.asLoadScriptAndStyle({
            //    urls: [
            //        { url: 'asFixedNav.css', kind: 'css' }
            //    ],
            //    loadedCallback: function() {
            // merge default and user parameters
            var defaultsParam = $.extend(params, { numberOfPixcelBeforeStart: 30 });
            $._asNumberOfPixcelBeforeStartMoveFixedNav = defaultsParam.numberOfPixcelBeforeStart
            //// traverse all nodes
            //this.each(function() {
            //    // express a single node as a jQuery object
            //    $nav = $(this);

            $(window).bind('scroll', function () {
                if ($(window).scrollTop() > defaultsParam.numberOfPixcelBeforeStart) {
                    $nav.addClass('as-fixed-nav');
                    $nav.css({ "padding-top": "0" });
                } else {
                    $nav.removeClass('as-fixed-nav');
                    $nav.css({ "padding-top": "85px" });
                }
            });

            //});
            //    }
            //})
        }
        // allow jQuery chaining
        return this;
    };
})(jQuery);