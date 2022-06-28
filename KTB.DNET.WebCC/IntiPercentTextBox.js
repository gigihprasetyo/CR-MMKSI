
function IntiPercentTextBox_Init(control) {
	var name = control.name;
	var value = control.value;
	if (!value.match(/\./)) { value += '00'; }
	else if (value.match(/\.$/)) { value += '00'; }
	else if (value.match(/\.[0-9]$/)) { value += '0'; }
	else if (value.match(/\.[0-9][0-9]$/)) { ; }
	else if (value.match(/\.[0-9][0-9].*$/)) { value += value.substring(0, value.indexOf('.', 0)+2); }
	var negative = '';
	if ((value.replace(/[0-9,.]/g,'').length % 2)!=0)
	{
		negative = '-';
	}
	control.value=format_money(negative, value.replace(/[-,.]/g,''));
	document.forms[0].elements[name+'_prev'].value = control.value;
}

/// 
function IntiPercentTextBox_Change(control, event) {
	var org_offset = control.value.length;
	// textbox.createTextRange is non portable
	if (control.createTextRange) {
		var old_range = control.ownerDocument.selection.createRange().duplicate();
		if (old_range.parentElement()==control) {
			old_range.moveEnd('character', control.value.length);
			// find the offset from left
			org_offset = old_range.text.length;
		}
	}
	var name = control.name;
	if (document.forms[0].elements[name+'_prev'].value==control.value) {
		// optimized: don't reformat if it was not modified
		return;
	}
	if (control.value.match(/^[-0-9,.]*$/)) 
	{
		var value = control.value;
		var negative = '';
		// remove the '-' if there are double of them
		if ((value.replace(/[0-9,.]/g,'').length % 2)!=0)
		{
			negative = '-';
		}
		control.value=format_money(negative, value.replace(/[-,.]/g,''));
		if (control.value>100.0 || control.value<-100.0) {
			// replace back to the old value stored in <name>_prev
			control.value = document.forms[0].elements[name+'_prev'].value;
		} else {
			document.forms[0].elements[name+'_prev'].value = control.value;
		}
	}
	else
	// if the value is invalid
	{
		// replace back to the old value stored in <name>_prev
		control.value = document.forms[0].elements[name+'_prev'].value;
	}
	// textbox.createTextRange is non portable
	if (control.createTextRange) {
		// move the caret back to original offset position
		var range = control.createTextRange();
		range.moveStart('character', control.value.length-org_offset);
		range.collapse();
		range.select();
	}
}

/// select the whole text box on focus,
/// so user can imediatelly replace the whole text
function IntiPercentTextBox_Focus(control, event) {
	control.select();
}

/* test cases

//*/

