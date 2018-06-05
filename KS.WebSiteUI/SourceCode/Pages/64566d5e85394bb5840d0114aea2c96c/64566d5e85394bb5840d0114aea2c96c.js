var 
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
 bindEvent()