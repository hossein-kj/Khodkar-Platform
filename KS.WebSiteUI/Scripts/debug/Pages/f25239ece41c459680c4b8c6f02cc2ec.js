 $('#if25239ece41c459680c4b8c6f02cc2ec').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('Source Manager');var asPageEvent = '#if25239ece41c459680c4b8c6f02cc2ec'; var asPageId = '.if25239ece41c459680c4b8c6f02cc2ec.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({}, $.asUrls); var
    $win=$(asPageId),
     $grvChanges=as("#grvChanges"),
     $btnSelect=as("#btnSelect"),
     $fromDateInput=as("#fromDateInput"),
     $toDateInput=as("#toDateInput"),
     $fromTimeInput=as("#fromTimeInput"),
     $toTimeInput=as("#toTimeInput"),
     $txtComment=as("#txtComment"),
     $txtUser=as("#txtUser"),
     $btnSearch=as("#btnSearch"),
     $btnCompare=as("#btnCompare"),
     selectedChangeSetId=0,
     getUrl="",
     totalRowCount=0,
     toDateTime="!",
     fromDateTime="!";
     
    $fromDateInput.asDateTimeInput({ type: 'calendar', calendar: { params: { height: '30px', width: '190px' }, name: 'gregorian',lang: 'en' }, theme: 'public',layout:'ltr' });
    $toDateInput.asDateTimeInput({ type: 'calendar', calendar: { params: { height: '30px', width: '190px' }, name: 'gregorian',lang: 'en' }, theme: 'public',layout:'ltr' });
    
     $fromTimeInput.asDateTimeInput({ type: 'time', theme: 'public' });
    $toTimeInput.asDateTimeInput({ type: 'time', theme: 'public' });
    
      if(asPageParams){
          getUrl=asPageParams.getUrl;
           }
     var onSuccess = function(){
           $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
              selectedItems.items={};
     }

  $grvChanges.asBootGrid({
     
    rowCount:[10,25,50,100,-1],
    source:{
        url:''
    },
    requestHandler:function(request){

        if(getUrl !== ""){
                    var orderbyValue = "LocalDateTime desc"
                var skip = 0
                if(request.current > 1)
                skip=(request.current - 1) * request.rowCount
                $.each(request.sort, function(key, value) {
                         orderbyValue = key + " " + value
                        });
              
                request.url = $.asInitService(getUrl, [
                    { name: '@orderby', value: orderbyValue }
                    ,{ name: '@skip', value: skip }
                     ,{ name: '@take', value: request.rowCount}
                     ,{ name: '@comment', value: "!"}
                     ,{ name: '@user', value: "!"}
                     ,{ name: '@fromDateTime', value: fromDateTime === "!" ? "!":fromDateTime.replace(":","_")}
                     ,{ name: '@toDateTime', value: toDateTime === "!" ? "!" :toDateTime.replace(":","_")}]);
                
                      selectedChangeSetId=0;
                      $grvChanges.asBootGrid("deselect");
                     return request
        }

    },
        selection: true,
        rowSelect:true,
        multiSelect:false
}).on("selected.rs.jquery.asBootGrid", function(e, rows)
{
    selectedChangeSetId =rows[0].Id;
    
}).on("deselected.rs.jquery.asBootGrid", function(e, rows)
{
    selectedChangeSetId=0;
});

     var bindEvent = function(){
        $(asPageEvent).on($.asEvent.modal.reopen, function (event,params) {
            if(params.getUrl !== asPageParams.getUrl || params.parent !== asPageParams.parent || params.event !== asPageParams.event){
            
                asPageParams=params;
                getUrl=asPageParams.getUrl;

          
        
               

                    selectedChangeSetId=0;
                    $grvChanges.asBootGrid("reload");
            }
        });
        $btnSelect.click(function () {
            if(selectedChangeSetId !== 0 && asPageParams){
             $(asPageParams.parent).trigger(asPageParams.selectEvent, [selectedChangeSetId]);
              $win.asModal('hide');
            }else{
                $.asShowMessage({ template: "error", message:  " No Change Selected"})
            }
        });
      as("#btnRemoveFilte").click(function () {
          $txtUser.val("");
          $txtComment.val("");
          toDateTime="!";
          $toTimeInput.asDateTimeInput("setTime","");
          fromDateTime="!";
           $fromTimeInput.asDateTimeInput("setTime","");
           toDateInput="!";
           $toDateInput.asDateTimeInput("setDate","");
           fromTimeInput="!"
           $fromDateInput.asDateTimeInput("setDate","");
    });
    $btnCompare.click(function () {
       if(selectedChangeSetId !== 0 && asPageParams){
             $(asPageParams.parent).trigger(asPageParams.compareEvent, [selectedChangeSetId]);
              $win.asModal('hide');
            }else{
                $.asShowMessage({ template: "error", message:  "No Change Selected "})
            }
    });
    $btnSearch.click(function () {
      
        if($toDateInput.asDateTimeInput('getDate').length > 0){
            toDateTime=$toDateInput.asDateTimeInput('getDate');
            if($toTimeInput.asDateTimeInput('getTime') !== "")
             toDateTime += " " + $toTimeInput.asDateTimeInput('getTime') + "_00";
            else
                toDateTime += " " + $toTimeInput.asDateTimeInput('getTime') + "23_59_59";
        }
        
    
        if($fromDateInput.asDateTimeInput('getDate').length > 0){
            fromDateTime=$fromDateInput.asDateTimeInput('getDate');
            if($fromTimeInput.asDateTimeInput('getTime') !== "")
             fromDateTime += " " + $fromTimeInput.asDateTimeInput('getTime') + "_00";
            else
                fromDateTime += " " + $fromTimeInput.asDateTimeInput('getTime') + "00_00_00";
        }

        $grvChanges.asBootGrid("reload");
    });

        


         
  

        asOnPageDispose = function(){
          $grvChanges.asBootGrid("destroy");
        }
     }
     bindEvent();  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });