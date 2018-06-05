(function ($) {
    "use strict";
    $.fn.asCodeEditor = function (params, value,options) {
        var $editor = $.as(this),mode = "ace/mode/";
        if ($.type(params) === "string") {
            //if (params === "getValue") {
            //   return ace.edit($editor.prop('id')).getValue();
            //} else {
            //    return ace.edit($editor.prop('id'))[params](value);
            //}
            if (params === "editor")
                return ace.edit($editor.prop('id'))
            else if (params === "setEditorMode") {
                return ace.edit($editor.prop('id')).getSession().setMode(mode + value);
            }
            return ace.edit($editor.prop('id'))[params](value, options);
        } else {
            ace.require("ace/theme/tomorrow");
            ace.require("ace/mode/javascript");
            this.each(function() {
                $editor = $.as(this);

                var defaultsParam = $.extend({
                    theme: "ace/theme/tomorrow",
                    mode: "javascript",
                    enableBasicAutocompletion: true,
                    enableLiveAutocompletion: true,
                    scriptPath: $.asGetScriptPath() +  "codeeditor/"
                }, params);
                $.asCodeEditorScriptPath = $.asCodeEditorScriptPath || defaultsParam.scriptPath;
                var editor = ace.edit($editor.prop('id'));
                editor.setOptions({
                    enableBasicAutocompletion: defaultsParam.enableBasicAutocompletion,
                    //enableSnippets: true,
                    enableLiveAutocompletion: defaultsParam.enableLiveAutocompletion
                });

                editor.setTheme(defaultsParam.theme)
                //if (defaultsParam.mode === 'css')
                //    defaultsParam.mode = "ace/mode/css"
                //else if (defaultsParam.mode === 'html')
                //    defaultsParam.mode = "ace/mode/html"
                defaultsParam.mode = mode + defaultsParam.mode
                editor.session.setMode(defaultsParam.mode)
                editor.$blockScrolling = Infinity
                if (defaultsParam.wysiwygEditor)
                    $("#" + $editor.prop('id')).keypress(function(e) {
                        if (e.which == 13) {
                            tinymce.get(defaultsParam.wysiwygEditor).setContent(editor.getValue())
                        }
                    });
            });

        }
        // allow jQuery chaining
        return this;
    };

})(jQuery);