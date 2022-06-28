<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmEntryInvoiceRevision.aspx.vb" Inherits="FrmEntryInvoiceRevision" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmEntryInvoiceRevision</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js" type="text/javascript"></script>
    <script language="javascript" src="../WebResources/InputValidation.js?id=0" type="text/javascript"></script>
    <script language="javascript" src="../WebResources/jquery-1.10.2.js" type="text/javascript"></script>
    <style type="text/css">
        .dragable {
            POSITION: relative;
        }

        .dragbar {
            CURSOR: default;
        }
    </style>

    <script language="javascript" type="text/javascript">
        $(function () {
            $("#form1").submit(function (e) {
                if (!DisclaimerAgreed) {
                    e.preventDefault ? e.preventDefault() : e.returnValue = false;    //stop form from submitting
                }
            });
        });

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

        var DisclaimerAgreed = false;

        function ToggleAdditionalInformation(sGroup) {
            //alert(sGroup);
            if (isValidated == false) {
                if (nn6) {
                    if (sGroup == "chkPelanggaranWilayah") {
                        var obj = document.getElementById(sGroup);
                        var checked = obj.getAttribute("checked");
                        var disabled = obj.getAttribute("disabled");
                        var ddl = document.getElementById("ddlPWPaymentMethod");
                        if ((obj.checked && isValidated == false)) {
                            ddl.disabled = false;
                            document.getElementById("txtPWAmount").disabled = false;
                            document.getElementById("txtPWBank").disabled = false;
                            document.getElementById("txtPWNomorGiro").disabled = false;
                            var val0 = document.getElementById("PWVal0");
                            var val1 = document.getElementById("PWVal1");
                            if (val0 != null)

                                val0.enabled = true;
                            if (val1 != null)
                                val1.enabled = true;
                        }
                        else //if ((checked == null && disabled == null) || (disabled == "disabled") || isValidated == true)
                        {
                            ddl.disabled = true;
                            document.getElementById("txtPWAmount").disabled = true;
                            document.getElementById("txtPWBank").disabled = true;
                            document.getElementById("txtPWNomorGiro").disabled = true;
                            var val0 = document.getElementById("PWVal0");
                            var val1 = document.getElementById("PWVal1");
                            if (val0 != null)

                                val0.enabled = false;
                            if (val1 != null)
                                val1.enabled = false;
                        }
                    }
                    else if (sGroup == "chkPembayaranPenalti") {
                        var obj = document.getElementById(sGroup);
                        var checked = obj.getAttribute("checked");
                        var disabled = obj.getAttribute("disabled");
                        var ddl = document.getElementById("ddlPPPaymentMethod");
                        if ((obj.checked && isValidated == false)) {
                            ddl.disabled = false;
                            document.getElementById("txtPPAmount").disabled = false;
                            document.getElementById("txtPPBank").disabled = false;
                            document.getElementById("txtPPNomorGiro").disabled = false;
                            var val0 = document.getElementById("PPVal0");
                            var val1 = document.getElementById("PPVal1");
                            if (val0 != null)

                                val0.enabled = true;
                            if (val1 != null)
                                val1.enabled = true;
                        }
                        else {
                            ddl.disabled = true;
                            document.getElementById("txtPPAmount").disabled = true;
                            document.getElementById("txtPPBank").disabled = true;
                            document.getElementById("txtPPNomorGiro").disabled = true;
                            var val0 = document.getElementById("PPVal0");
                            var val1 = document.getElementById("PPVal1");
                            if (val0 != null)

                                val0.enabled = false;
                            if (val1 != null)
                                val1.enabled = false;
                        }
                    }
                    else if (sGroup == "chkMCP") {
                        var obj = document.getElementById(sGroup);
                        var checked = obj.getAttribute("checked");
                        var disabled = obj.getAttribute("disabled");
                        //  document.getElementById("txtNoLKPP").value = "";
                        if ((obj.checked && isValidated == false)) {
                            document.getElementById("txtNoMCP").disabled = false;
                            document.getElementById("lblMCPNumber").style.visibility = "visible";
                        }
                        else {
                            document.getElementById("txtNoMCP").disabled = true;
                            document.getElementById("lblMCPNumber").style.visibility = "hidden";
                        }
                    }
                    else if (sGroup == "chkLKPP") {
                        var obj = document.getElementById(sGroup);
                        var checked = obj.getAttribute("checked");
                        // document.getElementById("txtNoMCP").value = "";

                        var disabled = obj.getAttribute("disabled");
                        if ((obj.checked && isValidated == false)) {
                            document.getElementById("txtNoLKPP").disabled = false;
                            document.getElementById("lblLKPPNumber").style.visibility = "visible";
                        }
                        else {
                            document.getElementById("txtNoLKPP").disabled = true;
                            document.getElementById("lblLKPPNumber").style.visibility = "hidden";
                        }
                    }
                    else {
                        var obj = document.getElementById(sGroup);
                        var checked = obj.getAttribute("checked");
                        var disabled = obj.getAttribute("disabled");
                        if ((obj.checked && isValidated == false)) {
                            document.getElementById("txtSRNomorSurat").disabled = false;
                            var val0 = document.getElementById("SRVal1");
                            if (val0 != null)

                                val0.enabled = true;
                        }
                        else {
                            document.getElementById("txtSRNomorSurat").disabled = true;
                            var val0 = document.getElementById("SRVal1");
                            if (val0 != null)

                                val0.enabled = false;
                        }
                    }
                }
                else {
                    if (sGroup == "chkPelanggaranWilayah") {
                        var ddl = document.getElementById("ddlPWPaymentMethod");
                        if (document.getElementById(sGroup).checked == true && isValidated == false) {
                            ddl.disabled = false;
                            document.getElementById("txtPWAmount").disabled = false;
                            document.getElementById("txtPWBank").disabled = false;
                            document.getElementById("txtPWNomorGiro").disabled = false;
                            document.getElementById("PWVal0").enabled = true;
                            document.getElementById("PWVal1").enabled = true;
                        }
                        else {
                            ddl.disabled = true;
                            document.getElementById("txtPWAmount").disabled = true;
                            document.getElementById("txtPWBank").disabled = true;
                            document.getElementById("txtPWNomorGiro").disabled = true;
                            document.getElementById("PWVal0").enabled = false;
                            document.getElementById("PWVal1").enabled = false;
                        }
                    }
                    else if (sGroup == "chkPembayaranPenalti") {
                        var ddl = document.getElementById("ddlPPPaymentMethod");
                        if (document.getElementById(sGroup).checked == true && isValidated == false) {
                            ddl.disabled = false;
                            document.getElementById("txtPPAmount").disabled = false;
                            document.getElementById("txtPPBank").disabled = false;
                            document.getElementById("txtPPNomorGiro").disabled = false;
                            document.getElementById("PPVal0").enabled = true;
                            document.getElementById("PPVal1").enabled = true;
                        }
                        else {
                            ddl.disabled = true;
                            document.getElementById("txtPPAmount").disabled = true;
                            document.getElementById("txtPPBank").disabled = true;
                            document.getElementById("txtPPNomorGiro").disabled = true;
                            document.getElementById("PPVal0").enabled = false;
                            document.getElementById("PPVal1").enabled = false;
                        }
                    }
                    else if (sGroup == "chkMCP") {
                        if (document.getElementById(sGroup).checked == true && isValidated == false) {
                            document.getElementById("txtNoMCP").disabled = false;
                            document.getElementById("lblMCPNumber").style.visibility = "visible";
                        }
                        else {
                            document.getElementById("txtNoMCP").disabled = true;
                            document.getElementById("lblMCPNumber").style.visibility = "hidden";
                        }
                    }
                    else if (sGroup == "chkLKPP") {
                        if (document.getElementById(sGroup).checked == true && isValidated == false) {
                            document.getElementById("txtNoLKPP").disabled = false;
                            document.getElementById("lblLKPPNumber").style.visibility = "visible";
                        }
                        else {
                            document.getElementById("txtNoLKPP").disabled = true;
                            document.getElementById("lblLKPPNumber").style.visibility = "hidden";
                        }
                    }

                    else {
                        if (document.getElementById(sGroup).checked == true && isValidated == false) {
                            document.getElementById("txtSRNomorSurat").disabled = false;
                            document.getElementById("SRVal1").enabled = true;
                        }
                        else {
                            document.getElementById("txtSRNomorSurat").disabled = true;
                            document.getElementById("SRVal1").enabled = false;
                        }
                    }
                }
            }
        }

        function MinimizeAddtionalInformation() {
            var PanelAdditionalInformation = document.getElementById("divAdditionalInformation")
            if (PanelAdditionalInformation.style.height == "22px") {
                PanelAdditionalInformation.style.width = "310px"
                PanelAdditionalInformation.style.height = "465px"
                document.getElementById("imgAddInfo").src = "../images/minus.gif"
                //document.getElementById("tabelAddInfo").style.visibility="visible"
            }
            else {
                PanelAdditionalInformation.style.height = "22px"
                PanelAdditionalInformation.style.width = "170px"
                document.getElementById("imgAddInfo").src = "../images/plus.gif"
                //document.getElementById("tabelAddInfo").style.visibility="hidden"
            }
        }

        function ToggleDisclaimerAgreement() {
            if (document.getElementById("chkDisclaimerAgreement").checked == true) {
                DisclaimerAgreed = true;
            }
            else {
                DisclaimerAgreed = false;
                alert("Anda belum menyetujui isi disclaimer");
            }
        }

        function alphaNumericWith(event, addKey) {
            var pressedKey

            if (navigator.appName == "Microsoft Internet Explorer")
                pressedKey = event.keyCode;
            else {
                pressedKey = event.charCode;
            }
            if ((isAccepted(pressedKey, addKey)) || (pressedKey >= 48 && pressedKey <= 57) || (pressedKey >= 97 && pressedKey <= 122) || (pressedKey >= 65 && pressedKey <= 90) || (pressedKey == 0)) {
                return true;
            }
            else
                return false;
        }

        function RebindModel() {
            var btnBindModel = document.getElementById("btnBindModel");
            var ddlJenis = document.getElementById("DDLIST86_7");
            var txtNewKindID = document.getElementById("txtNewKindID");

            txtNewKindID.value = ddlJenis.options[ddlJenis.selectedIndex].value;
            btnBindModel.click();
        }

        function ChoosenModel() {
            var ddlModel = document.getElementById("DDLIST87_7");
            var txtNewModelID = document.getElementById("txtNewModelID");
            //alert(ddlModel.options[ddlModel.selectedIndex].value);
            txtNewModelID.value = ddlModel.options[ddlModel.selectedIndex].value;

        }

        function ShowLKPPSelection() {
            var lblChassisNumber = document.getElementById('lblChassisNumber');
            showPopUp('../PopUp/PopUpLKPPTersedia.aspx?IsGroupDealer=1&SN=' + lblChassisNumber.innerHTML, '', 500, 800, LKPPSelection);
        }

        function LKPPSelection(selectedLKPP) {
            var temp = selectedLKPP.split(";")
            var txtNoLKPP = document.getElementById('txtNoLKPP');
            var hdnNoLKPP = document.getElementById('hdnNoLKPP');
            txtNoLKPP.value = temp[0];
            hdnNoLKPP.value = temp[0];

            if (navigator.appName == "Microsoft Internet Explorer") {
                hdnNoLKPP.blur();
            }
            else {
                hdnNoLKPP.onchange();
            }
        }
        
        function ShowFleetSelection() {
            showPopUp('../PopUp/PopUpFleetReqTersedia.aspx?IsGroupDealer=1', '', 500, 800, FleetReqSelection);
        }

        function FleetReqSelection(selectedFleetReq) {
            var temp = selectedFleetReq.split(";")
            var txtNoFleetReq = document.getElementById('txtNoFleetReq');
            txtNoFleetReq.value = temp[0];
        }

        function DisableLeasing() {
            __doPostBack('btnDisableLeasing', '')

            var btnDisableLeasing = document.getElementById("btnDisableLeasing");
            btnDisableLeasing.click();
        }

        function CustomerSelection(selectedCustomer) {
            var txtCustomerID = document.getElementById("txtCustomerID");
            txtCustomerID.value = selectedCustomer;
            __doPostBack("txtCustomerID", "");
        }

        function ShowCustomerList() {
            showPopUp('../PopUp/PopUpCustomerList.aspx?Tyjiuy678=code', '', 500, 760, CustomerSelection);
        }
                
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server" method="post" >
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">Revision FAKTUR&nbsp;- Permohonan Revisi	Faktur Kendaraan</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img alt="" height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img alt="" height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <asp:Panel ID="pnlRevType" Visible="False" runat="server">
                            <tr>
                                <td class="titleField" width="80" height="24">Tipe Revisi</td>
                                <td width="5">:</td>
                                <td width="230">
                                    <asp:HiddenField ID="hidRevisionType" runat="server" />
                                    <asp:Label ID="lblRevisionType" runat="server"> </asp:Label>
                                </td>
                                <td class="titleField" width="80" height="24"></td>
                                <td width="5"></td>
                                <td width="230"></td>
                            </tr>
                        </asp:Panel>
                        

                        <tr>
                            <td class="titleField" width="80" height="24">Nomor Rangka</td>
                            <td width="5">:</td>
                            <td width="230">
                                <asp:Label ID="lblChassisNumber" runat="server"></asp:Label></td>
                            <td class="titleField" width="80" height="24">Kode Dealer</td>
                            <td width="5">:</td>
                            <td width="230">
                                <asp:Label ID="lblDealerCode" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" height="24">Nomor Mesin</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblEngineNumber" runat="server"></asp:Label></td>
                            <td class="titleField" height="24">Nama Dealer</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblDealerName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" height="24">Model / Tipe / Warna</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblModelTypeColor" runat="server"></asp:Label></td>
                            <td class="titleField" height="24">Cabang Dealer</td>
                            <td>:</td>
                            <td>
                                &nbsp; <asp:Label runat="server" ID="lblDealerBranch"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" height="24">Status</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblStatus" runat="server"></asp:Label></td>
                            <td class="titleField" height="24">Nama Pesanan Khusus</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblNamaPesananKhusus" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" height="24">Nomor Faktur</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblNomorFaktur" runat="server"></asp:Label></td>
                            <td class="titleField" height="24">Cetak DO</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblDODate" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" height="24">Tgl Faktur</td>
                            <td>:</td>
                            <td>
                                <cc1:inticalendar id="icInvoiceDate" runat="server"></cc1:inticalendar>
                                <asp:Label ID="lblInvoiceDate" runat="server"></asp:Label></td>
                            <td class="titleField" height="24">Diskon</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblDiscountAmount" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" height="24">No&nbsp;Rangka Pengganti</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericWith(event,'');" ID="txtRefChassisNumber" onblur="document.getElementById('txtRefChassisNumber').value = document.getElementById('txtRefChassisNumber').value.toUpperCase();omitSomeCharacter('txtRefChassisNumber',';');"
                                    runat="server" MaxLength="20"></asp:TextBox>
                                <asp:Label ID="lblRefChassisNo" runat="server"></asp:Label></td>
                            <td class="titleField" height="24">Cara Pembayaran</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTOPayment" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="Table4" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td valign="top" align="left">
                                <asp:Panel ID="pnlEndCustomer" runat="server" Width="100%">
                                    <table id="Table3" cellspacing="1" cellpadding="1" width="100%" border="0">
                                        <tr>
                                            <td class="titleTableSales" colspan="3"><strong>Data&nbsp;Konsumen</strong></td>
                                        </tr>
                                        <asp:Panel ID="pnlCustomerCode" Visible="False" runat="server">
                                            <tr>
                                                <td class="titleField" width="100" height="24">Customer Code</td>
                                                <td width="5" height="24">:</td>
                                                <td height="24">
                                                    <asp:Label ID="lblCustCode" runat="server" Visible="False"></asp:Label>
										            <asp:TextBox onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');" ID="txtCustomerID"
											            onblur="omitSomeCharacter('txtCustomerID','<>?*%$;');" runat="server" Width="88px" Visible="false"
											            MaxLength="10"></asp:TextBox>
										            <asp:Label id="lbCustomerID" onclick="ShowCustomerList();" Runat="server" Visible="false">
											            <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
										            </asp:Label>
										            <asp:LinkButton id="lnkReloadCust" runat="server" CausesValidation="False" Visible="false" >
											            <img src="../images/reload.gif" style="cursor:hand" border="0" alt="Reload">
										            </asp:LinkButton>
                                            </tr>
                                        </asp:Panel>
                                        <tr>
                                            <td class="titleField" width="100" height="24">Nama 1</td>
                                            <td width="5" height="24">:</td>
                                            <td height="24">
                                                <asp:Label ID="lblName1" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" width="100" height="24">Nama 2</td>
                                            <td width="5" height="24">:</td>
                                            <td height="24">
                                                <asp:Label ID="lblName2" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" width="100" height="24">Gedung</td>
                                            <td width="5" height="24">:</td>
                                            <td height="24">
                                                <asp:Label ID="lblName3" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" width="100" height="24">Alamat</td>
                                            <td width="5" height="24">:</td>
                                            <td height="24">
                                                <asp:Label ID="lblAddress" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" width="100" height="24">Kelurahan</td>
                                            <td width="5" height="24">:</td>
                                            <td height="24">
                                                <asp:Label ID="lblKel" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" width="100" height="24">Kecamatan</td>
                                            <td width="5" height="24">:</td>
                                            <td height="24">
                                                <asp:Label ID="lblKec" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" width="100" height="24">Kode POS</td>
                                            <td width="5" height="24">:</td>
                                            <td height="24">
                                                <asp:Label ID="lblPOSCode" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" width="100" height="24">Kodya/Kabupaten</td>
                                            <td width="5" height="24">:</td>
                                            <td height="24">
                                                <asp:Label ID="lblCity" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" width="100" height="17">Propinsi</td>
                                            <td width="5" height="17">:</td>
                                            <td height="17">
                                                <asp:Label ID="lblProvince" runat="server"></asp:Label>
                                                <strong><asp:Label ID="lblCapCetak" runat="server">Cetak</asp:Label></strong>&nbsp;
													<asp:Label ID="lblCetak" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" width="100" height="24">No. KTP/TDP</td>
                                            <td width="5" height="24">:</td>
                                            <td height="24">
                                                <asp:Label ID="lblKTP" runat="server"></asp:Label></td>
                                        </tr>
                                        <asp:Panel ID="Phone" Visible="False" runat="server">
                                            <tr>
                                                <td class="titleField" width="100" height="16">Email</td>
                                                <td width="5" height="16">:</td>
                                                <td height="16">
                                                    <asp:Label ID="lblEmail" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="titleField" width="100" height="24">Telepon/Fax</td>
                                                <td width="5" height="24">:</td>
                                                <td height="24">
                                                    <asp:Label ID="lblPhone" runat="server"></asp:Label></td>
                                            </tr>
                                        </asp:Panel>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="Table6" style="height: 34px" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td class="titleTableSales" colspan="4"><strong>Profil&nbsp;Konsumen</strong></td>
                        </tr>
                        <tr></tr>
                    </table>
                    <table id="Table7" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td>
                                <asp:Panel ID="pnlInformasion" runat="server" BackColor="#ffff66"></asp:Panel>
                            </td>
                        </tr>
                    </table>
               </td>
            </tr>
            <tr>
                <td>
                    <table id="Table10" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td style="width: 75px">Dibuat oleh</td>
                            <td style="width: 6px">:</td>
                            <td>
                                <asp:Label ID="lblCreatedBy" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 75px">
                                <asp:Label ID="lblTitleValidateBy" runat="server">Divalidasi oleh</asp:Label></td>
                            <td style="width: 6px">
                                <asp:Label ID="lblDelimeterValidateBy" runat="server">:</asp:Label></td>
                            <td>
                                <asp:Label ID="lblValidatedBy" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <p>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server"></asp:ValidationSummary>
                        <asp:TextBox ID="txtDisclaimer" runat="server" Width="672px" Height="50px" ReadOnly="True" TextMode="MultiLine">Kami menjamin isi permohonan ini adalah benar dan tepat serta digunakan sesuai dengan kewenangan yang diberikan oleh konsumen tersebut di atas. Kami bertanggung jawab dan karena itu membebaskan serta melepaskan MMKSI dari setiap tuntutan, perselisihan yang mungkin timbul sehubungan dengan penyalahgunaan, ketidaktepatan, dan ketidakbenaran data konsumen yang tercantum di atas.</asp:TextBox><br>
                        <asp:CheckBox ID="chkDisclaimerAgreement" runat="server" Text="Saya telah membaca dan menyetujui pernyataan di atas"></asp:CheckBox>
                    </p>
                    <p>
                        <asp:CheckBox ID="chkPrintProvinceOnInvoice" runat="server" Text="Cetak Propinsi di Faktur" Visible="False"></asp:CheckBox></p>
                    <p>
                        <asp:Button ID="btnSave" runat="server" Text="Simpan" ></asp:Button>
                        <asp:Button ID="btnCancel" runat="server" Text="Batal" CausesValidation="False" Visible="False"></asp:Button>
                        <asp:Button ID="btnValidate" runat="server" Text="Validasi"></asp:Button>
                        <asp:Button ID="btnCancelValidate" runat="server" Text="Batal Validasi" CausesValidation="False"></asp:Button>
                        <asp:Button ID="btnKembali" runat="server" Text="Kembali" CausesValidation="False"></asp:Button>
                        <asp:Button ID="btnBindModel" runat="server" Text="Bind Model" CausesValidation="False" Style="display: none"></asp:Button>
                        <asp:TextBox ID="txtNewKindID" runat="server" Style="display: none">0</asp:TextBox>
                        <asp:TextBox ID="txtNewModelID" runat="server" Style="display: none">0</asp:TextBox>
                    </p>
                </td>
            </tr>
        </table>
        <div class="dragable" id="divAdditionalInformation"
            style="border-right: black 1px solid; border-top: black 1px solid; z-index: 101; left: 460px; overflow: hidden; border-left: black 1px solid; width: 310px; border-bottom: black 1px solid; position: absolute; top: 250px; height: 465px; background-color: #f5f1ee">
            <table id="tabelAddInfoHeader" cellspacing="1" cellpadding="1" width="100%" border="0">
                <tr>
                    <td class="dragbar" bgcolor="#f28625" colspan="3" height="24px">
                        <strong><asp:Image ID="imgAddInfo" runat="server" Width="22px" Height="12px" ImageUrl="../images/minus.gif"></asp:Image>&nbsp;
								<asp:Label ID="lblAddInfo" runat="server" BackColor="#F28625" ForeColor="White" CssClass="dragbar">Informasi Tambahan</asp:Label>
                        </strong></td>
                </tr>
            </table>
            <table id="tabelAddInfo" cellspacing="1" cellpadding="1" width="100%" border="0">
                <tr>
                    <td style="height: 23px" bgcolor="#ffcc77" colspan="3">
                        <asp:CheckBox ID="chkPelanggaranWilayah" runat="server" BackColor="#FFCC77" Text="Pelanggaran Wilayah"></asp:CheckBox></td>
                </tr>
                <tr>
                    <td>Tipe&nbsp;Pembayaran</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="lblPWPaymentMethod" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlPWPaymentMethod" runat="server"></asp:DropDownList>
                        <asp:CompareValidator ID="PWval0" runat="server" ErrorMessage="*" ControlToValidate="ddlPWPaymentMethod"
                            ValueToCompare="Silahkan Pilih" Operator="NotEqual" Enabled="False">*</asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>Jumlah (Rp)</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="lblPWAmount" runat="server"></asp:Label>
                        <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" ID="txtPWAmount" onkeyup="pic(this,this.value,'9999999999','N')" runat="server" MaxLength="12" CssClass="textRight"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PWval1" runat="server" ErrorMessage="*" ControlToValidate="txtPWAmount" Enabled="False"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Nama Bank</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;','');" ID="txtPWBank" onblur="omitSomeCharacter('txtPWBank','<>?*%$;');"
                            runat="server" MaxLength="30"></asp:TextBox><asp:Label ID="lblPWBank" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Nomor Giro / Transfer</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;','');" ID="txtPWNomorGiro" onblur="omitSomeCharacter('txtPWNomorGiro','<>?*%$;');" runat="server" MaxLength="30"></asp:TextBox>
                        <asp:Label ID="lblPWNomorGiro" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#ffcc77" colspan="3">
                        <asp:CheckBox ID="chkPembayaranPenalti" runat="server" BackColor="#FFCC77" Text="Pembayaran Penalti"></asp:CheckBox></td>
                </tr>
                <tr>
                    <td>Tipe&nbsp;Pembayaran</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="lblPPPaymentMethod" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlPPPaymentMethod" runat="server"></asp:DropDownList>
                        <asp:CompareValidator ID="PPval0" runat="server" ErrorMessage="*" ControlToValidate="ddlPPPaymentMethod" ValueToCompare="Silahkan Pilih" Operator="NotEqual" Enabled="False">*</asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>Jumlah (Rp)</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="lblPPAmount" runat="server"></asp:Label><asp:TextBox onkeypress="return NumericOnlyWith(event,'');" ID="txtPPAmount" onkeyup="pic(this,this.value,'9999999999','N')"
                            runat="server" MaxLength="12" CssClass="textRight"></asp:TextBox><asp:RequiredFieldValidator ID="PPval1" runat="server" ErrorMessage="*" ControlToValidate="txtPPAmount" Enabled="False"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td>Nama Bank</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;','');" ID="txtPPBank" onblur="omitSomeCharacter('txtPPBank','<>?*%$;');" runat="server" MaxLength="30"></asp:TextBox>
                        <asp:Label ID="lblPPBank" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Nomor Giro / Transfer</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;','');" ID="txtPPNomorGiro" onblur="omitSomeCharacter('txtPPNomorGiro','<>?*%$;');" runat="server" MaxLength="30"></asp:TextBox>
                        <asp:Label ID="lblPPNomorGiro" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#ffcc77" colspan="3">
                        <asp:CheckBox ID="chkSuratReferensi" runat="server" BackColor="#FFCC77" Text="Surat Referensi"></asp:CheckBox></td>
                </tr>
                <tr>
                    <td>Nomor Surat</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="lblSRNomorSurat" runat="server"></asp:Label>
                        <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;','');" ID="txtSRNomorSurat" onblur="omitSomeCharacter('txtSRNomorSurat','<>?*%$;');" runat="server" MaxLength="40"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="SRval1" runat="server" ErrorMessage="*" ControlToValidate="txtSRNomorSurat" Enabled="False"></asp:RequiredFieldValidator>
                    </td>
                </tr>                
                <tr>
                    <td bgcolor="#ffcc77" colspan="3">
                        <asp:CheckBox ID="chkLKPP" runat="server" BackColor="#FFCC77" Text="LKPP"></asp:CheckBox>
                    </td>
                </tr>
                <tr valign="top">
                    <td>Nomor Pengadaan</td>
                    <td>:</td>
                    <td>
                        <asp:HiddenField ID="hdnNoLKPP" runat="server"/>
                        <asp:Label ID="lblNoLKPP" runat="server"></asp:Label>
                        <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;','');" ID="txtNoLKPP"
                            onblur="omitSomeCharacter('txtNoLKPP','<>?*%$;');" runat="server" MaxLength="60"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="SRValLKPP" runat="server" ErrorMessage="*" ControlToValidate="txtNoLKPP"
                            Enabled="False"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblLKPPNumber" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                        </asp:Label>
                    </td>
                </tr>
                <tr style="display:none;">
                    <td bgcolor="#ffcc77" colspan="3">
                        <asp:CheckBox ID="chkFleet" runat="server" BackColor="#FFCC77" Text="Program Extended Free Service"></asp:CheckBox>
                    </td>
                </tr>
                <tr style="display:none;">
                    <td>No Extended Free Service</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="lblNoFleet" runat="server"></asp:Label>
                        <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;','');" ID="txtNoFleetReq"
                            onblur="omitSomeCharacter('txtNoFleetReq','<>?*%$;');" runat="server" MaxLength="60"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtNoFleetReq"
                            Enabled="False"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblNoRegRequest" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                        </asp:Label>
                    </td>
                </tr>
            </table>
        </div>

        <asp:TextBox runat="server" ID="txtLKPPConfirmation" name="txtLKPPConfirmation" CausesValidation="False" Text="-1" Style="visibility: hidden"></asp:TextBox>
        <asp:Button id="btnDisableLeasing" runat="server" Text="DisableLeasing" CausesValidation="False" style="display:none;"></asp:Button>
    </form>
     <script language="javascript" type="text/javascript">
         if (document.getElementById("btnValidate") != null) {
             isValidated = (document.getElementById("btnValidate").disabled == true);
         }
         else {
             isValidate = false;
         }
         isValidate = true;
         ToggleAdditionalInformation("chkPelanggaranWilayah");
         ToggleAdditionalInformation("chkPembayaranPenalti");
         ToggleAdditionalInformation("chkSuratReferensi");
         ToggleAdditionalInformation("chkMCP");
         ToggleAdditionalInformation("chkLKPP");

    </script>
</body>
</html>
