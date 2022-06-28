<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpSPPODetail.aspx.vb" Inherits="PopUpSPPODetail" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Daftar Pesanan Detail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PEMESANAN - Pesanan</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>
								<td class="titleField" width="24%">Kode Dealer</td>
								<td width="1%">:</td>
								<TD width="20%"><asp:label id="lblDealerCode" runat="server"></asp:label></TD>
								<td width="50%"></td>
							</tr>
							<TR>
								<TD class="titleField">Nama Dealer</TD>
								<td width="1%">:</td>
								<TD><asp:label id="lblDealerName" runat="server"></asp:label>/
									<asp:label id="lblDealerTerm" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Tipe Order</TD>
								<td width="1%">:</td>
								<TD><asp:dropdownlist id="ddlOrderType" runat="server" Width="140px" Enabled="False"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Nomor /Tanggal PO</TD>
								<td width="1%">:</td>
								<TD width="20%"><asp:textbox id="txtPONumber" runat="server" size="22" ReadOnly="True" Enabled="False">[Dibuat oleh sistem]</asp:textbox></TD>
								<TD width="55%"><asp:label id="LblPODate" runat="server"></asp:label></TD>
							</TR>
							<tr>
								<td class="titleField">Nilai Pemesanan</td>
								<td width="1%">:</td>
								<td><asp:label id="lblTotPOAmount" runat="server">0</asp:label></td>
								<td></td>
							</tr>
							<tr>
								<td class="titleField">Cara Pembayaran</td>
								<td width="1%">:</td>
								<td><asp:label id="lblTOP" runat="server"></asp:label></td>
								<td></td>
							</tr>
						</TABLE>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 280px"><asp:datagrid id="dtgPODetail" runat="server" Width="100%" AutoGenerateColumns="False" ShowFooter="True"
								CellPadding="3" CellSpacing="1" BackColor="#CDCDCD" BorderColor="Gainsboro" BorderWidth="0px" AllowSorting="True" EnableViewState="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn ReadOnly="True" HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.PartNumber" HeaderText="Nomor Barang">
										<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.PartName" HeaderText="Nama Barang">
										<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblPartname runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Quantity" HeaderText="Jumlah">
										<HeaderStyle HorizontalAlign="Right" Width="12%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' CssClass="textRight">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="RetailPrice" HeaderText="Harga Eceran">
										<HeaderStyle HorizontalAlign="Right" Width="12%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblRetailPrice runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RetailPrice","{0:#,##0}") %>' CssClass="textRight">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Amount" HeaderText="Total Harga">
										<HeaderStyle HorizontalAlign="Right" Width="12%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblPOAmount runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Amount","{0:#,##0}") %>' CssClass="textRight">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD align="center">&nbsp;<INPUT id="btnBack" onclick="window.close()" type="button" value="Tutup">
					</TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
			&nbsp;&nbsp;
		</form>
	</body>
</HTML>
