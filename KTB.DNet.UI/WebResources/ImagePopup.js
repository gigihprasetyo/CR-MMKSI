//-----------------------------------------------------------------------------
// Author   : Sudipta Das
// Date     : 18-Nov-2008
// Purpose  : This script is to show a pop-up image.
//-----------------------------------------------------------------------------
function DownloadFile(obj)
{
	window.location.href="../Download.aspx?file="+ obj.lowsrc;
}

function Large(obj)
{
	
	var imgbox=document.getElementById("imgbox");
	imgbox.style.visibility='visible';
	var img = document.createElement("img");    
	//alert(obj.lowsrc);
	//return;
	img.src=obj.lowsrc;
	img.style.width='200px';
	img.style.height='200px';
    
	if(img.addEventListener){
		img.addEventListener('mouseout',Out,false);
	} else {
		img.attachEvent('onmouseout',Out);
	}             
	imgbox.innerHTML='';    
	imgbox.appendChild(img);    
	imgbox.style.left=(getElementLeft(obj)) +'px';
	imgbox.style.top=(getElementTop(obj)) + 'px';     
}  

function Out()
{
	document.getElementById("imgbox").style.visibility='hidden';
}

function getElementLeft(elm) 
{
	var x = 0;

	//set x to elm’s offsetLeft
	x = elm.offsetLeft;

	//set elm to its offsetParent
	elm = elm.offsetParent;

	//use while loop to check if elm is null
	// if not then add current elm’s offsetLeft to x
	//offsetTop to y and set elm to its offsetParent

	while(elm != null)
	{
		x = parseInt(x) + parseInt(elm.offsetLeft) - 26;
		elm = elm.offsetParent;
	}
	
	//x = 600;
     
	return x;
}

function getElementTop(elm) 
{
	var y = 0;

	//set x to elm’s offsetLeft
	y = elm.offsetTop;

	//set elm to its offsetParent
	elm = elm.offsetParent;

	//use while loop to check if elm is null
	// if not then add current elm’s offsetLeft to x
	//offsetTop to y and set elm to its offsetParent

	while(elm != null)
	{
		y = parseInt(y) + parseInt(elm.offsetTop) - 16;
		elm = elm.offsetParent;
	}
	//y = 100;
     
	return y;
}
