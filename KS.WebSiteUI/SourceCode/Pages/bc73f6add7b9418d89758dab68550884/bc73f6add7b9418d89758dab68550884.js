  $.asInitApp({
                appName: 'khodkarSystem',
                 lang: "en",
                culture: "US",
                country: "United States",
                rightToLeft: false,
                version:1,
                placeHolder: "#placeHolder",
                 enableMobile:true,
                 reloadUrl:$.asUrls.frameWorkServices_reload,
                changeTemplateUrl:$.asUrls.frameWorkServices_changeTemplate,
                isAuthenticatedUrl:$.asUrls.frameWorkServices_isAuthenticated,
                logOffUrl:$.asUrls.frameWorkServices_logOff,
                formUrl:$.asUrls.frameWorkServices_getWebPage,
                loginServiceUrl:$.asUrls.frameWorkServices_login,
                unLinkClasses:["fon-has-menu","fon-item-back"],
                modalUrls:{
                    systemReportManager: "en/admin/develop/reports/manager",
                    sourceManager :"en/admin/develop/codes/source-control/manager/",
                    fileSelector: "en/admin/fms/file-selector/",
                    directorySelector:"en/admin/fms/folder-selector/",
                    sourceComparator :"en/admin/develop/codes/source-control/compare/",
                    translator:"en/admin/cms/languageandculture-manager/data-translator/",
                    pathSelector:"en/admin/cms/file-and-path-manager/path-selector/",
                    fileManager:"en/admin/fms/files-folders-manager-for-a-path/",
                    remoteDownloadManager:"en/admin/fms/remote-download-manager/",
                    uploadManager:"en/admin/fms/upload-manager/",
                    fileAddOrUpdate:"en/admin/fms/file-add-or-modify/",
                    unitTestRunner:"en/admin/develop/codes/os/dotnet/unit-test-runner/",
                    changePassword:"en/admin/security/change-password/"
                },
                onFontLoad:function(){
                    var font = new FontFaceObserver('A.Google');
                    font.load("ب", 20000).then(function () {
                        $.asFontLoaded = true;
                    }, function () {
                        $.asShowMessage({ template: "error", message: 'Font A.Google is not available' });
                    });
                },onFontLoaded:function(){
                    if( $.asFontLoaded === true){
                         $(".fonts-a-google-pre").addClass("fonts-a-google")
                         $(".fonts-a-google").removeClass("fonts-a-google-pre")
                    }

                }
            });