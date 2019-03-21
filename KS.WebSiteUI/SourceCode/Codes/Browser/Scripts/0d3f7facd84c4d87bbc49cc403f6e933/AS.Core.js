"use strict";
(function () {
    $.as=function(params){
        if(params.pageId){
            this.pageId=params.pageId
            this.as=function(id){
                var pageId = this.pageId
                var temp = $(pageId).find(id); 
                temp.selector=this.pageId+' '+id;
                 return temp;
            }
        }else{
            var temp = $(params);
            if(params.selector){
            temp.selector=params.selector;
            }else if(temp.hasClass("asPage")){
                temp.selector="";
                var classes = temp.attr('class').split(' ');
                for(var i=0; i<classes.length; i++){
                    temp.selector+="." + classes[i];
                }
            }
             return temp;
        }
    }
    $.asDebugMessageTimeOut = 10000000
    $.asReleaseMessageTimeOut = 8000
    $.asTemp = {}
    $.asEvent = {}
    $.asEvent = {
        global: {
            dataLoadComplete: 'dataLoadComplete',
            beforeSendAjaxRequest: 'beforeSendAjaxRequest',
            login: 'loginSuccessed',
            logoff: 'logoffSuccessed',
            queryStringChange: 'queryStringChange',
            fontLoad:'fontLoad',
            fontLoaded:'fontLoaded'
        },
        page: {
            dataLoading: "pageDataLoading",
            dataLoaded: "pageDataLoaded",
            loading: "pageLoading",
            loaded: "pageLoaded",
            dispose: "pageDispose",
            ready: "pageReady",
            queryStringChange: 'queryStringChange',
            beforeSendAjaxRequest: 'beforeSendAjaxRequest'
        },modal:{
            reopen:"modalReopened"
        }

    };
    $.asModule = {
        loaded: {}
    }
    $.asGetPageEventName = function (selector) {
        return "#" + selector.selector.substring(1, selector.selector.indexOf("." + $.asPageClass))
    }
 
    $.asCallDisposeEventOfAllPage = function(){
        $.each($.asPage.ids, function (key, val) {
            $("#" + key).trigger($.asEvent.page.dispose, [])
            });
    }

    //#region bind event
    $.asBindEvent = function () {

        $($.asEvent.global).on($.asEvent.global.queryStringChange, function (event, pageUrl, queryString) {
            $.each($.asPage.ids, function (key, val) {
                $("#" + key).trigger($.asEvent.page.queryStringChange, [pageUrl, queryString])
            });

        });

        $($.asEvent.global).on($.asEvent.global.dataLoadComplete, function (event, id, name) {

            $($.asEvent.page).trigger($.asEvent.page.dataLoading, [id, name]);
            //$($.asEvent.template).trigger($.asEvent.global.dataLoadComplete, [id, name]);
        });

        $($.asEvent.global).on($.asEvent.global.beforeSendAjaxRequest, function (event, setting) {

            $.each($.asPage.ids, function (key, val) {
                $("#" + key).trigger($.asEvent.page.beforeSendAjaxRequest, [setting])
            });
        });

        $($.asEvent.global).on($.asEvent.global.logoff, function (event) {
            $.asCallDisposeEventOfAllPage()
            $($.asEvent.page).trigger($.asEvent.global.logoff, []);
        });

        $($.asEvent.global).on("info", function (ex, message, userVisible, errorMesage, logable) {
            if (userVisible) {
                $.toaster({ priority: 'info', title: $.asRes[$.asLang].info, message: message, settings: {
                    'timeout': $.asDebugMode() === true ? $.asDebugMessageTimeOut:$.asReleaseMessageTimeOut 
                    ,'debyg':$.asDebugMode()
                } });
            }
        });

        $($.asEvent.global).on("warning", function (ex, message, userVisible, errorMesage, logable) {
            if (userVisible) {
                $.toaster({ priority: 'warning', title: $.asRes[$.asLang].warning, message: message, settings: {
                    'timeout': $.asDebugMode() === true ? $.asDebugMessageTimeOut:$.asReleaseMessageTimeOut 
                    ,'debyg':$.asDebugMode()
                    
                } });
            }
        });

        $($.asEvent.global).on("error", function (ex, message, userVisible, errorMesage, logable) {
            if (userVisible) {
                //$("<div>" + message + "</div>").asNotification({
                //    width: 250,
                //    position: "bottom-right",
                //    opacity: 0.9,
                //    autoOpen: true,
                //    animationOpenDelay: 800,
                //    autoClose: true,
                //    autoCloseDelay: 3000,
                //    template: "error",
                //    showCloseButton: true
                //});

                $.toaster({ priority: 'danger', title: $.asRes[$.asLang].error, message: message, settings: {
                    'timeout': $.asDebugMode() === true ? $.asDebugMessageTimeOut:$.asReleaseMessageTimeOut 
                    ,'debyg':$.asDebugMode()
                    
                } });
            }
        });
        $($.asEvent.global).on("success", function (load, message, userVisible, errorMesage, logable,showTime) {

            //$("<div>" + message + "</div>").asNotification({
            //    width: 250,
            //    position: "bottom-right",
            //    opacity: 0.9,
            //    autoOpen: true,
            //    animationOpenDelay: 800,
            //    autoClose: true,
            //    autoCloseDelay: 3000,
            //    template: "success",
            //    showCloseButton: true
            //});

            $.toaster({ priority: 'success', title: $.asRes[$.asLang].message, message: message, settings: { 
                   'timeout':showTime===null ? ($.asDebugMode() === true ? $.asDebugMessageTimeOut:$.asReleaseMessageTimeOut ):showTime
                    ,'debyg':$.asDebugMode()
                } });
        });
        $($.asEvent.global).on($.asEvent.global.login,
            function () {
                //$($.asEvent.template).trigger($.asEvent.global.login, []);
                //$($.asEvent.page).trigger($.asEvent.global.login, []);
                $.asAccountManager.mainHandler()
                $.asAccountManager.otherHandler()
                $.asAccountManager.loginInProcess = false
            }
        );
    }

    $.asBindPageEvent = function (pageId) {
        var pageEvent = "#" + pageId
        $(pageEvent).on($.asEvent.page.loading, function (event, mustLoad, loaded) {

        });

        $(pageEvent).one($.asEvent.page.dataLoaded, function (event) {
            $(pageEvent).trigger($.asEvent.page.ready, [])
            
        
                    
             $($.asEvent.global).trigger($.asEvent.global.fontLoad, []);
             
        });
    }


    $.asMoveByFixedNave = function (params) {
        var moveByFixedNave = function () {
            if ($(window).scrollTop() > $._asNumberOfPixcelBeforeStartMoveFixedNav) {
                params.$eleman.css({ 'position': 'fixed', 'top': '0' })
            } else {
                params.$eleman.css({ 'top': params.initialTop + 'px' })
            }
        }
        moveByFixedNave()
        $(window).bind('scroll', function () {
            moveByFixedNave()
        });
    }

    //#endregion

    $.asDebugMode = function (val) {
        if (typeof (val) != 'undefined') {
            $.asStorage.setItem($.asCoockiesName.asIsDebugMode, val)
            $.asCookies.set($.asCoockiesName.asIsDebugMode, { IsDebugMode: val }, { expires: 10000000 })
        }
        else {
               try{
            if ($.asStorage.getItem($.asCoockiesName.asIsDebugMode) !== null) {
                    return $.asStorage.getItem($.asCoockiesName.asIsDebugMode) == "true"
            }
            else {
                return false;
            }
            }catch(err){
                  return false 
               }
        }

    }

    $.asGetScriptPath = function(){
        var debugId = $.asStorage.getItem($.asDebugId) === null ? "": $.asDebugIdSign + "/" + $.asStorage.getItem($.asDebugId)+ "/"
        var path = $.asDebugMode() === true ? $.asDebugBaceScriptUrl + debugId : $.asBaceScriptUrl
        return path ? path:"/scripts/dist/"
    }
    
    $.asGetStylePath = function(){
        var path = $.asDebugMode() === true ? $.asDebugBaceStyleUrl : $.asBaceStyleUrl
        return path?path:"/content/dist/"
    }
    
    $.asCacheSuffix = "version=1"
    $.asCurrentTemplateUrl = ""
    $.asCurrentFrameWorkUrl = ""
    $.asDebugIdSign = "debugid"
    $.asCounter = 0
    $.asUrls = {};
    $.asLang = 'fa';
    $.asWaitingViewSmall = '';
    $.asWaitingView = '';
    $.asGlobalMessageBox = null;
    $.asAppName = '';
    $.asTheme = '';
    $.asThemeName = 'white';
    $.asPageTheme = '';
    $.asLoginAccessToken = 'loginAccessToken'
    $.asDebugId = 'debugid'
    $.asUserName = 'userName'
    $._asNumberOfPixcelBeforeStartMoveFixedNav = 30

    $.asBrowserLastState = {}
    $.asPageClass = "asPage"
    $._asPageId = "asPageId"
    $.asUnLinkClasses = []
    $.asPage = {
        ids: {}
    }
    $.asLanguageAndCulture = {}
    $.asStorage = {
        getItem: function (key) {
            return localStorage.getItem(key);
        },
        setItem: function (key, value) {
            localStorage.setItem(key, value);
        },
        getJson: function (key) {

            return JSON.parse(localStorage.getItem(key));
        },
        setJson: function (key, value) {
            localStorage.setItem(key, JSON.stringify(value));
        },
        removeItem: function (key) {
            localStorage.removeItem(key)
        }
    }

    $.asIsAuthenticated = function () {
        var token = $.asStorage.getItem($.asLoginAccessToken);
        if (token)
            return true
        return false
    }
    $.asLogOff = function (url, loginTimeOut) {

        if (url !== null)
            if (url[0] !== "/")
                url = "/" + url;
        function logOff() {
            return $.asAjaxTask({
                url: $.asAccountManager.logOffUrl
            });
        }

        function claerCurrentUserSession() {
            $.asStorage.removeItem($.asLoginAccessToken)
            $.asStorage.removeItem($.asUserName)
            $.asDebugMode(false)
            $.asCookies.set($.asCoockiesName.asIsAuthenticated, { IsAuthenticated: false }, { expires: 10000000 })
        }
        if (loginTimeOut === true) {
            $.asAccountManager.loginInProcess = false
            claerCurrentUserSession()
            //var loginUrl = $.asLang + $.asAccountManager.loginUrl
            var loginUrl = $.asAccountManager.loginUrl
            var pageUrl = $.asInitService($.asReloadUrl, [{ name: "@url", value: loginUrl }])
            var state = History.getState()
            if (state.data.pureUrl !== loginUrl) {


                if (typeof (state.data.pureUrl) != "undefined")
                    $.asAccountManager.returnUrl = decodeURIComponent(state.data.pureUrl)
                $.asAccountManager.returnQueryString = $.asGetQueryString()
            }


            if ($.asAccountManager.returnQueryString === null)
                $.asAccountManager.returnQueryString = state.data.queryString
            History.pushState({ pageUrl: pageUrl, placeHolder: 'body', pureUrl: $.asLang + loginUrl }, '/' + $.asLang + loginUrl, '/' + loginUrl)
          
            //$.asShowMessage({ template: "error", message: 'نیاز به ورود مجدد' });
        } else {
            var $pageSelector = $("body");
            $pageSelector.selector = "body";
            $pageSelector.asAfterTasks([
           logOff()
            ], function (success) {
                if (success === true) {

                    claerCurrentUserSession()
                    var pageUrl = $.asInitService($.asReloadUrl, [{ name: "@url", value: url + $.asUrlDelimeter }])

                    //var state = History.getState();
                    //if (state.data.pageUrl === pageUrl) {
                    //    $._asLoadPage(state.data.pageUrl)
                    //}
                    //$('body').addClass('as-loading')
                    History.pushState({ pageUrl: pageUrl, placeHolder: 'body', pureUrl: url + $.asUrlDelimeter }, url, url)
                    $($.asEvent.global).trigger($.asEvent.global.logoff, []);
                   
                }
            }, { removeOverlay: false })
        }



    }

    $.asAccountManager = {
        mainHandler: function () {
         
            if ($.asAccountManager.returnUrl !== $.asUrlDelimeter) {
                var reg = new RegExp($.asUrlDelimeter, "gi");
                var returnUrlIsByQuer=false;

                var url = []
                var qSign = $.asQueryStringSign.replace(new RegExp($.asQueryStringSign, "gi"), $.asQueryStringShadowSign).toLowerCase()
               
                if ($.asAccountManager.returnUrl.toLowerCase().indexOf(qSign.toLowerCase()) > -1) {

                    url = $.asAccountManager.returnUrl.split(qSign)
                }else if($.asAccountManager.returnUrl.toLowerCase().indexOf($.asQueryStringSign.toLowerCase()) > -1){
                     url = $.asAccountManager.returnUrl.split($.asQueryStringSign)
                     returnUrlIsByQuer=true
                }
              
                //url = url.substring(url.indexOf(qSign))
                //console.log(url)
                //console.log($.asAccountManager.returnUrl)

                if (url.length === 2) {
                  if(returnUrlIsByQuer){
                              History.pushState({
                            pageUrl: $.asInitService($.asReloadUrl, [{ name: "@url", value: $.asAccountManager.returnUrl}]),
                            placeHolder: 'body',
                            pureUrl: url[0]
                        },
                            $.asAccountManager.returnUrl,
                            $.asAccountManager.returnUrl)
                  }else{
                        var query = ($.asAccountManager.returnQueryString !== null && typeof ($.asAccountManager.returnQueryString) != "undefined")
                            ? $.asQueryStringSign + $.asAccountManager.returnQueryString : ""
                            
                        History.pushState({
                            pageUrl: $.asInitService($.asReloadUrl, [{ name: "@url", value: url[0] + query }]),
                            placeHolder: 'body',
                            pureUrl: url[0]
                        },
                            url[0].replace(reg, "/") + query,
                            url[0].replace(reg, "/") + query)
                  }

                } else if ($.asAccountManager.returnQueryString !== null && typeof ($.asAccountManager.returnQueryString) != "undefined") {

                    
                    History.pushState({
                        pageUrl: $.asInitService($.asReloadUrl, [{ name: "@url", value: $.asAccountManager.returnUrl + $.asQueryStringSign + $.asAccountManager.returnQueryString }]),
                        placeHolder: 'body',
                        pureUrl: $.asAccountManager.returnUrl
                    },
                        $.asAccountManager.returnUrl.replace(reg, "/") + $.asQueryStringSign + $.asAccountManager.returnQueryString,
                        $.asAccountManager.returnUrl.replace(reg, "/") + $.asQueryStringSign + $.asAccountManager.returnQueryString)
                } else {

                    
                    if($.asAccountManager.returnUrl.toLowerCase().indexOf($.asAccountManager.loginUrl.replace(/\//g, $.asUrlDelimeter).toLowerCase()) >-1 || 
                    $.asAccountManager.returnUrl.toLowerCase().indexOf($.asAccountManager.loginUrl.toLowerCase()) >-1){
                        
                                        History.pushState({
                    pageUrl: $.asInitService($.asReloadUrl, [{ name: "@url", value: $.asAccountManager.url.replace(/\//g, $.asUrlDelimeter) }]),
                    placeHolder: 'body',
                    pureUrl: $.asAccountManager.url
                },
                    $.asAccountManager.url,
                    $.asAccountManager.url);
                    }else{
                        History.pushState({
                        pageUrl: $.asInitService($.asReloadUrl, [{ name: "@url", value: $.asAccountManager.returnUrl }]),
                        placeHolder: 'body',
                        pureUrl: $.asAccountManager.returnUrl
                    },
                        $.asAccountManager.returnUrl.replace(reg, "/"),
                        $.asAccountManager.returnUrl.replace(reg, "/"))
                    }
                    
                }
            } else {
                History.pushState({
                    pageUrl: $.asInitService($.asReloadUrl, [{ name: "@url", value: $.asAccountManager.url.replace(/\//g, $.asUrlDelimeter) }]),
                    placeHolder: 'body',
                    pureUrl: $.asAccountManager.url
                },
                    $.asAccountManager.url,
                    $.asAccountManager.url);
            } //var state = History.getState();
            //if (state.data.pageUrl === pageUrl) {
            //    $._asLoadPage(state.data.pageUrl)
            //}


            //    var pageUrl = "/" + $.asFormUrl + "/" + url
            //    //var pageUrl = "/" + "cms/GetWebPage" + "/" + url
            //    var state = History.getState();
            //    if (state.data.pageUrl === pageUrl) {
            //        $._asLoadPage(state.data.pageUrl)
            //    }
            //    else if (changeUrl === true)
            //        History.pushState({ pageUrl: pageUrl }, location, location)
            //    else
            //        History.pushState({ pageUrl: pageUrl }, location)

            //$($.asPlaceHolder).asAjax({
            //    //url: $.asFormHtmlPath + $(this).data('as-url') + ".html",
            //    url: url,
            //    type: "get",
            //    success: function (data) {
            //        $(document).prop('title', data.title);
            //        $($.asPlaceHolder).html(data.page);
            //    }
            //}); 

        },
        otherHandler: function () {

        },
        setToken:function(username,token){
            $.asCookies.set($.asCoockiesName.asIsAuthenticated, { IsAuthenticated: true }, { expires: 10000000 })
            $.asStorage.setItem($.asLoginAccessToken, token)
            $.asStorage.setItem($.asUserName, username)
            
            if (token) {
                $.connection.hub.stop();
                $.connection.hub.qs={'BearerToken':token}
                $.connection.hub.start();
            }
        },
        //eventLogin: 'LoginSuccessed',
        //eventLogOff: 'LogOffSuccessed',
        url: '',
        returnUrl: $.asUrlDelimeter,
        isAuthenticatedUrl: "/Defaults/IsAuthenticated",
        returnQueryString: null,
        loginUrl: '/login',
        logOffUrl: '/Defaults/logOff',
        logOffSign: 'LogOff',
        loginInProcess: false
    }

    //$.asLoaderCssJs = {
    //    currentStyle: [],
    //    currentScript: [],
    //    loadingCssJs: []
    //    //callBack:[]
    //}
    //var msgId = "globalAsMrssagebox"

    $.asInitService = function (serviceUrl, params) {
        $.asEach(params, function (parameter) {

            serviceUrl = serviceUrl.replace(new RegExp(parameter.name, "gi"), (encodeURIComponent(parameter.value)).replace(new RegExp("%25", "gi"), "%20"));
        });
        return serviceUrl;
    }
    //$.asGetWebpageDependentModuleInRunTime = function(designTimeModuleArray,pageParams)
    //{
    //    var urls = []
    //    if (typeof (pageParams.loadScriptAndStyle) != "undefined")
    //        urls = pageParams.loadScriptAndStyle.urls || [];
    //    return designTimeModuleArray.concat(urls)
    //}
    
        $.asModalManager = {
        modals:{},
       urls:{
        login:"/login/"
       },
       get:function(params){
            var defaultsParams = $.extend({ isGlobal: false, id:$.asuniqueId() }, params)
            var modal = $.asModalManager.modals[defaultsParams.url]
            if(modal)
            return modal
            if ($("#divGlobalModal").length === 0) {
                    $("body").append("<div style='display:none' id='divGlobalModal'></div>"  );
            }
            if ($("#divLocalModal").length === 0) {
                    $($.asPlaceHolder).append("<div style='display:none' id='divLocalModal'></div>"  );
            }
            if(defaultsParams.isGlobal){
                    $("#divGlobalModal").append("<div id='" + defaultsParams.id  + "'></div>"  );
            } else{
                     $("#divLocalModal").append("<div id='" + defaultsParams.id  + "'></div>"  );
                }
            $.asModalManager.modals[defaultsParams.url]=$("#"+defaultsParams.id)
            return $.asModalManager.modals[defaultsParams.url];
       }
       
    }
    $.asGetWebpageDependentModuleInRunTime = function (designTimeModuleArray, pageParams) {

        if (designTimeModuleArray.indexOf('[{"url":"') === 0)
            designTimeModuleArray = eval("(" + designTimeModuleArray + ")")
        else if ($.isArray(designTimeModuleArray) === false)
            throw "designTimeModuleArray not Array"
        if (typeof (pageParams) == "string") {
            if (pageParams.indexOf('{') === 0) {
                pageParams = eval("(" + pageParams + ")")
            }
        } else if (typeof (pageParams) != "object")
            throw "pageParams not Object";


        var urls = []
        //var designTimeModuleArray = JSON.parse({ designTimeModuleArray: designTimeModule })
        if (typeof (pageParams.loadScriptAndStyle) != "undefined")
            urls = pageParams.loadScriptAndStyle.urls || [];
        //var temp = designTimeModuleArray.concat(urls)
        //console.dir(temp)
        return designTimeModuleArray.concat(urls)
    }
    $.asSetWebPageData = function (params) {
        var defaultsParam = $.extend({ isModal: false, isTemplate: false, replaceHtml: true }, params);
        var currentPageId = defaultsParam.$holder.data($._asPageId)
        var currentPageEvent = "#" + currentPageId
        //if (defaultsParam.isTemplate === true)
        //    $($.asEvent.template).off()
        //else

        $($.asEvent.page).off()
        //dispose call
        $("." + currentPageId + "." + $.asPageClass).data('dataItems', [])
        $(currentPageEvent).trigger($.asEvent.page.dispose, [])
   
        //if (defaultsParam.isModal === false)
        $(currentPageEvent).off()
        delete $.asPage.ids[currentPageId]

        if (defaultsParam.$holder.selector === "body") {
           $.asCallDisposeEventOfAllPage()
        }
        if (defaultsParam.replaceHtml === true) {
            defaultsParam.$holder.removeClass(defaultsParam.$holder.data($._asPageId) + " " + $.asPageClass)
            defaultsParam.$holder.empty()
        }
        defaultsParam.$holder.data($._asPageId, defaultsParam.data.pageId)
        defaultsParam.$holder.addClass(defaultsParam.data.pageId + " " + $.asPageClass)
        if (defaultsParam.replaceHtml === true)
            defaultsParam.$holder.html(defaultsParam.data.html);
        $.asBindPageEvent(defaultsParam.data.pageId)
        $.asPage.ids[defaultsParam.data.pageId] = defaultsParam.data.pageId
    }
    $.asFilter = function (a, f) {
        var o = [];

        $.asEach(a, function (v, index) {
            if (!f || f(v, index, a)) {
                o.push(v);
            }
        });

        return o;
    }

    $.asEach = function (o, cb, s) {
        var n, l;

        if (!o) {
            return 0;
        }

        s = s || o;

        if (o.length !== undefined) {
            // Indexed arrays, needed for Safari
            for (n = 0, l = o.length; n < l; n++) {
                if (cb.call(s, o[n], n, o) === false) {
                    return 0;
                }
            }
        } else {
            // Hashtables
            for (n in o) {
                if (o.hasOwnProperty(n)) {
                    if (cb.call(s, o[n], n, o) === false) {
                        return 0;
                    }
                }
            }
        }

        return 1;
    }

    $.asAddCacheSuffix = function (url) {
        var cacheSuffix = $.asCacheSuffix;

        if (cacheSuffix) {
            url += (url.indexOf('?') === -1 ? '?' : '&') + cacheSuffix;
        }

        return url;
    }

    $.asFindItemInJsonArray = function (json, searchKey, itemKey, value) {
        var item = $.asFindInJsonArray(json, searchKey, value)
        if (item != null)
            return $.asGetPropertybyName(item, itemKey);
        //return item[itemKey];
        return null;
    };
    $.asFindInJsonArray = function (json, searchKey, value) {
        for (var i = 0; json.length > i; i += 1) {
            if ($.asGetPropertybyName(json[i], searchKey) === value) {

                return json[i];
            }
            //if (json[i][searchKey] === value) {

            //    return json[i];
            //}
        }
        return null;
    };
    $.asNotFound = function (name) {
        $.asShowMessage({ template: "error", message: name + $.asRes[$.asLang].notFound })
    }
    //var asConsole = 
    $.asConsole = (function () {
        return {
            log: function (text) {
                if (typeof console !== "undefined" && console.log)
                    console.log(text)
            },
            error: function (err) {
                if (typeof console !== "undefined" && console.error)
                    console.error(err)
            },
            dir: function (data) {
                if (typeof console !== "undefined" && console.dir)
                    console.dir(data)
            }
        }
    })()

    //$.asFindItemInJsonArray = function (json, searchKey, itemKey, value) {
    //    var item = $.asFindInJsonArray(json, searchKey, value)
    //    if (item != null)
    //        return item[itemKey];
    //    return null;
    //};
    //$.asFindInJsonArray = function (json, searchKey, value) {
    //    for (var i = 0; json.length > i; i += 1) {
    //        if (json[i][searchKey] === value) {

    //            return json[i];
    //        }
    //    }
    //    return null;
    //};

    $.asObjectToArray = function (object) {
        return $.map(object, function (el) { return el })
    }
    $.asReturnNotNull = function (first, second) {
        if (first) return first;
        return second;
    };

    //$.asValidation = {
    //    requirText: function (cell, value) {
    //        if (value.toString().trim().length < 1) {
    //            return { message: $.asRes[$.asLang].requireError, result: false };
    //        }
    //        return true;
    //    }
    //};

    //$.asShowMessageBox = function (message, onOk) {
    //    $.asGlobalMessageBox = $("#" + msgId).asMessageBox({}, message, onOk);

    //    $($.asGlobalMessageBox).asMessageBox('open', message, onOk);
    //};
    $.asShowMessage = function (params) {
        var defaultsParam = $.extend({ template: "success", userVisible: true, logable: false,showTime:null }, params);
        //if (defaultsParam.resMessage)
        //    $($.asEvent.global).trigger(defaultsParam.template, [$.asRes[$.asLang][defaultsParam.resMessage], defaultsParam.userVisible, defaultsParam.errorMesage, defaultsParam.logable]);
        //else
        $($.asEvent.global).trigger(defaultsParam.template, [defaultsParam.message, defaultsParam.userVisible, defaultsParam.errorMesage, defaultsParam.logable,defaultsParam.showTime]);
    };
    $.asSort = function (params) {
        if (params.datatype != 'array') {
            if (params.hierarchy) {

                if (params.hierarchy.type === "tree") {

                    $.asSortRecursive(params)
                } else if (params.hierarchy.type === "flat") {
                    params.array = $.asSortHierarhy({ array: params.array, order: params.order })
                }
            } else {
                if (params.order === 'asc') {
                    params.array.sort(function (a, b) {
                        //var aVal = a[params.orderby]
                        //var bVal = b[params.orderby]

                        var aVal = $.asGetPropertybyName(a, params.orderby)
                        var bVal = $.asGetPropertybyName(b, params.orderby)
                        return (aVal == bVal) ? 0 : (aVal > bVal) ? 1 : -1;
                    })
                } else {
                    params.array.sort(function (a, b) {
                        var aVal = $.asGetPropertybyName(a, params.orderby)
                        var bVal = $.asGetPropertybyName(b, params.orderby)
                        return (aVal == bVal) ? 0 : (aVal > bVal) ? -1 : 1;
                    })
                }
            }
        } else {
            if (params.order === 'asc') {
                params.array.sort(function (a, b) { return (a == b) ? 0 : (a > b) ? 1 : -1; });
            } else {
                params.array.sort(function (a, b) { return (a == b) ? 0 : (a > b) ? -1 : 1; });
            }
        }
    }

    $.asSortRecursive = function (params) {
        if (params.order === 'asc') {
            params.array.sort(function (a, b) {
                var aVal = $.asGetPropertybyName(a, params.orderby)
                var bVal = $.asGetPropertybyName(b, params.orderby)
                return (aVal == bVal) ? 0 : (aVal > bVal) ? 1 : -1;
            })
        } else {
            params.array.sort(function (a, b) {
                var aVal = $.asGetPropertybyName(a, params.orderby)
                var bVal = $.asGetPropertybyName(b, params.orderby)
                return (aVal == bVal) ? 0 : (aVal > bVal) ? -1 : 1;
            })
        }

        $.each(params.array, function (index, value) {
            $.each(params.array[index], function (key, val) {
                if ($.isArray(val) === true) {
                    $.asSortRecursive({ array: val, orderby: params.orderby, order: params.order })
                }
            })
        })
    }

    //sort flat hierachy data
    $.asSortHierarhy = function (params) {
        var defaultsParams = $.extend({ key: 0, isHashArray: false, order: 'asc', result: [] }, params)
        var hashArray = {};
        if (defaultsParams.isHashArray === false) {
            for (var i = 0; i < defaultsParams.array.length; i++) {
                if (hashArray[defaultsParams.array[i].parent] == undefined) hashArray[defaultsParams.array[i].parent] = [];
                hashArray[defaultsParams.array[i].parent].push(defaultsParams.array[i]);
            }
        } else
            hashArray = defaultsParams.array

        if (hashArray[defaultsParams.key] == undefined) return;
        //var arr = hashArray[key].sort(hierarchySortFunc);
        var arr
        if (defaultsParams.order === 'asc') {
            arr = hashArray[defaultsParams.key].sort(function (a, b) { return (a == b) ? 0 : (a > b) ? 1 : -1; });
        } else {
            arr = hashArray[defaultsParams.key].sort(function (a, b) { return (a == b) ? 0 : (a > b) ? -1 : 1; });
        }

        for (var i = 0; i < arr.length; i++) {
            defaultsParams.result.push(arr[i]);
            $.asSortHierarhy(hashArray, arr[i]._id, true, defaultsParams.order, defaultsParams.result);
        }

        return defaultsParams.result;
    }

    $.asGetPropertybyName = function (object, fullPropertyName) {
        fullPropertyName = fullPropertyName.replace(/\[(\w+)\]/g, '.$1'); // convert indexes to properties
        fullPropertyName = fullPropertyName.replace(/^\./, ''); // strip a leading dot
        var a = fullPropertyName.split('.');
        for (var i = 0, n = a.length; i < n; ++i) {
            var k = a[i];
            if (k in object) {
                object = object[k];
            } else {
                return;
            }
        }
        return object;
    }

    //function getDescendantProp(obj, desc) {
    //    var arr = desc.split(".");
    //    while (arr.length && (obj = obj[arr.shift()]));
    //    return obj;
    //}


    $.asSetPropertyByName = function (obj, path, value) {
        var schema = obj;  // a moving reference to internal objects within obj
        var pList = path.split('.');
        var len = pList.length;
        for (var i = 0; i < len - 1; i++) {
            var elem = pList[i];
            if (!schema[elem]) schema[elem] = {}
            schema = schema[elem];
        }

        schema[pList[len - 1]] = value;
    }

    $.asParseTree = function (params) {
        //try{
        var defaultsParams = $.extend({ keyDataField: { name: 'Id' }, parentDataField: { name: 'ParentId' }, isLeafDataField: 'IsLeaf' }, params)
        var treeList = [];

        defaultsParams.list.forEach(function (node) {
            if ($.asGetPropertybyName(node, defaultsParams.isLeafDataField) === null) {
                // $.asConsole.error(node["[MasterDataKeyValue][IsLeaf]"])
                // $.asConsole.error(defaultsParams)
                throw 'Tree Data Not Have IsLeaf Property'
            } else {
                var currentNode = node;
                if ($.asGetPropertybyName(node, defaultsParams.isLeafDataField) === true) {
                    //$._asBuildTree(node, treeList, defaultsParams)


                    do {

                        if ($.asFindItemInJsonArray(treeList,
                                defaultsParams.keyDataField.name,
                                defaultsParams.keyDataField.name,
                            $.asGetPropertybyName(currentNode, defaultsParams.keyDataField.name)
                            //currentNode[defaultsParams.keyDataField.name]
                            ) ==
                            null)
                            //if (!newTree.Exists(t => t.Id == currentNode.Id))
                            treeList.push(currentNode);
                        else
                            break;

                        //if (!newTree.Exists(t => t.Id == currentNode.Id))
                        //    newTree.Add(currentNode);
                        //else
                        //    break;
                        var previousNode = currentNode;
                        //if(currentNode.ParentId != null)
                        //if (currentNode[defaultsParams.parentDataField.name] != null)
                        if ($.asGetPropertybyName(currentNode, defaultsParams.parentDataField.name) != null)
                            currentNode = $
                                .asFindInJsonArray(defaultsParams.list,
                                    defaultsParams.keyDataField.name,
                                    //currentNode[defaultsParams.parentDataField.name]
                                    $.asGetPropertybyName(currentNode, defaultsParams.parentDataField.name)
                                    )
                        //currentNode = baseTree.Find(t => t.Id == currentNode.ParentId);
                        if (currentNode == null) {
                            $.asSetPropertyByName(previousNode, defaultsParams.parentDataField.name, null)
                            //previousNode[defaultsParams.parentDataField.name] = null;
                            break;
                        }
                        //if (currentNode[defaultsParams.parentDataField.name] != null) continue;
                        if ($.asGetPropertybyName(currentNode, defaultsParams.parentDataField.name) != null) continue;
                        {
                            if ($.asFindItemInJsonArray(treeList,
                                    defaultsParams.keyDataField.name,
                                    defaultsParams.keyDataField.name,
                                //currentNode[defaultsParams.keyDataField.name]
                                  $.asGetPropertybyName(currentNode, defaultsParams.keyDataField.name)
                                ) ==
                                null)
                                //if (!newTree.Exists(t => t.Id == currentNode.Id))
                                //newTree.Add(currentNode);
                                treeList.push(currentNode);
                        }
                        //} while (currentNode[defaultsParams.parentDataField.name] != null);
                    } while ($.asGetPropertybyName(currentNode, defaultsParams.parentDataField.name) != null);

                }
                //if (node[defaultsParams.parentDataField.name] == null) {
                //    treeList.push(currentNode);
                //}
            }

        });
        //console.dir(treeList)
        return treeList;
        //} catch (err) {
        //    console.error(err)
        //}
    }

    $.asTreeify = function (params) {
        var defaultsParams = $.extend({ keyDataField: { name: 'id' }, parentDataField: { name: 'parentId' }, childrenDataField: 'children', removeChildLessParent: false }, params)
        if (defaultsParams.removeChildLessParent === true) {
            defaultsParams.list = $.asParseTree(defaultsParams);
        }
       
        var treeList = [];
        var lookup = {};
        defaultsParams.list.forEach(function (obj) {
            lookup[$.asGetPropertybyName(obj, defaultsParams.keyDataField.name)] = obj;
            obj[defaultsParams.childrenDataField] = [];
        });
        //console.dir(defaultsParams.list)
        defaultsParams.list.forEach(function (obj) {
            if ($.asGetPropertybyName(obj, defaultsParams.parentDataField.name) != null) {
                if (typeof (lookup[$.asGetPropertybyName(obj, defaultsParams.parentDataField.name)]) === 'undefined')
                    treeList.push(obj);
                else
                    lookup[$
                        .asGetPropertybyName(obj, defaultsParams.parentDataField.name)][defaultsParams.childrenDataField
                    ].push(obj);
            } else {
                treeList.push(obj);
            }
        });
        return treeList;
    };

    $.asNullizeChildrenOfLenegthOfZero = function (list, childrenName) {
        list.forEach(function (obj) {
            if (typeof ($.asGetPropertybyName(obj, childrenName)) != 'undefined')
                if ($.asGetPropertybyName(obj, childrenName) != null)
                    if (($.asGetPropertybyName(obj, childrenName)).length > 0)
                        $.asNullizeChildrenOfLenegthOfZero($.asGetPropertybyName(obj, childrenName), childrenName)
                    else {
                        $.asSetPropertyByName(obj, childrenName, null);
                        //obj[$.asGetPropertybyName(obj, childrenName)] = null
                    }
        });
    }


    /**
     * Returns a unique id. This can be useful when generating elements on the fly.
     * This method will not check if the element already exists.
     *
     * @method uniqueId
     * @param {String} prefix Optional prefix to add in front of all ids - defaults to "as_".
     * @return {String} Unique id.
     */
    $.asuniqueId = function (prefix) {
        return (!prefix ? 'as_' : prefix) + ($.asCounter++);
    }

    $.asPageLoadComplete = function (params, pageId) {
        if (typeof (params) == "string")
            params = eval("(" + params + ")")
        var defaultParams = $.extend({
            //lazyLoadSelector: 'img'

        }, params);
        var as = function (id) {
            return typeof (pageId) != "undefined" ? $(pageId).find(id) : $(id);
        }
        //$.Khodkar.res = $.asRes[$.asLang];
        as('[data-toggle="popover"]').popover();
        $(window).lazyLoadXT();
        //var defaultsParam = $.extend(params, {showMessage:false});
        if ($.fn.asValidate) {
            as("form").each(function () {
                jQuery.validator.setDefaults({
                    errorElement: "span",
                    errorPlacement: function (error, element) {

                        // Add the `help-block` class to the error element
                        //error.addClass("help-block");
                        //$(element).parents().addClass("as-erroe-val");


                        if (element.prop("type") === "checkbox") {
                            error.insertAfter(element.parent("label"));
                        } else {
                            error.insertAfter(element);
                        }
                        error.insertAfter($(element).parent());

                        //        $(element)
                        //.closest("form")
                        //    .find("label[for='" + element.attr("id") + "']")
                        //        .append(error);

                    },
                    highlight: function (element, errorClass, validClass) {

                        $(element).parents(".form-group").addClass("has-error").removeClass("has-success");

                    },
                    unhighlight: function (element, errorClass, validClass) {
                        $(element).parents(".form-group").addClass("has-success").removeClass("has-error");

                    }
                });
            });
        }
        as("[data-localize]").asLocalize();

        //if (typeof defaultParams.formName !== 'undefined' && $.asShowFormMessage === true)
        //    $.asShowMessage({ message: $.asRes[$.asLang].form + " " + defaultParams.formName + " " + $.asRes[$.asLang].loadComplete });
        //$.asShowMessage({ message: $.asRes[$.asLang].form + " " + $.asRes[$.asLang][formName] + " " + $.asRes[$.asLang].loadComplete });
    };
    $.asToThemeClass = function (className, type) {
        if (type === '3rd') {
            if ($.asTheme != '')
                return className + " " + className + "-" + $.asTheme;
            else
                return className;
        } else {
            return className + " " + className + "-" + $.asThemeName;
        }
    }
    $.asSetTheme = function (themeName) {
        $.asThemeName = themeName;
        if (themeName === 'sky') {
            $.asTheme = 'ui-redmond';
            $.asPageTheme = 'ui-start';
        } else if (themeName === 'blue') {
            $.asTheme = 'ui-start';
            $.asPageTheme = 'ui-start';
        } else if (themeName === 'violet') {
            $.asTheme = 'ui-redmond';
            $.asPageTheme = 'ui-redmond';
        } else if (themeName === 'public') {
            $.asTheme = '';
            $.asPageTheme = '';
        } else if (themeName === 'white') {
            $.asTheme = 'bootstrap';
            $.asPageTheme = 'bootstrap';
        } else if (themeName === 'black') {
            $.asTheme = 'shinyblack';
            $.asPageTheme = 'shinyblack';
        }
        $.jqx.theme = $.asTheme;
    }
    $._asGetHeaders = function (customeHeaders) {
        var headers = {};

        var token = $.asStorage.getItem($.asLoginAccessToken);

        if (token) {

            headers.Authorization = 'Bearer ' + token;

        }

        headers = $.asSetLanguageAndCulterForCurrentRequest(headers)
        headers.isDebugMode = $.asDebugMode()
        headers.isMobileMode =  $.asStorage.getItem($.asCoockiesName.asIsMobileMode).IsMobileMode;
        var isAuth = $.asCookies.getJSON($.asCoockiesName.asIsAuthenticated)
        if (typeof (isAuth) === "undefined")
            headers.isAuthenticated =  false;
            else
            headers.isAuthenticated =  isAuth.IsAuthenticated;
        return $.extend(headers, customeHeaders)
    }
    //#region error Handler
    $.asAjaxError = function (jqXHR, textStatus, errorThrown) {
        $.asConsole.error(jqXHR)
        if (jqXHR.status === 401) {
            if ($.asAccountManager.loginInProcess === false) {
                $.asAccountManager.loginInProcess = true


                setTimeout(function () {
                    $.ajax({
                        url: $.asAccountManager.isAuthenticatedUrl,
                        type: "get",
                        success: function (data) {
                            $.asAccountManager.loginInProcess = false
                            $.asShowMessage({ template: "error", message: $.asRes[$.asLang].accessDeny });
                        },
                        error: function (result) {

                            if (result.status === 401) {
                                $.asLogOff(null, true)
                            }

                        }
                    }, { overLay: false })
                }, 1000);
            }
        }else  if (jqXHR.status === 404) {
            $.asNotFound( $.asRes[$.asLang].url);
            }else if (jqXHR.responseText != null) {
            var respnse = jqXHR.responseText.replace(/\s/g, '');
            if (respnse.length > 0) {
                var error = { ExceptionMessage: null }
                try {
                    error = JSON.parse(jqXHR.responseText);
                }
                catch (err) {
                    error.ExceptionMessage = jqXHR.responseText
                }
                if (error.ExceptionMessage != null) {
                    if (error.ExceptionMessage.indexOf("asError") >= 0) {
                        try {
                            var errorMessage = JSON.parse(error.ExceptionMessage);
                            $.asShowMessage({ template: "error", message: errorMessage.asError });
                        }
                        catch (err) {
                            $.asConsole.error(err)
                            var i = error.ExceptionMessage.indexOf('asError')
                            if (i > 0)
                                $.asShowMessage({ template: "error", message: error.ExceptionMessage.substring(i + 10) });
                            else
                                $.asShowMessage({ template: "error", message: error.ExceptionMessage });
                        }
                    } else{
                         $.asConsole.error(error)
                        //$.asShowMessage({ template: "error", message: error.ExceptionMessage });
                    }
                       
                } else {
                    if(error.asError != null)
                    $.asShowMessage({ template: "error", message: error.asError });
                    else
                    $.asShowMessage({ template: "error", message: $.asRes[$.asLang].ajaxError + " " + errorThrown });
                }
            } else
                $.asShowMessage({ template: "error", message: $.asRes[$.asLang].ajaxError + " " + errorThrown });
        } else {
            $.asShowMessage({ template: "error", message: $.asRes[$.asLang].ajaxError });
        }
    }
    //#endregion 

    $.asInitApp = function (params) {
        //Begin bootStrap For IE 10
        if (navigator.userAgent.match(/IEMobile\/10\.0/)) {
            var msViewportStyle = document.createElement('style');
            msViewportStyle.appendChild(
                document.createTextNode(
                    '@-ms-viewport{width:auto!important}'
                )
            );
            document.querySelector('head').appendChild(msViewportStyle);
        };
        //End bootStrap For IE 10
        var defaultsParam = $.extend({
            lang: "fa",
            culture: "IR",
            country: "ایران",
            rightToLeft: true,
            version:1,
            showUncaughtError: false,
            frameWorkAndTemplatePattern:{},
            isAuthenticatedUrl: "/Defaults/IsAuthenticated",
            loginServiceUrl: '/Token',
            loginUrl: '/login',
            logOffUrl: '/Defaults/logOff',
            logOffSign: 'LogOff',
            debugIdSign:'debugid',
            formUrl: "/Defaults/GetWebPage",
            reloadUrl: "/Defaults/Reload",
            changeTemplateUrl: "/Defaults/ChangeTemplate",
            placeHolder: "#placeHolder",
            baceStyleUrl: "/Content/dist/",
            baceScriptUrl: "/Scripts/dist/",
            debugBaceStyleUrl: "/Content/debug/",
            debugBaceScriptUrl: "/Scripts/debug/",
            loginSuccessedUrl: "/home",
            thumbnailPath: "/files/thumbnail",
            asPageClass: "asPage",
            urlDelimeter: "@",
            queryStringSign: "/q/",
            mobileSign: "/mobile/",
            showFormMessage: true,
            languageAndCultureCoockie: 'asLanguageAndCulture',
            isMobileModeCoockie: 'asIsMobileMode',
            isDebugModeCoockie: 'asIsDebugMode',
            isAuthenticatedCoockie:'asIsAuthenticated',
            javaScriptPolyFillsFolder:"polyfills",
            javaScriptLibraryFolder:"libs",
            modalUrls: {},
            unLinkClasses: []
        }, params);
        
         
          $($.asEvent.global).on($.asEvent.global.fontLoad,
                defaultsParam.onFontLoad)
            
         $($.asEvent.global).on($.asEvent.global.fontLoaded,function(){
                     setTimeout(defaultsParam.onFontLoaded, 2000);
         })
        
        $.asModalManager.urls = $.extend(defaultsParam.modalUrls, $.asModalManager.urls)
        
      
        
        $.asCoockiesName = {
            asLanguageAndCulture: defaultsParam.languageAndCultureCoockie,
            asIsDebugMode: defaultsParam.isDebugModeCoockie,
            asIsMobileMode: defaultsParam.isMobileModeCoockie,
            asIsAuthenticated:defaultsParam.isAuthenticatedCoockie
        }
        
         $.asCookies.set($.asCoockiesName.asIsMobileMode, { IsMobileMode: false }, { expires: 10000000 })
        $.asStorage.setJson($.asCoockiesName.asIsMobileMode, {
         IsMobileMode: false
      })
      
        $.asCacheSuffix = "version=" + defaultsParam.version
        $.asDebugIdSign =defaultsParam.debugIdSign
        $.asDebugId = $.asDebugIdSign
        $.asUrlDelimeter = defaultsParam.urlDelimeter
        $.asQueryStringSign = defaultsParam.queryStringSign
        $.asQueryStringShadowSign = $.asQueryStringSign.replace(new RegExp("/", "gi"), $.asUrlDelimeter)
        $.asMobileSign = defaultsParam.mobileSign
        $.asMobileShadowSign = $.asMobileSign.replace(new RegExp("/", "gi"), $.asUrlDelimeter)
        $.asFrameWorkAndTemplatePattern = defaultsParam.frameWorkAndTemplatePattern
        $.asUnLinkClasses = defaultsParam.unLinkClasses
        $.asPageClass = defaultsParam.asPageClass;
        $.asLang = defaultsParam.lang;
        $.asCulture = defaultsParam.culture
        $.asCountry = defaultsParam.country
        $.asRightToLeft = defaultsParam.rightToLeft

        $.asAccountManager.isAuthenticatedUrl = defaultsParam.isAuthenticatedUrl
        $.asAccountManager.loginUrl = defaultsParam.loginUrl
        $.asAccountManager.logOffUrl = defaultsParam.logOffUrl
        $.asAccountManager.logOffSign = defaultsParam.logOffSign
        $.asAccountManager.returnUrl = $.asUrlDelimeter
        $.asAccountManager.loginServiceUrl = defaultsParam.loginServiceUrl

        $.asPlaceHolder = defaultsParam.placeHolder
        $.asFormUrl = defaultsParam.formUrl
        $.asReloadUrl = defaultsParam.reloadUrl
        $.asChangeTemplateUrl = defaultsParam.changeTemplateUrl
        $.asArrayTabs = [];
        $.asBaceStyleUrl = defaultsParam.baceStyleUrl
        $.asBaceScriptUrl = defaultsParam.baceScriptUrl

        $.asDebugBaceStyleUrl = defaultsParam.debugBaceStyleUrl
        $.asDebugBaceScriptUrl = defaultsParam.debugBaceScriptUrl
        $.asAccountManager.url = "/" + $.asLang + defaultsParam.loginSuccessedUrl
        
        $.asJavaScriptPolyFillsFolder=defaultsParam.javaScriptPolyFillsFolder
        $.asJavaScriptLibraryFolder=defaultsParam.javaScriptLibraryFolder
        
        $.asThumbnailPath = defaultsParam.thumbnailPath
        $.asSetLanguageAndCulter($.asLang, $.asCulture, $.asCountry, $.asRightToLeft, true);
        $('body').addClass($.asPageClass)
        $.asBindEvent()



        $.ajaxSetup({
            error: $.asAjaxError,
            beforeSend: function (xhr, settings) {
                var header = xhr
                //settings.headers = $._asGetHeaders()
                $.each($._asGetHeaders(), function (k, v) {
                    header.setRequestHeader(k, v);
                });
                $($.asEvent.global).trigger($.asEvent.global.beforeSendAjaxRequest, [settings])
            },
            complete:function(){
                $($.asEvent.global).trigger($.asEvent.global.fontLoaded, []);
            }
        });

        if (defaultsParam.showUncaughtError) {
            window.onerror = function (message, filename, linenumber) {
                $.asShowMessage({ template: "error", message: "JS error: " + message + " on line " + linenumber + " for " + filename });
            }
        }

        if (defaultsParam.lang === "fa")
            $.asTopersian();
        $.asAppName = defaultsParam.appName;
        //msgId += $.asAppName;
        //$("<div />", { id: msgId, style: "display: none;" }).html("<div></div><div></div>").appendTo("body");
        var waitingMessage = $.asRes[$.asLang].waiting;

        //$.jqx.theme = defaultsParam.theme;

        //$.asCookies.set($.asCoockiesName.asLanguageAndCulture, { lang: $.asLang, culture: $.asCulture, country: $.asCountry, rightToLeft: $.asRightToLeft }, { expires: 10000000 })
        //console.dir($.asCookies.getJSON($.asCoockiesName.asLanguageAndCulture))

        $.asWaitingView =
            "<div class='text-center'><div class='as-waiting-view' title='" + waitingMessage + "'></div><div class='@name'></div></div>";
        $.asWaitingViewSmall =
            "<div class='text-center'><div class='as-waiting-view-small' title='" + waitingMessage + "'></div></div>";

        // Bind to StateChange Event
        History.Adapter.bind(window, 'statechange', function () { // Note: We are using statechange instead of popstate
       
            var state = History.getState(); // Note: We are using History.getState() instead of event.state
            if(state.data.pageUrl === undefined){
                var url =$.asUrlAsParameter(state.hash.substring(1))
                state.data.pureUrl=url
                
                 state.data.pageUrl = $.asInitService($.asFormUrl, [{name: "@url", value: url}, { name: "/@isModal", value: "" }])
            }
            //if ($.asBrowserLastState.data === state.data) return;
            //if (typeof ($.asBrowserLastState.data) !== "undefined")
            //    console.log("$.asBrowserLastState.data.queryString" + $.asBrowserLastState.data.queryString)
            //console.log("state.data.queryString " + state.data.queryString)
            //console.log("-----------")
            var lastStateUrl;
            var currentStateUrl;
            if (typeof (state.data.loadPage) != "undefined") {
                if (state.data.loadPage === true) {
                    if (typeof ($.asBrowserLastState.data) != "undefined") {
                        lastStateUrl = $.asBrowserLastState.data.pureUrl;
                        currentStateUrl = state.data.pureUrl;
                        lastStateUrl = lastStateUrl[0] === $.asUrlDelimeter ? lastStateUrl : $.asUrlDelimeter + lastStateUrl
                        currentStateUrl = currentStateUrl[0] === $.asUrlDelimeter ? currentStateUrl : $.asUrlDelimeter + currentStateUrl

                        //var lastPageUrl = $.asBrowserLastState.data.pageUrl.indexOf($.asUrlDelimeter) > -1 ? $.asBrowserLastState.data.pageUrl.replace($.asBrowserLastState.data.pageUrl.substring($.asBrowserLastState.data.pageUrl.indexOf($.asUrlDelimeter)), "") : $.asBrowserLastState.data.pageUrl;
                        //var currentPageUrl = state.data.pageUrl.indexOf($.asUrlDelimeter) > -1 ? state.data.pageUrl.replace(state.data.pageUrl.substring(state.datapageUrl.indexOf($.asUrlDelimeter)), "") : state.data.pageUrl;
                        if (lastStateUrl !== currentStateUrl || ($.asBrowserLastState.data.pageUrl.indexOf($.asInitService($.asReloadUrl, [{ name: "/@url", value: "" }])) > -1) || (typeof (state.data.queryString) == "undefined" && typeof ($.asBrowserLastState.data.queryString) != "undefined")) {
                            $._asLoadPage(state.data.pageUrl, state.data.placeHolder)
                        }
                    }
                    $.asBrowserLastState = state
                    var pageUrl = state.data.pureUrl.replace(new RegExp($.asUrlDelimeter, "gi"), "/")
                    if (pageUrl.toLowerCase().indexOf($.asQueryStringSign) > -1)
                        pageUrl = pageUrl.replace(pageUrl.substring(pageUrl.toLowerCase().indexOf($.asQueryStringSign)), "")
                    pageUrl = pageUrl[0] === "/" ? pageUrl : "/" + pageUrl
                    //else
                    //    pageUrl = state.data.pureUrl.replace(new RegExp($.asUrlDelimeter, "gi"), "/")
                    $($.asEvent.global).trigger($.asEvent.global.queryStringChange, [pageUrl, $.asGetQueryString()])
                    //$($.asEvent.page).trigger($.asEvent.page.dataLoading, [pageUrl])
                }
                else {
                    //History.pushState({ pureUrl: state.data.pureUrl, queryString: q, pageUrl: pageUrl + $.asQueryStringSign + q, loadPage: false }, q, "/" + url.replace(reg, "/") + $.asQueryStringSign + q)
                    if (typeof ($.asBrowserLastState.data) !== "undefined")
                        $.asBrowserLastState.data.loadPage = true

                    History.replaceState({ pureUrl: state.data.pureUrl, queryString: state.data.queryString, pageUrl: state.data.pageUrl, loadPage: true }, state.title, state.url, state.queue)

                }
            } else {
                if (typeof ($.asBrowserLastState.data) != "undefined") {
                    lastStateUrl = $.asBrowserLastState.data.pureUrl;
                    currentStateUrl = state.data.pureUrl;
                    lastStateUrl = lastStateUrl[0] === $.asUrlDelimeter ? lastStateUrl : $.asUrlDelimeter + lastStateUrl
                    currentStateUrl = currentStateUrl[0] === $.asUrlDelimeter ? currentStateUrl : $.asUrlDelimeter + currentStateUrl

                    if (lastStateUrl !== currentStateUrl || ($.asBrowserLastState.data.pageUrl.indexOf($.asInitService($.asReloadUrl, [{ name: "/@url", value: "" }])) > -1) || (typeof (state.data.queryString) == "undefined" && typeof ($.asBrowserLastState.data.queryString) != "undefined")) {
                        $.asBrowserLastState = state
                        
                        
                        var url = decodeURIComponent(state.data.pageUrl)
                         url = url.indexOf($.asQueryStringShadowSign) > -1 ?
                url.replace(url.substring(url.toLowerCase().indexOf($.asQueryStringShadowSign)), "") : url
                        
                        
                        
                        $._asLoadPage(url, state.data.placeHolder)
                    }
                } else {
                    $.asBrowserLastState = state
                    $._asLoadPage(state.data.pageUrl, state.data.placeHolder)
                }
            }

        });

        var setupPage = function () {

            $.asSetupPage(defaultsParam.templateParams, "body")

            if (typeof (defaultsParam.logOff) != "undefined")
                $.asLogOff(defaultsParam.logOff)
            else {
                if (typeof (defaultsParam.loadPage) != "undefined")
                    $.asLoadPage(defaultsParam.loadPage.location, defaultsParam.loadPage.url)
                else{
                        if(defaultsParam.pageParams.url)
                                {
                                   
                                       if(defaultsParam.pageParams.url !== window.location.pathname)
                                      {
                        
                                        if(defaultsParam.pageParams.url === "/" + $.asLang + $.asAccountManager.loginUrl || defaultsParam.pageParams.url === $.asAccountManager.loginUrl){
                                            if($.asAccountManager.loginUrl != window.location.pathname){
                                                 $.asAccountManager.returnUrl = decodeURIComponent(window.location.pathname)
                                                
                                            }
                                            
                                        }
                                       
                                      }
                               }
                     $.asSetupPage(defaultsParam.pageParams)
                }
                    

            }

        }



        var polyFills = []

        var path = $.asGetScriptPath();
        if (Modernizr.matchmedia) {

            polyFills.push({ url: path + $.asJavaScriptLibraryFolder +'/enquire.js', kind: 'js' })

        } else {
            polyFills.push({ url: path + $.asJavaScriptPolyFillsFolder + '/respond-matchmedia-enquire.js', kind: 'js' })
        }

        if (!Modernizr.promises)
            polyFills.push({ url: path + $.asJavaScriptPolyFillsFolder + '/es6-promise-auto.js', kind: 'js' })

        polyFills.push({ url: path + $.asJavaScriptLibraryFolder + '/fontfaceobserver.js', kind: 'js' })



    var pageSelector = $("body");
    pageSelector.selector="body";
        pageSelector
     .asLoadModule({
         urls: polyFills,
         loadedCallback: function () {
             if (typeof (respond) != 'undefined')
                 respond.update();
             if (defaultsParam.enableMobile === true) {
                 enquire.register("only screen and (max-width: 40em)", {
                     match: function () {
                         $.asCookies.set($.asCoockiesName.asIsMobileMode, { IsMobileMode: true }, { expires: 10000000 })
                         $.asStorage.setJson($.asCoockiesName.asIsMobileMode, {
                             IsMobileMode: true
                         })

                         if (window.location.pathname.toLowerCase().indexOf($.asMobileSign) > -1 || (window.location.pathname.toLowerCase() + "/").indexOf($.asMobileSign) > -1)
                             setupPage()
                         else {

                             window.location.assign("/" + defaultsParam.lang + $.asMobileSign + window.location.pathname.replace("/" + defaultsParam.lang, "").replace("/", ""))
                         }

                     }
                 });
                 enquire.unregister("only screen and (max-width: 40em)");

                 enquire.register("only screen and (min-width:40em)", function () {

                     $.asCookies.set($.asCoockiesName.asIsMobileMode, { IsMobileMode: false }, { expires: 10000000 })
                     $.asStorage.setJson($.asCoockiesName.asIsMobileMode, {
                         IsMobileMode: false
                     })
                     setupPage()

                 });
                 enquire.unregister("only screen and (min-width:40em)");
             } else {

                 setupPage()
             }


         }
     })

        // Declare a proxy to reference the hub. 
       
            var notificationManager = $.connection.notificationHub;
            // Create a function that the hub can call to broadcast messages.
            notificationManager.client.broadcastMessage = function (template,message) {
                $.asShowMessage({ template: template, message: $.asRes[$.asLang][message] ? $.asRes[$.asLang][message]:message })
            };
            
            
            
            
            
            
            
            
            
            
            
            
            var hidden = "hidden";
        
        var onchange = function (evt) {
            var v = "visible", h = "hidden",
                evtMap = {
                  focus:v, focusin:v, pageshow:v, blur:h, focusout:h, pagehide:h
                };
        
            evt = evt || window.event;
            if (evt.type in evtMap){
                if(evtMap[evt.type] === "visible"){
                   // Start the connection.
                    $.connection.hub.start();
                }else{
                     $.connection.hub.stop();
                }
            }
              
            else
              if((this[hidden] ? "hidden" : "visible") === "visible"){
                   // Start the connection.
                    $.connection.hub.start();
                }else{
                     $.connection.hub.stop();
                }
          }
          
          // Standards:
          if (hidden in document)
            document.addEventListener("visibilitychange", onchange);
          else if ((hidden = "mozHidden") in document)
            document.addEventListener("mozvisibilitychange", onchange);
          else if ((hidden = "webkitHidden") in document)
            document.addEventListener("webkitvisibilitychange", onchange);
          else if ((hidden = "msHidden") in document)
            document.addEventListener("msvisibilitychange", onchange);
          // IE 9 and lower:
          else if ("onfocusin" in document)
            document.onfocusin = document.onfocusout = onchange;
          // All others:
          else
            window.onpageshow = window.onpagehide
            = window.onfocus = window.onblur = onchange;
        

        
          // set the initial state (but only if browser supports the Page Visibility API)
          if( document[hidden] !== undefined )
            onchange({type: document[hidden] ? "blur" : "focus"});
            
    

    };
    $.asSetLanguageAndCulter = function (lang, culture, country, rightToLeft, noChangeUrl) {


        if (typeof (lang) != "undefined")
            if (lang != null) {
                $.asLang = lang;
                $.asCulture = culture
                $.asCountry = country
                $.asRightToLeft = rightToLeft
                $.asCookies.set($.asCoockiesName.asLanguageAndCulture, { lang: $.asLang, culture: $.asCulture, country: $.asCountry, rightToLeft: $.asRightToLeft }, { expires: 10000000 })
                $.asStorage.setJson($.asCoockiesName.asLanguageAndCulture, {
                    lang: $.asLang,
                    culture: $.asCulture,
                    country: $.asCountry,
                    rightToLeft: $.asRightToLeft
                })

                if (!noChangeUrl) {

                    var path = window.location.pathname
                    var newPath = "/"
                    if (path.indexOf("/") > -1) {
                        var arrPath = path.replace("/", "").split("/")

                        var item = $.asFindInJsonArray($.asLanguageAndCulture, "language", arrPath[0])
                        if (item != null) {
                            for (var i = 1; i < arrPath.length; i++)
                                newPath += arrPath[i] + (i + 1 === arrPath.length ? "" : "/")
                        } else {
                            for (var i = 0; i < arrPath.length; i++)
                                newPath += arrPath[i] + (i + 1 === arrPath.length ? "" : "/")
                        }

                        path = newPath
                    }

                    window.location.assign("/" + lang + path)
                }

            }
    }

    $.asGetDefaultsLnguageAndCulter = function () {
        var lang = $.asCookies.getJSON($.asCoockiesName.asLanguageAndCulture)
        if (typeof (lang) === "undefined")
            lang = JSON.parse($.asStorage.getItem($.asCoockiesName.asLanguageAndCulture))
        return lang
    }



    //#region route
    $(document).delegate('a', 'click', function (e) {
         if($(this).attr('target') === "_blank")
           return;
        var noLink = false, className
        if (e.which === 1 && !e.metaKey && !e.shiftKey) {
            
            if (typeof ($(this).attr('href')) == "undefined")
                return true;
                
            var href = $(this).attr('href').toLowerCase()
            
            if (href.indexOf("javascript:") > -1 || href.indexOf("javascript :") > -1)
                return false;
            
            var isModal = false
            if(href.length > 1){
                 var modalUrl =  ("/" + href + "/" ).replace("//","/").toLowerCase()
               
                $.each($.asModalManager.urls, function (index, value) {
                  
                    if(("/" + value+ "/" ).replace("//","/").toLowerCase() === modalUrl){
                           var $winLogin= $.asModalManager.get({url:value,isglobal:true})
                             $winLogin.asModal()
                             $winLogin.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter(value)},{name:"@isModal",value:true}]))
                              isModal = true
                              return false
                           }

                 })
              }
            if(isModal)
            return false;
            
            if (this.className) {
                className = this.className
                $.each($.asUnLinkClasses, function (index, value) {

                    if (className.indexOf(value) > -1) {
                        noLink = true
                        return false
                    }

                });
                if (noLink) return false
            }

            var url
            if (href[0] === "/" && href.length > 1)
                url = href.substring(1).replace(/\//g, $.asUrlDelimeter)
            else
                url = href.replace(/\//g, $.asUrlDelimeter)

            if (href[0] === "#")
                return false
            var logoffUrl = "/" + $.asLang + "/" + $.asAccountManager.logOffSign.toLowerCase();
            if (href.toLowerCase().indexOf(logoffUrl) == 0 || href.toLowerCase().indexOf("/" + $.asAccountManager.logOffSign.toLowerCase()) == 0) {
                var returnUrl = href.substring(href.toLowerCase().indexOf(logoffUrl) + logoffUrl.length)
                $.asLogOff(returnUrl);


                return false
            } else if ($(this).attr('data-as-modal')) {
                return false
            } else {
                $.asLoadPage(href, url, true)
                return false
            }
        }

        return false
    })
    
    $.asGetCurrentPageTemplatePatternAndTemplate = function(pageUrl){
                var newTemplateUrls=""
                var newFrameWorkUrls=""
                if(pageUrl){
                     pageUrl=pageUrl.toLowerCase()
                     $.asFrameWorkAndTemplatePattern
                     $.each($.asFrameWorkAndTemplatePattern, function (key, value) {
                        $.each(value, function (k, v) {
                            if(pageUrl.indexOf(v.toLowerCase()) === 0){
                                if(newTemplateUrls.length < v.length){
                                       newTemplateUrls = v.toLowerCase()
                                       newFrameWorkUrls = key
                                }
                            }
                               
                        });
                    });
                }
                return {
                    templateUrl:newTemplateUrls,
                    frameWorkUrl:newFrameWorkUrls
                }
            }
    $.asSetupPage = function (params, placeHolder, replaceHtml, isModal, pageParams) {

        if (typeof (replaceHtml) == "undefined")
            replaceHtml = false

        if (typeof (isModal) == "undefined")
            isModal = false

        var asPageId = "." + params.pageId + "." + $.asPageClass;
        var pageEvent = "#" + params.pageId;
        var holder, $holder

        if (isModal === false) {
            holder = $.asPlaceHolder
            if (placeHolder) {
                holder = placeHolder
            }

            if (holder === 'body') {
                $(holder).removeClass("modal-open");
            } else {
                $(document).prop('title', params.title);
            }
            $holder = $(holder)
        } else {
            $holder = placeHolder
            if ($(pageEvent).length === 0) {
    
                $( $.asPlaceHolder).append("<span style='display:none' id='" + params.pageId  + "'></span>"  );
            }
        }

             var pageTemplatePatternAndFrameWork = $.asGetCurrentPageTemplatePatternAndTemplate(params.url)
              $.asCurrentTemplateUrl = pageTemplatePatternAndFrameWork.templateUrl
              $.asCurrentFrameWorkUrl = pageTemplatePatternAndFrameWork.frameWorkUrl
        // if (typeof params.templateUrl !== 'undefined') {

        //     $.asCurrentTemplateUrl = params.templateUrl
        //     $.asCurrentFrameWorkUrl = params.frameWorkUrl

        // }




        $.asSetWebPageData({ data: params, $holder: $holder, replaceHtml: replaceHtml });
        if(params.dependentModules === "[]")
            params.dependentModules=[]
        if(params.param === "{}")
           params.param={}
           
        var pageSelector = $(asPageId);
        pageSelector.selector=asPageId;
        pageSelector
            .asLoadModule({
                urls: $.asGetWebpageDependentModuleInRunTime(params.dependentModules, params.param),
                loadedCallback: function () {
                    if (typeof (respond) != "undefined")
                        respond.update()
                    $.asPageLoadComplete(params, asPageId)
                    try {
                        
                        $(pageEvent).trigger($.asEvent.page.loaded, [params.requestedUrl, pageParams])
                    } catch (err) {
                        $.asShowMessage({ template: "error", message: err })
                        $.asConsole.error(err)
                    }
                }
            });

    }
    $.asLoadPage = function (location, url, changeUrl) {

        var pageUrl = $.asInitService($.asFormUrl, [{
            name: "@url", value: url.indexOf($.asQueryStringShadowSign) > -1 ?
                url.replace(url.substring(url.toLowerCase().indexOf($.asQueryStringShadowSign)), "") : url
        }, { name: "/@isModal", value: "" }])

        //var pageUrl = "/" + "cms/GetWebPage" + "/" + url
        var state = History.getState();
        if (state.data.pageUrl === pageUrl) {
            $._asLoadPage(state.data.pageUrl)
        } else if (changeUrl === true) {

            History.pushState({ pageUrl: pageUrl, pureUrl: url }, location, location)
        } else {
            History.pushState({ pageUrl: pageUrl, pureUrl: url }, location)
        }

    }

    $._asLoadPage = function (url, placeHolder) {
        var holder = $.asPlaceHolder
        if (placeHolder) {
            holder = placeHolder
        }

        if (url.toLowerCase().indexOf($.asQueryStringSign) > -1)
            url = url.replace(new RegExp(url.substring(url.toLowerCase().indexOf($.asQueryStringSign)), "gi"), "")
            
         url = url.replace(new RegExp(" ", "gi"), "%");
            
        var loadPage = function (data) {

                // if (typeof data.templateUrl !== 'undefined') {
                
                //     if ($.asCurrentTemplateUrl !== data.templateUrl && $.asCurrentTemplateUrl !== "") {
                //         if ($.asCurrentFrameWorkUrl !== data.frameWorkUrl && $.asCurrentFrameWorkUrl !== "") {
                //             window.location.assign("/" + url.substring(url.indexOf($.asFormUrl + "/")))
                //         } else {
                //             var pageUrl = $.asChangeTemplateUrl + "/" + data.url.replace(/\//gi, $.asUrlDelimeter);
                //             $('body')
                //                 .asAjax({
                //                     url: pageUrl,
                //                     type: "get",
                //                     success: function (template) {
                //                         $('body').removeClass("modal-open");
                //                         $.asSetupPage(template, 'body', true)
                //                         $.asSetupPage(data, holder, true)
                //                     }
                //                 })
                //         }
                //     }
                //     else {
                //         $.asSetupPage(data, holder, true)
                //     }
                // }
                
                if (typeof data.url !== 'undefined') {
                    var pageTemplatePatternAndFrameWork = $.asGetCurrentPageTemplatePatternAndTemplate(data.url)
                    if ($.asCurrentTemplateUrl !== pageTemplatePatternAndFrameWork.templateUrl && $.asCurrentTemplateUrl !== "") {
                        if ($.asCurrentFrameWorkUrl !== pageTemplatePatternAndFrameWork.frameWorkUrl && $.asCurrentFrameWorkUrl !== "") {
                            location.reload()
                            // window.location.assign("/" + url.substring(url.indexOf($.asFormUrl + "/")))
                        } else {
                            
                             $.asCurrentTemplateUrl = pageTemplatePatternAndFrameWork.templateUrl
                             $.asCurrentFrameWorkUrl = pageTemplatePatternAndFrameWork.frameWorkUrl
                            
                            var pageUrl = $.asChangeTemplateUrl + "/" + data.url.replace(/\//gi, $.asUrlDelimeter);
                            var $pageSelector = $('body');
                            $pageSelector.selector="body";
                            $pageSelector
                                .asAjax({
                                    url: pageUrl,
                                    type: "get",
                                    success: function (template) {
                                        $('body').removeClass("modal-open");
                                        $.asSetupPage(template, 'body', true)
                                        $.asSetupPage(data, holder, true)
                                    }
                                })
                        }
                    }
                    else {
                        $.asSetupPage(data, holder, true)
                    }
                }
                else {
                    $.asSetupPage(data, holder, true)
                }


            }
        $.as(holder).asAjax({
            url: url,
            type: "get",
            success: function (data) {
               
               if(data.url)
            {
               
                   if(data.url !== url.replace($.asFormUrl.substring(0, $.asFormUrl.toLowerCase().indexOf("@")),"/").replace(new RegExp($.asUrlDelimeter, "gi"), "/").replace("//","/"))
                  {
                       
                    if(data.url === "/" + $.asLang + $.asAccountManager.loginUrl || data.url === $.asAccountManager.loginUrl){
                       
                          $.asAccountManager.returnUrl = decodeURIComponent(url.replace($.asFormUrl.substring(0, $.asFormUrl.toLowerCase().indexOf("@")),"/").replace(new RegExp($.asUrlDelimeter, "gi"), "/").replace("//","/"))
                          
                    }
                  
                  }
             }
               
                loadPage(data)

            },
            error:function(data){
             
                 loadPage(data.responseJSON)
            }
        })

    }

    //$.asChangeTemplate = function () {
    //    //console.log("$.asChangeTemplate")
    //    //$($.asPlaceHolder).empty()
    //    //$($.asPlaceHolder).html($.asCurrentPage.page)
    //}
    $.asUrlAsParameter = function (url) {
        return url.replace(new RegExp("/", "gi"), $.asUrlDelimeter);
    }
    //#endregion 



    //#region Arabic Ye And Ke To Persiann

    $.asArabicYeCharCode = 1610;
    $.asPersianYeCharCode = 1740;
    $.asArabicKeCharCode = 1603;
    $.asPersianKeCharCode = 1705;

    $.asTopersian = function () {
        $(document).keypress(function (e) {
            var keyCode = e.keyCode ? e.keyCode : e.which;
            //var response = $.asResponse();
            //var browser = response.browser.accessName;

            //if ($.browser.msie) {
            if (bowser.msie) {
                switch (keyCode) {
                    case $.asArabicYeCharCode:
                        event.keyCode = $.asPersianYeCharCode;
                        break;
                    case $.asArabicKeCharCode:
                        event.keyCode = $.asPersianKeCharCode;
                        break;
                }
            }
                //else if ($.browser.mozilla) {
            else if (bowser.firefox) {
                switch (keyCode) {
                    case $.asArabicYeCharCode:
                        asSubstituteCharInFireFox($.asPersianYeCharCode, e);
                        break;
                    case $.asArabicKeCharCode:
                        asSubstituteCharInFireFox($.asPersianKeCharCode, e);
                        break;
                }
            } else {
                switch (keyCode) {
                    case $.asArabicYeCharCode:
                        asInsertAtChare(String.fromCharCode($.asPersianYeCharCode), e);
                        break;
                    case $.asArabicKeCharCode:
                        asInsertAtChare(String.fromCharCode($.asPersianKeCharCode), e);
                        break;
                }
            }
        });

        $('input,textarea').on('paste', function (e) {
            var el = $(this);
            //we need to wait about 100ms for the paste value to actually change the val()
            setTimeout(function () {
                var text = $(el).val();
                $(el).val(text.replace(new RegExp(String.fromCharCode($.asArabicYeCharCode), "g"), String.fromCharCode($.asPersianYeCharCode))
                    .replace(new RegExp(String.fromCharCode($.asArabicKeCharCode), "g"), String.fromCharCode($.asPersianKeCharCode)));
            }, 100);
        });
    }

    function asSubstituteCharInFireFox(charCode, e) {
        var keyEvt = document.createEvent("KeyboardEvent");
        keyEvt.initKeyEvent("keypress", true, true, null, false, false, false, false, 0, charCode);
        e.target.dispatchEvent(keyEvt);
        e.preventDefault();
    }

    function asSubstituteCharInChrome(charCode, e) {
        //it does not work yet! /*$.browser.webkit*/
        //https://bugs.webkit.org/show_bug.cgi?id=16735
        var keyEvt = document.createEvent("KeyboardEvent");
        keyEvt.initKeyboardEvent("keypress", true, true, null, false, false, false, false, 0, charCode);
        e.target.dispatchEvent(keyEvt);
        e.preventDefault();
    };

    function asInsertAtChare(myValue, e) {
        var obj = e.target;
        var startPos = obj.selectionStart;
        var endPos = obj.selectionEnd;
        var scrollTop = obj.scrollTop;
        obj.value = obj.value.substring(0, startPos) + myValue + obj.value.substring(endPos, obj.value.length);
        obj.focus();
        obj.selectionStart = startPos + myValue.length;
        obj.selectionEnd = startPos + myValue.length;
        obj.scrollTop = scrollTop;
        e.preventDefault();
    };
    $.asTopersianData = function (data) {

        if ($.asLang === "fa" && typeof (data) != "undefined") {
            if (typeof (data) === "string") {
                return data.replace(new RegExp(String.fromCharCode($.asArabicYeCharCode), "g"),
                        String.fromCharCode($.asPersianYeCharCode))
                    .replace(new RegExp(String.fromCharCode($.asArabicKeCharCode), "g"),
                        String.fromCharCode($.asPersianKeCharCode));
            } else {
                var strData = JSON.stringify(data)

                strData = strData.replace(new RegExp(String.fromCharCode($.asArabicYeCharCode), "g"),
                        String.fromCharCode($.asPersianYeCharCode))
                    .replace(new RegExp(String.fromCharCode($.asArabicKeCharCode), "g"),
                        String.fromCharCode($.asPersianKeCharCode));
                return JSON.parse(strData)
            }

        }

        else
            return data
    }
    //#endregion Arabic Ye And Ke To Persiann

    $.asSetLanguageAndCulterForCurrentRequest = function (headers) {

        var urlLangs = window.location.pathname.split('/')
        if ($.asGetDefaultsLnguageAndCulter() != null) {
            if ($.asGetDefaultsLnguageAndCulter().lang != urlLangs[1]) {
                var lang = $.asFindItemInJsonArray($.asLanguageAndCulture, 'language', 'language', urlLangs[1])
                if (lang != null) {
                    var culture = $.asFindItemInJsonArray($.asLanguageAndCulture, 'language', 'culture', urlLangs[1])
                    var rightToLeft = $.asFindItemInJsonArray($.asLanguageAndCulture, 'language', 'rightToLeft', urlLangs[1])
                    $.asSetLanguageAndCulter(lang, culture, $.asFindItemInJsonArray($.asLanguageAndCulture, 'language', 'country', urlLangs[1]), rightToLeft, true)
                }
            }
        }

        if (headers) {
            if ($.asGetDefaultsLnguageAndCulter() != null) {
                headers.lang = $.asGetDefaultsLnguageAndCulter().lang;
                headers.culture = $.asGetDefaultsLnguageAndCulter().culture;
                //headers.country = $.asGetDefaultsLnguageAndCulter().country;
                headers.rightToLeft = $.asGetDefaultsLnguageAndCulter().rightToLeft;
            }
            return headers
        }

    };
    $.asAjaxTask = function (params) {

        //var headers = {};

        //var token = $.asStorage.getItem($.asLoginAccessToken);

        //if (token) {

        //    headers.Authorization = 'Bearer ' + token;

        //}

        //headers = $.asSetLanguageAndCulterForCurrentRequest(headers)
        //headers.isDebugMode = $.asDebugMode()

        //var loadComplete = function(data) {
        //    if (typeof (data["odata.metadata"]) != "undefined")
        //        return data.value
        //    else
        //        return data
        //}

        var defaultsParam = $.extend({
            //complete: function (jqxhr,status) {
            //    jqxhr.responseJSON =jqxhr.responseJSON.value
            //},
            url: $.asTopersianData(params.url),
            beforeSend: function (jqXHR, settings) {
                var header = jqXHR
                var options = settings
                if (typeof (params.beforeSend) != "undefined")
                    params.beforeSend(header, options);
                $.each($._asGetHeaders(), function (k, v) {
                    header.setRequestHeader(k, v);
                });
                $($.asEvent.global).trigger($.asEvent.global.beforeSendAjaxRequest, [settings])
            },
            //global: false,
            contentType: 'application/json; charset=utf-8',
            type: 'get',
        }, params);
       
        if(defaultsParam.dataType === undefined){
            defaultsParam = $.extend({
                       dataFilter: function (data) {
                                  try {
                    data = JSON.parse(data);
                    if (data != null) {
                        if (typeof (data["odata.metadata"]) != "undefined") {
                            if (typeof (data["odata.count"]) != "undefined") {
                                var jsonData = {
                                    total: data["odata.count"],
                                    rows: data.value
                                }
                                return JSON.stringify(jsonData)
                            }
                            return JSON.stringify(data.value)
                        }
                        else
                            return JSON.stringify(data)
                    } else {
                        return JSON.stringify([])
                    }
                    //console.log(data)
                } catch (err) {
                    if (err.message === "Unexpected token < in JSON at position 0") {
                        $.asConsole.error("Invalid Jsaon Data :")
                        throw data
                    }
                    else
                        throw err
                }
            }
            },defaultsParam)
        }
        return $.ajax(defaultsParam);
    };
    $.asSetQueryString = function (q) {
        if (typeof (q) != "undefined") {
            var state = History.getState()
            var reg = new RegExp($.asUrlDelimeter, "gi")


            var url = state.data.pureUrl
            var pageUrl = state.data.pageUrl
            
            if(url === undefined){
                 url = state.data.pureUrl=$.asUrlAsParameter(state.hash.substring(1))
                pageUrl = $.asInitService($.asFormUrl, [{name: "@url", value: url}, { name: "/@isModal", value: "" }])
            }
            // if(url){
                 if (url.toLowerCase().indexOf($.asQueryStringSign) > -1)
                url = url.replace(new RegExp(url.substring(url.toLowerCase().indexOf($.asQueryStringSign)), "gi"), "")
            if (url.toLowerCase().indexOf($.asQueryStringSign.replace(new RegExp($.asQueryStringSign, "gi"), $.asQueryStringShadowSign)) > -1)
                url = url.replace(url.substring(url.toLowerCase().indexOf($.asQueryStringSign.replace(new RegExp($.asQueryStringSign, "gi"), $.asQueryStringShadowSign))), "")

           

            if (pageUrl.toLowerCase().indexOf($.asQueryStringSign) > -1)
                pageUrl = pageUrl.replace(new RegExp(pageUrl.substring(pageUrl.toLowerCase().indexOf($.asQueryStringSign)), "gi"), "")

            if (pageUrl.toLowerCase().indexOf($.asQueryStringSign.replace($.asQueryStringSign, $.asQueryStringShadowSign)) > -1)
                pageUrl = pageUrl.replace(new RegExp(pageUrl.substring(pageUrl.toLowerCase().indexOf($.asQueryStringSign.replace($.asQueryStringSign, $.asQueryStringShadowSign))), "gi"), "")

            // }else{
            //     url = state.data.pureUrl=$.asUrlAsParameter(state.hash.substring(1))
            //     pageUrl = $.asFormUrl + "/" + url
            // }
           

            History.pushState({ pureUrl: state.data.pureUrl, queryString: q, pageUrl: pageUrl + $.asQueryStringSign + q, loadPage: false }, q, ("/" + url.replace(reg, "/") + $.asQueryStringSign + q)
            .replace(new RegExp("//", "gi"), "/"))
        }
    }
    $.asGetQueryString = function () {
        var url = window.location.pathname
        //var reg = new RegExp($.asUrlDelimeter, "g");
        //var qSign = $.asQueryStringSign.replace(reg, "/")

        if (url.toLowerCase().indexOf($.asQueryStringSign) > -1)
            return url.substring(url.toLowerCase().indexOf($.asQueryStringSign) + 3)
        return null

    }
    $._asDataLoaded = function (pageId, pageEvent, id, name) {
        $($.asEvent.global).trigger($.asEvent.global.dataLoadComplete, [id, name])
        if (pageId !== "") {
            $(pageEvent).trigger($.asEvent.page.dataLoading, [id, name])
            var dataItems = $(pageId).data('dataItems') || [];
            dataItems.pop();
            $(pageId).data('dataItems', dataItems)
            if (dataItems.length === 0) {
                $(pageEvent).trigger($.asEvent.page.dataLoaded, [])

            }
        }
    }
    $._asDataRequested = function (context, page) {
        var id = $(context).prop('id'), pageEvent = "", pageId = "", dataItems
        //if (typeof (page) == "undefined")
        //    throw 'Source Missing Page Property : ' + id

        //var pageId = (page.selector.indexOf("." + $.asPageClass) > -1)
        //    ? page.selector.substring(0, page.selector.indexOf("." + $.asPageClass)) + "." + $.asPageClass
        //    : "";
        //var pageEvent = ""

        //if (pageId !== "") {
        //    pageEvent = $.asGetPageEventName(page)
        //    var dataItems = $(pageId).data('dataItems') || []

        //    dataItems.push(name)

        //    $(pageId).data('dataItems', dataItems)
        //}






        if (typeof (page) != "undefined") {
            pageId = (page.selector.indexOf("." + $.asPageClass) > -1)
               ? page.selector.substring(0, page.selector.indexOf("." + $.asPageClass)) + "." + $.asPageClass
               : "";

            if (pageId !== "") {
                pageEvent = $.asGetPageEventName(page)
                dataItems = $(pageId).data('dataItems') || []

                dataItems.push(id)

                $(pageId).data('dataItems', dataItems)
            }
        } else if (typeof (context) != "undefined") {
            context.selector = context.selector || $.asPlaceHolder;
            pageId = (context.selector.indexOf("." + $.asPageClass) > -1)
               ? context.selector.substring(0, context.selector.indexOf("." + $.asPageClass)) + "." + $.asPageClass
               : "";

            if (pageId !== "") {
                pageEvent = $.asGetPageEventName(context)
                dataItems = $(pageId).data('dataItems') || []

                dataItems.push(id)

                $(pageId).data('dataItems', dataItems)
            }
        } else
            throw 'Ajax Get Missing Page Property or Using as Selector : ' + id









        return { pageId: pageId, pageEvent: pageEvent, id: id }
    }
})();


(function ($) {
    $.fn.asShake = function (options) {
        // defaults
        var settings = {
            'shakes': 2,
            'distance': 10,
            'duration': 400
        };
        // merge options
        if (options) {
            $.extend(settings, options);
        }
        // make it so
        var pos;
        return this.each(function () {
            var $this = $(this);
            // position if necessary
            pos = $this.css('position');
            if (!pos || pos === 'static') {
                $this.css('position', 'relative');
            }
            // shake it
            for (var x = 1; x <= settings.shakes; x++) {
                if (settings.direction === 'h') {
                    $this.animate({ left: settings.distance * -1 }, (settings.duration / settings.shakes) / 4)
                   .animate({ left: settings.distance }, (settings.duration / settings.shakes) / 2)
                   .animate({ left: 0 }, (settings.duration / settings.shakes) / 4);
                } else if (settings.direction === 'v') {
                    $this.animate({ top: settings.distance * -1 }, (settings.duration / settings.shakes) / 4)
                        .animate({ top: settings.distance }, (settings.duration / settings.shakes) / 2)
                        .animate({ top: 0 }, (settings.duration / settings.shakes) / 4);
                }
                else {
                    $this.animate({ left: settings.distance * -1 }, (settings.duration / settings.shakes) / 4)
                       .animate({ left: settings.distance }, (settings.duration / settings.shakes) / 2)
                       .animate({ left: 0 }, (settings.duration / settings.shakes) / 4).animate({ top: settings.distance * -1 }, (settings.duration / settings.shakes) / 4)
                       .animate({ top: settings.distance }, (settings.duration / settings.shakes) / 2)
                       .animate({ top: 0 }, (settings.duration / settings.shakes) / 4);
                }
            }
        });
    };
    $.fn.asAfterTasks = function (tasks, doneFunc, setting) {
        var currentContext = $._asDataRequested(this);
        var defaultsSetting = $.extend({ overLay: true, overlayClass: 'as-overlay', removeOverlay: true, waitingView: $.asWaitingView }, setting);
        var $elamn = $.as(this);
        if (defaultsSetting.overLay)
            $($elamn).loadingOverlay({ loadingText: defaultsSetting.waitingView, loadingClass: 'as-loading', overlayClass: defaultsSetting.overlayClass });
        $.when.apply($, tasks).done(doneFunc).then(function () {
            if (defaultsSetting.overLay && defaultsSetting.removeOverlay === true)
                $($elamn).loadingOverlay('remove', { loadingClass: 'as-loading', overlayClass: defaultsSetting.overlayClass });
            $._asDataLoaded(currentContext.pageId, currentContext.pageEvent, currentContext.id);
        });
        return this;
    };
    $.fn.asCenter = function () {
        this.css("position", "absolute");
        this.css("top", Math.max(0, (($(window).height() - $(this).outerHeight()) / 2) +
            $(window).scrollTop()) + "px");
        this.css("left", Math.max(0, (($(window).width() - $(this).outerWidth()) / 2) +
            $(window).scrollLeft()) + "px");
        return this;
    };
    $.fn.asAjax = function (params, setting) {

        var $elamn = $.as(this), currentContext
        var defaultsSetting = $.extend({ overLay: true, overlayClass: 'as-overlay', loadingText: $.asWaitingView, validate: true, removeOverlay: true }, setting);
        if (defaultsSetting.dataAdepter !== true){
            currentContext = $._asDataRequested(this);
        }
            
        //if (typeof (source.page) != "undefined")
        //{

        //     pageId = (source.page.selector.indexOf("." + $.asPageClass) > -1)
        //        ? source.page.selector.substring(0, source.page.selector.indexOf("." + $.asPageClass)) + "." + $.asPageClass
        //        : "";

        //    if (pageId !== "") {
        //        pageEvent = $.asGetPageEventName(source.page)
        //         dataItems = $(pageId).data('dataItems') || []

        //        dataItems.push(source.name)

        //        $(pageId).data('dataItems', dataItems)
        //    }
        //} else if (typeof (this) != "undefined") {
        //    pageId = (this.selector.indexOf("." + $.asPageClass) > -1)
        //       ? this.selector.substring(0, this.selector.indexOf("." + $.asPageClass)) + "." + $.asPageClass
        //       : "";

        //    if (pageId !== "") {
        //        pageEvent = $.asGetPageEventName(this)
        //         dataItems = $(pageId).data('dataItems') || []

        //        dataItems.push(source.name)

        //        $(pageId).data('dataItems', dataItems)
        //    }
        //} else
        //    throw 'Ajax Get Missing Page Property or Using as Selector : ' + id

        //var pageId = (source.page.selector.indexOf("." + $.asPageClass) > -1)
        //    ? source.page.selector.substring(0, source.page.selector.indexOf("." + $.asPageClass)) + "." + $.asPageClass
        //    : "";
        //var pageEvent = ""

        //if (pageId !== "") {
        //    pageEvent = $.asGetPageEventName(source.page)
        //    var dataItems = $(pageId).data('dataItems') || []

        //    dataItems.push(source.name)

        //    $(pageId).data('dataItems', dataItems)
        //}





        var customeBeforeSend = params.beforeSend, customeError = params.error
        delete params.beforeSend
        delete params.error
        var defaultsParam = $.extend({
            //headers: headers,
            //global: false,
            url: $.asTopersianData(params.url),
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            type: 'post',
            error: function (jqXHR, textStatus, errorThrown) {
                if (typeof (customeError) != "undefined")
                    customeError(jqXHR, textStatus, errorThrown)
                $.asAjaxError(jqXHR, textStatus, errorThrown)
            },
            beforeSend: function (jqXHR, settings) {
                var header = jqXHR
                var options = settings
                if (typeof (customeBeforeSend) != "undefined")
                    customeBeforeSend(header, options);
                $.each($._asGetHeaders(), function (k, v) {
                    header.setRequestHeader(k, v);
                });
                $($.asEvent.global).trigger($.asEvent.global.beforeSendAjaxRequest, [settings])
                if (defaultsSetting.overLay)
                    $($elamn).loadingOverlay({ loadingText: defaultsSetting.loadingText, loadingClass: 'as-loading', overlayClass: defaultsSetting.overlayClass });
            },
            complete: function (jqXHR, textStatus) {
                if (defaultsSetting.overLay && defaultsSetting.removeOverlay === true)
                    $($elamn).loadingOverlay('remove', { loadingClass: 'as-loading', overlayClass: defaultsSetting.overlayClass });
                if (defaultsSetting.dataAdepter !== true && defaultsParam.type === "get") {
                    $._asDataLoaded(currentContext.pageId, currentContext.pageEvent, currentContext.id)
                }
            }
        }, params)

                
            if(defaultsParam.dataType === undefined){
            defaultsParam = $.extend({
                       dataFilter: function (data) {
                        
                           
                                                 try {
                                    
                    data = JSON.parse(data);
                    if (data != null) {
                        if (typeof (data["odata.metadata"]) != "undefined") {
                            if (typeof (data["odata.count"]) != "undefined") {
                                var jsonData = {
                                    total: data["odata.count"],
                                    rows: data.value
                                }
                                return JSON.stringify(jsonData)
                            }
                            return JSON.stringify(data.value)
                        }
                        else
                            return JSON.stringify(data)
                    } else {
                        return JSON.stringify([])
                    }
                    //console.log(data)
                } catch (err) {
                    if (err.message === "Unexpected token < in JSON at position 0") {
                        $.asConsole.error("Invalid Jsaon Data :")
                        throw data
                    }
                    else
                        throw err
                }
                           
                
            }
            },defaultsParam)
        }
        
        if (typeof defaultsSetting.$form !== 'undefined' && defaultsParam.data === undefined)
            defaultsParam.data = defaultsSetting.$form.asSerializeObject()

        defaultsParam.data = $.asTopersianData(defaultsParam.data)
        if (defaultsParam.type.toLowerCase() === 'post') {
            if (defaultsSetting.validate && defaultsSetting.$form) {
                if (defaultsSetting.$form.asValidate('valid'))
                    $.ajax(defaultsParam);
            } else
                $.ajax(defaultsParam)
        } else {
            $.ajax(defaultsParam)
        }

        // allow jQuery chaining
        return this;
    };
    //#region Localize
    $.fn.asLocalize = function () {
        var wrappedSet = this;

        var valueForKey = function (key, data) {
            var keys, value, _i, _len;
            keys = key.split(/\./);
            value = data;
            for (_i = 0, _len = keys.length; _i < _len; _i++) {
                key = keys[_i];
                value = value != null ? value[key] : null;
            }
            return value;
        };


        var setAttrFromValueForKey = function (elem, key, value) {
            value = valueForKey(key, value);
            if (value != null) {
                return elem.attr(key, value);
            }
        };
        var setTextFromValueForKey = function (elem, key, value) {
            value = valueForKey(key, value);
            if (value != null) {
                return elem.text(value);
            }
        };

        var localizeInputElement = function (elem, key, value) {
            var val = $.isPlainObject(value) ? value.value : value;
            if (elem.is("[placeholder]")) {
                return elem.attr("placeholder", val);
            } else {
                return elem.val(val);
            }
        };
        var localizeForSpecialKeys = function (elem, value) {
            setAttrFromValueForKey(elem, "title", value);
            setAttrFromValueForKey(elem, "href", value);
            return setTextFromValueForKey(elem, "text", value);
        };
        var localizeOptgroupElement = function (elem, key, value) {
            return elem.attr("label", value);
        };
        var localizeImageElement = function (elem, key, value) {
            setAttrFromValueForKey(elem, "alt", value);
            return setAttrFromValueForKey(elem, "src", value);
        };

        var localizeElement = function (elem, key, value) {
            if (elem.is('input')) {
                localizeInputElement(elem, key, value);
            } else if (elem.is('textarea')) {
                localizeInputElement(elem, key, value);
            } else if (elem.is('img')) {
                localizeImageElement(elem, key, value);
            } else if (elem.is('optgroup')) {
                localizeOptgroupElement(elem, key, value);
            } else if (!$.isPlainObject(value)) {
                elem.html(value);
            }
            if ($.isPlainObject(value)) {
                return localizeForSpecialKeys(elem, value);
            }
        };




        //return wrappedSet.each(function () {
        //    var elem, key, value;
        //    elem = $(this);
        //    key = elem.data("localize");
        //    key || (key = elem.attr("rel").match(/localize\[(.*?)\]/)[1]);
        //    value = valueForKey(key, $.asRes[$.asLang]);
        //    if (value != null) {
        //        return localizeElement(elem, key, value);
        //    }
        //    return this;
        //});

        return wrappedSet.each(function () {
            var key;
            var elem = $(this);
            key = elem.data("localize");
            key || (key = elem.attr("rel").match(/localize\[(.*?)\]/)[1]);
            var value = "";
            $.each(key.split(" "), function (index, val) {
                value += valueForKey(val, $.asRes[$.asLang]) + " ";
            });

            if (value != null) {
                return localizeElement(elem, key, $.trim(value));
            }
            return this;
        });
    };
    //#endregion
    //#region serialize Form
    $.fn.asSerializeObject = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            var name = this.name.substring(this.name.lastIndexOf('_') + 1)
            if (o[name] !== undefined) {
                if (!o[name].push) {
                    o[name] = [o[name]];
                }
                o[name].push(this.value || '');
            } else {
                o[name] = this.value || '';
            }
        });
        return o;
    };
    //#endregion

    $.fn.asDataAdepter = function (source, settings) {

        //try {


        //var id = $(this).prop('id')
        //if (typeof (source.page) == "undefined")
        //    throw 'Source Missing Page Property : ' + id

        //var pageId = (source.page.selector.indexOf("." + $.asPageClass) > -1)
        //    ? source.page.selector.substring(0, source.page.selector.indexOf("." + $.asPageClass)) + "." + $.asPageClass
        //    : "";
        //var pageEvent = ""

        //if (pageId !== "") {
        //    pageEvent = $.asGetPageEventName(source.page)
        //    var dataItems = $(pageId).data('dataItems') || []

        //    dataItems.push(source.name)

        //    $(pageId).data('dataItems', dataItems)
        //}
        var currentContext = $._asDataRequested(this, source.page);

        var defaultsSource = $.extend({
            pageId: currentContext.pageId,
            pageEvent: currentContext.pageEvent
        },
            source);
        var loadComplete = function (data) {

            if (data.length > 0) {
                if (data.length == 3)
                    if (data[1] === "success")
                        data = data[0]
                if (typeof (defaultsSource.hierarchy) != "undefined")
                    if (defaultsSource.hierarchy.type === "flat") {
                        var defaultsParams = $.extend({
                            list: data
                        },
                            defaultsSource.hierarchy);
                        //data = $.asTreeify({ list: data, keyDataField: defaultsSource.hierarchy.keyDataField, parentDataField: defaultsSource.hierarchy.parentDataField, childrenDataField: defaultsSource.hierarchy.childrenDataField })
                        data = $.asTreeify(defaultsParams)
                        defaultsSource.hierarchy.type = 'tree'
                        if (defaultsSource.hierarchy.convertchildByZeroLengthToNull === true)
                            $.asNullizeChildrenOfLenegthOfZero(data, defaultsSource.hierarchy.childrenDataField)
                    }
            }

            defaultsSource.loadComplete(data)

            $._asDataLoaded(defaultsSource.pageId, defaultsSource.pageEvent, currentContext.id, defaultsSource.name)
            //$($.asEvent.global).trigger($.asEvent.global.dataLoadComplete, [id, defaultsSource.name])
            //if (defaultsSource.pageId !== "") {
            //    $(defaultsSource.pageEvent).trigger($.asEvent.page.dataLoading, [id, defaultsSource.name])
            //    var dataItems = $(defaultsSource.pageId).data('dataItems') || [];
            //    dataItems.pop();
            //    $(defaultsSource.pageId).data('dataItems', dataItems)
            //    if (dataItems.length === 0) {
            //        $(defaultsSource.pageEvent).trigger($.asEvent.page.dataLoaded, [])

            //    }
            //}

        }

        defaultsSource.hierarchy = $.extend({
            childrenDataField: 'children',
            convertchildByZeroLengthToNull: false,
            removeChildLessParent: false
        },
            source.hierarchy);

        //var defaultssettings = $.extend({
        //}, settings);

        if (defaultsSource.url) {
            var defaultssettings = $.extend({
                settings: {
                    success: function (data) {
                        if (data != null)
                            if (typeof (data["odata.metadata"]) != "undefined") {
                                if (typeof (data["odata.count"]) != "undefined") {
                                    var jsonData = {
                                        total: data["odata.count"],
                                        rows: data.value
                                    }
                                    loadComplete(jsonData)
                                }
                                loadComplete(data.value)
                            }
                            else
                                loadComplete(data)
                    },
                    url: defaultsSource.url,
                    type: 'get',
                    dataAdepter: true
                }
            },
                settings);

            $.as(this).asAjax(defaultssettings.settings, defaultssettings.extraSettings)
        } else if (defaultsSource.localData) {
            loadComplete(defaultsSource.localData)
        }

        // allow jQuery chaining
        return this;
        //} catch (err) {
        //    console.error(err)
        //}
    };
    //    $.fn.asLoadPartialPage = function (params) {
    //        // merge default and user parameters

    //        var $partial = $(this);

    //        if ($.type(params) === "string") {

    //        } else {
    //            var url
    //            if (params.url === "/" && params.url.length > 1)
    //                url = params.url.substring(1).replace(/\//g, $.asUrlDelimeter)
    //            else
    //                url = params.url.replace(/\//g, $.asUrlDelimeter)

    //            if (params.url[0] === "#")
    //                return false;

    //            $partial.html('<div style="width: 15px;height: 15px"></div>')
    //            $partial.asAjax({
    //                url: "/" + $.asFormUrl + "/" + url,
    //                type: "get",
    //                success: function (data) {
    //                    $partial.html(data.page);

    //                }
    //            })
    //        }

    //        // allow jQuery chaining
    //        return this;
    //    };
})(jQuery);
(function ($) {
    $.fn.asLogin = function (params) {
        if (typeof (Storage) !== "undefined") {
            var $elamn = $.as(this);
            
            if ($.type(params) === "string") {
                //if (params === "logout")
                //    $.asStorage.removeItem($.asLoginAccessToken)
            } else {
                var defaultsParams = $.extend({ url: $.asAccountManager.loginServiceUrl, accessToken: 'loginAccessToken' }, params);
                $.asLoginAccessToken = defaultsParams.accessToken

                var user = defaultsParams.$form.asSerializeObject()
                var loginData = {
                    grant_type: 'password',

                    username: user.username,

                    password: user.password

                };

                $elamn.asAjax({
                    type: 'POST',

                    url: defaultsParams.url,

                    data: loginData,
                    success: function (data) {
                        if(data.debugId){
                             $.asStorage.setItem($.asDebugId, data.debugId)
                        }
                        $.asAccountManager.setToken(user.username,data.access_token)
                        // $.asCookies.set($.asCoockiesName.asIsAuthenticated, { IsAuthenticated: true }, { expires: 10000000 })
                        // $.asStorage.setItem($.asLoginAccessToken, data.access_token)
                        // $.asStorage.setItem($.asUserName, user.username)

                        $.asShowMessage({ message: $.asRes[$.asLang].loginSuccess })

                        if (defaultsParams.onSuccess)
                            defaultsParams.onSuccess()
                        $($.asEvent.global).trigger($.asEvent.global.login, []);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        // if (jqXHR.responseText != null) {
                            try {
                                    $.asShowMessage({ template: "error", message: JSON.parse(jqXHR.responseJSON.error).asError  })
                                }
                                catch(err) {
                                    $.asShowMessage({ template: "error", message: $.asRes[$.asLang].InvalidLogin })
                                }
                           
                        // }
                    }
                }, { $form: defaultsParams.$form, overlayClass: 'as-overlay-absolute' })
            }
        } else
            $.asShowMessage({ template: "error", message: $.asRes[$.asLang].storageNotSupport })
        return this
    }

})(jQuery);
(function ($) {
    $.fn.asDraggabilly = function (params) {
        var $draggable = $(this)
        if ($.type(params) === "string") {
            return this
        } else {
            var defaultsParams = $.extend({ handle: '.as-handle' }, params);
            this.each(function () {
                $draggable.draggabilly(defaultsParams)
            });
        }

        return this
    }

})(jQuery);
/*
 * loading-overlay
 * https://github.com/jgerigmeyer/jquery-loading-overlay
 *
 * Copyright (c) 2014 Jonny Gerig Meyer
 * Licensed under the MIT license.
 */

(function ($) {

    'use strict';

    var methods = {
        init: function (options) {
            var opts = $.extend({}, $.fn.loadingOverlay.defaults, options);

            var target = $(this).addClass(opts.loadingClass);
            var overlay = '<div class="' + opts.overlayClass + '">' +
              '<p class="' + opts.spinnerClass + '">' +
              '<span class="' + opts.iconClass + '"></span>' +
              '<span class="' + opts.textClass + '">' + opts.loadingText + '</span>' +
              '</p></div>';
            // Don't add duplicate loading-overlay
            if (!target.data(opts.overlayClass)) {
                target.prepend($(overlay)).data(opts.overlayClass, true);
            }
            return target;
        },

        remove: function (options) {
            var opts = $.extend({}, $.fn.loadingOverlay.defaults, options);
            var target = $(this).data(opts.overlayClass, false);
            target.find('.' + opts.overlayClass).detach();
            if (target.hasClass(opts.loadingClass)) {
                target.removeClass(opts.loadingClass);
            } else {
                target.find('.' + opts.loadingClass).removeClass(opts.loadingClass);
            }
            return target;
        },

        // Expose internal methods to allow stubbing in tests
        exposeMethods: function () {
            return methods;
        }
    };

    $.fn.loadingOverlay = function (method) {
        if (methods[method]) {
            return methods[method].apply(
              this,
              Array.prototype.slice.call(arguments, 1)
            );
        } else if (typeof method === 'object' || !method) {
            return methods.init.apply(this, arguments);
        } else {
            $.error('Method ' + method + ' does not exist on jQuery.loadingOverlay');
        }
    };

    /* Setup plugin defaults */
    $.fn.loadingOverlay.defaults = {
        loadingClass: 'loading',          // Class added to target while loading
        overlayClass: 'loading-overlay',  // Class added to overlay (style with CSS)
        spinnerClass: 'loading-spinner',  // Class added to loading overlay spinner
        iconClass: 'loading-icon',        // Class added to loading overlay spinner
        textClass: 'loading-text',        // Class added to loading overlay spinner
        loadingText: 'loading'            // Text within loading overlay
    };

}(jQuery));

//#region LoadCssJs


(function ($) {

    $.fn.asLoadModule = function (options) {
        var $page = $(this)
        var counterClass = ($page.selector + "conter").replace(/\./g, "")
        var counterId = "." + counterClass


        $($page).loadingOverlay({ loadingText: $.asWaitingView.replace("@name", counterClass), loadingClass: 'as-loading-module', overlayClass: 'as-overlay-module' });

        var pageEvent = $.asGetPageEventName(this)

        var prm = $.extend({ loadedCount: 0, mustLoad: options.urls.length, baceScriptUrl:$.asGetScriptPath(), baceStyleUrl: $.asGetStylePath(), pageEvent: pageEvent }, options);

        $(counterId).html(prm.loadedCount + "/" + prm.mustLoad)
        var callBackFunc = function () {
            ++prm.loadedCount;
            $(counterId).html(prm.loadedCount + "/" + prm.mustLoad)

            $(prm.pageEvent).trigger($.asEvent.page.loading, [prm.mustLoad, prm.loadedCount])
            //$.asLoaderCssJs.loadingCssJs.pop();
            if (prm.mustLoad === prm.loadedCount) {
                $.each(prm.urls, function (index, value) {
                    $.asModule.loaded[value.url.toLowerCase()] = true
                })
            
                prm.loadedCallback();
                $($page).loadingOverlay('remove', { loadingClass: 'as-loading-module', overlayClass: 'as-overlay-module' });
            }

        };

        var callBackErrorFunc = function () {
            $.asShowMessage({ template: "error", message: $.asRes[$.asLang].ajaxError + " " + $.asRes[$.asLang].pleaseRetry });
        };
        
        if(prm.urls.length === 0){
            prm.loadedCount--
             callBackFunc()
        }
               

        var loadCss = function (params) {

            //var idCount = 0
            var loadedStates = {}, maxLoadTime = 10000;

            load(params.url, params.loadedCallback, params.errorCallback)


            function appendToHead(node) {
                document.getElementsByTagName('head')[0].appendChild(node);
            }


            function wrappedSetTimeout(callback, time) {
                if (typeof time != 'number') {
                    time = 0;
                }

                return setTimeout(callback, time);
            }

            /**
                 * Loads the specified css style sheet file and call the loadedCallback once it's finished loading.
                 *
                 * @method load
                 * @param {String} url Url to be loaded.
                 * @param {Function} loadedCallback Callback to be executed when loaded.
                 * @param {Function} errorCallback Callback to be executed when failed loading.
                 */
            function load(url, loadedCallback, errorCallback) {
                var style, state;

                var link = document.createElement('link');
                link.rel = 'stylesheet';
                link.type = 'text/css';
                link.id = $.asuniqueId();
                link.async = false;
                link.defer = false;
                var startTime = new Date().getTime();

                function passed() {
                    var callbacks = state.passed, i = callbacks.length;

                    while (i--) {
                        callbacks[i]();
                    }

                    state.status = 2;
                    state.passed = [];
                    state.failed = [];
                }

                function failed() {
                    var callbacks = state.failed, i = callbacks.length;

                    while (i--) {
                        callbacks[i]();
                    }

                    state.status = 3;
                    state.passed = [];
                    state.failed = [];
                }

                // Sniffs for older WebKit versions that have the link.onload but a broken one
                function isOldWebKit() {
                    var webKitChunks = navigator.userAgent.match(/WebKit\/(\d*)/);
                    return !!(webKitChunks && webKitChunks[1] < 536);
                }

                // Calls the waitCallback until the test returns true or the timeout occurs
                function wait(testCallback, waitCallback) {
                    if (!testCallback()) {
                        // Wait for timeout
                        if ((new Date().getTime()) - startTime < maxLoadTime) {
                            wrappedSetTimeout(waitCallback);
                        } else {
                            failed();
                        }
                    }
                }

                // Workaround for WebKit that doesn't properly support the onload event for link elements
                // Or WebKit that fires the onload event before the StyleSheet is added to the document
                function waitForWebKitLinkLoaded() {
                    wait(function () {
                        var styleSheets = document.styleSheets, styleSheet, i = styleSheets.length, owner;

                        while (i--) {
                            styleSheet = styleSheets[i];
                            owner = styleSheet.ownerNode ? styleSheet.ownerNode : styleSheet.owningElement;
                            if (owner && owner.id === link.id) {
                                passed();
                                return true;
                            }
                        }
                    }, waitForWebKitLinkLoaded);
                }

                // Workaround for older Geckos that doesn't have any onload event for StyleSheets
                function waitForGeckoLinkLoaded() {
                    wait(function () {
                        try {
                            // Accessing the cssRules will throw an exception until the CSS file is loaded
                            var cssRules = style.sheet.cssRules;
                            passed();
                            return !!cssRules;
                        } catch (ex) {
                            // Ignore
                        }
                    }, waitForGeckoLinkLoaded);
                }

                url = $.asAddCacheSuffix(url);

                if (!loadedStates[url]) {
                    state = {
                        passed: [],
                        failed: []
                    };

                    loadedStates[url] = state;
                } else {
                    state = loadedStates[url];
                }

                if (loadedCallback) {
                    state.passed.push(loadedCallback);
                }

                if (errorCallback) {
                    state.failed.push(errorCallback);
                }

                // Is loading wait for it to pass
                if (state.status == 1) {
                    return;
                }

                // Has finished loading and was success
                if (state.status == 2) {
                    passed();
                    return;
                }

                // Has finished loading and was a failure
                if (state.status == 3) {
                    failed();
                    return;
                }

                // Start loading
                state.status = 1;


                // Feature detect onload on link element and sniff older webkits since it has an broken onload event
                if ("onload" in link && !isOldWebKit()) {
                    link.onload = waitForWebKitLinkLoaded;
                    link.onerror = failed;
                } else {
                    // Sniff for old Firefox that doesn't support the onload event on link elements
                    // TODO: Remove this in the future when everyone uses modern browsers
                    if (navigator.userAgent.indexOf("Firefox") > 0) {
                        style = document.createElement('style');
                        style.textContent = '@import "' + url + '"';
                        waitForGeckoLinkLoaded();
                        appendToHead(style);
                        return;
                    }

                    // Use the id owner on older webkits
                    waitForWebKitLinkLoaded();
                }

                appendToHead(link);
                link.href = url;
            }
        };

        var loadJs = function (params) {


            /**
    * ScriptLoader.js
    *
    * Released under LGPL License.
    * Copyright (c) 1999-2015 Ephox Corp. All rights reserved
    *
    * License: http://www.tinymce.com/license
    * Contributing: http://www.tinymce.com/contributing
    */

            /*globals console*/

            /**
         * This class handles asynchronous/synchronous loading of JavaScript files it will execute callbacks
         * when various items gets loaded. This class is useful to load external JavaScript files.
         *
         * @class tinymce.dom.ScriptLoader
         * @example
         * // Load a script from a specific URL using the global script loader
         * tinymce.ScriptLoader.load('somescript.js');
         *
         * // Load a script using a unique instance of the script loader
         * var scriptLoader = new tinymce.dom.ScriptLoader();
         *
         * scriptLoader.load('somescript.js');
         *
         * // Load multiple scripts
         * var scriptLoader = new tinymce.dom.ScriptLoader();
         *
         * scriptLoader.add('somescript1.js');
         * scriptLoader.add('somescript2.js');
         * scriptLoader.add('somescript3.js');
         *
         * scriptLoader.loadQueue(function() {
         *    alert('All scripts are now loaded.');
         * });
         */

            var each = $.asEach, grep = $.asFilter;
            var QUEUED = 0,
                LOADING = 1,
                LOADED = 2,
                states = {},
                queue = [],
                scriptLoadedCallbacks = {},
                queueLoadedCallbacks = [],
                loading = 0,
                undef;


            loadScript(params.url, function () {
                //$.asLoaderCssJs.loadingCssJs.pop();
                params.loadedCallback()
            });


            /**
                 * Loads a specific script directly without adding it to the load queue.
                 *
                 * @method load
                 * @param {String} url Absolute URL to script to add.
                 * @param {function} callback Optional callback function to execute ones this script gets loaded.
                 */
            function loadScript(url, callback) {

                //var dom = DOM, elm, id;
                var id = $.asuniqueId();

                // Create new script element
                var elm = document.createElement('script');
                elm.id = id;
                elm.type = 'text/javascript';
                elm.src = $.asAddCacheSuffix(url);

                // Execute callback when script is loaded
                function done() {
                    //dom.remove(id);
                    $("#" + id).remove();
                    if (elm) {
                        elm.onreadystatechange = elm.onload = elm = null;
                    }

                    callback();
                }

                function error() {
                    /*eslint no-console:0 */

                    // Report the error so it's easier for people to spot loading errors
                    //if (typeof console !== "undefined" && console.log) {
                    //    console.log("Failed to load: " + url);
                    $.asShowMessage({ template: "error", message: "Failed to load: " + url });
                    //}

                    // We can't mark it as done if there is a load error since
                    // A) We don't want to produce 404 errors on the server and
                    // B) the onerror event won't fire on all browsers.
                    // done();
                }



                // Seems that onreadystatechange works better on IE 10 onload seems to fire incorrectly
                if ("onreadystatechange" in elm) {
                    elm.onreadystatechange = function () {
                        if (/loaded|complete/.test(elm.readyState)) {
                            done();
                        }
                    };
                } else {
                    elm.onload = done;
                }

                // Add onerror event will get fired on some browsers but not all of them
                elm.onerror = error;

                // Add script to document
                (document.getElementsByTagName('head')[0] || document.body).appendChild(elm);
            }

            /**
         * Returns true/false if a script has been loaded or not.
         *
         * @method isDone
         * @param {String} url URL to check for.
         * @return {Boolean} true/false if the URL is loaded.
         */
            var isDone = function (url) {
                return states[url] == LOADED;
            };

            /**
         * Marks a specific script to be loaded. This can be useful if a script got loaded outside
         * the script loader or to skip it from loading some script.
         *
         * @method markDone
         * @param {string} url Absolute URL to the script to mark as loaded.
         */
            var markDone = function (url) {
                states[url] = LOADED;
            };

            /**
         * Adds a specific script to the load queue of the script loader.
         *
         * @method add
         * @param {String} url Absolute URL to script to add.
         * @param {function} callback Optional callback function to execute ones this script gets loaded.
         * @param {Object} scope Optional scope to execute callback in.
         */
            var add
            var load = add = function (url, callback, scope) {
                var state = states[url];

                // Add url to load queue
                if (state == undef) {
                    queue.push(url);
                    states[url] = QUEUED;
                }

                if (callback) {
                    // Store away callback for later execution
                    if (!scriptLoadedCallbacks[url]) {
                        scriptLoadedCallbacks[url] = [];
                    }

                    scriptLoadedCallbacks[url].push({
                        func: callback,
                        scope: scope || this
                    });
                }
            };
            /**
* Loads the specified queue of files and executes the callback ones they are loaded.
* This method is generally not used outside this class but it might be useful in some scenarios.
*
* @method loadScripts
* @param {Array} scripts Array of queue items to load.
* @param {function} callback Optional callback to execute ones all items are loaded.
* @param {Object} scope Optional scope to execute callback in.
*/
            var loadScripts = function (scripts, callback, scope) {


                function execScriptLoadedCallbacks(url) {
                    // Execute URL callback functions
                    each(scriptLoadedCallbacks[url], function (callback) {
                        callback.func.call(callback.scope);
                    });

                    scriptLoadedCallbacks[url] = undef;
                }

                queueLoadedCallbacks.push({
                    func: callback,
                    scope: scope || this
                });

                var loadScripts = function () {
                    var loadingScripts = grep(scripts);

                    // Current scripts has been handled
                    scripts.length = 0;

                    // Load scripts that needs to be loaded
                    each(loadingScripts, function (url) {
                        // Script is already loaded then execute script callbacks directly
                        if (states[url] == LOADED) {
                            execScriptLoadedCallbacks(url);
                            return;
                        }

                        // Is script not loading then start loading it
                        if (states[url] != LOADING) {
                            states[url] = LOADING;
                            loading++;

                            loadScript(url, function () {
                                states[url] = LOADED;
                                loading--;

                                execScriptLoadedCallbacks(url);

                                // Load more scripts if they where added by the recently loaded script
                                loadScripts();
                            });
                        }
                    });

                    // No scripts are currently loading then execute all pending queue loaded callbacks
                    if (!loading) {
                        each(queueLoadedCallbacks, function (callback) {
                            callback.func.call(callback.scope);
                        });

                        queueLoadedCallbacks.length = 0;
                    }
                };

                loadScripts();
            };

            /**
         * Starts the loading of the queue.
         *
         * @method loadQueue
         * @param {function} callback Optional callback to execute when all queued items are loaded.
         * @param {Object} scope Optional scope to execute the callback in.
         */
            var loadQueue = function (callback, scope) {
                loadScripts(queue, callback, scope);
            };


        };

        $.each(prm.urls, function (index, value) {
            if (value.url.indexOf("?") > -1)
                value = $.extend({ kind: value.url.substring(value.url.lastIndexOf(".") + 1, value.url.indexOf("?")) }, value);
            else
                value = $.extend({ kind: value.url.substring(value.url.lastIndexOf(".") + 1) }, value);
                
            //  if(value.url.toLowerCase().indexOf("?") > -1){
            //           var currentUrlParts = value.url.toLowerCase().split("?")
            //           $.each($.asModule.loaded, function (index, value) {
            //               var moduleNameParts = index.split("?")
            //               if (moduleNameParts.length === 2) {
            //                   if (currentUrlParts[0] === moduleNameParts[0] && currentUrlParts[1] !== moduleNameParts[1])
            //                       location.reload()
            //                       return;
            //               }
            //           })
            //       }
                 
            if (value.kind === "css") {
                //if ($.inArray(value.url, $.asLoaderCssJs.currentStyle) >= 0) {
                //    callBackFunc()
                //} else {
                //$.asLoaderCssJs.currentStyle.push(value.url)
                //$.asLoaderCssJs.loadingCssJs.push(value.url)
                
                if ($.asModule.loaded[value.url.toLowerCase()] !== true) {
                  
                    loadCss({
                        url: (value.url[0] === "/" || value.url[0] === "~" ) ? value.url : prm.baceStyleUrl + value.url,
                        loadedCallback: callBackFunc,
                        errorCallback: callBackErrorFunc
                    })
                }
                else{
                      
                    callBackFunc()
                }
                //}
            } else {
                //if ($.inArray(value.url, $.asLoaderCssJs.currentScript) >= 0) {
                //    callBackFunc()
                //} else {
                //    $.asLoaderCssJs.currentScript.push(value.url)
                //    $.asLoaderCssJs.loadingCssJs.push(value.url)
                
                
                //   if ($.asModule.loaded[value.url.toLowerCase()] !== true) {
  
                 loadJs({
                     url: (value.url[0] === "/" || value.url[0] === "~") ? value.url : prm.baceScriptUrl + value.url,
                     loadedCallback: callBackFunc,
                     errorCallback: callBackErrorFunc
                 })
                // }
                // else{
                       
                //     callBackFunc()
                // }
            

                //}
            }

        });

        return this;
    };
})(jQuery);
(function ($) {
    "use strict";
    $.fn.asRegisterPageEvent = function (params) {
        
        var $plugin = $.as(this);
        var source, dataAdepter, page = this
        

        if ($.type(params) === "string") {

        } else {
            var loadComplete = function (data) {

      
            }

            params = $.extend({ source:{}}, params);
            source = $.extend({ datatype: "json", order: 'asc', orderby: 'order', loadComplete: loadComplete, page: page ,localData: {name:"test",order:"1"}}, params.source);
         
            dataAdepter = $.extend({ extraSettings: { loadingText: $.asWaitingViewSmall } }, params.dataAdepter)
     



            if (source.orderbyDesc) {
                source.orderby = source.orderbyDesc
            }
            delete source.orderbyDesc
            delete params.source;
            delete params.dataAdapter;
            // traverse all nodes
            this.each(function () {

                    $plugin = $.as(this);
                    $plugin.asDataAdepter(source, dataAdepter)

            });

        }
        // allow jQuery chaining
        return this;
    };
})(jQuery);
//#endregion

 //#region polyfill for ie < 9
    Array.prototype.indexOf || (Array.prototype.indexOf = function (d, e) {
        var a;
        if (null == this) throw new TypeError('"this" is null or not defined');
        var c = Object(this),
            b = c.length >>> 0;
        if (0 === b) return -1;
        a = +e || 0;
        Infinity === Math.abs(a) && (a = 0);
        if (a >= b) return -1;
        for (a = Math.max(0 <= a ? a : b - Math.abs(a), 0) ; a < b;) {
            if (a in c && c[a] === d) return a;
            a++
        }
        return -1
    });
    //#region polyfill




