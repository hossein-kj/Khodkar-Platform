

// include plug-ins
var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var del = require('del');
var minifyCSS = require('gulp-minify-css');
var copy = require('gulp-copy');
var bower = require('gulp-bower');
var sourcemaps = require('gulp-sourcemaps');

var config = {
    libSrc: [
        // 'bower_components/jquery/dist/jquery-2.2.2.min.js',
        'bower_components/bootstrap-sass/assets/javascripts/bootstrap.js',
        //'bower_components/modernizr/modernizr.js',
        //'bower_components/respond-minmax/src/respond.js',
        //'Scripts/Source/App.js',
'bower_components/bowser/bowser.min.js',
//'bower_components/jquery-loading-overlay/src/loading-overlay.js',
//'Scripts/Source/Tools/Core/enquire.min.js',
'Scripts/Source/Tools/Core/jquery.toaster.js',
'Scripts/Source/Tools/Core/Transform/v2/jquery.json2html.js',
'Scripts/Source/Tools/Core/Transform/v2/json2html.js',
'Scripts/Source/Tools/Core/jquery.history.js',
//Slider Issue
'Scripts/Source/Tools/Slider/jquery-transit-modified.js',
'Scripts/Source/Tools/Core/AS.Cookie.js',
'Scripts/Source/Tools/Core/jquery.linq.js',
//'Scripts/Source/Tools/Core/q.min.js',
//'Scripts/Source/Tools/Core/breeze.min.js',
'Scripts/Source/Tools/Core/LazyLoad/jquery.lazyloadxt.js',
'Scripts/Source/Tools/Core/LazyLoad/jquery.lazyloadxt.html.js',
'Scripts/Source/Tools/Core/LazyLoad/jquery.lazyloadxt.bg.js',
'Scripts/Source/Tools/Core/LazyLoad/jquery.lazyloadxt.print.js',
'Scripts/Source/Tools/Core/LazyLoad/jquery.lazyloadxt.srcset.js',
'Scripts/Source/Tools/Core/LazyLoad/jquery.lazyloadxt.video.js',
'Scripts/Source/Tools/Core/LazyLoad/jquery.lazyloadxt.widget.js',
'Scripts/Source/Tools/Core/draggabilly.js'
//'Scripts/Source/Tools/Core/fontfaceobserver.js'
//'Scripts/Source/Tools/AS.CorePlugins.js'
//'Scripts/Source/Tools/AS.Core.js',
//'Scripts/Source/Tools/Core/require.js'
    ],
    asSelectSrc: [
       'Scripts/Source/Tools/Select/bootstrap-select.js',
       'Scripts/Source/Tools/AS.Select.js'
    ],
    asSliderSrc: [
      'Scripts/Source/Tools/Slider/jquery-easing-1.3.js',
      //'Scripts/Source/Tools/Slider/jquery-transit-modified.js',
      'Scripts/Source/Tools/Slider/layerslider.kreaturamedia.jquery.js',
      'Scripts/Source/Tools/Slider/layerslider.transitions.js',
      'Scripts/Source/Tools/AS.Slider.js'
    ],
    asCaptchaSrc: [
   'Scripts/Source/Tools/AS.Captcha.js'
    ],
    asLoginSrc: [
    'Scripts/Source/Tools/AS.Login.js'
    ],
    asPopoversSrc: [
   'Scripts/Source/Tools/AS.Popovers.js'
    ],
    asOffCanvasSrc: [
        'Scripts/Source/Tools/OffCanvas/foonav.js',
        'Scripts/Source/Tools/AS.OffCanvas.js'
    ],
    asMegaMenuSrc: [
        'Scripts/Source/Tools/AS.MegaMenu.js'
    ],
    asFixedNavSrc: [
        'Scripts/Source/Tools/AS.FixedNav.js'
    ],
    asDropdownSrc: [
    'Scripts/Source/Tools/dropdown/jquery.dropdown.js',
     'Scripts/Source/Tools/AS.Dropdown.js'
    ],
    asValidateSrc: [
'Scripts/Source/Tools/Validator/core.js',
'Scripts/Source/Tools/Validator/ajax.js',
 'Scripts/Source/Tools/AS.Validate.js'
    ],
    asEditorSrc: [
'Scripts/Source/Tools/Editor/js/tinymce/tinymce.js',
'Scripts/Source/Tools/AS.Editor.js'
    ],
    asCodeEditorSrc: [
'Scripts/Source/Tools/CodeEditor/src-noconflict/ace.js',
'Scripts/Source/Tools/CodeEditor/src-noconflict/ext-language_tools.js',
'Scripts/Source/Tools/AS.CodeEditor.js'
    ],
    //CORE files that will be combined into a CORE bundle

    siteCoreCssSrc: [
         'Content/Source/Core/3rdParty/bootstrap.min.css',
         //'Content/Source/Core/3rdParty/font-awesome.min.css',
         //'Content/Source/Core/3rdParty/RTL/bootstrap-rtl.min.css',
         //'Content/Source/Core/Tools/DateTimeInput/clockpicker.css',
         //'Content/Source/Core/Tools/Calendar/ui.calendars.picker-rtl.css ',
         //'Content/Source/Core/Tools/Calendar/jquery-ui.structure.css',
         //'Content/Source/Core/Tools/PopUp/bootstrap-modal-bs3patch.css ',
         //'Content/Source/Core/Tools/PopUp/bootstrap-modal.css',
         'Content/Source/Core/core.css',
         //'Content/Source/Core/Customize/Others.css',
         'Content/Source/Core/Customize/Bootstrap/bootstrap.css'
          //'Content/Source/Core/Tools/Slider/layerslider.css',
         //'Content/Source/Core/Tools/Select/bootstrap-select.min.css',
         //'Content/Source/Core/Tools/OffCanvas/foonav.css',
         //'Content/Source/Core/Tools/OffCanvas/foonav.icons.css',
         //'Content/Source/Core/Tools/OffCanvas/themes/foonav.blue.css',
         //'Content/Source/Core/Tools/OffCanvas/themes/foonav.dark.css',
         //'Content/Source/Core/Tools/OffCanvas/themes/foonav.green.css',
         //'Content/Source/Core/Tools/OffCanvas/themes/foonav.light.css',
         //'Content/Source/Core/Tools/OffCanvas/themes/flat-ui/foonav.flat-ui.amethyst.css',
         //'Content/Source/Core/Tools/OffCanvas/themes/flat-ui/foonav.flat-ui.asbestos.css',
         //'Content/Source/Core/Tools/OffCanvas/themes/flat-ui/foonav.flat-ui.asphalt.css',
         //'Content/Source/Core/Tools/OffCanvas/themes/flat-ui/foonav.flat-ui.blue.css',
         //'Content/Source/Core/Tools/OffCanvas/themes/flat-ui/foonav.flat-ui.emerald.css',
         //'Content/Source/Core/Tools/OffCanvas/themes/flat-ui/foonav.flat-ui.orange.css',
         //'Content/Source/Core/Tools/OffCanvas/themes/flat-ui/foonav.flat-ui.pumpkin.css',
         //'Content/Source/Core/Tools/OffCanvas/themes/flat-ui/foonav.flat-ui.red.css',
         //'Content/Source/Core/Tools/OffCanvas/themes/flat-ui/foonav.flat-ui.silver.css',
         //'Content/Source/Core/Tools/OffCanvas/themes/flat-ui/foonav.flat-ui.turquoise.css',
         //'Content/Source/Core/Tools/Dropdown/dropdown.css'
    ],
    dropdownCssSrc: [
    'Content/Source/Core/Tools/Dropdown/dropdown.css'
    ],
    selectCssSrc: [
    'Content/Source/Core/Tools/Select/bootstrap-select.css'
    ],
    sliderCssSrc: [
       'Content/Source/Core/Tools/Slider/layerslider.css',
       'Content/Source/Core/Tools/Slider/customize.css'
    ],
    offCanvasCssSrc: [
        'Content/Source/Core/Tools/OffCanvas/foonav.css',
        'Content/Source/Core/Tools/OffCanvas/foonav.icons.css',
        'Content/Source/Core/Tools/OffCanvas/customize.css'
    ],
    editorCssSrc: [
       'Content/Source/Core/Tools/Editor/skins/lightgray/Content.min.css',
       'Content/Source/Core/Tools/Editor/skins/lightgray/skin.dev.min.css',
       'Content/Source/Core/Tools/Editor/skins/lightgray/skin.ie7.dev.min.css',
       'Content/Source/Core/Tools/Editor/skins/lightgray/Content.Inline.min.css'
    ],

    calendarCssSrc: [
         'Content/Source/Core/Tools/Calendar/ui.calendars.picker-rtl.css ',
         'Content/Source/Core/Tools/Calendar/jquery-ui.structure.css',
         'Content/Source/Core/Tools/Calendar/customize.css'
    ],
    dateTimeInputCssSrc: [
        'Content/Source/Core/Tools/DateTimeInput/clockpicker.css'
    ],
    modalCssSrc: [
             'Content/Source/Core/Tools/Modal/bootstrap-modal-bs3patch.css ',
            'Content/Source/Core/Tools/Modal/bootstrap-modal.css'
    ],
    megaMenuCssSrc: [
        'Content/Source/Core/Tools/MegaMenu.css'
    ],
    fixedNavCssSrc: [
      'Content/Source/Core/Tools/FixedNav.css'
    ],
    validatorCssSrc: [
  'Content/Source/Core/Tools/Validator.css'
    ],
    distCssOut: 'Content/Dist',
    calendarTheme: 'Content/Dist/calendar/theme',
    editorCssOut: 'Content/Dist/Editor/Skins/LightGray',
    distjsOut: 'Scripts/Dist',
    themeName: 'theme'
}
// Synchronously delete the output lib script file(s)
gulp.task('clean-lib', [], function (cb) {
    return del([config.distjsOut + '/lib.js'], cb);
});

// Synchronously delete the output asSelect script file(s)
gulp.task('clean-asSelect', [], function (cb) {
    return del([config.distjsOut + '/asSelect.js'], cb);
});

// Synchronously delete the output asSlider script file(s)
gulp.task('clean-asSlider', [], function (cb) {
    return del([config.distjsOut + '/asSlider.js'], cb);
});

// Synchronously delete the output asCaptcha script file(s)
gulp.task('clean-asCaptcha', [], function (cb) {
    return del([config.distjsOut + '/asCaptcha.js'], cb);
});

// Synchronously delete the output asLogin script file(s)
gulp.task('clean-asLogin', [], function (cb) {
    return del([config.distjsOut + '/asLogin.js'], cb);
});

// Synchronously delete the output asPopovers script file(s)
gulp.task('clean-asPopovers', [], function (cb) {
    return del([config.distjsOut + '/asPopovers.js'], cb);
});

// Synchronously delete the output asOffCanvas script file(s)
gulp.task('clean-asOffCanvas', [], function (cb) {
    return del([config.distjsOut + '/asOffCanvas.js'], cb);
});

// Synchronously delete the output asMegaMenu script file(s)
gulp.task('clean-asMegaMenu', [], function (cb) {
    return del([config.distjsOut + '/asMegaMenu.js'], cb);
});

// Synchronously delete the output asFixedNav script file(s)
gulp.task('clean-asFixedNav', [], function (cb) {
    return del([config.distjsOut + '/asFixedNav.js'], cb);
});

// Synchronously delete the output asDropdown script file(s)
gulp.task('clean-asDropdown', [], function (cb) {
    return del([config.distjsOut + '/asDropdown.js'], cb);
});

// Synchronously delete the output asValidate script file(s)
gulp.task('clean-asValidate', [], function (cb) {
    return del([config.distjsOut + '/asValidate.js'], cb);
});

// Synchronously delete the output asEditorTheme script file(s)
gulp.task('clean-asEditorTheme', [], function (cb) {
    return del(['Scripts/Dist/Editor/themes/modern/theme.js'], cb);
});

// Synchronously delete the output asEditor script file(s)
gulp.task('clean-asEditor', ['clean-asEditorTheme'], function (cb) {
    return del([config.distjsOut + '/asEditor.js'], cb);
});

// Synchronously delete the output asCodeEditor script file(s)
gulp.task('clean-asCodeEditor', [], function (cb) {
    return del([config.distjsOut + '/asCodeEditor.js'], cb);
});


//Create a asDropDownButton bundled file
gulp.task('AS.Select', ['clean-asSelect'], function () {
    return gulp.src(config.asSelectSrc)
     .pipe(uglify())
     .pipe(concat('asSelect.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asSlider bundled file
gulp.task('AS.Slider', ['clean-asSlider'], function () {
    return gulp.src(config.asSliderSrc)
     .pipe(uglify())
     .pipe(concat('asSlider.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asCaptcha bundled file
gulp.task('AS.Captcha', ['clean-asCaptcha'], function () {
    return gulp.src(config.asCaptchaSrc)
     .pipe(uglify())
     .pipe(concat('asCaptcha.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asLogin bundled file
gulp.task('AS.Login', ['clean-asLogin'], function () {
    return gulp.src(config.asLoginSrc)
     .pipe(uglify())
     .pipe(concat('asLogin.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asPopovers bundled file
gulp.task('AS.Popovers', ['clean-asPopovers'], function () {
    return gulp.src(config.asPopoversSrc)
     .pipe(uglify())
     .pipe(concat('asPopovers.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a lib bundled file
gulp.task('lib', ['clean-lib'], function () {
    return gulp.src(config.libSrc)
     .pipe(uglify())
     .pipe(concat('lib.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asOffCanvas bundled file
gulp.task('AS.OffCanvas', ['clean-asOffCanvas'], function () {
    return gulp.src(config.asOffCanvasSrc)
     .pipe(uglify())
     .pipe(concat('asOffCanvas.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asMegaMenu bundled file
gulp.task('AS.MegaMenu', ['clean-asMegaMenu'], function () {
    return gulp.src(config.asMegaMenuSrc)
     .pipe(uglify())
     .pipe(concat('asMegaMenu.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asFixedNav bundled file
gulp.task('AS.FixedNav', ['clean-asFixedNav'], function () {
    return gulp.src(config.asFixedNavSrc)
     .pipe(uglify())
     .pipe(concat('asFixedNav.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asDropdown bundled file
gulp.task('AS.Dropdown', ['clean-asDropdown'], function () {
    return gulp.src(config.asDropdownSrc)
     //.pipe(uglify())
     .pipe(concat('asDropdown.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asValidate bundled file
gulp.task('AS.Validate', ['clean-asValidate'], function () {
    return gulp.src(config.asValidateSrc)
     .pipe(uglify())
     .pipe(concat('asValidate.js'))
     .pipe(gulp.dest(config.distjsOut));
});
//Create a asEditor bundled file
gulp.task('AS.Editor', ['clean-asEditor'], function () {
    gulp.src('Scripts/Source/Tools/Editor/js/tinymce/themes/modern/theme.js')
        .pipe(uglify())
  .pipe(gulp.dest('Scripts/Dist/editor/themes/modern'));
    return gulp.src(config.asEditorSrc)
     .pipe(uglify())
     .pipe(concat('asEditor.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asCodeEditor bundled file
gulp.task('AS.CodeEditor', ['clean-asCodeEditor'], function () {
    return gulp.src(config.asCodeEditorSrc)
     .pipe(uglify())
     .pipe(concat('asCodeEditor.js'))
     .pipe(gulp.dest(config.distjsOut));
});



gulp.task('scripts', [
    'AS.Select',
    'AS.Slider',
    'AS.Captcha',
    'AS.Login',
    'AS.Popovers',
    'AS.Core',
    'AS.OffCanvas',
    'AS.MegaMenu',
    'AS.FixedNav',
    'AS.Dropdown',
    'AS.Validate',
    'AS.Editor',
    'AS.CodeEditor'
], function () {

});



//Restore all bower packages
gulp.task('bower-restore', function () {
    return bower();
});

//Set a default tasks 
gulp.task('default', ['scripts', 'style'], function () {

});

gulp.task('style', [
    'siteCoreCss'
], function () {
});


gulp.task('clean-siteCoreCSS', function (cb) {
    return del([config.distCssOut + '/siteCore.*'], cb);
});

gulp.task('clean-editorCSS', function (cb) {
    return del([config.editorCssOut + '/*.min.css'], cb);
});

gulp.task('clean-offCanvasCSS', function (cb) {
    return del([config.distCssOut + '/asOffCanvas.css'], cb);
});

gulp.task('clean-sliderCSS', function (cb) {
    return del([config.distCssOut + '/asSlider.css'], cb);
});

gulp.task('clean-selectCSS', function (cb) {
    return del([config.distCssOut + '/asSelect.css'], cb);
});

gulp.task('clean-dropdownCSS', function (cb) {
    return del([config.distCssOut + '/asDropdown.css'], cb);
});

gulp.task('clean-calendarCSS', function (cb) {
    return del([config.distCssOut + '/asCalendar.css'], cb);
});

gulp.task('clean-dateTimeInputCSS', function (cb) {
    return del([config.distCssOut + '/asDateTimeInput.css'], cb);
});

gulp.task('clean-modalCSS', function (cb) {
    return del([config.distCssOut + '/asModal.css'], cb);
});

gulp.task('clean-megaMenuCSS', function (cb) {
    return del([config.distCssOut + '/asMegaMenu.css'], cb);
});

gulp.task('clean-fixedNavCSS', function (cb) {
    return del([config.distCssOut + '/asFixedNav.css'], cb);
});

gulp.task('clean-validatorCSS', function (cb) {
    return del([config.distCssOut + '/asValidator.css'], cb);
});

gulp.task('siteCoreCss', ['clean-siteCoreCSS', 'bower-restore'], function () {
    return gulp.src(config.siteCoreCssSrc)
     .pipe(concat('siteCore.css'))
     .pipe(gulp.dest(config.distCssOut))
     .pipe(minifyCSS({ keepSpecialComments: 0 }))
     .pipe(concat('siteCore.min.css'))
     .pipe(gulp.dest(config.distCssOut));
});

gulp.task('editorCss', ['clean-editorCSS'], function () {
    gulp.src(config.editorCssSrc)
     .pipe(gulp.dest(config.editorCssOut));
});

gulp.task('offCanvasCss', ['clean-offCanvasCSS'], function () {
    return gulp.src(config.offCanvasCssSrc)
     .pipe(minifyCSS({ keepSpecialComments: 0 }))
     .pipe(concat('asOffCanvas.css'))
     .pipe(gulp.dest(config.distCssOut));
});

gulp.task('sliderCss', ['clean-sliderCSS'], function () {
    return gulp.src(config.sliderCssSrc)
     .pipe(minifyCSS({ keepSpecialComments: 0 }))
     .pipe(concat('asSlider.css'))
     .pipe(gulp.dest(config.distCssOut));
});

gulp.task('selectCss', ['clean-selectCSS'], function () {
    return gulp.src(config.selectCssSrc)
     .pipe(minifyCSS({ keepSpecialComments: 0 }))
     .pipe(concat('asSelect.css'))
     .pipe(gulp.dest(config.distCssOut));
});

gulp.task('dropdownCss', ['clean-dropdownCSS'], function () {
    return gulp.src(config.dropdownCssSrc)
     .pipe(minifyCSS({ keepSpecialComments: 0 }))
     .pipe(concat('asDropdown.css'))
     .pipe(gulp.dest(config.distCssOut));
});

gulp.task('calendarCss', ['clean-calendarCSS'], function () {
    return gulp.src(config.calendarCssSrc)
     .pipe(minifyCSS({ keepSpecialComments: 0 }))
     .pipe(concat('asCalendar.css'))
     .pipe(gulp.dest(config.distCssOut));
});

gulp.task('dateTimeInputCss', ['clean-dateTimeInputCSS'], function () {
    return gulp.src(config.dateTimeInputCssSrc)
     .pipe(minifyCSS({ keepSpecialComments: 0 }))
     .pipe(concat('asDateTimeInput.css'))
     .pipe(gulp.dest(config.distCssOut));
});

gulp.task('modalCss', ['clean-modalCSS'], function () {
    return gulp.src(config.modalCssSrc)
     .pipe(minifyCSS({ keepSpecialComments: 0 }))
     .pipe(concat('asModal.css'))
     .pipe(gulp.dest(config.distCssOut));
});

gulp.task('megaMenuCss', ['clean-megaMenuCSS'], function () {
    return gulp.src(config.megaMenuCssSrc)
     .pipe(minifyCSS({ keepSpecialComments: 0 }))
     .pipe(concat('asMegaMenu.css'))
     .pipe(gulp.dest(config.distCssOut));
});

gulp.task('fixedNavCss', ['clean-fixedNavCSS'], function () {
    return gulp.src(config.fixedNavCssSrc)
     .pipe(minifyCSS({ keepSpecialComments: 0 }))
     .pipe(concat('asFixedNav.css'))
     .pipe(gulp.dest(config.distCssOut));
});

gulp.task('validatorCss', ['clean-validatorCSS'], function () {
    return gulp.src(config.validatorCssSrc)
     .pipe(minifyCSS({ keepSpecialComments: 0 }))
     .pipe(concat('asValidator.css'))
     .pipe(gulp.dest(config.distCssOut));
});

//#region Calendar and DateTimeInput Theme
gulp.task('skyTheme', [], function () {
    return gulp.src([
    'Content/Source/Theme/Sky/Tools/Calendar/Redmond/jquery-ui.redmond.css',
    'Content/Source/Theme/Sky/Tools/Calendar/Redmond/ui-redmond.calendars.picker.css',
    'Content/Source/Theme/Base/Tools/Calendar/ui.calendars.picker.css',
    'Content/Source/Theme/Sky/Tools/DateTimeInput/sky.css'
    //'Content/Source/Theme/3rdParty/jqWidgets/jqx.ui-redmond.css',
    //'Content/Source/Theme/3rdParty/jqWidgets/jqx.ui-start.css',
    //'Content/Source/Theme/Sky/Sky.css'
    ])
   .pipe(concat('sky.css'))
   .pipe(gulp.dest(config.calendarTheme))
   .pipe(minifyCSS({ keepSpecialComments: 0 }))
   .pipe(concat('sky.min.css'))
   .pipe(gulp.dest(config.calendarTheme));
});
gulp.task('blueTheme', [], function () {
    return gulp.src([
    'Content/Source/Theme/Blue/Tools/Calendar/Start/jquery-ui.start.css',
    'Content/Source/Theme/Blue/Tools/Calendar/Start/ui-start.calendars.picker.css',
    'Content/Source/Theme/Base/Tools/Calendar/ui.calendars.picker.css',
    'Content/Source/Theme/Blue/Tools/DateTimeInput/Blue.css'
    //'Content/Source/Theme/3rdParty/jqWidgets/jqx.ui-start.css',
    //'Content/Source/Theme/Blue/Blue.css'
    ])
   .pipe(concat('blue.css'))
   .pipe(gulp.dest(config.calendarTheme))
   .pipe(minifyCSS({ keepSpecialComments: 0 }))
   .pipe(concat('blue.min.css'))
   .pipe(gulp.dest(config.calendarTheme));
});
//gulp.task('VioletTheme', [], function () {
//    return gulp.src([
//    'Content/Source/Theme/Violet/Tools/Calendar/Redmond/jquery-ui.redmond.css',
//    'Content/Source/Theme/Violet/Tools/Calendar/Redmond/ui-redmond.calendars.picker.css',
//    'Content/Source/Theme/Base/Tools/Calendar/ui.calendars.picker.css',
//     'Content/Source/Theme/Violet/Tools/DateTimeInput/Violet.css'
//    //'Content/Source/Theme/3rdParty/jqWidgets/jqx.ui-redmond.css',
//    //'Content/Source/Theme/Violet/Violet.css'
//    ])
//   .pipe(concat('violet.css'))
//   .pipe(gulp.dest(config.calendarTheme))
//   .pipe(minifyCSS({ keepSpecialComments: 0 }))
//   .pipe(concat('violet.min.css'))
//   .pipe(gulp.dest(config.calendarTheme));
//});
gulp.task('blackTheme', [], function () {
    return gulp.src([
    'Content/Source/Theme/Black/Tools/Calendar/ShinyBlack/jquery-ui.shinyblack.css',
    'Content/Source/Theme/Black/Tools/Calendar/ShinyBlack/ui-ui-darkness.calendars.picker.css',
    'Content/Source/Theme/Base/Tools/Calendar/ui.calendars.picker.css',
    'Content/Source/Theme/Black/Tools/DateTimeInput/black.css'
    //'Content/Source/Theme/3rdParty/jqWidgets/jqx.shinyblack.css',
    //'Content/Source/Theme/Black/Black.css'
    ])
   .pipe(concat('black.css'))
   .pipe(gulp.dest(config.calendarTheme))
   .pipe(minifyCSS({ keepSpecialComments: 0 }))
   .pipe(concat('black.min.css'))
   .pipe(gulp.dest(config.calendarTheme));
});
gulp.task('publicTheme', [], function () {
    return gulp.src([
    'Content/Source/Theme/Public/Tools/Calendar/Flick/jquery-ui.flick.css',
    'Content/Source/Theme/Public/Tools/Calendar/Flick/ui-flick.calendars.picker.css',
    'Content/Source/Theme/Base/Tools/Calendar/ui.calendars.picker.css',
    'Content/Source/Theme/Public/Tools/DateTimeInput/Public.css'
    //'Content/Source/Theme/Public/Public.css'
    ])
   .pipe(concat('public.css'))
   .pipe(gulp.dest(config.calendarTheme))
   .pipe(minifyCSS({ keepSpecialComments: 0 }))
   .pipe(concat('public.min.css'))
   .pipe(gulp.dest(config.calendarTheme));
});
gulp.task('WhiteTheme', [], function () {
    return gulp.src([
    'Content/Source/Theme/White/Tools/Calendar/Smoothness/jquery-ui.smoothness.css',
    'Content/Source/Theme/White/Tools/Calendar/Smoothness/ui.smoothness.calendars.picker.css',
    'Content/Source/Theme/Base/Tools/Calendar/ui.calendars.picker.css',
    'Content/Source/Theme/white/Tools/DateTimeInput/white.css'
    //'Content/Source/Theme/3rdParty/jqWidgets/jqx.bootstrap.css',
    //'Content/Source/Theme/White/White.css'
    ])
   .pipe(concat('white.css'))
   .pipe(gulp.dest(config.calendarTheme))
   .pipe(minifyCSS({ keepSpecialComments: 0 }))
   .pipe(concat('white.min.css'))
   .pipe(gulp.dest(config.calendarTheme));
});
gulp.task('cleanThemCSS', function (cb) {
    return del([config.calendarTheme + '/*' + '.css',
    config.calendarTheme + '/*' + '.min.css'], cb);
});
//#endregion Theme
