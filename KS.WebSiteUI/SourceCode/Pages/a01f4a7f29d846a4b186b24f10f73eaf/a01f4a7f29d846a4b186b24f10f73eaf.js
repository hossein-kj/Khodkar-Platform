var 
    $drpLanguge= as("#defaultTemplate_drp_languge"),
    $winLogin= $.asModalManager.get({url:$.asModalManager.urls.login,isGlobal:true}),
    $mainMenu=as("#defaultTemplate_menu"),
    menuUrl= $.asInitService($.asUrls.cms_link_public_getAll,[{name:'@lang',value:$.asLang},{name:'@isMobile',value:false}]),
  
    $mainDefaultMegaMenu=as('#defaultTemplate_mega_menu');


//if($.asIsAuthenticated()){
//   $(".top-link-box").html(' <div div style="float: left;margin-top: 0;margin-left: 10px" id="defaultTemplate_arrow_profile"><i class="as-mainprofileimagearrow glyphicon glyphicon-chevron-down"></i><img style="height: 45px" src="/Content/images/user.png" /> </div>')
//var userCardTemplate = [
//                '<div class="as-user-card">',
//                    '<div class="as-avatar"><img src="/Content/images/user-big.png" /></div>',
//                    '<div>',
//                       '<ul style="list-style-type: none">',
//                            '<li><i class="glyphicon glyphicon-user"></i>&nbsp;' + $.asRes[$.asLang].profile + '</li>',
//                            '<li><i class="glyphicon glyphicon-cog"></i> &nbsp;' + $.asRes[$.asLang].settings + '</li>',
//                            '<li onclick=""><i class="glyphicon glyphicon-log-out"></i>&nbsp;' + $.asRes[$.asLang].logOff + '</li>',
//                        '</ul>',
//                    '</div>',
//                         '<div class="col-lg-2"></div>',
//                 '</div>'
// ].join('')
// $("#defaultTemplate_arrow_profile").asPopovers({ content: userCardTemplate })

//}



if ($.asIsAuthenticated()) {
    menuUrl = $.asInitService($.asUrls.cms_link_userMenus,[{name:'@lang',value:$.asLang},{name:'@isMobile',value:false}]),

    as("#defaultTemplate_login_link").html(' <a class="top-link-divider" href="/logoff/"  >Logoff</a>')
           var loadLinks = function (){
      return $.asAjaxTask({
     url:$.asInitService($.asUrls.cms_link_userAccess,[{name:'@lang',value:$.asLang},{name:'@isMobile',value:false}])
      });
}
      as("#defaultTemplate_drp_menu").asAfterTasks([
    loadLinks()
], function (links) {
    
            $.asEach(links, function (link) {
          if(link.Link.Url.toLowerCase() === '#admin')
          link.Link.Text = $.asStorage.getItem($.asUserName);
           
        });
    as("#defaultTemplate_drp_menu").asDropdown({
        source: {
           hierarchy:
            {
                type: 'flat',
                keyDataField: { name: 'Link.Id' },
                parentDataField: { name: 'Link.ParentId' },
                isLeafDataField: 'Link.IsLeaf',
               childrenDataField: 'Children',
                removeChildLessParent:true
            },
            valueDataField: 'Link.Url',
            orderby: 'Link.Order',
            displayDataField: 'Link.Text',
           localData:links
        },
        link: true,
        parentMode: "uniq",
        selectParents: false,
        moveByFixedNav: {
            initialTop: 86
        }
    })
}
,{overlayClass:'as-overlay-relative',waitingView:$.asWaitingViewSmall});
}










$winLogin.asModal(
    //{backdrop:'static'}
    )


var loadLanguages = function () {
    return $.asAjaxTask({
        url: $.asUrls.cms_languageAndCulture_public_getAll
    });
}

$drpLanguge.asAfterTasks([
    loadLanguages()
], function (languages) {
    $.asLanguageAndCulture = languages;
    var langParams = {
        source: {
            displayDataField: 'country',
            valueDataField: 'language',
            urlDataField: 'flagUrl',
            idDataField: 'culture',
            exteraDataField: 'isRightToLeft',
            localData: languages
        }
    }



    var lang = $.asGetDefaultsLnguageAndCulter()
    if (lang) {
        langParams.selectedValue = lang.country
    }

    $drpLanguge.asFlexSelect(langParams);


    $drpLanguge.on("selectedIndexChanged", function (event, id, value, text, url, extera) {
        if (url)
            $drpLanguge.asFlexSelect('setItem', '<img src="' + url + '">&nbsp;<span class="caret"></span>')
        $.asSetLanguageAndCulter(value, id, text, extera);

    });

}, { overlayClass: "as-overlay-absolute" })

$mainMenu.asFixedNav()


var loadMenus = function (url) {
    return $.asAjaxTask({
        url: url
    });
}

$mainMenu.asAfterTasks([
    loadMenus(menuUrl)
], function (menusData) {
    var slideMenuSetting = {
        source: {
            text:'Text',
            href:'Url',
            children:'Children',
            hierarchy:
               {
                   type: 'flat',
                   keyDataField: { name: 'Id' },
                   parentDataField: { name: 'ParentId' },
                    childrenDataField: 'Children',
                   removeChildLessParent:true,
                   convertchildByZeroLengthToNull:true
               },
            localData: menusData,
        },
        smart: {
            enable: true,
            anchors: false,
            close: true,
            open: false,
            remember: true,
            scroll: true
        },
        classes: "fon-rounded fon-shadow"
    }
    $mainMenu.asOffCanvas(slideMenuSetting)


if (!(bowser.msie && bowser.version <= 9))
$mainDefaultMegaMenu.asMegaMenu({
    source: {
        hierarchy:
           {
               type: 'flat',
               keyDataField: { name: 'Id' },
               parentDataField: { name: 'ParentId' },
                childrenDataField: 'Children'
                ,
                 removeChildLessParent:true
           },
       localData: menusData
    },
    cssClass:''
})




}, { overlayClass: "as-overlay-absolute" })

as("#lnkLogin").click(function(){
    $winLogin.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.login)},{name:"@isModal",value:true}]));
})



