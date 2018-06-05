/// <reference path="C:\Users\hossein\Documents\Visual Studio 2013\Projects\KhodkarSystem\KS.WebUI\Scripts/Source/Tools/Core/jquery.toaster.js" />

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
    //JavaScript files that will be combined into a jquery bundle
    jquerysrc: [
        'bower_components/jquery/dist/jquery-2.2.2.min.js',
        'bower_components/bootstrap-sass/assets/javascripts/bootstrap.js',
        'bower_components/modernizr/modernizr.js',
        'bower_components/respond-minmax/src/respond.js',
        //'bower_components/requirejs/require.js',
        'Scripts/Source/App.js'
    ],

    asButtonSrc: [
       'Scripts/Source/3rdParty/jqWidgets/jqxbuttons.js',
       'Scripts/Source/Tools/AS.Button.js'
    ],

    asCalendarSrc: [
        'Scripts/Source/Tools/Calendar/jquery.plugin.js',
        'Scripts/Source/Tools/Calendar/jquery.calendars.js',
        'Scripts/Source/Tools/Calendar/jquery.calendars.plus.js',
        'Scripts/Source/Tools/Calendar/jquery.calendars.picker.js',
        'Scripts/Source/Tools/Calendar/jquery.calendars.picker.ext.js',
        'Scripts/Source/Tools/Calendar/jquery.calendars.persian.js',
        'Scripts/Source/Tools/Calendar/jquery.calendars.picker-fa.js',
        'Scripts/Source/Tools/Calendar/jquery.calendars.persian-fa.js',
        'Scripts/Source/Tools/AS.Calendar.js'
    ],
    asCheckBoxSrc: [
     'Scripts/Source/3rdParty/jqWidgets/jqxcheckbox.js',
     'Scripts/Source/Tools/AS.CheckBox.js'
    ],
    asComboBoxSrc: [
    'Scripts/Source/3rdParty/jqWidgets/jqxComboBox.js',
    'Scripts/Source/Tools/AS.ComboBox.js'
    ],
    asCoreSrc: [
     'bower_components/bowser/bowser.min.js', 
     'bower_components/jquery-loading-overlay/src/loading-overlay.js',   
     'Scripts/Source/3rdParty/jqWidgets/jqxcore.js',
     'Scripts/Source/Tools/Core/jquery.toaster.js',
     'Scripts/Source/Tools/AS.Core.js'     
    ],
    asCrudContextMenuSrc:[
        'Scripts/Source/Tools/AS.CrudContextMenu.js'
    ],
    asDataSrc:[
        'Scripts/Source/3rdParty/jqWidgets/jqxdata.js',
        'Scripts/Source/Tools/AS.Data.js'
    ],
    asDataTableSrc:[
        'Scripts/Source/3rdParty/jqWidgets/jqxdatatable.js',
        'Scripts/Source/Tools/AS.DataTable.js'
    ],
    asDateTimeInputSrc: [
        'Scripts/Source/Tools/DateTimeInput/clockpicker.js',
        'Scripts/Source/Tools/AS.DateTimeInput.js'
    ],
    asDropDownListSrc: [
        'Scripts/Source/3rdParty/jqWidgets/jqxdropdownlist.js',
        'Scripts/Source/Tools/AS.DropDownList.js'
    ],
    asGridSrc: [
       'Scripts/Source/3rdParty/jqWidgets/jqxgrid.js',
       'Scripts/Source/Tools/AS.Grid.js'
    ],
    asInputSrc: [
   'Scripts/Source/3rdParty/jqWidgets/jqxInput.js',
   'Scripts/Source/Tools/AS.Input.js'
    ],
    asMenuSrc: [
        'Scripts/Source/3rdParty/jqWidgets/jqxmenu.js',
        'Scripts/Source/Tools/AS.Menu.js'
    ],
    asMessageBoxSrc: [
        'Scripts/Source/Tools/AS.MessageBox.js'
    ],
    asNotificationSrc: [
       'Scripts/Source/3rdParty/jqWidgets/jqxnotification.js',
       'Scripts/Source/Tools/AS.Notification.js'
    ],
    
    //CORE files that will be combined into a CORE bundle
    coreCssSrc: [
            'Content/Source/Core/3rdParty/bootstrap.min.css',
            //'Content/Source/Core/3rdParty/font-awesome.min.css',
            'Content/Source/Core/3rdParty/RTL/bootstrap-rtl.min.css',
            'Content/Source/Core/3rdParty/jqWidgets/jqx.base.css',
            'Content/Source/Core/Tools/DateTimeInput/clockpicker.css',
            'Content/Source/Core/Tools/Calendar/ui.calendars.picker-rtl.css ',
            'Content/Source/Core/Tools/Calendar/jquery-ui.structure.css',
            'Content/Source/Core/Tools/PopUp/bootstrap-modal-bs3patch.css ',
            'Content/Source/Core/Tools/PopUp/bootstrap-modal.css',
            'Content/Source/Core/core.css',
            'Content/Source/Core/Customize/jqWidgets.css',
            'Content/Source/Core/Customize/Others.css',
            'Content/Source/Core/Customize/Bootstrap/bootstrap.css'
    ],
    distCssOut: 'Content/Dist',
    distjsOut: 'Scripts/Dist',
    themeName: 'theme'
}

// Synchronously delete the output script file(s)
gulp.task('clean-jqueryRequire', ['bower-restore'], function (cb) {
    return del([config.distjsOut + '/jqueryRequire.min.js'], cb);
});

// Synchronously delete the output AS.Button script file(s)
gulp.task('clean-asButton', [], function (cb) {
    return del([config.distjsOut + '/asButton.js'], cb);
});

// Synchronously delete the output AS.Calendar script file(s)
gulp.task('clean-asCalendar', [], function (cb) {
    return del([config.distjsOut + '/asCalendar.js'], cb);
});

// Synchronously delete the output asCheckBox script file(s)
gulp.task('clean-asCheckBox', [], function (cb) {
    return del([config.distjsOut + '/asCheckBox.js'], cb);
});

// Synchronously delete the output asComboBox script file(s)
gulp.task('clean-asComboBox', [], function (cb) {
    return del([config.distjsOut + '/asComboBox.js'], cb);
});

// Synchronously delete the output asCore script file(s)
gulp.task('clean-asCore', [], function (cb) {
    return del([config.distjsOut + '/asCore.js'], cb);
});

// Synchronously delete the output asCrudContextMenu script file(s)
gulp.task('clean-asCrudContextMenu', [], function (cb) {
    return del([config.distjsOut + '/asCrudContextMenu.js'], cb);
});

// Synchronously delete the output asData script file(s)
gulp.task('clean-asData', [], function (cb) {
    return del([config.distjsOut + '/asData.js'], cb);
});

// Synchronously delete the output asDataTable script file(s)
gulp.task('clean-asDataTable', [], function (cb) {
    return del([config.distjsOut + '/asDataTable.js'], cb);
});

// Synchronously delete the output asDateTimeInput script file(s)
gulp.task('clean-asDateTimeInput', [], function (cb) {
    return del([config.distjsOut + '/asDateTimeInput.js'], cb);
});

// Synchronously delete the output asDropDownList script file(s)
gulp.task('clean-asDropDownList', [], function (cb) {
    return del([config.distjsOut + '/asDropDownList.js'], cb);
});

// Synchronously delete the output asGrid script file(s)
gulp.task('clean-asGrid', [], function (cb) {
    return del([config.distjsOut + '/asGrid.js'], cb);
});

// Synchronously delete the output asInput script file(s)
gulp.task('clean-asInput', [], function (cb) {
    return del([config.distjsOut + '/asInput.js'], cb);
});

// Synchronously delete the output asMenu script file(s)
gulp.task('clean-asMenu', [], function (cb) {
    return del([config.distjsOut + '/asMenu.js'], cb);
});

// Synchronously delete the output asMessageBox script file(s)
gulp.task('clean-asMessageBox', [], function (cb) {
    return del([config.distjsOut + '/asMessageBox.js'], cb);
});

// Synchronously delete the output asNotification script file(s)
gulp.task('clean-asNotification', [], function (cb) {
    return del([config.distjsOut + '/asNotification.js'], cb);
});

//Create a jqueryRequire bundled file
gulp.task('jqueryRequire', ['clean-jqueryRequire'], function () {
    return gulp.src(config.jquerysrc)
     .pipe(uglify())
     .pipe(concat('jqueryRequire.min.js'))
     .pipe(gulp.dest(config.distjsOut));
});


//Create a AS.Button bundled file
gulp.task('AS.Button', ['clean-asButton'], function () {
    return gulp.src(config.asButtonSrc)
     .pipe(uglify())
     .pipe(concat('asButton.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asCalendar bundled file
gulp.task('AS.Calendar', ['clean-asCalendar'], function () {
    return gulp.src(config.asCalendarSrc)
     .pipe(uglify())
     .pipe(concat('asCalendar.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a CheckBox bundled file
gulp.task('AS.CheckBox', ['clean-asCheckBox'], function () {
    return gulp.src(config.asCheckBoxSrc)
     .pipe(uglify())
     .pipe(concat('asCheckBox.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asComboBox bundled file
gulp.task('AS.ComboBox', ['clean-asComboBox'], function () {
    return gulp.src(config.asComboBoxSrc)
     .pipe(uglify())
     .pipe(concat('asComboBox.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asCore bundled file
gulp.task('AS.Core', ['clean-asCore'], function () {
    return gulp.src(config.asCoreSrc)
     .pipe(uglify())
     .pipe(concat('asCore.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asCrudContextMenu bundled file
gulp.task('AS.CrudContextMenu', ['clean-asCrudContextMenu'], function () {
    return gulp.src(config.asCrudContextMenuSrc)
     .pipe(uglify())
     .pipe(concat('asCrudContextMenu.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asData bundled file
gulp.task('AS.Data', ['clean-asData'], function () {
    return gulp.src(config.asDataSrc)
     .pipe(uglify())
     .pipe(concat('asData.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asDataTable bundled file
gulp.task('AS.DataTable', ['clean-asDataTable'], function () {
    return gulp.src(config.asDataTableSrc)
     .pipe(uglify())
     .pipe(concat('asDataTable.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asDateTimeInput bundled file
gulp.task('AS.DateTimeInput', ['clean-asDateTimeInput'], function () {
    return gulp.src(config.asDateTimeInputSrc)
     .pipe(uglify())
     .pipe(concat('asDateTimeInput.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asDropDownList bundled file
gulp.task('AS.DropDownList', ['clean-asDropDownList'], function () {
    return gulp.src(config.asDropDownListSrc)
     .pipe(uglify())
     .pipe(concat('asDropDownList.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asGrid bundled file
gulp.task('AS.Grid', ['clean-asGrid'], function () {
    return gulp.src(config.asGridSrc)
     .pipe(uglify())
     .pipe(concat('asGrid.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asInput bundled file
gulp.task('AS.Input', ['clean-asInput'], function () {
    return gulp.src(config.asInputSrc)
     .pipe(uglify())
     .pipe(concat('asInput.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asMenu bundled file
gulp.task('AS.Menu', ['clean-asMenu'], function () {
    return gulp.src(config.asMenuSrc)
     .pipe(uglify())
     .pipe(concat('asMenu.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asMessageBox bundled file
gulp.task('AS.MessageBox', ['clean-asMessageBox'], function () {
    return gulp.src(config.asMessageBoxSrc)
     .pipe(uglify())
     .pipe(concat('asMessageBox.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asNotification bundled file
gulp.task('AS.Notification', ['clean-asNotification'], function () {
    return gulp.src(config.asNotificationSrc)
     .pipe(uglify())
     .pipe(concat('asNotification.js'))
     .pipe(gulp.dest(config.distjsOut));
});

gulp.task('scripts', [
    'jqueryRequire',
    'AS.Button',
    'AS.Calendar',
    'AS.CheckBox',
    'AS.ComboBox',
    'AS.Core',
    'AS.CrudContextMenu',
    'AS.Data',
    'AS.DataTable',
    'AS.DateTimeInput',
    'AS.DropDownList',
    'AS.Grid',
    'AS.Input',
    'AS.Menu',
    'AS.MessageBox',
    'AS.Notification'
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
    'coreCss',
    'cleanThemCSS',
    'skyTheme',
    'blackTheme',
    'publicTheme',
    'WhiteTheme',
    'blueTheme',
    'VioletTheme'
], function () {
});

// Synchronously delete the output style files (css / fonts)
gulp.task('clean-coreCSS', function (cb) {
    return del([config.distCssOut + '/core.*'], cb);
});

gulp.task('coreCss', ['clean-coreCSS', 'bower-restore'], function () {
    return gulp.src(config.coreCssSrc)
     .pipe(concat('core.css'))
     .pipe(gulp.dest(config.distCssOut))
     .pipe(minifyCSS({ keepSpecialComments: 0 }))
     .pipe(concat('core.min.css'))
     .pipe(gulp.dest(config.distCssOut));
});

//#region Theme
gulp.task('skyTheme', [], function () {
    return gulp.src([
    'Content/Source/Theme/Sky/Tools/Calendar/Redmond/jquery-ui.redmond.css',
    'Content/Source/Theme/Sky/Tools/Calendar/Redmond/ui-redmond.calendars.picker.css',
    'Content/Source/Theme/Base/Tools/Calendar/ui.calendars.picker.css',
    'Content/Source/Theme/3rdParty/jqWidgets/jqx.ui-redmond.css',
    'Content/Source/Theme/3rdParty/jqWidgets/jqx.ui-start.css',
    'Content/Source/Theme/Sky/Sky.css'
    ])
   .pipe(concat('Sky' + config.themeName + '.css'))
   .pipe(gulp.dest(config.distCssOut))
   .pipe(minifyCSS({ keepSpecialComments: 0 }))
   .pipe(concat('Sky' + config.themeName + '.min.css'))
   .pipe(gulp.dest(config.distCssOut));
});
gulp.task('blueTheme', [], function () {
    return gulp.src([
    'Content/Source/Theme/Blue/Tools/Calendar/Start/jquery-ui.start.css',
    'Content/Source/Theme/Blue/Tools/Calendar/Start/ui-start.calendars.picker.css',
    'Content/Source/Theme/Base/Tools/Calendar/ui.calendars.picker.css',
    'Content/Source/Theme/3rdParty/jqWidgets/jqx.ui-start.css',
    'Content/Source/Theme/Blue/Blue.css'
    ])
   .pipe(concat('Blue' + config.themeName + '.css'))
   .pipe(gulp.dest(config.distCssOut))
   .pipe(minifyCSS({ keepSpecialComments: 0 }))
   .pipe(concat('Blue' + config.themeName + '.min.css'))
   .pipe(gulp.dest(config.distCssOut));
});
gulp.task('VioletTheme', [], function () {
    return gulp.src([
    'Content/Source/Theme/Violet/Tools/Calendar/Redmond/jquery-ui.redmond.css',
    'Content/Source/Theme/Violet/Tools/Calendar/Redmond/ui-redmond.calendars.picker.css',
    'Content/Source/Theme/Base/Tools/Calendar/ui.calendars.picker.css',
    'Content/Source/Theme/3rdParty/jqWidgets/jqx.ui-redmond.css',
    'Content/Source/Theme/Violet/Violet.css'
    ])
   .pipe(concat('Violet' + config.themeName + '.css'))
   .pipe(gulp.dest(config.distCssOut))
   .pipe(minifyCSS({ keepSpecialComments: 0 }))
   .pipe(concat('Violet' + config.themeName + '.min.css'))
   .pipe(gulp.dest(config.distCssOut));
});
gulp.task('blackTheme', [], function () {
    return gulp.src([
    'Content/Source/Theme/Black/Tools/Calendar/ShinyBlack/jquery-ui.shinyblack.css',
    'Content/Source/Theme/Black/Tools/Calendar/ShinyBlack/ui-ui-darkness.calendars.picker.css',
    'Content/Source/Theme/Base/Tools/Calendar/ui.calendars.picker.css',
    'Content/Source/Theme/3rdParty/jqWidgets/jqx.shinyblack.css',
    'Content/Source/Theme/Black/Black.css'
    ])
   .pipe(concat('Black' + config.themeName + '.css'))
   .pipe(gulp.dest(config.distCssOut))
   .pipe(minifyCSS({ keepSpecialComments: 0 }))
   .pipe(concat('Black' + config.themeName + '.min.css'))
   .pipe(gulp.dest(config.distCssOut));
});
gulp.task('publicTheme', [], function () {
    return gulp.src([
    'Content/Source/Theme/Public/Tools/Calendar/Flick/jquery-ui.flick.css',
    'Content/Source/Theme/Public/Tools/Calendar/Flick/ui-flick.calendars.picker.css',
    'Content/Source/Theme/Base/Tools/Calendar/ui.calendars.picker.css',
    'Content/Source/Theme/Public/Public.css'
    ])
   .pipe(concat('Public' + config.themeName + '.css'))
   .pipe(gulp.dest(config.distCssOut))
   .pipe(minifyCSS({ keepSpecialComments: 0 }))
   .pipe(concat('Public' + config.themeName + '.min.css'))
   .pipe(gulp.dest(config.distCssOut));
});
gulp.task('WhiteTheme', [], function () {
    return gulp.src([
    'Content/Source/Theme/White/Tools/Calendar/Smoothness/jquery-ui.smoothness.css',
    'Content/Source/Theme/White/Tools/Calendar/Smoothness/ui.smoothness.calendars.picker.css',
    'Content/Source/Theme/Base/Tools/Calendar/ui.calendars.picker.css',
    'Content/Source/Theme/3rdParty/jqWidgets/jqx.bootstrap.css',
    'Content/Source/Theme/White/White.css'
    ])
   .pipe(concat('White' + config.themeName + '.css'))
   .pipe(gulp.dest(config.distCssOut))
   .pipe(minifyCSS({ keepSpecialComments: 0 }))
   .pipe(concat('White' + config.themeName + '.min.css'))
   .pipe(gulp.dest(config.distCssOut));
});
gulp.task('cleanThemCSS', function (cb) {
    return del([config.distCssOut + '/*' + config.themeName + '.css',
    config.distCssOut + '/*' + config.themeName + '.min.css'], cb);
});
//#endregion Theme