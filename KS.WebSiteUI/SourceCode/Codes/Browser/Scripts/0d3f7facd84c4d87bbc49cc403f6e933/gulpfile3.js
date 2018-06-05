

// include plug-ins
var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var del = require('del');

var copy = require('gulp-copy');
var bower = require('gulp-bower');
var sourcemaps = require('gulp-sourcemaps');

var config = {
    asDockingLayoutSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxDockingLayout.js',
'Scripts/Source/Tools/AS.DockingLayout.js'
    ],
    asTextAreaSrc: [
   'Scripts/Source/3rdParty/jqWidgets/jqxTextArea.js',
   'Scripts/Source/Tools/AS.TextArea.js'
    ],
    asProgressBarSrc: [
   'Scripts/Source/3rdParty/jqWidgets/jqxProgressBar.js',
   'Scripts/Source/Tools/AS.ProgressBar.js'
    ],
    asFileUploadSrc: [
  'Scripts/Source/3rdParty/jqWidgets/jqxFileUpload.js',
  'Scripts/Source/Tools/AS.FileUpload.js'
    ],
    asMaskedInputSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxMaskedInput.js',
'Scripts/Source/Tools/AS.MaskedInput.js'
    ],
    asLayoutSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxLayout.js',
'Scripts/Source/Tools/AS.Layout.js'
    ],
    asValidatorSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxValidator.js',
'Scripts/Source/Tools/AS.Validator.js'
    ],
    asColorPickerSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxColorPicker.js',
'Scripts/Source/Tools/AS.ColorPicker.js'
    ],
    asTooltipSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxTooltip.js',
'Scripts/Source/Tools/AS.Tooltip.js'
    ],

    asGaugeSrc: [
 'Scripts/Source/3rdParty/jqWidgets/jqxGauge.js',
 'Scripts/Source/Tools/AS.Gauge.js'
    ],
    asRibbonSrc: [
   'Scripts/Source/3rdParty/jqWidgets/jqxRibbon.js',
   'Scripts/Source/Tools/AS.Ribbon.js'
    ],
    asEditorSrc: [
   'Scripts/Source/3rdParty/jqWidgets/jqxEditor.js',
   'Scripts/Source/Tools/AS.Editor.js'
    ],
    asRangeSelectorSrc: [
  'Scripts/Source/3rdParty/jqWidgets/jqxRangeSelector.js',
  'Scripts/Source/Tools/AS.RangeSelector.js'
    ],
    asChartSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxChart.js',
'Scripts/Source/Tools/AS.Chart.js'
    ],
    asDrawSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxDraw.js',
'Scripts/Source/Tools/AS.Draw.js'
    ],
    asComplexInputSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxComplexInput.js',
'Scripts/Source/Tools/AS.ComplexInput.js'
    ],
    asFormattedInputSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxFormattedInput.js',
'Scripts/Source/Tools/AS.FormattedInput.js'
    ],
    asNumberInputSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxNumberInput.js',
'Scripts/Source/Tools/AS.NumberInput.js'
    ],
    asDropDownButtonSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxDropDownButton.js',
'Scripts/Source/Tools/AS.DropDownButton.js'
    ],
    asPanelSrc: [
        'Scripts/Source/3rdParty/jqWidgets/jqxpanel.js',
        'Scripts/Source/Tools/AS.Panel.js'
    ],
    distjsOut: 'Scripts/Dist'
}



// Synchronously delete the output asDockingLayout script file(s)
gulp.task('clean-asDockingLayout', [], function (cb) {
    return del([config.distjsOut + '/asDockingLayout.js'], cb);
});

// Synchronously delete the output asTextArea script file(s)
gulp.task('clean-asTextArea', [], function (cb) {
    return del([config.distjsOut + '/asTextArea.js'], cb);
});

// Synchronously delete the output asProgressBar script file(s)
gulp.task('clean-asProgressBar', [], function (cb) {
    return del([config.distjsOut + '/asProgressBar.js'], cb);
});

// Synchronously delete the output asFileUpload script file(s)
gulp.task('clean-asFileUpload', [], function (cb) {
    return del([config.distjsOut + '/asFileUpload.js'], cb);
});

// Synchronously delete the output asMaskedInput script file(s)
gulp.task('clean-asMaskedInput', [], function (cb) {
    return del([config.distjsOut + '/asMaskedInput.js'], cb);
});

// Synchronously delete the output asLayout script file(s)
gulp.task('clean-asLayout', [], function (cb) {
    return del([config.distjsOut + '/asLayout.js'], cb);
});

// Synchronously delete the output asValidator script file(s)
gulp.task('clean-asValidator', [], function (cb) {
    return del([config.distjsOut + '/asValidator.js'], cb);
});

// Synchronously delete the output asColorPicker script file(s)
gulp.task('clean-asColorPicker', [], function (cb) {
    return del([config.distjsOut + '/asColorPicker.js'], cb);
});

// Synchronously delete the output asTooltip script file(s)
gulp.task('clean-asTooltip', [], function (cb) {
    return del([config.distjsOut + '/asTooltip.js'], cb);
});

// Synchronously delete the output asGauge script file(s)
gulp.task('clean-asGauge', [], function (cb) {
    return del([config.distjsOut + '/asGauge.js'], cb);
});

// Synchronously delete the output asRibbon script file(s)
gulp.task('clean-asRibbon', [], function (cb) {
    return del([config.distjsOut + '/asRibbon.js'], cb);
});

// Synchronously delete the output asEditor script file(s)
gulp.task('clean-asEditor', [], function (cb) {
    return del([config.distjsOut + '/asEditor.js'], cb);
});

// Synchronously delete the output asRangeSelector script file(s)
gulp.task('clean-asRangeSelector', [], function (cb) {
    return del([config.distjsOut + '/asRangeSelector.js'], cb);
});

// Synchronously delete the output asChart script file(s)
gulp.task('clean-asChart', [], function (cb) {
    return del([config.distjsOut + '/asChart.js'], cb);
});

// Synchronously delete the output asDraw script file(s)
gulp.task('clean-asDraw', [], function (cb) {
    return del([config.distjsOut + '/asDraw.js'], cb);
});

// Synchronously delete the output asComplexInput script file(s)
gulp.task('clean-asComplexInput', [], function (cb) {
    return del([config.distjsOut + '/asComplexInput.js'], cb);
});

// Synchronously delete the output asFormattedInput script file(s)
gulp.task('clean-asFormattedInput', [], function (cb) {
    return del([config.distjsOut + '/asFormattedInput.js'], cb);
});

// Synchronously delete the output asNumberInput script file(s)
gulp.task('clean-asNumberInput', [], function (cb) {
    return del([config.distjsOut + '/asNumberInput.js'], cb);
});

// Synchronously delete the output asDropDownButton script file(s)
gulp.task('clean-asDropDownButton', [], function (cb) {
    return del([config.distjsOut + '/asDropDownButton.js'], cb);
});

// Synchronously delete the output asPanel script file(s)
gulp.task('clean-asPanel', [], function (cb) {
    return del([config.distjsOut + '/asPanel.js'], cb);
});



//Create a asDockingLayout bundled file
gulp.task('AS.DockingLayout', ['clean-asDockingLayout'], function () {
    return gulp.src(config.asDockingLayoutSrc)
     .pipe(uglify())
     .pipe(concat('asDockingLayout.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asTextArea bundled file
gulp.task('AS.TextArea', ['clean-asTextArea'], function () {
    return gulp.src(config.asTextAreaSrc)
     .pipe(uglify())
     .pipe(concat('asTextArea.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asProgressBar bundled file
gulp.task('AS.ProgressBar', ['clean-asProgressBar'], function () {
    return gulp.src(config.asProgressBarSrc)
     .pipe(uglify())
     .pipe(concat('asProgressBar.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asFileUpload bundled file
gulp.task('AS.FileUpload', ['clean-asFileUpload'], function () {
    return gulp.src(config.asFileUploadSrc)
     .pipe(uglify())
     .pipe(concat('asFileUpload.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asMaskedInput bundled file
gulp.task('AS.MaskedInput', ['clean-asMaskedInput'], function () {
    return gulp.src(config.asMaskedInputSrc)
     .pipe(uglify())
     .pipe(concat('asMaskedInput.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asLayout bundled file
gulp.task('AS.Layout', ['clean-asLayout'], function () {
    return gulp.src(config.asLayoutSrc)
     .pipe(uglify())
     .pipe(concat('asLayout.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asValidator bundled file
gulp.task('AS.Validator', ['clean-asValidator'], function () {
    return gulp.src(config.asValidatorSrc)
     .pipe(uglify())
     .pipe(concat('asValidator.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asColorPicker bundled file
gulp.task('AS.ColorPicker', ['clean-asColorPicker'], function () {
    return gulp.src(config.asColorPickerSrc)
     .pipe(uglify())
     .pipe(concat('asColorPicker.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asTooltip bundled file
gulp.task('AS.Tooltip', ['clean-asTooltip'], function () {
    return gulp.src(config.asTooltipSrc)
     .pipe(uglify())
     .pipe(concat('asTooltip.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asGauge bundled file
gulp.task('AS.Gauge', ['clean-asGauge'], function () {
    return gulp.src(config.asGaugeSrc)
     .pipe(uglify())
     .pipe(concat('asGauge.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asRibbon bundled file
gulp.task('AS.Ribbon', ['clean-asRibbon'], function () {
    return gulp.src(config.asRibbonSrc)
     .pipe(uglify())
     .pipe(concat('asRibbon.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asEditor bundled file
gulp.task('AS.Editor', ['clean-asEditor'], function () {
    return gulp.src(config.asEditorSrc)
     .pipe(uglify())
     .pipe(concat('asEditor.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asRangeSelector bundled file
gulp.task('AS.RangeSelector', ['clean-asRangeSelector'], function () {
    return gulp.src(config.asRangeSelectorSrc)
     .pipe(uglify())
     .pipe(concat('asRangeSelector.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asChart bundled file
gulp.task('AS.Chart', ['clean-asChart'], function () {
    return gulp.src(config.asChartSrc)
     .pipe(uglify())
     .pipe(concat('asChart.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asDraw bundled file
gulp.task('AS.Draw', ['clean-asDraw'], function () {
    return gulp.src(config.asDrawSrc)
     .pipe(uglify())
     .pipe(concat('asDraw.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asComplexInput bundled file
gulp.task('AS.ComplexInput', ['clean-asComplexInput'], function () {
    return gulp.src(config.asComplexInputSrc)
     .pipe(uglify())
     .pipe(concat('asComplexInput.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asFormattedInput bundled file
gulp.task('AS.FormattedInput', ['clean-asFormattedInput'], function () {
    return gulp.src(config.asFormattedInputSrc)
     .pipe(uglify())
     .pipe(concat('asFormattedInput.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asNumberInput bundled file
gulp.task('AS.NumberInput', ['clean-asNumberInput'], function () {
    return gulp.src(config.asNumberInputSrc)
     .pipe(uglify())
     .pipe(concat('asNumberInput.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asDropDownButton bundled file
gulp.task('AS.DropDownButton', ['clean-asDropDownButton'], function () {
    return gulp.src(config.asDropDownButtonSrc)
     .pipe(uglify())
     .pipe(concat('asDropDownButton.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asDropDownButton bundled file
gulp.task('AS.Panel', ['clean-asPanel'], function () {
    return gulp.src(config.asPanelSrc)
     .pipe(uglify())
     .pipe(concat('asPanel.js'))
     .pipe(gulp.dest(config.distjsOut));
});


gulp.task('scripts', [
    'AS.DockingLayout',
    'AS.TextArea',
    'AS.ProgressBar',
    'AS.FileUpload',
    'AS.MaskedInput',
    'AS.Layout',
    'AS.Validator',
    'AS.ColorPicker',
    'AS.Tooltip',
    'AS.Gauge',
    'AS.Ribbon',
    'AS.Editor',
    'AS.RangeSelector',
    'AS.Chart',
    'AS.Draw',
    'AS.ComplexInput',
    'AS.FormattedInput',
    'AS.NumberInput',
    'AS.DropDownButton',
    'AS.Panel'
], function () {

});



//Restore all bower packages
gulp.task('bower-restore', function () {
    return bower();
});

