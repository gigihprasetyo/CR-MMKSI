<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmAppSetting.aspx.vb" Inherits="FrmAppSetting" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>Maintenance - Area1 </title>
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
					<td class="titlePage">MASTER BARANG - Setting Maksimum Quantity</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="/images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">
									Kode</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtKodeArea" onblur="HtmlCharBlur(txtKodeArea)"
										runat="server" MaxLength="50" Width="150px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtKodeArea" ErrorMessage="*"></asp:requiredfieldvalidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Nilai</TD>
								<TD width="1%">:</TD>
								<TD width="75%">
									<asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtNilai" onblur="HtmlCharBlur(txtNamaArea)"
										runat="server" Width="400px" MaxLength="250"></asp:textbox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" ErrorMessage="*" ControlToValidate="txtNilai"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField">
									Deskripsi</TD>
								<TD>:</TD>
								<td><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtNamaArea" onblur="HtmlCharBlur(txtNamaArea)"
										runat="server" MaxLength="250" Width="400px"></asp:textbox>&nbsp;<asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtNamaArea" ErrorMessage="*"></asp:requiredfieldvalidator></td>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<td><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 390px"><asp:datagrid id="dtgArea" runat="server" Width="100%" CellSpacing="1" PageSize="25" AllowSorting="True"
								AllowPaging="True" AllowCustomPaging="True" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" GridLines="None" BorderStyle="None" BorderColor="#E0E0E0"
								AutoGenerateColumns="False">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Code" SortExpression="Code" HeaderText="Kode">
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Nilai" SortExpression="Nilai" HeaderText="Nilai">
									<HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
										<HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
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
