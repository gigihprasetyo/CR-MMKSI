<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmWSCHeaderBB.aspx.vb" Inherits=".FrmWSCHeaderBB" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>FrmWSCHeaderBB</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        

        function CheckAll(aspCheckBoxID, checkVal) {
            re = new RegExp(':' + aspCheckBoxID + '$')
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox') {
                    if (re.test(elm.name)) {
                        elm.checked = checkVal
                    }
                }
            }
        }

        function GetCurrentInputIndex(GridName) {
            var dtgDamageCode = document.getElementById(GridName);
            var currentRow;
            var index = 0;
            var inputs;
            var indexInput;

            index = dtgDamageCode.rows.length - 1
            inputs = dtgDamageCode.rows[index].getElementsByTagName("INPUT");

            if (inputs != null && inputs.length > 0) {
                for (indexInput = 0; indexInput < inputs.length; indexInput++) {
                    if (inputs[indexInput].type != "hidden" && inputs[indexInput].type != "checkbox")
                        return index;
                }
            }

            return -1;
        }

        function validateAmount(val) {
            if (val != "") {
                //var index = GetCurrentInputIndex("dgOngkosKerja");
                var index = val.parentNode.parentNode.rowIndex;
                var ddlClaimType = document.getElementById("ddlClaimType");
                var ddlRefDoc = document.getElementById("ddlRefDoc");
                var selectedClaimTypeValue = ddlClaimType.options[ddlClaimType.selectedIndex].value;
                var selectedddlRefDocValue = ddlRefDoc.options[ddlRefDoc.selectedIndex].value;
                var positionCode = dgOngkosKerja.rows[index].getElementsByTagName("INPUT")[0];
                var workCode = dgOngkosKerja.rows[index].getElementsByTagName("INPUT")[1];
                var jumlah = dgOngkosKerja.rows[index].getElementsByTagName("INPUT")[2];
                var amountKerja = dgOngkosKerja.rows[index].getElementsByTagName("INPUT")[3];
                //if (selectedClaimTypeValue == "Z4") {


                //if (positionCode.value.toLowerCase() == "xee999" && workCode.value.toLowerCase() == "99") {
                //    amountKerja.style.display = 'table-row'
                //    amountKerja.disable = false
                //    amountKerja.value = '1'
                //}
                //else 
                jumlah.readOnly = false;

                if (selectedddlRefDocValue == 1) {
                    amountKerja.style.display = 'table-row'
                    amountKerja.disabled = true
                    if (workCode.value.toLowerCase() == "99") {
                        //amountKerja.style.display = 'table-row'
                        amountKerja.disabled = false
                        amountKerja.MaxLength = '2'
                        jumlah.readOnly = true;
                        if (amountKerja.value == 0 || amountKerja.value == "") {
                            amountKerja.value = '0'
                            jumlah.value = '1'
                        }
                    }
                    else {
                        //amountKerja.style.display = 'none'
                        //amountKerja.disabled = true
                        amountKerja.MaxLength = '1'
                        if (amountKerja.value == 0 || amountKerja.value == "") {
                            amountKerja.value = '0'
                        }
                    }
                } else if (selectedddlRefDocValue == 0) {
                    //amountKerja.style.display = 'table-row'
                    //amountKerja.disabled = true
                    if (workCode.value.toLowerCase() == "90") {
                        amountKerja.style.display = 'table-row'
                        amountKerja.disabled = false
                        amountKerja.MaxLength = '2'
                        if (amountKerja.value == 0 || amountKerja.value == "") {
                            amountKerja.value = '0'
                        }
                    }
                    else {
                        amountKerja.style.display = 'none'
                        amountKerja.disabled = true
                        amountKerja.MaxLength = '1'
                        if (amountKerja.value == 0 || amountKerja.value == "") {
                            amountKerja.value = '0'
                        }
                    }
                }
                //}
            }
        }

        function validateAmountParts(val) {
            if (val != "") {
                var ddlClaimType = document.getElementById("ddlClaimType");
                var selectedClaimTypeValue = ddlClaimType.options[ddlClaimType.selectedIndex].value;
                var index = GetCurrentInputIndex("dgParts");
                var dgParts = document.getElementById("dgParts");
                var positionCode = dgParts.rows[index].getElementsByTagName("INPUT")[0];
                var amountKerja = dgParts.rows[index].getElementsByTagName("INPUT")[2];
                if (positionCode.value.toLowerCase() == "npn7") {
                    //if (selectedClaimTypeValue.toLowerCase() == "z4" && positionCode.value.toLowerCase() == "npn7") {
                    amountKerja.style.display = 'table-row'
                    if (amountKerja.value == 0 || amountKerja.value == "") {
                        amountKerja.value = '0'
                    }
                }
                else {
                    amountKerja.style.display = 'none'

                    if (amountKerja.value == 0 || amountKerja.value == "") {
                        amountKerja.value = '0'
                    }
                }
            }
        }

        function getSelectedWork(selectedWork) {
            var hdnIndexSelectedGridOngkosKerja = document.getElementById("hdnIndexSelectedGridOngkosKerja");

            //var index = GetCurrentInputIndex("dgOngkosKerja");
            var index = hdnIndexSelectedGridOngkosKerja.value;
            var dgOngkosKerja = document.getElementById("dgOngkosKerja");
            var tempParams = selectedWork.split(';');
            var code = dgOngkosKerja.rows[index].getElementsByTagName("INPUT")[1];

            code.value = tempParams[0];
            validateAmount(code);
        }

        function getSelectedPartsCode(selectedPart) {
            var index = GetCurrentInputIndex("dgParts");
            var dgParts = document.getElementById("dgParts");
            var tempParams = selectedPart.split(';');
            var codePart = dgParts.rows[index].getElementsByTagName("INPUT")[0];
            var namePart = dgParts.rows[index].getElementsByTagName("SPAN")[1];
            codePart.value = tempParams[0];
            namePart.innerHTML = tempParams[1];
        }

        function showPopupSearchPartCode(evt) {
            var txtNoChasisSelection = document.getElementById("txtNoChasis");
            if (txtNoChasisSelection.value == "") {
                alert("Nomor rangka harus di isi !");
                return;
            }

            var index = GetCurrentInputIndex("dgParts");
            var dgParts = document.getElementById("dgParts");
            var codePart = dgParts.rows[index].getElementsByTagName("INPUT")[0];
            if (codePart.disabled == true || dgParts.disabled == true) { return; }
            showPopUp('../PopUp/PopUpPartsCodeSelection.aspx', '', 500, 760, getSelectedPartsCode);
        }

        function getRowIndex(el) {
            while ((el = el.parentNode) && el.nodeName.toLowerCase() !== 'tr');

            if (el)
                return el.rowIndex;
        }

        function showPopupSearchPositionCode(evt) {
            var hdnIndexSelectedGridOngkosKerja = document.getElementById("hdnIndexSelectedGridOngkosKerja");
            var idx = getRowIndex(evt);
            hdnIndexSelectedGridOngkosKerja.value = idx


            var ddlClaimType = document.getElementById("ddlClaimType");
            var selectedClaimTypeValue = ddlClaimType.options[ddlClaimType.selectedIndex].value;
            var ddlRefDoc = document.getElementById("ddlRefDoc");
            var selectedRefDocValue = ddlRefDoc.options[ddlRefDoc.selectedIndex].value;
            var ddlRefDocValue = ddlRefDoc.options[ddlRefDoc.selectedIndex].value;
            if (selectedClaimTypeValue == "") {
                alert("Tipe Claim harus di isi !");
                return;
            }

            var txtNoChasisSelection = document.getElementById("txtNoChasis");
            if (txtNoChasisSelection.value == "") {
                alert("Nomor rangka harus di isi !");
                return;
            }

            var index = GetCurrentInputIndex("dgOngkosKerja");
            var dgOngkosKerja = document.getElementById("dgOngkosKerja");
            var txtPQRNo = document.getElementById("txtPQRNo");
            var positionCode = dgOngkosKerja.rows[idx].getElementsByTagName("INPUT")[0];
            if (positionCode.disabled == true || dgOngkosKerja.disabled == true) { return; }
            showPopUp('../PopUp/PopUpPositionCodeSelectionWSC.aspx?ClaimType=' + selectedClaimTypeValue + '&refDoc=' + ddlRefDocValue + '&chaNum=' + txtNoChasisSelection.value + '&wscBBFlag=1', '', 500, 760, getSelectedPosition);
        }

        function getSelectedPosition(selectedPosition) {
            var hdnIndexSelectedGridOngkosKerja = document.getElementById("hdnIndexSelectedGridOngkosKerja");

            //var index = GetCurrentInputIndex("dgOngkosKerja");
            var index = hdnIndexSelectedGridOngkosKerja.value;
            var dgOngkosKerja = document.getElementById("dgOngkosKerja");
            var ddlClaimType = document.getElementById("ddlClaimType");
            var selectedClaimTypeValue = ddlClaimType.options[ddlClaimType.selectedIndex].value;
            var positionCode = dgOngkosKerja.rows[index].getElementsByTagName("INPUT")[0];
            var workCode = dgOngkosKerja.rows[index].getElementsByTagName("INPUT")[1];
            var amountKerja = dgOngkosKerja.rows[index].getElementsByTagName("INPUT")[3];
            var tempParams = selectedPosition.split(';');
            var code = dgOngkosKerja.rows[index].getElementsByTagName("INPUT")[0];
            code.value = tempParams[0];

            if (selectedClaimTypeValue == "Z4") {
                if (positionCode.value.toLowerCase() == "xee999" && workCode.value.toLowerCase() == "99") {
                    amountKerja.style.display = 'table-row'
                    amountKerja.value = ''
                }
                else {
                    amountKerja.style.display = 'none'
                    amountKerja.value = '0'
                }
            }
        }

        function showPopupSearchWorkCode(evt) {
            var hdnIndexSelectedGridOngkosKerja = document.getElementById("hdnIndexSelectedGridOngkosKerja");
            var idx = getRowIndex(evt);
            hdnIndexSelectedGridOngkosKerja.value = idx

            var ddlClaimType = document.getElementById("ddlClaimType");
            var selectedClaimTypeValue = ddlClaimType.options[ddlClaimType.selectedIndex].value;
            if (selectedClaimTypeValue == "") {
                alert("Tipe Claim harus di isi !");
                return;
            }

            var txtNoChasisSelection = document.getElementById("txtNoChasis");
            if (txtNoChasisSelection.value == "") {
                alert("Nomor rangka harus di isi !");
                return;
            }

            //var index = GetCurrentInputIndex("dgOngkosKerja");
            var index = idx
            var dgOngkosKerja = document.getElementById("dgOngkosKerja");
            var position = dgOngkosKerja.rows[index].getElementsByTagName("INPUT")[0];
            var positionCode = position.value;
            if (position.disabled == true || dgOngkosKerja.disabled == true) { return; }



            showPopUp('../PopUp/PopUpWorkCode.aspx?positionCode=' + positionCode + '&ChassisNumber=' + txtNoChasisSelection.value, '', 500, 760, getSelectedWork);
        }

        function ShowPPInfoKendaraanSelection() {
            var txtNoChasisSelection = document.getElementById("txtNoChasis");
            showPopUp('../PopUp/PopUpInfoKendaraan.aspx?cn=' + txtNoChasisSelection.value, '', 770, 700, InfoKendaraanSelection);
        }

        function InfoKendaraanSelection(selectedRefNumber) {
            var txtNoChasisSelection = document.getElementById("txtNoChasis");
            txtNoChasisSelection.value = selectedRefNumber;
            if (navigator.appName == "Microsoft Internet Explorer") {
                txtNoChasisSelection.focus();
                txtNoChasisSelection.blur();
            }
            else {
                txtNoChasisSelection.onchange();
            }
        }

        function ShowPPInfoDokumenSelection() {
            var ddlRefDoc = document.getElementById("ddlRefDoc");
            var ddlClaimType = document.getElementById("ddlClaimType");
            var selectedClaimTypeValue = ddlClaimType.options[ddlClaimType.selectedIndex].value;
            var selectedRefDocValue = ddlRefDoc.options[ddlRefDoc.selectedIndex].value;

            if (selectedRefDocValue == "-1") {
                alert("Silakan pilih Referensi Dokumen !");
                return;
            }
            if (selectedClaimTypeValue == "-1") {
                alert("Silakan pilih Tipe Claim !");
                return;
            }

            if (selectedRefDocValue == "0") {
                showPopUp('../PopUp/PopUpRefDocumentServiceBulletin.aspx', '', 500, 600, InfoDokumenSBSelection);
            }
            else if (selectedRefDocValue == "1") {
                showPopUp('../PopUp/PopUpRefDocumentPQR.aspx?ClaimType=' + selectedClaimTypeValue, '', 500, 510, InfoDokumenPQRSelection);
            }
        }

        function InfoDokumenPQRSelection(selectedDoc) {
            var txtPQRNoSelection = document.getElementById("txtPQRNo");
            var tempParams = selectedDoc.split(';');
            txtPQRNoSelection.value = tempParams[0];

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtPQRNoSelection.focus();
                txtPQRNoSelection.blur();
            }
            else {
                txtPQRNoSelection.onchange();
            }
        }

        function ShowPPInfoWSCSelection() {
            var ddlRefDoc = document.getElementById("ddlRefDoc");
            var ddlClaimType = document.getElementById("ddlClaimType");
            var selectedClaimTypeValue = ddlClaimType.options[ddlClaimType.selectedIndex].value;
            var selectedRefDocValue = ddlRefDoc.options[ddlRefDoc.selectedIndex].value;

            if (selectedRefDocValue == "-1") {
                alert("Silakan pilih Referensi Dokumen !");
                return;
            }
            if (selectedClaimTypeValue == "-1") {
                alert("Silakan pilih Tipe Claim !");
                return;
            }
            showPopUp('../PopUp/PopUpRefDocumentWSC.aspx?BB=True', '', 500, 600, InfoDokumenWSCSelection);
        }

        function InfoDokumenWSCSelection(selectedDoc) {
            var txtRefClaimNumberSelection = document.getElementById("txtRefClaimNumber");
            var tempParams = selectedDoc.split(';');
            txtRefClaimNumberSelection.value = tempParams[0];

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtRefClaimNumberSelection.focus();
                txtRefClaimNumberSelection.blur();
            }
            else {
                txtRefClaimNumberSelection.onchange();
            }
        }

        function InfoDokumenSBSelection(selectedDoc) {
            var txtPQRNoSelection = document.getElementById("txtPQRNo");
            var tempParams = selectedDoc.split(';');
            txtPQRNoSelection.value = tempParams[0];

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtPQRNoSelection.focus();
                txtPQRNoSelection.blur();
            }
            else {
                txtPQRNoSelection.onchange();
            }
        }

        function ShowChassisSelection() {
            var txtNoChasisSelection = document.getElementById("txtNoChasis");
            var ddlRefDoc = document.getElementById("ddlRefDoc");
            var ddlClaimType = document.getElementById("ddlClaimType");
            var selectedClaimTypeValue = ddlClaimType.options[ddlClaimType.selectedIndex].value;
            var selectedRefDocValue = ddlRefDoc.options[ddlRefDoc.selectedIndex].value;
            var txtPQRNo = document.getElementById("txtPQRNo");

            if (selectedRefDocValue == "-1") {
                alert("Silakan pilih Referensi Dokumen !");
                return;
            }
            if (selectedClaimTypeValue == "-1") {
                alert("Silakan pilih Tipe Claim !");
                return;
            }

            if (selectedRefDocValue == "0") {
                showPopUp('../SparePart/../PopUp/PopUpChassisMasterSelection2.aspx?BB=True&pqrNo=' + txtPQRNo.value, '', 500, 500, InfoChassisSelection);
            }
            else {
                alert('Tipe claim dan referensi dokumen tidak sesuai')
                return;
            }
        }

        function CheckChassis() {
            document.getElementById('lnkbtnCheckChassis').click();
        }

        function CheckPPChassis() {
            document.getElementById('lnkbtnPPCheckChassis').click();
        }

        function InfoChassisSelection(selectedChassis) {
            var tempParam = selectedChassis.split(';');
            var txtNoChasisSelection = document.getElementById("txtNoChasis");
            var lblNoChasisVal = document.getElementById("lblNoChasisVal");
            var hdntxtNoChasis = document.getElementById("hdntxtNoChasis");

            txtNoChasisSelection.value = tempParam[0];
            lblNoChasisVal.value = tempParam[0];
            hdntxtNoChasis.value = txtNoChasisSelection.value
            CheckPPChassis();
            //CheckChassis();
            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    txtNoChasisSelection.focus();
            //    txtNoChasisSelection.blur();
            //}
            //else {
            //    txtNoChasisSelection.onchange();
            //}
        }

        function ShowPPWSCSendEmail() {
            showPopUp('../Service/FrmWSCSendEmail.aspx', '', 600, 600, InfoDokumenSBSelection);
        }
        function DummyFunction() {

        }

        function MaxLengthCount(text) {
            //asp.net textarea maxlength doesnt work; do it by hand
            var maxlength = 2000; //set your value here (or add a parm and pass it in)
            var object = document.getElementById(text.id)  //get your object
            if (object.value.length > maxlength) {
                object.focus(); //set focus to prevent jumping
                object.value = text.value.substring(0, maxlength); //truncate the value
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
            return true;
        }

        function ShowPPDealerBranchSelection() {
            var lblDealer = document.getElementById("lblDealerVal");
            var dealerCode = lblDealer.innerText.split("/")[0].replace(/\s/g, '');
            showPopUp('../General/../PopUp/PopUpDealerBranchSelectionSingle.aspx?m=d&deal=' + dealerCode, '', 500, 760, TemporaryOutlet);
        }

        function TemporaryOutlet(selectedDealer) {
            if (selectedDealer.indexOf(";") > 0) {
                var txtDealerSelection = document.getElementById("txtDealerBranchCode");
                var txtBranchName = document.getElementById("txtBranchName");
                txtDealerSelection.value = selectedDealer.split(";")[0];
                txtBranchName.value = selectedDealer.split(";")[1];
            }
            else {
                var txtDealerSelection = document.getElementById("txtDealerBranchCode");
                txtDealerSelection.value = selectedDealer;
            }
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Waranty Service Claim - Input WSC Special</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
        </table>
        <table cellspacing="1" cellpadding="4" width="100%" border="0">
            <tr valign="top">
                <td width="50%">
                    <table cellspacing="1" cellpadding="2" width="460px" border="0">
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:HiddenField ID="hdnIndexSelectedGridOngkosKerja" runat="server" />
                                <asp:Label ID="lblDealer" runat="server">Kode Dealer </asp:Label></td>
                            <td>:</td>
                            <td width="37%">
                                <asp:Label ID="lblDealerVal" runat="server" Font-Size="8"></asp:Label>&nbsp;/&nbsp;
                                <asp:Label ID="lblDealerSearchTerm1" runat="server"></asp:Label>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblDealerNm" runat="server">Nama Dealer </asp:Label></td>
                            <td>:</td>
                            <td width="34%">
                                <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblKodeCabang" runat="server">Kode Cabang</asp:Label></td>
                            <td>:</td>
                            <td width="34%">
                                <asp:TextBox ID="txtDealerBranchCode" runat="server" Width="150px" onkeypress="return alphaNumericExcept(event,'<>?*%$;{},+=');"
                                    ToolTip="Kode Cabang" AutoPostBack="true"></asp:TextBox>
                                <asp:Label ID="lblPopUpDealerBranch" runat="server" Width="10"> 
									<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>
                                <asp:Label ID="lblDealerBranch" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblNamaCabang" runat="server">Nama Cabang</asp:Label></td>
                            <td>:</td>
                            <td width="34%">
                                <asp:TextBox ID="txtBranchName" Width="80%" runat="server" BackColor="#EBEBE4"></asp:TextBox>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">WO Number</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtWONumber" runat="server" Width="150px"></asp:TextBox>
                                <asp:LinkButton ID="lnkBtnCheckWONumber" runat="server" CausesValidation="False" ToolTip="Validate WO Number">
									<img style="cursor:hand" alt="Check WO Number" src="../images/tanya.gif" border="0"></asp:LinkButton>
                                <asp:Label ID="lblWONumberVal" runat="server"></asp:Label>

                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">Tipe Claim</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlClaimType" AutoPostBack="true"
                                    runat="server" Width="50%">
                                </asp:DropDownList>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">Referensi Dokumen</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlRefDoc" Enabled="false" AutoPostBack="true" Width="80%"></asp:DropDownList>
                                <asp:TextBox ID="txtPQRNo" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtPQRNo','<>?*%$;')"
                                    AutoPostBack="True" runat="server" Width="160px"></asp:TextBox>
                                <asp:LinkButton ID="lnkbtnPopUpInfoDokumen"
                                    runat="server" CausesValidation="False" ToolTip="View Info Dokumen">
										<img style="cursor:hand" alt="View Info Dokumen" src="../images/popup.gif" border="0"></asp:LinkButton>
                                <asp:LinkButton ID="lblPQRNoVal" Visible="false" runat="server" Font-Size="8"></asp:LinkButton>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">Nomor WSC</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblClaimNumber" runat="server">[Automatically by System]</asp:Label>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblRefClaimNumber" runat="server">No WSC Ref</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtRefClaimNumber" runat="server" Width="160px"></asp:TextBox>&nbsp;
                                    <asp:LinkButton ID="lnkbtnPopUpRefClaimNumber"
                                        runat="server" CausesValidation="False" ToolTip="View Ref Dokumen WSC">
                                            <img style="cursor:hand" alt="View Ref Dokumen WSC" src="../images/popup.gif" border="0"></asp:LinkButton>&nbsp;

                                <asp:Label ID="lblRefClaimNumberVal" Visible="false" runat="server" Font-Size="8" Width="196px"></asp:Label></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblNoChasis" runat="server">Nomor Rangka</asp:Label>
                                <asp:Label ID="lblNoChasisVal" Style="display: none" runat="server" Font-Size="8"></asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:HiddenField ID="hdntxtNoChasis" runat="server" />
                                <asp:HiddenField ID="hdnPPtxtNoChasis" runat="server" />
                                <asp:TextBox ID="txtNoChasis" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="CheckChassis();omitSomeCharacter('txtNoChasis','<>?*%$;')"
                                    Enabled="false" runat="server" Width="160px" ReadOnly="true"></asp:TextBox>&nbsp;

                                <%--<asp:TextBox ID="txtNoChasis" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtNoChasis','<>?*%$;')"
                                        AutoPostBack="true" ReadOnly="true" runat="server" Width="160px"></asp:TextBox>&nbsp;--%>

                                <asp:Label class="hideSpanOnPrint" ID="lblSearchChassis" runat="server" Visible="false">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup" onclick="ShowChassisSelection();">
                                </asp:Label>
                                <asp:LinkButton ID="lnkbtnCheckChassis" runat="server" CausesValidation="False" ToolTip="Validate Chassis">
										<img style="cursor:hand" alt="Check Chassis" src="../images/tanya.gif" border="0"></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnPopUpInfoKendaraan" runat="server" CausesValidation="False" ToolTip="View Info Kendaraan" Visible="false">
										<img style="cursor:hand" alt="View Info Kendaraan" src="../images/dok.gif" border="0"></asp:LinkButton>
                                <asp:RequiredFieldValidator ID="rfvChassisNumber" runat="server" ErrorMessage="No Rangka harus diisi" ControlToValidate="txtNoChasis">*</asp:RequiredFieldValidator>
                                <asp:LinkButton ID="lnkbtnPPCheckChassis" runat="server" CausesValidation="False" Style="visibility: hidden">
										<img style="cursor:hand" alt="Check Chassis" src="../images/tanya.gif" border="0"></asp:LinkButton>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr style="display: none">
                            <td class="titleField" style="width: 126px">Status Stock Dealer</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblAlreadySaled" runat="server" Width="213">Value Of Status Stok</asp:Label>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr style="display: none">
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblNoMesin" runat="server">No Mesin</asp:Label></td>
                            <td style="height: 16px">:</td>
                            <td style="height: 16px">
                                <asp:Label ID="lblNoMesinVal" runat="server" Width="213">Value Of No Mesin</asp:Label></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr style="display: none">
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblNamaPemilik" runat="server">Nama Pemilik</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblNamaPemilikVal" runat="server" Width="213">Value Of Nama</asp:Label></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblTglKirim" runat="server">Tanggal Kirim</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTglKirimVal" runat="server" Width="213">[Tanggal Kirim]</asp:Label></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr style="display: none">
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblTglPKT" runat="server">Tanggal PKT</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTglPKTVal" runat="server" Width="213">[Tanggal PKT]</asp:Label></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblTglKerusakan" runat="server">Tanggal Kerusakan</asp:Label></td>
                            <td>:</td>
                            <td>
                                <cc1:IntiCalendar ID="icTglKerusakan" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                <asp:Label ID="lblTglKerusakanVal" runat="server" Style="display: none"></asp:Label>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblTglPerbaikan" runat="server">Tanggal Service</asp:Label></td>
                            <td>:</td>
                            <td>
                                <cc1:IntiCalendar ID="icTglPerbaikan" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                <asp:Label ID="lblTglPerbaikanVal" runat="server" Style="display: none"></asp:Label></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblOdometer" runat="server">Jarak Tempuh</asp:Label></td>
                            <td>:</td>
                            <td valign="top">
                                <asp:TextBox ID="txtOdometer" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')"
                                    runat="server" Width="70"></asp:TextBox>&nbsp;&nbsp;<span style="font-size: 8pt">Km</span>
                                <asp:RequiredFieldValidator ID="rfvOdometer" runat="server" ErrorMessage="Odometer harus diisi" ControlToValidate="txtOdometer">*</asp:RequiredFieldValidator>
                                &nbsp;<asp:Label ID="lblOdometerVal" Visible="false" runat="server" Style="display: none"></asp:Label>&nbsp;
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <font class="titleField">Ongkos Kerja :</font>
                                <div id="containerKerja" style="overflow: auto; height: auto; width: 99%;">
                                    <asp:DataGrid ID="dgOngkosKerja" TabIndex="99" runat="server" Width="100%" BorderWidth="1px" BorderColor="#CDCDCD"
                                        CellPadding="3" AutoGenerateColumns="False" ShowFooter="True" BackColor="White">
                                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                        <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                                        <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="25px" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Kode Posisi">
                                                <HeaderStyle Width="108px" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPositionCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PositionCode")%>' Visible="false">

                                                    </asp:Label>
                                                    <asp:TextBox ID="txtPostionCodeItem" onblur="validateAmount(this)" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PositionCode")%>' Width="84px" MaxLength="10" />
                                                    <asp:Label ID="lblSearchPositionCodeItem" runat="server" TabIndex="0" onclick="showPopupSearchPositionCode(this);">
                                                        <img src="../images/popup.gif" style="cursor: pointer;" border="0" alt="Klik popup"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Wrap="False"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtPositionCodeFooter" name="txtPositionCodeFooter" onblur="validateAmount(this)" Width="84px" MaxLength="10" runat="server"></asp:TextBox>
                                                    <asp:Label ID="lblSearchPositionCode" runat="server" TabIndex="0" onclick="showPopupSearchPositionCode(this);">
                                                        <img src="../images/popup.gif" style="cursor: pointer;" border="0" alt="Klik popup"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Kode Kerja">
                                                <HeaderStyle Width="108px" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWorkCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WorkCode")%>' Visible="false">

                                                    </asp:Label>
                                                    <asp:TextBox ID="txtWorkCodeItem" onblur="validateAmount(this)" Text='<%# DataBinder.Eval(Container, "DataItem.WorkCode")%>' runat="server" Width="84px" MaxLength="2" />
                                                    <asp:Label ID="lblSearchWorkCodeItem" runat="server" TabIndex="0" onclick="showPopupSearchWorkCode(this);">
                                                        <img src="../images/popup.gif" style="cursor: pointer;" border="0" alt="Klik popup"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Wrap="False"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtWorkCodeFooter" name="txtWorkCodeFooter" onblur="validateAmount(this)" Width="84px" MaxLength="2" runat="server" ></asp:TextBox>
                                                    <asp:Label ID="lblSearchWorkCode" runat="server" TabIndex="0" onclick="showPopupSearchWorkCode(this);">
                                                        <img src="../images/popup.gif" style="cursor: pointer;" border="0" alt="Klik popup"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn SortExpression="Quantity" HeaderText="Jml">
                                                <HeaderStyle Width="16%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity", "{0:#,##0}") %>' Visible="false">

                                                    </asp:Label>
                                                    <asp:TextBox ID="txtQuantityItem" runat="server" MaxLength="2" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity", "{0:#,##0}") %>' Width="100%" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')">
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtQuantityFooter" runat="server" MaxLength="2" Width="100%" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')">
                                                    </asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn SortExpression="PartPrice" HeaderText="Amount">
                                                <HeaderStyle Width="13%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartPrice", "{0:#,##0}")%>' Visible="false">

                                                    </asp:Label>
                                                    <asp:TextBox ID="txtAmountItem" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartPrice", "{0:#,##0}")%>' MaxLength="9" Width="100%" onkeypress="return NumericOnlyWith(event,'');">
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtPartPriceFooter" runat="server" MaxLength="9" Width="100%" onkeypress="return NumericOnlyWith(event,'');">
                                                    </asp:TextBox>
                                                </FooterTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" ID="txtPartPriceEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartPrice", "{0:#,##0}")%>' MaxLength="9" Width="100%">
                                                    </asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn SortExpression="" HeaderText="Master Valid">
                                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbMasterValidItem" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:CheckBox ID="cbMasterValidFooter" runat="server" Enabled="false" />
                                                </FooterTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="50px" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <%--<asp:LinkButton ID="lnkbtnAddOKerja" runat="server" CommandName="Edit" CausesValidation="False" TabIndex="0">
												            <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>--%>
                                                    <asp:LinkButton ID="lnkbtnDeleteOKerja" runat="server" CommandName="Delete" CausesValidation="False">
												            <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkbtnAddOKerja" runat="server" CommandName="Add" CausesValidation="False" TabIndex="0">
												            <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                                </FooterTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnSaveOKerja" CommandName="Save" Text="Simpan" runat="server" CausesValidation="False">
												            <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnCancelOKerja" CommandName="Cancel" Text="Batal" runat="server" CausesValidation="False">
												            <img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIDItem" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>' runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblIDFooter" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>' runat="server">
                                                    </asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <font class="titleField">Ongkos Part :</font>
                                <div id="containerPart" style="overflow: auto; height: auto; width: 99%">
                                    <asp:DataGrid ID="dgParts" TabIndex="99" runat="server" Width="100%"
                                        BorderWidth="1px" DataKeyField="ID" BorderColor="#CDCDCD"
                                        CellPadding="3" AutoGenerateColumns="False" ShowFooter="True" BackColor="White">
                                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                        <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                                        <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <HeaderTemplate>
                                                    <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														                document.forms[0].chkAllItems.checked)" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="25px" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nomor Part">
                                                <HeaderStyle Width="108px" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodeParts" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber")%>'>
                                                    </asp:Label>
                                                    <%--<asp:TextBox ID="txtKodePartsItem" runat="server" Width="84px" TabIndex="0" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber")%>'>
                                                    </asp:TextBox>--%>
                                                </ItemTemplate>
                                                <FooterStyle Wrap="False"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodePartsFooter"
                                                        runat="server" Width="84px" TabIndex="0" onblur="validateAmountParts(this)"></asp:TextBox>
                                                    <asp:Label ID="lblSearchPartsFooter" runat="server" TabIndex="0" onclick="showPopupSearchPartCode();">
												            <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Nama Part">
                                                <HeaderStyle Width="300px" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNamaParts" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Wrap="False"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblNamaPartsFooter" runat="server"><span style="font-size: 8pt"></span></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn SortExpression="Quantity" HeaderText="Jml">
                                                <HeaderStyle Width="16%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity", "{0:#,##0}") %>'>



                                                    </asp:Label>--%>
                                                    <asp:TextBox ID="txtQtyItem" runat="server" MaxLength="2" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity", "{0:#,##0}") %>' onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')">
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtQtyFooter" runat="server" MaxLength="2" Width="100%" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')">
                                                    </asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn SortExpression="PartPrice" HeaderText="Amount">
                                                <HeaderStyle Width="13%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartPrice", "{0:#,##0}")%>'>



                                                    </asp:Label>--%>
                                                    <asp:TextBox ID="txtPartPriceItem" runat="server" MaxLength="9" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.PartPrice", "{0:#,##0}")%>' onkeypress="return NumericOnlyWith(event,'');">
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtPartPriceFooter" runat="server" MaxLength="9" Width="100%" onkeypress="return NumericOnlyWith(event,'');">
                                                    </asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn SortExpression="" HeaderText="Main Part">
                                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbMainPartItem" runat="server" />
                                                    <asp:HiddenField ID="hdnMainPartItem" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.MainPart", "{0:#}")%>' />
                                                </ItemTemplate>
                                                <FooterStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:CheckBox ID="cbMainPartFooter" runat="server" Enabled="true" />
                                                </FooterTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="50px" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnDeleteParts" runat="server" CommandName="Delete" CausesValidation="False">
												            <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkbtnAddParts" runat="server" CommandName="Add" CausesValidation="False" TabIndex="0">
												            <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn Visible="False">
                                                <HeaderStyle Width="50px" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIDItem" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>' runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblIDFooter" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>' runat="server">
                                                    </asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <br />
                                <asp:Button ID="btnPrintBarcode" TabIndex="4" runat="server" Text="Print Barcode" Visible="false"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" width="100%">
                    <table cellspacing="0" cellpadding="0" border="0" runat="server" id="Table2" width="100%">
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="0" border="0" runat="server" id="Table3" width="100%">
                                    <tr valign="top">
                                        <td class="titleField" style="width: 126px">
                                            <asp:Label ID="lblGejala" runat="server">Gejala Kerusakan</asp:Label></td>
                                        <td align="center">:</td>
                                        <td valign="top">
                                            <asp:TextBox ID="txtGejala" TabIndex="0" runat="server" Width="260px" TextMode="MultiLine" Height="60px" MaxLength="100"></asp:TextBox>&nbsp;
                                            <%--<asp:RequiredFieldValidator ID="rfvGejala" runat="server" ErrorMessage="Gejala Kerusakan harus diisi" ControlToValidate="txtGejala">*</asp:RequiredFieldValidator>--%></td>
                                    </tr>
                                    <tr valign="top">
                                        <td class="titleField">
                                            <asp:Label ID="lblPemeriksaan" runat="server">Pemeriksaan</asp:Label></td>
                                        <td align="center">:</td>
                                        <td valign="top">
                                            <asp:TextBox ID="txtPemeriksaan" TabIndex="0" runat="server" Width="260px" TextMode="MultiLine" Height="60px" MaxLength="100"></asp:TextBox>&nbsp;
                                            <%--<asp:RequiredFieldValidator ID="rfvPemeriksaan" runat="server" ErrorMessage="Pemeriksaan harus diisi"
                                                ControlToValidate="txtPemeriksaan">*</asp:RequiredFieldValidator>--%></td>
                                    </tr>
                                    <tr valign="top">
                                        <td class="titleField">
                                            <asp:Label ID="lblHasil" runat="server">Perbaikan / Hasil</asp:Label></td>
                                        <td align="center">:</td>
                                        <td>
                                            <asp:TextBox ID="txtHasil" TabIndex="0" runat="server" Width="260px" TextMode="MultiLine" Height="60px" MaxLength="100"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvHasil" runat="server" ErrorMessage="Perbaikan/Hasil harus diisi"
                                                ControlToValidate="txtHasil">*</asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="0" border="0" runat="server" id="tblKodeKerusakan" width="100%">
                                    <tr>
                                        <td colspan="4">
                                            <table>
                                                <tr>
                                                    <td colspan="4"><strong>Kode Kerusakan</strong></td>
                                                </tr>
                                                <tr>
                                                    <td align="center" class="titleField">A</td>
                                                    <td width="5%" align="center">:</td>
                                                    <td style="height: 2px" width="100%">
                                                        <asp:DropDownList runat="server" ID="ddlKodeWSCA" Width="60%"></asp:DropDownList>
                                                    </td>
                                                    <td style="height: 2px"></td>
                                                </tr>
                                                <tr>
                                                    <td align="center" class="titleField"><strong>B</strong></td>
                                                    <td width="5%" align="center">:</td>
                                                    <td style="height: 2px" width="100%">
                                                        <asp:DropDownList runat="server" ID="ddlKodeWSCB" Width="60%"></asp:DropDownList>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td align="center" class="titleField"><strong>C</strong></td>
                                                    <td width="5%" align="center">:</td>
                                                    <td style="height: 2px" width="100%">
                                                        <asp:DropDownList runat="server" ID="ddlKodeWSCC" Width="60%"></asp:DropDownList>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Lampiran</strong>&nbsp;
                                <div id="div3" style="overflow: auto; height: 150px; width: 426px;">
                                    <asp:DataGrid ID="dgFileWSCEvidenceBB" TabIndex="99" runat="server" Width="100%" BorderWidth="1px"
                                        BorderColor="#CDCDCD" CellPadding="3" AutoGenerateColumns="False" ShowFooter="True" BackColor="White">
                                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                        <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                                        <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tipe Bukti">
                                                <HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWSCEvidenceType" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlWSCEvidenceTypeFooter" Width="100px"></asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="File">
                                                <HeaderStyle Width="90%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnFileWSCEVIDENCE" runat="server" CommandName="Download" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.PathFile")%>'>
                                                        <asp:Label ID="lblFileWSCEVIDENCE" runat="server" alt="Download"></asp:Label>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle Wrap="False"></FooterStyle>
                                                <FooterTemplate>
                                                    <input type="file" id="iFileWSCEVIDENCE" runat="server" tabindex="0">
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnFileAttachmentTopDelete" runat="server" CommandName="Delete" CausesValidation="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkbtnFileAttachmentTopAdd" runat="server" CommandName="Add" CausesValidation="False"
                                                        TabIndex="0">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table>
                    </table>
                </td>
            </tr>
            <tr valign="top">
                <td style="width: 398px">
                    <font class="titleField">
                        <asp:Label runat="server" ID="CaptionNotes" Text="Penjelasan MMKSI" Visible="false"></asp:Label></font>
                    <br>
                    <asp:TextBox ID="txtNotes" TabIndex="0" runat="server" Width="360px" TextMode="MultiLine" Visible="false"
                        Height="130px"></asp:TextBox><br>
                </td>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnBatal" TabIndex="3" runat="server" CausesValidation="False" Text="Kembali"></asp:Button>&nbsp;&nbsp;
                        &nbsp;&nbsp;
                        <asp:Button ID="btnSimpan" OnClientClick="return confirm('Simpan Data WSC ?')" TabIndex="0" runat="server" Text="Simpan"></asp:Button>&nbsp;&nbsp;
						&nbsp;&nbsp;
                        <asp:Button ID="btnRilis" TabIndex="1" runat="server" Text="&nbsp;&nbsp;Rilis&nbsp;&nbsp;"></asp:Button>&nbsp;&nbsp;
                        &nbsp;&nbsp;
                        <asp:Button ID="btnPermintaanBukti" TabIndex="2" runat="server" Text="Permintaan Bukti"></asp:Button>&nbsp;&nbsp;
						<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
                    </td>
                </tr>
        </table>
    </form>

    <script type="text/javascript">
        if (window.parent == window) {
            if (!navigator.appName == "Microsoft Internet Explorer") {
                self.opener = null;
                self.close();
            }
            else {
                this.name = "origWin";
                origWin = window.open(window.location, "origWin");
                window.opener = top;
                window.close();
            }
        }

        document.attachEvent('onreadystatechange', function () {
            var hdnIndexSelectedGridOngkosKerja = document.getElementById("hdnIndexSelectedGridOngkosKerja");

            //var index = GetCurrentInputIndex("dgOngkosKerja");
            var index = hdnIndexSelectedGridOngkosKerja.value;
            if (index != '') {
                var dgOngkosKerja = document.getElementById("dgOngkosKerja");
                var code = dgOngkosKerja.rows[index].getElementsByTagName("INPUT")[1];

                validateAmount(code);
            }
        }, false);
    </script>
</body>
</html>
