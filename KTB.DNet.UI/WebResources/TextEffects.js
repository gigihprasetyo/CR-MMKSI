function AntsEffect(animationSpeed){		
	window.antsEffect = this;
	this.elements = new Object();
	this.animationSpeed = animationSpeed;
	this.frameCounter = 0;
}

AntsEffect.prototype.DoEffect = function(){	
	var arrTopImages = ["url(images/Ants1.bmp)", "url(images/ants2.bmp)", "url(images/ants3.bmp)", "url(images/ants4.bmp)", "url(images/ants5.bmp)", "url(images/ants6.bmp)", "url(images/ants7.bmp)","url(images/ants8.bmp)","url(images/ants9.bmp)"]
	var arrLeftImages = ["url(images/antsL1.bmp)", "url(images/antsL2.bmp)", "url(images/antsL3.bmp)", "url(images/antsL4.bmp)", "url(images/antsL5.bmp)", "url(images/antsL6.bmp)", "url(images/antsL7.bmp)","url(images/antsL8.bmp)","url(images/antsL9.bmp)"]
		
	var _objEffect = window.antsEffect;
	
	for(var i in _objEffect.elements){	
//top		
		_objEffect.elements[i].tBodies[0].rows[0].cells[0].style.height = "1px";
		_objEffect.elements[i].tBodies[0].rows[0].cells[0].style.backgroundImage = arrTopImages[_objEffect.frameCounter];
		_objEffect.elements[i].tBodies[0].rows[0].cells[0].style.backgroundRepeat = 'repeat-x';
//left		
		_objEffect.elements[i].tBodies[0].rows[1].cells[0].width = "1px";
		_objEffect.elements[i].tBodies[0].rows[1].cells[0].style.backgroundImage = arrLeftImages[_objEffect.frameCounter];
		_objEffect.elements[i].tBodies[0].rows[1].cells[0].style.backgroundRepeat = 'repeat-y';		
//right
		_objEffect.elements[i].tBodies[0].rows[1].cells[2].width = "1px";
		_objEffect.elements[i].tBodies[0].rows[1].cells[2].style.backgroundImage = arrLeftImages[arrTopImages.length - 1 - _objEffect.frameCounter];
		_objEffect.elements[i].tBodies[0].rows[1].cells[2].style.backgroundRepeat = 'repeat-y';		
//bottom
		_objEffect.elements[i].tBodies[0].rows[2].cells[0].style.height = "1px";
		_objEffect.elements[i].tBodies[0].rows[2].cells[0].style.backgroundImage = arrTopImages[arrTopImages.length - 1 - _objEffect.frameCounter];
		_objEffect.elements[i].tBodies[0].rows[2].cells[0].style.backgroundRepeat = 'repeat-x';
		
	}	
	
	_objEffect.frameCounter++;
	if(_objEffect.frameCounter >= arrTopImages.length) _objEffect.frameCounter = 0;
}

AntsEffect.prototype.AddTable = function(tbl){
	if(tbl.tagName != "TABLE"){
		new Error("must be a table element");
	}
	tbl.cellSpacing = 0;
	tbl.cellPadding = 0;
	this.elements[tbl.id] = tbl;
}

AntsEffect.prototype.Start = function(){	
	this.DoEffect();
	window.setInterval(this.DoEffect, this.animationSpeed);
}


function LasVegasLightsEffect(animationSpeed){		
	window.LasVegasLightsEffect = this;
	this.elements = new Object();
	this.animationSpeed = animationSpeed;
	this.frameCounter = 0;
}

LasVegasLightsEffect.prototype.DoEffect = function(){	
	var arrTopImages = ["url(images/Vegas1.bmp)", "url(images/Vegas2.bmp)", "url(images/Vegas3.bmp)", "url(images/Vegas4.bmp)", "url(images/Vegas5.bmp)"]
		
	var _objEffect = window.LasVegasLightsEffect;
	
	for(var i in _objEffect.elements){	
//top		
		_objEffect.elements[i].tBodies[0].rows[0].cells[0].style.height = "6px";
		_objEffect.elements[i].tBodies[0].rows[0].cells[0].style.backgroundImage = arrTopImages[_objEffect.frameCounter];
		_objEffect.elements[i].tBodies[0].rows[0].cells[0].style.backgroundRepeat = 'repeat-x';
//left		
		_objEffect.elements[i].tBodies[0].rows[1].cells[0].width = "6px";
		_objEffect.elements[i].tBodies[0].rows[1].cells[0].style.backgroundImage = arrTopImages[_objEffect.frameCounter];
		_objEffect.elements[i].tBodies[0].rows[1].cells[0].style.backgroundRepeat = 'repeat-y';		
//right
		_objEffect.elements[i].tBodies[0].rows[1].cells[2].width = "6px";
		_objEffect.elements[i].tBodies[0].rows[1].cells[2].style.backgroundImage = arrTopImages[_objEffect.frameCounter];
		_objEffect.elements[i].tBodies[0].rows[1].cells[2].style.backgroundRepeat = 'repeat-y';		
//bottom
		_objEffect.elements[i].tBodies[0].rows[2].cells[0].style.height = "6px";
		_objEffect.elements[i].tBodies[0].rows[2].cells[0].style.backgroundImage = arrTopImages[_objEffect.frameCounter];
		_objEffect.elements[i].tBodies[0].rows[2].cells[0].style.backgroundRepeat = 'repeat-x';
		
	}	
	
	_objEffect.frameCounter++;
	if(_objEffect.frameCounter >= arrTopImages.length) _objEffect.frameCounter = 0;
}

LasVegasLightsEffect.prototype.AddTable = function(tbl){
	if(tbl.tagName != "TABLE"){
		new Error("must be a table element");
	}
	tbl.cellSpacing = 0;
	tbl.cellPadding = 0;
	this.elements[tbl.id] = tbl;
}

LasVegasLightsEffect.prototype.Start = function(){	
	this.DoEffect();
	window.setInterval(this.DoEffect, this.animationSpeed);
}

function BlinkingEffect(animationSpeed){		
	window.BlinkingEffect = this;
	this.elements = new Object();
	this.animationSpeed = animationSpeed;
	this.frameCounter = 0;	
	this.originalBGColor = "";
}

BlinkingEffect.prototype.DoEffect = function(){	
		
	var _objEffect = window.BlinkingEffect;
	var strColor = "aqua";
	if(_objEffect.frameCounter >= 1){
		strColor = _objEffect.originalBGColor;
	}
	
	
	
	for(var i in _objEffect.elements){	
//top		
		_objEffect.elements[i].tBodies[0].rows[0].cells[0].style.height = "6px";
		_objEffect.elements[i].tBodies[0].rows[0].cells[0].style.backgroundColor = strColor;
//left		
		_objEffect.elements[i].tBodies[0].rows[1].cells[0].width = "6px";
		_objEffect.elements[i].tBodies[0].rows[1].cells[0].style.backgroundColor = strColor;
//center
		_objEffect.elements[i].tBodies[0].rows[1].cells[1].style.backgroundColor = strColor;
//center child
		_objEffect.elements[i].tBodies[0].rows[1].cells[1].childNodes[0].style.backgroundColor = strColor;
//right
		_objEffect.elements[i].tBodies[0].rows[1].cells[2].width = "6px";
		_objEffect.elements[i].tBodies[0].rows[1].cells[2].style.backgroundColor = strColor;
//bottom
		_objEffect.elements[i].tBodies[0].rows[2].cells[0].style.height = "6px";
		_objEffect.elements[i].tBodies[0].rows[2].cells[0].style.backgroundColor = strColor;
		
	}	
	
	_objEffect.frameCounter++;
	if(_objEffect.frameCounter > 1) _objEffect.frameCounter = 0;
}

BlinkingEffect.prototype.AddTable = function(tbl){
	if(tbl.tagName != "TABLE"){
		new Error("must be a table element");
	}
	tbl.cellSpacing = 0;
	tbl.cellPadding = 0;
	this.elements[tbl.id] = tbl;
	this.originalBGColor = tbl.tBodies[0].rows[0].cells[0].style.backgroundColor;
}

BlinkingEffect.prototype.Start = function(){	
	this.DoEffect();
	window.setInterval(this.DoEffect, this.animationSpeed);
}