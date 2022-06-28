<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputClaim.aspx.vb" Inherits="FrmInputClaim" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmAplikasiHeader</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        var idDetail = "";

        function ResetHdnREvent()
        {
            var _txtRegEvent = document.getElementById('txtRegEvent');

            if (_txtRegEvent.value == "")
            {
                document.getElementById('hfIIdEvent').value = "";
                //getElement("input", "hfIIdEvent").value = value

                //getElement("input", "txtRegEvent").value = valueshow
            }
          
        }
        function ShowRecomendation(id, noreco) {
            // showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
            //txtIdDetailMaster
            if (getElement('input', 'txtIdDetailMaster') != undefined) {
                //alert('id : ' + id)
                //console.log(getElement('input', 'txtIdDetailMaster').value);

                var selectPilihanClaim = getElement('select', 'ddlPilihanClaim');
                var selectLeasing = getElement('select', 'ddlLeasing');
                var result = selectPilihanClaim.value.split(';');
                if (result[1] == '1') {
                    if (selectLeasing.value == '1') {
                        alert('Tidak dapat melakukan Cetak untuk Leasing ini.');
                        return;
                    }
                }
                else {
                    alert('Tidak dapat melakukan Cetak untuk Leasing ini.');
                    return;
                }

                showPopUp('../General/../Benefit/frmSuratRekomendasi.aspx?idmasterdetil=' + getElement('input', 'txtIdDetailMaster').value +
                    '&idchassis=' + id + '&norecom=' + noreco, '', 1000, 1000, null);
            }
        }

        function ShowRecomendation(id, noreco,BCID) {
            // showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
            //txtIdDetailMaster
            if (getElement('input', 'txtIdDetailMaster') != undefined)
            {
                //alert('id : ' + id)
                //console.log(getElement('input', 'txtIdDetailMaster').value);

                var selectPilihanClaim = getElement('select', 'ddlPilihanClaim');
                var selectLeasing = getElement('select', 'ddlLeasing');
                var result = selectPilihanClaim.value.split(';');
                if (result[1] == '1') {
                    if (selectLeasing.value == '1') {
                        alert('Tidak dapat melakukan Cetak untuk Leasing ini.');
                        return;
                    }
                }
                else {
                    alert('Tidak dapat melakukan Cetak untuk Leasing ini.');
                    return;
                }

                showPopUp('../General/../Benefit/frmSuratRekomendasi.aspx?idmasterdetil=' + getElement('input', 'txtIdDetailMaster').value +
                '&idchassis=' + id + '&norecom=' + noreco + '&bcid=' + BCID, '', 1000, 1000, null);
            }
        }


        function ShowPPDealerSelection() {
            //showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
            showPopUp('../General/../Benefit/PopUpDealerSelectionBenefit.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }

        function getElement(tipeElement, IdElement) {
            var selectbox;
            var inputs = document.getElementsByTagName(tipeElement);

            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].id.indexOf(IdElement) > -1) {
                    selectbox = inputs[i]
                    break;
                }
            }
            return selectbox;
        }
        function closepanel2() {
            showhideLeasing()
            document.getElementById('cbRefClaim').checked = '';
            document.getElementById('Panel2').style.display = 'none'
            document.getElementById('Panel3').style.display = 'none'
            document.getElementById('Panel4').style.display = 'none'
            document.getElementById('Panel1').style.display = ''

            document.getElementById('Panel8').style.display = 'none'

        }

        function GetSelectedValue() {
            var table;
            var bcheck = false;
            table = document.getElementById('dgGridDetil');
            var val = '';
            var value = ""; var valueshow = ""; var valueformula = ""
            for (i = 1; i < table.rows.length; i++) {

                var radioBtn = table.rows[i].cells[0].getElementsByTagName('INPUT')[0];
                if (radioBtn != null && radioBtn.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        value = table.rows[i].cells[1].innerText;
                        valueshow = table.rows[i].cells[4].innerText;
                        valueformula = table.rows[i].cells[5].innerText;
                        bcheck = true;
                    }
                    else {
                        value = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        valueshow = replace(table.rows[i].cells[4].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        valueformula = replace(table.rows[i].cells[5].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        bcheck = true;
                    }
                    break;
                }
            }

            if (bcheck) {
                getElement('input', 'tempDdlPilihanClaim').value = getElement('select', 'ddlPilihanClaim').value


                tempPilihanClaim = getElement('select', 'ddlPilihanClaim').value

                document.getElementById('txtIdDetailMaster').value = value
                document.getElementById('txtIdDetailMasterShow').value = valueshow
                document.getElementById('txtFormula').value = valueformula


                if (value != "")
                    getElement("a", "Linkbutton1").style.display = '';

                if (idDetail != value) {
                    var table1 = document.getElementById('dgTable');
                    if (table1.rows.length > 3) {
                        for (i = 1; i < table1.rows.length - 1; i++) {

                            table1.deleteRow(i);
                        }
                    }

                    var table2 = document.getElementById('dgNoRangka');

                    if (table2.rows.length > 2) {
                        //for (i = 1; i < table2.rows.length; i++) {
                        //    console.log(table2.rows[1].cells[3].getElementsByTagName("span")[0].innerHTML)
                        //    //table2.deleteRow(i);
                        //    table2.deleteRow(1);
                        //}
                        for (i = table2.rows.length - 2; i > 0; i--) {
                            //console.log(table2.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML)
                            //table2.deleteRow(i);
                            table2.deleteRow(i);
                        }

                        //  console.log(table2.rows[1].cells[0]);
                        if (navigator.appName == "Microsoft Internet Explorer") {
                            table2.rows[1].cells[0].innerText = '<span>1</span>'
                        }
                        else {
                            table2.rows[1].cells[0].innerHTML = '<span>1</span>'
                        }
                        // <span>1</span>

                    }

                    getElement("span", "lblotal").innerHTML = ""
                    getElement("span", "lblotal").innerText = ""

                    getElement("span", "lblNilaiClaim").innerHTML = ""
                    getElement("span", "lblNilaiClaim").innerText = ""

                    getElement("span", "lblPpn").innerHTML = ""
                    getElement("span", "lblPpn").innerText = ""

                    getElement("span", "lblPph").innerHTML = ""
                    getElement("span", "lblPph").innerText = ""


                    idDetail = value;
                }


                closepanel2()
            }
            else {
                alert("Silahkan pilih ");
            }
        }

        function GetSelectedValue1() {
            var table;
            var bcheck = false;
            table = document.getElementById('dgNoRangka');
            var val = '';
            var value = ""; var valueshow = ""
            for (i = 1; i < table.rows.length; i++) {

                var radioBtn = table.rows[i].cells[0].getElementsByTagName('INPUT')[0];
                if (radioBtn != null && radioBtn.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        value = table.rows[i].cells[1].innerText;
                        valueshow = table.rows[i].cells[3].innerText + " ; " + table.rows[i].cells[4].innerText;
                        //valueshow = table.rows[i].cells[3].innerText;

                        bcheck = true;
                    }
                    else {
                        value = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        valueshow = replace(table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        + " ; " + replace(table.rows[i].cells[4].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        bcheck = true;
                    }
                    break;
                }
            }

            if (bcheck) {

                getElement("input", "hfNoRangkaGrid").value = value

                getElement("input", "txtNoRangkaGrid").value = valueshow

                if (value != "")
                    getElement("a", "Linkbutton1").style.display = '';
                closepanel3()
            }
            else {
                alert("Silahkan pilih ");
            }
        }

        function GetSelectedValue2() {
            var table;
            var bcheck = false;
            table = document.getElementById('dgEvent');
            var val = '';
            var value = ""; var valueshow = ""
            for (i = 1; i < table.rows.length; i++) {

                var radioBtn = table.rows[i].cells[0].getElementsByTagName('INPUT')[0];
                if (radioBtn != null && radioBtn.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        value = table.rows[i].cells[1].innerText;
                        valueshow = table.rows[i].cells[3].innerText + " ; " + table.rows[i].cells[4].innerText;

                        bcheck = true;
                    }
                    else {
                        value = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        valueshow = replace(table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML, ' ', '')
                        + " ; " + replace(table.rows[i].cells[4].getElementsByTagName("span")[0].innerHTML, ' ', '')
                        bcheck = true;
                    }
                    break;
                }
            }

            if (bcheck) {

                getElement("input", "hfIIdEvent").value = value;

                getElement("input", "txtRegEvent").value = valueshow;


                closepanel3()
            }
            else {
                alert("Silahkan pilih ");
            }
        }

        function ShowNoRangkaGrid() {



            if (getElement("input", "txtIdDetailMaster").value == "") {
                alert("Silakan pilih Referensi Claim terlebih dahulu")
                return
            }


            showhideLeasing()


            document.getElementById('cbNorangka').checked = 'checked'
            document.getElementById('Panel2').style.display = 'none'
            document.getElementById('Panel1').style.display = 'none'
            document.getElementById('Panel3').style.display = ''
            document.getElementById('Panel4').style.display = 'none'

            document.getElementById('Panel8').style.display = 'none'

            var table;
            var bcheck = false;
            table = document.getElementById('dgNoRangka');
            var val = '';
            var value = ""; var valueshow = "";

            for (i = 1; i < table.rows.length - 1; i++) {

                var radioBtn = table.rows[i].cells[0].getElementsByTagName('INPUT')[0];
                if (radioBtn == undefined) {
                    var radioInput = document.createElement('input');
                    radioInput.setAttribute('type', 'radio');
                    radioInput.setAttribute('name', 'rb1');
                    table.rows[i].cells[0].appendChild(radioInput)
                }
            }


        }

        function closepanel3() {
            showhideLeasing()
            document.getElementById('cbNorangka').checked = ''
            document.getElementById('Panel2').style.display = 'none'
            document.getElementById('Panel3').style.display = 'none'
            document.getElementById('Panel1').style.display = ''
            document.getElementById('Panel4').style.display = 'none'

            document.getElementById('Panel8').style.display = 'none'


        }

        function CheckAll(aspCheckBoxID) {
            var selectbox = getElement('input', 'chkAllItems')
            var inputs = document.getElementsByTagName("input");
            var stringlist = ""
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].id.indexOf(aspCheckBoxID) > -1) {
                    if (inputs[i].type == 'checkbox') {
                        if (selectbox.checked == true) {
                            inputs[i].checked = "checked"

                        }

                        else
                            inputs[i].checked = ""
                    }
                }
            }

            var table = document.getElementById('dgTable');
            var exitsno = '';
            for (i = 1; i < table.rows.length - 1; i++) {

                //stringlist = stringlist + ";" + table.rows[i].cells[0].getElementsByTagName("input")[0].checked;
                if (table.rows[i].cells[0].getElementsByTagName("input")[0] != undefined) {
                    if (table.rows[i].cells[0].getElementsByTagName("input")[0].checked == true)
                        stringlist = stringlist + ";" + i
                }


            }

            var arrayCheck = getElement('input', 'arrayCheck')
            if (selectbox.checked == true) {
                arrayCheck.value = stringlist
            } else arrayCheck.value = ""
        }


        function generateCheckBoxClick() {
            var inputs = document.getElementsByTagName("input");
            var stringlist = ""
            for (var i = 0; i < inputs.length; i++) {

                if (inputs[i].id.indexOf('cbAllGrid') > -1) {
                    if (inputs[i].type == 'checkbox') {

                        inputs[i].onclick = function () { setValueCheckBox(); };
                    }
                }
            }
        }

        function setValueCheckBox() {
            var table = document.getElementById('dgTable');
            var stringlist = '';
            for (i = 1; i < table.rows.length - 1; i++) {
                if (table.rows[i].cells[0].getElementsByTagName("input")[0] != undefined) {
                    if (table.rows[i].cells[0].getElementsByTagName("input")[0].checked == true)
                        stringlist = stringlist + ";" + i
                }


            }
            var arrayCheck = getElement('input', 'arrayCheck')

            arrayCheck.value = stringlist

        }

        var tempPilihanClaim = ''

        function showhideLeasing() {
            var selectPilihanClaim = getElement('select', 'ddlPilihanClaim');
            var selectLeasing = getElement('select', 'ddlLeasing');

            var result = selectPilihanClaim.value.split(';');

            // if (selectPilihanClaim.options[selectPilihanClaim.selectedIndex].text.toLowerCase().indexOf('leas') > -1)
            if (result[1] == "1")
                selectLeasing.style.display = '';
            else
                selectLeasing.style.display = 'none';

            if (result[2] == "1")
                getElement('a', 'lblPopUpRegEvent').style.display = '';
            else {
                getElement('a', 'lblPopUpRegEvent').style.display = 'none';
                getElement('input', 'txtRegEvent').value = '';
                getElement('input', 'hfIIdEvent').value = '';
            }





            //alert("1111 -- " + getElement('input', 'tempDdlPilihanClaim').value + " - " + selectPilihanClaim.value + " - " + document.getElementById('Panel1').style.display)
            if (getElement('input', 'tempDdlPilihanClaim').value != "" && getElement('input', 'tempDdlPilihanClaim').value != selectPilihanClaim.value && document.getElementById('Panel1').style.display == '') {

                document.getElementById('txtIdDetailMaster').value = '';
                document.getElementById('txtIdDetailMasterShow').value = document.getElementById('txtIdDetailMaster').value;
                document.getElementById('txtFormula').value = document.getElementById('txtIdDetailMaster').value;

                // alert(getElement('input', 'tempDdlPilihanClaim').value + " - " + selectPilihanClaim.value + " - " + document.getElementById('Panel1').style.display)

                var table1 = document.getElementById('dgTable');
                if (table1.rows.length > 3) {
                    for (i = 1; i < table1.rows.length - 1; i++) {

                        table1.deleteRow(i);
                    }
                }

                var table2 = document.getElementById('dgNoRangka');

                if (table2.rows.length > 2) {
                    for (i = table2.rows.length - 2; i > 0; i--) {
                        table2.deleteRow(i);
                    }

                    if (navigator.appName == "Microsoft Internet Explorer") {
                        table2.rows[1].cells[0].innerText = '<span>1</span>';
                    }
                    else {
                        table2.rows[1].cells[0].innerHTML = '<span>1</span>';
                    }

                }

                getElement("span", "lblotal").innerHTML = "";
                getElement("span", "lblotal").innerText = "";

                getElement("span", "lblNilaiClaim").innerHTML = "";
                getElement("span", "lblNilaiClaim").innerText = "";

                getElement("span", "lblPpn").innerHTML = "";
                getElement("span", "lblPpn").innerText = "";

                getElement("span", "lblPph").innerHTML = "";
                getElement("span", "lblPph").innerText = "";


                getElement('input', 'tempDdlPilihanClaim').value = selectPilihanClaim.value;
            }







        }

        setTimeout(function () {
            generateCheckBoxClick();

            if (getElement('input', 'btnSimpan') != undefined) {

                getElement('input', 'chkAllItems').style.display = 'none';
            } else {
                if (getElement('input', 'btnDelete') != undefined)
                    getElement('input', 'chkAllItems').style.display = 'none';
                else
                    getElement('input', 'chkAllItems').style.display = '';
            }
            showhideLeasing();
            try {
                getElement('input', 'txtNoRangkaGrid').disabled = true;
            } catch (e) {

            }

        }, 2000);


        function closepanel8() {

            document.getElementById('Panel2').style.display = 'none'
            document.getElementById('Panel3').style.display = 'none'
            document.getElementById('Panel1').style.display = ''
            document.getElementById('Panel5').style.display = ''
            document.getElementById('Panel4').style.display = 'none'

            document.getElementById('Panel8').style.display = 'none'


        }

        function ShowPPMMKSINotesSelection() {
            var BCHID = document.getElementById("hdnBenefitClaimHeaderID");

            showPopUp('../Benefit/../PopUp/PopUpMMKSINotesClaim.aspx?ID=' + BCHID.value, '', 300, 560, MMKSINotesSelection);
        }

        function MMKSINotesSelection(selectedMMKSINotes) {
            var data = selectedMMKSINotes.split(";");
            var txtMMKSINotes = document.getElementById("txtMMKSINotes");
            txtMMKSINotes.value = data[0]

            var btnReload = document.getElementById("btnReload");
            btnReload.click();

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtMMKSINotes.focus();
                txtMMKSINotes.blur();
            }
            else {
                txtMMKSINotes.onchange();
            }
        }

    </script>
    <style>
        #areahidden1a {
            /* Fallback for web browsers that don't support RGBa */
            background-color: rgb(0, 0, 0);
            /* RGBa with 0.6 opacity */
            background-color: rgba(0, 0, 0, 0.6);
            /* For IE 5.5 - 7*/
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr=#99000000, endColorstr=#99000000);
            /* For IE 8*/
            -ms-filter: "progid:DXImageTransform.Microsoft.gradient(startColorstr=#99000000, endColorstr=#99000000)";
        }

        .hiddencol {
            display: none;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <asp:Panel ID="Panel1" runat="server">

        <asp:HiddenField ID="hdnBenefitClaimHeaderID" runat="server" />

            <table id="Table2" cellspacing="5" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage" style="height: 17px" colspan="2">SALES CAMPAIGN - Input Claim</td>
                </tr>
                <tr>
                    <td background="../images/bg_hor.gif" colspan="2" height="1">
                        <img height="1" src="../images/bg_hor.gif" border="0"></td>
                </tr>
                <tr>
                    <td style="height: 6px" colspan="2" height="6">
                        <img height="1" src="../images/dot.gif" border="0"></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table>
                            <tr valign="top">
                                <td class="titleField" width="10%">Dealer&nbsp;</td>
                                <td width=4px>:</td>
                                <td colspan="3">
                                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeDealer" onblur="omitSomeCharacter('txtKeyWords','<>?*%$;')"
                                        runat="server" Width="242px"></asp:TextBox>
                                    &nbsp;<asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
							        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                                    <asp:Label ID="lblDelerSession" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>

                            <tr valign="top">
                                <td class="titleField" width="15%">Pilihan Claim&nbsp;</td>
                                <td width=4px>:</td>
                                <td>
                                    <asp:HiddenField ID="tempDdlPilihanClaim" runat="server" />
                                    <asp:DropDownList ID="ddlPilihanClaim" runat="server"></asp:DropDownList>

                                    <asp:DropDownList ID="ddlLeasing" runat="server"></asp:DropDownList>
                                </td>
                                <td class="titleField" width="15%">Tanggal Claim&nbsp;</td>
                                <td width=4px>:</td>
                                <td>
                                    <cc1:inticalendar id="icClaimDate" runat="server" Enabled="false" TextBoxWidth="60"></cc1:inticalendar>

                                </td>
                            </tr>

                            <tr>
                                <td class="titleField" width="15%">No Surat&nbsp;</td>
                                <td width=4px>:</td>
                                <td width="300px">
                                    <!--   <asp:DropDownList ID="ddlReferensiClaim" runat="server">                                        
                                                            <asp:ListItem Value="" Text=""></asp:ListItem>
                                                           
                                                        </asp:DropDownList>   
                                     -->

                                    <asp:HiddenField ID="txtFormula" runat="server" />
                                    <asp:HiddenField ID="txtIdDetailMaster" runat="server" />
                                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtIdDetailMasterShow" onblur="omitSomeCharacter('txtKeyWords','<>?*%$;')"
                                        runat="server" Width="242px"></asp:TextBox>
                                    <asp:Button ID="btnRefClaim" runat="server" Text="Ref Claim" Width="60px"></asp:Button>&nbsp;
                                    <span style="display: none">
                                        <asp:CheckBox ID="cbRefClaim" runat="server" />

                                    </span>
                                </td>
                                <td class="titleField" width="15%">Nomor Claim Reg&nbsp;</td>
                                <td width=4px>:</td>
                                <td>
                                    <asp:TextBox Enabled="false" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtRegClaimNo" onblur="omitSomeCharacter('txtKeyWords','<>?*%$;')"
                                        runat="server" Width="175px"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td class="titleField" width="15%">Total Unit&nbsp;</td>
                                <td width=4px>:</td>
                                <td>
                                    <asp:Label ID="lblotal" runat="server"></asp:Label>
                                </td>
                                <td class="titleField" width="15%">No Reg Event&nbsp;
                                    <asp:HiddenField ID="hfIIdEvent" runat="server" />
                                </td>
                                <td width=4px>:</td>
                                <td>
                                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtRegEvent" onblur="omitSomeCharacter('txtKeyWords','<>?*%$;')" onchange="ResetHdnREvent();"
                                        runat="server" Width="175px"></asp:TextBox>
                                    &nbsp;
                                    <asp:LinkButton ID="lblPopUpRegEvent" runat="server" Visible="true">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                    </asp:LinkButton>

                                </td>
                            </tr>

                            <tr>
                                <td class="titleField" width="15%">Total Nilai Claim Nett&nbsp;</td>
                                <td width=4px>:</td>
                                <td align="left" >
                                    <asp:Label ID="lblNilaiClaim" runat="server"></asp:Label>
                                </td>
                                <td class="titleField" width="15%" valign="top">Keterangan MMKSI</td>
                                <td width=4px valign="top">:</td>
                                <td valign="top">
                                    <asp:TextBox ID="txtMMKSINotes" runat="server" Height="50px" TabIndex="0" ReadOnly="true"
                                        TextMode="MultiLine" Width="260px"></asp:TextBox>&nbsp;
                                    <asp:Label ID="lblPopUpMMKSINotes" runat="server" Width="16px" style="display:none">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                    </asp:Label>
                                    <asp:Button ID="btnReload" runat="server" Text="..."  style="display:none" />
                                </td>
                            </tr>

                            <tr>
                                <td class="titleField" width="15%">Total Ppn&nbsp;</td>
                                <td width=4px>:</td>
                                <td align="left">
                                    <asp:Label ID="lblPpn" runat="server"></asp:Label>
                                </td>
                                <td class="titleField" width="15%">Nomor Rangka Reduksi&nbsp;</td>
                                <td width=4px>:</td>
                                <td>
                                    <asp:Label ID="lblChassisNumberReduksi" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td class="titleField" width="15%">Total Pph&nbsp;</td>
                                <td width=4px>:</td>
                                <td align="left">
                                    <asp:Label ID="lblPph" runat="server"></asp:Label>
                                </td>
                                <td colspan="3"></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="15%">Total Reduksi&nbsp;</td>
                                <td width=4px>:</td>
                                <td align="left">
                                    <asp:Label ID="lblTotalReduksi" runat="server"></asp:Label>
                                </td>
                                <td colspan="3"></td>
                            </tr>

                            <tr>
                                <td class="titleField" width="15%">Upload File&nbsp;</td>
                                <td width=4px>:</td>
                                <td colspan="3">
                                    <asp:FileUpload ID="fileUploadExcel" runat="server" />
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" />
                                </td>

                            </tr>



                        </table>

                    </td>
                </tr>




                <tr>
                    <td class="titleField" width="15%">

                        <asp:LinkButton ID="LinkDownload" runat="server">Template  Upload Excel</asp:LinkButton>
                    </td>
                    <td>
                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" Width="60px"></asp:Button>&nbsp;
					 <asp:Button ID="btnBatal" runat="server" Text="Kembali" Width="60px"></asp:Button>&nbsp;
                        <asp:HiddenField ID="arrayCheck" runat="server" />

                        &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Hapus" Width="60px" CausesValidation="False"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="height: 11px">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tr>
                                <td valign="top" colspan="6">
                                    <div id="div1" style="overflow: auto; max-height: 440px">
                                        <asp:DataGrid ID="dgTable" runat="server" Width="100%" AllowPaging="False" AllowSorting="False"
                                            PageSize="25" AllowCustomPaging="False" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
                                            CellPadding="3" DataKeyField="ID">
                                            <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                            <ItemStyle BackColor="White"></ItemStyle>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="">
                                                    <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <HeaderTemplate>
                                                        <input id="chkAllItems" onclick="CheckAll('cbAllGrid')"
                                                            type="checkbox" style="display: none">
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbAllGrid" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="No">
                                                    <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoGrid" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Nomor Rangka">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoRangkaGrid" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div id="arealeasing">
                                                            <div style="display: none">
                                                                <asp:TextBox ID="hfNoRangkaGrid" class="hfLeasingGrid" runat="server" Width="50"></asp:TextBox>
                                                            </div>

                                                            <asp:TextBox ID="txtNoRangkaGrid" class="txtLeasingGrid" runat="server" Width="80%"></asp:TextBox>

                                                            <asp:Label ID="lblNoRangkaGrid" runat="server">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Deskripsi Kendaraan">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDecriptionGrid" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Nomor Mesin">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoMesinGrid" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Faktur Open Date">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFakturOpenGrid" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Tanggal Validasi Faktur">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValidasiFakturGrid" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Nama Customer">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCutomerGrid" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Alamat Customer">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKotaGrid" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Nilai Claim Nett">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNilaiGrid" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Max Claim">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMaxClaimGrid" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Durasi Claim">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDurasiGrid" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Status">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatusGrid" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Error">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKeterangan" runat="server"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Keterangan Dealer">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKetDealer" runat="server"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtKetDealer" runat="server" TextMode="MultiLine">

                                                        </asp:TextBox>
                                                    </FooterTemplate>

                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Keterangan MMKSI">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKetKtb" runat="server"></asp:Label>
                                                    </ItemTemplate>



                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn>
                                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="lnkbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False" CommandName="Delete">
															<img alt="Delete" onclick="return confirm('Yakin ingin menghapus data ini?');" src="../images/trash.gif"
																border="0"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnPrint" runat="server" Width="20px" Text="Detail" CausesValidation="False" CommandName="View">
															<img alt="Recomendation" src="../images/print.gif" border="0"></asp:LinkButton>

                                                        <asp:LinkButton ID="lnkKet" runat="server" Width="20px" Text="Tambah Keterangan" CausesValidation="False" CommandName="AddKet" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
															<img alt="Tambah Keterangan" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAABmJLR0QA/wD/AP+gvaeTAAAACXBIWXMAAABIAAAASABGyWs+AAAACXZwQWcAAAAQAAAAEABcxq3DAAADYUlEQVQ4y23Rf0yUBRzH8fdzz3Pcc8cB1x0nd3oKx49OqFPih4tBZTWENtBo2Mo1nQGB02hzTspVW/aPa7P1V621VnOtFlJR5lJrg02HoIDHOCAROH7KwYHHLg444J6nP1psOL//f1777PMVeMT5ejtVT+vlq2srK12qqrSKorRNb4ybEiVpvrTqVPeddo861TpJ2btlgvSoMEDq0/sy7Xbb84qqnlSjUUU2Gg1n6o68ucNhWwZYHRviGeHFQvFhIOQfjR55+/QLep2UIOsN4mMWm2QwmbR62cjjeQUHtiQYjw/MjnHlj2voJO15zcPAa5UHjwHEW2zIMRJoNGgEkalQhN8GQ4wuCox09VJ1+ig7njOsbQKezXYV7TtUvXV1YQ6IoJHjAfCOTPH9jX6GfcMMTwc490EDiaaYB+GlxelNGxTkZ58EaP75IoNeL2ZTPNY9JfhUE56edgI9Hr7+sBZzaha3fro4+E3TlcWNBsX5bsOx2uoKgMrK/bxSUc64YKJjZp1eTyeepu/YKc3z6bmzLIzfY3xoqAdgo8H2rUk1ybmFKPM+NLKBUUMaOreZwdvtiNN36b56ATnBzMTwCMY4WfVNTLRtAir2l58gMElNbT273zhFXyDMYFsLwZ7rfHv+fWTZCFot27Py8fW3C0vLy8EN4NXiwpyyl0vSA8F1Mg810DHqJzg8QIYmhDUnjd8vXeLzL7/CoNNhd9hxZ+dERZjZAPLcme8M3V/ks5uz+AN+fB3XOVFWwNGaj2BpBmV1nTVFobe/D2V5ic7b3VOSIM4CSGfqDhtKyw8cTg83E+j2c3cSPqku5aWSvdy43EiyMwVrUhKy3kjert0QH0vTL7/eUVA1AJI9xVlslUaY6/6Tg04XWccbyHoyGSU4z832NpoaG5kcG0XW6airr6dobxFzs3N/q6omAiBlZu9pbjj7HpHo2g9mp2oJCD+6vF0WuyPRGlP3VhVxFhND3gE6bnWTm5tDOBQiFPrHG4n8N6Kwy2FNfGqHbiUaZ5G3ONzaUHDeGAqHrbMPgmZBI+40yHK6NdHizEhNdqVlOB1z8wviX9da3M0tbV4A4f83vl78hCbNVYDff18bUaLYbNu0sXGxwj1vv25pfU0QVUWjKtF4UdTqXakpfR9/cSEK8C9pUFaFcR4MNwAAACV0RVh0ZGF0ZTpjcmVhdGUAMjAxMC0wMi0xMFQwMTo0NDoyNS0wNjowMA7gDBsAAAAldEVYdGRhdGU6bW9kaWZ5ADIwMDQtMDUtMjJUMDQ6MzU6MzQtMDU6MDCF1VG1AAAAAElFTkSuQmCC" border="0"></asp:LinkButton>


                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="Linkbutton1" runat="server" CausesValidation="true" CommandName="Add">
															<img src="../images/add.gif" border="0" alt="Tambah">
                                                        </asp:LinkButton>
                                                    </FooterTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="lbtnSave" TabIndex="40" runat="server" CausesValidation="True" CommandName="Update"
                                                            Text="Simpan">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnCancel" TabIndex="50" runat="server" CausesValidation="True" CommandName="Cancel"
                                                            Text="Batal">
															<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                        </asp:DataGrid>
                                    </div>

                                    <asp:Panel ID="Panel6" runat="server">
                                        <asp:Label ID="lblpanel6" runat="server" Text="" Width="100%"></asp:Label>
                                    </asp:Panel>

                                    <asp:Panel ID="Panel7" runat="server">
                                        <asp:Label ID="lblpanel7" runat="server" Text="" Width="100%"></asp:Label>
                                    </asp:Panel>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>



            </table>


        </asp:Panel>

        <asp:Panel ID="Panel2" runat="server">
            <div id="areahidden">

                <div class="titlePage">
                    Daftar Referensi
                </div>

                <div style="background-image: url(../images/bg_hor.gif)">
                    <img border="0" src="../images/bg_hor.gif">
                </div>
                <div>
                    <img height="1" border="0" src="../images/dot.gif">
                </div>
                <div>
                    <asp:DataGrid ID="dgGridDetil" runat="server" Width="100%" 
                        AllowPaging="True" AllowSorting="True" PageSize="5" AllowCustomPaging="True" 
                        AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" 
                        CellSpacing="1" BorderWidth="0px"
                        CellPadding="3" DataKeyField="ID">
                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                        <Columns>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>

                                <ItemTemplate>
                                    &nbsp;
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ID">
                                <HeaderStyle Width="0px" CssClass="titleTableSales hiddencol"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" CssClass=" hiddencol"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblIDoGridDetil" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNoGridDetil" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="No Reg Benefit">
                                <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNoRegBenefitGridDetil" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="No Surat">
                                <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblnnosuratGridDetil" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Formula">
                                <HeaderStyle Width="15%" CssClass="titleTableSales hiddencol"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" CssClass="hiddencol"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblformula" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Deskripsi">
                                <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbldeskripsiGridDetil" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>



                        </Columns>
                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>


                </div>

                <input style="width: 60px" onclick="GetSelectedValue()" type="button"
                    value="Pilih" name="btnChoose" />

                &nbsp;<input style="width: 60px" onclick="closepanel2()" type="button" value="Tutup" />
            </div>

        </asp:Panel>


        <asp:Panel ID="Panel3" runat="server">
            <div id="areahidden1">

                <div class="titlePage">
                    Pencarian Nomor Rangka
                </div>

                <div style="background-image: url(../images/bg_hor.gif)">
                    <img height="1" border="0" src="../images/bg_hor.gif">
                </div>
                <div style="height: 6px">
                    <img height="1" border="0" src="../images/dot.gif">
                </div>

                <table>




                    <tr>
                        <td>Nomor Rangka</td>
                        <td style="width: 200px" colspan="2">
                            <asp:TextBox ID="txtNoRangkaPanel3" runat="server"></asp:TextBox>



                        </td>

                    </tr>
                    <tr>
                        <td>Deskripsi Kendaraan</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtDeskripsiPanel3" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Nomor Mesin</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtNoMesinPanel3" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Customer</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtCustomerPanel3" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Tanggal Validasi/Open Faktur</td>
                        <td>
                            <asp:CheckBox ID="cbFakterPanel3" runat="server" />
                        </td>
                        <td>
                            <cc1:inticalendar id="icFakturPanel3" runat="server" TextBoxWidth="60"></cc1:inticalendar>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnCariPanel3" runat="server" Style="width: 60px" Text="Cari" />

                            <span style="display: none">

                                <asp:CheckBox ID="cbNorangka" runat="server" />
                            </span>

                            <input style="width: 60px" onclick="GetSelectedValue1()" type="button"
                                value="Pilih" name="btnChoose" />

                            &nbsp;<input style="width: 60px" onclick="closepanel3()" type="button" value="Tutup" />
                        </td>
                    </tr>
                </table>

                <div>
                    <asp:DataGrid ID="dgNoRangka" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
                        PageSize="25" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
                        CellPadding="3" DataKeyField="ID">
                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                        <Columns>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="5px" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>

                                <ItemTemplate>
                                    &nbsp;
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ID">
                                <HeaderStyle Width="0px" CssClass="titleTableSales hiddencol"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" CssClass=" hiddencol"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblIDoGridPanel3" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNoGridPanel3" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="No Rangka">
                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNoChassisPanel3" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Deskripsi">
                                <HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDeskripsiPanel3" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Tanggal Validasi/Open Faktur">
                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblValidasiFakturPanel3" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nomor Mesin">
                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNoMesinPanel3" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Customer">
                                <HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerPanel3" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>



                        </Columns>
                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>


                </div>

            </div>

        </asp:Panel>







        <asp:Panel ID="Panel4" runat="server">
            <div id="areahidden2">

                <div class="titlePage">
                    Daftar Event
                </div>

                <div style="background-image: url(../images/bg_hor.gif)">
                    <img border="0" src="../images/bg_hor.gif">
                </div>
                <div>
                    <img height="1" border="0" src="../images/dot.gif">
                </div>
                <div>
                    <asp:DataGrid ID="dgEvent" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
                        PageSize="2000" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
                        CellPadding="3" DataKeyField="ID">
                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                        <Columns>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="5px" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>

                                <ItemTemplate>
                                    &nbsp;
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ID">
                                <HeaderStyle Width="0px" CssClass="titleTableSales hiddencol"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" CssClass=" hiddencol"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblIDoGridPanel4" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNoGridPanel4" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="No Event">
                                <HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNoEventPanel4" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nama Event">
                                <HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNamaEventPanel4" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Tanggal Event">
                                <HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblEventDatePanel4" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Status">
                                <HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatusPanel4" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>



                        </Columns>
                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>


                </div>

                <input style="width: 60px" onclick="GetSelectedValue2()" type="button"
                    value="Pilih" name="btnChoose" />

                &nbsp;<input style="width: 60px" onclick="closepanel3()" type="button" value="Tutup" />
            </div>

        </asp:Panel>

        <asp:Panel ID="Panel5" runat="server">
            <table>
                <tr>
                    <td>Mengubah Status</td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server">

                            <asp:ListItem Value="1" Text="OK"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Tidak"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnStatus" runat="server" Text="Proses" Width="60px"></asp:Button>&nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>




        <asp:Panel ID="Panel8" runat="server">
            <div id="areahidden2">

                <div class="titlePage">
                    Keterangan 
                </div>

                <div style="background-image: url(../images/bg_hor.gif)">
                    <img border="0" src="../images/bg_hor.gif">
                </div>
                <div>
                    <img height="1" border="0" src="../images/dot.gif">
                </div>
                <div>
                    <asp:TextBox ID="txtKeterangan" runat="server" TextMode="MultiLine"></asp:TextBox>

                    <asp:HiddenField ID="txtIdDetailForKet" runat="server" />

                </div>
                <asp:Button ID="btnSaveKet" runat="server" Text="Ok" />


                &nbsp;<input style="width: 60px" onclick="closepanel8()" type="button" value="Tutup" />
            </div>

        </asp:Panel>


        <!-- <div style=" position:absolute; top:0px; left:0px;" id="areahidden"> -->
    </form>
</body>
</html>
