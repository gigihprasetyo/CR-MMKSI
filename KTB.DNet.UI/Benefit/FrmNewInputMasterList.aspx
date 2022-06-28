<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmNewInputMasterList.aspx.vb" Inherits="FrmNewInputMasterList" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>



<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAplikasiHeader</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">

		    function Count(text) {
		        //asp.net textarea maxlength doesnt work; do it by hand
		        var maxlength = 100; //set your value here (or add a parm and pass it in)
		        var object = document.getElementById(text.id)  //get your object
		        if (object.value.length > maxlength) {
		            object.focus(); //set focus to prevent jumping
		            object.value = text.value.substring(0, maxlength); //truncate the value
		            object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
		            return false;
		        }
		        return true;
		    }
            
		    function checkTextAreaMaxLength(textBox, e, length) {
		        var mLen = textBox["MaxLength"];
		        if (null == mLen)
		            mLen = length;

		        var maxLength = parseInt(mLen);
		        if (!checkSpecialKeys(e)) {
		            if (textBox.value.length > maxLength - 1) {
		                return false;
		            }
		        }
		    }

		    function checkSpecialKeys(e) {
		        if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
		            return false;
		        else
		            return true;
		    }
		    function getElement(tipeElement, IdElement) {
		        var selectbox;		        
		        var inputs = document.getElementsByTagName(tipeElement);
		        //console.log(IdElement)
		        for (var i = 0; i < inputs.length; i++) {
		            //console.log(inputs[i].id + " _ " + IdElement + " _ " + inputs[i].id.indexOf(IdElement))
		            if (inputs[i].id.indexOf(IdElement) > -1) {
		           // if (inputs[i].id.indexOf(IdElement) != -1) {
		                selectbox = inputs[i]
		                break;
		            }		            
		        }
		        return selectbox;
		    }

		    function objToString(obj) {
		        var str = '';
		        for (var p in obj) {
		            //if (obj.hasOwnProperty(p)) {
		                str += p + '::' + obj[p] + '\n';
		            //}
		        }
		        return str;
		    }

		    function ShowPPRefBenefitSelection() {
		        showPopUp('../General/../PopUp/PopUpRefBenefitSelection.aspx?DEALERONLY=True', '', 500, 760, RefBenefitSelection);
		    }

		    function RefBenefitSelection(selectedRefBenefit) {
		        var txtRefBenefitSelection = document.getElementById("txtRefBenefit");
		        var hdnBenefitMasterID = document.getElementById("hdnBenefitMasterID")
		        var btnReload = document.getElementById("btnReload")
		        
		        var result = selectedRefBenefit.split(';');

		        hdnBenefitMasterID.value = result[0];
		        txtRefBenefitSelection.value = result[1];
		        btnReload.click();
		    }

		    function btnReloadClick()
		    {
		        var hdnBenefitMasterID = document.getElementById("hdnBenefitMasterID")
		        hdnBenefitMasterID.value = '';
		        var txtRefBenefit = document.getElementById("txtRefBenefit");
		        if (txtRefBenefit.value != '')
		        {
		            var btnReload = document.getElementById("btnReload")
		            btnReload.click();
		        }
		    }

		    function ShowPPDealerSelection() {
		        //showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
		        //showPopUp('../General/../Benefit/PopUpDealerSelectionBenefit.aspx', '', 500, 760, DealerSelection);
		        showPopUp('../General/../PopUp/PopUpDealerHistorySelection.aspx?DEALERONLY=True', '', 500, 760, DealerSelection);
		    }

		    function DealerSelection(selectedDealer) {
		        var txtDealerSelection = document.getElementById("txtKodeDealer");
		        txtDealerSelection.value = selectedDealer;
		    }

		    function ShowPPLeasingSelection() {
		        showPopUp('../General/../Benefit/PopUpLeasing.aspx', '', 500, 760, LeasingSelection);
		    }

		    function LeasingSelection(selectedLaesing) {
		        ////var txtLeasingGrid = document.getElementById("dgTable__ctl4_txtLeasingGrid");
		        //var txtLeasingGrid = document.getElementsByClassName("txtLeasingGrid")[0];
		        //txtLeasingGrid.value = selectedLaesing.name;
		        ////var hfLeasingGrid = document.getElementById("dgTable__ctl4_hfLeasingGrid");
		        //var hfLeasingGrid = document.getElementsByClassName("hfLeasingGrid")[0];
		        //hfLeasingGrid.value = selectedLaesing.id;

		        //console.log(objToString(selectedLaesing))
		        var selectbox = getElement("input","txtLeasingGrid")
		        var selectboxhide = getElement("input", "hfLeasingGrid")		       
		        
		        if (selectbox != undefined) {
		            selectbox.value = selectedLaesing.name;
		        }
		        if (selectboxhide != undefined) {
		            selectboxhide.value = selectedLaesing.id
		        }

		    }


		    function ShowPPLeasingSelectionEdit() {
		       // console.log("fgdfgdfg : "+getElement("input", "hfLeasingEditGrid").value)
		        
		        var paramLeasing = getElement("input", "hfLeasingEditGrid")

		        if (paramLeasing.value != "")
		            showPopUp('../General/../Benefit/PopUpLeasing.aspx?select=' + paramLeasing.value, '', 500, 760, LeasingSelectionEdit);
                    else
		        showPopUp('../General/../Benefit/PopUpLeasing.aspx', '', 500, 760, LeasingSelectionEdit);
		    }

		    function LeasingSelectionEdit(selectedLaesing) {
		       
		        var selectbox = getElement("input", "txtLeasingEditGrid")
		        var selectboxhide = getElement("input", "hfLeasingEditGrid")

		        if (selectbox != undefined) {
		            selectbox.value = selectedLaesing.name;
		        }
		        if (selectboxhide != undefined) {
		            selectboxhide.value = selectedLaesing.id
		        }

		    }

		    function ShowPPLeasingEditSelection() {
		        showPopUp('../General/../Benefit/PopUpLeasing.aspx', '', 500, 760, LeasingEditSelection);
		    }

		    function LeasingEditSelection(selectedLaesing) {    
		        var selectbox = getElement("input", "txtLeasingGrid")
		        var selectboxhide = getElement("input", "hfLeasingGrid")

		        if (selectbox != undefined) {
		            selectbox.value = selectedLaesing.name;
		        }
		        if (selectboxhide != undefined) {
		            selectboxhide.value = selectedLaesing.id
		        }
		    }

		    //
		    function ShowPPTypeSelection() {
		        var selectbox = getElement("select", "ddlModelGrid")		        
		        var selectboxVal;
		        if (selectbox.value == "")
		            selectboxVal = "0"
		        else
		            selectboxVal = selectbox.value

		        showPopUp('../General/../Benefit/PopUpType.aspx?model='+
                    //document.getElementById("dgTable__ctl4_ddlModelGrid").value, '', 500, 760, TypeSelection);
                    //document.getElementsByClassName("ddlModelGrid")[0].value, '', 500, 760, TypeSelection);
                    selectboxVal, '', 500, 760, TypeSelection);
		       
		    }

		    function TypeSelection(selectedType) {
		        //var txtLeasingGrid = document.getElementById("dgTable__ctl4_txtTypeGrid");
		        //var txtLeasingGrid = document.getElementsByClassName("txtTypeGrid")[0];
		        //txtLeasingGrid.value = selectedType.name;
		        ////var hfLeasingGrid = document.getElementById("dgTable__ctl4_hfTypeGrid");
		        //var hfLeasingGrid = document.getElementsByClassName("hfTypeGrid")[0];
		        //hfLeasingGrid.value = selectedType.id;


		        

		        var selectbox = getElement("input", "txtTypeGrid")
		        var selectboxhide = getElement("input", "hfTypeGrid")

		        if (selectbox != undefined) {
		            selectbox.value = selectedType.name;
		        }
		        if (selectboxhide != undefined) {
		            selectboxhide.value = selectedType.id
		        }


		    }

		    function ShowPPTypeSelectionEdit() {

		        var selectbox = getElement("select", "ddlModelEditGrid")
		        var selectboxVal;
		        if (selectbox.value == "")
		            selectboxVal = "0"
		        else
		            selectboxVal = selectbox.value

		        showPopUp('../General/../Benefit/PopUpType.aspx?model=' +              
                    selectboxVal, '', 500, 760, TypeSelectionEdit);

		    }

		    function TypeSelectionEdit(selectedType) {	       

		        var selectbox = getElement("input", "txtTypeEditGrid")
		        var selectboxhide = getElement("input", "hfTypeEditGrid")

		        if (selectbox != undefined) {
		            selectbox.value = selectedType.name;
		        }
		        if (selectboxhide != undefined) {
		            selectboxhide.value = selectedType.id
		        }
		    }


		    function findNo() {
                
		        //var selectbox = document.getElementById("dgTable__ctl4_ddlNoGrid");
		        var selectbox = document.getElementsByClassName("ddlNoGrid")[0];
		        
                
		        var table = document.getElementById('dgTable');
		        var exitsno = '';		        
		        for (i = 1; i < table.rows.length-2; i++) {
		            if (navigator.appName == "Microsoft Internet Explorer") {
		                exitsno = exitsno + table.rows[i].cells[0].innerText;
		                }		               
		            else {
		                    exitsno = exitsno + table.rows[i].cells[0].getElementsByTagName("span")[0].innerHTML;
		                }
		        }		        
		        var i;
		        for (i = selectbox.options.length - 1; i >= 0; i--) {
		            selectbox.remove(i);
		        }		        
		        var constabjad = "ABCDEFGHIHJKLMNOPQRSTUVWXYZ"
		        for (i = 0; i < constabjad.length; i++) {

		            if (exitsno.indexOf(constabjad[i]) < 0) {
		                var opt = document.createElement('option');
		                opt.value = constabjad[i];
		                opt.innerHTML = constabjad[i];
		                selectbox.appendChild(opt);
		            }		          
		        }

		        document.getElementsByClassName("ddlNoGrid")[0].removeAttribute("name");
		    }

		    function showhideLeasing() {
               
		        //var selectbox = document.getElementById("dgTable__ctl4_ddlTypeBenefitGrid");
		        //var selectbox = document.getElementsByClassName("ddlTypeBenefitGrid")[0];
		        
		        var selectbox = getElement("select", "ddlTypeBenefitGrid")
		       
		        if (selectbox != undefined) {
		            var selectboxname = selectbox.options[selectbox.selectedIndex].text

		            var result = selectbox.value.split(';');

		            if (result[3] == '1') {
		                document.getElementById("arealeasing").style.display = '';           // H
		            } else {
		                var hfLeasingsGrid= getElement("input", "hfLeasingsGrid")
		                if (hfLeasingsGrid != undefined)
		                    hfLeasingsGrid.value = '';
		                document.getElementById("arealeasing").style.display = 'none';
		            }

		            
		            var cbdiskon = getElement("input", "cbDiskonGrid")
		            if (cbdiskon != undefined) {
		                if (result[1] == '1')
		                    cbdiskon.style.display = '';
                        else
		                    cbdiskon.style.display = 'none';
		            }

		            var areaopenfaktur = getElement("div", "areaopenfaktur")
		            var areavalidatefaktur = getElement("div", "areavalidatefaktur")
		            if (areaopenfaktur != undefined && areavalidatefaktur != undefined) {
		                if (result[2] == '1'){
		                    areaopenfaktur.style.display = '';
		                    areavalidatefaktur.style.display = 'none';
		                }		                   
		                else {
		                    areaopenfaktur.style.display = 'none';
		                    areavalidatefaktur.style.display = '';
		                }
		                    
		            }

		        }
		        
		    }

		    function showhideLeasingEdit() {

		        //var selectbox = document.getElementById("dgTable__ctl4_ddlTypeBenefitGrid");
		        //var selectbox = document.getElementsByClassName("ddlTypeBenefitGrid")[0];

		        var selectbox = getElement("select", "ddlTypeBenefitEditGrid")

		        //var aaa = getElement("input", "hfLeasingEditGrid");

		        if (selectbox != undefined) {
		            var selectboxname = selectbox.options[selectbox.selectedIndex].text

		            var result = selectbox.value.split(';');

		            if (result[3] == '1') {
		                // H
		                //console.log('sadasdas')

		               

                        getElement("div", "arealeasing1").style.display = '';           // H
		            } else {
		                //console.log('sssssssssss')
		                getElement("input", "txtLeasingEditGrid").value = '';
		                getElement("input", "hfLeasingEditGrid").value = '';
		                getElement("div", "arealeasing1").style.display = 'none';
		                
		            }


		            
		            var cbdiskon = getElement("input", "cbDiskonEditGrid")
		            if (cbdiskon != undefined) {
		                if (result[1] == '1')
		                    cbdiskon.style.display = '';
		                else
		                    cbdiskon.style.display = 'none';
		            }
		            var areaopenfaktur = getElement("div", "areaeditopenfaktur")
		            var areavalidatefaktur = getElement("div", "areaeditvalidatefaktur")
		            if (areaopenfaktur != undefined && areavalidatefaktur != undefined) {
		                if (result[2] == '1'){
		                    areaopenfaktur.style.display = '';
                            areavalidatefaktur.style.display = 'none';
                        }		                   
                        else {
                            areaopenfaktur.style.display = 'none';
                            areavalidatefaktur.style.display = '';
                        }
		            }
		        }

		    }

		    function showhideModelType() {
		        var selectbox = getElement("input", "hfTypeGrid")
		        var selectboxhide = getElement("input", "txtTypeGrid")

		        if (selectbox != undefined) {
		            selectbox.value = ""
		        }
		        if (selectboxhide != undefined) {
		            selectboxhide.value = ""
		        }
		    }

		    function showhideModelTypeEdit() {
		        var selectbox = getElement("input", "hfTypeEditGrid")
		        var selectboxhide = getElement("input", "txtTypeEditGrid")

		        if (selectbox != undefined) {
		            selectbox.value = ""
		        }
		        if (selectboxhide != undefined) {
		            selectboxhide.value = ""
		        }
		    }

		    setTimeout(function () { generateFormula(); showhideLeasing(); showhideLeasingEdit() }, 2000);

		    function addFormula() {  
		        var constformula = " &."
		        var multipleCharFormula = [];
		        var table = document.getElementById('dgTable');		       
		        if (table != null) {
		            for (i = 1; i < table.rows.length - 2; i++) {
		                if (navigator.appName == "Microsoft Internet Explorer") {
		                    var rows = table.rows[i].cells[0].innerText.replace(/^\s+|\s+$/g, '');
		                    if (rows != "" && rows != " ")
		                    {
                                if (rows.length == 1)
		                            constformula = constformula + rows;
                                if (rows.length > 1)
                                    multipleCharFormula.push(rows);
		                    }
		                }
		                else {
		                    var rows = table.rows[i].cells[0].getElementsByTagName("span")[0].innerHTML.replace(/^\s+|\s+$/g, '');
		                    if (rows != "" && rows != " ")
		                    {

		                        if (rows.length == 1)
		                            constformula = constformula + rows;
                                if (rows.length > 1)
                                    multipleCharFormula.push(rows);
		                    }
		                }
		            }
		            
		            var formula = document.getElementById("formula");
		            var randomid = Math.floor((Math.random() * 10000) + 1);
		            var idspandelete = "r" + randomid
		            var createSpan = document.createElement("span");
		            createSpan.setAttribute("id", "parent_" + idspandelete);
		            var createSpanDelete = document.createElement("span");
		            createSpanDelete.innerHTML = "-"
		            //createSpanDelete.setAttribute("onclick", "deleteformula('" + idspandelete + "')");		            
		            createSpanDelete.onclick = function () { deleteformula( idspandelete ); };
		            createSpanDelete.setAttribute("id", idspandelete);
		            //createSpanDelete.click = deleteformula()

		            var randomid = "wwww";
		            var createSelect = document.createElement("select");
		            //createSelect.setAttribute("onchange", "showformula()");
		            //createSelect.setAttribute("onkeydown", "showformula()");
		            createSelect.onchange = function () { showformula(); };
		            createSelect.onkeydown = function () { showformula(); };
		            createSelect.setAttribute("name", "r" + randomid);
		            
		            //var option = document.createElement("option");
		            for (var i = 0; i < constformula.length; i++) {
		                var option = document.createElement("option");
		                

		                option.label = constformula.charAt(i);
		                option.value = constformula.charAt(i);
		                if (typeof (option.innerText) != 'undefined') {
		                    option.innerText = constformula.charAt(i);
		                }
		                else {
		                    option.text = constformula.charAt(i);
		                }

		                createSelect.appendChild(option);
                        
		            }

		            for (var i = 0; i < multipleCharFormula.length; i++)
		            {
		                var option = document.createElement("option");

		                option.label = multipleCharFormula[i];
		                option.value = multipleCharFormula[i];
		                if (typeof (option.innerText) != 'undefined') {
		                    option.innerText = multipleCharFormula[i];
		                }
		                else {
		                    option.text = multipleCharFormula[i];
		                }

		                createSelect.appendChild(option);
		            }

		            //createSelect.appendChild(option);

		            
		            createSpan.appendChild(createSelect);
		            createSpan.appendChild(createSpanDelete);
		            formula.appendChild(createSpan);
		            //console.log(objToString(formula));
		        }
		      
		       
		    }

		    function removeElement(element) {
		        // element && element.parentNode && element.parentNode.removeChild(element);
		        element.parentNode.removeChild(element);
		        // element.parentNode.removeChild(element);
		    }

		    function deleteformula(id) {
		        var elementt = document.getElementById("parent_" + id)
		        elementt.innerHTML = ""
		        elementt.innerText = ""
		        //removeElement(elementt);
		       // generateFormula()
		       // alert(elementt)
		        showformula()
		        generateFormula()
		    }

		    function isLetter(c) {
		        return c.toLowerCase() != c.toUpperCase();
		    }

		    function generateFormula() {
		        var hffrmula = document.getElementById("hfformula").value
		        var arrHffrmula = hffrmula.split(';');
		        var formula = document.getElementById("formula");
		        formula.innerHTML = ""
		        formula.innerText = ""
		        document.getElementById("areashowformula").innerHTML = arrHffrmula.join("");
		        document.getElementById("areashowformula").innerText = arrHffrmula.join("");

		        var multipleCharFormula = [];
		        var multipleHfrmula = [];

		        var akhir = 0;
		        
		        if (document.getElementById("btnSimpan") == undefined) {
		        //if (document.getElementById("ddlStatus").disabled == false) {
		            akhir = 1
		            document.getElementById('areatambahformula').remove()
		        } else {
		            var isMultiple = false;
		            akhir = 2
		            document.getElementById('areatambahformula').innerHTML = "Tambah Formula"
		            document.getElementById('areatambahformula').innerText = "Tambah Formula"
		            for (var k = 0; k < arrHffrmula.length-1; k++) {
		                var constformula = " ;&;.;"
		                var table = document.getElementById('dgTable');
		                //console.log(table.rows.length)
		                if (table != null) {
		                    //for (i = 1; i < table.rows.length - 2; i++) {
		                    for (i = 1; i < table.rows.length - akhir; i++) {
		                        if (navigator.appName == "Microsoft Internet Explorer") {
		                            var rows = table.rows[i].cells[0].innerText.replace(/^\s+|\s+$/g, '');
		                            //console.log(rows + " : "+rows.charCodeAt())
		                            if (rows != "" && rows != " ")
		                            {
		                                    constformula = constformula + rows + ";";
		                            }
		                        }
		                        else {
		                            var rows = table.rows[i].cells[0].getElementsByTagName("span")[0].innerHTML.replace(/^\s+|\s+$/g, '');
		                            if (rows != "" && rows != " ")
		                            {
		                                    constformula = constformula + rows + ";";
		                            }
		                        }
		                    }
		                   
		                    var randomid = Math.floor((Math.random() * 10000) + 1);
		                    var idspandelete = "r" + randomid
		                    var createSpan = document.createElement("span");
		                    createSpan.setAttribute("id", "parent_" + idspandelete);
		                    var createSpanDelete = document.createElement("span");
		                    createSpanDelete.innerHTML = "-"
		                    //createSpanDelete.setAttribute("onclick", "deleteformula('" + idspandelete + "')")
		                    createSpanDelete.onclick = function () { deleteformula(idspandelete); };
		                    createSpanDelete.setAttribute("id", idspandelete);
		                    //createSpanDelete.click = deleteformula()

		                    var randomid = "wwww";
		                    var createSelect = document.createElement("select");
		                    createSelect.setAttribute("name", "r" + randomid);
		                    //createSelect.setAttribute("onchange", "showformula()");
		                    //createSelect.setAttribute("onkeydown", "showformula()");
		                    createSelect.onchange = function () { showformula(); };
		                    createSelect.onkeydown = function () { showformula(); };
		                    //createSelect.setAttribute("id", "r" + randomid);
		                    var arrConstFormula = constformula.split(';');
		                    var selectetd = 0;
		                    for (var i = 0; i < arrConstFormula.length - 1; i++) {
		                        var option = document.createElement("option");
		                        option.label = arrConstFormula[i];
		                        option.value = arrConstFormula[i];
		                        if (typeof (option.innerText) != 'undefined') {
		                            option.innerText = arrConstFormula[i];
		                        }
		                        else {
		                            option.text = arrConstFormula[i];
		                        }

		                        createSelect.appendChild(option);

		                        if ((arrHffrmula[k] == arrConstFormula[i])) selectetd = i
		                    }

		                    createSpan.appendChild(createSelect);
		                    createSpan.appendChild(createSpanDelete);
		                    formula.appendChild(createSpan);
		                    document.getElementById("parent_" + idspandelete).getElementsByTagName("option")[selectetd].selected = 'selected'
		                }
		            }
		        }
		    }

		    function showformula() {
		        var formula = document.getElementById("formula").getElementsByTagName("select");
                
                var aa = ""
                var hiddenAaa = "";
                for (var i = 0; i < formula.length; i++) {
                    if (formula[i].value != ""){
                        aa += formula[i].value
                        hiddenAaa += formula[i].value + ";";
                    }
		        }
		        // console.log("aaa "+aa)
                document.getElementById("hfformula").value = hiddenAaa
		        document.getElementById("areashowformula").innerHTML = aa
		        document.getElementById("areashowformula").innerText = aa
		        document.getElementById("HiddenFieldFormula").value = hiddenAaa;
		    }

		   
		</script>
        <style>
           
            .hiddencol
              {
                display: none;
              }
        </style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
        <asp:HiddenField ID="hdnBenefitMasterID" runat="server" />
			<TABLE id="Table2" cellSpacing="5" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px" colSpan="2">SALES CAMPAIGN - Input Benefit </td>
				</tr>
                <tr>
					<td background="../images/bg_hor.gif" colSpan="2" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" colSpan="2" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
                <tr>
					<td colSpan="2">
                        <table>
                            <tr>
					            <td class="titleField" width="20%">Referensi Benefit&nbsp;</td>
					            <td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtRefBenefit" onblur="omitSomeCharacter('txtRefBenefit','<>?*%$;');btnReloadClick()"
							            runat="server" Width="242px"></asp:textbox>
                                    &nbsp;<asp:label id="lblPopUpRefBenefit" runat="server" width="16px">
							        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label>
                                      <asp:Label ID="Label2" runat="server" ></asp:Label>
                                    <asp:button id="btnReload" runat="server" Text="..." Style="display:none" width="60px"></asp:button>&nbsp;
					            </td>
                                <td class="titleField" width="20%">&nbsp;&nbsp;&nbsp;&nbsp;Benefit Reg No&nbsp;</td>
					            <td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtRegNo" onblur="omitSomeCharacter('txtRegNo','<>?*%$;')"
							        Enabled="false"    runat="server" Width="242px" ></asp:textbox>                                   
					            </td>
				            </tr>
				           
                            <tr>
					            <td class="titleField" width="20%">Dealer&nbsp;</td>
					            <td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')"
							            runat="server" Width="242px" ></asp:textbox>
                                    &nbsp;<asp:label id="lblPopUpDealer" runat="server" width="16px">
							        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label>
                                      <asp:Label ID="lblDelerSession" runat="server" ></asp:Label>
					            </td>
                                <td class="titleField" width="20%">&nbsp;&nbsp;&nbsp;&nbsp;Remark&nbsp;</td>
					            <td><asp:textbox MaxLength="100" onKeyUp="javascript:Count(this);" onChange="javascript:Count(this);" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtRemark" onblur="omitSomeCharacter('txtRemark','<>?*%$;')"
							            runat="server" Width="242px" TextMode="MultiLine"></asp:textbox>                                                                       
					            </td>
				            </tr>
				           
                            <tr>
					            <td class="titleField" width="20%">No Surat&nbsp;</td>
					            <td><asp:textbox MaxLength="40" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtNoSurat" onblur="omitSomeCharacter('txtNoSurat','<>?*%$;')"
							            runat="server" Width="242px"></asp:textbox>                                    
					            </td>
                                <td colspan="2"></td>
				            </tr>

                             <tr>
					            <td class="titleField" width="20%">Status&nbsp;</td>
					            <td colspan=3>
                                    <%--<asp:DropDownList ID="ddlStatus" runat="server">                                        
                                        <asp:ListItem Value="0" Text="Aktif"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Tidak Aktif"></asp:ListItem>
                                    </asp:DropDownList> --%>  
                                    <asp:DropDownList ID="ddlStatus" runat="server">                                                                               
                                    </asp:DropDownList>                   
					            </td>
                               
				             </tr>
				          


                        </table>

					</td>
				</tr>


				
							
				<tr>
					<td class="titleField" width="20%">&nbsp;</td>
					<td>                        
                        <asp:HiddenField ID="hfformula" runat="server" />
                        <asp:button id="btnSimpan" runat="server" Text="Simpan" width="60px"></asp:button>&nbsp;
						<asp:button id="btnBatal" runat="server" Text="Batal" width="60px" CausesValidation="False"></asp:button>
                        &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:button id="btnDelete"  runat="server" Text="Hapus" width="60px" CausesValidation="False"></asp:button>

					</td>
				</tr>
				<TR>
					<TD colSpan="2">
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; max-height: 440px">
                                        <asp:datagrid id="dgTable" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
											AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px"
											CellPadding="3" DataKeyField="ID"
                                            
                                            >
                                             <SelectedItemStyle Font-Bold="True" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableSales" ></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														 <asp:Label ID="lblNoGrid" Runat="server"></asp:Label>
													</ItemTemplate>
                                                    
                                                    <FooterStyle Font-Size="Small"></FooterStyle>
                                                    <FooterTemplate>
														<asp:DropDownList id="ddlNoGrid"  runat="server" class="ddlNoGrid"  CausesValidation="False"  ></asp:DropDownList>
													</FooterTemplate>
                                                     <EditItemTemplate>
														<asp:Label ID="lblNoEditGrid" Runat="server"></asp:Label>
													</EditItemTemplate>
												</asp:TemplateColumn>												
												<asp:TemplateColumn HeaderText="Tipe Benefit">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                    
													<ItemTemplate>
                                                        <asp:Label ID="lblTypeBenefitGrid" Runat="server"  class="lblTypeBenefitGrid"></asp:Label>   
													
													</ItemTemplate>
                                                    
                                                    <FooterTemplate>
														<asp:DropDownList id="ddlTypeBenefitGrid" class="ddlTypeBenefitGrid" runat="server"></asp:DropDownList>
													</FooterTemplate>
                                                     <EditItemTemplate>
														
                                                         <asp:DropDownList id="ddlTypeBenefitEditGrid" class="ddlTypeBenefitGrid" runat="server"></asp:DropDownList>
                                                       
													</EditItemTemplate>
                                                   
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Leasing ">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
                                                        <asp:Label ID="lblLeasingGrid" Runat="server"></asp:Label>   
													</ItemTemplate>
                                                    <FooterTemplate>
                                                        <div id="arealeasing" style="display:none">
                                                            <div style="display:none"><asp:TextBox id="hfLeasingGrid" class="hfLeasingGrid" runat="server" Width="100" ></asp:TextBox></div>
                                                           

                                                              <table>
                                                            <tr>
                                                                <td><asp:TextBox id="txtLeasingGrid" class="txtLeasingGrid" runat="server" Width="100" ></asp:TextBox></td>
                                                                <td>
                                                                     <asp:Label id="lblLeasingGrid" runat="server" style="width:10px">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>


														    
														
														   
                                                        </div>                                                        
													</FooterTemplate>
                                                     <EditItemTemplate>
                                                         <asp:Panel ID="arealeasing1" runat="server" >
                                                          
                                                              
                                                            <div style="display:none"><asp:TextBox id="hfLeasingEditGrid" class="hfLeasingGrid" runat="server" Width="100" ></asp:TextBox></div>
                                                           

                                                               <table>
                                                            <tr>
                                                                <td>    <asp:TextBox id="txtLeasingEditGrid" class="txtLeasingEditGrid" runat="server" Width="100" ></asp:TextBox></td>
                                                                <td>
                                                                    <asp:Label id="lblLeasingEditGrid" runat="server">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                                        

                                                                </td>
                                                            </tr>
                                                        </table>

														
														
														   

                                                         </asp:Panel> 
														
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deskripsi">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblDescriptionGrid" Runat="server"></asp:Label>   
													</ItemTemplate>
                                                    <FooterTemplate>
														<asp:textbox onkeypress="return alphaNumericExcept(event,'?*%$;')" id="txtDescriptionGrid" onblur="omitSomeCharacter('txtDescriptionGrid','?*%$;')"
							                                    runat="server" ></asp:textbox>
													</FooterTemplate>
                                                    <EditItemTemplate>
														<asp:textbox onkeypress="return alphaNumericExcept(event,'?*%$;')" id="txtDescriptionEditGrid" onblur="omitSomeCharacter('txtDescriptionEditGrid','?*%$;')"
							                                    runat="server" ></asp:textbox>
													</EditItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Nilai">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" />
													<ItemTemplate>
														<asp:Label id="lblAmountGrid" runat="server" >
														</asp:Label>
													</ItemTemplate>
                                                    
                                                    <FooterTemplate>
														<asp:TextBox id="txtAmountGrid" runat="server" Width="60px" CssClass="textRight" onkeypress="return numericOnlyUniv(event)"
															onkeyup="pic(this,this.value,'9999999999','N')" MaxLength="11"></asp:TextBox>
														
													</FooterTemplate>
                                                     <EditItemTemplate>
														<asp:TextBox id="txtAmountEditGrid" runat="server" Width="60px" CssClass="textRight" onkeypress="return numericOnlyUniv(event)"
															onkeyup="pic(this,this.value,'9999999999','N')" MaxLength="11"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Periode Validasi Faktur">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblPeriodeGrid" runat="server" >
														</asp:Label>
													</ItemTemplate>
                                                     
                                                    <FooterTemplate>
                                                         <div id="areavalidatefaktur" style="display:none">
														<cc1:inticalendar id="clDariGrid" runat="server" TextBoxWidth="60"></cc1:inticalendar>
                                                        s/d
                                                        <cc1:inticalendar id="clSampaiGrid" runat="server" TextBoxWidth="60"></cc1:inticalendar>
														</div>
													</FooterTemplate>
                                                    <EditItemTemplate>
                                                         <div id="areaeditvalidatefaktur" style="display:none">
														<cc1:inticalendar id="clDariEditGrid" runat="server" TextBoxWidth="60"></cc1:inticalendar>
                                                        s/d
                                                        <cc1:inticalendar id="clSampaiEditGrid" runat="server" TextBoxWidth="60"></cc1:inticalendar>
                                                             </div>
													</EditItemTemplate>
												</asp:TemplateColumn>


                                                <asp:TemplateColumn HeaderText="Periode Open Faktur">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblOpenGrid" runat="server" >
														</asp:Label>
													</ItemTemplate>
                                                     
                                                    <FooterTemplate>
                                                        <div id="areaopenfaktur" style="display:none">
														<cc1:inticalendar id="clDariOpenGrid" runat="server" TextBoxWidth="60"></cc1:inticalendar>
                                                        s/d
                                                        <cc1:inticalendar id="clSampaiOpenGrid" runat="server" TextBoxWidth="60"></cc1:inticalendar>
														 </div>	
													</FooterTemplate>
                                                    <EditItemTemplate>
                                                         <div id="areaeditopenfaktur"  style="display:none">
                                                            <cc1:inticalendar id="clDariOpenEditGrid" runat="server" TextBoxWidth="60"></cc1:inticalendar>
                                                            s/d
                                                            <cc1:inticalendar id="clSampaiOpenEditGrid" runat="server" TextBoxWidth="60"></cc1:inticalendar>
                                                       			</div>										
													</EditItemTemplate>
												</asp:TemplateColumn>



                                                <asp:TemplateColumn HeaderText="Model Kendaraan">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblModelGrid" runat="server" >
														</asp:Label>
													</ItemTemplate>
                                                    
                                                    <FooterTemplate>
														<asp:DropDownList ID="ddlModelGrid" runat="server" class="ddlModelGrid">                                        
                                                            
                                                        </asp:DropDownList>  
													</FooterTemplate>
                                                    <EditItemTemplate>														
                                                       <asp:DropDownList ID="ddlModelEditGrid" runat="server" class="ddlModelGrid">                                        
                                                            
                                                        </asp:DropDownList>  
													</EditItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Type Kendaraan">
													<HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
                                                        <asp:Label id="lblTypeGrid" runat="server" >
														</asp:Label>
													</ItemTemplate>
                                                    
                                                    <FooterTemplate>
                                                        <div style="display:none">
                                                            <asp:TextBox id="hfTypeGrid" class="hfTypeGrid" runat="server" Width="50" ></asp:TextBox></div>
                                                           
                                                        <table>
                                                            <tr>
                                                                <td><asp:TextBox id="txtTypeGrid"  class="txtTypeGrid" runat="server" Width="50"></asp:TextBox></td>
                                                                <td>
                                                                    <asp:Label id="lblTypeGrid" runat="server">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
														
														
														
													</FooterTemplate>
                                                     <EditItemTemplate>
                                                      <div style="display:none">
                                                            <asp:TextBox id="hfTypeEditGrid" class="hfTypeGrid" runat="server" Width="50" ></asp:TextBox></div>
                                                           
                                                        
                                                         <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox id="txtTypeEditGrid"  class="txtTypeGrid" runat="server" Width="100"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    
														<asp:Label id="lblTypeEditGrid" runat="server">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>

														
														
                                                           
														
													</EditItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Tahun Perakitan">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" />
													<ItemTemplate>
                                                        <asp:Label id="lblAssyGrid" runat="server" >
														</asp:Label>                                                     
													</ItemTemplate>
                                                    
                                                    <FooterTemplate>
														<asp:TextBox id="txtAssyGrid" runat="server"  Width="50" CssClass="textRight" onkeypress="return numericOnlyUniv(event)"
															 MaxLength="4"></asp:TextBox>
														
													</FooterTemplate>
                                                     <EditItemTemplate>
														<asp:TextBox id="txtAssyEditGrid" runat="server"  Width="50" CssClass="textRight" onkeypress="return numericOnlyUniv(event)"
															 MaxLength="4"></asp:TextBox>
													</EditItemTemplate>
                                                    
												</asp:TemplateColumn>
                                                 <asp:TemplateColumn HeaderText="Maks. Pengajuan">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right" />
													<ItemTemplate>
                                                         <asp:Label id="lblMaxGrid" runat="server" >
														</asp:Label>                                                                  
													</ItemTemplate>
                                                   
                                                      <FooterTemplate>
														<asp:TextBox id="txtMaxGrid" runat="server" Width="50" CssClass="textRight" onkeypress="return numericOnlyUniv(event)"
															onkeyup="pic(this,this.value,'9999999999','N')" MaxLength="11"></asp:TextBox>
														
													</FooterTemplate>
                                                     <EditItemTemplate>
														<asp:TextBox id="txtMaxEditGrid" runat="server" Width="50" CssClass="textRight" onkeypress="return numericOnlyUniv(event)"
															onkeyup="pic(this,this.value,'9999999999','N')" MaxLength="11"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="WS Diskon">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center" />
													<ItemTemplate>
                                                     
                                                        <asp:Label id="lblDiskonGrid" runat="server" >
														</asp:Label>                                                                                                                       
													</ItemTemplate>
                                                   <FooterTemplate>														
                                                       <asp:CheckBox ID="cbDiskonGrid" runat="server"  />
													</FooterTemplate>
                                                     <EditItemTemplate>
                                                         <asp:CheckBox ID="cbDiskonEditGrid" runat="server" />
														
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle  CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>														
														
														<asp:LinkButton id="lnkbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' >
															<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
														<asp:LinkButton ID="lnkbtnDelete" Runat = "server" Width = "20px" Text = "Hapus" CausesValidation = "False" CommandName ="Delete" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
															<img alt="Delete" onclick="return confirm('Yakin ingin menghapus data ini?');" src="../images/trash.gif"
																border="0"></asp:LinkButton>
                                                        <asp:LinkButton id="lbtnCopy" runat="server" CausesValidation="False" CommandName="AddCopy" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
															<img src="../images/simpan.gif" border="0" alt="Copy"></asp:LinkButton>
													</ItemTemplate>
                                                    <FooterTemplate>
														<asp:LinkButton id="Linkbutton1" runat="server" CausesValidation="true" CommandName="Add">
															<img src="../images/add.gif" border="0" alt="Tambah">
														</asp:LinkButton>
													</FooterTemplate>
                                                    <EditItemTemplate>
														<asp:LinkButton id="lbtnSave" tabIndex="40" Runat="server" CausesValidation="True" CommandName="Update"
															text="Simpan">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
														<asp:LinkButton id="lbtnCancel" tabIndex="50" Runat="server" CausesValidation="True" CommandName="Cancel"
															text="Batal">
															<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
													</EditItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>


                <TR>
					<TD colSpan="2">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<thead>
                                <tr>
                                    <th>
                                        Formula
                                    </th>
                                </tr>

							</thead>
                            <tbody>
                                <TR>
								    <TD vAlign="top" colSpan="6">
                                        <div >
                                          <a>
                                            <span id="areatambahformula" onclick="addFormula()" style="cursor:pointer"></span>
                                            </a>
                                        </div>
                                        

                                        <asp:Panel ID="formula" runat="server">
                                            
                                           
                                        </asp:Panel>
                                        Formula : <div id="areashowformula" ></div>
                                        <asp:HiddenField ID="HiddenFieldFormula" runat="server" />
                                    </TD>
							    </TR>
                            </tbody>
                            
						</TABLE>
					</TD>
				</TR>


			</TABLE>
		</form>
	</body>
</HTML>
