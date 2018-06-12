 $('#i667b8403d2e14b38b4fede2e76585a51').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('ورود اعضاء');var asPageEvent = '#i667b8403d2e14b38b4fede2e76585a51'; var asPageId = '.i667b8403d2e14b38b4fede2e76585a51.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({}, $.asUrls); 
      var 
$btnlogin = as("#login_login"),
$frmUser=as("#login_user"),
$chkDebug=as("#login_debug"),
formClickCount=0;
      

        var bindEvent = function () {
            $frmUser.click(function(){
                formClickCount++;
                if(formClickCount >= 15)
                {
                    as("#login_debugContainer").css({"display":"inline"});
                }
            });
            $btnlogin.on('click', function() {
            $.asDebugMode($chkDebug.is(':checked'));
                //if ($frmUser.valid())
                //    alert("yt")
                $frmUser.asLogin({
                    $form: $frmUser
                });
            });
        }

          



            bindEvent()
     

           $frmUser.asValidate({
                rules: {
                    login_username: {
                        required: true,
                        minlength: 5,
                        maxlength: 45,
                        email: true
                    },
                    login_password: {
                        required: true,
                        minlength: 6,
                        maxlength: 20
                    }
                    //,
                    //securityLoginTxtNcode: {
                    //    required: true,
                    //    minlength: 2
                    //}
                }
                //,
                //messages: {
                //    firstname: "Please enter your firstname"
                //}
            }
          );
    

  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });