var
    $win=$(asPageId),
    $frm = as("#frmDebug"),
    $winAddOrEdit=as("#winAddOrEdit"),
     $grvDebugs=as("#grvDebugs"),
     $fromDateInput=as("#fromDateInput"),
     $toDateInput=as("#toDateInput"),
     $fromTimeInput=as("#fromTimeInput"),
     $toTimeInput=as("#toTimeInput"),
     $txtDecimal=as("#txtDecimal"),
     $txtDecimalValueModal=as("#txtDecimalValueModal"),
     $txtData=as("#txtData"),
     $txtDataModal=as("#txtDataModal"),
     $txtIntegerValue=as("#txtIntegerValue"),
     $txtIntegerValueModal=as("#txtIntegerValueModal"),
     $btnSearch=as("#btnSearch"),
     $btnEdit=as("#btnEdit"),
     $btnNew=as("#btnNew"),
     $btnDell=as("#btnDell"),
     $txtDateTime=as("#txtDateTime"),
     $chkIsOk=as("#chkIsOk"),
     $btnExecute=as("#btnExecute"),
     selectedItems={
         items:{}
     },
     selectedDebugId=0,
     isNew=false,
     codeId=0,
     totalRowCount=0,
     toDateTime="!",
     fromDateTime="!";
     
     $winAddOrEdit.asModal(
    {backdrop:'static', keyboard: false}
    );
    $fromDateInput.asDateTimeInput({ type: 'calendar', calendar: { params: { height: '30px', width: '190px' } }, theme: 'public' });
    $toDateInput.asDateTimeInput({ type: 'calendar', calendar: { params: { height: '30px', width: '190px' } }, theme: 'public' });
    
     $fromTimeInput.asDateTimeInput({ type: 'time', theme: 'public' });
    $toTimeInput.asDateTimeInput({ type: 'time', theme: 'public' });
    
     var calculateSelectedDebug = function(event, rows){

        if(event.type==="selected"){
            selectedItems.items[rows[0].Id]={
            Id:rows[0].Id
        ,Data:rows[0].Data
        ,IsOk:rows[0].IsOk
        ,IntegerValue:rows[0].IntegerValue
        ,DecimalValue:rows[0].DecimalValue
        ,DateTime:rows[0].DateTime
            }
        }else{
            delete selectedItems.items[rows[0].Id]
        }
    }
    
    
      if(asPageParams){
          codeId=asPageParams.codeId || "";
           }
     var onSuccess = function(){
           $.asShowMessage({ message: $.asRes[$.asLang].successOpration });
              selectedItems.items={};
              $grvDebugs.asBootGrid("deselect");
     }

  $grvDebugs.asBootGrid({
     
    rowCount:[10,25,50,100,-1],
    source:{
        url:''
    },
    requestHandler:function(request){

        if(codeId !== 0 && request){
            
                    var orderbyValue = "CreateDateTime desc"
                var skip = 0
                if(request.current > 1)
                skip=(request.current - 1) * request.rowCount
                $.each(request.sort, function(key, value) {
                         orderbyValue = key + " " + value
                        });
              
                request.url = $.asInitService($.asUrls.develop_code_os_dotNet_GetDebugInfos, [
                    { name: '@orderby', value: orderbyValue }
                    ,{ name: '@skip', value: skip }
                     ,{ name: '@take', value: request.rowCount}
                     ,{ name: '@data', value: $txtData.val() === "" ? "!": $txtData.val()}
                     ,{ name: '@integerValue', value: $txtIntegerValue.val() === "" ? null: $txtIntegerValue.val()}
                     ,{ name: '@decimalValue', value: $txtDecimal.val() === "" ? null: $txtDecimal.val()}
                     ,{ name: '@codeId', value: codeId}
                     ,{ name: '@fromDateTime', value: fromDateTime === "!" ? "!":fromDateTime.replace(":","_")}
                     ,{ name: '@toDateTime', value: toDateTime === "!" ? "!" :toDateTime.replace(":","_")}]);
                
                      selectedItems.items={};
                      $grvDebugs.asBootGrid("deselect");
                   
                     return request
        }

    },
        selection: true,
        rowSelect:true,
        multiSelect:true
}).on("selected.rs.jquery.asBootGrid", function(e, rows)
{
   calculateSelectedDebug(e,rows)
    
}).on("deselected.rs.jquery.asBootGrid", function(e, rows)
{
   calculateSelectedDebug(e,rows)
});

     var bindEvent = function(){
        $(asPageEvent).on($.asEvent.modal.reopen, function (event,params) {
            if(params.codeId !== asPageParams.codeId){
            
                asPageParams=params;
                codeId=asPageParams.codeId;

                selectedItems.items={};
                $grvDebugs.asBootGrid("reload");
            }
        });

      as("#btnRemoveFilte").click(function () {
          $txtData.val("");
          $txtDecimal.val("");
          $txtIntegerValue.val("");
          toDateTime="!";
          $toTimeInput.asDateTimeInput("setTime","");
          fromDateTime="!";
           $fromTimeInput.asDateTimeInput("setTime","");
           toDateInput="!";
           $toDateInput.asDateTimeInput("setDate","");
           fromTimeInput="!"
           $fromDateInput.asDateTimeInput("setDate","");
    });
    var clearModal = function(){
                   $txtIntegerValueModal.val("");
                  $txtDecimalValueModal.val("");
                  $txtDataModal.val("");
                  $txtDateTime.val("");
                  $chkIsOk.prop('checked', false)
             }
    $btnNew.click(function () {
        clearModal();
        isNew=true;
        $winAddOrEdit.asModal("show");
    });
    
    $btnDell.click(function () {
        
            var selecteds=[]
              $.each(selectedItems.items,function(i,v){
                selecteds.push(v.Id)
            });
            
            if(selecteds.length === 0){
                $.asShowMessage({template:"error", message: "برای حذف حداقل یک خطایاب باید انتخاب شود"});
                return;
            }
            
            
             $win.asAjax({
            url: $.asUrls.develop_code_os_dotNet_deleteDebugInfo,
            data: JSON.stringify({
                DebugInfoIds:selecteds,
                CodeId:codeId
            }),
            success: function (result) {
                     
             $grvDebugs.asBootGrid("remove");
                  
              onSuccess();
            }
        },{$form: $frm,overlayClass: "as-overlay-absolute"});
    });
     $btnEdit.click(function () {
        clearModal();
        isNew=false;
        
         var selectedDebug;
         var selectedCounts = 0;
        $.each(selectedItems.items,function(i,v){
              selectedCounts++;
             selectedDebug= v;
            });
            
            if(selectedCounts !== 1){
                $.asShowMessage({template:"error", message: "برای ویرایش یک خطایاب باید انتخاب شده باشد"});
                return;
            }
        
                  $txtIntegerValueModal.val(selectedDebug.IntegerValue === null ? "":selectedDebug.IntegerValue);
                  $txtDecimalValueModal.val(selectedDebug.DecimalValue === null ? "":selectedDebug.DecimalValue);
                  $txtDataModal.val(selectedDebug.Data);
                  $txtDateTime.val(selectedDebug.DateTime === null ? "":selectedDebug.DateTime);
                  $chkIsOk.prop('checked', selectedDebug.IsOk)
                 selectedDebugId=selectedDebug.Id;
        
        $winAddOrEdit.asModal("show");
    });
    
    $btnExecute.click(function () {
     $winAddOrEdit.asAjax({
            url: $.asUrls.develop_code_os_dotNet_addOrUpdateDebugInfo,
            data: JSON.stringify({
                Id:selectedDebugId,
                CodeId:codeId,
                Data:$txtDataModal.val(),
                IntegerValue:$txtIntegerValueModal.val() === "" ? null:$txtIntegerValueModal.val(),
                DecimalValue:$txtDecimalValueModal.val() === "" ? null:$txtDecimalValueModal.val(),
                DateTime:$txtDateTime.val() === "" ? null:$txtDateTime.val(),
                IsOk:$chkIsOk.is(':checked')
            }),
            success: function (result) {
                if( isNew===true){
                   
                    isNew=false;
                }else{
                     
                      $grvDebugs.asBootGrid("remove");
                }
                 $grvDebugs.asBootGrid("append",[result]);
                  
              onSuccess();
            }
        },{$form: $frm,overlayClass: "as-overlay-absolute"});
       });
    as("#btnCancel").click(function () {
            
       $winAddOrEdit.asModal('hide');
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

        $grvDebugs.asBootGrid("reload");
    });

        


         
  

        asOnPageDispose = function(){
          $grvDebugs.asBootGrid("destroy");
        }
     }
     bindEvent();