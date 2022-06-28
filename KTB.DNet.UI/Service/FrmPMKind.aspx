<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPMKind.aspx.vb" Inherits="FrmPMKind" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Jenis Free Service</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PERIODICAL MAINTENANCE -&nbsp; Jenis PM</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Jenis PM</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharUniv(txtKindCode)" id="txtKindCode"
										runat="server" Width="70px" MaxLength="2"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Silahkan isi kode Free Service (tidak boleh kosong)"
										ControlToValidate="txtKindCode" BorderStyle="Inset" Display="None"></asp:requiredfieldvalidator>
									<asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" ControlToValidate="txtKindCode" ErrorMessage="*"></asp:RequiredFieldValidator></td>
							</TR>
							<TR>
								<TD class="titleField" width="24%">KM</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox id="TxtKM" runat="server" Width="70px" onkeypress="return NumericOnlyWith(event,'');"
										onkeyup="pic(this,this.value,'9999999999','N')" MaxLength="7" ToolTip="Harus diisi dengan angka"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ErrorMessage="Silahkan isi KM dengan angka (tidak boleh kosong)"
										ControlToValidate="TxtKM" Display="None"></asp:requiredfieldvalidator>
									<asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" ControlToValidate="TxtKM" ErrorMessage="*"></asp:RequiredFieldValidator></TD>
							</TR>
							<TR>
								<TD class="titleField">Deskripsi</TD>
								<TD>:</TD>
								<td><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtKindDesc" runat="server" Width="304px"
										MaxLength="30"></asp:textbox>
									<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtKindDesc" ErrorMessage="Silahkan isi deskripsi"
										Display="None"></asp:RequiredFieldValidator>
									<asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" ControlToValidate="txtKindDesc" ErrorMessage="*"></asp:RequiredFieldValidator></td>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<td>&nbsp;
									<asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button>
									<asp:ValidationSummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"
										DisplayMode="SingleParagraph"></asp:ValidationSummary></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top"><div id="div1" style="OVERFLOW: auto; HEIGHT: 370px"><asp:datagrid id="dtgPMKind" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" GridLines="Horizontal"
								CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True"
								PageSize="25" AllowPaging="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="KindCode" SortExpression="KindCode" HeaderText="Jenis PM">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="KM" SortExpression="KM" HeaderText="KM" DataFormatString="{0:#,##0}">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="KindDescription" SortExpression="KindDescription" HeaderText="Deskripsi">
										<HeaderStyle Width="40%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
												CommandName="View">
												<img alt="Lihat Detil" src="../images/detail.gif" border="0"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img alt="Hapus" src="../images/trash.gif" border="0"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
			</TABLE>
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
