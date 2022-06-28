<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmPreCustomerUpdateForm.aspx.vb" Inherits="FrmPreCustomerUpdateForm" SmartNavigation="False" MaintainScrollPositionOnPostback="true" %>

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
        /* Deddy H	validasi value *********************************** */
        /* ini untuk handle char yg tdk diperbolehkan, saat paste */
        function TxtBlur(objTxt) {
            omitSomeCharacter(objTxt, '<>?*%$;');
        }
        /* ini untuk handle char yg tdk diperbolehkan, saat keypress */
        function TxtKeypress() {
            return alphaNumericExcept(event, '<>?*%$;')
        }

        //function js untuk handle alphanumeric, dengan menghilangkan karakter numeric
        function alphaNumericNonNumeric(event) {
            if (navigator.appName == "Microsoft Internet Explorer")
                pressedKey = event.keyCode;
            else
                pressedKey = event.which

            if ((pressedKey == 32) || (pressedKey >= 97 && pressedKey <= 122) || (pressedKey >= 65 && pressedKey <= 90)) {
                return true;
            }
            else {
                return false;
            }
        }

        function TxtBlurNonNumeric(objTxt) {
            omitSomeCharacter(objTxt, '<>?*%$;0123456789');
        }

        function textCounter(field, maxlimit) {
            if (field.value.length > maxlimit) {
                field.value = field.value.substring(0, maxlimit);
            } else {
                //countfield.value = maxlimit - field.value.length;
            }
        }

        // ******************
        function ShowPopUpSAPRegisterSalesman() {

            //var txtSapNo = document.getElementById("txtSapNo");
            //showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx','',460,760,SAPRegisterSelection);
            showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx?FilterIndicator=Unit&IsGroupDealer=0', '', 500, 760, SAPRegisterSelection);
        }

        function SAPRegisterSelection(SelectedSalesman) {

            //var indek = GetCurrentInputIndex();
            var dgSAPCustomer = document.getElementById("dgSAPCustomer");
            var tempParam = SelectedSalesman.split(';');
            var txtSalesmanID = document.getElementById("txtSalesmanID");
            var txtSalesmanName = document.getElementById("txtSalesmanName");

            txtSalesmanName.value = tempParam[1];
            txtSalesmanID.value = tempParam[0];
            //__doPostBack('__Page', 'searchsalesman');
        }

        function ShowPopUpVechileType() {
            showPopUp('../PopUp/PopUpVechileType.aspx?CategoryID=1&IsActive=A', '', 500, 760, VechileTypeSelection);
        }

        function ShowPPEventDealerSelection() {
            showPopUp('../PopUp/PopUpBabitEventProposalSelectionOne.aspx', '', 500, 760, EventDealerSelection);
        }

        function EventDealerSelection(selectedEvent) {
            var data = selectedEvent.split(";");

            var txtCampaignName = document.getElementById("txtCampaignName");
            txtCampaignName.value = data[1];

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtCampaignName.focus();
                txtCampaignName.blur();
            }
            else {
                txtCampaignName.onchange();
            }
        }

        function ShowPopUpCustomer() {
            showPopUp('../PopUp/PopUpCustomerName.aspx', '', 500, 760, CustomerSelection);
        }

        function CustomerSelection(SelectedCustomer) {
            var indek = GetCurrentInputIndex();
            var dgSAPCustomer = document.getElementById("dgSAPCustomer");
            var tempParam = SelectedCustomer.split(';');
            // input berupa teks box, urutan dikolom
            var txtCustomerName = dgSAPCustomer.rows[indek].getElementsByTagName("INPUT")[0];
            var txtCustomerCode = dgSAPCustomer.rows[indek].getElementsByTagName("INPUT")[1];
            var txtCustomerAddress = dgSAPCustomer.rows[indek].getElementsByTagName("INPUT")[2];
            // span berupa label
            ///var DescArea = dgSAPCustomer.rows[indek].getElementsByTagName("SPAN")[1];

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtCustomerName.innerText = tempParam[1];
                txtCustomerCode.innerText = tempParam[0];
                txtCustomerAddress.innerText = tempParam[2];
                //DescArea.innerHTML = tempParam[1];	
            }
            else {
                txtCustomerName.value = tempParam[1];
                //DescArea.value = tempParam[1];
            }
        }
        function GetCurrentInputIndex() {
            var dgSAPCustomer = document.getElementById("dgSAPCustomer");
            var currentRow;
            var index = 0;
            var inputs;
            var indexInput;

            for (index = 0; index < dgSAPCustomer.rows.length; index++) {
                inputs = dgSAPCustomer.rows[index].getElementsByTagName("INPUT");

                if (inputs != null && inputs.length > 0) {
                    for (indexInput = 0; indexInput < inputs.length; indexInput++) {
                        if (inputs[indexInput].type != "hidden")
                            return index;
                    }
                }
            }
            return -1;
        }

        function VechileTypeSelection(SelectedVechileType) {
            var tempParam = SelectedVechileType.split(';');
            var VechileTypeCode = document.getElementById("txtVehicleType");
            VechileTypeCode.value = replace(tempParam[0], ' ', '');
            __doPostBack('txtVehicleType')
        }

        function ShowPPSAP() {
            showPopUp('../SparePart/../PopUp/PopUpSAP.aspx?x=Territory', '', 500, 760, SAPSelection);
        }

        function SAPSelection(selectedSAP) {
            var tempParam = selectedSAP.split(';');

            var txtSAPNo = document.getElementById("txtSAPNo");
            var lblDateFrom = document.getElementById("lblDateFrom");
            var lblDateUntil = document.getElementById("lblDateUntil");
            var txtPeriod = document.getElementById("txtPeriod");

            txtSAPNo.value = tempParam[0];
            lblDateFrom.innerText = tempParam[1];
            lblDateUntil.innerText = tempParam[2];
            txtPeriod.value = tempParam[1] + ';' + tempParam[2];


        }

        function SetSalesmanCode(selectedSales, mode) {

            if (selectedSales != '') {
                var indek = GetCurrentInputIndex();
                var dgSAPCustomer = document.getElementById("dgSAPCustomer");
                var txtSalesmanCode = document.getElementById("txtSalesmanCode");
                // setting posisi berdasarkan urutan kolom di grid
                var lblSalesmanCode = dgSAPCustomer.rows[indek].getElementsByTagName("SPAN")[1];
                lblSalesmanCode.innerText = selectedSales.value;
                txtSalesmanCode.value = selectedSales.value;
            }
        }

        function ShowCustomerList() {
            var txtRefKodePelanggan = document.getElementById("txtRefKodePelanggan");
            var text = txtRefKodePelanggan.value;
            //alert(text);
            showPopUp('../PopUp/PopUpCustomerList.aspx?Tyjiuy678=code&tipe=' + text, '', 500, 760, CustomerSelection);

            //var ddl = document.getElementById("ddlCostumerType");
            //if (ddl) {
            //    var val = ddl.options[ddl.selectedIndex].value;
            //    var text = ddl.options[ddl.selectedIndex].text + '.';
            //    alert(text);
            //    showPopUp('../PopUp/PopUpCustomerList.aspx?Tyjiuy678=code&tipe=' + text, '', 500, 760, CustomerSelection);
            //}
            //else {
            //    showPopUp('../PopUp/PopUpCustomerList.aspx?Tyjiuy678=code', '', 500, 760, CustomerSelection);
            //}
        }

        function CustomerSelection(selectedCustomer) {
            var txtRefKodePelanggan = document.getElementById("txtRefKodePelanggan");
            txtRefKodePelanggan.value = selectedCustomer;
        }

        //CR SPK
        function ShowPopUpSPKMasterCountryCode() {

            //var txtSapNo = document.getElementById("txtSapNo");
            showPopUp('../PopUp/PopUpSPKMasterCountry.aspx', '', 460, 760, MasterCountrySelection);
            //showPopUp('../PopUp/PopUpSPKMasterCountry.aspx?FilterIndicator=Unit&IsGroupDealer=0', '', 500, 760, SAPRegisterSelection);
        }

        function MasterCountrySelection(SelectedSalesman) {

            //var indek = GetCurrentInputIndex();
            //var dgSAPCustomer = document.getElementById("dgSAPCustomer");
            var tempParam = SelectedSalesman.split(';');
            var txtCountryCode = document.getElementById("txtCountryCode");
            var txtCountryName = document.getElementById("txtTelp");

            //txtCountryCode.value = tempParam[0];
            //txtCountryName.value = tempParam[1];
            txtCountryCode.value = replace(tempParam[0], ' ', '');
            //__doPostBack('__Page', 'search');
        }

        function changesCampaign(obj) {
            __doPostBack(obj);
        }

        function Right(str, chr) {
            return str.slice(str.length - chr, str.length)
        }

        function reCallDisc(price) {
            if (price.value == '') price.value = 0;

            var txtHargaPrice = 0
            var ddlHargaPriceListDealer = document.getElementById("ddlHargaPriceListDealer");
            if (ddlHargaPriceListDealer) {
                txtHargaPrice = ddlHargaPriceListDealer.options[ddlHargaPriceListDealer.selectedIndex].text
            }
            if (txtHargaPrice == 'Silahkan Pilih') txtHargaPrice = 0;

            var txtHargaPriceList = replaceAll(replaceAll(txtHargaPrice, ".", ""), ",", "");
            var pricevalue = replaceAll(replaceAll(price.value, ".", ""), ",", "");
            var TxtJumlahDiskonView = 0
            if (parseFloat(txtHargaPriceList) > 0) {
                if (parseFloat(txtHargaPriceList) > parseFloat(pricevalue)) {
                    TxtJumlahDiskonView = parseFloat(txtHargaPriceList) - parseFloat(pricevalue)
                }
            }

            if (Right(TxtJumlahDiskonView.toLocaleString(), 3) == '.00') {
                TxtJumlahDiskonView = replaceAll(TxtJumlahDiskonView.toLocaleString(), ".00", "")
            }
            else {
                TxtJumlahDiskonView = TxtJumlahDiskonView.toLocaleString()
            }
            var TxtJumlahDiskon = document.getElementById("TxtJumlahDiskon");
            TxtJumlahDiskon.value = replaceAll(TxtJumlahDiskonView.toLocaleString(), ",", ".")
            __doPostBack(price.id);
        }

        function replaceAll(str, find, replace) {
            return str.replace(new RegExp(escapeRegExp(find), 'g'), replace);
        }
        function escapeRegExp(string) {
            return string.replace(/[.*+\-?^${}()|[\]\\]/g, '\\$&'); // $& means the whole matched string
        }

    </script>
    <style type="text/css">
        .auto-style1 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            height: 21px;
            width: 14%;
        }

        .auto-style3 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            height: 20px;
            width: 14%;
        }

        .auto-style4 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            height: 20px;
            width: 7px;
        }

        .auto-style6 {
            height: 20px;
            width: 19%;
        }

        .auto-style7 {
            width: 290px;
        }

        .auto-style8 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            height: 21px;
            width: 15%;
        }
        .auto-style9 {
            height: 21px;
        }
        .auto-style10 {
            width: 290px;
            height: 21px;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">Marketing - Update Prospektif Konsumen</td>
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
                <td>
                    <table id="titleUmum" border="0" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titlePanel"><b>INFORMASI DEALER</b></td>
                        </tr>
                        <tr>
                            <td height="1" background="../images/bg_hor.gif">
                                <img border="0" src="../images/bg_hor.gif" height="1"></td>
                        </tr>
                    </table>
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="auto-style1">Dealer</td>
                            <td style="height: 21px" width="1%">
                                <asp:Label ID="lblColon3" runat="server">:</asp:Label></td>
                            <td colspan="3">
                                <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                                &nbsp - &nbsp
                                <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                            </td>
                            <td style="height: 21px" width="29%">
                                <input id="txtSalesmanCode" type="hidden" runat="server" name="txtSalesmanCode"></td>
                        </tr>
                        <tr>
                            <td class="auto-style3">Salesman</td>
                            <td style="height: 20px" width="1%">
                                <asp:Label ID="Label2" runat="server">:</asp:Label></td>
                            <td style="height: 20px" nowrap>
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtSalesmanID" TabIndex="14" runat="server"
                                    MaxLength="50" Width="70px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator6" runat="server" ControlToValidate="txtSalesmanID" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtSalesmanName" TabIndex="14" runat="server"
                                    MaxLength="50" Width="130px"></asp:TextBox>
                                <asp:Label ID="lblSearchSalesman" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>
                                <asp:LinkButton ID="lbtnSearchSalesman" Style="display: none" runat="server">Dont remove</asp:LinkButton>
                            <td class="auto-style4"></td>
                            <td class="auto-style6"></td>
                            <td style="height: 20px" width="29%"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="titleUmum" border="0" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titlePanel"><b>INFORMASI KONSUMEN</b></td>
                        </tr>
                        <tr>
                            <td height="1" background="../images/bg_hor.gif">
                                <img border="0" src="../images/bg_hor.gif" height="1"></td>
                        </tr>
                    </table>
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="auto-style8">Tipe Konsumen</td>
                            <td style="height: 21px" width="1%">
                                <asp:Label ID="Label19" runat="server">:</asp:Label></td>
                            <td class="auto-style7">
                                <asp:DropDownList ID="ddlCostumerType" runat="server" Width="274px" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td class="auto-style8">Referensi Konsumen</td>
                            <td style="height: 21px" width="1%"></td>
                            <td>
                                <asp:Label ID="lblRefKodePlgn" runat="server" Visible="False"></asp:Label>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');" ID="txtRefKodePelanggan"
                                    onblur="omitSomeCharacter('txtRefKodePelanggan','<>?*%$;');" runat="server" Width="137px"
                                    MaxLength="10"></asp:TextBox>
                                <asp:Label ID="lbtnRefKode" onclick="ShowCustomerList();" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>
                                <asp:LinkButton ID="lnkReloadPlg" runat="server" CausesValidation="False">
											<img src="../images/reload.gif" style="cursor:hand" border="0" alt="Reload">
                                </asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">Nama Konsumen</td>
                            <td style="height: 21px" width="1%">
                                <asp:Label ID="Label20" runat="server">:</asp:Label></td>
                            <td class="auto-style7">
                                <asp:TextBox ID="txtCustomerName" runat="server" Width="274px" MaxLength="40" 
                                    onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');"
									onblur="omitSomeCharacterExcludeSingleQuote('txtCustomerName','<>?*%$;');" 
                                    onkeyup="autofocus(this,'txtCustomerName');"
									onchange="this.value=this.value.toUpperCase();">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator5" runat="server" ControlToValidate="txtCustomerName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1"
                                        ControlToValidate="txtCustomerName"
                                        ValidationExpression="^[\s\S]{0,40}$"
                                        ErrorMessage="Panjang maksimal 40 karakter"></asp:RegularExpressionValidator>
                            </td>
                            <td class="auto-style8">Sumber Informasi</td>
                            <td style="height: 21px" width="1%">
                                <asp:Label ID="Label8" runat="server">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlType" runat="server" Width="274px"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlType" InitialValue="0" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">Alamat Konsumen</td>
                            <td style="height: 21px" width="1%">
                                <asp:Label ID="Label5" runat="server">:</asp:Label></td>
                            <td class="auto-style7">
                                <asp:TextBox ID="txtCustomerAddress" runat="server" TextMode="MultiLine" Height="46px" Width="274px" MaxLength="60" 
                                    onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');"
									onblur="omitSomeCharacterExcludeSingleQuote('txtCustomerAddress','<>?*%$;');" 
                                    onkeydown="textCounter(this.form.txtCustomerAddress,60);"
				                    onkeyup="textCounter(this.form.txtCustomerAddress,60);"
									onchange="this.value=this.value.toUpperCase();">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ControlToValidate="txtCustomerAddress" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="valInput"
                                        ControlToValidate="txtCustomerAddress"
                                        ValidationExpression="^[\s\S]{0,60}$"
                                        ErrorMessage="Panjang maksimal 60 karakter"></asp:RegularExpressionValidator>
                            </td>
                            <td class="auto-style8">Sumber Lead</td>
                            <td style="height: 21px" width="1%">
                                <asp:Label ID="Label10" runat="server">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlSource" runat="server" Width="274px"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSource" InitialValue="0" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">Phone</td>
                            <td style="height: 21px" width="1%">
                                <asp:Label ID="Label7" runat="server">:</asp:Label></td>
                            <td class="auto-style7">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtCountryCode" TabIndex="14" runat="server"
									MaxLength="50" Width="50px"></asp:TextBox>
								<asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" ControlToValidate="txtCountryCode" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtTelp" runat="server" Width="180px" MaxLength="50" onkeypress="return numericOnlyUniv(event)"
											onblur="numericOnlyBlur(txtTelp)"> </asp:TextBox>
                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ControlToValidate="txtTelp" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblSearchCountryCode" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
								</asp:Label>
								<asp:LinkButton ID="lbtnSearchCountryCode" Style="display: none" runat="server">Dont remove</asp:LinkButton>
                            </td>
                            <td class="auto-style8">Tujuan Konsumen</td>
                            <td style="height: 21px" width="1%">
                                <asp:Label ID="Label12" runat="server">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlPurpose" runat="server" Width="274px"></asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlPurpose" InitialValue="0" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">Email</td>
                            <td style="height: 21px" width="1%">
                                <asp:Label ID="Label9" runat="server">:</asp:Label></td>
                            <td class="auto-style7">
                                <asp:TextBox ID="txtEmail" runat="server" Width="274px" MaxLength="50" onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');"
                                    onchange="this.value=this.value.toUpperCase();"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            </td>
                            <td class="auto-style8">Status</td>
                            <td style="height: 21px" width="1%">
                                <asp:Label ID="Label1" runat="server">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="274px"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlStatus" InitialValue="99" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">Usia</td>
                            <td style="height: 21px" width="1%">
                                <asp:Label ID="Label11" runat="server">:</asp:Label></td>
                            <td class="auto-style7">
                                <asp:DropDownList ID="ddlAge" runat="server" Width="137px"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlAge" InitialValue="0" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td class="auto-style8">Curr. Vehicle Brand</td>
                            <td style="height: 21px" width="1%">
                                <asp:Label ID="Label14" runat="server">:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtCurrVehicleBrand" runat="server" Width="274px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">Jenis Kelamin</td>
                            <td style="height: 21px" width="1%">
                                <asp:Label ID="Label13" runat="server">:</asp:Label></td>
                            <td class="auto-style7">
                                <asp:DropDownList ID="ddlGender" runat="server" Width="137px"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlGender" InitialValue="0" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td class="auto-style8">Curr. Vehicle Type</td>
                            <td style="height: 21px" width="1%">
                                <asp:Label ID="Label16" runat="server">:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtCurrVehicleType" runat="server" Width="274px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">Tipe Kendaraan</td>
                            <td width="1%" class="auto-style9">
                                <asp:Label ID="Label15" runat="server">:</asp:Label></td>
                            <td class="auto-style10">
                                <asp:TextBox ID="txtVehicleType" runat="server" Width="137px" onkeypress="return HtmlCharUniv(event)" AutoPostBack="true"></asp:TextBox> &nbsp;&nbsp;
                                <asp:Label ID="lblVehicleType" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label></td>
                            <%--<asp:RequiredFieldValidator ID="Requiredfieldvalidator12" runat="server" ControlToValidate="txtVehicleType" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            <td class="auto-style8">Note</td>
                            <td width="1%" class="auto-style9">
                                <asp:Label ID="Label18" runat="server">:</asp:Label></td>
                            <td class="auto-style9">
                                <asp:TextBox ID="txtNote" runat="server" Width="274px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">Quantity</td>
                            <td style="height: 21px" width="1%">
                                <asp:Label ID="Label17" runat="server">:</asp:Label></td>
                            <td class="auto-style7">
                                <asp:TextBox ID="txtQty" runat="server" Width="137px" MaxLength="5" onkeypress="return numericOnlyUniv(event)"
                                    onblur="numericOnlyBlur(txtQty)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator9" runat="server" ControlToValidate="txtQty" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td class="auto-style8">Kode Registrasi</td>
                            <td style="height: 21px" width="1%">
                                <asp:Label ID="Label4" runat="server">:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtWebID" runat="server" Width="274px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan ="6">
                                <asp:Panel ID="pnlEntry" runat="server">
                                    <table id="tblEntry" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="auto-style8">Status</td>
                                            <td style="height: 21px" width="1%">
                                                <asp:Label ID="Label3" runat="server">:</asp:Label></td>
                                            <td class="auto-style7">
                                                <asp:DropDownList ID="ddlStatusDetail" runat="server" Width="274px"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlStatusDetail" InitialValue="99" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="auto-style8">Komentar</td>
                                            <td style="height: 21px" width="1%">
                                                <asp:Label ID="Label6" runat="server">:</asp:Label></td>
                                            <td class="auto-style7">
                                                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="3" MaxLength="255" Width="274px"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="Requiredfieldvalidator14" runat="server" ControlToValidate="txtComment" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="auto-style8"></td>
                                            <td style="height: 21px" width="1%"></td>
                                            <td class="auto-style7"></td>
                                        </tr>

                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
				<td colspan="6">
					<asp:Panel ID="pnlInterface" runat="server" style="border: 1px solid gray;padding: 0px 10px 10px 10px; margin-top: 10px">
						<table id="titleInterface" border="0" cellspacing="0" cellpadding="0" width="100%">
							<tr>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td class="titlePanel"><b>ADDITIONAL DATA FOR DMS</b></td>
							</tr>
							<tr>
								<td height="1" background="../images/bg_hor.gif">
									<img border="0" src="../images/bg_hor.gif" height="1"></td>
							</tr>
						</table>
						<table id="TableInterface" cellspacing="1" cellpadding="2" width="100%" border="0">
							<tr>
								<td class="auto-style8">Nama Belakang</td>
								<td style="height: 21px" width="1%">
									<asp:Label ID="Label21" runat="server">:</asp:Label>
								</td>
								<td class="auto-style7">
									<asp:TextBox ID="TxtNamaBelakang" runat="server" Width="274px"></asp:TextBox>                                
								</td>

                                <td class="auto-style8">Status Follow Up</td>
								<td style="height: 21px" width="1%">
									<asp:Label ID="Label31" runat="server">:</asp:Label></td>
								<td class="auto-style7">
									<asp:DropDownList ID="ddlLeadStatus" runat="server" Width="274px"></asp:DropDownList>
								</td>
                                
							</tr>
                            <tr>
								<td class="auto-style8">Nomor Telepon</td>
								<td style="height: 21px" width="1%">
									<asp:Label ID="Label22" runat="server">:</asp:Label>
								</td>
								<td class="auto-style7">
									<asp:TextBox ID="TxtNoTelp" runat="server" Width="274px"></asp:TextBox>                                
								</td>

                                <td class="auto-style8">Status Akhir</td>
								<td style="height: 21px" width="1%">
									<asp:Label ID="Label32" runat="server">:</asp:Label></td>
								<td>
									<asp:DropDownList ID="ddlStateCode" runat="server" Width="274px"></asp:DropDownList>
								</td>
                                
							</tr>
							<tr>
								<td class="auto-style8">Tanggal Lahir</td>
								<td style="height: 21px" width="1%">
									<asp:Label ID="lblBirthDate" runat="server">:</asp:Label>
								</td>
								<td class="auto-style7">
									<cc1:inticalendar id="IntcalBirthDate" runat="server" TextBoxWidth="70"></cc1:inticalendar>                                
								</td>
                                
                                <td class="auto-style8">Alasan Batal</td>
								<td style="height: 21px" width="1%">
									<asp:Label ID="Label35" runat="server">:</asp:Label></td>
								<td>
									<asp:DropDownList ID="ddlStatusCode" runat="server" Width="274px"></asp:DropDownList>									
								</td>
							</tr>
                            <tr>
								<td class="auto-style8">Tipe Kartu Identitas</td>
								<td style="height: 21px" width="1%">
									<asp:Label ID="Label23" runat="server">:</asp:Label>
								</td>
								<td class="auto-style7">
									<asp:DropDownList ID="ddlTipeKartuIdentitas" runat="server" Width="274px" AutoPostBack="true"></asp:DropDownList>                                
								</td>
                                
                                <td class="auto-style8">Dealer Babit & Event</td>
								<td style="height: 21px" width="1%">
									<asp:Label ID="Label27" runat="server">:</asp:Label></td>
								<td valign="middle" class="auto-style7">
                                    <asp:DropDownList ID="ddlBabitEventType" AutoPostBack="true" runat="server" ></asp:DropDownList>
									<asp:TextBox ID="txtCampaignName" runat="server" Width="154px" MaxLength="40" 
									onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');"
									onblur="omitSomeCharacterExcludeSingleQuote('txtCampaignName','<>?*%$;');changesCampaign('txtCampaignName');" 
									onchange="this.value=this.value.toUpperCase();">
								</asp:TextBox>
                                <asp:Label ID="lblPopUpEvent" runat="server" Width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label><br />
								<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2"
										ControlToValidate="txtCampaignName"
										ValidationExpression="^[\s\S]{0,40}$"
										ErrorMessage="Panjang maksimal 100 karakter"></asp:RegularExpressionValidator>
								</td>
							</tr>
                            <tr>
								<td class="auto-style8">Nomor Identitas</td>
								<td style="height: 21px" width="1%">
									<asp:Label ID="Label24" runat="server">:</asp:Label>
								</td>
								<td class="auto-style7">
									<asp:TextBox ID="TxtNomorIdentitas" runat="server" Width="274px"></asp:TextBox>                                
								</td>
                                
                                <td class="auto-style8">Industri</td>
								<td style="height: 21px" width="1%">
									<asp:Label ID="Label28" runat="server">:</asp:Label></td>
								<td>
									<asp:DropDownList ID="ddlBusinessSector" runat="server" Width="274px"></asp:DropDownList>
								</td>
							</tr>
                            <tr>
								<td class="auto-style8">Pekerjaan</td>
								<td style="height: 21px" width="1%">
									<asp:Label ID="Label29" runat="server">:</asp:Label>
								</td>
								<td class="auto-style7">
									<asp:DropDownList ID="ddlPekerjaan" runat="server" Width="274px"></asp:DropDownList>                                
								</td>
                                
                                <td class="auto-style8"><asp:Label ID="Label25" runat="server">Keterangan</asp:Label></td>
								<td style="height: 21px" width="1%">
									<asp:Label ID="lblDesc" runat="server">:</asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Height="46px" Width="274px" MaxLength="60" 
										onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');"
										onblur="omitSomeCharacterExcludeSingleQuote('txtDesc','<>?*%$;');" 
										onkeyup="autofocus(this,'txtDesc');">
									</asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="auto-style8">Rencana Pembelian</td>
								<td style="height: 21px" width="1%">
									<asp:Label ID="lblTglTtp" runat="server">:</asp:Label></td>
								<td class="auto-style7">
									<cc1:inticalendar id="IntcalEstimatedCloseDate" runat="server" TextBoxWidth="70"></cc1:inticalendar>                                
								</td>

                                <td class="auto-style8">Rating</td>
								<td style="height: 21px" width="1%">
									<asp:Label ID="Label33" runat="server">:</asp:Label>
								</td>
								<td class="auto-style7">
									<asp:DropDownList ID="ddlRating" runat="server" Width="274px"></asp:DropDownList>                                
								</td>
							</tr>
                            <tr>
								<td class="auto-style8">Warna</td>
								<td style="height: 21px" width="1%">
									<asp:Label ID="Label26" runat="server">:</asp:Label></td>
								<td class="auto-style7">
									<asp:DropDownList ID="ddlWarna" runat="server" Width="274px" AutoPostBack="true"></asp:DropDownList>
								</td>

                                <td class="auto-style8">Nomor Blanko SPK</td>
								<td style="height: 21px" width="1%">:</td>
								<td>
									<asp:TextBox ID="TxtNoBlankoSPK" runat="server" Width="274px"></asp:TextBox> 							
								</td>
							</tr>
							<tr>
								<td class="auto-style8">Harga PriceList Dealer</td>
								<td style="height: 21px" width="1%">
									<asp:Label ID="Label30" runat="server">:</asp:Label></td>
								<td>
									<asp:DropDownList ID="ddlHargaPriceListDealer" runat="server" Width="274px" AutoPostBack="true"></asp:DropDownList>
								</td>
                                
								<td class="auto-style8">Lampiran Blanko SPK</td>
								<td style="height: 21px" width="1%">:</td>								
                                <td>
									<input onkeypress="return false;" id="DataFile" style="height: 20px" type="file" size="29"
                                    name="File1" runat="server"><br />
                                     <asp:LinkButton ID="LnkDownloadTemplate" runat="server" ToolTip="Download Blanko SPK">Download Blanko SPK</asp:LinkButton>                             					
								</td>
							</tr>
							<tr>
								<td class="auto-style8">Harga Permintaan Konsumen</td>
								<td style="height: 21px" width="1%">:</td>
								<td>
									<asp:TextBox ID="TxtHargaPermintaanKonsumen" onblur="reCallDisc(this)" 
                                        onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" runat="server" Width="274px"></asp:TextBox>
								</td>
								
                                <td class="auto-style8"></td>
								<td style="height: 21px" width="1%"></td>
                                <td>								
                                    &nbsp;&nbsp;Max 2 MB (*.pdf / *.doc / *.docx / *.jpg / *.png)                                    					
								</td>
							</tr>
                            <tr>
                                <td class="auto-style8">Jumlah Diskon</td>
								<td style="height: 21px" width="1%">:</td>
								<td>
									<asp:TextBox ID="TxtJumlahDiskon" runat="server" Width="274px" ReadOnly="true"></asp:TextBox> 							
								</td>
								
                                <td class="auto-style8">Kendaraan Pembanding</td>
								<td style="height: 21px" width="1%">:</td>
								<td>
									<asp:TextBox ID="TxtKendaraanPembanding" runat="server" Width="274px"></asp:TextBox> 							
								</td>
                            </tr>
                            
                            <tr>
								<td class="auto-style8">Booking Fee (IDR)</td>
								<td style="height: 21px" width="1%">:</td>
								<td>
									<asp:TextBox ID="TxtBookingFee" runat="server" Width="274px"></asp:TextBox> 							
								</td>

                                <td class="auto-style8"></td>
								<td style="height: 21px" width="1%"></td>
								<td>
									<asp:TextBox ID="TxtBlanko" runat="server" Width="274px" Visible="true" ></asp:TextBox> 							
								</td>
                            </tr>
                            <tr>								
								<td class="auto-style8">Type BBN</td>
								<td style="height: 21px" width="1%">
									<asp:Label ID="Label34" runat="server">:</asp:Label></td>
								<td>
									<asp:DropDownList ID="ddlTypeBBN" runat="server" Width="274px" AutoPostBack="true"></asp:DropDownList>
								</td>
                                
							</tr>
						</table>
					</asp:Panel>
				</td>
			</tr>
                        <tr>
                            <td class="auto-style8"></td>
                            <td style="height: 21px" width="1%">
                            <td class="auto-style7">
                                <asp:Button ID="btnSave" Text="Simpan" runat="server" />
                                <asp:Button ID="btnBack" Text="Kembali" runat="server" CausesValidation="false" />
                            </td>
                            <td class="auto-style8"></td>
                            <td style="height: 21px" width="1%">
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="div1" style="overflow: auto; height: 150px">
            <asp:DataGrid ID="dgCase" runat="server" Width="100%" DataKeyField="ID" BorderStyle="None"
                AllowPaging="True" PageSize="25" AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px"
                BackColor="White" CellPadding="3" GridLines="Horizontal" ForeColor="Gray" CellSpacing="1">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="#F1F6FB"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" VerticalAlign="Top" BackColor="White"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="titleTableSales" BackColor="#000084"></HeaderStyle>
                <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Status">
                        <HeaderStyle Width="15%" CssClass="titleTableMrk"></HeaderStyle>
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Note">
                        <HeaderStyle Width="60%" CssClass="titleTableMrk"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="CreatedTime" SortExpression="CreatedTime" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Tanggal Update">
                        <HeaderStyle Width="10%" CssClass="titleTableMrk"></HeaderStyle>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn Visible="false">
                        <HeaderStyle Width="1%" CssClass="titleTableMrk"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDetail" runat="server" Text="Detail">
									<img alt="Data Detail" src="../images/popup.gif" style="cursor:hand"
										border="0"></asp:Label>
                            <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Ubah" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Edit" CausesValidation="False">
									<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
    </form>
</body>
</html>
