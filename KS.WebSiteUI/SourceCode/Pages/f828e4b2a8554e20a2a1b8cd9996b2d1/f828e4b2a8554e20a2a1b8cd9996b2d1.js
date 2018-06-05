 var 
$drpLanguge= as("#home_drp_languge");

$drpLanguge.asFlexSelect({
    source: {
        displayDataField:'country',
        valueDataField:'language',
        urlDataField:'flagUrl',
        idDataField:'culture',
        url: $.asUrls.cms_languageAndCulture_public_getAll
    }
});


as("#home_drp_languge").on("selectedIndexChanged", function (event,id, value, text, url) {
   if(url)
      as("#home_drp_languge").asFlexSelect('setItem','<img src="' + url + '">&nbsp;<span class="caret"></span>')
    
});
            