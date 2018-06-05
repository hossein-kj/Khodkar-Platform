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
                 orderbyValue = key.replace(".","/") + " " + value
                });
        request.url = $.asInitService($.asUrls.cms_localFilePath_GetByOtherLanguagesAndTypeCodeByPaging , [
             { name: '@lang', value: $.asLang }
            ,{ name: '@typeCode', value: 1 }
            ,{ name: '@orderby', value: orderbyValue }
            ,{ name: '@skip', value: skip }
             ,{ name: '@top', value: request.rowCount}])
         
             return request
    },
    formatters: {
        Url: function (column, row)
        {
            /* "this" is mapped to the grid instance */
            return "<img src='" + ($.asThumbnailPath + (row.FilePath ? row.FilePath.Url : row.Url)).replace("//","/").replace("~","") + "' alt='Smiley face'>";
        },
        Id: function (column, row)
        {
            return row.FilePath ? row.FilePath.Id : row.Id;
        }
    },
        selection: true,
    rowSelect: true,
}).on("selected.rs.jquery.asBootGrid", function(e, rows)
{
    selectedPath =rows[0].FilePath.Url
    selectedId=rows[0].FilePath.Id
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
                    if(selectedPath !== "" && typeof(selectedPath) != "undefined"){
                    if(asPageParams){
                      $(asPageParams.parent).trigger(asPageParams.event, [selectedPath,selectedId])
                    }
                     $win.asModal('hide');
                   }else{
                        $.asShowMessage({ template: "error", message:  "No Path Selected"})
                   }
            })
 }
 bindEvent()