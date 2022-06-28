<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputDiscountProposal.aspx.vb" Inherits="FrmInputDiscountProposal" SmartNavigation="False" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
    <title>Pengajuan Discount Proposal</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script type="text/javascript" src="../WebResources/jquery.min.js"></script>
    <script type="text/javascript">

        $(function () {
            var focusedElement;
            $(document).on('focus', 'input', function () {
                if (focusedElement == this) return; //already focused, return so user can now place cursor at specific point in input.
                focusedElement = this;
                setTimeout(function () { focusedElement.select(); }, 100); //select all text in any field on focus for easy re-entry. Delay sightly to allow focus to "stick" before selecting.
            });
        });

        function ValidatePage() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
            }

            if (Page_IsValid) {
                return true;
            }
            else {
                alert('Field mandatory mohon diisi dahulu !');
                return false;
            }
        }

        function KonfirmasiSimpan(obj) {
            var btnSave = document.getElementById(obj.id);
            if (!confirm('Anda Yakin Mau Simpan ?')) {
                btnSave.disabled = false;
                return false;
            }
            else {
                if (!ValidatePage()) { return false; }
                btnSave.disabled = true;
                document.body.style.cursor = 'wait';
                document.getElementById('btnSave2').click();
                return true;
            }
        }

        function CekBlankToZerro(obj) {
            if (trim(obj.value) == '') {
                obj.value = 0;
            }
        }

        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var data = selectedDealer.split(";");
            var txtDealerCodeSelection = document.getElementById("txtDealerCode");
            txtDealerCodeSelection.value = data[0];
            var lblDealerCodeName = document.getElementById("lblDealerCodeName");
            lblDealerCodeName.innerHTML = data[0] + ' / ' + data[1];
            var btnGetInfoDealer = document.getElementById("btnGetInfoDealer");
            btnGetInfoDealer.click();

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtDealerCodeSelection.focus();
                txtDealerCodeSelection.blur();
            }
            else {
                //txtDealerCodeSelection.onchange();
            }
        }

        function OpenChild(fchID, dealerID) {
            var WinSettings = "center:yes;resizable:no;dialogHeight:120px"
            var MyArgs = window.showModalDialog("../PopUp/PopUpConfirmationDiscountProposal.aspx?fchID=" + fchID + "&dealerID=" + dealerID, MyArgs, WinSettings);
            if (MyArgs == null) {
                window.alert("Nothing returned from child. No changes made to input boxes");
            }
            else {
                var hdnValNew = document.getElementById("hdnValNew");
                hdnValNew.value = MyArgs;
                var btnReloadConfirm = document.getElementById("btnReloadConfirm");
                btnReloadConfirm.click();
            }
        }

        function ConfirmationFleetCustomer(retValue) {
            var data = retValue.split(";");
            var hdnValNew = document.getElementById("hdnValNew");
            hdnValNew.value = data[0];
            var btnReloadConfirm = document.getElementById("btnReloadConfirm");
            btnReloadConfirm.click();
        }

        function getRowIndex(el) {
            while ((el = el.parentNode) && el.nodeName.toLowerCase() !== 'tr');

            if (el)
                return el.rowIndex;
        }

        function ShowPPFleetCustSelection() {
            var ddlCustomerType = document.getElementById("ddlCustomerType");
            var selectedDDlCustomerType = ddlCustomerType.options[ddlCustomerType.selectedIndex].value;

            showPopUp('../DiscountProposal/../PopUp/PopUpFleetCustomer.aspx?custType=' + selectedDDlCustomerType, '', 500, 760, FleetCustSelection);
        }

        function FleetCustSelection(selectedFleetCust) {
            var data = selectedFleetCust.split(";");
            var txtFleetCustomerName = document.getElementById("txtFleetCustomerName");
            var hdnFleetCustomerHeaderID = document.getElementById("hdnFleetCustomerHeaderID");
            var btnReloadDataFleet = document.getElementById("btnReloadDataFleet");

            hdnFleetCustomerHeaderID.value = data[0]
            txtFleetCustomerName.disable = false;
            btnReloadDataFleet.click();

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtFleetCustomerName.focus();
                txtFleetCustomerName.blur();
            }
            else {
                //txtFleetCustomerName.onchange();
            }
        }

        function setSelectedValue(dropDownList, valueToSet) {
            var option = dropDownList.firstChild;
            for (var i = 0; i < dropDownList.length; i++) {
                if (option.text.trim().toLowerCase() == valueToSet.trim().toLowerCase()) {
                    option.selected = true;
                    return;
                }
                option = option.nextElementSibling;
            }
        }

        function toCommas(value) {
            return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
        }
        function trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }

        function UploadFile1(fileUpload) {
            var btnUpload = document.getElementById('btnUploadSuratKomitmen');
            try {
                if (fileUpload.value != '') {
                    if (btnUpload) btnUpload.click();
                }
            } catch (e) {
                alert(e.message);
            }
        }

        function UploadFile2(fileUpload) {
            var btnUpload = document.getElementById('btnUploadSuratPernyataan');

            try {
                if (fileUpload.value != '') {
                    if (btnUpload) btnUpload.click();
                }
            } catch (e) {
                alert(e.message);
            }
        }

        function UploadFile3(fileUpload) {
            var btnUpload = document.getElementById('btnUploadLampiranPOSPK');

            try {
                if (fileUpload.value != '') {
                    if (btnUpload) btnUpload.click();
                }
            } catch (e) {
                alert(e.message);
            }
        }

        function ShowPanelCustomer() {
            var btnGetDataCustomer = document.getElementById('btnGetDataCustomer')
            btnGetDataCustomer.click();
        }

        function ChangeDDLTipe(val) {
            var hdnVechileTypeID = document.getElementById('hdnVechileTypeID');
            hdnVechileTypeID.value = val;
        }
        function ChangeDDLAssyYear(val) {
            var hdnAssyYear = document.getElementById('hdnAssyYear');
            hdnAssyYear.value = val;
        }
        function ChangeDDLModelYear(val) {
            var hdnModelYear = document.getElementById('hdnModelYear');
            hdnModelYear.value = val;
        }
        function ChangeDDLColor(val) {
            var hdnVechileColorID = document.getElementById('hdnVechileColorID');
            hdnVechileColorID.value = val;
        }
        function ChangeSubTotalCostDealer(val) {
            var hdnSubTotalCostDealer = document.getElementById('hdnSubTotalCostDealer');
            hdnSubTotalCostDealer.value = val;
        }

        function showPopupSearchFPriceReff(obj) {
            var hdnIndexProposedDiscountGrid = document.getElementById("hdnIndexProposedDiscountGrid");
            var idx = getRowIndex(obj);
            hdnIndexProposedDiscountGrid.value = idx
            var dgProposedDiscount = document.getElementById("dgProposedDiscount");
            var priceReff = dgProposedDiscount.rows[idx].getElementsByTagName("INPUT")[4];
            var today = new Date();
            var yyyy = today.getFullYear();
            var mm = today.getMonth() + 1;
            mm = '0' + mm;
            mm = mm.substr(mm.length - 2);
            var mmyyyy = String(mm) + String(yyyy);

            if (priceReff.value == '' || priceReff.value == '0') { priceReff.value = mmyyyy; }
            showPopUp('../PopUp/PopUpReferensiHarga.aspx?priceReff=' + priceReff.value, '', 150, 350, selectedPriceReff);
        }

        function selectedPriceReff(selectedOpt) {
            var tempParams = selectedOpt.split(';');

            var hdnIndexProposedDiscountGrid = document.getElementById("hdnIndexProposedDiscountGrid");
            var idx = hdnIndexProposedDiscountGrid.value;
            var dgProposedDiscount = document.getElementById("dgProposedDiscount");
            var priceReff = dgProposedDiscount.rows[idx].getElementsByTagName("INPUT")[4];
            priceReff.value = trim(tempParams[0]);
            if (navigator.appName == "Microsoft Internet Explorer") {
                priceReff.focus();
                priceReff.blur();
            }
            else {
                //priceReff.onchange();
            }
        }

        function showPopupSearchFApplicationNo(obj, dealerCode) {
            var hdnIndexProposedDiscountGrid = document.getElementById("hdnIndexProposedDiscountGrid");
            var idx = getRowIndex(obj);
            hdnIndexProposedDiscountGrid.value = idx

            var dgProposedDiscount = document.getElementById("dgProposedDiscount");
            var ddlTipeKendaraan = dgProposedDiscount.rows[idx].getElementsByTagName("SELECT")[1];
            var ddlDiscountType = dgProposedDiscount.rows[idx].getElementsByTagName("SELECT")[2];
            var selectedTipeKendaraanValue = ddlTipeKendaraan.options[ddlTipeKendaraan.selectedIndex].value;
            var selectedDiscountTypeValue = ddlDiscountType.options[ddlDiscountType.selectedIndex].value;

            if (ddlTipeKendaraan.selectedIndex == 0) {
                alert("Tipe harus di isi !");
                return;
            }
            if (ddlDiscountType.selectedIndex == 0) {
                alert("Discount Type harus di isi !");
                return;
            }

            showPopUp('../PopUp/PopUpAplikasiSPLSelection.aspx?DealerCode=' + dealerCode + '&TipeKendaraan=' + selectedTipeKendaraanValue + '&DiscountType=' + selectedDiscountTypeValue, '', 520, 700, selectedSPL);
        }

        function selectedSPL(selectedSPL) {
            var tempParams = selectedSPL.split(';');

            var hdnIndexProposedDiscountGrid = document.getElementById("hdnIndexProposedDiscountGrid");
            var idx = hdnIndexProposedDiscountGrid.value;
            
            var btnRetrieveDetailDiscount = document.getElementById("btnRetrieveDetailDiscount");
            var dgProposedDiscount = document.getElementById("dgProposedDiscount");
            var noreg = dgProposedDiscount.rows[idx].getElementsByTagName("INPUT")[2];
            var id = dgProposedDiscount.rows[idx].getElementsByTagName("INPUT")[1];
            id.value = tempParams[0];
            noreg.value = trim(tempParams[1]);
            btnRetrieveDetailDiscount.click();
            if (navigator.appName == "Microsoft Internet Explorer") {
                noreg.focus();
                noreg.blur();
            }
            else {
                //noreg.onchange();
            }
        }

        function onBlurEmailName(obj) {
            var idx = getRowIndex(obj);

            var dgEmailUser = document.getElementById("dgEmailUser");
            var hdnFRecipientName = dgEmailUser.rows[idx].getElementsByTagName("INPUT")[1];
            var txtFRecipientName = dgEmailUser.rows[idx].getElementsByTagName("INPUT")[2];
            var txtFRecipientPosition = dgEmailUser.rows[idx].getElementsByTagName("INPUT")[3];
            var txtFEmail = dgEmailUser.rows[idx].getElementsByTagName("INPUT")[4];

            if (trim(txtFRecipientName.value.toLowerCase()) != trim(hdnFRecipientName.value.toLowerCase())) {
                txtFRecipientPosition.disabled = false;
                txtFEmail.disabled = false;
                return;
            }
        }

        function showPopupSearchFEmailUser(obj, dealerCode) {
            var hdnDiscountProposalHeaderID = document.getElementById("hdnDiscountProposalHeaderID");
            var hdnIndexEmailUserGrid = document.getElementById("hdnIndexEmailUserGrid");
            var idx = getRowIndex(obj);
            hdnIndexEmailUserGrid.value = idx

            showPopUp('../PopUp/PopUpRecipientEmailDPSelection.aspx?DealerCode=' + dealerCode + '&DiscountProposalHeaderID=' + hdnDiscountProposalHeaderID.value, '', 480, 700, selectedEmailUser);
        }

        function selectedEmailUser(selectEmailUser) {
            var tempParams = selectEmailUser.split(';');

            var hdnIndexEmailUserGrid = document.getElementById("hdnIndexEmailUserGrid");
            var idx = hdnIndexEmailUserGrid.value;

            var dgEmailUser = document.getElementById("dgEmailUser");
            var hdnFDPEmailID = dgEmailUser.rows[idx].getElementsByTagName("INPUT")[0];
            var hdnFRecipientName = dgEmailUser.rows[idx].getElementsByTagName("INPUT")[1];
            var txtFRecipientName = dgEmailUser.rows[idx].getElementsByTagName("INPUT")[2];
            var txtFRecipientPosition = dgEmailUser.rows[idx].getElementsByTagName("INPUT")[3];
            var txtFEmail = dgEmailUser.rows[idx].getElementsByTagName("INPUT")[4];

            var txtRecipientNameValue = '';
            var txtRecipientPositionValue = '';
            var txtEmailValue = '';
            for (i = 1; i < dgEmailUser.rows.length - 1; i++) {
                if (navigator.appName == "Microsoft Internet Explorer") {
                    txtRecipientNameValue = dgEmailUser.rows[i].cells[1].innerText;
                    txtRecipientPositionValue = dgEmailUser.rows[i].cells[2].innerText;
                    txtEmailValue = dgEmailUser.rows[i].cells[3].innerText;
                }
                else {
                    txtRecipientNameValue = dgEmailUser.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML;
                    txtRecipientPositionValue = dgEmailUser.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML;
                    txtEmailValue = dgEmailUser.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML;
                }
                if (trim(txtRecipientNameValue) == trim(tempParams[1]) && trim(txtRecipientPositionValue) == trim(tempParams[2]) && trim(txtEmailValue) == trim(tempParams[3])) {
                    alert('Data email user sudah pernah di inputkan');
                    return;
                }
            }

            hdnFDPEmailID.value = tempParams[0];
            hdnFRecipientName.value = tempParams[1];
            txtFRecipientName.value = tempParams[1];
            txtFRecipientPosition.value = trim(tempParams[2]);
            txtFEmail.value = trim(tempParams[3]);
            txtFRecipientPosition.disabled = true;
            txtFEmail.disabled = true;

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtFRecipientName.focus();
                txtFRecipientName.blur();
            }
            else {
                txtFRecipientName.onchange();
            }
        }

    </script>

    <script type="text/javascript">
        function DisableButton() {
            document.form1.btnSave.disabled = true
        }

        function HideDiv() {
            var p = document.getElementsByTagName("Loading");
            p.style.display = 'none';
        }

        function GetIEVersion() {
            var sAgent = window.navigator.userAgent;
            var Idx = sAgent.indexOf("MSIE");

            if (Idx > 0) {
                return parseInt(sAgent.substring(Idx + 5, sAgent.indexOf(".", Idx)));
            }
            else if (!!navigator.userAgent.match(/Trident\/7\./))
                return 11;

            else
                return 0; //It is not IE
        }
         

        $(document).ready(function () {
            function hideAll() {
                $('#Loading').hide();
            }

            hideAll();

            $('.waitLoad').click(function () {
                hideAll();
                switch ($(this).attr("id")) {
                    case "lnkReloadHistoryPembelian":
                        $('#Loading').show();
                        break;
                }
            }); // end of function for clicking 
        });
    </script>

    <style type="text/css">
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
     <style type="text/css">
        input[type="submit"]:hover
        {
            background-color: red !important;
             cursor: pointer !important;
             cursor: pointer !important; 
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" runat="server">
        <input id="hdnDiscountProposalHeaderID" type="hidden" value="0" runat="server">

        <input id="hdnModelYear" type="hidden" value="0" runat="server">
        <input id="hdnAssyYear" type="hidden" value="0" runat="server">
        <input id="hdnVechileTypeID" type="hidden" value="0" runat="server">
        <input id="hdnVechileColorID" type="hidden" value="0" runat="server">
        <input id="hdnSubTotalCostDealer" type="hidden" value="0" runat="server">
        <input id="hdnShowDataCustomer" type="hidden" value="0" runat="server" name="hdnShowDataCustomer">
        <input id="hdnIndexProposedDiscountGrid" type="hidden" value="" runat="server" name="hdnIndexProposedDiscountGrid">
        <input id="hdnIndexEmailUserGrid" type="hidden" value="" runat="server" name="hdnIndexEmailUserGrid">
        <input id="hdnValNew" type="hidden" value="-1" runat="server" name="hdnValNew">

        <div id="MainPanel" runat="server" style="margin: 0 auto; width:855px;">
            <div style="width:1500px;">
                <table id="Table2" cellspacing="0" cellpadding="0" width="80%" border="0">
                    <tr>
                        <td class="titlePage">DISCOUNT PROPOSAL - Pengajuan Discount Proposal</td>
                    </tr>
                    <tr>
                        <td background="../images/bg_hor.gif" height="1" colspan="2">
                            <img height="1" src="../images/bg_hor.gif" border="0"></td>
                    </tr>
                    <tr>
                        <td height="10" colspan="2">
                            <img height="1" src="../images/dot.gif" border="0"></td>
                    </tr>
                    <tr>
                        <td style="width: 50%" valign="top">
                            <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                                <tr>
                                    <td class="titleField" style="width: 180px">Dealer</td>
                                    <td style="width: 2px">:</td>
                                    <td>
                                        <asp:Label ID="lblDealerCodeName" runat="server"></asp:Label>
                                        <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtDealerCode"
                                            onblur="omitSomeCharacter('txtDealerCode','<>?*%$')" runat="server" ToolTip="Dealer Search 1" Width="128px"></asp:TextBox>
                                        <asp:Label ID="lblPopUpDealer" runat="server" Width="16px" Style="display: none"> <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"> </asp:Label>
                                        <asp:Button ID="btnGetInfoDealer" runat="server" Text="..." Style="display: none"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleField" style="width: 180px">Tgl Pengajuan / Status</td>
                                    <td style="width: 2px">:</td>
                                    <td>
                                        <asp:HiddenField ID="hdnSubmitDate" runat="server" />
                                        <asp:Label ID="lblSubmitDate" runat="server" Text="27 May 2020"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lbl3Months" runat="server" Text="(Expired in 2 Months)"></asp:Label>&nbsp;
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50%" valign="top">
                            <table id="Table11" cellspacing="1" cellpadding="2" width="100%" border="0">
                                <tr>
                                    <td class="titleField" style="width: 180px">
                                        <asp:Label ID="Label32" runat="server">No. Reg Aplikasi</asp:Label>
                                    </td>
                                    <td style="width: 2px">:</td>
                                    <td>
                                        <asp:Label ID="lblProposalRegNo" runat="server" Text="[Auto Generated]"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleField">
                                        <asp:Label ID="label45" runat="server">Nomor Aplikasi Dealer</asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtDealerProposalNo" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                            onblur="omitSomeCharacter('txtDealerProposalNo','<>?*%$')" runat="server" Width="178px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>

            <div style="height: 1%"></div>
            <div style="width:1500px;">
                <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="Fleet Customer Data" Font-Size="15px" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td background="../images/bg_hor.gif" height="1" colspan="2">
                        <img height="1" src="../images/bg_hor.gif" border="0"></td>
                </tr>
            </table>
            </div>
            <div style="width:1500px;">
                <table width="80%">
                <tr>
                    <td style="width: 50%" valign="top">
                        <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tr>
                                <td class="titleField" style="width: 180px">Tipe Customer</td>
                                <td style="width: 2px">:</td>
                                <td>
                                    <asp:DropDownList ID="ddlCustomerType" runat="server" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="width: 180px">Kategori Fleet Customer</td>
                                <td style="width: 2px">:</td>
                                <td>
                                    <asp:DropDownList ID="ddlFleetCategory" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="titleField" style="width: 180px">Nama Fleet Customer</td>
                                <td style="width: 2px">:</td>
                                <td>
                                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtFleetCustomerName" Enabled="false"
                                        onblur="omitSomeCharacter('txtFleetCustomerName','<>?*%$')" runat="server" Width="178px"></asp:TextBox>
                                    <asp:Label ID="lblPopUpFleetCustomer" runat="server" Width="16px"> 
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"> </asp:Label>&nbsp;&nbsp;
                                    <asp:Button ID="btnBuatFleet" runat="server" Text="Buat Fleet Baru"></asp:Button>

                                    <asp:Button ID="btnReloadDataFleet" runat="server" style="display:none"></asp:Button>
                                    <asp:HiddenField ID="hdnFleetCustomerDetailID" runat="server" />
                                    <asp:HiddenField ID="hdnFleetCustomerHeaderID" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="width: 180px" valign="top">Alamat</td>
                                <td style="width: 2px">:</td>
                                <td class="titleField">
                                    <asp:TextBox ID="txtAddressFleetCustomerDtl" runat="server" Width="260px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trKTP" runat="server">
                                <td class="titleField" style="width: 180px">KTP</td>
                                <td style="width: 2px">:</td>
                                <td class="titleField">
                                    <asp:TextBox ID="txtNoKTP" onkeypress="return alphaNumericExcept(event,'<>?*%$')" Enabled="false"
                                        onblur="omitSomeCharacter('txtNoKTP','<>?*%$')" runat="server" Width="178px"></asp:TextBox>&nbsp;
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                        runat="server" Display="dynamic"
                                        ControlToValidate="txtNoKTP"
                                        ValidationExpression="^([\S\s]{0,16})$"
                                        ErrorMessage="Panjang karakter maksimal 16 digit"> </asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr id="trNIB" runat="server">
                                <td class="titleField" style="width: 180px">NIB</td>
                                <td style="width: 2px">:</td>
                                <td class="titleField">
                                    <asp:TextBox ID="txtNoNIB" onkeypress="return alphaNumericExcept(event,'<>?*%$')" Enabled="false"
                                        onblur="omitSomeCharacter('txtNoNIB','<>?*%$')" runat="server" Width="178px"></asp:TextBox>&nbsp;
                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                        runat="server" Display="dynamic"
                                        ControlToValidate="txtNoNIB"
                                        ValidationExpression="^([\S\s]{0,13})$"
                                        ErrorMessage="Panjang karakter maksimal 13 digit"> </asp:RegularExpressionValidator>--%>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="titleField" style="width: 180px">Nama di Faktur/ STNK</td>
                                <td style="width: 2px">:</td>
                                <td class="titleField">
                                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtNameOnFaktur" ReadOnly="true"
                                        onblur="omitSomeCharacter('txtNameOnFaktur','<>?*%$')" runat="server" ToolTip="" Width="260px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                    <asp:LinkButton ID="lnkPopUpNameOnFaktur" runat="server"> <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"> </asp:LinkButton>

                                    <%-- <asp:Label ID="lnkPopUpNameOnFaktur" runat="server" Width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                    </asp:Label>&nbsp;--%>
                                    <asp:Button ID="btnGetDataCustomer" runat="server" Text=".." Style="display: none"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="width: 180px">Area BBN</td>
                                <td style="width: 2px">:</td>
                                <td class="titleField">
                                    <asp:DropDownList ID="ddlBBNAreaProvince" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="width: 180px">Jenis Usaha</td>
                                <td style="width: 2px">:</td>
                                <td class="titleField">
                                    <asp:DropDownList ID="ddlBusinessSector" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="titleField" style="width: 180px">Data Terakhir Pembelian</td>
                                <td style="width: 2px">
                                    <asp:Label ID="Label10" runat="server">:</asp:Label></td>
                                <td class="titleField">
                                    <cc1:IntiCalendar ID="icLastPurchaseDate" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%" valign="top">
                        <table id="Table11" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tr>
                                <td class="titleField" style="width: 180px">Nama Proyek</td>
                                <td style="width: 2px">:</td>
                                <td class="titleField">
                                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtProjectName"
                                        onblur="omitSomeCharacter('txtProjectName','<>?*%$')" runat="server" Width="178px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="width: 180px">Metode Pengadaan Proyek</td>
                                <td style="width: 2px">:</td>
                                <td>
                                    <asp:DropDownList ID="ddlProjectKindMethod" runat="server" Width="140px" AutoPostBack="true"></asp:DropDownList>&nbsp
                                    <asp:TextBox ID="txtProjectKindMethodOther" runat="server" Width="180px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField">Penjualan Langsung oleh Dealer</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlDealerDirectSales" runat="server" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="width: 180px">Nama Kontraktor</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtContractorName"
                                        onblur="omitSomeCharacter('txtContractorName','<>?*%$')" runat="server" Width="178px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="width: 180px">Surat Perjanjian Jual Beli</td>
                                <td style="width: 2px">:</td>
                                <td valign="top">
                                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtSuratKomitmen" ReadOnly="true"
                                        onblur="omitSomeCharacter('txtSuratKomitmen','<>?*%$')" runat="server" Width="178px"></asp:TextBox>

                                    <input id="FUSuratKomitmentKontrak" onkeydown="return false;" style="width: 70px; height: 20px" type="file" size="25"
                                        name="FUSuratKomitmentKontrak" runat="server">
                                    <%--<asp:Label ID="lblSuratKomitmentKontrak" runat="server" Visible="false"></asp:Label>--%>
                                    <asp:LinkButton ID="LinkDownloadSuratKomitmentKontrak" runat="server" Visible="false"></asp:LinkButton> 
                                    <asp:Button ID="btnUploadSuratKomitmen" runat="server" Text="Upload" Style="display: none"></asp:Button>
                                    &nbsp;<asp:LinkButton ID="lbtnDeleteFileSuratKomitmen" OnClientClick="return confirm('Anda yakin mau hapus?');" Text="Hapus" runat="server"> <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="width: 180px"></td>
                                <td style="width: 2px"></td>
                                <td valign="top">
                                    <asp:LinkButton ID="LinkDownload" runat="server"><i>Template surat.docsx</i></asp:LinkButton>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="titleField" style="width: 180px">Surat Pernyataan<br />
                                    "Tidak untuk Keperluan Perang"</td>
                                <td style="width: 2px">:</td>
                                <td>
                                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtSuratPernyataan" ReadOnly="true"
                                        onblur="omitSomeCharacter('txtSuratPernyataan','<>?*%$')" runat="server" Width="178px"></asp:TextBox>
                                    <input id="FUSuratPernyataan" onkeydown="return false;" style="width: 70px; height: 20px" type="file" size="25"
                                        name="FUSuratPernyataan" runat="server">
                                    <%--<asp:Label ID="lblSuratPernyataan" runat="server" Visible="false"></asp:Label>--%>
                                    <%--perubahan untuk download dokumen--%>
                                    <asp:LinkButton ID="LinkDownloadSuratPernyataan" runat="server" Visible="false"></asp:LinkButton> 
                                    <asp:Button ID="btnUploadSuratPernyataan" runat="server" Text="Upload" Style="display: none"></asp:Button>
                                    &nbsp;<asp:LinkButton ID="lbtnDeleteFileSuratPernyataan" OnClientClick="return confirm('Anda yakin mau hapus?');" Text="Hapus" runat="server"> <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </div>

            <div style="height: 1%"></div>
            <div style="width:1500px;">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Kepemilikan Kendaraan" Font-Size="15px" Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr>
                        <td background="../images/bg_hor.gif" height="1" colspan="2">
                            <img height="1" src="../images/bg_hor.gif" border="0"></td>
                    </tr>
                </table>
            </div>

            <div style="width:900px;">
                <asp:DataGrid ID="dgKepemilikanKendaraan" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
                    CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                    <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                    <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                    <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                    <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="No.">
                            <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="" visible="false">
                            <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Brand">
                            <HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblBrandCategory" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                            <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlFBrandCategory" Style="width: 200px" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlFBrandCategory_SelectedIndexChanged">
                                </asp:DropDownList>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlEBrandCategory" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlFBrandCategory_SelectedIndexChanged">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Merk">
                            <HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblBrandNameCategory" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                            <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                            <FooterTemplate>
                                <asp:TextBox ID="txtFBrandNameCategory" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                    onblur="omitSomeCharacter(this.id,'<>?*%$')" runat="server" Width="200px"></asp:TextBox>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEBrandNameCategory" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                    onblur="omitSomeCharacter(this.id,'<>?*%$')" runat="server" Width="200px"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Model">
                            <HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblModel" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                            <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                            <FooterTemplate>
                                <asp:TextBox ID="txtFModel" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                    onblur="omitSomeCharacter(this.id,'<>?*%$')" runat="server" Width="200px"></asp:TextBox>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEModel" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                    onblur="omitSomeCharacter(this.id,'<>?*%$')" runat="server" Width="200px"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Unit">
                            <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblQtyUnit" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                            <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                            <FooterTemplate>
                                <asp:TextBox ID="txtFQtyUnit" Style="text-align: right" runat="server"
                                    onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="60px" />
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEQtyUnit" Style="text-align: right" runat="server" onkeypress="return NumericOnlyWith(event,'');"
                                    onkeyup="pic(this,this.value,'9999999999','N')" Width="60px" />
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEdit" CausesValidation="False" CommandName="edit" Text="Ubah" runat="server">
                                        <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Anda yakin mau hapus?');" CausesValidation="False" CommandName="delete" Text="Hapus" runat="server">
                                        <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Center"></FooterStyle>
                            <FooterTemplate>
                                <asp:LinkButton ID="lbtnAdd" CommandName="add" Text="Tambah" runat="server">
                                        <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbtnSave" CommandName="save" Text="Simpan" runat="server">
                                        <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancel" CommandName="cancel" Text="Batal" runat="server">
                                        <img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
            <br />

            <div id="divMMKSISide2" runat="server" style="width:1500px;">
                <div>
                    <table id="tblMMKSISide2" width="100%" runat="server">
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="<u>History Pembelian Kendaraan<u>" Font-Size="12px" Font-Bold="True"></asp:Label>&nbsp;&nbsp;

                                <asp:LinkButton ID="lnkReloadHistoryPembelian" runat="server" Width="16px" class="waitLoad">
                                        <img style="cursor:hand" alt="Reload Histori Pembelian" src="../images/reload.gif" border="0">
                                </asp:LinkButton>
                                <div id="Loading">
                                    <img src="../images/loader.gif" width="50px" height="50px" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 75%">
                    <asp:GridView ID="dgHistoryPembelian" runat="server" CssClass="grid" AutoGenerateColumns="True" PageSize="1000"
                        CellPadding="4" ShowFooter="True" BackColor="White" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#99CCCC" ForeColor="Black" Font-Bold="True" Height="15px"></FooterStyle>
                        <HeaderStyle CssClass="titleTableSales" Font-Bold="True" ForeColor="White"></HeaderStyle>
                        <PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="No">
                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <div style="height: 1%"></div>
            <div style="width:1500px;">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Detail Pembelian" Font-Size="15px" Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr>
                        <td background="../images/bg_hor.gif" height="1" colspan="2">
                            <img height="1" src="../images/bg_hor.gif" border="0"></td>
                    </tr>
                </table>
            </div>

            <div style="width:1000px;">
                <table width="100%">
                <tr>
                    <td style="width: 40%" valign="top">
                        <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tr>
                                <td class="titleField" style="width: 180px">Tujuan Pembelian</td>
                                <td style="width: 2px">:</td>
                                <td>
                                    <asp:DropDownList ID="ddlPurchaseKind" runat="server" Width="140px" AutoPostBack="true"></asp:DropDownList>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="width: 180px">Waktu Pengiriman</td>
                                <td style="width: 2px">:</td>
                                <td>
                                    <asp:TextBox onkeypress="return numericOnlyUniv(event)" ID="txtDeliveryPlanDate" runat="server" Width="72px" MaxLength="6"></asp:TextBox>
                                    <asp:Label ID="Label4" runat="server" Style="font-weight: bold; font-style: italic;">&nbsp;MMyyyy&nbsp;</asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDeliveryPlanDate" ErrorMessage="* Harap di isi.."></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="width: 180px">Area Pengiriman</td>
                                <td style="width: 2px">:</td>
                                <td>
                                    <asp:DropDownList ID="ddlDeliveryRegionCode" runat="server" Width="140px" AutoPostBack="true"></asp:DropDownList>&nbsp
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="titleField" style="width: 180px" valign="top">Lampiran PO/SPK</td>
                                <td style="width: 2px">:</td>
                                <td class="titleField">
                                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtLampiranPOSPK" ReadOnly="true"
                                        onblur="omitSomeCharacter('txtLampiranPOSPK','<>?*%$')" runat="server" Width="178px"></asp:TextBox>

                                    <input id="FULampiranPOSPK" onkeydown="return false;" style="width: 70px; height: 20px" type="file" size="25"
                                        name="FULampiranPOSPK" runat="server">
                                    <asp:Label ID="lblLampiranPOSPK" runat="server" Visible="false"></asp:Label>
                                    <asp:Button ID="btnUploadLampiranPOSPK" runat="server" Text="Upload" Style="display: none"></asp:Button>
                                    &nbsp;<asp:LinkButton ID="lbtnDeleteFileLampiranPOSPK" OnClientClick="return confirm('Anda yakin mau hapus?');" runat="server"> <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 60%" valign="top">
                        <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tr valign="top">
                                <td class="titleField" style="width: 180px">Metode Pembelian Customer</td>
                                <td style="width: 2px">:</td>
                                <td>
                                    <asp:DropDownList ID="ddlPurchaseMethod" runat="server" Width="140px" AutoPostBack="true"></asp:DropDownList>&nbsp
                                    <asp:DropDownList ID="ddlLeasing" runat="server" Style="display: none"></asp:DropDownList>&nbsp
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="titleField"></td>
                                <td style="width: 2px"></td>
                                <td>
                                    <asp:DropDownList ID="ddlAPMSubsidy" runat="server" Width="140px" Style="display: none"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="width: 180px">Metode Pembayaran Dealer</td>
                                <td style="width: 2px">:</td>
                                <td>
                                    <asp:DropDownList ID="ddlPaymentMethod" runat="server" Width="140px" AutoPostBack="true"></asp:DropDownList>&nbsp
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </div>

            <div style="height: 1%"></div>
            <div style="width:1500px;">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label13" runat="server" Text="Rincian Harga Kendaraan" Font-Size="15px" Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr>
                        <td background="../images/bg_hor.gif" height="1" colspan="2">
                            <img height="1" src="../images/bg_hor.gif" border="0"></td>
                    </tr>
                </table>
            </div>

            <div id="divAddVehicle" runat="server" style="width:1500px;">
                <asp:Button ID="btnAddVehicle" runat="server" Text="&nbsp;&nbsp;Tambah Kendaraan" class="hideButtonOnPrint" Style="width: 140px; background-image: url('../images/add.gif'); background-repeat: no-repeat"></asp:Button>&nbsp;
            </div>
            <div style="overflow-x: auto; width: 3500px">
                <asp:GridView ID="dgRincianHargaKendaraan" runat="server" CssClass="grid" AutoGenerateColumns="True" PageSize="1000"
                    CellPadding="4" ShowFooter="True" BackColor="White" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#99CCCC" ForeColor="Black" Font-Bold="True" Height="15px"></FooterStyle>
                    <HeaderStyle CssClass="titleTableSales" Font-Bold="True" ForeColor="White"></HeaderStyle>
                    <PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                    <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                    <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                    <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                </asp:GridView>
            </div>

            <div style="height: 1%"></div>
            <div style="width:1500px;">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Lain - lain" Font-Size="15px" Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr>
                        <td background="../images/bg_hor.gif" height="1" colspan="2">
                            <img height="1" src="../images/bg_hor.gif" border="0"></td>
                    </tr>
                </table>
            </div>

            <div style="width:1000px;">
                <table width="100%">
                <tr valign="top">
                    <td style="width: 50%" valign="top">
                        <table id="Table12" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tr valign="top">
                                <td class="titleField" style="width: 130px">Catatan</td>
                                <td style="width: 2px">:</td>
                                <td>
                                    <asp:TextBox ID="txtDealerNotes" runat="server" Width="300px" TextMode="MultiLine" Height="80px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%" valign="top">
                        <table id="Table13" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tr valign="top">
                                <td class="titleField" style="width: 150px">Lampiran PO/SPK</td>
                                <td style="width: 2px">:</td>
                                <td class="titleField">
                                    <div style="width: 50%">
                                        <asp:DataGrid ID="dgUploadFile" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1"
                                            AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                                            <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                                            <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                            <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="No.">
                                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Dokumen">
                                                    <HeaderStyle CssClass="titleTableSales" Width="20%"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnFileName" runat="server" CommandName="Download" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Path")%>'>
                                                            <asp:Label ID="lblFileName" runat="server" alt="Download"></asp:Label>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                    <FooterTemplate>
                                                        <input id="UploadFile" onkeydown="return false;" style="width: 267px; height: 20px" type="file" size="25" name="File1" runat="server">
                                                        <asp:Label ID="lblFileUpload" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Anda yakin mau hapus?');" CausesValidation="False" CommandName="Delete" Text="Hapus" runat="server">
                                                                <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnAdd" CommandName="add" Text="Tambah" runat="server">
                                                                <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table id="Table15" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tr valign="top">
                                <td style="width: 125px">
                                    <asp:Label ID="label123" runat="server">Penerima Email Notifikasi</asp:Label>
                                </td>
                                <td>:</td>
                                <td>
                                    <div style="width:755px;">
                                        <asp:DataGrid ID="dgEmailUser" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
                                            CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                                            <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                                            <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                            <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="No.">
                                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle Width="3%" HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Nama">
                                                    <HeaderStyle Width="14%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle Width="14%" HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRecipientName" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                                    <FooterTemplate>
                                                        <asp:HiddenField ID="hdnFDPEmailID" runat="server" />
                                                        <input ID="hdnFRecipientName" type="hidden" runat="server">
                                                        <asp:TextBox ID="txtFRecipientName" onkeypress="return alphaNumericExcept(event,'<>?*%$')" 
                                                            onblur="omitSomeCharacter(this.id,'<>?*%$');onBlurEmailName(this);" runat="server" Width="150px"></asp:TextBox>
                                                        <asp:Label ID="lblSearchFEmailUser" runat="server" TabIndex="0">
                                                        <img src="../images/popup.gif" style="cursor: pointer;" border="0" alt="Klik popup"></asp:Label>
                                                    </FooterTemplate>
                                                    <EditItemTemplate>
                                                        <asp:HiddenField ID="hdnEDPEmailID" runat="server" />
                                                        <input ID="hdnERecipientName" type="hidden" runat="server">
                                                        <asp:TextBox ID="txtERecipientName" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                                            onblur="omitSomeCharacter(this.id,'<>?*%$');onBlurEmailName(this);" runat="server" Width="150px"></asp:TextBox>
                                                        <asp:Label ID="lblSearchEEmailUser" runat="server" TabIndex="0">
                                                        <img src="../images/popup.gif" style="cursor: pointer;" border="0" alt="Klik popup"></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Jabatan">
                                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRecipientPosition" runat="server">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtFRecipientPosition" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                                            onblur="omitSomeCharacter(this.id,'<>?*%$')" runat="server" Width="200px"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtERecipientPosition" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                                            onblur="omitSomeCharacter(this.id,'<>?*%$')" runat="server" Width="200px"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Alamat Email">
                                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmail" runat="server">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtFEmail" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                                            onblur="omitSomeCharacter(this.id,'<>?*%$')" runat="server" Width="200px"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFEmail"
                                                            ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                                            Display="Dynamic" ErrorMessage="Alamat email tidak valid" />
                                                    </FooterTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEEmail" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                                            onblur="omitSomeCharacter(this.id,'<>?*%$')" runat="server" Width="200px"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEEmail"
                                                            ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                                            Display="Dynamic" ErrorMessage="Alamat email tidak valid" />
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnEdit" CausesValidation="False" CommandName="edit" Text="Ubah" runat="server">
                                                                <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Anda yakin mau hapus?');" CausesValidation="False" CommandName="delete" Text="Hapus" runat="server">
                                                                <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnAdd" CommandName="add" Text="Tambah" runat="server">
                                                                <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                                    </FooterTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="lbtnSave" CommandName="save" Text="Simpan" runat="server">
                                                                <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnCancel" CommandName="cancel" Text="Batal" runat="server">
                                                                <img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </div>

            <div style="height: 1%"></div>
            <div id="divMMKSISide" runat="server" style="width:1500px;">
                <div>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Proposed Discount" Font-Size="15px" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor.gif" height="1" colspan="2">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                    </table>
                </div>

                <div style="width: 1500px; height:100%; overflow: scroll">
                    <asp:DataGrid ID="dgProposedDiscount" runat="server" Width="1300px" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
                        CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                        <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                        <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                        <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                        <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="No.">
                                <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Model">
                                <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblModelKendaraan" runat="server"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlFModelKendaraan" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlFModelKendaraan_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlEModelKendaraan" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlFModelKendaraan_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Tipe">
                                <HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnLabelTotal" runat="server" />
                                    <asp:Label ID="lblTipeKendaraan" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlFTipeKendaraan" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlFTipeKendaraan_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlETipeKendaraan" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlFTipeKendaraan_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Unit">
                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFQuantity" Style="text-align: right" runat="server" onkeypress="return NumericOnlyWith(event,'');"
                                        onkeyup="pic(this,this.value,'9999999999','N')" Width="40px" />
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEQuantity" Style="text-align: right" runat="server"
                                        onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="40px" />
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Discount Type">
                                <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDiscountType" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlFDiscountType" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlFDiscountType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlEDiscountType" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlFDiscountType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Application No.">
                                <HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblApplicationNo" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                <FooterTemplate>
                                    <asp:HiddenField ID="hdnFSPLID" runat="server" OnValueChanged="hdnFSPLID_ValueChanged" />
                                    <asp:TextBox ID="txtFApplicationNo" onkeypress="return alphaNumericExcept(event,'<>?*%$')" OnTextChanged="txtFApplicationNo_TextChanged"
                                        onblur="omitSomeCharacter(this.id,'<>?*%$')" runat="server" Width="100px" AutoPostBack="true">
                                    </asp:TextBox>
                                    <asp:Label ID="lblSearchFApplicationNo" runat="server" TabIndex="0">
                                    <img src="../images/popup.gif" style="cursor: pointer;" border="0" alt="Klik popup"></asp:Label>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:HiddenField ID="hdnESPLID" runat="server" OnValueChanged="hdnFSPLID_ValueChanged" />
                                    <asp:TextBox ID="txtEApplicationNo" name="txtEApplicationNo" Width="90px" OnTextChanged="txtFApplicationNo_TextChanged"
                                        onkeypress="return alphaNumericExcept(event,'<>?*%$;')" AutoPostBack="true"
                                        onblur="omitSomeCharacter(this.id,'<>?*%$;')" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblSearchEApplicationNo" runat="server" TabIndex="0">
                                    <img src="../images/popup.gif" style="cursor: pointer;" border="0" alt="Klik popup"></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Detail Discount">
                                <HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDetailDiscount" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFDetailDiscount" Style="text-align: right" runat="server" 
                                        onkeyup="pic(this,this.value,'9999999999','N')" onkeypress="return NumericOnlyWith(event,'');" Width="80px" />
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEDetailDiscount" Style="text-align: right" runat="server"
                                        onkeyup="pic(this,this.value,'9999999999','N')" onkeypress="return NumericOnlyWith(event,'');" Width="80px" />
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Price Reff">
                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPriceReff" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFPriceReff" name="txtFPriceReff" onkeypress="return NumericOnlyWith(event,'');" Width="50px" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblSearchFPriceReff" runat="server" TabIndex="0">
                                    <img src="../images/popup.gif" style="cursor: pointer;" border="0" alt="Klik popup"></asp:Label>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEPriceReff" name="txtEPriceReff" onkeypress="return NumericOnlyWith(event,'');" Width="50px" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblSearchEPriceReff" runat="server" TabIndex="0">
                                    <img src="../images/popup.gif" style="cursor: pointer;" border="0" alt="Klik popup"></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="TOP">
                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTOP" runat="server" Text="0">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFTOP" Style="text-align: right" runat="server" onkeypress="return NumericOnlyWith(event,'');"
                                        onkeyup="pic(this,this.value,'9999999999','N')" Width="40px" Text="0" />
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtETOP" Style="text-align: right" runat="server" Text="0"
                                        onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="40px" />
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Bebas Bunga">
                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblInterest" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlFInterest" Width="50px" runat="server" AutoPostBack="false"></asp:DropDownList>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlEInterest" Width="50px" runat="server" AutoPostBack="false"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Delivery Time [MMyyyy]">
                                <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDeliveryTime" runat="server"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFDeliveryTime" Style="text-align: left" runat="server" MaxLength="6"
                                        onkeypress="return NumericOnlyWith(event,'');" Width="50px" />
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEDeliveryTime" Style="text-align: left" runat="server" MaxLength="6"
                                        onkeypress="return NumericOnlyWith(event,'');" Width="50px" />
                                    <%--<asp:Label ID="lblEFormatDeliveryTime" runat="server"><i>MMyyyy</i></asp:Label>--%>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnEdit" CausesValidation="False" CommandName="edit" Text="Ubah" runat="server">
                                        <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Anda yakin mau hapus?');" CausesValidation="False" CommandName="delete" Text="Hapus" runat="server">
                                        <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnAdd" CommandName="add" Text="Tambah" runat="server">
                                        <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="lbtnSave" CommandName="save" Text="Simpan" runat="server">
                                        <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnCancel" CommandName="cancel" Text="Batal" runat="server">
                                        <img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </div>
            <div style="width:1500px;">
                <table id="tblMMKSISide3" width="100%" runat="server">
                    <tr valign="top">
                        <td width="50%" class="titleField">
                            <u><asp:Label runat="server" Style="font-weight: bold" Text="Consideration :"></asp:Label></u>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:checkboxlist id="CBConsideration" runat="server" RepeatLayout="table" RepeatColumns="2" RepeatDirection="vertical"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%">
                            <table id="tblMMKSISide" runat="server" cellspacing="1" cellpadding="2" style="width:855px;" border="0">
                                <tr id="trFinalApproval" runat="server">
                                    <td></td>
                                    <td></td>
						            <TD class="titleField">
                                        <asp:checkbox id="chkFinalApproval" runat="server" Checked="False" Text="Final Approval oleh CFO"></asp:checkbox>
						            </TD>
                                </tr>
                                <tr valign="top">
                                    <td class="titleField" style="width: 100px">Comment</td>
                                    <td style="width: 2px">:</td>
                                    <td>
                                        <asp:TextBox ID="txtMMKSINotes" runat="server" Width="300px" TextMode="MultiLine" Height="80px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>

            <div style="width:1500px;">
                <hr />
            </div>

            <asp:Button ID="btnSave2" Style="display: none;width: 70px;" runat="server" Text="Simpan"></asp:Button>&nbsp;&nbsp;
            <asp:Button ID="btnSave" OnClientClick="return KonfirmasiSimpan(this)" Style="width: 70px" runat="server" Text="Simpan"></asp:Button>
            <asp:Button ID="btnBaru" runat="server" Text="Baru" Style="width: 70px"></asp:Button>
            <asp:Button ID="btnValidasi" runat="server" Text="Validasi" Style="width: 70px" OnClientClick="return confirm('Anda yakin mau ubah status ke Validasi ?');"></asp:Button>
            <asp:Button ID="btnHapus" OnClientClick="return confirm('Anda yakin mau hapus data ini?');" runat="server" Text="Hapus" Style="width: 70px"></asp:Button>&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnKembali" runat="server" Text="Kembali" class="hideButtonOnPrint" Style="width: 70px" Visible="false"></asp:Button>
            <asp:Button ID="btnRetrieveDetailDiscount" runat="server" Text=".." Style="width: 70px;display:none"></asp:Button>
            <asp:Button ID="btnReloadConfirm" runat="server" Text=".." Style="width: 70px;display:none"></asp:Button>
            <br />
        </div>

        <asp:LinkButton ID="lnkGenerateExcel" visible="false" runat="server" Text=".."></asp:LinkButton>
        <asp:LinkButton ID="lnkAppointmentLetter" visible="false" runat="server" Text=".."></asp:LinkButton>

        <div id="PanelDataCustomer" runat="server" style="display: none">
            <div>
                <table width="100%">
                    <tr>
                        <td class="titlePage">
                            <asp:Label ID="Label5" runat="server" Text="DISCOUNT PROPOSAL - Input Customer" Font-Size="15px" Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr>
                        <td background="../images/bg_hor.gif" height="1" colspan="2">
                            <img height="1" src="../images/bg_hor.gif" border="0"></td>
                    </tr>
                </table>
            </div>

            <div style="width: 1080px; overflow: auto;">
                <asp:HiddenField ID="hdnCustomerCode" runat="server" />
                <input id="hdnIndexSelectedGrid" type="hidden" value="0" runat="server">

                <asp:DataGrid ID="dgDPCustomer" runat="server" Width="50%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
                    CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                    <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                    <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                    <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                    <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="No.">
                            <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Nama">
                            <HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server"></asp:Label>
                            </ItemTemplate>
                            <FooterStyle Wrap="False" HorizontalAlign="Left"></FooterStyle>
                            <FooterTemplate>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtFName" 
                                    onblur="omitSomeCharacter(this.id,'<>?*%$')" runat="server" Width="250px">
                                </asp:TextBox>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtEName" 
                                    onblur="omitSomeCharacter(this.id,'<>?*%$')" runat="server" Width="250px">
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="NIB/KTP *">
                            <HeaderStyle Width="18%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblIdentityNumber" runat="server"></asp:Label>
                            </ItemTemplate>
                            <FooterStyle Wrap="False" HorizontalAlign="Left"></FooterStyle>
                            <FooterTemplate>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtFIdentityNumber" 
                                    onblur="omitSomeCharacter(this.id,'<>?*%$')" runat="server" Width="180px">
                                </asp:TextBox>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtEIdentityNumber" 
                                    onblur="omitSomeCharacter(this.id,'<>?*%$')" runat="server" Width="180px">
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEdit" CausesValidation="False" CommandName="edit" Text="Ubah" runat="server">
                                    <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Anda yakin mau hapus?');" CausesValidation="False" CommandName="delete" Text="Hapus" runat="server">
                                    <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Center"></FooterStyle>
                            <FooterTemplate>
                                <asp:LinkButton ID="lbtnAdd" CommandName="add" Text="Tambah" runat="server">
                                    <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbtnSave" CommandName="save" Text="Simpan" runat="server">
                                    <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancel" CommandName="cancel" Text="Batal" runat="server">
                                    <img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
                <table width="50%">
                    <tr align="left">
                        <td align="left">
                            <label style="color:red"><i>* NIB : Nomor Induk Berusaha</i></label><br />
                            <label style="color:red"><i>&nbsp;KTP : Kartu Tanda Penduduk</i></label>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:Button ID="btnKembaliToMain" runat="server" Text="Kembali" Style="width: 70px"></asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
