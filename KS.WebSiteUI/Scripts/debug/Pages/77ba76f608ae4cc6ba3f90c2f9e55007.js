 $('#i77ba76f608ae4cc6ba3f90c2f9e55007').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('انتخاب فایل');var asPageEvent = '#i77ba76f608ae4cc6ba3f90c2f9e55007'; var asPageId = '.i77ba76f608ae4cc6ba3f90c2f9e55007.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({fms_GetFileAndFoldersOfPathByPaging:"/fms/GetByPagination/@path/@orderby/@skip/@take/@createThumbnail"}, $.asUrls); 
var 
     $win=$(asPageId),
     $btnCancel=as("#btnCancel"),
     $btnSelect=as("#btnSelect"),
     $grvFile=as("#grvFile"),
     $divPath=as("#divPath"),
    requestedPath="",
     selectedFile,
     selectedId,
     paths=[],
     pathIndex=0,
     rootUrl,
     subUrl,
     changeSubFolder = false;
     

   var reloadGrid = function(){
         pathIndex++;
               changeSubFolder = true;
               $grvFile.asBootGrid("reload");
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
                
                 $grvFile.asBootGrid("reload");
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
                $grvFile.asBootGrid({
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
        request.url = $.asInitService($.asUrls.fms_GetFileAndFoldersOfPathByPaging, [
            { name: '@path', value: pageUrl }
            ,{ name: '@orderby', value: orderbyValue }
            ,{ name: '@skip', value: skip }
             ,{ name: '@take', value: request.rowCount}
             ,{ name: '@createThumbnail', value: false}]);
            
              $grvFile.asBootGrid("deselect");
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
    if(rows[0].IsFolder === false){
        selectedFile =rows[0].Name
    selectedId=rows[0].Id
    }
    
}).on("deselected.rs.jquery.asBootGrid", function(e, rows)
{
    selectedFile=""
}).on("loaded.rs.jquery.asBootGrid", function(e,columns, row)
{
 
   as(".folder-link").click(function(){
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
                   
                      $grvFile.asBootGrid("reload");
            }
        });
     $(asPageEvent).on($.asEvent.page.ready, function (event) {
          
            
        });
    
     $btnCancel.on('click', function () {
                $win.asModal('hide');
            })
            
     $btnSelect.on('click', function () {
                    if(selectedFile !== "" && typeof(selectedFile) != "undefined"){
                    if(asPageParams){
                        var fullPath = ""
                        $.each(paths,function(i,v){
                        if(v !== null){
                            fullPath += v + "/"
                            }
                       });
                      $(asPageParams.parent).trigger(asPageParams.event, [(requestedPath + "/" + fullPath + selectedFile).replace(new RegExp("//", "gi"), "/"),selectedId,selectedFile])
                    }
                     $win.asModal('hide');
                   }else{
                        $.asShowMessage({ template: "error", message:  "فایلی انتخاب نشده است "})
                   }
            })
            
     asOnPageDispose = function(){
          $grvFile.asBootGrid("destroy");
           as(".folder-link").off();
        }
            
 }
 bindEvent();
  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });