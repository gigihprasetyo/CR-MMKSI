<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmVehicleColorInformation.aspx.vb" Inherits="FrmVehicleColorInformation" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmVehicleColorInformation</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">UMUM - Informasi Warna Kendaraan</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><img src="../images/bg_hor.gif" height="1" border="0"></td>
				</tr>
				<tr>
					<td height="10"><img src="../images/dot.gif" height="1" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellPadding="2" width="100%" border="0">
							<TR>
								<TD WIDTH="24%" class="titleField" style="HEIGHT: 23px"><asp:label id="lblCategory" runat="server"> Kategori</asp:label></TD>
								<TD WIDTH="1%" style="HEIGHT: 23px">:</TD>
								<td width="147" style="WIDTH: 147px; HEIGHT: 23px"><asp:dropdownlist id="ddlCategory" runat="server" Width="140px" AutoPostBack="True"></asp:dropdownlist></td>
								<TD width="55%" style="HEIGHT: 23px">
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlSubCategory" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblType" runat="server"> Tipe</asp:label></TD>
								<TD WIDTH="1%">:</TD>
								<TD style="WIDTH: 147px"><asp:dropdownlist id="ddlType" runat="server" Width="140px"></asp:dropdownlist></TD>
								<TD><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button></TD>
							</TR>
							<TR>
								<TD height="10" colspan="4"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD valign="top"><div id="div1" style="HEIGHT: 370px; OVERFLOW: auto"><asp:datagrid id="dgColor" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#E0E0E0"
								BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="None" CellSpacing="1" AllowCustomPaging="True" AllowPaging="True" AllowSorting="True"
								PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="VechileType.Category.Description" HeaderText="Kategori">
										<HeaderStyle Width="22%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Category.Description")%>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="VechileType.VechileTypeCode" HeaderText="Kode Tipe">
										<HeaderStyle Width="13%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.VechileTypeCode") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ColorCode" SortExpression="ColorCode" ReadOnly="True" HeaderText="Kode Warna">
										<HeaderStyle Width="13%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="VechileType.Description" HeaderText="Nama Tipe">
										<HeaderStyle Width="22%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Description") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ColorEngName" SortExpression="ColorEngName" ReadOnly="True" HeaderText="B.Inggris">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ColorIndName" SortExpression="ColorIndName" ReadOnly="True" HeaderText="B.Indonesia">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD height="8" style="HEIGHT: 8px">
						<asp:button id="btnDnLoad" runat="server" Width="60px" Text="Download" Enabled="False"></asp:button></TD>
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
