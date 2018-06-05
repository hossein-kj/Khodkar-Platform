var
    $frm = as("#frmZip"),
    $frmAction=as("#frmAction"),
    $btnFolderSelector=as("#btnFolderSelector"),
    $winFileSelector=$.asModalManager.get({url:$.asModalManager.urls.fileSelector}),
    $winFolderSelector=$.asModalManager.get({url:$.asModalManager.urls.directorySelector}),
    $winFileAddOrUpdate=$.asModalManager.get({url:$.asModalManager.urls.fileAddOrUpdate}),
    $winRemoteDownloadManager=$.asModalManager.get({url:$.asModalManager.urls.remoteDownloadManager}),
    $winUploadManager=$.asModalManager.get({url:$.asModalManager.urls.uploadManager}),
    $winAction=as("#winAction"),
    $winZipOption=as("#winZipOption"),
    $winZipViewer=as("#winZipViewer"),
    $drpPath= as("#drpPath"),
    $drpCompression=as("#drpCompression"),
    $drpReplace=as("#drpReplace"),
    $drpEncryption=as("#drpEncryption"),
    $txtNameZip=as("#txtNameZip"),
    $txtNameZipDir=as("#txtNameZipDir"),
    $txtPassword=as("#txtPassword"),
    $txtName=as("#txtName"),
    $btnUploadFromUrl=as("#btnUploadFromUrl"),
     $grvFile=as("#grvFile"),
     $divPath=as("#divPath"),
     $btnMove=as("#btnMove"),
     $btnCopyByReplace=as("#btnCopyByReplace"),
     $btnCopyMain=as("#btnCopyMain"),
     $btnCopy = as("#btnCopy"),
     $btnZip=as("#btnZip"),
     $btnZipView=as("#btnZipView"),
     $btnZipAddOrUpdate=as("#btnZipAddOrUpdate"),
     $btnUnZip=as("#btnUnZip"),
     $btnSelectZip=as("#btnSelectZip"),
     $btnDell=as("#btnDell"),
     $btnAddOrUpdateFile=as("#btnAddOrUpdateFile"),
     $chkTumbnail=as("#chkTumbnail"),
     $btnRename=as("#btnRename"),
     $btnExecute=as("#btnExecute"),
     $btnSelectZipDir=as("#btnSelectZipDir"),
     $btnExecAction=as("#btnExecAction"),
     $btnCancelAction=as("#btnCancelAction"),
     $btnCreateDir=as("#btnCreateDir"),
     $lblNameZipDir=as("#lblNameZipDir"),
     $chkNew=as("#chkNew"),
     $chkReplaceZip=as("#chkReplaceZip"),
     $divZipDir=as("#divZipDir"),
     $divReplace=as("#divReplace"),
    $btnUploadFromDisk=as("#btnUploadFromDisk"),
     $txtFileName=as("#txtFileName"),
     $txtFileContent=as("#txtFileContent"),
        selectedPath,
     selectedPaths="",
     paths=[],
     pathIndex=0,
     rootUrl,
     subUrl,
     changeSubFolder = false,
     selectedItems={
         hasFolder:function(){
            var result = false
             $.each(this.items,function(i,v){
               
                 if(v === true){
                       result= true;
                 }
               
             })
             return result;
         },
         path:"",
         items:{}
     },
     isFolderClick = false,
     actionMode="move",
     zipActionMode="zip",
     fileActionMode = "add",
     replaceAction="OverwriteSilently",
     actionMode="rename",
     encryption="PkzipWeak",
     compressionLevel="Default",
     validate,
     validateAction,
     validateFileAction,
     selectedForRename="",
     isRenameDirectory=false,
     totalRowCount=0;
     var onSuccess = function(){
           $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
              selectedItems.items={};
     }
     var validateRule = {
         txtNameZip:{
            required: true
        },
        txtNameZipDir:{
            required: {
                depends: function (element) {
                return $(element).is(":visible");
            }
            }
        }
    };
    
    validate =$frm.asValidate({ rules: validateRule});
    
    
     var validateRuleAction = {
         txtName:{
            required: true
        }
    };
    
    validateAction =$frmAction.asValidate({ rules: validateRuleAction});
    

     
     $winZipOption.asModal(
    {backdrop:'static', keyboard: false}
    );
    $winAction.asModal(
    {backdrop:'static', keyboard: false}
    );
  
     $winFileSelector.asModal({width:800}) ;
     $winZipViewer.asModal({width:800}) ;
     $winFolderSelector.asModal({width:800}) ;
     $winFileAddOrUpdate.asModal({width:800}) ;
     $winRemoteDownloadManager.asModal({width:800}) ;
     $winUploadManager.asModal({width:800}) ;
        
  $drpPath.asDropdown({
    source: {
        hierarchy:
        {
            type: 'flat',
            keyDataField: { name: 'MasterDataKeyValue.Id' },
            parentDataField: { name: 'MasterDataKeyValue.ParentId' },
            removeChildLessParent: false
        },
        url:$.asInitService($.asUrls.cms_masterDataKeyValue_GetByOtherLanguagesAndTypeId, [{ name: '@typeId', value: 1025 },{ name: '@lang', value: $.asLang }])
        , displayDataField: 'Name'
          , valueDataField: 'MasterDataKeyValue.PathOrUrl',
        orderby: 'MasterDataKeyValue.Order'
    }
    , selectParents: true
  , parentMode: "uniq"

});
    var btnClick=function(actionName){
                 actionMode=actionName;
          if ($drpPath.asDropdown('selected')) {
            $winFolderSelector.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.directorySelector)},{name:"@isModal",value:true}])
            ,{path:$drpPath.asDropdown('selected').value,parent:asPageEvent,event:"folderSelected"})
          }else
           $.asShowMessage({template:"error", message: "No Path Selected"});
          
    }
    var move=function(files,folders){
                $btnMove.button('loading')  
             $btnMove.asAjax({
            url: $.asUrls.fms_move,
            data: JSON.stringify({
                SourcePath: selectedItems.path.replace(new RegExp($.asUrlDelimeter, "gi"),"/").replace(new RegExp("//", "gi"),"/"),
                DestinationPath:selectedPaths,
                Files:files,
                Folders:folders
            }),
            success: function (result) {
                $grvFile.asBootGrid("remove");
                 $btnMove.button('reset')
             onSuccess()
            },
            error:function(){
                 $btnMove.button('reset')
            }
        },{validate: false})
    }
      var copy=function(files,folders,overWrite){
                $btnCopyMain.button('loading')  
             $btnMove.asAjax({
            url: $.asUrls.fms_copy,
            data: JSON.stringify({
                SourcePath: selectedItems.path.replace(new RegExp($.asUrlDelimeter, "gi"),"/").replace(new RegExp("//", "gi"),"/"),
                DestinationPath:selectedPaths,
                Files:files,
                Folders:folders,
                OverWrite:overWrite
            }),
            success: function (result) {
                 $btnCopyMain.button('reset')
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
            },
            error:function(){
                 $btnCopyMain.button('reset')
            }
        },{validate: false})
    }
 
    var reloadGrid = function(){
         pathIndex++;
               changeSubFolder = true;
               $grvFile.asBootGrid("reload");
    }
    var handler = function(){
        //   isFolderClick=true;
                      $divPath.empty()
              pathIndex = $(this).data("index")
            subUrl = rootUrl;
              for(var i=0;i<=paths.length - 1;i++){
                  if(i<=pathIndex && paths[i] !== null){
                      setLink(i,paths[i])
                      
                  }else
                      paths[i]=null
              }
               reloadGrid()
          };

    var setLink = function(index,value){
         var  link = $("<span  data-index='" + index + "' style='font-weight: bold;cursor: pointer;'>" + value + "</span> > ")
                $divPath.append(link);
                link.click(handler);
                 subUrl += "/" + value;
    }
    var calculateSelectedFileAndFolder = function(event, rows){

        if(event.type==="selected"){
            selectedItems.items[rows[0].Name]=rows[0].IsFolder
        }else{
            delete selectedItems.items[rows[0].Name]
        }
    }
     var bindEvent = function(){
  
  
  
         $drpCompression.asDropdown({
                source: {
                    localData:[
                {Name:"Level1",id:1},
                {Name:"Level2",id:2},
                {Name:"Level3",id:3},
                {Name:"Level4",id:4},
                {Name:"Level5",id:5},
                {Name:"Level6",id:6},
                {Name:"Level7",id:7},
                {Name:"Level8",id:8},
                {Name:"Level9",id:9},
                {Name:"BestCompression",id:10},
                {Name:"BestSpeed",id:11},
                {Name:"Default",id:12},
                {Name:"None",id:13}
            ],
                     displayDataField: 'Name'
                      , valueDataField: 'Name',
                    orderby: 'Name'
                }
        
            });
            
            $drpCompression.asDropdown('selectValue', "Default");
       
        
       
        
         $drpReplace.asDropdown({
                source: {
                    localData:[
                {Name:"OverwriteSilently",id:1},
                {Name:"DoNotOverwrite",id:2},
                {Name:"Throw",id:3}
            ],
                     displayDataField: 'Name'
                      , valueDataField: 'Name',
                    orderby: 'Name'
                }
        
            });
            
            $drpReplace.asDropdown('selectValue', "OverwriteSilently");
        
     
         
         $drpEncryption.asDropdown({
                source: {
                    localData:[
                        {Name:"PkzipWeak",id:1},
                        {Name:"WinZipAes128",id:2},
                        {Name:"WinZipAes256",id:3},
                         {Name:"None",id:4}
                    ],
                     displayDataField: 'Name'
                      , valueDataField: 'Name',
                    orderby: 'Name'
                }
        
            });
            
            $drpEncryption.asDropdown('selectValue', "PkzipWeak");
            
          $(asPageEvent).on("folderZipSelected",function(event,selectedZipFolder,selectedId){
                $txtNameZipDir.val(selectedZipFolder)
            })
            $(asPageEvent).on("folderSelected",function(event,selectedFolder,selectedId){
                selectedPaths = selectedFolder
                
                
                 var files=[]
            var folders=[]
            $.each(selectedItems.items,function(i,v){
                if(v)
                folders.push(i)
                else
                files.push(i)
            })
            if(actionMode==="move")
                move(files,folders);
                else if(actionMode==="copy")
                copy(files,folders,false);
                else if(actionMode==="copyByReplace")
                copy(files,folders,true);
            })
            $(asPageEvent).on("zipSelected",function(event,selectedZip,selectedId){
                $txtNameZip.val(selectedZip)
            })
              $(asPageEvent).on($.asEvent.page.ready, function (event) {
           
                $grvFile.asBootGrid({
                    caseSensitive:false,
    rowCount:[10,25,50,100],
    source:{
        url:''
    },
    requestHandler:function(request){
         if ($drpPath.asDropdown('selected')) {
        
             selectedItems.items={};
             selectedItems.path="";
             var pageUrl=""
        if(!changeSubFolder){
            rootUrl = selectedPath;
            pageUrl = rootUrl.replace(/\//g, $.asUrlDelimeter);
        }
        
        else{
           
            pageUrl = subUrl.replace(/\//g, $.asUrlDelimeter)
        }
        selectedItems.path=pageUrl;
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
              ,{ name: '@createThumbnail', value: $chkTumbnail.is(':checked')}]);
               selectedItems.items={};
              $grvFile.asBootGrid("deselect");
             return request
         }
         return null;
    },
    formatters: {
        Name: function (column, row)
        {
            /* "this" is mapped to the grid instance */
            if(row.IsFolder)
            return "<span class='folder-link' data-row='" + JSON.stringify(row) + "' style='font-weight: bold;cursor: pointer;'>" 
             + "<i class='glyphicon glyphicon-folder-open'></i> &nbsp;"+ row.Name  + "</span>" ;
            else
            return "<span>" + "<i class='glyphicon glyphicon-file'></i> &nbsp;" + row.Name  + "</span>";
        }
    },
        selection: true,
        rowSelect:true,
        multiSelect:true
}).on("selected.rs.jquery.asBootGrid", function(e, rows)
{
    if(isFolderClick===false)
    calculateSelectedFileAndFolder(e,rows)
    else
    isFolderClick=false;
    
}).on("deselected.rs.jquery.asBootGrid", function(e, rows)
{
       if(isFolderClick===false)
    calculateSelectedFileAndFolder(e,rows)
    else
    isFolderClick=false;
}).on("loaded.rs.jquery.asBootGrid", function(e,columns, row)
{
    totalRowCount = $grvFile.asBootGrid("getTotalRowCount");
   $(".folder-link").click(function(){
             isFolderClick=true;
       var row = $(this).data("row")
     
        if(row.IsFolder){
      var index= $.inArray( row.Name, paths);

          
      if(index === -1){
           
          paths[pathIndex]=row.Name
      $divPath.empty()
            subUrl = rootUrl;
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
            
        });
        
            $drpPath.on("change", function (event, item) {
                  for(var i=0;i<=paths.length - 1;i++){
                      paths[i]=null
              }
              selectedPath = item.value;
              $divPath.empty();
                changeSubFolder=false;
            $grvFile.asBootGrid("reload");
        });
     
     $btnSelectZip.click(function () {
          if ($drpPath.asDropdown('selected')) {
            $winFileSelector.asModal('load',  $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.fileSelector)},{name:"@isModal",value:true}])
            ,{path:$drpPath.asDropdown('selected').value,parent:asPageEvent,event:"zipSelected"})
          }else{
                 $.asShowMessage({template:"error", message: "No Path Selected"});
          }
          

    });
           $(asPageEvent).on("fileAdded",function(event,result){
           $grvFile.asBootGrid("append",[{Id:++totalRowCount,Name:result.name,IsFolder:false,Size:0,ModifieLocalDateTime:result.modifieLocalDateTime}])
         });
         
        //   $btnExecFileAction.click(function () {
        //  if ($drpPath.asDropdown('selected')) {
        //     $winFileAction.asAjax({
        //     url: $.asUrls.fms_save,
        //     data: JSON.stringify({
        //         Path: selectedItems.path.replace(new RegExp($.asUrlDelimeter, "gi"),"/").replace(new RegExp("//", "gi"),"/"),
        //         Name:$txtFileName.val(),
        //         Content:$txtFileContent.val()
        //     }),
        //     success: function (result) {
        //       $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
        //       if(fileActionMode === "add"){
        //           $grvFile.asBootGrid("append",[{Id:++totalRowCount,Name:$txtFileName.val(),IsFolder:false,Size:0,ModifieLocalDateTime:result}])
        //       }
        //     }
        // },{$form: $frmFileAction,overlayClass: "as-overlay-absolute"});
        //   }else{
        //          $.asShowMessage({template:"error", message: "هیچ مسیری انتخاب نشده است"});
        //   }
        // });
        
             
    $btnUploadFromDisk.click(function(){
         if ($drpPath.asDropdown('selected')) {
                $winUploadManager.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.uploadManager)},{name:"@isModal",value:true}])
                ,{path:selectedItems.path.replace(new RegExp($.asUrlDelimeter, "gi"), "/")});

          }else 
            $.asShowMessage({template:"error", message: "No Path Selected  "});
     });
     
    $btnAddOrUpdateFile.click(function(){
                  var files=[]
            var folders=[]
            $.each(selectedItems.items,function(i,v){
                if(v)
                folders.push(i)
                else
                files.push(i)
            });
               
      
      
             if(files.length === 0 && $drpPath.asDropdown('selected')){
                 fileActionMode= "add";
                $winFileAddOrUpdate.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.fileAddOrUpdate)},{name:"@isModal",value:true}])
                ,{fileActionMode:fileActionMode,parent:asPageEvent,event:"fileAdded",path:selectedItems.path});
             }
        else if(files.length === 1){
       
        if ($drpPath.asDropdown('selected')) {
            fileActionMode= "update";
            
             $winFileAddOrUpdate.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.fileAddOrUpdate)},{name:"@isModal",value:true}])
                ,{files:files,fileActionMode:fileActionMode,path:selectedItems.path});
            

          }else{
                 $.asShowMessage({template:"error", message: "No Path Selected"});
          }
        } else{
              $.asShowMessage({template:"error", message: "One File Must be Selected For Edit"});
          }
          
     });
    $btnSelectZipDir.click(function(){
                  if ($drpPath.asDropdown('selected')) {
            $winFolderSelector.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.directorySelector)},{name:"@isModal",value:true}])
            ,{path:$drpPath.asDropdown('selected').value,parent:asPageEvent,event:"folderZipSelected"})
          }else
           $.asShowMessage({template:"error", message: "No Path Selected"});
    })
    
        $btnFolderSelector.click(function () {

          

    });
    $btnMove.click(function () {
           btnClick("move")
        });
          $btnCopy.click(function () {
           btnClick("copy")
        });
          $btnCopyByReplace.click(function () {
           btnClick("copyByReplace")
        });
        $btnCreateDir.click(function(){
            actionMode="createDir";
            $winAction.find('.modal-title').text("Create Directory");
         $txtName.val("")
             $winAction.asModal("show");
        });
        $btnRename.click(function(){
            actionMode="rename";
            selectedForRename=""
         var files=[]
            var folders=[]
            $.each(selectedItems.items,function(i,v){
                if(v)
                folders.push(i)
                else
                files.push(i)
            });
            
            if(files.length === 1 && folders.length == 0){
                selectedForRename = files[0];
                isRenameDirectory=false;
            }else if(folders.length === 1 && files.length == 0){
                selectedForRename = folders[0]
                isRenameDirectory=true;
            }
          else{
               $.asShowMessage({template:"error", message: "To rename, just select a file or a directory"});
          }
          
          if(selectedForRename !== ""){
               $txtName.val(selectedForRename)
               $winAction.find('.modal-title').text("Rename");
             $winAction.asModal("show");
          }
            
        })
        $btnCancelAction.click(function () {
       $winAction.asModal('hide');
        });
  
        $btnZipAddOrUpdate.click(function () {
            zipActionMode="zip"
            as(".unzip").hide()
            as(".zip").show()
            $lblNameZipDir.html("The path to create a new zip")
             $txtNameZip.val("");
                if($chkNew.is(':checked')){
            $divZipDir.show()
            $btnSelectZip.hide()
            $divReplace.show()
                }else{
            $divZipDir.hide()
            $btnSelectZip.show()
            $divReplace.hide()
                }
            $winZipOption.asModal("show");
        });
        $btnUnZip.click(function () {
            zipActionMode="unzip"
             as(".zip").hide()
            as(".unzip").show()
            $divZipDir.show()
            $btnSelectZip.show()
                $txtPassword.val();
                $txtNameZip.val("");
                  $lblNameZipDir.html("UnZip Path");
   
                $winZipOption.asModal("show");
            
        });
        $btnDell.click(function () {
                      
    var files=[]
    var folders=[]
    $.each(selectedItems.items,function(i,v){
                if(v)
                folders.push(i)
                else
                files.push(i)
    })
            if(files.length > 0 || folders.length > 0){
                 $btnDell.button('loading')  
            $winAction.asAjax({
            url:$.asUrls.fms_delete,
            data: JSON.stringify({
                Path: selectedItems.path.replace(new RegExp($.asUrlDelimeter, "gi"),"/").replace(new RegExp("//", "gi"),"/"),
                Files:files,
                Folders:folders
            }),
            success: function (result) {
                $btnDell.button('reset')
                $grvFile.asBootGrid("remove");
             onSuccess()
            },error:function(){
                $btnDell.button('reset')
            }
        },{$form: $frmAction,overlayClass: "as-overlay-absolute"})
        
            }else{
                       $.asShowMessage({template:"error", message: "The file or directory is not selected for deletion"});
            }
        
        });
   
        $btnExecAction.click(function () {
            if(actionMode==="rename"){
            $winAction.asAjax({
            url:$.asUrls.fms_rename,
            data: JSON.stringify({
                OldPath: selectedItems.path.replace(new RegExp($.asUrlDelimeter, "gi"),"/").replace(new RegExp("//", "gi"),"/") + "/" + selectedForRename,
                NewPath:selectedItems.path.replace(new RegExp($.asUrlDelimeter, "gi"),"/").replace(new RegExp("//", "gi"),"/") + "/" + $txtName.val(),
                IsDirectory:isRenameDirectory
            }),
            success: function (result) {
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
            }
        },{$form: $frmAction,overlayClass: "as-overlay-absolute"})
            }else{
            $winAction.asAjax({
            url:$.asUrls.fms_createDir,
            data: JSON.stringify({
                path:selectedItems.path.replace(new RegExp($.asUrlDelimeter, "gi"),"/").replace(new RegExp("//", "gi"),"/") + "/" + $txtName.val()
            }),
            success: function (result) {
                $grvFile.asBootGrid("append",[{Id:++totalRowCount,Name:$txtName.val(),IsFolder:true,Size:0,ModifieLocalDateTime:result}])
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
            }
        },{$form: $frmAction,overlayClass: "as-overlay-absolute"})
            }

        
        });
        $btnZipView.click(function () {
            var zip=[];
        $.each(selectedItems.items,function(i,v){
                if(!v)
                zip.push(i)
            })
            if(zip.length === 1)
          $winZipViewer.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter("en/admin/fms/zip-view/")},{name:"@isModal",value:true}])
          ,{zipFullName:selectedItems.path + "@" + zip[0]})
          else{
               $.asShowMessage({template:"error", message: "Just select a zip file to view"});
          }
        });
         $chkNew.change(function () {
        if(this.checked === true){
            $divZipDir.show()
            $btnSelectZip.hide()
            $divReplace.show()
        }else{
            $divZipDir.hide()
            $btnSelectZip.show()
            $divReplace.hide()
        }
         });
        
        as("#btnCancelZip").click(function () {
            
       $winZipOption.asModal('hide');
       $chkNew.prop('checked', false)
        });
        
     
        
        $btnExecute.click(function () {
           
     if(zipActionMode==="zip")
        {
                    
             var files=[]
            var folders=[]
            $.each(selectedItems.items,function(i,v){
                if(v)
                folders.push(i)
                else
                files.push(i)
            })
            
         $winZipOption.asAjax({
            url: $.asUrls.fms_zip_addOrUpdate,
            data: JSON.stringify({
                SourcePath: selectedItems.path.replace(new RegExp($.asUrlDelimeter, "gi"),"/").replace(new RegExp("//", "gi"),"/"),
                DestinationPath:$chkNew.is(':checked') ? $txtNameZipDir.val() + "/" +  $txtNameZip.val().replace(new RegExp($.asUrlDelimeter, "gi"),"/").replace(new RegExp("//", "gi"),"/") : $txtNameZip.val().replace(new RegExp($.asUrlDelimeter, "gi"),"/").replace(new RegExp("//", "gi"),"/"),
                Files:files,
                Folders:folders,
                OverWrite:$chkNew.is(':checked') ?$chkReplaceZip.is(':checked'):true,
                Encryption:encryption,
                Password:$txtPassword.val(),
                CompressionLevel:compressionLevel,
                IsNew:$chkNew.is(':checked')
            }),
            success: function (result) {
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
            }
        },{$form: $frm,overlayClass: "as-overlay-absolute"})
                }
                else if(zipActionMode==="unzip"){
         
                    $winZipOption.asAjax({
            url: $.asUrls.fms_zip_extract,
            data: JSON.stringify({
                SourcePath: $txtNameZip.val().replace(new RegExp($.asUrlDelimeter, "gi"),"/").replace(new RegExp("//", "gi"),"/"),
                DestinationPath:$txtNameZipDir.val(),
                Password:$txtPassword.val(),
                  OverWrite:replaceAction
            }),
            success: function (result) {
              $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
            }
        },{$form: $frm,overlayClass: "as-overlay-absolute"})
                }
        });
    
         $drpCompression.on("change", function (event, item) {
            if(item){
                  compressionLevel= item.value;
            }
        });
         
          $drpReplace.on("change", function (event, item) {
            if(item){
                  replaceAction= item.value;
            }
        }); 
        
        $drpEncryption.on("change", function (event, item) {
            if(item){
                  encryption= item.value;
            }
        });   
        
    $btnUploadFromUrl.click(function(){
                 

       
        if ($drpPath.asDropdown('selected')) {
                $winRemoteDownloadManager.asModal('load', $.asInitService($.asFormUrl,[{name:"@url",value:$.asUrlAsParameter($.asModalManager.urls.remoteDownloadManager)},{name:"@isModal",value:true}])
                ,{urlTypeId:1047,path:selectedItems.path.replace(new RegExp($.asUrlDelimeter, "gi"), "/")});
          }else 
            $.asShowMessage({template:"error", message: "No Path selected"});
      
      
         
          
     });
     
        asOnPageDispose = function(){
          $grvFile.asBootGrid("destroy");
           validate.destroy();
           validateAction.destroy();

        }
     }
     bindEvent();