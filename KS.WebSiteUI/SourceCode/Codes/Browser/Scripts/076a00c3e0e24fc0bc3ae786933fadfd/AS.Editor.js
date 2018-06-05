(function ($) {
    "use strict";
    $.fn.asEditor = function (params, value) {
       
        var $editor = $.as(this);
        if ($.type(params) === "string") {
            if (params === "setContent")
                tinymce.get($editor.prop('id')).setContent(value)
        } else {
            tinymce.remove();
         
            this.each(function () {
                $editor = $.as(this);
                // merge default and user parameters
                var defaultsParam = $.extend({
                    selector: "#" + $editor.prop('id'),
                    rtl_ui: true,
                    directionality: 'rtl',
                    skin_url:$.asGetStylePath() +'editor/skins/lightgray',
                    theme_url: $.asGetScriptPath() +'editor/themes/modern/theme.js',
                    language_url:$.asGetScriptPath() +'editor/langs/' + $.asLang  + '.js',
                    setup: function (editor) {
                        // editor.on('change', function (e) {

                        // });
                        
                        //     editor.onKeyDown.add(function(ed, event) {

                        //       if (event.keyCode == 13)  {
                        //         // if (tinymce.get($editor.prop('id')) !== null) {
                        //         //      params.htmlEditor.asCodeEditor('setValue', tinymce.get($editor.prop('id')).getContent());
                        //         // }
                        //       }
                        //   });
                    }
                }, params);
                delete defaultsParam.htmlEditor;
                tinymce.init(defaultsParam);
            });

        }
        // allow jQuery chaining
        return this;
    };

})(jQuery);