<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmSalesmanPart.aspx.vb" Inherits="FrmSalesmanPart" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmAplikasiHeader</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript">
        /* Deddy H	validasi value *********************************** */
        /* ini untuk handle char yg tdk diperbolehkan, saat paste */
        function TxtBlur(objTxt) {
            omitSomeCharacter(objTxt, '<>?*%$;');
        }
        function TxtBlurName(objTxt) {
            omitSomeCharacterExcludeSingleQuote(objTxt, '<>?*%$;');
        }
        /* ini untuk handle char yg tdk diperbolehkan, saat keypress */
        function TxtKeypress() {
            return alphaNumericExcept(event, '<>?*%$;');
        }
    </script>
    <script language="javascript" type="text/javascript">

        function HandleSparePartPanel() {
            var chkSparePart = document.getElementById("SparePartIndicator");
            var pnl = document.getElementById("pnlSparePart");
            if (chkSparePart.checked) {
                pnl.style.display = "block";
            }
            else {
                pnl.style.display = "none";
            }

        }

        function ShowPopUpSuperior() {
            var ddlJobPositionDesc = document.getElementById("ddlJobPositionDesc");
            var Position = ddlJobPositionDesc.value;
            var lblDealerCodes = document.getElementById("lblDealerCode");
            var oDealerSalesman = lblDealerCodes.innerHTML
            if (ddlJobPositionDesc.value == '') {
                alert('Kategori harus dipilih dulu !');
                return;
            }
            //showPopUp('../PopUp/PopUpSalesman.aspx?PositionID=' + Position,'',600,600,SuperiorSelection);
            showPopUp('../PopUp/PopUpSalesman.aspx?PositionID=' + Position + '&DealerSalesman=' + oDealerSalesman, '', 600, 600, SuperiorSelection);
        }

        function SuperiorSelection(SelectedSuperior) {

            var tempParam = SelectedSuperior.split(';');
            var txtSuperior = document.getElementById("txtSuperior");
            var txtSuperiorName = document.getElementById("txtSuperiorName");
            //alert(txtSuperior);

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtSuperior.innerText = tempParam[0];
                txtSuperiorName.innerText = tempParam[1];
            }
            else {
                txtSuperior.value = tempParam[0];
                txtSuperiorName.value = tempParam[1];
            }
        }


        function ShowPopUpDealerHistory() {
            showPopUp('../PopUp/PopUpSelectingDealer.aspx', '', 600, 600, DealerSelectionHistory);
        }


        function DealerSelectionHistory(SelectedHistory) {
            var dtgHistory = document.getElementById("dtgHistory");
            var indek = GetCurrentInputIndex(dtgHistory);
            //alert(indek);
            var tempParam = SelectedHistory.split(';');
            var KodeArea = dtgHistory.rows[indek].getElementsByTagName("INPUT")[0];
            var DescArea = dtgHistory.rows[indek].getElementsByTagName("SPAN")[1];

            if (navigator.appName == "Microsoft Internet Explorer") {
                KodeArea.innerText = tempParam[0];
                DescArea.innerHTML = tempParam[1];
            }
            else {
                KodeArea.value = tempParam[0];
                DescArea.value = tempParam[1];
            }
        }

        function ShowPopUpSalesManArea() {
            //var myDate = new Date( );
            //showPopUp('../PopUp/PopUpSalesmanArea.aspx?time='+myDate.getTime( ),'',600,600,AreaSelection);
            showPopUp('../PopUp/PopUpSalesmanArea.aspx', '', 600, 600, AreaSelection);
        }


        function AreaSelection(SelectedArea) {

            var dtgArea = document.getElementById("dtgArea");
            var indek = GetCurrentInputIndex(dtgArea);
            var tempParam = SelectedArea.split(';');
            var KodeArea = dtgArea.rows[indek].getElementsByTagName("INPUT")[0];
            var DescArea = dtgArea.rows[indek].getElementsByTagName("SPAN")[1];

            if (navigator.appName == "Microsoft Internet Explorer") {
                KodeArea.innerText = tempParam[0];
                DescArea.innerHTML = tempParam[1];
            }
            else {
                KodeArea.value = tempParam[0];
                DescArea.value = tempParam[1];
            }
        }

        function GetCurrentInputIndex(dtg) {
            //var dtgArea = document.getElementById("dtgArea");
            var currentRow;
            var index = 0;
            var inputs;
            var indexInput;
            for (index = 0; index < dtg.rows.length; index++) {
                inputs = dtg.rows[index].getElementsByTagName("INPUT");

                if (inputs != null && inputs.length > 0) {
                    for (indexInput = 0; indexInput < inputs.length; indexInput++) {
                        if (inputs[indexInput].type != "hidden")
                            return index;
                    }
                }
            }
            return -1;
        }


        function SetDealer(Parameter) {
            arrParam = Parameter.split(";");
            document.Form1.txtDealerCode.value = trim(arrParam[0]);
            document.Form1.txtDealerName.value = trim(arrParam[1]);

        }

        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpSelectingDealer.aspx?multi=' + true, '', 600, 600, DealerSelection);
        }
        function DealerSelection(selectedDealer) {
            var txtDealerCodeSelection = document.getElementById("txtDealerCode");
            var txtDealerNameSelection = document.getElementById("txtDealerName");
            var arrValue = selectedDealer.split(';');

            txtDealerCodeSelection.value = arrValue[0];
            txtDealerNameSelection.value = arrValue[1];
        }

        function ShowJobPosSelection() {
            showPopUp('../PopUp/PopUpJobPosition.aspx?Menu=1', '', 600, 600, JobPosSelection);
        }
        function JobPosSelection(selectedJobPos) {
            var txtJobPos = document.getElementById("txtJobPosition");
            var txtJobPosDesc = document.getElementById("txtJobPositionDesc");
            var arrValue = selectedJobPos.split(';');
            txtJobPos.value = arrValue[0];
            txtJobPosDesc.value = arrValue[1];

        }
        // popUp City
        function ShowCitySelection() {
            showPopUp('../PopUp/PopUpCity.aspx', '', 600, 600, CitySelection);
        }
        function CitySelection(selectedCity) {
            var txtKota = document.getElementById("txtKota");
            var arrValue = selectedCity.split(';');
            txtKota.value = arrValue[1];
        }

        // comment
        function ShowLeadJobPosSelection() {
            showPopUp('../PopUp/PopUpJobPosition.aspx?Menu=1', '', 600, 600, LeadJobPosSelection);
        }
        function LeadJobPosSelection(selectedLeadJobPos) {
            var txtLeadJobPos = document.getElementById("txtLeadJobPosition");
            var txtLeadJobPosDesc = document.getElementById("txtLeadJobPositionDesc");
            var arrValue = selectedLeadJobPos.split(';');
            txtLeadJobPos.value = arrValue[0];
            txtLeadJobPosDesc.value = arrValue[1];
        }

        function ShowSalesmanSelection() {
            var lblSalesmanCode = document.getElementById("lblSalesmanCode");
            alert(lblSalesmanCode.innerText);
            showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx?SSCode=' + lblSalesmanCode.innerText, '', 600, 600, SalesmanSelection);
        }

        function SalesmanSelection(SelectedSalesman) {
            var tempParam = SelectedSalesman.split(';');
            var txtLeaderName = document.getElementById("txtLeaderName");
            var txtLeadJobPositionDesc = document.getElementById("txtLeadJobPositionDesc");
            var txtLeadJobPosition = document.getElementById("txtLeadJobPosition");
            var txtSalesmanCode = document.getElementById("txtSalesmanCode");

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtLeaderName.innerText = tempParam[1];
                txtLeadJobPositionDesc.innerText = tempParam[3];
            }
            else {
                txtLeaderName.value = tempParam[1];
                txtLeadJobPositionDesc.value = tempParam[3];
            }
            txtSalesmanCode.value = tempParam[0]
            txtLeadJobPosition.value = tempParam[2];
        }

        function ShowSalesmanResign() {
            var txtMode = document.getElementById("txtMode");
            showPopUp('../PopUp/PopUpSalesmanPartResign.aspx?Code=Resign' + '&Mode=' + txtMode.value, '', 500, 760, SalesmanResignSelection);
        }

        function SalesmanResignSelection(selectedVal) {
            var tempParam = selectedVal.split(';');
            var txtRefSalesman = document.getElementById("txtRefSalesman");
            txtRefSalesman.value = tempParam[0];
        }
        function ShowPopUpPartShop() {
            //alert('test');
            showPopUp('../PopUp/PopUppartshop.aspx', '', 500, 600, PartShopSelection);
        }

        function PartShopSelection(SelectedPartShop) {
            var dgPartshop = document.getElementById("dgPartshop");
            var indek = GetCurrentInputIndex(dgPartshop);
            var tempParam = SelectedPartShop.split(';');
            var Code = dgPartshop.rows[indek].getElementsByTagName("INPUT")[0];
            var Name = dgPartshop.rows[indek].getElementsByTagName("SPAN")[1];
            var City = dgPartshop.rows[indek].getElementsByTagName("SPAN")[2];

            if (navigator.appName == "Microsoft Internet Explorer") {
                Code.innerText = tempParam[0];
                Name.innerHTML = tempParam[1];
                City.innerHTML = tempParam[2];
            }
            else {
                Code.value = tempParam[0];
                Name.value = tempParam[1];
                City.value = tempParam[2];
            }
        }

        function ShowPPDealerBranchSelection() {
            var dealerCode = document.getElementById("lblDealerCode").innerText;
            showPopUp('../Service/../PopUp/PopUpDealerBranchSelectionSingle.aspx?m=d&deal=' + dealerCode, '', 500, 760, TemporaryOutlet);
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
        <table id="Table2" cellspacing="0" cellpadding="0" width="720" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">
                    <asp:Label ID="lblPageTitle" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td class="titleField" width="180">Indicator</td>
                                <td>
                                    <asp:Label ID="Label5" runat="server">:</asp:Label></td>
                                <td width="263">
                                    <asp:DropDownList ID="ddlIndicator" runat="server"></asp:DropDownList><asp:Label ID="lblIndicator" runat="server" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="Requiredfieldvalidator9" runat="server" ControlToValidate="ddlIndicator" ErrorMessage="Indicator Harus dipilih">*</asp:RequiredFieldValidator></td>
                                <td colspan="3">
                                    <asp:Label ID="lblRef" runat="server">Part Employee Ref :</asp:Label>&nbsp;&nbsp;&nbsp;
										<asp:TextBox onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');" ID="txtRefSalesman"
                                            onblur="omitSomeCharacter('txtRefKodePelanggan','<>?*%$;');" runat="server"
                                            Width="120px" MaxLength="14"></asp:TextBox><asp:Label ID="lbtnRefSalesman" onclick="ShowSalesmanResign();" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                            </asp:Label><asp:LinkButton ID="lnkReloadSalesman" runat="server" CausesValidation="False">
											<img src="../images/reload.gif" style="cursor:hand" border="0" alt="Reload"></asp:LinkButton></td>
                            </tr>
                            <tr valign="top">
                                <td class="titleField" width="180">Kode Dealer</td>
                                <td>
                                    <asp:Label ID="lblColon3" runat="server">:</asp:Label></td>
                                <td width="263">
                                    <asp:Label ID="lblDealerCode" runat="server"></asp:Label></td>
                                <td colspan="3" rowspan="9">
                                    <table cellspacing="1" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td>Insert Photo</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divPhoto" style="overflow: auto; width: 184px; height: 105px" align="center">
                                                    <asp:Image ID="photoView" runat="server" Width="100%" Height="100%"></asp:Image>
                                                </div>
                                                <asp:Button ID="btnRemoveFile" Visible="False" runat="server" CausesValidation="False" Text="Remove Picture"></asp:Button><asp:LinkButton ID="lblRemoveImage" runat="server" CausesValidation="False" Text="Hapus Photo" CommandName="deleteImage">
														<img src="../images/trash.gif" border="0" alt="Hapus Photo"></asp:LinkButton></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input onkeypress="return false;" id="photoSrc" tabindex="19" type="file" size="29" name="File1"
                                                    runat="server">
                                                <asp:Label ID="lblInputIdentityId" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="SalesUnitIndicator" TabIndex="20" runat="server" Visible="False" Text="Sales Unit"></asp:CheckBox><br>
                                                <asp:CheckBox ID="MechanicIndicator" TabIndex="21" runat="server" Visible="False" Text="Mekanik"></asp:CheckBox><br>
                                                <asp:CheckBox ID="SparePartIndicator" onclick="HandleSparePartPanel();" TabIndex="22" runat="server"
                                                    Visible="False" Text="Spare Part"></asp:CheckBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlSparePart" runat="server" Width="178px">
                                                    <asp:CheckBox ID="AdmIndicator" TabIndex="23" runat="server" Visible="False" Text="Administrasi (ADM)"></asp:CheckBox>
                                                    <br>
                                                    <asp:CheckBox ID="WHIndicator" TabIndex="24" runat="server" Visible="False" Text="Warehouse (WH)"></asp:CheckBox>
                                                    <br>
                                                    <asp:CheckBox ID="CounterIndicator" TabIndex="25" runat="server" Visible="False" Text="Counter"></asp:CheckBox>
                                                    <br>
                                                    <asp:CheckBox ID="SalesIndicator" TabIndex="26" runat="server" Visible="False" Text="Sales"></asp:CheckBox>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="180">Nama Dealer</td>
                                <td>
                                    <asp:Label ID="Label1" runat="server">:</asp:Label></td>
                                <td width="263">
                                    <asp:Label ID="lblDealerName" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="180">Cabang Dealer</td>
                                <td>
                                    <asp:Label ID="Label11" runat="server">:</asp:Label></td>
                                <td width="263">
                                    <asp:TextBox ID="txtDealerBranchCode" Width="150px" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblPopUpDealerBranch" runat="server" Width="10"> 
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                    </asp:Label>
                                    <asp:Label ID="lblDealerBranchCode" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField">Nama Cabang Dealer</td>
                                <td>
                                    <asp:Label ID="Label15" runat="server">:</asp:Label>
                                </td>
                                <td>
                                    <asp:textbox id="txtBranchName" Width="200px" Runat="server" disabled=""></asp:textbox>
                                    <asp:Label ID="lblDealerBranchName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="180">Grade Dealer</td>
                                <td>
                                    <asp:Label ID="Label9" runat="server">:</asp:Label></td>
                                <td width="263">
                                    <asp:Label ID="lblGradeDealer" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="180">
                                    <asp:Label ID="lblKodeSalesman" runat="server"></asp:Label></td>
                                <td>:</td>
                                <td nowrap width="263">
                                    <asp:Label ID="lblSalesmanCode" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="180">
                                    <asp:Label ID="lblAreaSingleTitle" runat="server" Visible="False">Area</asp:Label></td>
                                <td>
                                    <asp:Label ID="lblAreasd" runat="server" Visible="False">:</asp:Label></td>
                                <td nowrap width="263">
                                    <asp:DropDownList ID="DdlArea" runat="server"></asp:DropDownList><asp:Label ID="lblArea" runat="server"></asp:Label><asp:RequiredFieldValidator ID="Areafieldvalidator" runat="server" ControlToValidate="DdlArea" ErrorMessage="Area Harus dipilih">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="180">Nama</td>
                                <td>:</td>
                                <td nowrap width="263">
                                    <asp:TextBox onkeypress="TxtKeypress();" ID="txtName" onblur="TxtBlurName('txtName');" TabIndex="4"
                                        runat="server" Width="128px" MaxLength="60"></asp:TextBox><asp:Label ID="lblName" runat="server"></asp:Label><asp:RequiredFieldValidator ID="valName" runat="server" ControlToValidate="txtName" ErrorMessage="Nama Harus diisi">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr valign="top">
                                <td class="titleField" width="180">Tempat/Tgl Lahir</td>
                                <td>:</td>
                                <td style="width: 263px">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td style="width: 117px">
                                                <asp:TextBox onkeypress="TxtKeypress();" ID="txtPlaceOfBirth" onblur="TxtBlur('txtPlaceOfBirth');"
                                                    TabIndex="5" runat="server" Width="110px" MaxLength="60"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPlaceOfBirth"
                                                        ErrorMessage="Tempat Lahir Harus diisi">*</asp:RequiredFieldValidator><asp:Label ID="lblPlaceOfBirth" runat="server"></asp:Label></td>
                                            <td style="width: 4px">/</td>
                                            <td>
                                                <cc1:IntiCalendar ID="ICDateOfBirth" TabIndex="6" runat="server"></cc1:IntiCalendar><asp:Label ID="lblDateOfBirth" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="180">Jenis Kelamin</td>
                                <td>:</td>
                                <td nowrap width="263">
                                    <asp:DropDownList ID="ddlGender" TabIndex="7" runat="server"></asp:DropDownList><asp:Label ID="lblGender" runat="server"></asp:Label><asp:RequiredFieldValidator ID="Requiredfieldvalidator7" runat="server" ControlToValidate="ddlGender" ErrorMessage="Jenis Kelamin Harus dipilih">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="180">Status Perkawinan</td>
                                <td>:</td>
                                <td nowrap width="263">
                                    <asp:DropDownList ID="ddlMarriedStatus" TabIndex="7" runat="server"></asp:DropDownList><asp:Label ID="lblMarriedStatus" runat="server"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlMarriedStatus"
                                        ErrorMessage="Status Perkawinan Harus dipilih">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="180">Agama</td>
                                <td>:</td>
                                <td nowrap width="263">
                                    <asp:DropDownList ID="ddlReligion" TabIndex="7" runat="server"></asp:DropDownList><asp:Label ID="lblReligion" runat="server"></asp:Label></td>
                            </tr>
                            <tr valign="top">
                                <td class="titleField" width="180">Alamat</td>
                                <td>:</td>
                                <td nowrap width="263">
                                    <asp:TextBox onkeypress="TxtKeypress();" ID="txtAlamat" onblur="TxtBlur('txtAlamat');" TabIndex="8"
                                        runat="server" Width="232px" MaxLength="200" TextMode="MultiLine"></asp:TextBox><asp:Label ID="lblAlamat" runat="server"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAlamat" ErrorMessage="Alamat Harus diisi">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="180" style="height: 21px">Propinsi</td>
                                <td style="height: 21px">:</td>
                                <td nowrap width="263" style="height: 21px">
                                    <asp:DropDownList ID="ddlPropinsi" runat="server" AutoPostBack="True"></asp:DropDownList><asp:Label ID="lblPropinsi" runat="server" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="Requiredfieldvalidator8" runat="server" ControlToValidate="ddlPropinsi" ErrorMessage="Propinsi Harus dipilih">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="180">Kota</td>
                                <td>:</td>
                                <td nowrap width="263">
                                    <asp:DropDownList ID="ddlKota" runat="server"></asp:DropDownList><asp:Label ID="lblKota" runat="server"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlKota" ErrorMessage="Kota Harus dipilih">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td colspan="3" width="100%">
                                    <asp:Panel ID="Panel1" TabIndex="18" runat="server" Width="100%"></asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 20px" width="180">Kategori</td>
                                <td style=" height: 20px" >:</td>
                                <td style="width: 263px; height: 20px" nowrap width="263">
                                    <asp:DropDownList ID="ddlJobPositionDesc" runat="server" AutoPostBack="True"></asp:DropDownList><asp:Label ID="lblJobPositionDesc" runat="server"></asp:Label><asp:RequiredFieldValidator ID="Requiredfieldvalidator5" runat="server" ControlToValidate="ddlJobPositionDesc"
                                        ErrorMessage="Kategori Harus dipilih">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 20px" width="180">Posisi</td>
                                <td style=" height: 20px" >:</td>
                                <td style="width: 263px; height: 20px" nowrap width="263">
                                    <asp:DropDownList ID="ddlSalesmanLevel" TabIndex="12" runat="server" AutoPostBack="True"></asp:DropDownList><asp:Label ID="lblSalesmanLevel" runat="server"></asp:Label><asp:RequiredFieldValidator ID="Requiredfieldvalidator6" runat="server" ControlToValidate="ddlSalesmanLevel"
                                        ErrorMessage="Level Harus dipilih">*</asp:RequiredFieldValidator></td>
                                <td class="titleField" colspan="4">
                                    <asp:Panel ID="pnlSuperior" runat="server">
                                        <asp:Label ID="lblSuperior" runat="server">Atasan :</asp:Label>
                                        <table>
                                            <tr>
                                                <td width="70">
                                                    <asp:TextBox onkeypress="TxtKeypress();" ID="txtSuperior" onblur="TxtBlur('txtName');" TabIndex="4"
                                                        runat="server" MaxLength="10" Width="60px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="ValAtasan" runat="server" ErrorMessage="Atasan Harus diisi" ControlToValidate="txtSuperior">*</asp:RequiredFieldValidator></td>
                                                <td>
                                                    <asp:TextBox onkeypress="TxtKeypress();" ID="txtSuperiorName" onblur="TxtBlur('txtName');" TabIndex="4"
                                                        runat="server" BorderColor="Silver" BorderStyle="None"></asp:TextBox></td>
                                                <td>
                                                    <asp:Label ID="Label4" onclick="ShowPopUpSuperior();" runat="server">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                                    </asp:Label></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 20px" width="180">
                                    <asp:Label ID="lblLevelText" runat="server">Level</asp:Label></td>
                                <td style=" height: 20px" >
                                    <asp:Label ID="lblLevelSeparator" runat="server">:</asp:Label></td>
                                <td style="width: 263px; height: 20px" nowrap width="263">
                                    <asp:DropDownList ID="ddlGrade" runat="server" AutoPostBack="false"></asp:DropDownList><asp:Label ID="lblGrade" runat="server"></asp:Label><asp:RequiredFieldValidator ID="Requiredfieldvalidator10" runat="server" ControlToValidate="ddlGrade" ErrorMessage="Level harus dipilih">*</asp:RequiredFieldValidator></td>
                </td>
                <td class="titleField" style="width: 136px; height: 20px" width="136"></td>
                <td style="height: 20px" width="1%"></td>
                <td style="height: 20px" width="29%"></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 20px" width="180">Tgl Masuk</td>
                <td style=" height: 20px" >:</td>
                <td style="width: 263px; height: 20px" nowrap width="263">
                    <cc1:IntiCalendar ID="ICStartWork" TabIndex="15" runat="server" CanPostBack="True"></cc1:IntiCalendar><asp:Label ID="lblStartWork" runat="server"></asp:Label></td>
                <td class="titleField" style="width: 136px; height: 20px" width="136">
                    <input id="txtMode" style="width: 88px; height: 20px" type="hidden" size="9" name="txtMode"
                        runat="server"></td>
                <td style="height: 20px" width="1%"></td>
                <td style="height: 20px" width="29%"></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 20px" width="180">Tgl Keluar</td>
                <td style=" height: 20px" >:</td>
                <td style="width: 263px; height: 20px" nowrap width="263">
                    <cc1:IntiCalendar ID="ICEndWork" runat="server" Enabled="False"></cc1:IntiCalendar><asp:Label ID="lblEndWork" runat="server"></asp:Label></td>
                <td class="titleField" style="width: 136px; height: 20px" width="136">
                    <input id="txtJobPosition" style="width: 88px; height: 20px" type="hidden" size="9" name="txtJobPosition"
                        runat="server"></td>
                <td style="height: 20px" width="1%"></td>
                <td style="height: 20px" width="29%"></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 20px" width="180">Gaji</td>
                <td style=" height: 20px" >:</td>
                <td style="width: 263px; height: 20px" nowrap width="263">
                    <asp:TextBox ID="txtSalary" runat="server" Width="120px" MaxLength="60"></asp:TextBox><asp:Label ID="lblSalary" runat="server"></asp:Label></td>
                <td class="titleField" style="width: 136px; height: 20px" width="136"></td>
                <td style="height: 20px" width="1%"></td>
                <td style="height: 20px" width="29%"></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 20px" width="180">Alasan</td>
                <td style=" height: 20px" >:</td>
                <td style="width: 263px; height: 20px" nowrap width="263">
                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtResignReason" TabIndex="17" runat="server"
                        Width="200px" MaxLength="50" Enabled="False"></asp:TextBox><asp:Label ID="lblResignReason" runat="server"></asp:Label></td>
                <td class="titleField" style="width: 136px; height: 20px" width="136"></td>
                <td style="height: 20px" width="1%"></td>
                <td style="height: 20px" width="29%"></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 20px" width="180">Status</td>
                <td style=" height: 20px" >:</td>
                <td style="width: 263px; height: 20px" nowrap width="263">
                    <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList><asp:Label ID="lblStatus" runat="server"></asp:Label></td>
                <td class="titleField" style="width: 136px; height: 20px" width="136"></td>
                <td style="height: 20px" width="1%"></td>
                <td style="height: 20px" width="29%"></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 20px" width="180"></td>
                <td style=" height: 20px" ></td>
                <td style="width: 263px; height: 20px" nowrap width="263"></td>
                <td class="titleField" style="width: 136px; height: 20px" width="136"></td>
                <td style="height: 20px" width="1%"></td>
                <td style="height: 20px" width="29%"></td>
            </tr>
            <tr valign="top">
                <td class="titleField" style="height: 20px" width="180">
                    <asp:Label ID="lblPartshop" runat="server"> Part Shop</asp:Label></td>
                <td style=" height: 20px" ></td>
                <td nowrap colspan="4">
                    <div id="div1" style="overflow: auto; height: 100px">
                        <asp:DataGrid ID="dgPartshop" runat="server" Width="100%" BorderColor="Gainsboro" PageSize="25"
                            CellSpacing="1" AllowSorting="true" BorderWidth="0px" CellPadding="3" ShowFooter="true" AutoGenerateColumns="False" BackColor="#CDCDCD" AllowPaging="true"
                            AllowCustomPaging="true">
                            <SelectedItemStyle Font-Bold="true" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                            <HeaderStyle Font-Bold="true" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titletableRsd"></HeaderStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="PartShop.PartShopCode" HeaderText="Kode Part Shop">
                                    <HeaderStyle Width="20%" CssClass="titletableRsd"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartShopCode1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartShop.PartShopCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtPartShopCode" TabIndex="10" MaxLength="6" runat="server" Width="70" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
                                            onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
                                        <asp:Label ID="lblPartshopCode" TabIndex="20" runat="server" Height="10px">
												<img style="cursor:hand" alt="Klik disini untuk memilih Part Shop" src="../images/popup.gif"
													border="0"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="PartShop.Name" HeaderText="Nama Part Shop">
                                    <HeaderStyle HorizontalAlign="Right" Width="40%" CssClass="titletableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartshopName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartShop.Name") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblPartshopNameF" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="PartShop.CityPart.CityName" HeaderText="Kota">
                                    <HeaderStyle HorizontalAlign="Right" Width="30%" CssClass="titletableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartshopCity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartShop.CityPart.CityName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblPartshopCityF" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titletableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbDeleteParthop" CausesValidation="False" CommandName="delete" Text="Hapus" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbAddParthop" CausesValidation="False" CommandName="add" Text="Tambah" runat="server">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="titleField" style="height: 20px" width="180"></td>
                <td style=" height: 20px" ></td>
                <td style="width: 263px; height: 20px" nowrap width="263">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Width="208px"></asp:ValidationSummary>
                </td>
                <td class="titleField" style="width: 136px; height: 20px" width="136"></td>
                <td style="height: 20px" width="1%"></td>
                <td style="height: 20px" width="29%"></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 20px" width="180"></td>
                <td style=" height: 20px" ></td>
                <td style="height: 20px" colspan="4">
                    <asp:Button ID="btnSimpan" TabIndex="27" runat="server" Width="60px" Text="Simpan"></asp:Button>
                    <asp:Button ID="btnRequestID" TabIndex="28" runat="server" Text="Request ID"></asp:Button>
                    <asp:Button ID="btnBatal" TabIndex="29" runat="server" Width="60px" CausesValidation="False" Text="Batal"></asp:Button>
                    <asp:Button ID="btnSearch" TabIndex="30" runat="server" Width="60px" CausesValidation="False" Text="Kembali"></asp:Button>
                </td>
            </tr>
            <tr>
                <td class="titleField" style="height: 20px" width="180"></td>
                <td style=" height: 20px" ></td>
                <td style="width: 263px; height: 20px" nowrap width="238"></td>
                <td class="titleField" style="width: 136px; height: 20px" width="136"></td>
                <td style="height: 20px" width="1%"></td>
                <td style="height: 20px" width="29%"></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 20px" width="180"></td>
                <td style=" height: 20px" ></td>
                <td style="width: 263px; height: 20px" nowrap width="238"></td>
                <td class="titleField" style="width: 136px; height: 20px" width="136"></td>
                <td style="height: 20px" width="1%"></td>
                <td style="height: 20px" width="29%"></td>
            </tr>
            <tr valign="top">
                <td class="titleField" style="height: 20px" width="180">
                    <asp:Label ID="TitleHistory" runat="server"> History Pekerjaan Salesman</asp:Label></td>
                <td style=" height: 20px" ></td>
                <td nowrap colspan="4">
                    <div id="div1" style="overflow: auto; height: 100px">
                        <asp:DataGrid ID="dtgHistory" runat="server" Width="100%" BorderColor="Gainsboro" PageSize="25"
                            CellSpacing="1" AllowSorting="True" BorderWidth="0px" CellPadding="3" ShowFooter="True" AutoGenerateColumns="False" BackColor="#CDCDCD" AllowPaging="True"
                            AllowCustomPaging="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer Tempat Kerja Sebelumnya">
                                    <HeaderStyle Width="20%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") + " - " + DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtDealerHistory" TabIndex="10" MaxLength="6" runat="server" Width="70" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
                                            onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
                                        <asp:Label ID="Label7" TabIndex="20" runat="server" Height="10px">
												<img style="cursor:hand" alt="Klik Disini untuk memilih Dealer" src="../images/popup.gif"
													border="0" onclick="ShowPopUpDealerHistory();"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="DateIn" HeaderText="Tanggal Masuk">
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.DateIn"),"dd/MM/yyyy") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <cc1:IntiCalendar ID="dtDateIn" TabIndex="6" runat="server"></cc1:IntiCalendar>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="DateOut" HeaderText="Tanggal Keluar">
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label10" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.DateOut"),"dd/MM/yyyy") %>' CssClass="textRight">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <cc1:IntiCalendar ID="dtDateOut" TabIndex="6" runat="server"></cc1:IntiCalendar>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" Width="15%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LbtnDeleteHistory" CausesValidation="False" CommandName="delete" Text="Hapus" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="Linkbutton9" CausesValidation="False" CommandName="add" Text="Tambah" runat="server">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr valign="top">
                <td class="titleField" style="height: 20px" width="180">
                    <asp:Label ID="lblTarget" runat="server">Target</asp:Label></td>
                <td style=" height: 20px" ></td>
                <td nowrap colspan="4">
                    <div id="div1" style="overflow: auto; height: 100px">
                        <asp:DataGrid ID="dtgTarget" runat="server" Width="100%" BorderColor="Gainsboro" CellSpacing="1"
                            AllowSorting="True" BorderWidth="0px" CellPadding="3" ShowFooter="True" AutoGenerateColumns="False" BackColor="#CDCDCD" AllowPaging="True"
                            AllowCustomPaging="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="YearTarget" HeaderText="Tahun">
                                    <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblYearTarget" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.YearTarget") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlYearTargetAdd" runat="server"></asp:DropDownList>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlYearTargetEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.YearTarget") %>'>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="MonthTarget" HeaderText="Bulan">
                                    <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonthTarget" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlMonthTargetAdd" runat="server"></asp:DropDownList>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlMonthTargetEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MonthTarget") %>'>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ValueTarget" HeaderText="Target Penjualan (Rp)">
                                    <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbValueTarget" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.ValueTarget"),"#,##0") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtValueTargetAdd" runat="server" onkeyup="pic(this,this.value,'999999999','N')"
                                            onkeypress="return NumericOnlyWith(event,'');" onblur="NumOnlyBlurWithOnGridTxt(this,'');"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtValueTargetEdit" runat="server" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'999999999','N')" onblur="NumOnlyBlurWithOnGridTxt(this,'');" Text='<%# DataBinder.Eval(Container, "DataItem.ValueTarget") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="aksi">
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEditTarget" CausesValidation="False" CommandName="editTarget" Text="Ubah"
                                            runat="server">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDeleteTarget" CausesValidation="False" CommandName="deleteTarget" Text="Hapus"
                                            runat="server">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="Linkbutton5" runat="server" CausesValidation="False" CommandName="addTarget">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lbtnSaveTarget" TabIndex="40" CommandName="saveTarget" Text="Simpan" runat="server"
                                            CausesValidation="False">
												<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                        <asp:LinkButton ID="Linkbutton7" TabIndex="50" CommandName="cancelTarget" Text="Batal" runat="server"
                                            CausesValidation="False">
												<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
        <table cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="titleField">
                    <asp:Label ID="lblAreaPemasaran" runat="server">Area Pemasaran</asp:Label></td>
            </tr>
            <tr>
                <td class="titleField">
                    <div id="div1" style="overflow: auto; height: 100px">
                        <asp:DataGrid ID="dtgArea" runat="server" Width="100%" BorderColor="Gainsboro" PageSize="25" CellSpacing="1"
                            AllowSorting="True" BorderWidth="0px" CellPadding="3" ShowFooter="True" AutoGenerateColumns="False" BackColor="#CDCDCD" AllowPaging="True"
                            AllowCustomPaging="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="SalesmanArea.AreaCode" HeaderText="Kode Area">
                                    <HeaderStyle Width="20%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanArea.AreaCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAreaCode" TabIndex="10" runat="server" Width="70" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
                                            onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
                                        <asp:Label ID="lblPopUpSalesManArea" TabIndex="20" runat="server" Height="10px">
												<img style="cursor:hand" alt="Klik Disini untuk memilih Part Number" src="../images/popup.gif"
													border="0"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="SalesmanArea.AreaDesc" HeaderText="Nama Area">
                                    <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanArea.AreaDesc") %>' CssClass="textRight">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblAreaDesc" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" Width="15%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDelete" CausesValidation="False" CommandName="delete" Text="Hapus" runat="server">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnAdd" CausesValidation="False" CommandName="add" Text="Tambah" runat="server">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="lblTraining" runat="server">Training</asp:Label></td>
            </tr>
            <tr>
                <td>
                    <div id="div1" style="overflow: auto; height: 100px">
                        <asp:DataGrid ID="dtgTraining" runat="server" Width="100%" BorderColor="Gainsboro" PageSize="25"
                            CellSpacing="1" AllowSorting="True" BorderWidth="0px" CellPadding="3" ShowFooter="True" AutoGenerateColumns="False" BackColor="#CDCDCD" AllowPaging="True"
                            AllowCustomPaging="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrainingModule" HeaderText="Modul">
                                    <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblModulTraining" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrainingModule") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtModulTrainingAdd" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
                                            onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtModulTrainingEdit" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" Text='<%# DataBinder.Eval(Container, "DataItem.TrainingModule") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrainingPlaceAndDate" HeaderText="Tempat/Tanggal">
                                    <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTempatTanggal" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrainingPlaceAndDate") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTempatTanggalAdd" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
                                            onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtTempatTanggalEdit" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" Text='<%# DataBinder.Eval(Container, "DataItem.TrainingPlaceAndDate") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrainingProvider" HeaderText="Penyelenggara">
                                    <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPenyelenggara" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrainingProvider") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtPenyelenggaraAdd" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
                                            onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPenyelenggaraEdit" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" Text='<%# DataBinder.Eval(Container, "DataItem.TrainingProvider") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="aksi">
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEditTrain" CausesValidation="False" CommandName="editTrain" Text="Ubah"
                                            runat="server">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDeleteTrain" CausesValidation="False" CommandName="deleteTrain" Text="Hapus"
                                            runat="server">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="Linkbutton1" runat="server" CausesValidation="False" CommandName="addTrain">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lbtnSaveTrain" TabIndex="40" CommandName="saveTrain" Text="Simpan" runat="server"
                                            CausesValidation="False">
												<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                        <asp:LinkButton ID="Linkbutton6" TabIndex="50" CommandName="cancelTrain" Text="Batal" runat="server"
                                            CausesValidation="False">
												<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="lblPengalaman" runat="server">Pengalaman</asp:Label></td>
            </tr>
            <tr>
                <td>
                    <div id="div1" style="overflow: auto; height: 100px">
                        <asp:DataGrid ID="dtgExperience" runat="server" Width="100%" BorderColor="Gainsboro" PageSize="25"
                            CellSpacing="1" AllowSorting="True" BorderWidth="0px" CellPadding="3" ShowFooter="True" AutoGenerateColumns="False" BackColor="#CDCDCD" AllowPaging="True"
                            AllowCustomPaging="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="YearExperience" HeaderText="Tahun">
                                    <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblYearExperience" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.YearExperience") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtYearExperienceAdd" runat="server" MaxLength="4" onkeypress="return NumericOnlyWith(event,'');"
                                            onblur="NumOnlyBlurWithOnGridTxt(this,'');"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtYearExperienceEdit" runat="server" MaxLength="4" onkeypress="return NumericOnlyWith(event,'');" onblur="NumOnlyBlurWithOnGridTxt(this,'');" Text='<%# DataBinder.Eval(Container, "DataItem.YearExperience") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Company" HeaderText="Perusahaan">
                                    <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCompany" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Company") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtCompanyAdd" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
                                            onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCompanyEdit" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" Text='<%# DataBinder.Eval(Container, "DataItem.Company") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="JobPosition" HeaderText="Posisi">
                                    <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPosisi" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.JobPosition") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtJobPositionAdd" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
                                            onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtJobPositionEdit" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" Text='<%# DataBinder.Eval(Container, "DataItem.JobPosition") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="aksi">
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEditExp" CausesValidation="False" CommandName="editExp" Text="Ubah" runat="server">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDeleteExp" CausesValidation="False" CommandName="deleteExp" Text="Hapus"
                                            runat="server">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="Linkbutton2" runat="server" CausesValidation="False" CommandName="addExp">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lbtnSave" TabIndex="40" CommandName="saveExp" Text="Simpan" runat="server" CausesValidation="False">
												<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" TabIndex="50" CommandName="cancelExp" Text="Batal" runat="server"
                                            CausesValidation="False">
												<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="lblPrestasi" runat="server">Prestasi</asp:Label></td>
            </tr>
            <tr>
                <td>
                    <div id="div1" style="overflow: auto; height: 100px">
                        <asp:DataGrid ID="dtgPrestasi" runat="server" Width="100%" BorderColor="Gainsboro" PageSize="25"
                            CellSpacing="1" AllowSorting="True" BorderWidth="0px" CellPadding="3" ShowFooter="True" AutoGenerateColumns="False" BackColor="#CDCDCD" AllowPaging="True"
                            AllowCustomPaging="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="AccomplishYear" HeaderText="Tahun">
                                    <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccomplishYear" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AccomplishYear") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAccomplishYearAdd" runat="server" MaxLength="4" onkeypress="return NumericOnlyWith(event,'');"
                                            onblur="NumOnlyBlurWithOnGridTxt(this,'');"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtAccomplishYearEdit" runat="server" MaxLength="4" onkeypress="return NumericOnlyWith(event,'');" onblur="NumOnlyBlurWithOnGridTxt(this,'');" Text='<%# DataBinder.Eval(Container, "DataItem.AccomplishYear") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Accomplishment" HeaderText="Prestasi">
                                    <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccomplishment" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Accomplishment") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAccomplishmentAdd" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
                                            onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtAccomplishmentEdit" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" Text='<%# DataBinder.Eval(Container, "DataItem.Accomplishment") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="aksi">
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEditAccomp" CausesValidation="False" CommandName="editAccomp" Text="Ubah"
                                            runat="server">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDeleteAccomp" CausesValidation="False" CommandName="deleteAccomp" Text="Hapus"
                                            runat="server">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="Linkbutton3" runat="server" CausesValidation="False" CommandName="addAccomp">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lbtnSaveAccomp" TabIndex="40" CommandName="saveAccomp" Text="Simpan" runat="server"
                                            CausesValidation="False">
												<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                        <asp:LinkButton ID="Linkbutton4" TabIndex="50" CommandName="cancelAccomp" Text="Batal" runat="server"
                                            CausesValidation="False">
												<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
        <input id="hdnVal" type="hidden" name="hdnVal" runat="server">
        <input id="IsInsertSuccess" type="hidden" name="IsInsertSuccess" runat="server">
    </form>
    <script type="javascript">
        alert('Hello');
        var IsInsertSuccess = document.getElementById("IsInsertSuccess");
        if (IsInsertSuccess.value == '1') {
            alert('Data berhasil disimpan');
        }
        //(function () {
        //    var tId = setInterval(function () {
        //        if (document.readyState == "complete") onComplete()
        //    }, 11);
        //    function onComplete() {
        //        clearInterval(tId);
        //        var labelIdentityId = document.getElementById('labelPhoto');
        //        labelIdentityId.innerHTML = 'Hello';
        //    };
        //})()
    </script>
</body>
</html>
