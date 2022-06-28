<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmWorkCode.aspx.vb" Inherits="FrmWorkCode"  smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>FrmVehicleModel</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 16px">WSC - Deskripsi Kode Kerja</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="1"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="2" cellPadding="2">
							<TR>
								<TD class="titleField" width="24%">Kode</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtKode" onblur="HtmlCharBlur(txtKode)"
										runat="server" MaxLength="4"></asp:textbox>&nbsp;<asp:requiredfieldvalidator id="rfvKode" runat="server" EnableClientScript="False" Width="16px" InitialValue=" "
										ControlToValidate="txtKode" ErrorMessage="*"></asp:requiredfieldvalidator></td>
							</TR>
							<TR>
								<TD class="titleField">Deskripsi</TD>
								<TD>:</TD>
								<td><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtDeskripsi" onblur="HtmlCharBlur(txtDeskripsi)"
										width="300px" runat="server" MaxLength="40"></asp:textbox>&nbsp;<asp:requiredfieldvalidator id="rfvDeskripsi" runat="server" EnableClientScript="False" Width="11px" ControlToValidate="txtDeskripsi"
										ErrorMessage="*"></asp:requiredfieldvalidator><asp:dropdownlist id="ddlStatus" runat="server" Visible="False"></asp:dropdownlist></td>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<td><asp:button id="btnSimpan" runat="server" width="60px" Text="Simpan"></asp:button>&nbsp;
									<asp:button id="btnBatal" runat="server" width="60px" Text="Batal" CausesValidation="False"></asp:button>&nbsp;
									<asp:button id="btnCari" runat="server" width="60px" Text=" Cari " CausesValidation="False"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 360px"><asp:datagrid id="dtgModel" runat="server" Width="100%" AllowSorting="True" BorderColor="#E0E0E0"
								BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0" GridLines="None" CellPadding="3" AutoGenerateColumns="False" ToolTip="Model Kendaraan"
								CellSpacing="1" AllowPaging="True" AllowCustomPaging="True" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="KodeKerja" HeaderText="Kode">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblKode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.KodeKerja") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.KodeKerja") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Description" HeaderText="Deskripsi">
										<HeaderStyle Width="40%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" SortExpression="Status" HeaderText="Status">
										<HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblStatus runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="btnLihat" runat="server" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="btnUbah" runat="server" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="btnHapus" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
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
