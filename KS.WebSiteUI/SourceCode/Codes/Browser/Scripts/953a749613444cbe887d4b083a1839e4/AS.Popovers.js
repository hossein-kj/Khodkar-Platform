(function ($) {
    "use strict";
    $.fn.asPopovers = function (params) {
        var $popup = $.as(this);
        if ($.type(params) === "string") {
            return $popup.popover(params);
        } else {
            // merge default and user parameters
            var defaultsParam = $.extend({ html: true, animation: true, placement: 'auto bottom'}, params);

            // traverse all nodes
            this.each(function () {
                // express a single node as a jQuery object
                $popup = $.as(this);

                $popup.popover(defaultsParam);

            });

        }
        // allow jQuery chaining
        return this;
    };
})(jQuery);