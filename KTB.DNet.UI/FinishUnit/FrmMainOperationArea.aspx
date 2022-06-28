<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmMainOperationArea.aspx.vb" Inherits="FrmMainOperationArea" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FAKTUR KENDARAAN - Main Operation Area</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">FAKTUR KENDARAAN - Daerah Utama Operasi</TD>
				</TR>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD width="24%" class="titleField">Kode</TD>
								<td width="1%">:</td>
								<TD width="75%">
									<asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtCodeMainOperationArea)" id="txtCodeMainOperationArea" runat="server" MaxLength="3"></asp:textbox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" ControlToValidate="txtCodeMainOperationArea"
										ErrorMessage="*" EnableClientScript="False"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField">Deskripsi</TD>
								<td>:</td>
								<TD>
									<asp:TextBox onkeypress="return HtmlCharUniv(event)"  onblur="HtmlCharBlur(txtDeskripsiMainOperationArea)" id="txtDeskripsiMainOperationArea" runat="server" MaxLength="30"></asp:TextBox>
									<asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtDeskripsiMainOperationArea"
										ErrorMessage="*" EnableClientScript="False"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField">Status</TD>
								<td>:</td>
								<TD>
									<asp:CheckBox Checked="True" id="chkStatusMainOperationArea" runat="server" Text="Aktif"></asp:CheckBox></TD>
							</TR>
							<TR>
								<TD></TD>
								<td></td>
								<TD>
									<asp:button id="btnSimpan" runat="server" Text="Simpan" Width="60px"></asp:button>
									<asp:button id="btnBatal" runat="server" Text="Batal" Width="60px" CausesValidation="False"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:DataGrid id="dtgMainOperationArea" runat="server" Width="100%" AllowCustomPaging="True" AllowPaging="True"
							AllowSorting="True" GridLines="None" BorderWidth="0px" 
						BorderStyle="None" AutoGenerateColumns="False" BorderColor="#CDCDCD" BackColor="#CDCDCD" CellPadding="3" CellSpacing="1">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
									<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" Runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Code" SortExpression="Code" HeaderText="Kode">
									<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
									<HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="Status" HeaderText="StatusTemp">
									<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
									<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnView" runat="server" Text="Lihat" Width="20px" CausesValidation="False"
											CommandName="View">
											<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
										<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" Width="20px" CausesValidation="False" CommandName="Edit">
											<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
										<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
											<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></TD>
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
