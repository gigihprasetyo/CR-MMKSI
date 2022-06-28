<%@ Import Namespace="KTB.DNet.Domain"%>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPurchaseOrderEstimateDetail.aspx.vb" Inherits="FrmPurchaseOrderEstimateDetail" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PURCHASE ORDER ESTIMATION DETAIL</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="4" cellPadding="0" width="100%" border="0">
				<tr>
					<td height="20">
						<TABLE id=Table5 cellSpacing=0 cellPadding=0 width="100%" border=0>
						<TR>
							<TD class=titlePage>PEMESANAN - Rincian Estimasi Pesanan </TD></TR>
						<tr>
							<td background=../images/bg_hor.gif height=1><IMG height=1 src="../images/bg_hor.gif" border=0 ></TD></TR>
						<tr>
							<td height=10><IMG height=1 src="../images/dot.gif" border=0 ></TD></TR>
						</table>					
					</td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%"><asp:label id="Label1" runat="server"> Kode Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD width="34%"><asp:label id="lblDealerCode" runat="server"></asp:label></TD>
								<TD class="titleField" width="20%"><asp:label id="Label11" runat="server">Nilai Tagihan (Rp)</asp:label></TD>
								<TD width="1%"><asp:label id="Label14" runat="server">:</asp:label></TD>
								<TD width="20%" align="right"><asp:label id="lblTotAllocAmount" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">
									<asp:label id="Label19" runat="server">Nama Dealer</asp:label></TD>
								<TD>:</TD>
								<TD>
									<asp:label id="lblDealerName" runat="server"></asp:label>&nbsp;/
									<asp:label id="lblDealerTerm" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="Label12" runat="server">PPN (Rp)</asp:label></TD>
								<TD><asp:label id="Label18" runat="server">:</asp:label></TD>
								<TD align="right"><asp:label id="lblTotTax" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label2" runat="server">Jenis Order</asp:label></TD>
								<TD><asp:label id="Label7" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblOrderType" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="Label17" runat="server" Width="191px">Deposit C2 - Hanya Untuk RO (Rp) </asp:label></TD>
								<TD><asp:label id="Label15" runat="server">:</asp:label></TD>
								<TD align="right"><asp:label id="lblDepositC2" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label3" runat="server">Nomor Pesanan - Tanggal</asp:label></TD>
								<TD><asp:label id="Label8" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblPO" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="Label13" runat="server"> Total Tagihan (Rp)</asp:label></TD>
								<TD><asp:label id="Label16" runat="server">:</asp:label></TD>
								<TD align="right"><asp:label id="lblGrandAmount" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label4" runat="server">Nomor Penjualan MMKSI - Tanggal</asp:label></TD>
								<TD><asp:label id="Label9" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblSO" runat="server"></asp:label></TD>
								<TD class="titleField">Cara Pembayaran</TD>
								<TD>:</TD>
								<TD><asp:label id="lblCaraPembayaran" runat="server"></asp:label></TD>
							<TR>
								<TD class="titleField"><asp:label id="Label5" runat="server"> Jadwal Pengiriman / Pembayaran</asp:label></TD>
								<TD><asp:label id="Label10" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblSchedule" runat="server"></asp:label></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD align=center> <div id=div1 style="OVERFLOW: auto; HEIGHT: 250px"><asp:datagrid id="dgEstimateDetail" runat="server" Width="100%" AutoGenerateColumns="False" BorderWidth="0px"
							CellSpacing="1" CellPadding="3" PageSize="50" BackColor="Gainsboro" BorderColor="Gainsboro"
							AllowPaging="True" AllowCustomPaging="True" AllowSorting="True">
							<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
							<HeaderStyle Font-Bold="True" Height="30px" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
									<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="No">
									<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" ></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PartNumber" HeaderText="Nomor Barang" SortExpression="PartNumber">
									<HeaderStyle Width="12%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PartName" HeaderText="Nama Barang" SortExpression="PartName">
									<HeaderStyle Width="23%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="OrderQty" HeaderText="Jumlah Pesanan" DataFormatString="{0:#,##0}" SortExpression="OrderQty">
									<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" ></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AllocQty" HeaderText="Jumlah Pemenuhan" DataFormatString="{0:#,##0}" SortExpression="AllocQty">
									<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" ></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AllocationQty" HeaderText="Jumlah Sudah Dialokasi" DataFormatString="{0:#,##0}" SortExpression="AllocationQty">
									<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" ></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="OpenQty" HeaderText="Jumlah Belum Dialokasi" DataFormatString="{0:#,##0}" SortExpression="OpenQty">
									<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" ></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Harga Eceran (Rp)" SortExpression="RetailPrice">
									<HeaderStyle Width="9%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" ></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblPrice runat="server" Text='<%# String.Format("{0:#,##0}", CType(Container.DataItem, SparePartPOEstimateDetail).RetailPrice) %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nilai Pemenuhan (Rp)">
									<HeaderStyle Width="9%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" ></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblAllocAmount runat="server" Text='<%# String.Format("{0:#,##0}", CType(Container.DataItem, SparePartPOEstimateDetail).Amount) %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="AltPartNumber" HeaderText="Nomor Pengganti" SortExpression="AltPartNumber">
									<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>									
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Diskon (%)" SortExpression="Discount">
									<HeaderStyle Width="7%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" ></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblDiscount runat="server" Text='<%# String.Format("{0:0.00}", CType(Container.DataItem, SparePartPOEstimateDetail).Discount)%>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tagihan (Rp)">
									<HeaderStyle Width="9%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" ></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblTotal runat="server" Text='<%# String.Format("{0:#,##0}", CType(Container.DataItem, SparePartPOEstimateDetail).TotalAmount) %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></div>
						<INPUT style="WIDTH: 64px; HEIGHT: 24px" onclick="window.close()" type="button" value="Tutup">
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

<!--
    <asp:BoundColumn DataField="AllocationQty" HeaderText="Jumlah Sudah Dialokasi" DataFormatString="{0:#,##0}">
									<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" ></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="OpenQty" HeaderText="Jumlah Belum Dialokasi" DataFormatString="{0:#,##0}">
									<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" ></ItemStyle>
								</asp:BoundColumn>
								
-->