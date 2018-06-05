

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
   
    //CORE files that will be combined into a CORE bundle
    asCoreSrc: [
'Scripts/Source/Tools/AS.Core.js'
    ],
    respondSrc: [
'Scripts/Source/Tools/Core/Respond/matchMedia.js',
'Scripts/Source/Tools/Core/Respond/matchMedia.addListener.js',
'Scripts/Source/Tools/Core/Respond/respond.js',
'Scripts/Source/Tools/Core/Respond/enquire.js'
    ],
    fontPromiseSrc: [
'Scripts/Source/Tools/Core/font/es6-promise.js',
'Scripts/Source/Tools/Core/font/fontfaceobserver.standalone.js'
    ],
    asCorePluginsSrc: [
'Scripts/Source/Tools/AS.CorePlugins.js'
    ],
    asFlexSelectSrc: [
  'Scripts/Source/Tools/AS.FlexSelect.js'
    ],
    asOdataQueryBuilderSrc: [
         'Scripts/Source/Tools/As.OdataQueryBuilder.js',
       'Scripts/Source/Tools/OdataQueryBuilder/q.min.js',
'Scripts/Source/Tools/OdataQueryBuilder/breeze.min.js'

    ],
       
    flexSelectCssSrc: [
 'Content/Source/Core/Tools/FlexSelect.css'
    ],
    distCssOut: 'Content/Dist',
    calendarTheme: 'Content/Dist/calendar/theme',
    editorCssOut: 'Content/Dist/Editor/Skins/LightGray',
    distjsOut: 'Scripts/Dist',
    themeName: 'theme'
}

// Synchronously delete the output asCore script file(s)
gulp.task('clean-asCore', [], function (cb) {
    return del([config.distjsOut + '/asSiteCore.js'], cb);
});

// Synchronously delete the output respond script file(s)
gulp.task('clean-respond', [], function (cb) {
    return del([config.distjsOut + '/respond.js'], cb);
});

// Synchronously delete the output fontPromise script file(s)
gulp.task('clean-fontPromise', [], function (cb) {
    return del([config.distjsOut + '/font-promise.js'], cb);
});

// Synchronously delete the output asCorePlugins script file(s)
gulp.task('clean-asCorePlugins', [], function (cb) {
    return del([config.distjsOut + '/asCorePlugins.js'], cb);
});

// Synchronously delete the output asFlexSelect script file(s)
gulp.task('clean-asFlexSelect', [], function (cb) {
    return del([config.distjsOut + '/asFlexSelect.js'], cb);
});

// Synchronously delete the output asOdataQueryBuilder script file(s)
gulp.task('clean-asOdataQueryBuilder', [], function (cb) {
    return del([config.distjsOut + '/asOdataQueryBuilder.js'], cb);
});



//Create a asCore bundled file
gulp.task('AS.Core', ['clean-asCore'], function () {
    return gulp.src(config.asCoreSrc)
     .pipe(uglify())
     .pipe(concat('asSiteCore.js'))
     .pipe(gulp.dest(config.distjsOut));
});
//Create a respond bundled file
gulp.task('respond', ['clean-respond'], function () {
    return gulp.src(config.respondSrc)
     .pipe(uglify())
     .pipe(concat('respond.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a fontPromise bundled file
gulp.task('fontPromise', ['clean-fontPromise'], function () {
    return gulp.src(config.fontPromiseSrc)
     .pipe(uglify())
     .pipe(concat('font-promise.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asCorePlugins bundled file
gulp.task('AS.CorePlugins', ['clean-asCorePlugins'], function () {
    return gulp.src(config.asCorePluginsSrc)
     .pipe(uglify())
     .pipe(concat('asCorePlugins.js'))
     .pipe(gulp.dest(config.distjsOut));
});


//Create a asFlexSelect bundled file
gulp.task('AS.FlexSelect', ['clean-asFlexSelect'], function () {
    return gulp.src(config.asFlexSelectSrc)
     .pipe(uglify())
     .pipe(concat('asFlexSelect.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asFlexSelect bundled file
gulp.task('As.OdataQueryBuilder', ['clean-asOdataQueryBuilder'], function () {
    return gulp.src(config.asOdataQueryBuilderSrc)
     .pipe(uglify())
     .pipe(concat('asOdataQueryBuilder.js'))
     .pipe(gulp.dest(config.distjsOut));
});



gulp.task('scripts', [
    'AS.FlexSelect',
    'As.OdataQueryBuilder',
    'AS.Core',
    'AS.CorePlugins'
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
    'flexSelectCss'
], function () {
});



gulp.task('clean-flexSelectCSS', function (cb) {
    return del([config.distCssOut + '/asFlexSelect.css'], cb);
});



gulp.task('flexSelectCss', ['clean-flexSelectCSS'], function () {
    return gulp.src(config.flexSelectCssSrc)
     .pipe(minifyCSS({ keepSpecialComments: 0 }))
     .pipe(concat('asFlexSelect.css'))
     .pipe(gulp.dest(config.distCssOut));
});



