<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSPPOFakturDetail.aspx.vb" Inherits="FrmSPPOFakturDetail" smartNavigation="False"%>
<%--<%@ Import Namespace="KTB.DNet.Domain"%>--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>Pemesanan - Detail Faktur</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
      	<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">PEMESANAN - Detail Faktur</TD>
				</TR>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" width="200">Kode Dealer</TD>
								<TD width="5">:</TD>
								<TD><asp:label id="lblDealerCode" runat="server">Label</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Dealer</TD>
								<TD>:</TD>
								<TD><asp:label id="lblDealerName" runat="server">Label</asp:label>&nbsp;/
									<asp:label id="lblDealerTerm" runat="server">Label</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Tipe Order</TD>
								<TD>:</TD>
								<TD><asp:label id="lblOrderType" runat="server">Label</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Nomor Pesanan - Tanggal</TD>
								<TD>:</TD>
								<TD><asp:label id="lblOrder" runat="server">Label</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Nomor DO MMKSI - Tanggal</TD>
								<TD>:</TD>
								<TD><asp:label id="lblDO" runat="server">Label</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Nomor Faktur - Tanggal</TD>
								<TD>:</TD>
								<TD><asp:label id="lblInvoice" runat="server">Label</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Total Nilai Tagihan (Rp)</TD>
								<TD>:</TD>
								<TD><asp:label id="lblTotalNilaiTagihan" runat="server">Label</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Cara Pembayaran</TD>
								<TD>:</TD>
								<TD><asp:label id="lblCaraPembayaran" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD>&nbsp;</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><div id="div1" style="OVERFLOW: auto; HEIGHT: 320px">
                        <asp:datagrid id="dtgSPPOStatusDetail" runat="server" AllowCustomPaging="False" AllowPaging="False"
							AllowSorting="True" PageSize="10" BorderWidth="0px" BackColor="Gainsboro" BorderColor="Gainsboro"
							CellSpacing="1" CellPadding="3" Width="100%" AutoGenerateColumns="False">
							<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
							<HeaderStyle Font-Bold="True" Height="30px" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="id" HeaderText="ID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server">Label</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="SparePartMaster.PartNumber" HeaderText="Nomor Barang">
									<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblNomorBarang" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="SparePartMaster.PartName" HeaderText="Nama Barang">
									<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
                                        <asp:Label ID="lblNamaBarang" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartDODetail.SparePartMaster.PartName") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="SparePartDODetail.Qty" HeaderText="Jumlah Pesanan">
									<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
                                        <asp:Label ID="lblJmlPesanan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartDODetail.Qty","{0:#,##0}") %>' CssClass="textRight"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Quantity" HeaderText="Jumlah Dipenuhi">
									<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
                                        <asp:Label ID="lblJmlDipenuhi" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity","{0:#,##0}") %>' CssClass="textRight"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="ItemPrice" HeaderText="Harga Jual MMKSI (Rp)">
									<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
                                        <asp:Label ID="lblHargaJualKTB" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemPrice","{0:#,##0}") %>' CssClass="textRight"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Tax" HeaderText="PPn (Rp)">
									<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
                                        <asp:Label ID="lblTax" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Tax","{0:#,##0}") %>' CssClass="textRight"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nilai Tagihan (Rp)" SortExpression="TotalPrice">
									<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
                                        <asp:Label ID="lblNilaiTagihan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalPrice","{0:#,##0}") %>' CssClass="textRight"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<%--<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>--%>
						</asp:datagrid></div></TD>
				</TR>
				<TR>
					<TD align=center>
                        <asp:Button ID="btnDownloadData" runat="server" Text="Download" />
                        <INPUT id="btnClose" onclick="window.close()" type="button" value="Tutup"></TD>
				</TR>
			</TABLE>
		</form>
        
	</body>
</HTML>
