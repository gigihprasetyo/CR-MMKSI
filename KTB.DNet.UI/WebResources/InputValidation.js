function omitSomeCharacter(controlName,toBeOmittedChars_)
{
	var key = document.getElementById(controlName).value;
	var newValue = "";
	var isOmitted = false;
	var toBeOmittedChars = toBeOmittedChars_ +"'";
	for (i=0;i<key.length;i++)	
	{
		isOmitted = false;
		for (j=0;j<toBeOmittedChars.length;j++)	
		{	
			if (key.charCodeAt(i) == toBeOmittedChars.charCodeAt(j))
			{
				isOmitted = true;
				break;
			}
		}	
		if (isOmitted == false)
			newValue = newValue + key.substring(i,i+1);
	}
	document.getElementById(controlName).value = newValue;
}

function omitSomeCharacterExcludeSingleQuote(controlName,toBeOmittedChars_)
{
	var key = document.getElementById(controlName).value;
	var newValue = "";
	var isOmitted = false;
	var toBeOmittedChars = toBeOmittedChars_;
	for (i=0;i<key.length;i++)	
	{
		isOmitted = false;
		for (j=0;j<toBeOmittedChars.length;j++)	
		{	
			if (key.charCodeAt(i) == toBeOmittedChars.charCodeAt(j))
			{
				isOmitted = true;
				break;
			}
		}	
		if (isOmitted == false)
			newValue = newValue + key.substring(i,i+1);
	}
	document.getElementById(controlName).value = newValue;
}


	function CheckClistVal(paramObjChkBox, objInvisible)
	{
		var txtInvisible = document.getElementById(objInvisible);
		var blnCheck =false;
		var tableBody = document.getElementById(paramObjChkBox).childNodes[0];
		txtInvisible.value='';
		
		for (var i=0;i<tableBody.childNodes.length; i++)
		{
			var currentTd = tableBody.childNodes[i].childNodes[0];
			var listControl = currentTd.childNodes[0];
			
			if (listControl.checked == true)
			{
				blnCheck=true;
				txtInvisible.value='Def';
			}
		}
		if (blnCheck==false) 
		{
			txtInvisible.value='';
		}
		Page_ClientValidate();
		
	}
	function ValidateTimeFormat(ParamTxtInput)
		{
			ParamTxtInput.value=Trim(ParamTxtInput.value);
			var re = new RegExp("^([0-1][0-9]|[2][0-3]):([0-5][0-9])$"); 
			if (re == " ") 
				{ 
				return true; 
				}
			if (ParamTxtInput.value.match(re)) 
				{ 
				return true; 
				} 
			else 
				{ 
				return false; 
				} 	
		}
		
	function omitCharsOnCompsTxt(param1,toBeOmittedChars_)
	{
		var key = document.getElementById(param1.id).value;
		var newValue = "";
		var toBeOmittedChars= toBeOmittedChars_+ "'";
		
		var isOmitted = false;
		for (i=0;i<key.length;i++)	
		{
			isOmitted = false;
			for (j=0;j<toBeOmittedChars.length;j++)	
			{	
				if (key.charCodeAt(i) == toBeOmittedChars.charCodeAt(j))
				{
					isOmitted = true;
					break;
				}
			}	
			if (isOmitted == false)
				newValue = newValue + key.substring(i,i+1);
		}
		document.getElementById(param1.id).value = newValue;
	}


function isAccepted(key,addKey)
{	
	if (addKey != "") 
	{
		for (j=0;j<addKey.length;j++)
		{
			
			if (key == addKey.charCodeAt(j))
			{
				return true;
			}
			
		}
	}
	return false;
}



function alphaNumericExceptExcludeSingleQuote(event, addKey, nextControlName)
{
	var pressedKey
	var nextControl = document.getElementById(nextControlName);
	var thisControl;
	
	if(navigator.appName == "Microsoft Internet Explorer")
		thisControl = event.srcElement;
	else
		thisControl = event.target;

	if(navigator.appName == "Microsoft Internet Explorer")	
		pressedKey = event.keyCode;
	else
	{
		pressedKey = event.charCode;
	}				
	if ( (isAccepted(pressedKey,addKey)==false) || (pressedKey == 0))
	{		
		if (nextControlName != "") 
		{
			if ( thisControl.maxLength == thisControl.value.length + 1 )
			{
				if (pressedKey>0)
				{
					thisControl.value = thisControl.value + String.fromCharCode(pressedKey);
					if (nextControl){
						nextControl.focus();
					}
					return false;
				}
			}		
			return true;
		}
		else
		{
			return true;
		}
	}
	else
		return false;
}


function alphaNumericExcept(event, addKey, nextControlName)
{
	var pressedKey
	var nextControl = document.getElementById(nextControlName);
	var thisControl;
	
	if(navigator.appName == "Microsoft Internet Explorer")
		thisControl = event.srcElement;
	else
		thisControl = event.target;

	if(navigator.appName == "Microsoft Internet Explorer")	
		pressedKey = event.keyCode;
	else
	{
		pressedKey = event.charCode;
	}				
	if (pressedKey == 39)
	{ 
		return false;
	}
	if ( (isAccepted(pressedKey,addKey)==false) || (pressedKey == 0))
	{		
		if (nextControlName != "") 
		{
			if ( thisControl.maxLength == thisControl.value.length + 1 )
			{
				if (pressedKey>0)
				{
					thisControl.value = thisControl.value + String.fromCharCode(pressedKey);
					if (nextControl){
						nextControl.focus();
					}
					return false;
				}
			}		
			return true;
		}
		else
		{
			return true;
		}
	}
	else
		return false;
}

function NumericOnlyWith(event, addKey)
{
	var pressedKey
	
	if(navigator.appName == "Microsoft Internet Explorer")	
		pressedKey = event.keyCode;
	else
	{
		pressedKey = event.charCode;
	}					
	if ((pressedKey >=48 && pressedKey<=57) || (pressedKey == 0)) 
		{
			return true;
		}
	else
		{
				if (isAccepted(pressedKey,addKey))
					{
					return true;
					}
				else
					{
					return false;
					}
		}
		
}

function NumericOnlyBlurWith(controlName, addKey)
{
	var key = controlName.value;
	var newValue = "";
	for (i=0;i<key.length;i++)	
	{
		if ( (key.charCodeAt(i) >=48 && key.charCodeAt(i)<=57) || (key.charCodeAt(i) == 0) )
		{
			newValue = newValue + key.charAt(i);
		}				
	}			
	controlName.value = newValue;	
}

function NumOnlyBlurWithOnGridTxt(param1, addKey)
	{
		var key = document.getElementById(param1.id).value;
		var newValue = "";
		for (i=0;i<key.length;i++)	
		{
			if ((key.charCodeAt(i) >=48 && key.charCodeAt(i)<=57) || (key.charCodeAt(i) == 0))
			{
				newValue = newValue + key.charAt(i);
			}	
			else
			{
				if (isAccepted(key.charCodeAt(i),addKey))
				{
					newValue = newValue + key.charAt(i);
				}
			}
		}			
		document.getElementById(param1.id).value = newValue;
	}


function numericOnlyUniv(event)
{	
	if(navigator.appName == "Microsoft Internet Explorer")	
		pressedKey = event.keyCode;
	else
		pressedKey = event.which
	
	if ((pressedKey >=48 && pressedKey<=57) || pressedKey==8)
	{
		return true;
	}
	else
	{	
		return false;
	}
}



function numericOnlyWithComa(event)
{	
	if(navigator.appName == "Microsoft Internet Explorer")	
		pressedKey = event.keyCode;
	else
		pressedKey = event.which;
	
	if ((pressedKey >=48 && pressedKey<=57) || pressedKey==8 || pressedKey==44 || pressedKey==46)
	{
		return true;
	}
	else
	{	
		return false;
	}
}



function alphaNumericPlusUniv(event)
{	
	if(navigator.appName == "Microsoft Internet Explorer")	
		pressedKey = event.keyCode;
	else
		pressedKey = event.which
	
	if ((pressedKey ==45) || (pressedKey >=47 && pressedKey<=57) || (pressedKey >=65 && pressedKey <=90) || (pressedKey >=97 && pressedKey <=122) || pressedKey==8)
	{
		return true;
	}
	else
	{	
		return false;
	}
}

function alphaNumericPlusBlur(controlName)
{	
	var key = controlName.value;
	var newValue = "";
	for (i=0;i<key.length;i++)	
	{
		if ((key.charCodeAt(i) ==45) || (key.charCodeAt(i) >=47 && key.charCodeAt(i)<=57) || (key.charCodeAt(i) >=65 && key.charCodeAt(i) <=90) || (key.charCodeAt(i) >=97 && key.charCodeAt(i) <=122) || key.charCodeAt(i)==8)
		{
			newValue = newValue + key.charAt(i);
		}				
	}			
	controlName.value = newValue;
}

function alphaNumericPlusSpaceUniv(event)
{	
	if(navigator.appName == "Microsoft Internet Explorer")	
		pressedKey = event.keyCode;
	else
		pressedKey = event.which
	
	if ((pressedKey ==32 || pressedKey ==45) || (pressedKey >=47 && pressedKey <=57) || (pressedKey >=65 && pressedKey <=90) || (pressedKey >=97 && pressedKey <=122) || pressedKey==8)
	{
		return true;
	}
	else
	{	
		return false;
	}
}

function alphaNumericPlusSpaceBlur(controlName)
{	
	var key = controlName.value;
	var newValue = "";
	for (i=0;i<key.length;i++)	
	{
		if ((key.charCodeAt(i) ==32 || key.charCodeAt(i) ==45) || (key.charCodeAt(i) >=47 && key.charCodeAt(i) <=57) || (key.charCodeAt(i) >=65 && key.charCodeAt(i) <=90) || (key.charCodeAt(i) >=97 && key.charCodeAt(i) <=122) || key.charCodeAt(i)==8)
		{
			newValue = newValue + key.charAt(i);
		}				
	}			
	controlName.value = newValue;
}


function HtmlCharUniv(event)
{	
	if(navigator.appName == "Microsoft Internet Explorer")	
		pressedKey = event.keyCode;
	else
		pressedKey = event.which
	
	if (pressedKey !=60 && pressedKey !=62 && pressedKey != 39)
	{
		return true;
	}
	else
	{	
		return false;
	}
}

function HtmlCharBlur(controlName)
{	
	var key = controlName.value;
	var newValue = "";
	for (i=0;i<key.length;i++)	
	{
		if (key.charCodeAt(i) !=60 && key.charCodeAt(i) !=62 && key.charCodeAt(i) != 39)
		{
			newValue = newValue + key.charAt(i);
		}				
	}			
	controlName.value = newValue;
}


function replace(string,text,by) 
			{
				var strLength = string.length, txtLength = text.length;
				if ((strLength == 0) || (txtLength == 0)) return string;

				var i = string.indexOf(text);
				if ((!i) && (text != string.substring(0,txtLength))) return string;
				if (i == -1) return string;

				var newstr = string.substring(0,i) + by;

				if (i+txtLength < strLength)
				newstr += replace(string.substring(i+txtLength,strLength),text,by);

				return newstr;
			}

function showPopUp(Url, Parameters, Height, Width, CallbackFunction)
{
	var strFeature = 'dialogHeight:' + Height + 'px;';	
	strFeature += 'dialogWidth:' + Width + 'px;';
	strFeature += 'center:yes;';	
	strFeature += 'status:no;';
	strFeature += 'help:no;';
	strFeature += 'resizable:no;';
	
	if(navigator.appName == "Microsoft Internet Explorer")
	{
		var retVal = window.showModalDialog(Url, Parameters,strFeature);
		if (CallbackFunction != null && retVal != null) {
		CallbackFunction(retVal);
		}
	}
	else {
	    openDGDialog(Url, Width, Height, CallbackFunction);
	    return false;
	}
}


function showPopUpPPT(Url, Parameters, Height, Width, CallbackFunction) {
    var strFeature = 'dialogHeight:' + Height + 'px;';
    strFeature += 'dialogWidth:' + Width + 'px';
    strFeature += 'center:yes;';
    strFeature += 'status:no;';
    strFeature += 'help:no;';
    strFeature += 'resizable:yes;';
    strFeature += 'menubar:no;';
    strFeature += 'toolbar:no;';
    strFeature += "maximize:yes;";
    strFeature += "fullscreen:yes;";
   
    
    if (navigator.appName == "Microsoft Internet Explorer") {
        var retVal = window.showModalDialog(Url, Parameters, strFeature);
        if (CallbackFunction != null && retVal != null) {
            CallbackFunction(retVal);
        }
    }
    else {
        openDGDialogPPT(Url, Width, Height, CallbackFunction);
        return false;
    }
}



var Nav4 = ((navigator.appName == "Netscape") && (parseInt(navigator.appVersion) == 4))

var dialogWin = new Object()

function openDGDialog(url, width, height, returnFunc, args) {
	if (!dialogWin.win || (dialogWin.win && dialogWin.win.closed)) {
	    dialogWin.returnFunc = returnFunc
		dialogWin.returnedValue = ""
		dialogWin.args = args
		dialogWin.url = url
		dialogWin.width = width
		dialogWin.height = height
		dialogWin.name = (new Date()).getSeconds().toString()
		if (Nav4) {
			dialogWin.left = window.screenX + 
			   ((window.outerWidth - dialogWin.width) / 2)
			dialogWin.top = window.screenY + 
			   ((window.outerHeight - dialogWin.height) / 2)
			var attr = "screenX=" + dialogWin.left + 
			   ",screenY=" + dialogWin.top + ",resizable=no,width=" + 
			   dialogWin.width + ",height=" + dialogWin.height
		} else {
			dialogWin.left = (screen.width - dialogWin.width) / 2
			dialogWin.top = (screen.height - dialogWin.height) / 2
			var attr = "left=" + dialogWin.left + ",top=" + 
			   dialogWin.top + ",resizable=no,width=" + dialogWin.width + 
			   ",height=" + dialogWin.height
		}
		
		dialogWin.win=window.open(dialogWin.url, dialogWin.name, attr)
		dialogWin.win.focus()
	} else {
		dialogWin.win.focus()
	}
}



function openDGDialogPPT(url, width, height, returnFunc, args) {
    if (!dialogWin.win || (dialogWin.win && dialogWin.win.closed)) {
        dialogWin.returnFunc = returnFunc
        dialogWin.returnedValue = ""
        dialogWin.args = args
        dialogWin.url = url
        dialogWin.width = width
        dialogWin.height = height
        dialogWin.name = (new Date()).getSeconds().toString()
        if (Nav4) {
            dialogWin.left = window.screenX +
			   ((window.outerWidth - dialogWin.width) / 2)
            dialogWin.top = window.screenY +
			   ((window.outerHeight - dialogWin.height) / 2)
            var attr = "screenX=" + dialogWin.left +
			   ",screenY=" + dialogWin.top + ",resizable=yes,width=" +
			   dialogWin.width + ",height=" + dialogWin.height
        } else {
            dialogWin.left = (screen.width - dialogWin.width) / 2
            dialogWin.top = (screen.height - dialogWin.height) / 2
            var attr = "left=" + dialogWin.left + ",top=" +
			   dialogWin.top + ",resizable=yes,width=" + dialogWin.width +
			   ",height=" + dialogWin.height
        }

        //attr += ',menubar=yes';
        attr += ',toolbar=no';
        attr += ',location=0';
        attr += ',menubar=no';
        
        dialogWin.win = window.open(dialogWin.url, dialogWin.name, attr)
        dialogWin.win.focus()
    } else {
        dialogWin.win.focus()
    }
}


function deadend() {
	if (dialogWin.win && !dialogWin.win.closed) {
		dialogWin.win.focus()
		return false
	}
}

var IELinkClicks

function checkModal() {
	finishChecking()
	return true
}

function finishChecking() {
	if (dialogWin.win && !dialogWin.win.closed) {
		dialogWin.win.focus() 
	}
}

function LockToModal() {
    if (dialogWin.win && !dialogWin.win.closed) {
        dialogWin.win.focus()
        document.onmousedown = deadend; document.onkeyup = deadend; document.onmousemove = deadend; document.onclick = deadend;
    }
}

function stopmouse(e) {
	if (navigator.appName == 'Netscape' && (e.which == 3 || e.which == 2))
		return false;
	else if (navigator.appName == 'Microsoft Internet Explorer' && (event.button == 2 || event.button == 3)) {
		alert("You can't see view source !");
		return false;
	}
	return true;
}
document.onmousedown=stopmouse;
if (document.layers) window.captureEvents(Event.MOUSEDOWN);
window.onmousedown=stopmouse;

function hb(b) {event.srcElement.className=b};

function pic(p1,p2,p3,p4) {
	p3 = trim(p3);
	var hsl="";
	cek="AN!";
	if ( (arguments.length!=4) || cek.indexOf(p4)<0 ) {
		alert("Error !!!, Syntac : PIC(this, this.value, 'c'/'C'/'x'/'X'/'9'/',', 'A'/'N'/'!' )");
		return;
	}
	cek="cCxX9.-()";
	for(i=0; i<p3.length; i++)
		if ( cek.indexOf(p3.charAt(i))<0 ) {
			alert("Error !!!, Syntac : PIC(this, this.value, 'c'/'C'/'x'/'X'/'9'/',', 'A'/'N' )");
			return;
		}
	if (p2.length>0 && p4=='A')
		for(i=0; i<p3.length; i++) {
			abc = p2.charAt(i);
			switch (p3.charAt(i)) {
				case "c" : { if ((abc>='a' && abc<='z') || (abc>='A' && abc<='Z')) hsl=hsl+abc.toLowerCase(); break; }
				case "C" : { if ((abc>='a' && abc<='z') || (abc>='A' && abc<='Z')) hsl=hsl+abc.toUpperCase(); break; }
				case "x" : { hsl=hsl+abc.toLowerCase(); break; }
				case "X" : { hsl=hsl+abc.toUpperCase(); break; }
				case "9" : { if(! isNaN(abc)) hsl=hsl+abc; break; }
			}
			if (i+1<p3.length && p3.charAt(i+1)=="." && hsl.length==i+1) hsl=hsl+".";
			if (i+1<p3.length && p3.charAt(i+1)=="-" && hsl.length==i+1) hsl=hsl+"-";
			if (i+1<p3.length && p3.charAt(i+1)=="(" && hsl.length==i+1) hsl=hsl+"(";
			if (i+1<p3.length && p3.charAt(i+1)==")" && hsl.length==i+1) hsl=hsl+")";
			if (p2.length-1<=i) i=p3.length;
		}
		if (p2.length>0 && p4=='N') {
			var num=0;
			var des=0;
			if (p2.length==2 && p2=='0-') p2='-';
			for(i=0; i<p3.length; i++) {
				if ( p3.charAt(i)=='9')
				if (des==0) num++;
				else des++;
				if ( p3.charAt(i)==',') des++;
			}
			for(i=0; i<p2.length; i++)
				if ( (p2.charAt(i)=='-' && i==0) || (p2.charAt(i)==',' && (p2.indexOf(".",2)==-1 || i==p2.indexOf(".")))  || (! isNaN(p2.charAt(i)) && p2.charAt(i)!=' ') ) hsl=hsl+p2.charAt(i);
			if (hsl.indexOf(".")>=0) {
				if (hsl.length-hsl.indexOf(".")>des) hsl=hsl.slice(0,hsl.length-1);
				var hsl2=hsl.slice(hsl.indexOf("."),hsl.length);
				var koma=-1;
				for(i=hsl.indexOf(".")-1; i>=0; i--) {
					koma++;
					if (koma==3 && hsl.charAt(i)!='-') {
						hsl2=hsl.charAt(i)+'.'+hsl2;
						koma=0;
					}
					else hsl2=hsl.charAt(i)+hsl2;
				}
				hsl=hsl2.replace(/\-/g,'');
			}
		else {
			if (hsl.charAt(0)=='-') num++;
			if (hsl.length>num) hsl=hsl.slice(0,num);
			if (hsl.length==num && des>0) titik=1;
			else titik=0;
			var hsl2="";
			var koma=-1;
			for(i=hsl.length-1; i>=0; i--) {
				koma++;
				if (koma==3 && hsl.charAt(i)!='-') {
					hsl2=hsl.charAt(i)+'.'+hsl2;
					koma=0;
				}
				else hsl2=hsl.charAt(i)+hsl2;
			}
			hsl2 = hsl2.replace(/\-/g,"");
			if (hsl2.length==0) hsl2=0;
			if (hsl2.length>1) {
				if (hsl2.charAt(0)=='0' && hsl2.charAt(1)!=',') hsl2 = hsl2.slice(1,hsl2.length);
				else if (hsl2.length>2 && hsl2.charAt(0)=='-' && hsl2.charAt(1)=='0' && hsl2.charAt(2)!=',') hsl2 = '-'+hsl2.slice(2,hsl2.length);
			}
			hsl=hsl2;
			if (titik==1) hsl=hsl+".";
			
		}
	}
	if (p2.length>0 && p4=='!')
	{
		for(i=0; i<p2.length; i++) {
			abc=p2.charAt(i);
			if ((abc>='a' && abc<='z') || (abc>='A' && abc<='Z')) hsl=hsl+abc.toUpperCase();
		}
	}
	p1.value=hsl;
	p1.focus;
	return true;
}

function date(p1,p2,p3,p4) {
	p3 = trim(p3);
	var hsl="";
	cek="AN!";
	if ( (arguments.length!=4) || cek.indexOf(p4)<0 ) {
		alert("Error !!!, Syntac : PIC(this, this.value, 'c'/'C'/'x'/'X'/'9'/',', 'A'/'N'/'!' )");
		return;
	}
	cek="cCxX9.-()";
	for(i=0; i<p3.length; i++)
		if ( cek.indexOf(p3.charAt(i))<0 ) {
			alert("Error !!!, Syntac : PIC(this, this.value, 'c'/'C'/'x'/'X'/'9'/',', 'A'/'N' )");
			return;
		}
	if (p2.length>0 && p4=='A')
		for(i=0; i<p3.length; i++) {
			abc = p2.charAt(i);
			switch (p3.charAt(i)) {
				case "c" : { if ((abc>='a' && abc<='z') || (abc>='A' && abc<='Z')) hsl=hsl+abc.toLowerCase(); break; }
				case "C" : { if ((abc>='a' && abc<='z') || (abc>='A' && abc<='Z')) hsl=hsl+abc.toUpperCase(); break; }
				case "x" : { hsl=hsl+abc.toLowerCase(); break; }
				case "X" : { hsl=hsl+abc.toUpperCase(); break; }
				case "9" : { if(! isNaN(abc)) hsl=hsl+abc; break; }
			}
			if (i+1<p3.length && p3.charAt(i+1)=="." && hsl.length==i+1) hsl=hsl+".";
			if (i+1<p3.length && p3.charAt(i+1)=="-" && hsl.length==i+1) hsl=hsl+"-";
			if (i+1<p3.length && p3.charAt(i+1)=="(" && hsl.length==i+1) hsl=hsl+"(";
			if (i+1<p3.length && p3.charAt(i+1)==")" && hsl.length==i+1) hsl=hsl+")";
			if (p2.length-1<=i) i=p3.length;
		}
		if (p2.length>0 && p4=='N') {
			var num=0;
			var des=0;
			if (p2.length==2 && p2=='0-') p2='-';
			for(i=0; i<p3.length; i++) {
				if ( p3.charAt(i)=='9')
				if (des==0) num++;
				else des++;
				if ( p3.charAt(i)==',') des++;
			}
			for(i=0; i<p2.length; i++)
				if ( (p2.charAt(i)=='-' && i==0) || (p2.charAt(i)==',' && (p2.indexOf(".",2)==-1 || i==p2.indexOf(".")))  || (! isNaN(p2.charAt(i)) && p2.charAt(i)!=' ') ) hsl=hsl+p2.charAt(i);
			if (hsl.indexOf(".")>=0) {
				if (hsl.length-hsl.indexOf(".")>des) hsl=hsl.slice(0,hsl.length-1);
				var hsl2=hsl.slice(hsl.indexOf("."),hsl.length);
				var koma=-1;
				for(i=hsl.indexOf(".")-1; i>=0; i--) {
					koma++;
					if (koma==3 && hsl.charAt(i)!='-') {
						hsl2=hsl.charAt(i)+'.'+hsl2;
						koma=0;
					}
					else hsl2=hsl.charAt(i)+hsl2;
				}
				hsl=hsl2;
			}
		else {
			if (hsl.charAt(0)=='-') num++;
			if (hsl.length>num) hsl=hsl.slice(0,num);
			if (hsl.length==num && des>0) titik=1;
			else titik=0;
			var hsl2="";
			var koma=-1;
			for(i=hsl.length-1; i>=0; i--) {
				koma++;
				if (koma==2 && hsl.charAt(i)!='-') {
					hsl2=hsl.charAt(i)+':'+hsl2;
					koma=0;
				}
				else hsl2=hsl.charAt(i)+hsl2;
			}
			if (hsl2.length==0) hsl2=0;
			if (hsl2.length>1) {
				if (hsl2.charAt(0)=='0' && hsl2.charAt(1)!=',') hsl2 = hsl2.slice(1,hsl2.length);
				else if (hsl2.length>2 && hsl2.charAt(0)=='-' && hsl2.charAt(1)=='0' && hsl2.charAt(2)!=',') hsl2 = '-'+hsl2.slice(2,hsl2.length);
			}
			hsl=hsl2;
			if (titik==1) hsl=hsl+".";
		}
	}
	if (p2.length>0 && p4=='!') {
		for(i=0; i<p2.length; i++) {
			abc=p2.charAt(i);
			if ((abc>='a' && abc<='z') || (abc>='A' && abc<='Z')) hsl=hsl+abc.toUpperCase();
		}
	}
	p1.value=hsl;
	p1.focus;
	return true;
}


function trim(input) {
   var hsl="" ;
   hsl = ltrim(input) ;
   hsl = rtrim(hsl) ;
   return hsl ;
}

function ltrim(input) {
	isiinput = input ;
	buang = 0 ;
	for(i=0; i<input.length; i++)
		if (input.charAt(i)==" ") buang++ ;
		else i = input.length+1 ;
	if (buang > 0 & input.length > buang) {
		isiinput = input.slice(buang,input.length) ;
		buang = 0 ;
	}
	if (buang == input.length) isiinput = "" ;
	return isiinput ;
}

function rtrim(input) {
	isiinput = input ;
	buang = 0 ;
	for(i=isiinput.length-1; i>=0; i--)
		if (isiinput.charAt(i)==" ") buang++ ;
		else i = -1 ;
	if (buang > 0 & isiinput.length > buang) {
		isiinput = isiinput.slice(0,isiinput.length - buang) ;
		buang = 0 ;
	}
	if (buang == isiinput.length) isiinput = "" ;
	return isiinput ;
}

function addThousandDelimeter(angka) {
    var number_string = angka.toString().replace(/[^\d]/g, '').toString(),
        split = number_string.split(','),
        sisa = split[0].length % 3,
        rupiah = split[0].substr(0, sisa),
        ribuan = split[0].substr(sisa).match(/\d{3}/gi);

    // tambahkan titik jika yang di input sudah menjadi angka ribuan
    if (ribuan) {
        separator = sisa ? ',' : '';
        rupiah += separator + ribuan.join(',');
    }

    rupiah = split[1] != undefined ? rupiah + ',' + split[1] : rupiah;
    return rupiah;
}


function removeThousandDelimeter(text) {
    var split = text.toString().split(".");
    return split.join("");
}



