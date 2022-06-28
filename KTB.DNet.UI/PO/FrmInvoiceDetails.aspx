<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmInvoiceDetails.aspx.vb" Inherits="FrmInvoiceDetails" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PODetails</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript">
		//function Back()
		//{
		//window.history.go(-1);
		//}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">INVOICE&nbsp;- Detail Invoice</td>
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
							<TR>
								<TD class="titleField" width="24%">Kode<asp:label id="Label1" runat="server"> Dealer</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:label id="lblDealerCodeValue" runat="server"></asp:label></TD>
								<TD class="titleField" width="20%"><asp:label id="label66" runat="server">Kota</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="29%"><asp:label id="lblCityValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label5" runat="server">Nama Dealer</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblNameValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="Label6" runat="server">Nomor O/C</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblContractNumberValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblNoRegPO" runat="server">No Invoice</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblNoInvoice" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="Label8" runat="server">Jenis O/C</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblJenisMOValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">
									<asp:label id="Label3" runat="server">Tanggal Invoice</asp:label></TD>
								<TD>:</TD>
								<TD>
									<asp:label id="lblTglInvoice" runat="server"></asp:label></TD>
								<TD class="titleField">
									<asp:label id="label" runat="server"> Kategori</asp:label></TD>
								<TD>:</TD>
								<TD>
									<asp:label id="lblSalesOrgValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label7" runat="server"> Nomor PO</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblDailyPONumberValue" runat="server"></asp:label></TD>
								<TD class="titleField">
									<asp:label id="Label11" runat="server">Tahun Perakitan / Impor</asp:label></TD>
								<TD>:</TD>
								<TD>
									<asp:label id="lblProductYearValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblTanggalPengajuan" runat="server">No S/O</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblNoSO" runat="server"></asp:label></TD>
								<TD class="titleField">
									<asp:label id="Label12" runat="server">Nama Pesanan Khusus</asp:label></TD>
								<TD>:</TD>
								<TD>
									<asp:label id="lblProjectNameValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">
									<asp:label id="Label13" runat="server"> Tanggal Pengajuan PO</asp:label></TD>
								<TD>:</TD>
								<TD>
									<asp:label id="lblTglPengajuanPO" runat="server"></asp:label></TD>
								<TD class="titleField">
									<asp:label id="label24" runat="server">Jenis Order</asp:label></TD>
								<TD>:</TD>
								<TD>
									<asp:label id="lblOrderTypeValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label2" runat="server"> Tanggal Permintaan Kirim</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblReqAllocValue" runat="server"></asp:label></TD>
								<TD class="titleField">
									<asp:label id="Total" runat="server">Total Harga Tebus *</asp:label></TD>
								<TD>:</TD>
								<TD class="titleField">
									<asp:label id="Label9" runat="server">Rp</asp:label>&nbsp;
									<asp:label id="lblTotalAmountValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 17px"><asp:label id="Label4" runat="server"> Cara Pembayaran</asp:label></TD>
								<TD style="HEIGHT: 17px">:</TD>
								<TD style="HEIGHT: 17px"><asp:label id="lblTermOfPaymentValue" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 17px"></TD>
								<TD style="HEIGHT: 17px"></TD>
								<TD style="HEIGHT: 17px"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD colSpan="6" style="HEIGHT: 342px"><div id="div1" style="OVERFLOW: auto; HEIGHT: 320px">
							<asp:datagrid id="dtgDetail" runat="server" CellPadding="3" BorderWidth="1px" BorderColor="#CDCDCD"
								BackColor="#CDCDCD" AutoGenerateColumns="False" Width="100%" ShowFooter="True">
								<AlternatingItemStyle BackColor="#EFEFEF"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle"
									BackColor="Blue"></HeaderStyle>
								<FooterStyle BackColor="White"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" HeaderImageUrl="ID" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" NAME="lblNo" text= '<%# container.itemindex+1 %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn HeaderText="Kode Tipe / Warna">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Model / Tipe / Warna">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Qty (unit)">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Harga (Rp)">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="PPH 22 (Rp)">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Interest (Rp)">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Sub Total (Rp)">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></div>
						<DIV>
							<asp:label id="Label10" runat="server">Catatan :</asp:label></DIV>
					</TD>
				</TR>
				<TR>
					<TD colSpan="6">
						<asp:label id="Label14" runat="server">a. Dokumen ini merupakan bagian yang tidak terpisahkan dari perjanjian Jual Beli no 24 Akta Notaris M.M.I Wiardi, SH Tanggal 14 Oktober 1996 termasuk setiap</asp:label></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 23px" colSpan="6">&nbsp;&nbsp;&nbsp;
						<asp:label id="Label15" runat="server">perubahan atau pembaharuannya.</asp:label></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 22px" colSpan="6">
						<asp:label id="Label16" runat="server">b. Dokumen  ini dibuat dalam bentuk dokumen elektronik dan merupakan bukti yang cukup dan sah meskipun tidak ditandatangani oleh PT.Krama Yudha Tiga</asp:label></TD>
				</TR>
				<TR>
					<TD colSpan="6">&nbsp;&nbsp;&nbsp;
						<asp:label id="Label17" runat="server">Berlian Motors</asp:label></TD>
				</TR>
				<TR>
					<TD colSpan="6"></TD>
				</TR>
				<TR>
					<TD><asp:Button ID="btnBack" Runat="server" Text="Kembali"></asp:Button></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
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
