var 
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
    