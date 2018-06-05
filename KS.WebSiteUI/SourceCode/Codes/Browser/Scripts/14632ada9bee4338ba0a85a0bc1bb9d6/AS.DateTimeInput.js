////(function ($) {

//    // jQuery plugin definition
//    $.fn.asDateTimeInput = function (params, date1, date2) {

//        // express a single node as a jQuery object
//        var $dateTimeInput = $(this);
//        if ($.type(params) === "string") {
//            return $dateTimeInput.jqxDateTimeInput(params, date1, date2);
//        } else {
//            // merge default and user parameters
//            var defaultsParam = $.extend(
//            {
//                calendar: {
//                    params: {
//                    },
//                    name: 'persian',
//                    lang: 'fa'
//                },
//                showCalendarButton: true,
//                value: null,
//                min: new Date(1000, 0, 1)
//            }, params);
//            var calendarParams = defaultsParam.calendar;
//            delete defaultsParam.calendar;
//            // traverse all nodes
//            this.each(function () {

//                $dateTimeInput.jqxDateTimeInput(defaultsParam);
//                var id = 'input' + $dateTimeInput.prop('id');
//                if (defaultsParam.showCalendarButton === true) {                   
//                    $('#' + id).asCalendar(calendarParams); //
//                } else {
//                    delete defaultsParam.showCalendarButton;

//                    var defaultsParamTime = $.extend(
//                    {
//                        donetext: $.asRes[$.asLang].select
//                    }, defaultsParam);
//                    $dateTimeInput.clockpicker(defaultsParamTime);
//                }
//            });

//        }
//        // allow jQuery chaining
//        return this;
//    };

//})(jQuery);

(function ($) {
    "use strict";
    $.fn.asDateTimeInput = function (params, date1, date2) {

        // express a single node as a jQuery object
        var $dateTimeInput = $.as(this);
        var id = 'input' + $dateTimeInput.prop('id');
        //var theme = $.asThemeName
        //var themeUrl
       

        if ($.type(params) === "string") {
            if($dateTimeInput.data("type") !== "clock")
             return $('#' + id).asCalendar(params);
            else{
                if(params === "getTime")
                return $('#' + id).val();
                else if(params === "setTime")
                return $('#' + id).val(date1);
            }
             
            //return $dateTimeInput.jqxDateTimeInput(params, date1, date2);
        } else {
            // merge default and user parameters
            var defaultsParam = $.extend(
            {
                calendar: {
                    params: {
                    },
                    name: 'persian',
                    lang: 'fa'
                },
                theme: "white",
                //themeUrl: 'calendar/theme/',
                type: 'calendar',
                layout:'rtl'
            }, params);

            //theme = defaultsParam.theme
            //themeUrl = defaultsParam.themeUrl

            var calendarParams = defaultsParam.calendar.params
            //calendarParams.theme = theme;
            //calendarParams.themeUrl = themeUrl;
            var calendarName = defaultsParam.calendar.name
            var calendarLang = defaultsParam.calendar.lang
            delete defaultsParam.calendar
            //delete defaultsParam.theme
            //delete defaultsParam.themeUrl
            // traverse all nodes
            this.each(function () {
                $dateTimeInput = $.as(this);
                //$.asLoadScriptAndStyle({
                //    urls: [
                //         { url: 'asCalendar.js', kind: 'js' },
                //        { url: 'asDateTimeInput.css', kind: 'css' },
                //        { url: themeUrl + theme + '.min.css', kind: 'css' }
                //    ],
                //    loadedCallback: function () {

                var toTheme = function(className) {
                    return className + " " + className + "-" + defaultsParam.theme;

                }

                var templateDateInput = [
                    '<span class="input-group-addon ui-state-default">',
                    '<div class="' + toTheme('as-date-input-button') + '"></div>',
                    '</span>',
                    '<input id="' + id + '" type="text" class="form-control ' + toTheme('as-date-input') + '" >'
                ].join('');

                var templateTimeInput = [
                  
                    '<span class="input-group-addon ' + toTheme('as-time-input-button') + ' ui-state-default">',
                    '<span class=""></span>',
                    '</span>',
                      '<input id="' + id + '" type="text" class="form-control" >'
                ].join('');

                if (defaultsParam.type === 'calendar'){
                     $dateTimeInput.css({ "direction": defaultsParam.layout}).addClass('input-group').html(templateDateInput);
                     $dateTimeInput.data("type","calendar")
                }
                   
                else{
                    $dateTimeInput.addClass('input-group clockpicker').html(templateTimeInput);
                    $dateTimeInput.data("type","clock")
                }


                //$dateTimeInput.jqxDateTimeInput(defaultsParam);

                if (defaultsParam.type === 'calendar') {
                    $('#' + id).asCalendar(calendarParams, calendarName, calendarLang); //
                } else {
                    delete defaultsParam.type;

                    var defaultsParamTime = $.extend(
                    {
                        donetext: $.asRes[$.asLang].select
                    }, defaultsParam);
                    $dateTimeInput.clockpicker(defaultsParamTime);
                }
                //    }
                //})
            });

        }
        // allow jQuery chaining
        return this;
    };

})(jQuery);