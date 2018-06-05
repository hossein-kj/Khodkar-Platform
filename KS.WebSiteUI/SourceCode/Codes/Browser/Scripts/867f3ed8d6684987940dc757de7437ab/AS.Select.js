(function ($) {
    "use strict";
    $.fn.asSelect = function (params,id,val) {
        var $select = $.as(this);
        var selectId = "#" + $select.prop('id')
        var source, dataAdepter, page = this
        //var order='asc'
        if ($.type(params) === "string") {
            if (params === "text") {
                return $(selectId + " option:selected").text()
            }
            return $select.selectpicker(params, id, val);
        } else {
            var _loadComplete = function (data) {
                if (data.length > 0) {
                    if (source.datatype === "array") {
                        $.asSort({ array: data, order: source.order, orderby: source.orderby, datatype: source.datatype })
                        $.each(data, function(key, text) {
                            $select.append('<option value=' + text + '>' + text + '</option>');
                        })
                    } else {
                        if (source.datatype === "json") {
                            $.asSort({ array: data, order: source.order, orderby: source.orderby, datatype: source.datatype })
                        } else if (source.datatype === "jsonObject") {
                            var dataArray = []
                            for (key in data) {
                                var value = data[key];
                                dataArray.push({ key: key, value: value })
                                $.asSort({ array: dataArray, order: source.order, orderby: source.orderby, datatype: source.datatype })
                                data = dataArray
                            }

                           
                        }
                        $.each(data, function(key, v) {
                            $select.append('<option value=' + v[source.valueMember] + '>' + v[source.displayMember] + '</option>')
                        })
                    }
                }


                $select.selectpicker(params)

                if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
                    $('.selectpicker').selectpicker('mobile');
                }

                $select.on('changed.bs.select', function (e, index) {
                    if(params.onSelectedChanged)
                        params.onSelectedChanged()
                });
            }
            //var defaultsParam = $.extend({ source: { loadComplete: _loadComplete }, dataAdepter: {} }, params);
            source = $.extend( {datatype: "json",order:'asc', orderby: 'value', valueMember: 'key', displayMember: 'value', loadComplete: _loadComplete,page:page },params.source);
            dataAdepter = $.extend({ extraSettings: { loadingText: $.asWaitingViewSmall } }, params.dataAdepter)
            if (source.orderbyDesc) {
                //order = 'desc'
                source.orderby = source.orderbyDesc
            }
            delete source.orderbyDesc
            delete params.source;
            delete params.dataAdapter;

            $.asLoadScriptAndStyle({
                urls: [
                    { url: 'asSelect.css', kind: 'css' }
                ],
                loadedCallback: function() {
                    // traverse all nodes
                    //this.each(function() {
                        // express a single node as a jQuery object
                        //$select = $(this);
                        $.fn.selectpicker.defaults = $.asRes[$.asLang].selectpicker

                        $select.asDataAdepter(source, dataAdepter)
                    //})
                }
            })

        }
        // allow jQuery chaining
        return this;
    };
})(jQuery);

//$('#selectpicker').asSelect({
//    source: {
//        //datatype: "jsonObject",
//        datatype: "json",
//        //datatype: "array",
//        //localdata: { "4": "mohsen", "3": "akbar" }
//        //localdata: ["one","akbar"]
//        localdata: [{ 'key': 3, 'value': "اکبر" }, { 'key': 2, 'value': "محسن" }],
//        order:'asc'
//    },onSelectedChanged: function() {
//        alert($('#selectpicker').asSelect("val"))
//        alert($('#selectpicker').asSelect("text"))
//    }
//});


//<select id="selectpicker" class="show-menu-arrow">
//           <!--<option>All Image</option>
//   <option>Ketchup</option>
//   <option>Relish</option>-->
//           <option>پیشنهاد و انتقاد</option>
//           <option>قدردانی و تشکر</option>
//           <option>درخواست پشتیبانی</option>
//       </select>