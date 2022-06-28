<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmUserInfoDetail.aspx.vb" Inherits="frmUserInfoDetail" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>User Info Detail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td colSpan="3">
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">ADMIN SISTEM - Profil User</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD class="titleField" width="24%" height="22"><asp:label id="lblKodeOrganisasi" runat="server">Kode Organisasi</asp:label></TD>
					<TD width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
					<TD width="75%"><asp:label id="lblKodeOrganisasiValue" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblNamaOrganisasi" runat="server">Nama Organisasi</asp:label></TD>
					<TD><asp:label id="Label4" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblNamaOrganisasiValue" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblID" runat="server">ID</asp:label></TD>
					<TD><asp:label id="Label3" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblIDValue" runat="server"></asp:label></TD>
				</TR>
                <TR>
					<TD class="titleField" width="24%" height="22"><asp:label id="Label24" runat="server">Kode Sub Organisasi</asp:label></TD>
					<TD width="1%"><asp:label id="Label25" runat="server">:</asp:label></TD>
					<TD width="75%"><asp:label id="lblSubOrgCode" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label27" runat="server">Nama Sub Organisasi</asp:label></TD>
					<TD><asp:label id="Label28" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblSubOrgName" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 14px"><asp:label id="lblNamaLogin" runat="server">Nama Login</asp:label></TD>
					<TD style="HEIGHT: 14px"><asp:label id="Label5" runat="server">:</asp:label></TD>
					<TD style="HEIGHT: 14px"><asp:label id="lblNamaLoginValue" runat="server"></asp:label></TD>
				</TR>
				<TR id="rowKataKunciLama" runat="server">
					<TD class="titleField"><asp:label id="lblKataKunciLama" runat="server">Kata Kunci Lama</asp:label><asp:label id="Label22" runat="server" ForeColor="red" Visible="False">*</asp:label></TD>
					<TD><asp:label id="Label8" runat="server">:</asp:label></TD>
					<TD><asp:textbox id="txtKataKunciLama" runat="server" TextMode="Password"></asp:textbox></TD>
				</TR>
				<TR id="rowKatakunciBaru" runat="server">
					<TD class="titleField"><asp:label id="lblKataKunciBaru" runat="server">Kata Kunci Baru</asp:label><asp:label id="Label9" runat="server" ForeColor="red" Visible="False">*</asp:label></TD>
					<TD><asp:label id="Label6" runat="server">:</asp:label></TD>
					<TD><asp:textbox id="txtKataKunciBaru" runat="server" TextMode="Password"></asp:textbox><asp:regularexpressionvalidator id="RegularExpressionValidator2" runat="server" ValidationExpression="\w{4,20}|w{0}"
							ControlToValidate="txtKataKunciBaru" ErrorMessage="Minimum 4 Karakter, Maximum 20 Karakter">Min 4 Karakter, Max 20 Karakter
				</asp:regularexpressionvalidator></TD>
				</TR>
				<TR id="rowKonfimasiKataKunci" runat="server">
					<TD class="titleField"><asp:label id="lblKonfirmasiKataKunciBaru" runat="server">Konfirmasi Kata Kunci Baru</asp:label><asp:label id="Label1" runat="server" ForeColor="red" Visible="False">*</asp:label></TD>
					<TD><asp:label id="Label7" runat="server">:</asp:label></TD>
					<TD><asp:textbox id="txtKonfirmasiKataKunciBaru" runat="server" TextMode="Password"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" Visible="False" ControlToValidate="txtKonfirmasiKataKunciBaru"
							ErrorMessage="Konfirmasi Kata Kunci Baru harus diisi" Display="Dynamic" Enabled="False">*</asp:requiredfieldvalidator><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" Visible="False" ControlToValidate="txtKataKunciBaru"
							ErrorMessage="Kata Kunci Baru harus diisi" Display="Dynamic" Enabled="False">Kata Kunci Baru harus diisi</asp:requiredfieldvalidator><asp:comparevalidator id="CompareValidator1" runat="server" Visible="False" ControlToValidate="txtKonfirmasiKataKunciBaru"
							ErrorMessage="Kata Kunci dan Konfirmasi Kata kunci tidak sama" Enabled="false" ControlToCompare="txtKataKunciBaru">*</asp:comparevalidator></TD>
				</TR>
				<TR id="rowPertanyaan" runat="server">
					<TD class="titleField"><asp:label id="lblPertanyaan" runat="server">Pertanyaan</asp:label><asp:label id="Label10" runat="server" ForeColor="red">*</asp:label></TD>
					<TD><asp:label id="Label11" runat="server">:</asp:label></TD>
					<TD><asp:textbox id="txtPertanyaan" runat="server"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ControlToValidate="txtPertanyaan" ErrorMessage="Pertanyaan harus diisi"
							Display="Dynamic">Pertanyaan harus diisi</asp:requiredfieldvalidator></TD>
				</TR>
				<TR id="opJawaban" runat="server">
					<TD class="titleField"><asp:label id="lblJawaban" runat="server">Jawaban</asp:label><asp:label id="Label12" runat="server" ForeColor="red">*</asp:label></TD>
					<TD><asp:label id="Label13" runat="server">:</asp:label></TD>
					<TD><asp:textbox id="txtJawaban" runat="server"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" ControlToValidate="txtJawaban" ErrorMessage="Jawaban harus diisi">Jawaban harus diisi</asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblNamaDepan" runat="server">Nama Depan</asp:label><asp:label id="Label14" runat="server" ForeColor="red">*</asp:label></TD>
					<TD><asp:label id="Label15" runat="server">:</asp:label></TD>
					<TD><asp:textbox id="txtNamaDepan" runat="server"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator5" runat="server" ControlToValidate="txtNamaDepan" ErrorMessage="Nama Depan harus diisi">Nama Depan harus diisi</asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblNamaBelakang" runat="server">Nama Belakang</asp:label></TD>
					<TD><asp:label id="Label17" runat="server">:</asp:label></TD>
					<TD><asp:textbox id="txtNamaBelakang" runat="server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblPosisi" runat="server">Posisi</asp:label></TD>
					<TD><asp:label id="Label16" runat="server">:</asp:label></TD>
					<TD><asp:dropdownlist id="ddlPosition" runat="server" Width="152px"></asp:dropdownlist><asp:textbox id="txtPosisi" runat="server" Visible="False"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lbTelepon" runat="server">Telepon</asp:label></TD>
					<TD><asp:label id="Label18" runat="server">:</asp:label></TD>
					<TD><asp:textbox id="txtTelepon" runat="server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblEmail" runat="server">Email</asp:label></TD>
					<TD><asp:label id="Label19" runat="server">:</asp:label></TD>
					<TD><asp:textbox id="txtEmail" runat="server" Visible="False"></asp:textbox><asp:label id="lblEmailValue" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblHP" runat="server">HP</asp:label></TD>
					<TD><asp:label id="Label20" runat="server">:</asp:label></TD>
					<TD><asp:textbox id="txtHP" runat="server" Visible="False"></asp:textbox><asp:label id="lblHPValue" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblStatus" runat="server">Status</asp:label></TD>
					<TD><asp:label id="Label21" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblStatusValue" runat="server"></asp:label></TD>
				</TR>
				<TR valign="top">
					<TD class="titleField">Foto (Maks. 20KB)</TD>
					<TD>
						<asp:label id="Label23" runat="server">:</asp:label></TD>
					<TD>
						<DIV id="divPhoto" style="OVERFLOW: auto; WIDTH: 110px; HEIGHT: 110px" align="left">
							<asp:image id="photoView" runat="server" Height="100px" ImageUrl="../WebResources/GetPhoto.aspx"
								Width="100px"></asp:image></DIV>
						<INPUT id="photoSrc" onkeydown="return false" style="WIDTH: 322px; HEIGHT: 20px" type="file"
							size="35" name="photoSrc" runat="server">
						<asp:checkbox id="cbDeletePhoto" onclick="changeDeletePhoto(this.checked);" runat="server" Text="Hapus Foto"></asp:checkbox></TD>
				</TR>
				<TR>
					<TD class="titleField" colSpan="3"><asp:validationsummary id="ValidationSummary1" runat="server"></asp:validationsummary></TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 190px"><asp:datagrid id="dtgRole" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
								BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0" CellPadding="3" GridLines="Horizontal" OnItemDataBound="dtgRole_ItemDataBound"
								CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
										<HeaderStyle Width="0%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="2%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" NAME="lblNo" text= '<%# container.itemindex+1 %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Font-Size="Small"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Role.RoleName" HeaderText="Nama Role">
										<HeaderStyle Width="49%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblNamaRole runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Role.RoleName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Role.Description" HeaderText="Deskripsi">
										<HeaderStyle Width="49%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblDescription runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Role.Description") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#404040" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" colSpan="3">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td><asp:button id="btnReset" runat="server" Text="Reset"></asp:button></td>
								<td><asp:button id="btnLanjut" runat="server" Text="Lanjut"></asp:button></td>
								<td><asp:button id="btnKembali" runat="server" Text="Kembali" CausesValidation="False"></asp:button></td>
								<td><asp:button id="btnSimpan" runat="server" Text="Simpan"></asp:button></td>
								<td><asp:button id="btnSave" runat="server" Text="Simpan"></asp:button></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<div id="divAdditionalInformation" style="Z-INDEX: 101; LEFT: 460px; WIDTH: 310px; POSITION: absolute; TOP: 45px; HEIGHT: 170px; BACKGROUND-COLOR: #f5f1ee"
				align="center" runat="server"><asp:panel id="pnlAdditionalInformation" runat="server" Width="310px" BorderColor="Black" BorderStyle="Solid"
					BackColor="#F5F1EE" Height="170px" borderwidth="0">
					<asp:Label id="lblInfo" Width="310px" Height="170px" Font-Size="14" Runat="server">Selamat Datang !<br> Berhubung ini pertama kalinya<br> Anda masuk ke sistem, kami<br>Mohon kesediaannya untuk<br>memeriksa data dan<br>mengubah kata kunci Anda<br>di menu Security untuk keamanan sistem <br>Terima Kasih.
					</asp:Label>
				</asp:panel></div>
		</form>
		<script language="javascript">
			if (window.parent==window)
			{
				if (!navigator.appName=="Microsoft Internet Explorer")
				{
				  self.opener = null;
				  self.close();
				}
				else
				{
				   this.name = "origWin";
				   origWin= window.open(window.location, "origWin");
				   window.opener = top;
                   window.close();
				}
			}	
		</script>
	</body>
</HTML>
