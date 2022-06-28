<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMSPRegistration.aspx.vb" Inherits=".FrmMSPRegistration" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
	<title>Registrasi MSP</title>
	<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
	<meta name="vs_defaultClientScript" content="JavaScript">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
	<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
	<script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript">
        function ShowCustomerList() {
            showPopUp('../PopUp/PopUpCustomerList.aspx?Tyjiuy678=code', '', 500, 760, CustomerSelection);
        }
        function CustomerSelection(selectedCustomer) {
            var txtRefCustomerCode = document.getElementById("txtRefCustomerCode");
            txtRefCustomerCode.value = selectedCustomer;
        }
    </script>
</head>
<body MS_POSITIONING="GridLayout">
    <form id="FrmMSPRegistration" method="post" runat="server">
		<table id="tblMSPRegistration" cellSpacing="0" cellPadding="0" width="100%" border="0">
            <tr>
				<td class="titlePage">MSP - MSP Registrasi</td>
			</tr>
            <tr>
				<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
			</tr>
            <tr>
				<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
			</tr>
            <tr>
                <td align="left">
                    <table id="Table2" border="0" cellSpacing="1" cellPadding="2" width="100%">
						<tr>
							<td class="titleField" colSpan="7">Data Pembuat Pelanggan</td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">Kode Dealer</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblDealer" runat="server"></asp:label>
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
                                <asp:label id="lblMSPNo" runat="server"></asp:label>
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
                                <asp:label id="lblSubmitBy" runat="server"></asp:label>
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
                                <asp:label id="lblCurrentDate" runat="server"></asp:label>
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
                                <asp:label id="lblStatus" runat="server"></asp:label>
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
                    <table id="titleUmum" border="0" cellSpacing="1" cellPadding="2" width="100%">
                        <tr>
							<td><asp:validationsummary id="ValidationSummary1" runat="server"></asp:validationsummary>&nbsp;</td>
						</tr>
						<tr>
							<td class="titlePanel"><b>INFORMASI PELANGGAN : UMUM</b></td>
						</tr>
                    </table>
                    <table border="0" cellSpacing="1" cellPadding="2" width="100%">
                        <tr>
							<td class="titleField" width="23%">No MSP Lama</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:TextBox ID="txtOldMSPNo" runat="server" MaxLength="10"></asp:TextBox>
                                <asp:label id="lblOldMSPNo" runat="server" Visible="false"></asp:label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%">Ref Kode Pelanggan</td>
							<td width="1%">:</td>
							<td width="25%">
                                <asp:label id="lblRefCustomerCode" runat="server" Visible="False"></asp:label>
                                <asp:textbox onblur="omitSomeCharacter('txtRefCustomerCode','<>?*%$;');" id="txtRefCustomerCode" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');" runat="server" Width="88px" MaxLength="10">
                                </asp:textbox>
                                <asp:label id="lbtnRefCustomerCode" onclick="ShowCustomerList();" Runat="server">
									<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
								</asp:label>
                                <asp:linkbutton id="lnkReloadCustomer" runat="server" CausesValidation="False">
								    <img src="../images/reload.gif" style="cursor:hand" border="0" alt="Reload">
                                </asp:linkbutton>
							</td>
						</tr>
                         <tr>
							<td class="titleField" width="23%">No KTP</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblKTPNo" runat="server" Visible="false"></asp:label>
                                <asp:TextBox ID="txtKTPNo" runat="server" MaxLength="16" onblur="numericOnlyBlur(txtKTPNo)" onkeypress="return numericOnlyUniv(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="No KTP tidak boleh kosong"
                                        ControlToValidate="txtKTPNo" Visible="false"  EnableClientScript="false" >*</asp:RequiredFieldValidator>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%"></td>
							<td width="1%"></td>
							<td width="25%"></td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">Nama</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblName" runat="server" Visible="false"></asp:label>
                                <asp:TextBox ID="txtName" runat="server" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Nama tidak boleh kosong"
                                        ControlToValidate="txtName">*</asp:RequiredFieldValidator>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%">Tipe MSP</td>
							<td width="1%">:</td>
							<td width="25%">
                                <asp:label id="lblMSPType" runat="server" Visible="false"></asp:label>
                                <asp:DropDownList runat="server" ID="ddlMSPType" AutoPostBack="true"></asp:DropDownList>
							</td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">Usia</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblAge" runat="server" Visible="false"></asp:label>
                                <asp:TextBox ID="txtAge" runat="server" Maxlength="2" onblur="numericOnlyBlur(txtAge)" onkeypress="return numericOnlyUniv(event)"></asp:TextBox>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%">Durasi/KM Package</td>
							<td width="1%">:</td>
							<td width="25%">
                                <asp:label id="lblDuration" runat="server" Visible="false"></asp:label>
                                <asp:DropDownList runat="server" ID="ddlDuration" AutoPostBack="true"></asp:DropDownList>
							</td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">Alamat</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblAddress" runat="server" Visible="false"></asp:label>
                                <asp:TextBox ID="txtAddress" runat="server" width="212px" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Alamat tidak boleh kosong"
                                        ControlToValidate="txtAddress" Enabled="false">*</asp:RequiredFieldValidator>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%">Harga MSP(Incl. PPN)</td>
							<td width="1%">:</td>
							<td width="25%">
                                <asp:label id="lblAmountMSP" runat="server"></asp:label>
							</td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">Kelurahan</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblKelurahan" runat="server" Visible="false"></asp:label>
                                <asp:TextBox ID="txtKelurahan" runat="server" MaxLength="50" onchange="this.value=this.value.toUpperCase();"></asp:TextBox>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%">Valid sampai dengan</td>
							<td width="1%">:</td>
							<td width="25%">
                                <asp:label ID="lblValidUntil" runat="server"></asp:label>
							</td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">Kecamatan</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblKecamatan" runat="server" Visible="false"></asp:label>
                                <asp:TextBox ID="txtKecamatan" runat="server" MaxLength="50" onchange="this.value=this.value.toUpperCase();"></asp:TextBox>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%">Dijual Oleh</td>
							<td width="1%">:</td>
							<td width="25%">
                                <asp:label id="lblSoldBy" runat="server" Visible="false"></asp:label>
                                <asp:dropdownlist ID="ddlSoldBy" runat="server">
                                    <asp:ListItem Value="" Selected="True">Silahkan Pilih</asp:ListItem>
							        <asp:ListItem Value="SALES">SALES</asp:ListItem>
							        <asp:ListItem Value="SERVICE">SERVICE</asp:ListItem>
                                </asp:dropdownlist>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Dijual oleh tidak boleh kosong"
                                        ControlToValidate="ddlSoldBy">*</asp:RequiredFieldValidator>
							</td>
						</tr>
                         <tr>
							<td class="titleField" width="23%">Provinsi</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblPropinsi" runat="server" Visible="False"></asp:label>
                                <asp:dropdownlist id="ddlPropinsi" runat="server" AutoPostBack="True"></asp:dropdownlist>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Provinsi tidak boleh kosong"
                                        ControlToValidate="ddlPropinsi" Enabled="false" EnableClientScript="false">*</asp:RequiredFieldValidator>
                            </td>
							<td width="1%"></td>
							<td colspan="3"></td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">Kota/Kabupaten</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblPreArea" runat="server" Visible="False"></asp:label>
                                <asp:label id="lblKota" runat="server" Visible="False"></asp:label>
                                <asp:dropdownlist id="ddlPreArea" runat="server">
							        <asp:ListItem Value="" Selected="True">Silahkan Pilih</asp:ListItem>
							        <asp:ListItem Value="KAB">KAB</asp:ListItem>
							        <asp:ListItem Value="KODYA">KODYA</asp:ListItem>
							        <asp:ListItem Value="KABUPATEN">KABUPATEN</asp:ListItem>
							        <asp:ListItem Value="KOTA MADYA">KOTAMADYA</asp:ListItem>
							        <asp:ListItem Value="KOTA">KOTA</asp:ListItem>
						        </asp:dropdownlist>
                                <asp:dropdownlist id="ddlKota" runat="server"></asp:dropdownlist>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="PreArea tidak boleh kosong"
                                        ControlToValidate="ddlPreArea" Enabled="false"  EnableClientScript="false">*</asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Kota tidak boleh kosong"
                                        ControlToValidate="ddlKota" Enabled="false"  EnableClientScript="false"></asp:RequiredFieldValidator>

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
                                <asp:label id="lblNotlp" runat="server" Visible="False"></asp:label>
                                <asp:TextBox runat="server" ID="txtNotlp" MaxLength="15" onblur="numericOnlyBlur(txtNotlp)" onkeypress="return numericOnlyUniv(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="No Telp/HP tidak boleh kosong"
                                        ControlToValidate="txtNotlp" Enabled="false">*</asp:RequiredFieldValidator>
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
                                <asp:label id="lblEmail" runat="server" Visible="False"></asp:label>
                                <asp:TextBox runat="server" ID="txtEmail" MaxLength="50"></asp:TextBox>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%"></td>
							<td width="1%"></td>
							<td width="25%"></td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">Nomor Chassis</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblChassisNumber" runat="server" Visible="False"></asp:label>
                                <asp:TextBox runat="server" ID="txtChassisNumber" AutoPostBack="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Nomor Chassis tidak boleh kosong"
                                        ControlToValidate="txtChassisNumber">*</asp:RequiredFieldValidator>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%"></td>
							<td width="1%"></td>
							<td width="25%"></td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">Tipe Kendaraan</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblVehicleType" runat="server"></asp:label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%"></td>
							<td width="1%"></td>
							<td width="25%"></td>
						</tr>
                         <tr>
							<td class="titleField" width="23%">Tanggal Buka Faktur</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblTglPKT" runat="server"></asp:label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%"></td>
							<td width="1%"></td>
							<td width="25%"></td>
						</tr>
                         <tr>
							<td class="titleField" width="23%">Nomor Mesin</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblEngineNumber" runat="server"></asp:label>
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
                <td>&nbsp;</td>
            </tr>
            <tr>
				<td>
                    <asp:button id="btnSave" runat="server" Text="Simpan"></asp:button>
                    <asp:button id="btnValidasi" runat="server" Text="Validasi" Visible="false" CausesValidation="False"></asp:button>
                    <asp:button id="btnConfirm" runat="server" CausesValidation="False" Text="Konfirmasi" Visible="false"></asp:button>
                    <%--<asp:button id="btnNew" runat="server" Text="Baru"></asp:button>--%>
                    <asp:button id="btnBack" runat="server" CausesValidation="False" Text="Tutup"></asp:button>
                 </td>
			</tr>
        </table>
    </form>
</body>
</html>
