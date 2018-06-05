(function ($) {
    "use strict";
    $.fn.asCalendar = function (params,name,lang) {
        var defaultsParam;
        var calendar;
        var $calendar = $.as(this);

        if ($.type(params) === "string") {
            return $calendar.calendarsPicker(params);
        } else {
            this.each(function () {
                $calendar = $.as(this);
                //defaultsParam = $.extend({ themeUrl: 'calendar/theme/', theme: $.asThemeName }, params);


                //$.asLoadScriptAndStyle({
                //    urls: [
                //           { url: 'asCalendar.css', kind: 'css' },
                //        { url: defaultsParam.themeUrl + defaultsParam.theme + '.min.css', kind: 'css' }
                //    ],
                //    loadedCallback: function() {
                if (name == "gregorian" && lang == "en") {
                    defaultsParam = $.extend({ renderer: $.calendarsPicker.themeRollerRenderer, dateFormat: 'yyyy/mm/dd' }, params);
                    $.calendarsPicker.setDefaults($.calendarsPicker.regionalOptions['']);
                } else if (name != null && lang != null) {
                    $.calendarsPicker.setDefaults($.calendarsPicker.regionalOptions[lang]);
                    calendar = $.calendars.instance(name, lang);
                    defaultsParam = $.extend({ calendar: calendar, renderer: $.calendarsPicker.themeRollerRenderer }, params);
                } else {
                    $.calendarsPicker.setDefaults($.calendarsPicker.regionalOptions['fa']);
                    calendar = $.calendars.instance('persian', 'fa');
                    defaultsParam = $.extend({ calendar: calendar, renderer: $.calendarsPicker.themeRollerRenderer }, params);
                }
                //this.each(function() {
                //    $calendar = $(this);

                $calendar.calendarsPicker(defaultsParam);
                $calendar.css({ "height": defaultsParam.height, "width": defaultsParam.width });
                //$calendar.css({"height":"30px","width":"90px"});
                //});

                //    }
                //})
            });
        }


        // allow jQuery chaining
        return this;
    };

})(jQuery);