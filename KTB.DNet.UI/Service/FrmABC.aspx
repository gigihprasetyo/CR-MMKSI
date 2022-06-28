<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmABC.aspx.vb" Inherits="FrmABC" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>MAINTENANCE - Area 2</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">WSC- Kode Failure</td>
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
								<TD class="titleField" style="HEIGHT: 25px" width="24%">Tipe&nbsp;ABC</TD>
								<TD style="HEIGHT: 25px" width="1%">:</TD>
								<td style="HEIGHT: 25px" width="75%"><asp:textbox id="txtTipeABC" onkeypress="return HtmlCharUniv(event)"  onblur="HtmlCharBlur(txtKodeArea)"
										runat="server" Width="72px" MaxLength="5"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtTipeABC" ErrorMessage="*"></asp:requiredfieldvalidator></td>
							</TR>
							<TR>
								<TD class="titleField">Kode&nbsp;ABC</TD>
								<TD>:</TD>
								<td><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtKodeABC" onblur="HtmlCharBlur(txtNamaArea)"
										runat="server" Width="120px" MaxLength="50"></asp:textbox>&nbsp;<asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtKodeABC" ErrorMessage="*"></asp:requiredfieldvalidator></td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px">Grup Posisi</TD>
								<TD style="HEIGHT: 26px">:</TD>
								<TD style="HEIGHT: 26px"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtPosisiABC"  onblur="HtmlCharBlur(txtNamaArea)"
										runat="server" Width="120px" MaxLength="50"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" ControlToValidate="txtPosisiABC" ErrorMessage="*"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<td><asp:button id="btnSave" runat="server" Text="Simpan"></asp:button>
									<asp:Button id="btnCari" runat="server" Text="Cari" CausesValidation="False" Width="48px"></asp:Button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 370px"><asp:datagrid id="dtgArea" runat="server" Width="80%" CellSpacing="1" AutoGenerateColumns="False"
								PageSize="25" AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" BorderColor="#E0E0E0" BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0"
								CellPadding="3" GridLines="None">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#f6f6f6"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="ID">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="CategoryCode" SortExpression="CategoryCode" HeaderText="Tipe ABC">
										<HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PostionCode" SortExpression="PostionCode" HeaderText="Kode ABC">
										<HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Code" SortExpression="Code" HeaderText="Grup Posisi">
										<HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
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
