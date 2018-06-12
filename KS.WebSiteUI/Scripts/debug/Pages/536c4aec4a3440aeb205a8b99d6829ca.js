 $('#i536c4aec4a3440aeb205a8b99d6829ca').on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) { console.log('Upload Manager');var asPageEvent = '#i536c4aec4a3440aeb205a8b99d6829ca'; var asPageId = '.i536c4aec4a3440aeb205a8b99d6829ca.' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()});  $.asUrls = $.extend({fms_upload:"/fms/upload"}, $.asUrls); var
     $win=$(asPageId),
    $frm=as("#frmUploadManager");
    
        // var CheckFileState = function (file) {
        //     as("#"+file.uniqueIdentifier).asAjax({
        //         url: $.asInitService( $.asUrls.fms_checkFileExistence, []),
        //             data: JSON.stringify({
        //                 FileName: file.name,
        //                 Path:"folder" //asPageParams.path
        //             }),
        //         success: function (result) {
        //              if(result){
        //              uploadQue.push(file.name);
        //             flow.addFile(file);
        //             as("#" + file.uniqueIdentifier + "State").html("آماده ارسال");
        //              }else{
        //                  as("#" + file.uniqueIdentifier + "State").html("فایلی با همین نام در سرور وجود دارد");
        //              }
        //         }
        //     },{overlayClass: "as-overlay-absolute-no-height"});
        // }
        
 



      
    




var setUpWin = function(){
    
            
           // Initialize the jQuery File Upload widget:
    $frm.fileupload({
        // Uncomment the following to send cross-domain cookies:
        //xhrFields: {withCredentials: true},
        formData: {path: asPageParams.path},
        url: $.asUrls.fms_upload,
        autoUpload:false,
        filesContainer: as('#tblUpload tbody'),
    uploadTemplateId: null,
    downloadTemplateId: null,
    uploadTemplate: function (o) {
        var rows = $();
        $.each(o.files, function (index, file) {
            var row = $('<tr class="template-upload">' +
                '<td><span class="preview"></span></td>' +
                '<td><p class="name"></p>' +
                '<div class="error"></div>' +
                '</td>' +
                '<td><p class="size"></p>' +
                ' <div class="progress progress-striped active" role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="0"><div class="progress-bar progress-bar-success" style="width:0%;"></div></div>' +
                '</td>' +
                '<td>' +
                (!index && !o.options.autoUpload ?
                    ' <button class="btn btn-primary start" disabled style="margin-left:7px;"><i class="glyphicon glyphicon-upload"></i><span>Start</span> </button>' : '') +
                (!index ? '<button class="btn btn-warning cancel"><i class="glyphicon glyphicon-ban-circle"></i> <span>Cancel</span> </button>' : '') +
                '</td>' +
                '</tr>');
            row.find('.name').text(file.name);
            row.find('.size').text(o.formatFileSize(file.size));
            if (file.error) {
                row.find('.error').text(file.error);
            }
            rows = rows.add(row);
        });
       
        return rows;
    },
    downloadTemplate: function (o) {
        var rows = $();
        $.each(o.files, function (index, file) {
            var row = $('<tr class="template-download">' +
                '<td><span class="preview"></span></td>' +
                '<td><p class="name"></p>' +
                (file.error ? '<div class="error"></div>' : '') +
                 (file.errorCode ? '<div class="errorCode" style="color: red;">error</div>' : '') +
                '</td>' +
                '<td><span class="size"></span></td>' +
                '<td></td>' +
                '</tr>');
            row.find('.size').text(o.formatFileSize(file.size));
            if (file.error) {
                row.find('.name').text(file.name);
                row.find('.error').text(file.error);
            } else {
                if (file.errorCode) {
                    row.find('.name').text(file.name);
                    row.find('.errorCode').text($.asRes[$.asLang][file.errorCode]);
                } else{
                        row.find('.name').append($('<a></a>').text(file.name));
                        // if (file.thumbnailUrl) {
                        //     row.find('.preview').append(
                        //         $('<a></a>').append(
                        //             $('<img>').prop('src', file.thumbnailUrl)
                        //         )
                        //     );
                        // }
                        // row.find('a')
                        //     .attr('data-gallery', '')
                        //     .prop('href', file.url);
                        // row.find('button.delete')
                        //     .attr('data-type', file.delete_type)
                        //     .attr('data-url', file.delete_url);
                }

            }
            rows = rows.add(row);
        });
        return rows;
    }
});

    // // Enable iframe cross-domain access via redirect option:
    // $('#fileupload').fileupload(
    //     'option',
    //     'redirect',
    //     window.location.href.replace(
    //         /\/[^\/]*$/,
    //         '/cors/result.html?%s'
    //     )
    // );
      // Load existing files:
    //   $frm.addClass('fileupload-processing');
    //     $.ajax({
    //         // Uncomment the following to send cross-domain cookies:
    //         //xhrFields: {withCredentials: true},
    //         url: $frm.fileupload('option', 'url'),
    //         dataType: 'json',
    //         context: $frm[0]
    //     }).always(function () {
    //         $(this).removeClass('fileupload-processing');
    //     }).done(function (result) {
    //         $(this).fileupload('option', 'done')
    //             .call(this, $.Event('done'), { result: result });
    //     });
        
}

    
    var bindEvent = function(){
 
         $(asPageEvent).on($.asEvent.modal.reopen, function (event,params) {
             as('#tblUpload tbody').html("");
               if(params.path !== asPageParams.path){
                   asPageParams=params;
                    setUpWin();
               }
                
            });
            
         

        //  $btnCancel.on('click', function () {
        //         $win.asModal('hide');
        //     });
            // asOnPageDispose = function(){
            //       validateRunTest.destroy();
            // }
    
          $(asPageEvent).on($.asEvent.page.ready, function (event) {
             setUpWin();
    });

    }
    bindEvent();

      

  ; $(asPageId).append('<span id="asRegisterPage"></span>');as('#asRegisterPage').asRegisterPageEvent(); if (typeof (requestedUrl) != 'undefined')  
                {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });