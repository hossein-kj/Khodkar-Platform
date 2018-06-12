 $('#i86892370946f4e08b0c45a32c3851967').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('Login');var asPageEvent = '#i86892370946f4e08b0c45a32c3851967'; var asPageId = '.i86892370946f4e08b0c45a32c3851967.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({}, $.asUrls); var 
$win=$(asPageId),
$btnCancel=as("#securityLogin_cancel"),
$btnlogin=as("#securityLogin_login"),
        //var $txtUserName = $(formId + "#securityLogin_username")
        //var $txtPass = $(formId + "#securityLogin_password")
$frmUser=as("#securityLogin_user"),
$chkDebug=as("#chkDebug"),
formClickCount=0;
 


          



            
       

            $frmUser.asValidate({
                rules: {
                    securityLogin_username: {
                        required: true,
                        minlength: 5,
                        maxlength: 45,
                        email: true
                    },
                    securityLogin_password: {
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
          //console.dir($frmUser.asSerializeObject())
    

        var bindEvent = function () {
                 $frmUser.click(function(){
                formClickCount++;
                if(formClickCount >= 15)
                {
                    as("#login_debugContainer").css({"display":"block"});
                }
            });
            $btnCancel.on('click', function () {
                $win.asModal('hide');
            })
            $btnlogin.on('click', function() {
          $.asDebugMode($chkDebug.is(':checked'));
                //if ($frmUser.valid())
                //    alert("yt")
                $win.asLogin({
                    $form: $frmUser, onSuccess: function () {
                        $win.asModal('hide');
                    }
                });
            })
        }
        bindEvent()
  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });