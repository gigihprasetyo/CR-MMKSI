<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMSPExtendedRegistration.aspx.vb" Inherits=".FrmMSPExtendedRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Registrasi MSP</title>
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript">
        function ShowChassisSelection() {
            showPopUp('../PopUp/PopUpChassisMasterSelection.aspx?indent=2', '', 500, 760, ChassisSelection);
        }
        function ChassisSelection(selectedChassis) {
            //alert(selectedChassis);
            var tempParam = selectedChassis.split(';');
            //alert(tempParam);
            var hdnChassisNo = document.getElementById("hdnChassisNo");
            var txtChassisNo = document.getElementById("txtChassisNo");
            var lnkReloadChassis = document.getElementById("lnkReloadChassis");
            hdnChassisNo.value = tempParam[0];
            txtChassisNo.value = tempParam[0];
            lnkReloadChassis.click();
        }
        function ShowMSPLama() {
            showPopUp('../PopUp/PopUpInputMSPExtendedSelection.aspx', '', 500, 760, MSPSelection);
        }
        function MSPSelection(selectedMSP) {
            //alert(selectedChassis);
            var tempParam = selectedMSP.split(';');
            //alert(tempParam);
            var hdnMSPLama = document.getElementById("hdnMSPLama");
            var txtMSPLama = document.getElementById("txtMSPLama");
            var lnkReloadMSPLama = document.getElementById("lnkReloadMSPLama");
            hdnMSPLama.value = tempParam[0];
            txtMSPLama.value = tempParam[0];
            lnkReloadMSPLama.click();
        }
        function LoadDataOnBlurChassis() {
            var hdnChassisNo = document.getElementById("hdnChassisNo");
            var txtChassisNo = document.getElementById("txtChassisNo");
            var lnkReloadChassis = document.getElementById("lnkReloadChassis");
            hdnChassisNo.value = txtChassisNo.value;
            if (hdnChassisNo.value != "") {
                lnkReloadChassis.click();
            }
        }
        function LoadDataOnBlurMSP() {
            var hdnMSPLama = document.getElementById("hdnMSPLama");
            var txtMSPLama = document.getElementById("txtMSPLama");
            var lnkReloadMSPLama = document.getElementById("lnkReloadMSPLama");
            hdnMSPLama.value = txtMSPLama.value;
            if (hdnMSPLama.value != "") {
                lnkReloadMSPLama.click();
            }
        }

        function ShowConfirm(msg, id) {
            var btn = document.getElementById(id);
            var hdConfirm = document.getElementById("hdConfirm");
            if (confirm(msg)) {
                hdConfirm.value = "0";
                btn.click();
            }
        }
        function LoadDataOnBlurODO() {
            var LinkButton1 = document.getElementById("LinkButton1");
            LinkButton1.click();
        }
        function btnSaveOnClick() {
            var txtChassisNo = document.getElementById("txtChassisNo");
            var ddlTipeMSP = document.getElementById("ddlTipeMSP");
            var ddlTipeMSPText = ddlTipeMSP.options[ddlTipeMSP.selectedIndex].text;
            return confirm("Apakah anda yakin akan menyimpan data Chassis " + txtChassisNo.value + " dengan tipe " + ddlTipeMSPText + "?");
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
            height: 25px;
        }

        .auto-style2 {
            height: 25px;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="FrmMSPRegistration" method="post" runat="server">
        <table id="tblMSPRegistration" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">MSP Extended - Registrasi Konsumen</td>
            </tr>
            <tr>
                <td height="1" background="../images/bg_hor.gif">
                    <img border="0" src="../images/bg_hor.gif" height="1"></td>
            </tr>
            <tr>
                <td height="10">
                    <img border="0" src="../images/dot.gif" height="1"></td>
            </tr>
            <tr>
                <td align="left">
                    <table id="Table2" border="0" cellspacing="1" cellpadding="2" width="100%">
                        <tr>
                            <td align="left">
                                <table id="Table2" border="0" cellspacing="1" cellpadding="2" width="100%">
                                    <tr>
                                        <td class="titlePanel" colspan="7">DATA PEMBUAT PELANGGAN</td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%">Kode Dealer</td>
                                        <td width="1%">:</td>
                                        <td width="26%">
                                            <asp:Label ID="lblDealer" runat="server"></asp:Label>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%"></td>
                                        <td width="1%"></td>
                                        <td width="25%"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%">Nomor MSP</td>
                                        <td width="1%">:</td>
                                        <td width="26%">
                                            <asp:Label ID="lblMSPNo" runat="server"></asp:Label>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%"></td>
                                        <td width="1%"></td>
                                        <td width="25%"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%">Diajukan Oleh</td>
                                        <td width="1%">:</td>
                                        <td width="26%">
                                            <asp:Label ID="lblSubmitBy" runat="server"></asp:Label>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%"></td>
                                        <td width="1%"></td>
                                        <td width="25%"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%">Tanggal Pengajuan</td>
                                        <td width="1%">:</td>
                                        <td width="26%">
                                            <asp:Label ID="lblCurrentDate" runat="server"></asp:Label>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%"></td>
                                        <td width="1%"></td>
                                        <td width="25%"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%">Status Pengajuan</td>
                                        <td width="1%">:</td>
                                        <td width="26%">
                                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%"></td>
                                        <td width="1%"></td>
                                        <td width="25%"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table id="titleUmum" border="0" cellspacing="1" cellpadding="2" width="100%">
                                    <tr>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="titlePanel"><b>INFORMASI PELANGGAN : UMUM</b></td>
                                    </tr>
                                </table>
                                <table border="0" cellspacing="1" cellpadding="2" width="100%">
                                    <tr>
                                        <td class="titleField" width="23%">Nomor Chassis</td>
                                        <td width="1%">:</td>
                                        <td width="26%">
                                            <asp:HiddenField ID="hdnChassisNo" runat="server" />
                                            <asp:TextBox runat="server" Width="150px" ID="txtChassisNo" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="LoadDataOnBlurChassis()"></asp:TextBox>
                                            <asp:Label ID="Label8" runat="server" Style="color: red;">*</asp:Label>
                                            <asp:Label ID="lbtnRefCustomerCode" onclick="ShowChassisSelection();" runat="server"> <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"> </asp:Label>
                                            <asp:LinkButton ID="lnkReloadChassis" runat="server" CausesValidation="False"> <img src="../images/reload.gif" style="cursor:hand" border="0" alt="Reload"> </asp:LinkButton>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%">Odometer</td>
                                        <td width="1%">:</td>
                                        <td width="25%">
                                            <asp:TextBox runat="server" Width="150px" ID="txtOdo" onkeypress="return NumericOnlyWith(event,'');"  onblur="LoadDataOnBlurODO()"></asp:TextBox>
                                            <asp:Label ID="Label7" runat="server" Style="color: red;">*</asp:Label>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" style="display: none;"> <img src="../images/reload.gif" style="cursor:hand" border="0" alt="Reload"> </asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%">Nomor MSP Extended Lama</td>
                                        <td width="1%">:</td>
                                        <td width="26%">
                                            <asp:HiddenField ID="hdnMSPLama" runat="server" />
                                            <asp:TextBox runat="server" Width="150px" ID="txtMSPLama" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="LoadDataOnBlurMSP()"></asp:TextBox>
                                            <asp:Label ID="Label1" onclick="ShowMSPLama();" runat="server"> <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"> </asp:Label>
                                            <asp:LinkButton ID="lnkReloadMSPLama" runat="server" CausesValidation="False"> <img src="../images/reload.gif" style="cursor:hand" border="0" alt="Reload"> </asp:LinkButton>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%">Tipe MSP Extended</td>
                                        <td width="1%">:</td>
                                        <td width="25%">
                                            <asp:DropDownList ID="ddlTipeMSP" runat="server" AutoPostBack="true"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%">Tipe Kendaraan</td>
                                        <td width="1%">:</td>
                                        <td width="26%">
                                            <asp:Label ID="lblTipeKendaraan" runat="server"></asp:Label>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%">Durasi/KM</td>
                                        <td width="1%">:</td>
                                        <td width="25%">
                                            <asp:Label ID="lblDurasi" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%">Tanggal Buka Faktur</td>
                                        <td width="1%">:</td>
                                        <td width="26%">
                                            <asp:Label ID="lblTglBukaFaktur" runat="server"></asp:Label>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%">Valid Sampai Tanggal</td>
                                        <td width="1%">:</td>
                                        <td width="25%">
                                            <asp:Label ID="lblValidSampaiTanggal" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%">Tanggal PKT</td>
                                        <td width="1%">:</td>
                                        <td width="26%">
                                            <asp:Label ID="lblTanggalPKT" runat="server"></asp:Label>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%">Valid Sampai KM</td>
                                        <td width="1%">:</td>
                                        <td width="25%">
                                            <asp:Label ID="lblValidSampaiKM" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%">Nomor Mesin</td>
                                        <td width="1%">:</td>
                                        <td width="26%">
                                            <asp:TextBox runat="server" Width="150px" ID="txtNoMesin" onkeypress="return alphaNumericExcept(event,'<>?*%$')"></asp:TextBox>
                                            <asp:Label ID="lblstar1" runat="server" Style="color: red;">*</asp:Label>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%">Tanggal Valid Warranty</td>
                                        <td width="1%">:</td>
                                        <td width="25%">
                                            <asp:Label ID="lblWarrantyValidSampaiDate" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%">No. KTP</td>
                                        <td width="1%">:</td>
                                        <td width="26%">
                                            <asp:TextBox runat="server" Width="150px" ID="txtNoKTP" onkeypress="return NumericOnlyWith(event,'');"></asp:TextBox>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%">KM Valid Warranty</td>
                                        <td width="1%">:</td>
                                        <td width="25%">
                                            <asp:Label ID="lblWarrantyValidSampaiKM" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%">Nama STNK</td>
                                        <td width="1%">:</td>
                                        <td width="26%">
                                            <asp:TextBox runat="server" Width="150px" ID="txtNama" onkeypress="return alphaNumericExcept(event,'<>?*%$')"></asp:TextBox>
                                            <asp:Label ID="Label4" runat="server" Style="color: red;">*</asp:Label>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%">Harga</td>
                                        <td width="1%">:</td>
                                        <td width="25%">
                                            <asp:Label ID="lblHarga" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%">Usia</td>
                                        <td width="1%">:</td>
                                        <td width="26%">
                                            <asp:TextBox runat="server" Width="32px" ID="txtUsia" onkeypress="return NumericOnlyWith(event,'');"></asp:TextBox>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%">PPN</td>
                                        <td width="1%">:</td>
                                        <td width="25%">
                                            <asp:Label ID="lblPPN" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%">Alamat</td>
                                        <td width="1%">:</td>
                                        <td width="26%">
                                            <asp:TextBox runat="server" Width="150px" ID="txtAlamat" onkeypress="return alphaNumericExcept(event,'<>?*%$')"></asp:TextBox>
                                            <asp:Label ID="Label5" runat="server" Style="color: red;">*</asp:Label>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%">Total Harga (Include PPN)</td>
                                        <td width="1%">:</td>
                                        <td width="25%">
                                            <asp:Label ID="lblTotalHarga" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%">Kelurahan</td>
                                        <td width="1%">:</td>
                                        <td width="26%">
                                            <asp:TextBox runat="server" Width="150px" ID="txtKelurahan" onkeypress="return alphaNumericExcept(event,'<>?*%$')"></asp:TextBox>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%"></td>
                                        <td width="1%"></td>
                                        <td width="25%"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%">Kecamatan</td>
                                        <td width="1%">:</td>
                                        <td width="26%">
                                            <asp:TextBox runat="server" Width="150px" ID="txtKecamatan" onkeypress="return alphaNumericExcept(event,'<>?*%$')"></asp:TextBox>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%"></td>
                                        <td width="1%"></td>
                                        <td width="25%"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1" width="23%">Provinsi</td>
                                        <td width="1%" class="auto-style2">:</td>
                                        <td width="26%" class="auto-style2">
                                            <asp:DropDownList ID="ddlPropinsi" runat="server" AutoPostBack="true"></asp:DropDownList>
                                        </td>
                                        <td width="1%" class="auto-style2"></td>
                                        <td class="auto-style1" width="23%"></td>
                                        <td width="1%" class="auto-style2"></td>
                                        <td width="25%" class="auto-style2"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%">Kota/Kabupaten</td>
                                        <td width="1%">:</td>
                                        <td width="26%">
                                            <asp:DropDownList ID="ddlPreArea" runat="server">
                                                <asp:ListItem Value="" Selected="True">Silahkan Pilih</asp:ListItem>
                                                <asp:ListItem Value="KAB">KAB</asp:ListItem>
                                                <asp:ListItem Value="KODYA">KODYA</asp:ListItem>
                                                <asp:ListItem Value="KABUPATEN">KABUPATEN</asp:ListItem>
                                                <asp:ListItem Value="KOTA MADYA">KOTAMADYA</asp:ListItem>
                                                <asp:ListItem Value="KOTA">KOTA</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlKota" runat="server"></asp:DropDownList>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%"></td>
                                        <td width="1%"></td>
                                        <td width="25%"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%">No Telp/HP</td>
                                        <td width="1%">:</td>
                                        <td width="26%">
                                            <asp:TextBox runat="server" Width="150px" ID="txtNoTelp" onkeypress="return NumericOnlyWith(event,'');"></asp:TextBox>
                                            <asp:Label ID="Label6" runat="server" Style="color: red;">*</asp:Label>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%"></td>
                                        <td width="1%"></td>
                                        <td width="25%"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%">Email</td>
                                        <td width="1%">:</td>
                                        <td width="26%">
                                            <asp:TextBox runat="server" Width="150px" ID="txtEmail" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtEmail)"></asp:TextBox>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%"></td>
                                        <td width="1%"></td>
                                        <td width="25%"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%"></td>
                                        <td width="1%"></td>
                                        <td width="26%"></td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%"></td>
                                        <td width="1%"></td>
                                        <td width="25%"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="23%"></td>
                                        <td width="1%"></td>
                                        <td width="26%">
                                            <input id="Hidden1" type="hidden" value="-1" runat="server">
                                            <input id="hdConfirm" type="hidden" value="-1" runat="server">
                                            <asp:Button ID="btnSave" runat="server" Text="Simpan" OnClientClick="if (btnSaveOnClick() == false) return(false);"></asp:Button>
                                            <asp:Button ID="btnValidasi" runat="server" Text="Validasi"></asp:Button>
                                            <asp:Button ID="btnBack" runat="server" Text="Tutup"></asp:Button>
                                        </td>
                                        <td width="1%"></td>
                                        <td class="titleField" width="23%"></td>
                                        <td width="1%"></td>
                                        <td width="25%"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
