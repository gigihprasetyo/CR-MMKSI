<%--<%@ Import Namespace="KTB.DNet.Domain"%>--%>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSPDOPackingListDetail.aspx.vb" Inherits="FrmSPDOPackingListDetail" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PEMESANAN - DETAIL PACKING LIST</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="4" cellPadding="0" width="100%" border="0">
				<tr>
					<td height="20">
						<TABLE id=Table5 cellSpacing=0 cellPadding=0 width="100%" border=0>
						<TR>
							<TD class=titlePage>PEMESANAN - DETAIL PACKING LIST</TD></TR>
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
								<TD class="titleField" width="20%"><asp:label id="Label11" runat="server">Nomor Penjualan (SO MMKSI)</asp:label></TD>
								<TD width="1%"><asp:label id="Label14" runat="server">:</asp:label></TD>
								<TD width="20%"><asp:label id="lblSONumber" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">
									<asp:label id="Label19" runat="server">Nama Dealer</asp:label></TD>
								<TD>:</TD>
								<TD>
									<asp:label id="lblDealerName" runat="server"></asp:label>&nbsp;/
									<asp:label id="lblDealerTerm" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="Label12" runat="server">Nomor DO MKS</asp:label></TD>
								<TD><asp:label id="Label18" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblDONumber" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label2" runat="server">Alamat Dealer</asp:label></TD>
								<TD><asp:label id="Label7" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblDealerAddr" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="Label13" runat="server">LOT</asp:label></TD>
								<TD><asp:label id="Label16" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblLOT" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label4" runat="server">Cara Pembayaran</asp:label></TD>
								<TD><asp:label id="Label9" runat="server">:</asp:label></TD>
								<TD>
                                    <asp:Label ID="lblCaraPembayaran" runat="server"></asp:Label>
                                    <asp:label id="lblSO" runat="server"></asp:label>
								</TD>
								<TD class="titleField"><asp:label id="Label17" runat="server" Width="191px">Material Packing</asp:label></TD>
								<TD><asp:label id="Label15" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblMaterialPacking" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp;</TD>
								<TD>&nbsp;</TD>
								<TD>&nbsp;</TD>
								<TD><asp:label id="Label5" runat="server" style="font-weight: 700">Total Item</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblTotalItem" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp;</TD>
								<TD>&nbsp;</TD>
								<TD>&nbsp;</TD>
								<TD><asp:label id="Label10" runat="server" style="font-weight: 700">Total Qty</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblQty" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp;</TD>
								<TD>&nbsp;</TD>
								<TD>&nbsp;</TD>
								<TD><asp:label id="Label20" runat="server" style="font-weight: 700">Total Berat (Kg)</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblWeight" runat="server"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD align=center> <div id=div1 style="OVERFLOW: auto; HEIGHT: 250px">
                        <asp:datagrid id="dgSPDOPackingDetail" runat="server" Width="100%" AutoGenerateColumns="False" BorderWidth="0px"
							CellSpacing="1" CellPadding="3" PageSize="1000" BackColor="Gainsboro" BorderColor="Gainsboro"
                            AllowSorting="True">
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
								<asp:BoundColumn HeaderText="Nomor Barang" SortExpression="PartNumber">
									<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Nama Barang" SortExpression="PartName">
									<HeaderStyle Width="23%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Qty" HeaderText="Qty" DataFormatString="{0:#,##0}" SortExpression="OrderQty">
									<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" ></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="UoM" HeaderText="Unit" >
									<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" ></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<%--<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>--%>
						</asp:datagrid></div>


						<asp:Button ID="btnDownload" runat="server" Text="Download" />
						<INPUT style="WIDTH: 64px; HEIGHT: 24px" onclick="window.close()" type="button" value="Tutup">
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
