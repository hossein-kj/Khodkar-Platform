 $('#i6fe777e24d9a44118290fd5de1cd1eb9').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('ورود اعضاء');var asPageEvent = '#i6fe777e24d9a44118290fd5de1cd1eb9'; var asPageId = '.i6fe777e24d9a44118290fd5de1cd1eb9.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({}, $.asUrls); var 
// $win=as("#defaultTemplate_login"),
$win=$(asPageId),
$btnCancel=as("#securityLogin_cancel"),
$btnlogin=as("#securityLogin_login"),
        //var $txtUserName = $(formId + "#securityLogin_username")
        //var $txtPass = $(formId + "#securityLogin_password")
$frmUser=as("#securityLogin_user"),
$chkDebug=as("#chkDebug"),
formClickCount=0;
 


          
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
        

           $frmUser.asValidate({
                rules: {
                    login_username: {
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
    

   
  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });