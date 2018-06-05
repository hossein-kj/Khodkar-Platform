     
         $.asInitApp({
                appName: 'khodkarSystem',
                lang: 'fa',
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
                    systemReportManager: "fa/admin/develop/reports/manager",
                    fileSelector: "fa/admin//fms/file-selector/",
                    directorySelector:"fa/admin/fms/folder-selector/",
                    pathSelector:"fa/admin/cms/file-and-path-manager/path-selector/",
                    fileManager:"fa/admin/fms/files-folders-manager-for-a-path/",
                    translator:"fa/admin/cms/languageandculture-manager/data-translator/",
                    fileAddOrUpdate:"fa/admin/fms/file-add-or-modify/",
                    sourceManager :"fa/admin/develop/codes/source-control/manager/",
                    sourceComparator :"fa/admin/develop/codes/source-control/compare/",
                    unitTestRunner:"fa/admin/develop/codes/os/dotnet/unit-test-runner/",
                    remoteDownloadManager:"fa/admin/fms/remote-download-manager/",
                    uploadManager:"fa/admin/fms/upload-manager/",
                    changePassword:"fa/admin/security/change-password/"
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
      
 
 
    