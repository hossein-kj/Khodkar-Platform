 $('#i64566d5e85394bb5840d0114aea2c96c').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('انتخاب مسیر');var asPageEvent = '#i64566d5e85394bb5840d0114aea2c96c'; var asPageId = '.i64566d5e85394bb5840d0114aea2c96c.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({cms_filePath_GetByDefaultsLanguagesAndTypeCodeByPaging:"/odata/cms/FilePaths?$filter=TypeCode%20eq%20@typeCoded&$orderby=@orderby&$skip=@skip&$top=@top&$select=Id%2CName%2CDescription%2CUrl%2CGuid&$inlinecount=allpages"}, $.asUrls); var 
     $win=$(asPageId),
     $btnCancel=as("#btnCancel"),
     $btnSelect=as("#btnSelect"),
     selectedPath,
     selectedId;
    
    as("#grvPath").asBootGrid({
    rowCount:[5,10,25,50,100],
    source:{
        url:''
    },
    requestHandler:function(request){
        var orderbyValue = "Name desc"
        var skip = 0
        if(request.current > 1)
        skip=(request.current - 1) * request.rowCount
        $.each(request.sort, function(key, value) {
                 orderbyValue = key + " " + value
                });
        request.url = $.asInitService($.asUrls.cms_filePath_GetByDefaultsLanguagesAndTypeCodeByPaging, [
            { name: '@typeCode', value: 1 }
            ,{ name: '@orderby', value: orderbyValue }
            ,{ name: '@skip', value: skip }
             ,{ name: '@top', value: request.rowCount}])
             return request
    },
    formatters: {
        Url: function (column, row)
        {
            /* "this" is mapped to the grid instance */
            return "<img src='" + ($.asThumbnailPath + row.Url).replace("//","/").replace("~","") + "' alt='Smiley face'>";
        }
    },
        selection: true,
    rowSelect: true,
}).on("selected.rs.jquery.asBootGrid", function(e, rows)
{
    selectedPath =rows[0].Url
    selectedId=rows[0].Id
}).on("deselected.rs.jquery.asBootGrid", function(e, rows)
{
    selectedPath=""
});
 var bindEvent =function(){
     
            $(asPageEvent).on($.asEvent.modal.reopen, function (event,params) {
            if(params.parent !== asPageParams.parent || params.event !== asPageParams.event){
                asPageParams=params;
            }
        });
        
     $(asPageEvent).on($.asEvent.page.ready, function (event) {
   
        });
    
            $btnCancel.on('click', function () {
                $win.asModal('hide');
            })
            
                $btnSelect.on('click', function () {
                    if(selectedPath != "" && typeof(selectedPath) != "undefined"){
                    if(asPageParams){
                      $(asPageParams.parent).trigger(asPageParams.event, [selectedPath,selectedId])
                    }
                     $win.asModal('hide');
                   }else{
                        $.asShowMessage({ template: "error", message:  "مسیری انتخاب نشده است"})
                   }
            })
 }
 bindEvent()  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });