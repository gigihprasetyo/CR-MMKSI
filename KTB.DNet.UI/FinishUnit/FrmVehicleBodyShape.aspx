<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmVehicleBodyShape.aspx.vb" Inherits="FrmVehicleBodyShape" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FAKTUR KENDARAAN - Vehicle Body Shape</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">FAKTUR KENDARAAN - Bentuk Body Kendaraan</TD>
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
								<TD class="titleField">Kategori</TD>
								<td>:</td>
								<TD><asp:dropdownlist id="ddlCategory" runat="server" Width="130px"></asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlCategory"
										EnableClientScript="False"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD width="24%" class="titleField">Kode</TD>
								<td width="1%">:</td>
								<TD width="75%"><asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtCodeVehicleBodyShape)" id="txtCodeVehicleBodyShape" runat="server" MaxLength="3"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" ErrorMessage="*" ControlToValidate="txtCodeVehicleBodyShape"
										EnableClientScript="False"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField">Deskripsi</TD>
								<td>:</td>
								<TD><asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtDeskripsiVehicleBodyShape)" id="txtDeskripsiVehicleBodyShape" runat="server" MaxLength="30"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtDeskripsiVehicleBodyShape"
										EnableClientScript="False"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField">Status</TD>
								<td>:</td>
								<TD><asp:checkbox id="chkStatusVehicleBodyShape" runat="server" Text="Aktif" Checked="True"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD></TD>
								<td>:</td>
								<TD><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><div id="div1" style="OVERFLOW: auto; HEIGHT: 350px"><asp:datagrid id="dtgVehicleBodyShape" runat="server" Width="100%" AllowCustomPaging="True" AllowPaging="True"
							AllowSorting="True" BorderWidth="0px" BorderStyle="None" AutoGenerateColumns="False" BorderColor="#CDCDCD"
							BackColor="#CDCDCD" CellPadding="3" CellSpacing="1" GridLines="None">
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
								<asp:TemplateColumn SortExpression="Category.CategoryCode" HeaderText="Kategori">
									<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id=lblCategory runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Category.CategoryCode") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Code" SortExpression="Code" HeaderText="Kode">
									<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
									<HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="Status" HeaderText="StatusTemp">
									<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
									<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
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
						</asp:datagrid></div></TD>
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
