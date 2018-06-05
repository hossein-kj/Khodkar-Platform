(function ($) {
    "use strict";
    $.fn.asOffCanvas = function (params) {

        var $menu = $.as(this);
        var source, dataAdepter, menu = $menu.data("menu"), page = this

        if ($.type(params) === "string") {
            menu[params]()
        } else {

            //params = $.extend({ themeUrl: 'offCanvas/', theme: 'fon-light' }, params);
            //$.asLoadScriptAndStyle({
            //    urls: [{ url: 'asOffCanvas.css', kind: 'css' },
            //        { url: params.themeUrl + params.theme + ".css", kind: 'css' }],
            //    loadedCallback: function() {


                    var loadComplete = function (data) {
                       
                        if (data.length > 0) {
                            //if (source.hierarchy) {
                                //if (source.hierarchy.type === "flat") {
                                //    data = $.asTreeify({ list: data, keyDataField: source.hierarchy.keyDataField, parentDataField: source.hierarchy.parentDataField, childrenDataField: source.hierarchy.childrenDataField })
                                //    source.hierarchy.type = 'tree'


                                //    $.asNullizeChildrenOfLenegthOfZero(data, 'children')
                                //}
                            //}
                            source.hierarchy.type = 'tree'
                            $.asSort({ array: data, order: source.order, orderby: source.orderby, type: source.datatype, hierarchy: source.hierarchy })
                           
                         
            
                            params = $.extend({ items: JSON.parse(JSON.stringify(data)
                            .replace(new RegExp('"' + source.text + '":', "g"), '"text":')
                            .replace(new RegExp('"' + source.href + '":', "g"), '"href":')
                            .replace(new RegExp('"' + source.children + '":', "g"), '"children":')) }, params);
                            
                            //console.log(params.items);
                            //params.items[0].Html="";
                            //console.log(params.items);
                            // params.items=[{
                            //                 Html:'<img src="/Content/images/Home-icon.gif" alt="home" width="20" height="21" style="padding-bottom: 3px;"> صفحه اصلی',
                            //         		href: 'http://fooplugins.github.io/foonav',
                            //         		text: 'Home',
                            //         		children:null,
                            //         		Id:1,
                            //         		IsLeaf:true,
                            //                 Order:0,
                            //                 ParentId:null
                            //             	},{
                            //             		href: '#playground',
                            //             		text: 'Playground',
                            //             		children: [{
                            //             			href: 'http://fooplugins.github.io/foonav/docs/playground.html#options',
                            //             			text: 'Options'
                            //             		}]
                            //             	},{
                            //             		href: 'https://github.com/fooplugins/foonav',
                            //             		text: 'GitHub Repo'
                            //             	}]
                            // params.items[1].href="#rooooooooooo";
                            // var arr=[];
                            // arr.push(params.items[0]);
                            // arr.push(params.items[1]);
                            // params.items=arr;
                            menu = FooNav.init(params);
                          
                            $menu.data("menu", menu)
                        }
               

                        window.onresize = function() {
                            menu.reinit(params);
                        };
                    }
                    source = $.extend({ datatype: "json", order: 'asc', orderby: 'order', loadComplete: loadComplete, page:page }, params.source);
                    source.hierarchy = $.extend({ convertchildByZeroLengthToNull:true}, params.source.hierarchy);
                    dataAdepter = $.extend({ extraSettings: { loadingText: $.asWaitingViewSmall } }, params.dataAdepter)
                    if (source.orderbyDesc) {
                        source.orderby = source.orderbyDesc
                    }
                    delete source.orderbyDesc
                    delete params.source;
                    delete params.dataAdapter;
                    // traverse all nodes
                    //this.each(function () {
                    //    // express a single node as a jQuery object
                    //    $menu = $(this);
                    $menu.asDataAdepter(source, dataAdepter)
                    //})


            //    }
            //})
        }


// allow jQuery chaining
        return this;
    };
})(jQuery);