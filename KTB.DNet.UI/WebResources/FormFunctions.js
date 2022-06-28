//checks all DataGrid CheckBoxes with the given name with the given
	function IsAnyCheckedCheckBox(aspCheckBoxID)
	{
        re = new RegExp(':' + aspCheckBoxID + '$')  
        var IsAnyChecked = false

        for(i = 0; i < document.forms[0].elements.length; i++) {
 
            elm = document.forms[0].elements[i]

            if (elm.type == 'checkbox') {

                if (re.test(elm.name)) {
		
					if (elm.checked == true) {
						
						IsAnyChecked = true;
						break;
						
					}                   
                }
            }
        }	
        return IsAnyChecked;
	}

    function CheckAllDataGridCheckBoxes(aspCheckBoxID, checkVal) {

        re = new RegExp(':' + aspCheckBoxID + '$')  
        //generated control name starts with a colon

        for(i = 0; i < document.forms[0].elements.length; i++) {
 
            elm = document.forms[0].elements[i]

            if (elm.type == 'checkbox') {

                if (re.test(elm.name)) {

                    elm.checked = checkVal

                }
            }
        }
    }
    
    function CheckAllDataGridCheckBoxesNew(aspCheckBoxID, checkVal) {

        re = new RegExp(':' + aspCheckBoxID + '$')
        //generated control name starts with a colon

        for (i = 0; i < document.forms[0].elements.length; i++) {

            elm = document.forms[0].elements[i]

            if (elm.type == 'checkbox') {

                if (re.test(elm.name)) {
                    if (!(elm.disabled)) {
                        elm.checked = checkVal
                    }

                }
            }
        }
    }
    
    function DisableDefaultDown()
		{	
			if (event.keyCode==13)
			{
				if (event.srcElement.type != 'submit')
				{
					event.returnValue=false;
					event.cancel=true;
				}
			}
		}

