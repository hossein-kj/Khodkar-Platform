
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
    

