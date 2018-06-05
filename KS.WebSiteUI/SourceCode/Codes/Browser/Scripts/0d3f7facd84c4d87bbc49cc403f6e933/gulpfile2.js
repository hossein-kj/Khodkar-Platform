
// include plug-ins
var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var del = require('del');

var copy = require('gulp-copy');
var bower = require('gulp-bower');
var sourcemaps = require('gulp-sourcemaps');

var config = {
    asScrollBarSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxscrollbar.js',
'Scripts/Source/Tools/AS.ScrollBar.js'
    ],
    asListBoxSrc: [
   'Scripts/Source/3rdParty/jqWidgets/jqxlistbox.js',
   'Scripts/Source/Tools/AS.ListBox.js'
    ],
    asPopoverSrc: [
   'Scripts/Source/3rdParty/jqWidgets/jqxpopover.js',
   'Scripts/Source/Tools/AS.Popover.js'
    ],
    asModalSrc: [
'Scripts/Source/Tools/Modal/bootstrap-modal.js',
'Scripts/Source/Tools/Modal/bootstrap-modalmanager.js',
'Scripts/Source/Tools/AS.Modal.js'
    ],
    asResponseSrc: [
  'Scripts/Source/3rdParty/jqWidgets/jqxresponse.js',
  'Scripts/Source/Tools/AS.Response.js'
    ],
    asSplitterSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxsplitter.js',
'Scripts/Source/Tools/AS.Splitter.js'
    ],
    asTabsSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxtabs.js',
'Scripts/Source/Tools/AS.Tabs.js'
    ],
    asTreeSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxtree.js',
'Scripts/Source/Tools/AS.Tree.js'
    ],
    asTreeGridSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxtreegrid.js',
'Scripts/Source/Tools/AS.TreeGrid.js'
    ],
    asWindowSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxwindow.js',
'Scripts/Source/Tools/AS.Window.js'
    ],
    asGridAggregatesSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxgrid.aggregates.js'
    ],
    asGridColumnsreorderSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxgrid.columnsreorder.js'
    ],
    asGridColumnsresizeSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxgrid.columnsresize.js'
    ],
    asGridEditSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxgrid.edit.js'
    ],
    asGridExportSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxgrid.export.js'
    ],
    asGridFilterSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxgrid.filter.js'
    ],
    asGridGroupingSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxgrid.grouping.js'
    ],
    asGridPagerSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxgrid.pager.js'
    ],
    asGridSelectionSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxgrid.selection.js'
    ],
    asGridSortSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxgrid.sort.js'
    ],
    asGridStorageSrc: [
'Scripts/Source/3rdParty/jqWidgets/jqxgrid.storage.js'
    ],
    distjsOut: 'Scripts/Dist'
}

// Synchronously delete the output asScrollBar script file(s)
gulp.task('clean-asScrollBar', [], function (cb) {
    return del([config.distjsOut + '/asScrollBar.js'], cb);
});

// Synchronously delete the output asListBox script file(s)
gulp.task('clean-asListBox', [], function (cb) {
    return del([config.distjsOut + '/asListBox.js'], cb);
});

// Synchronously delete the output asPopover script file(s)
gulp.task('clean-asPopover', [], function (cb) {
    return del([config.distjsOut + '/asPopover.js'], cb);
});

// Synchronously delete the output asModal script file(s)
gulp.task('clean-asModal', [], function (cb) {
    return del([config.distjsOut + '/asModal.js'], cb);
});

// Synchronously delete the output asResponse script file(s)
gulp.task('clean-asResponse', [], function (cb) {
    return del([config.distjsOut + '/asResponse.js'], cb);
});

// Synchronously delete the output asSplitter script file(s)
gulp.task('clean-asSplitter', [], function (cb) {
    return del([config.distjsOut + '/asSplitter.js'], cb);
});

// Synchronously delete the output asTabs script file(s)
gulp.task('clean-asTabs', [], function (cb) {
    return del([config.distjsOut + '/asTabs.js'], cb);
});

// Synchronously delete the output asTreeGrid script file(s)
gulp.task('clean-asTreeGrid', [], function (cb) {
    return del([config.distjsOut + '/asTreeGrid.js'], cb);
});

// Synchronously delete the output asTree script file(s)
gulp.task('clean-asTree', [], function (cb) {
    return del([config.distjsOut + '/asTree.js'], cb);
});

// Synchronously delete the output asWindow script file(s)
gulp.task('clean-asWindow', [], function (cb) {
    return del([config.distjsOut + '/asWindow.js'], cb);
});

// Synchronously delete the output asGridAggregates script file(s)
gulp.task('clean-asGridAggregates', [], function (cb) {
    return del([config.distjsOut + '/asGrid.Aggregates.js'], cb);
});

// Synchronously delete the output asGridColumnsreorder script file(s)
gulp.task('clean-asGridColumnsreorder', [], function (cb) {
    return del([config.distjsOut + '/asGrid.Columnsreorder.js'], cb);
});

// Synchronously delete the output asGridColumnsresize script file(s)
gulp.task('clean-asGridColumnsresize', [], function (cb) {
    return del([config.distjsOut + '/asGrid.Columnsresize.js'], cb);
});

// Synchronously delete the output asGridEdit script file(s)
gulp.task('clean-asGridEdit', [], function (cb) {
    return del([config.distjsOut + '/asGrid.Edit.js'], cb);
});

// Synchronously delete the output asGridFilter script file(s)
gulp.task('clean-asGridFilter', [], function (cb) {
    return del([config.distjsOut + '/asGrid.Filter.js'], cb);
});

// Synchronously delete the output asGridFilter script file(s)
gulp.task('clean-asGridFilter', [], function (cb) {
    return del([config.distjsOut + '/asGrid.Filter.js'], cb);
});

// Synchronously delete the output asGridGrouping script file(s)
gulp.task('clean-asGridGrouping', [], function (cb) {
    return del([config.distjsOut + '/asGrid.Grouping.js'], cb);
});

// Synchronously delete the output asGridPager script file(s)
gulp.task('clean-asGridPager', [], function (cb) {
    return del([config.distjsOut + '/asGrid.Pager.js'], cb);
});

// Synchronously delete the output asGridSelection script file(s)
gulp.task('clean-asGridSelection', [], function (cb) {
    return del([config.distjsOut + '/asGrid.Selection.js'], cb);
});

// Synchronously delete the output asGridSort script file(s)
gulp.task('clean-asGridSort', [], function (cb) {
    return del([config.distjsOut + '/asGrid.Sort.js'], cb);
});

// Synchronously delete the output asGridStorage script file(s)
gulp.task('clean-asGridStorage', [], function (cb) {
    return del([config.distjsOut + '/asGrid.Storage.js'], cb);
});

// Synchronously delete the output asGridExport script file(s)
gulp.task('clean-asGridExport', [], function (cb) {
    return del([config.distjsOut + '/asGrid.Export.js'], cb);
});

//Create a asListBox bundled file
gulp.task('AS.ScrollBar', ['clean-asScrollBar'], function () {
    return gulp.src(config.asScrollBarSrc)
     .pipe(uglify())
     .pipe(concat('asScrollBar.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asListBox bundled file
gulp.task('AS.ListBox', ['clean-asListBox'], function () {
    return gulp.src(config.asListBoxSrc)
     .pipe(uglify())
     .pipe(concat('asListBox.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asPopover bundled file
gulp.task('AS.Popover', ['clean-asPopover'], function () {
    return gulp.src(config.asPopoverSrc)
     .pipe(uglify())
     .pipe(concat('asPopover.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asModal bundled file
gulp.task('AS.Modal', ['clean-asModal'], function () {
    return gulp.src(config.asModalSrc)
     .pipe(uglify())
     .pipe(concat('asModal.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asResponse bundled file
gulp.task('AS.Response', ['clean-asResponse'], function () {
    return gulp.src(config.asResponseSrc)
     .pipe(uglify())
     .pipe(concat('asResponse.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asSplitter bundled file
gulp.task('AS.Splitter', ['clean-asSplitter'], function () {
    return gulp.src(config.asSplitterSrc)
     .pipe(uglify())
     .pipe(concat('asSplitter.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asSplitter bundled file
gulp.task('AS.Tabs', ['clean-asTabs'], function () {
    return gulp.src(config.asTabsSrc)
     .pipe(uglify())
     .pipe(concat('asTabs.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asTree bundled file
gulp.task('AS.Tree', ['clean-asTree'], function () {
    return gulp.src(config.asTreeSrc)
     .pipe(uglify())
     .pipe(concat('asTree.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asTreeGrid bundled file
gulp.task('AS.TreeGrid', ['clean-asTreeGrid'], function () {
    return gulp.src(config.asTreeGridSrc)
     .pipe(uglify())
     .pipe(concat('asTreeGrid.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asWindow bundled file
gulp.task('AS.Window', ['clean-asWindow'], function () {
    return gulp.src(config.asWindowSrc)
     .pipe(uglify())
     .pipe(concat('asWindow.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asGridAggregates bundled file
gulp.task('AS.GridAggregates', ['clean-asGridAggregates'], function () {
    return gulp.src(config.asGridAggregatesSrc)
     .pipe(uglify())
     .pipe(concat('asGrid.Aggregates.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asGridColumnsreorder bundled file
gulp.task('AS.GridColumnsreorder', ['clean-asGridColumnsreorder'], function () {
    return gulp.src(config.asGridColumnsreorderSrc)
     .pipe(uglify())
     .pipe(concat('asGrid.Columnsreorder.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asGridColumnsresize bundled file
gulp.task('AS.GridColumnsresize', ['clean-asGridColumnsresize'], function () {
    return gulp.src(config.asGridColumnsresizeSrc)
     .pipe(uglify())
     .pipe(concat('asGrid.Columnsresize.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asGridEdit bundled file
gulp.task('AS.GridEdit', ['clean-asGridEdit'], function () {
    return gulp.src(config.asGridEditSrc)
     .pipe(uglify())
     .pipe(concat('asGrid.Edit.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asGridExport bundled file
gulp.task('AS.GridExport', ['clean-asGridExport'], function () {
    return gulp.src(config.asGridExportSrc)
     .pipe(uglify())
     .pipe(concat('asGrid.Export.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asGridFilter bundled file
gulp.task('AS.GridFilter', ['clean-asGridFilter'], function () {
    return gulp.src(config.asGridFilterSrc)
     .pipe(uglify())
     .pipe(concat('asGrid.Filter.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asGridGrouping bundled file
gulp.task('AS.GridGrouping', ['clean-asGridGrouping'], function () {
    return gulp.src(config.asGridGroupingSrc)
     .pipe(uglify())
     .pipe(concat('asGrid.Grouping.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asGridPager bundled file
gulp.task('AS.GridPager', ['clean-asGridPager'], function () {
    return gulp.src(config.asGridPagerSrc)
     .pipe(uglify())
     .pipe(concat('asGrid.Pager.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asGridSelection bundled file
gulp.task('AS.GridSelection', ['clean-asGridSelection'], function () {
    return gulp.src(config.asGridSelectionSrc)
     .pipe(uglify())
     .pipe(concat('asGrid.Selection.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asGridSort bundled file
gulp.task('AS.GridSort', ['clean-asGridSort'], function () {
    return gulp.src(config.asGridSortSrc)
     .pipe(uglify())
     .pipe(concat('asGrid.Sort.js'))
     .pipe(gulp.dest(config.distjsOut));
});

//Create a asGridStorage bundled file
gulp.task('AS.GridStorage', ['clean-asGridStorage'], function () {
    return gulp.src(config.asGridStorageSrc)
     .pipe(uglify())
     .pipe(concat('asGrid.Storage.js'))
     .pipe(gulp.dest(config.distjsOut));
});


gulp.task('scripts', [
    'AS.ScrollBar',
    'AS.ListBox',
    'AS.Popover',
    'AS.Modal',
    'AS.Response',
    'AS.Splitter',
    'AS.Tabs',
    'AS.Tree',
    'AS.TreeGrid',
    'AS.Window',
    'AS.GridAggregates',
    'AS.GridColumnsreorder',
    'AS.GridColumnsresize',
    'AS.GridEdit',
    'AS.GridExport',
    'AS.GridFilter',
    'AS.GridGrouping',
    'AS.GridPager',
    'AS.GridSelection',
    'AS.GridSort',
    'AS.GridStorage'
], function () {

});



//Restore all bower packages
gulp.task('bower-restore', function () {
    return bower();
});

