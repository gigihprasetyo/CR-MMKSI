var message="";

function disableNewWindow(e)
{	
	if (navigator.appName=="Microsoft Internet Explorer")
	{
		var ch = String.fromCharCode(event.keyCode);			
		if ((event.ctrlKey||event.ctrlLeft) && (ch=="N" || ch=="n"))
		{
			event.returnValue=false;
			return false;
		}
	}
	else
	{
		var altKey = (e.altKey || e.altLeft);
		var ctrlKey = (e.ctrlKey || e.ctrlLeft);
		var shiftKey = (e.shiftKey || e.shiftKey);
		var ch = e.which;
		if ((ctrlKey) && (ch==78))
		{
			(message);
			alert("Tombol akses tidak diperbolehkan!!!")
			return false;
		}
	}
	return true;
}		

function clickIE() 
{
	if (document.all) 
	{
		(message);
		return false;
	}
}

function clickNS(e) 
{		
	if (navigator.appName=="Microsoft Internet Explorer")
	{
		if (event.button==2||event.button==3)
		{
			(message);
			event.returnValue=false;
			return false;
		}
		if (event.button==1 && (event.shiftKey || event.shiftLeft))
		{
			(message);					
			event.returnValue=false;
			alert("Tombol akses tidak diperbolehkan!!!")
			return false;
		}
	}
	else
	{	
		if (document.layers||(document.getElementById&&!document.all)) 
		{	
			var altKey = (e.modifiers & 1) || e.altKey;
			var ctrlKey = (e.modifiers & 2) || e.ctrlKey;
			var shiftKey = (e.modifiers & 4) || e.shiftKey;
			
			if (e.which==2||e.which==3) 
			{
				(message);
				return false;
			}
			if (e.which==1 && (shiftKey) && (ctrlKey))
			{
				(message);
				if (e.type == "mousedown")
				{
					alert("Tombol akses tidak diperbolehkan!!!");
					document.open("./Error.html","_self", "fullscreen=yes,titlebar=no,personalbar=no,toolbar=no,status=0,menubar=no,scrollbars=yes,resizable=no,directories=no,location=no", true);
				}				
				(message);
				return false;
			}
			if (e.which==1 && (shiftKey))
			{
				(message);
				if (e.type == "mousedown")
				{
					alert("Tombol akses tidak diperbolehkan!!!");
					document.open("./Error.html","_self", "fullscreen=yes,titlebar=no,personalbar=no,toolbar=no,status=0,menubar=no,scrollbars=yes,resizable=no,directories=no,location=no", true);
					return false;
				}
				(message);
				return false;
			}
		}
	}
	return true;
}

if (document.layers)
{
	document.captureEvents(Event.MOUSEDOWN);
	document.captureEvents(Event.KEYDOWN);
	document.onmousedown=clickNS;
	document.onmouseup=clickNS;
}
else
{	
	document.onmousedown=clickNS;
	document.onmouseup=clickNS;
	document.oncontextmenu=clickIE;
}

document.oncontextmenu=new Function("return false")				
document.onkeydown=disableNewWindow;
document.onkeyup=disableNewWindow;

