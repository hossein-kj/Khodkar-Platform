$(function(){
	
	//HTML to JSON
	$('#btn-render-json').click(function() {
		
		//Set html output
		$('#html-output').html( $('#html-input').val() );
		
		//Process to JSON and format it for consumption
		$('#html-json').html( FormatJSON(toTransform($('#html-output').children())) );
	});

});

//Convert obj or array to transform
function toTransform(obj) {
	
	var json;
	
	if( obj.length > 1 )
	{
		json = [];

		for(var i = 0; i < obj.length; i++)
			json[json.length++] = ObjToTransform(obj[i]);
	} else
		json = ObjToTransform(obj);

	return(json);
}

//Convert obj to transform
function ObjToTransform(obj)
{
	//Get the DOM element
	var el = $(obj).get(0);

	//Add the tag element
	var json = {'tag':el.nodeName.toLowerCase()};

	for (var attr, i=0, attrs=el.attributes, l=attrs.length; i<l; i++){
		attr = attrs[i];
		json[attr.nodeName] = attr.value;
	}
	
	var children = $(obj).children();

	if( children.length > 0 ) json['children'] = [];
	else json['html'] = $(obj).text();

	//Add the children
	for(var c = 0; c < children.length; c++)
		json['children'][json['children'].length++] = toTransform(children[c]);

	return(json);
}

//Format JSON (with indents)
function FormatJSON(oData, sIndent) {
	if (arguments.length < 2) {
		var sIndent = "";
	}
	var sIndentStyle = "  ";
	var sDataType = RealTypeOf(oData);

	// open object
	if (sDataType == "array") {
		if (oData.length == 0) {
			return "[]";
		}
		var sHTML = "[";
	} else {
		var iCount = 0;
		$.each(oData, function() {
			iCount++;
			return;
		});
		if (iCount == 0) { // object is empty
			return "{}";
		}
		var sHTML = "{";
	}

	// loop through items
	var iCount = 0;
	$.each(oData, function(sKey, vValue) {
		if (iCount > 0) {
			sHTML += ",";
		}
		if (sDataType == "array") {
			sHTML += ("\n" + sIndent + sIndentStyle);
		} else {
			sHTML += ("\"" + sKey + "\"" + ":");
		}

		// display relevant data type
		switch (RealTypeOf(vValue)) {
			case "array":
			case "object":
				sHTML += FormatJSON(vValue, (sIndent + sIndentStyle));
				break;
			case "boolean":
			case "number":
				sHTML += vValue.toString();
				break;
			case "null":
				sHTML += "null";
				break;
			case "string":
				sHTML += ("\"" + vValue + "\"");
				break;
			default:
				sHTML += ("TYPEOF: " + typeof(vValue));
		}

		// loop
		iCount++;
	});

	// close object
	if (sDataType == "array") {
		sHTML += ("\n" + sIndent + "]");
	} else {
		sHTML += ("}");
	}

	// return
	return sHTML;
}

//Get the type of the obj (can replace by jquery type)
function RealTypeOf(v) {
  if (typeof(v) == "object") {
	if (v === null) return "null";
	if (v.constructor == (new Array).constructor) return "array";
	if (v.constructor == (new Date).constructor) return "date";
	if (v.constructor == (new RegExp).constructor) return "regex";
	return "object";
  }
  return typeof(v);
}