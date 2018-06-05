(function ($) {
    "use strict";
    $.fn.asDropdown = function (params, value, options, newParent) {
        var $drp = $.as(this);
        var source, dataAdepter, defaultParams, page = this
        var $ul = $("#" + $drp.prop('id') + " ul:first");
        var transform = {
            // Printing the Execution stack 
            "levelOne": {
                "tag": "ul",
                //"style": "display: none",
                "children": function () {
                    return (json2html.transform(this.menu, transform.levelTwo));
                }
            },

            "levelTwo": {
                "tag": "li",
                'data-dropdown-text': function () {

                    if (this[source.hierarchy.childrenDataField])
                        if (this[source.hierarchy.childrenDataField].length) return ($.asGetPropertybyName(this, source.displayDataField));
                },
                'data-dropdown-value': function () {

                    //if (!this[source.hierarchy.childrenDataField])
                    return ($.asGetPropertybyName(this, source.valueDataField));
                },
                'data-dropdown-href': function () {

                    //if (!this[source.hierarchy.childrenDataField])
                    return ($.asGetPropertybyName(this, source.valueDataField));
                },
                "html": function () {
                    if (this[source.hierarchy.childrenDataField]) {
                        if (this[source.hierarchy.childrenDataField].length) {
                            return ''
                        } else {
                            return ($.asGetPropertybyName(this, source.displayDataField))
                        }
                    } else {
                        return ($.asGetPropertybyName(this, source.displayDataField))
                    }
                },
                "children": function () {
                    return (json2html.transform(this[source.hierarchy.childrenDataField], transform.levelThree));

                }
            },
            "levelThree": {
                "tag": "ul",
                "children": function () {
                    return (json2html.transform(this, transform.levelTwo));
                }
            }
        };

        var removeItem = function (item, lis) {
            if ($.isArray(lis)) {
                $.each(lis,
                    function (i, v) {

                        lis.find("span:contains('" + item.text + "')").parent().parent().hide()
                    })
            } else {
                lis.find("span:contains('" + item.text + "')").parent().parent().hide()
            }

            //console.dir($("#" + $drp.prop('id') + " ul:eq(2)").find("span:contains('" + item.text + "')").parent().parent())
            //.find('[data-dropdown-text="' + item.text + '"]').hide()
        }
        var start = function () {
            // page.each(function () {
                // $drp = $(page);
                //$.asLoadScriptAndStyle({
                //    urls: [
                //        { url: 'asDropdown.css', kind: 'css' }
                //    ],
                //    loadedCallback: function () {


                var loadComplete = function (data) {
                    $drp.html('')
                    if (data.length > 0) {
                        //if (source.hierarchy) {
                        //    if (source.hierarchy.type === "flat") {
                        //        data = $.asTreeify({ list: data, keyDataField: source.hierarchy.keyDataField, parentDataField: source.hierarchy.parentDataField, childrenDataField: source.hierarchy.childrenDataField })
                        //        source.hierarchy.type = 'tree'
                        //    }
                        //}
                        source.hierarchy.type = 'tree'
                        $.asSort({ array: data, order: source.order, orderby: source.orderby, type: source.datatype, hierarchy: source.hierarchy })
                        var normalData;
                        if (defaultParams.parentMode === "multi") {
                            normalData = { menu: data }
                        } else {
                            normalData = { menu: data[0][source.hierarchy.childrenDataField] }
                            defaultParams.toggleText = defaultParams.titleText = $.asGetPropertybyName(data[0], source.displayDataField)
                        }

                        $drp.json2html(normalData, transform.levelOne)
                        delete defaultParams.parentMode;
                    }

                    $("#" + $drp.prop('id') + " ul:first").dropdown(
                                //{
                                ////selectParents: true
                                //}
                                defaultParams
                            )

                    if (typeof defaultParams.moveByFixedNav !== 'undefined') {
                        $.asMoveByFixedNave({ $eleman: $drp, initialTop: defaultParams.moveByFixedNav.initialTop })
                    }




                }
                source = $.extend({ datatype: "json", order: 'asc', orderby: 'order', displayDataField: 'text', valueDataField: 'id', loadComplete: loadComplete, page: page }, params.source);
                source.hierarchy = $.extend({ childrenDataField: 'children' }, params.source.hierarchy);
                dataAdepter = $.extend({ extraSettings: { loadingText: $.asWaitingViewSmall, overlayClass: 'as-overlay-relative' } }, params.dataAdepter)

                if (source.orderbyDesc) {
                    source.orderby = source.orderbyDesc
                }
                defaultParams = $.extend({
                    parentMode: "multi",
                    toggleText: $.asRes[$.asLang].dropDownList.select,
                    titleText: $.asRes[$.asLang].dropDownList.select,
                    backText: $.asRes[$.asLang].back,
                    closeText: $.asRes[$.asLang].close,
                    link: false,
                    enableDeselectOnSingleMode: false

                }, params);
                //delete source.orderbyDesc
                //delete params.source;
                //delete params.dataAdapter;

                // traverse all nodes
                //this.each(function() {
                // express a single node as a jQuery object
                //$drp = $(this);
                $drp.html('<div style="width: 15px;height: 15px"></div>')
               
                $drp.after("<input type='hidden' id='" + $drp.prop('id') + "_as_hiden' name='" + $drp.prop('id') + "_as_hiden' class='as-validate-hidden' />");
                $drp.asDataAdepter(source, dataAdepter)
                //})

                //}
            // })
        }

        if ($.type(params) === "string") {
            if (params === "addItem") {
                var newItem
                //, newParents = $drp.data("newParents") || []
                //var newItem = {},childItem = {}, children = source.hierarchy.childrenDataField,text=source.displayDataField;
                //childItem.items= []

                //newItem[children] = { items: false }
                //newItem[text]
                //childItem.items.push()



                //if (options.children.items === false && newParents[options.value] !== true) {
                if (newParent === true) {
                    //newParents[options.value] = true
                    //$drp.data("newParents", newParents)

                    removeItem($ul.dropdown('selected'), $("#" + $drp.prop('id') + " ul").find('li.as-dropdown-selected'))
                    newItem = [{
                        children: { items: [{ children: { items: false }, text: value.text, value: value.value }] },
                        text: options.text, value: options.value, parent: options.parent
                    }]
                } else {
                    if (options != null)
                        newItem = [{ children: { items: false }, text: value.text, value: value.value, parent: options.uid }]
                    else
                        newItem = [{ children: { items: false }, text: value.text, value: value.value }]
                }
                if (options != null)
                    $ul.dropdown("addItem", newItem, options.menu)
                else
                    $ul.dropdown("addItem", newItem)
            }
            else if (params === "removeItem") {
                var selectedLi = $("#" + $drp.prop('id') + " ul").find('li.as-dropdown-selected')

                var selectedItems = $ul.dropdown('selected')
                if (selectedItems) {
                    if ($.isArray(selectedItems)) {
                        $.each(selectedItems,
                            function (i, v) {
                                removeItem(v, selectedLi)
                            })

                    } else {
                        removeItem(selectedItems, selectedLi)
                    }
                    $ul.dropdown('selectValue', [], true)
                    $ul.dropdown("reset")
                }

            } else if (params === "reload") {
                params = $drp.data("params")
                if(params){
                 delete params.source.url
                delete params.source.localData
                }else{
                     params =  {}
                }
                params.source = $.extend(value, params.source);

                $ul.dropdown("destroy")
                $("#" + $drp.prop('id') + "_as_hiden").remove()
                $drp.empty()
                $drp.data("params", params)
                start()
               
            } else if (params === "init") {
                $drp.data("params", options)
                $drp.html('<div>' + value + '</div>')
            } else {
                return $ul.dropdown(params, value, options)
            }

        } else {

            $drp.data("params", params)
            start()






        }
        // allow jQuery chaining
        return this;
    };
})(jQuery);

//<!--<ul style="display: none">
//                    <li>Item 1</li>
//                    <li data-dropdown-text="Item 2">
//                        <ul>
//                            <li data-dropdown-text="Item 2.1">
//                                <ul>
//                                    <li>Item 2.1.1</li>
//                                    <li>Item 2.1.2</li>
//                                    <li>Item 2.1.3</li>
//                                </ul>
//                            </li>
//                            <li data-dropdown-text="Item 2.2">
//                                <ul>
//                                    <li>Item 2.2.1</li>
//                                    <li>Item 2.2.2</li>
//                                    <li>Item 2.2.3</li>
//                                </ul>
//                            </li>
//                            <li data-dropdown-text="Item 2.3">
//                                <ul>
//                                    <li>Item 2.3.1</li>
//                                    <li>Item 2.3.2</li>
//                                    <li>Item 2.3.3</li>
//                                </ul>
//                            </li>
//                            <li>Item 2.4</li>
//                        </ul>
//                    </li>
//                    <li>Item 3</li>
//                </ul>-->