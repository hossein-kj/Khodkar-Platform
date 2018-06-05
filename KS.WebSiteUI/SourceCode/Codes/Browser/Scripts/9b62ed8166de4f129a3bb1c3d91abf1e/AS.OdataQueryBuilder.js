//(function ($) {
//	// jQuery plugin definition
//	$.fn.asOdataQueryBuilder = function (params) {
//	    var $query = $(this);


//	    $query.asLoadScriptAndStyle({
//	        urls: [
//	            { url: 'odataquerybuilder/q.js', kind: 'js' },
//	            { url: 'odataquerybuilder/breeze.js', kind: 'js' }
//	        ],
//	        loadedCallback: function() {

//	            //if ($.type(params) === "string") {
//	            //    if (params === "Predicate")
//	            //        return breeze.Predicate
//	            //    else if (params === "EntityQuery")
//	            //        return breeze.EntityQuery
//	            //    else return this
//	            //} else {

//	            if ($.asLoaderCssJs.loadingCssJs.length <= 0) {

//	                console.dir(define["breeze"])
//	                console.dir(define["amd"].breeze)
//	                var checkReady = function (callback) {
                       
//	                    if (require("breeze")) {
//                            console.log("y")
//                            callback(require("breeze"));
//	                    }
//	                    else {
//	                        window.setTimeout(function () { checkReady(callback); }, 100);
//	                    }
//	                };

//	                // Start polling...
//	                checkReady(function (breeze) {

//	                    var context = {}
//	                    var defaultsParam = $.extend({ url: "/odata/cms/" }, params);
//	                    var dataService = new breeze.DataService({
//	                        serviceName: defaultsParam.url,
//	                        hasServerMetadata: false
//	                    });
//	                    context.manager = new breeze.EntityManager({
//	                        dataService: dataService
//	                    });
//	                    context.predicate = breeze.Predicate
//	                    context.entityQuery = breeze.EntityQuery
//	                    defaultsParam.callBack(context)
//	                });


//	            }
//	            //}


//	        }
//	    });


	    //$query.asLoadScriptAndStyle({
	    //    urls: [
	    //        { url: 'odataquerybuilder/q.js', kind: 'js' },
	    //        { url: 'odataquerybuilder/breeze.js', kind: 'js' }
	    //    ],
	    //    loadedCallback: function () {
	    //        // Poll for breeze to come into existance
	    //        var checkReady = function (callback) {
	    //            if (window.breeze) {
	    //                callback(breeze);
	    //            }
	    //            else {
	    //                window.setTimeout(function () { checkReady(callback); }, 100);
	    //            }
	    //        };

	    //        checkReady(function (breeze) {

	    //        });


	    //    }
	    //})
//	};
//})(jQuery);



//define(["jquery","breeze"], // Require jquery
//    function($,breeze) {

//    // jQuery plugin definition
//    $.fn.asOdataQueryBuilder = function (params) {
//        //var $query = $(this);
//        if ($.type(params) === "string") {
//            if (params === "Predicate")
//                return breeze.Predicate
//            else if (params === "EntityQuery")
//                return breeze.EntityQuery
//            else return this
//        } else {

//            var defaultsParam = $.extend({ url: "/odata/cms/" }, params);
//            var dataService = new breeze.DataService({
//                serviceName: defaultsParam.url,
//                hasServerMetadata: false
//            });
//            var manager = new breeze.EntityManager({
//                dataService: dataService
//            });
//            return manager;
//        }
//    };

//    });


$.asOdataQueryBuilder = (function () {
    "use strict";
        return function(params) {
            //var $query = $(this);
            if (typeof params === "string") {
                if (params === "Predicate")
                    return breeze.Predicate
                else if (params === "EntityQuery")
                    return breeze.EntityQuery
                else return this
            } else {

                var defaultsParam = $.extend({ url: "/odata/cms/" }, params);
                var dataService = new breeze.DataService({
                    serviceName: defaultsParam.url,
                    hasServerMetadata: false
                });
                var manager = new breeze.EntityManager({
                    dataService: dataService
                });
                return manager;
            }
        };
    })();



//(function ($) {
//	// jQuery plugin definition
//	$.fn.asOdataQueryBuilder = function (params) {
//	    var $query = $(this);


//	    $query.asLoadScriptAndStyle({
//	        urls: [
//	            { url: 'odataquerybuilder/q.js', kind: 'js' },
//	            { url: 'odataquerybuilder/breeze.js', kind: 'js' }
//	        ],
//	        loadedCallback: function() {

//	            //if ($.type(params) === "string") {
//	            //    if (params === "Predicate")
//	            //        return breeze.Predicate
//	            //    else if (params === "EntityQuery")
//	            //        return breeze.EntityQuery
//	            //    else return this
//	            //} else {

//	            if ($.asLoaderCssJs.loadingCssJs.length <= 0) {
//                    console.log("x")
//	                require(["q","breeze"], // Require jquery
//	                    function(q,breeze) {
//	                        console.log("x")
//	                        var context = {}
//	                        var defaultsParam = $.extend({ url: "/odata/cms/" }, params);
//	                        var dataService = new breeze.DataService({
//	                            serviceName: defaultsParam.url,
//	                            hasServerMetadata: false
//	                        });
//	                        context.manager = new breeze.EntityManager({
//	                            dataService: dataService
//	                        });
//	                        context.predicate = breeze.Predicate
//	                        context.entityQuery = breeze.EntityQuery
//	                        defaultsParam.callBack(context)

//	                    })

//	            }
//	            //}


//	        }
//	    });
//	};
//})(jQuery);
