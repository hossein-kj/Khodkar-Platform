 $('#iebce1ef7a2c24f69b98ccd8888e365e1').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('Change Password');var asPageEvent = '#iebce1ef7a2c24f69b98ccd8888e365e1'; var asPageId = '.iebce1ef7a2c24f69b98ccd8888e365e1.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({security_User_ChangePassword:"/security/user/changePassword"}, $.asUrls); var 
// $win=as("#defaultTemplate_login"),
$win=$(asPageId),
$btnCancel=as("#btnCancel"),
$btnChange=as("#btnChange"),
$frmChangePass=as("#frmChangePass");
 
          
     var bindEvent = function () {
         
            $btnCancel.on('click', function () {
                        $win.asModal('hide');
                });
                    
            $btnChange.click(function () {
            
             $win.asAjax({
                url: $.asUrls.security_User_ChangePassword,
                data: JSON.stringify({
                    OldPassword:as("#txtOldPassword").val(),
                    NewPassword:as("#txtPassword").val()
                }),
                success: function (result) {
                  $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
                }
            },{$form: $frmChangePass,overlayClass: "as-overlay-absolute"});
              
        });
    }

     bindEvent()
        

           $frmChangePass.asValidate({
                rules: {
                    txtPassword: {
                        required: true,
                        minlength: 6,
                        maxlength: 20
                    },
                 txtPasswordAgain: {
                     equalTo: as("#txtPassword").selector
                    }
                }
            }
          );
          //console.dir($frmChangePass.asSerializeObject())
      ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });