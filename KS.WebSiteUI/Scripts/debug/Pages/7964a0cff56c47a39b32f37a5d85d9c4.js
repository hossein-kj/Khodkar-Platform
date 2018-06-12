 $('#i7964a0cff56c47a39b32f37a5d85d9c4').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('Directory Selector');var asPageEvent = '#i7964a0cff56c47a39b32f37a5d85d9c4'; var asPageId = '.i7964a0cff56c47a39b32f37a5d85d9c4.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({fms_GetFoldersOfPathByPaging:"/fms/GetFoldersByPagination/@path/@orderby/@skip/@take"}, $.asUrls); var 
     $win=$(asPageId),
     $btnCancel=as("#btnCancel"),
     $btnSelect=as("#btnSelect"),
     $grvFolder=as("#grvFolder"),
     $divPath=as("#divPath"),
    requestedPath="",
     selectedFolder,
     selectedId,
     paths=[],
     pathIndex=0,
     rootUrl,
     subUrl,
     changeSubFolder = false;
     

   var reloadGrid = function(){
         pathIndex++;
               changeSubFolder = true;
               $grvFolder.asBootGrid("reload");
    }
    var handler = function(){
  
                      $divPath.empty()
                      setLink(-1,"...")
              pathIndex = $(this).data("index")
              if(pathIndex === -1){
                  pathIndex=0;
                for(var i=0;i<=paths.length - 1;i++){
                      paths[i]=null
                         }

                changeSubFolder=false;
                
                 $grvFolder.asBootGrid("reload");
              }else{
                             subUrl = rootUrl;
              for(var i=0;i<=paths.length - 1;i++){
                  if(i<=pathIndex && paths[i] !== null){
                      setLink(i,paths[i])
                      
                  }else
                      paths[i]=null
              }
               reloadGrid() 
              }

          };

    var setLink = function(index,value){
         var  link = $("<span  data-index='" + index + "' style='font-weight: bold;cursor: pointer;'>" + value + "</span> > ")
                $divPath.append(link);
                link.click(handler);
                if(index !== -1)
                 subUrl += "/" + value;
    }
    
            if(asPageParams){
                 requestedPath=asPageParams.path
                  $divPath.empty();
                 setLink(-1,"...")
           }
                $grvFolder.asBootGrid({
    rowCount:[10,25,50,100],
    source:{
        url:''
    },
    requestHandler:function(request){
     
         if (requestedPath !== "") {
       
        
               var pageUrl=""
        if(!changeSubFolder){
            rootUrl = requestedPath
            pageUrl = rootUrl.replace(/\//g, $.asUrlDelimeter)
        }
        
        else{
           
            pageUrl = subUrl.replace(/\//g, $.asUrlDelimeter)
        }
        
        
        var orderbyValue = "Name desc"
        var skip = 0
        if(request.current > 1)
        skip=(request.current - 1) * request.rowCount
        $.each(request.sort, function(key, value) {
                 orderbyValue = key + " " + value
                });
        request.url = $.asInitService($.asUrls.fms_GetFoldersOfPathByPaging, [
            { name: '@path', value: pageUrl }
            ,{ name: '@orderby', value: orderbyValue }
            ,{ name: '@skip', value: skip }
             ,{ name: '@take', value: request.rowCount}]);
            
              $grvFolder.asBootGrid("deselect");
             return request
         }
    },
    formatters: {
        Name: function (column, row)
        {
            /* "this" is mapped to the grid instance */
            if(row.IsFolder)
            return "<span class='folder-link' data-row='" + JSON.stringify(row) + "' style='font-weight: bold;cursor: pointer;'>" + "<i class='glyphicon glyphicon-folder-open'></i> &nbsp;" + row.Name + "</span>" ;
            else
            return "<span>" + "<i class='glyphicon glyphicon-file'></i> &nbsp;" + row.Name + "</span>";
        }
    },
        selection: true,
        rowSelect:true
}).on("selected.rs.jquery.asBootGrid", function(e, rows)
{
    selectedFolder =rows[0].Name
    selectedId=rows[0].Id
}).on("deselected.rs.jquery.asBootGrid", function(e, rows)
{
    selectedFolder=""
}).on("loaded.rs.jquery.asBootGrid", function(e,columns, row)
{
 
   $(".folder-link").click(function(){
       var row = $(this).data("row")
     
         if(row.IsFolder){
       var index= $.inArray( row.Name, paths);

          
       if(index === -1){
           
           paths[pathIndex]=row.Name
       $divPath.empty()
       setLink(-1,"...")
            subUrl = requestedPath;
           $.each(paths,function(i,v){
            if(v !== null){
                 setLink(i,v)
              
                }
           });
            reloadGrid();
       }
      
    }
    
   })

});

 var bindEvent =function(){
            $(asPageEvent).on($.asEvent.modal.reopen, function (event,params) {
                if(params.path !== asPageParams.path || params.parent !== asPageParams.parent || params.event !== asPageParams.event){
                asPageParams=params;
                     subUrl = rootUrl=requestedPath=asPageParams.path;
                   $divPath.empty();
                   paths=[];
                   pathIndex=0;
                   
                      $grvFolder.asBootGrid("reload");
            }
        });

     $(asPageEvent).on($.asEvent.page.ready, function (event) {
          
            
        });
    
     $btnCancel.on('click', function () {
                $win.asModal('hide');
            })
            
     $btnSelect.on('click', function () {
                    if(selectedFolder != "" && typeof(selectedFolder) != "undefined" && $grvFolder.asBootGrid("getTotalRowCount") !== 0){
                    if(asPageParams){
                        var path = requestedPath;
                                $.each(paths,function(i,v){
                                if(v !== null){
                                     if(i != -1)
                                        path += "/"+ v 
                                    }
                              });
                      $(asPageParams.parent).trigger(asPageParams.event, [(path + "/" +selectedFolder).replace(new RegExp("//", "gi"),"/"),selectedId])
                    }
                     $win.asModal('hide');
                  }else{
                        $.asShowMessage({ template: "error", message:  "مسیری انتخاب نشده است"})
                  }
            });
                    asOnPageDispose = function(){
          $grvFolder.asBootGrid("destroy");
        }
 }
 bindEvent()  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });