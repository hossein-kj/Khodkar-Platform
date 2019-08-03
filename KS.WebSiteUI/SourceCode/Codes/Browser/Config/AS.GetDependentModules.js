(function () {
    "use strict";
    $.asGetDependentModules = function (source) {

        var dependencyModules = {};
        var modules = [];
        var addDependency = function (dependency) {
            $.each(dependency, function (i, v) {
                dependencyModules[v.url] = v;
            });
        }

        /*asDateTimeInput@Start*/ function asDateTimeInput() {  return [{ url: 'asDateTimeInput.css?minversion=21' },{ url:'asDateTimeInput.js?minversion=21' }].concat(asCalendar());  }  if (source.indexOf('asDateTimeInput') >= 0) { addDependency(asDateTimeInput()); } /*asDateTimeInput@End*/

        /*asCalendar@Start*/ function asCalendar() {  return [{ url: 'asCalendar.css?minversion=14' },{ url:'asCalendar.js?minversion=14' }].concat();  }  if (source.indexOf('asCalendar') >= 0) { addDependency(asCalendar()); } /*asCalendar@End*/

        

        /*asOffCanvas@Start*/ function asOffCanvas() {  return [{ url: 'asOffCanvas.css?minversion=25' },{ url:'asOffCanvas.js?minversion=25' }].concat();  }  if (source.indexOf('asOffCanvas') >= 0) { addDependency(asOffCanvas()); } /*asOffCanvas@End*/

        

        /*asBootGrid@Start*/ function asBootGrid() {  return [{ url: 'asBootGrid.css?minversion=19' },{ url:'asBootGrid.js?minversion=19' }].concat();  }  if (source.indexOf('asBootGrid') >= 0) { addDependency(asBootGrid()); } /*asBootGrid@End*/

        /*asPopovers@Start*/ function asPopovers() {  return [{ url:'asPopovers.js?minversion=4' }].concat();  }  if (source.indexOf('asPopovers') >= 0) { addDependency(asPopovers()); } /*asPopovers@End*/

        /*asDropdown@Start*/ function asDropdown() {  return [{ url: 'asDropdown.css?minversion=11' },{ url:'asDropdown.js?minversion=11' }].concat();  }  if (source.indexOf('asDropdown') >= 0) { addDependency(asDropdown()); } /*asDropdown@End*/

        /*asMegaMenu@Start*/ function asMegaMenu() {  return [{ url: 'asMegaMenu.css?minversion=10' },{ url:'asMegaMenu.js?minversion=10' }].concat();  }  if (source.indexOf('asMegaMenu') >= 0) { addDependency(asMegaMenu()); } /*asMegaMenu@End*/

        /*asFixedNav@Start*/ function asFixedNav() {  return [{ url: 'asFixedNav.css?minversion=5' },{ url:'asFixedNav.js?minversion=5' }].concat();  }  if (source.indexOf('asFixedNav') >= 0) { addDependency(asFixedNav()); } /*asFixedNav@End*/

        /*asValidate@Start*/ function asValidate() {  return [{ url: 'asValidate.css?minversion=5' },{ url:'asValidate.js?minversion=5' }].concat();  }  if (source.indexOf('asValidate') >= 0) { addDependency(asValidate()); } /*asValidate@End*/

        /*asEditor@Start*/ function asEditor() {  return [{ url:'asEditor.js?minversion=19' }].concat();  }  if (source.indexOf('asEditor') >= 0) { addDependency(asEditor()); } /*asEditor@End*/

        /*asCodeEditor@Start*/ function asCodeEditor() {  return [{ url:'asCodeEditor.js?minversion=8' }].concat();  }  if (source.indexOf('asCodeEditor') >= 0) { addDependency(asCodeEditor()); } /*asCodeEditor@End*/

        /*asFlexSelect@Start*/ function asFlexSelect() {  return [{ url: 'asFlexSelect.css?minversion=41' },{ url:'asFlexSelect.js?minversion=41' }].concat();  }  if (source.indexOf('asFlexSelect') >= 0) { addDependency(asFlexSelect()); } /*asFlexSelect@End*/

        /*asWindow@Start*/ function asWindow() {  return [{ url:'asWindow.js?minversion=5' }].concat();  }  if (source.indexOf('asWindow') >= 0) { addDependency(asWindow()); } /*asWindow@End*/

        /*asOdataQueryBuilder@Start*/
        function asOdataQueryBuilder() {
            return [{ url: 'asOdataQueryBuilder.js' }];
        }
        if (source.indexOf("asOdataQueryBuilder") >= 0) {
            addDependency(asOdataQueryBuilder());
        }
        /*asOdataQueryBuilder@End*/

        /*asModal@Start*/ function asModal() {  return [{ url: 'asModal.css?minversion=53' },{ url:'asModal.js?minversion=53' }].concat();  }  if (source.indexOf('asModal') >= 0) { addDependency(asModal()); } /*asModal@End*/ 
 
 
 
 
 
/*fileupload@Start*/ function fileupload() {  return [{ url: 'fileupload.css?minversion=18' },{ url:'fileupload.js?minversion=18' }].concat();  }  if (source.indexOf('fileupload') >= 0) { addDependency(fileupload()); } /*fileupload@End*/ 
/*asCaptcha@Start*/ function asCaptcha() {  return [{ url:'asCaptcha.js?minversion=9' }].concat();  }  if (source.indexOf('asCaptcha') >= 0) { addDependency(asCaptcha()); } /*asCaptcha@End*/ 
/*asSelect@Start*/ function asSelect() {  return [{ url: 'asSelect.css?minversion=6' },{ url:'asSelect.js?minversion=6' }].concat();  }  if (source.indexOf('asSelect') >= 0) { addDependency(asSelect()); } /*asSelect@End*/ 
/*sequence@Start*/ function sequence() {  return [{ url: 'asSlider.css?minversion=57' },{ url:'asSlider.js?minversion=57' }].concat();  }  if (source.indexOf('sequence') >= 0) { addDependency(sequence()); } /*sequence@End*/ 
/*@asNewModule@*/

        $.each(dependencyModules, function (i, v) {
            modules.push(v);
        });

        return JSON.stringify(modules);
    }

})();