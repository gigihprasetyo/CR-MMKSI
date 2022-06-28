<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmVehicleColor.aspx.vb" Inherits="FrmVehicleColor"  smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmVehicleModel</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="titlePage">UMUM - Daftar Warna Kendaraan</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
				</tr>
				<tr>
					<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="2" width="100%">
							<TR>
								<TD class="titleField" width="20%">Lokasi File</TD>
								<TD width="1%">:</TD>
								<td width="79%"><INPUT id="DataFile" onkeypress="return false;" size="50" type="file" name="File1" runat="server">
									<asp:button id="btnUpload" runat="server" Width="60px" Text="Upload"></asp:button><asp:button id="btnStore" runat="server" Width="60px" Text="Simpan" Enabled="False"></asp:button></td>
							</TR>
							<TR>
								<TD class="titleField">Status</TD>
								<TD>:</TD>
								<td><asp:dropdownlist id="ddlStatus" runat="server"></asp:dropdownlist></td>
							</TR>
							<TR>
								<TD class="titleField">Kategori</TD>
								<TD>:</TD>
								<td><asp:dropdownlist id="ddlCategory" runat="server" AutoPostBack="True"></asp:dropdownlist><asp:dropdownlist style="Z-INDEX: 0" id="ddlSubCategory" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
							</TR>
							<TR>
								<TD class="titleField">Tipe</TD>
								<TD>:</TD>
								<TD><asp:dropdownlist id="ddlType" runat="server"></asp:dropdownlist></TD>
							</TR>
							<tr>
								<td></td>
								<td></td>
								<td><asp:button id="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:button></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div style="HEIGHT: 320px; OVERFLOW: auto" id="div1"><asp:datagrid id="dgColorUpload" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#E0E0E0"
								BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal" CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="SpecialFlag" HeaderText="Warna Khusus">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Kategori">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCategoryCode" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="HeaderBOM" HeaderText="Kode BOM">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Kode Tipe">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblVehicleType" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ColorCode" HeaderText="Kode Warna">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ColorEngName" HeaderText="B. Inggris">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ColorIndName" HeaderText="B. Indonesia">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="MaterialDescription" HeaderText="Nama Kendaraan">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ErrorMessage" HeaderText="Pesan">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="MaterialNumber" HeaderText="Material">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="MaterialDescription" HeaderText="Mat Desc">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="MarketCode" HeaderText="Market">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid><asp:datagrid id="dgVehicleColor" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#E0E0E0"
								BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal" CellSpacing="1"
								AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle VerticalAlign="Top" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="#4A3C8C" VerticalAlign="Top" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="SpecialFlag" SortExpression="SpecialFlag" ReadOnly="True" HeaderText="Warna Khusus">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="VechileType.Category.CategoryCode" HeaderText="Kategori">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Category.CategoryCode") %>' ID="Label3">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="HeaderBOM" SortExpression="HeaderBOM" ReadOnly="True" HeaderText="Kode BOM">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="VechileType.VechileTypeCode" HeaderText="Kode Tipe">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.VechileTypeCode") %>' ID="Label1">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ColorCode" SortExpression="ColorCode" ReadOnly="True" HeaderText="Kode Warna">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ColorEngName" SortExpression="ColorEngName" ReadOnly="True" HeaderText="B. Inggris">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ColorIndName" SortExpression="ColorIndName" ReadOnly="True" HeaderText="B. Indonesia">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="MaterialDescription" SortExpression="MaterialDescription" ReadOnly="True"
										HeaderText="Nama Kendaraan">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="MaterialNumber" SortExpression="MaterialNumber" ReadOnly="True"
										HeaderText="Material"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="MaterialDescription" SortExpression="MaterialDescription"
										ReadOnly="True" HeaderText="Mat Desc">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="MarketCode" SortExpression="MarketCode" ReadOnly="True"
										HeaderText="Market">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Del" CommandName="Delete">
												<img src="../images/trash.gif" alt="Hapus" border="0"></asp:LinkButton>
											<asp:LinkButton id="lbtnActive" runat="server" Text="Undel" CommandName="Active">
												<img src="../images/aktif.gif" border="0" alt="Aktifkan"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD height="40" align="left"><asp:button id="btnDnLoad" runat="server" Width="60px" Text="Download" Enabled="False"></asp:button></TD>
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
