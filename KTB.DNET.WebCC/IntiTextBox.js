
// reject all character except number
// also reject numeric keypad, editing key and other key (f1)
// i wonder if this still work if i use dvorak keyboard :)
function IntiTextBox_Numeric_KeyDown(control, event) {
	if ( ! String.fromCharCode(event.keyCode).match(/[0-9]/)) {
		event.returnValue=false;
	}
}

