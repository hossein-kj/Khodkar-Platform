 $('#i55c6d80fc41f46debe942fadf98be42c').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('Login');var asPageEvent = '#i55c6d80fc41f46debe942fadf98be42c'; var asPageId = '.i55c6d80fc41f46debe942fadf98be42c.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({}, $.asUrls); 
      var 
$btnlogin=as("#login_login"),
$frmUser=as("#login_user"),
$chkDebug=as("#login_debug"),
formClickCount=0;
      


          



       
        

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
  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });