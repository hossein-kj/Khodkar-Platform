(function ($) {
    "use strict";
    // jQuery plugin definition
    $.fn.asValidate = function (params) {

        /*
 * Translated default messages for the jQuery validation plugin.
 * Locale: FA (Persian; فارسی)
 */
        $.extend($.validator.messages, $.asRes[$.asLang].validate ,
        {
            required: $.asRes[$.asLang].validate.required,
            remote: $.asRes[$.asLang].validate.remote,
            email: $.asRes[$.asLang].validate.email,
            url: $.asRes[$.asLang].validate.url,
            date: $.asRes[$.asLang].validate.date,
            dateFA: $.asRes[$.asLang].validate.dateFA,
            dateISO: $.asRes[$.asLang].validate.dateISO,
            number: $.asRes[$.asLang].validate.number,
            digits: $.asRes[$.asLang].validate.digits,
            creditcard: $.asRes[$.asLang].validate.creditcard,
            equalTo: $.asRes[$.asLang].validate.equalTo,
            extension: $.asRes[$.asLang].validate.extension,
            maxlength: $.validator.format($.asRes[$.asLang].validate.maxlength),
            minlength: $.validator.format($.asRes[$.asLang].validate.minlength),
            rangelength: $.validator.format($.asRes[$.asLang].validate.rangelength),
            range: $.validator.format($.asRes[$.asLang].validate.range),
            max: $.validator.format($.asRes[$.asLang].validate.max),
            min: $.validator.format($.asRes[$.asLang].validate.min),
            minWords: $.validator.format($.asRes[$.asLang].validate.minWords),
            maxWords: $.validator.format($.asRes[$.asLang].validate.maxWords)
        });

        $.validator.defaults = $.extend($.validator.defaults, { errorClass: "as-validate-error", validClass: "as-validate-valid" });

        var $form = $.as(this);
        if ($.type(params) === "string") {
            if (params === "valid") {

                //if (!$.data(this[0], "repeatedValidte")) {
                //    var validator = $.data(this[0], "validator");
                //    if (validator) {
                //        $.data(this[0], "rules", validator.settings.rules)
                //        $.each(validator.settings.rules, function(key, val) {
                //            delete val.asType
                //        })
                //    }
                //    $.data(this[0], "repeatedValidte", true)
                //}
                //console.dir($.data(this[0], "rules"))
            
                //console.dir($.data($form.get(0), "rules"))
                $.each($.data($form.get(0), "rules"), function (key, val) {
                    if (val.type === 'asDropdown') {
                       
                        var selected = $("#" + val.id.replace('_as_hiden', '')).asDropdown('selected')
                        //console.dir(selected)
                            if (selected)
                                $("#" + val.id).val(selected)
                            else
                                $("#" + val.id).val('')
                        }              
                })
        
                //var result = $form.valid()
                //if (result)
                //    $(".error").html('')
                var result = $form.valid()
                if (result === false)
                    $.asShowMessage({ template: "error", message: $.asRes[$.asLang].validateError });
                return result
            }else if (params === "validator") {
                return $.validator;
            }
           
        } else {

            //$.asLoadScriptAndStyle({
            //    urls: [
            //        { url: 'asValidate.css', kind: 'css' }
            //    ],
            //    loadedCallback: function() {
                    // merge default and user parameters
                    var defaultsParam = $.extend(params, {
                        ignore: ':hidden:not(".as-validate-hidden")',
                        //errorPlacement: function (error, element) {
                        //    if (element.attr("name").indexOf("_as_hiden") >= 0) {
                        //        $("#" + element.attr("name").replace('_as_hiden', '')).css("border", "1px solid #dc143c")
                        //    }
                        //    this.errorPlacement(error, element)
                        //},
                        // success: function (label) {
                        //    if (label.attr("id").indexOf("_as_hiden") >= 0) {
                        //        alert(label.attr("id").substring(0, label.attr("id").indexOf("_as_hiden")))
                        //        $("#" + label.attr("id").substring(0, label.attr("id").indexOf("_as_hiden"))).css("border", "none")
                        //    }
                        //}

                        highlight: function(element, errorClass, validClass) {
                            //$(element).addClass(errorClass).removeClass(validClass);
                            //$(element.form).find("label[for=" + element.id + "]")
                            //  .addClass(errorClass);
                            $(element).closest('.form-group').addClass('has-error');
                            if (element.type === "radio") {
                                this.findByName(element.name).addClass(errorClass).removeClass(validClass);
                            } else {
                                $(element).addClass(errorClass).removeClass(validClass);
                            }
                            if ($(element).attr("name").indexOf("_as_hiden") >= 0) {
                                var id = $(element).attr("name").replace('_as_hiden', '')
                                $(element.form).find("label[for=" + id + "]")
                                    .addClass(errorClass)
                                $("#" + id).addClass(errorClass);
                            }
                        },
                        unhighlight: function(element, errorClass, validClass) {
                            //$(element).removeClass(errorClass).addClass(validClass);
                            //$(element.form).find("label[for=" + element.id + "]")
                            //  .removeClass(errorClass);
                            $(element).closest('.form-group').removeClass('has-error');
                            if (element.type === "radio") {
                                this.findByName(element.name).removeClass(errorClass).addClass(validClass);
                            } else {
                                $(element).removeClass(errorClass).addClass(validClass);
                            }
                            if (typeof $(element).attr("name") != 'undefined') {
                                if ($(element).attr("name").indexOf("_as_hiden") >= 0) {
                                    var id = $(element).attr("name").replace('_as_hiden', '')
                                    $(element.form).find("label[for=" + id + "]")
                                        .removeClass(errorClass)
                                    $("#" + id).removeClass(errorClass);
                                }
                            }
                        }
                    });

                    //this.each(function () {
                    //    $form = $(this);
                    //$.each(defaultsParam.rules, function (index, rule) {
                    //    if(rule.asType ==='asDropdown')
                    //        rule.
                    //});

                    var newRule = {}
                    var asComponent = []
                    $.each(defaultsParam.rules, function(key, val) {
                        if (val.asType === 'asDropdown') {
                            newRule[key + '_as_hiden'] = val;
                        } else {
                            newRule[key] = val;
                        }

                    })

                    $.each(newRule, function(key, val) {
                        asComponent.push({ id: key, type: val.asType })

                        if (val.asType) {
                            delete val.asType
                        }

                    })
                    //console.dir(this)
                    if (typeof $form.get(0) != 'undefined')
                    $.data($form.get(0), "rules", asComponent)
                   
                    delete defaultsParam.rules;
                    defaultsParam.rules = newRule;
                  return  $form.validate(defaultsParam);
                    //});
            //    }
            //});
        }
        // allow jQuery chaining
        return this;
    };

})(jQuery);

//