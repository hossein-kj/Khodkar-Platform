(function ($) {
    "use strict";
    $.fn.asMegaMenu = function (params) {
        var $menu =$.as(this);
        var source, dataAdepter, defaultsParams, temp, page = this
        //var data = [
        //    {
        //        "testCase": [
        //            {
        //                "testCaseName": "tc1",
        //                "testStep": [
        //                    {
        //                        "result": "true",
        //                        "testStepName": "ts1",
        //                        "screenShot": "image"
        //                    }, {
        //                        "result": "true",
        //                        "testStepName": "ts2",
        //                        "screenShot": "image"
        //                    }
        //                ] //End of TestStep
        //            },
        //        ],
        //        "testSuiteName": "testSuite1",
        //    },
        //    {
        //        "testCase": [
        //        ],
        //        "testSuiteName": "testSuite2",
        //    }
        //]; // End of testSuite


        //var transform = {
        //    // Printing the Execution stack 
        //    "testSuite": {
        //        "tag": "ul",
        //        'class': "nav navbar-nav  hidden-sm hidden-xs",
        //        "children": function() {
        //            return (json2html.transform(this.menu, transform.testSuiteName));
        //        }
        //    },

        //    "testSuiteName": {
        //        "tag": "li",
        //        'class': function () {
        //            //alert(this.testCase.length)
        //            if (this.testCase.length) return ('dropdown mega-dropdown');
        //        },
        //        "html": function () {
        //            if (this.testCase.length)
        //                return ('<a href="#" class="dropdown-toggle" data-toggle="dropdown">' + this.testSuiteName + '<span class="caret"></span></a>');
        //            else
        //                return ('<a href="#">' + this.testSuiteName +'</a>');
        //        },
        //        "children": function () {
        //            if (this.testCase.length)
        //            return ('<ul class="dropdown-menu mega-dropdown-menu">' + json2html.transform(this.testCase, transform.testCaseName) + '</ul>');
        //        }
        //    },

        //    "testCaseName": {
        //        "tag": "li",
        //        "class": 'col-sm-3',
        //        "html": '<a href="#" class="dropdown-header">${testCaseName}</a> <div class="divider"></div>',
        //        "children": function() {
        //            return (json2html.transform(this.testStep, transform.testStepRetrieval));
        //        }
        //    },

        //    "testStepRetrieval": {
        //        "tag": "ul",
        //        "children": function() {
        //            return (json2html.transform(this, transform.testStep));
        //        }
        //    },

        //    "testStep": {
        //        "tag": "li",
        //        "html": '<li><a href="#">${testStepName}</a></li>'
        //        //"children": function() {
        //        //    return ('<ul>' + json2html.transform(this, transform.testStepResultDescription) + '</ul>');
        //        //}
        //    }


        //    //"testStepResultDescription": {
        //    //    "tag": "li",
        //    //    "children": [
        //    //        {
        //    //            "tag": "div",
        //    //            "html": "${screenShot}               -              ${result}"
        //    //        }
        //    //    ]
        //    //}
        //};

        

        if ($.type(params) === "string") {

        } else {
            var loadComplete = function (data) {
             
                if (data.length > 0) {
                    //if (source.hierarchy) {
                    //    if (source.hierarchy.type === "flat") {
                    //        data = $.asTreeify({ list: data, keyDataField: source.hierarchy.keyDataField, parentDataField: source.hierarchy.parentDataField, childrenDataField: source.hierarchy.childrenDataField })
                    //        source.hierarchy.type = 'tree'
                    //    }
                    //}
                    source.hierarchy.type = 'tree'
                    $.asSort({ array: data, order: source.order, orderby: source.orderby, type: source.datatype, hierarchy: source.hierarchy })
                    var normalData = { menu: data };
                    $menu.json2html(normalData, transform.levelOne)
                }
      
                //#region mega menu
                $(".dropdown").hover(
                    //function() {
                    //    $('.dropdown-menu', this).not('.in .dropdown-menu').stop(true, true).slideDown("400");
                    //    $(this).toggleClass('open');
                    //},
                    //function() {
                    //    $('.dropdown-menu', this).not('.in .dropdown-menu').stop(true, true).slideUp("400");
                    //    $(this).toggleClass('open');
                    //}
                    function () {
                        $('.dropdown-menu', this).stop().slideToggle(400);
                    }

                );
                //#endregion
            }


            source = $.extend({ datatype: "json", text: 'Text', html: 'Html', href: 'Url', order: 'asc', orderby: 'order', loadComplete: loadComplete, page: page }, params.source);
            source.hierarchy = $.extend({ childrenDataField: 'Children' }, params.source.hierarchy);
            dataAdepter = $.extend({ extraSettings: { loadingText: $.asWaitingViewSmall } }, params.dataAdepter)
            defaultsParams = $.extend({ colClass: 'col-sm', colNumber: '3', style: 'width:100%;padding-right:30px',cssClass:'fonts-a-google-pre' }, params)


            var childrenNumber;
            var transform = {
                // Printing the Execution stack 
                "levelOne": {
                    "tag": "ul",
                    'class': "nav navbar-nav  hidden-sm hidden-xs",
                    'style': defaultsParams.style,
                    "children": function () {
                        return (json2html.transform(this.menu, transform.levelTwo));
                    }
                },

                "levelTwo": {
                    "tag": "li",
                    'class': function () {
                        //alert(this.testCase.length)
                        if (this[source.hierarchy.childrenDataField])
                            if (this[source.hierarchy.childrenDataField].length) return ('dropdown as-mega-dropdown');
                    },
                    "html": function () {
                        if (this[source.hierarchy.childrenDataField]) {
                            if (this[source.hierarchy.childrenDataField].length) {
                                temp = this[source.html] ? this[source.html] : this[source.text]
                                return ('<a href="' + this[source.href] + '" class="dropdown-toggle ' + defaultsParams.cssClass + '" data-toggle="dropdown">' + temp + '<span class="caret"></span></a>');
                            } else {
                                temp = this[source.html] ? this[source.html] : this[source.text]
                                return ('<a href="' + this[source.href] + '" class="' + defaultsParams.cssClass + '">' + temp + '</a>');
                            }
                            //if (this.html)

                            //    return ('<a href="#" class="dropdown-toggle" data-toggle="dropdown">' + temp + '<span class="caret"></span></a>');
                            //else
                            //    return ('<a href="#" class="dropdown-toggle" data-toggle="dropdown">' + this.text + '<span class="caret"></span></a>');
                        } else {
                            temp = this[source.html] ? this[source.html] : this[source.text]
                            return ('<a href="' + this[source.href] + '" class="' + defaultsParams.cssClass + '">' + temp + '</a>');

                            //    return ('<a href="#">' + this.html + '</a>');
                            //else
                            //    return ('<a href="#">' + this.text + '</a>');
                        }
                    },
                    "children": function () {
                        if (this[source.hierarchy.childrenDataField]) {
                            childrenNumber = this[source.hierarchy.childrenDataField].length
                            if (this[source.hierarchy.childrenDataField].length)
                                return ('<ul class="dropdown-menu as-mega-dropdown-menu">' + json2html.transform(this[source.hierarchy.childrenDataField], transform.levelThree) + '</ul>');
                        }

                    }
                },

                "levelThree": {
                    "tag": "li",
                    "class": function () {
                        if (childrenNumber > (12 / defaultsParams.colNumber)) return ('pull-left col-sm-3');
                        else return (defaultsParams.colClass + '-' + defaultsParams.colNumber);
                    },
                    "html": '<a href="${' + source.href + '}" class="as-mega-dropdown-header ' + defaultsParams.cssClass + '">${' + source.text + '}</a> <div class="divider"></div>',
                    "children": function () {
                        return ('<ul>' + json2html.transform(this[source.hierarchy.childrenDataField], transform.levelFour) + '</ul>');
                    }
                },

                "levelFour": {
                    "tag": "li",
                    "html": '<li><a href="${' + source.href + '}" class="' + defaultsParams.cssClass + '">${' + source.text + '}</a></li>'
                }
            };
       


            if (source.orderbyDesc) {
                source.orderby = source.orderbyDesc
            }
            delete source.orderbyDesc
            delete params.source;
            delete params.dataAdapter;
            // traverse all nodes
            // this.each(function () {
            // express a single node as a jQuery object


            //$.asLoadScriptAndStyle({
            //    urls: [
            //        { url: 'asMegaMenu.css', kind: 'css' }
            //    ],
            //    loadedCallback: function() {
                    // $menu = $.as(this);
                    $menu.asDataAdepter(source, dataAdepter)
            //    }
            //})

            //})

            //// merge default and user parameters
            //var defaultsParam = $.extend(params, {});

            //// traverse all nodes
            //this.each(function () {
            //    // express a single node as a jQuery object
            //    $menu = $(this);

            //    //$menu.asJson2html(normalData, transform.testSuite);

            // });

        }
        // allow jQuery chaining
        return this;
    };
})(jQuery);