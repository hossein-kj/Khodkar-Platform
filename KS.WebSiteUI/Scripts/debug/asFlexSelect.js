(function ($) {
    "use strict";
    $.fn.asFlexSelect = function (params, item, options) {
        var $fSelect = $.as(this);

        var source, dataAdepter, defaultParams, selectedText, page = this


        if ($.type(params) === "string") {
            if (params === "setItem") {
                // return $("#" + $fSelect.prop('id') + " ." + $fSelect.data("mainClass")).find('.dropdown-toggle').html(item)
              
                     var selectotorParts = page.selector.split(' ')
                    return $(selectotorParts[0]).find(page.selector.replace(selectotorParts[0],"").substring(1)).find(" ." + $fSelect.data("mainClass")).find('.dropdown-toggle').html(item)
            }
        } else {



            //$.asLoadScriptAndStyle({
            //    urls: [
            //        { url: 'asFlexSelect.css', kind: 'css' }
            //    ],
            //    loadedCallback: function () {
            
            // this.each(function () {
                // console.log("asFlexSelect inner");
                // console.dir(this);
                // $fSelect = $.as(this);

                var loadComplete = function (data) {
                    $fSelect.html('')
                    if (data.length > 0) {
                        $.asSort({ array: data, order: source.order, orderby: source.orderby, type: source.datatype })

                        selectedText = $.asFindItemInJsonArray(data, defaultParams.selectedSearchKey, defaultParams.selectedItemKey, defaultParams.selectedValue)
                        var normalData = { menu: data }



                        defaultParams = $.extend({
                            transform: {
                                "levelZero": {
                                    "tag": "div",
                                    "class": defaultParams.mainClass,
                                    "html": defaultParams.selectedItemtemplate.replace(new RegExp('@selectedText', "g"),
                                            selectedText),
                                    "children": function () {
                                        return (json2html.transform(this, defaultParams.transform.levelOne));
                                    }
                                },
                                "levelOne": {
                                    "tag": "ul",
                                    "class": "dropdown-menu",
                                    "children": function () {
                                        return (json2html.transform(this.menu, defaultParams.transform.levelTwo));
                                    }
                                },

                                "levelTwo": {
                                    "tag": "li",
                                    "html": function () {
                                        return (defaultParams.itemTemplate.replace(new RegExp('@value', "g"),
                                            this[source.valueDataField]).replace(new RegExp('@text', "g"),
                                            this[source.displayDataField]).replace(new RegExp('@url', "g"),
                                            this[source.urlDataField]).replace(new RegExp('@id', "g"),
                                            this[source.idDataField]).replace(new RegExp('@extera', "g"),
                                            this[source.exteraDataField]))
                                    }
                                }
                            }
                        }, defaultParams);

                        $fSelect.json2html(normalData, defaultParams.transform.levelZero)
                    }

   

                    $fSelect.data("mainClass", defaultParams.mainClass)
                    
                   
                    
                    var selectotorParts = page.selector.split(' ')
                  
                     $(selectotorParts[0]).find(page.selector.replace(selectotorParts[0],"").substring(1)).find(" ." + defaultParams.mainClass + " ul li .as-flex-select-link")
                        .off();
                    $(selectotorParts[0]).find(page.selector.replace(selectotorParts[0],"").substring(1)).find(" ." + defaultParams.mainClass + " ul li .as-flex-select-link")
                    .click(function () {
                         $(selectotorParts[0]).find(page.selector.replace(selectotorParts[0],"").substring(1)).trigger("selectedIndexChanged", [$.as(this).data("id"), $.as(this).data("value"),
                             $.as(this).data("text"), $.as(this).data("url"), $.as(this).data("extera")]);
                    })
                    
                    
                    
                    // $("#" + $fSelect.prop('id') + " ." + defaultParams.mainClass + " ul li .as-flex-select-link")
                    //     .off();
                    // $("#" + $fSelect.prop('id') + " ." + defaultParams.mainClass + " ul li .as-flex-select-link")
                    // .click(function () {
                         
                    //      $("#" + $fSelect.prop('id')).trigger("selectedIndexChanged", [$(this).data("id"), $(this).data("value"),
                    //          $(this).data("text"), $(this).data("url"), $(this).data("extera")]);
                    // })

                }
                source = $.extend({ datatype: "json", order: 'asc', orderby: 'order', displayDataField: 'text', valueDataField: 'id', urlDataField: 'url', idDataField: 'id', exteraDataField: 'extera', loadComplete: loadComplete, page: page }, params.source);

                dataAdepter = $.extend({ extraSettings: { loadingText: $.asWaitingViewSmall, overlayClass: 'as-overlay-relative' } }, params.dataAdepter)

                if (source.orderbyDesc) {
                    source.orderby = source.orderbyDesc
                }
                defaultParams = $.extend({
                    //toggleText: $.asRes[$.asLang].dropDownList.select,
                    //titleText: $.asRes[$.asLang].dropDownList.select
                    mainClass: 'btn-group',
                    selectedItemtemplate: '<a class="btn btn-defaults dropdown-toggle" href="#" data-toggle="dropdown"><img src="@selectedText" /> <span class="caret"></span></a>',
                    itemTemplate: '<div class="as-flex-select-link" data-extera="@extera" data-text="@text" data-id="@id" data-value="@value" data-url="@url"><img src="@url">&nbsp;@text</div>',
                    selectedSearchKey: 'country', selectedItemKey: 'flagUrl', selectedValue: 'ایران'
                }, params);





                $fSelect.html('<div style="width: 15px;height: 15px"></div>')

                $fSelect.asDataAdepter(source, dataAdepter)


                //}
            // })







        }
        // allow jQuery chaining
        return this;
    };
})(jQuery);