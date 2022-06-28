<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPrintDeposit.aspx.vb" Inherits="FrmPrintDeposit" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DEPOSIT - Daftar Deposit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 8px">DEPOSIT&nbsp;- Daftar Deposit</td>
				</tr>
				<tr>
					<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 8px" height="8"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%"><asp:label id="lblCode" runat="server" Font-Bold="True">Kode Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="lblColon1" runat="server">:</asp:label></TD>
								<TD width="25%"><asp:label id="lblDealerCode" runat="server" Width="224px"></asp:label></TD>
								<TD class="titleField" width="24%"></TD>
								<TD width="1%"></TD>
								<TD width="25%"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%"><asp:label id="lblName" runat="server" Font-Bold="True">Nama Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="lblColon2" runat="server">:</asp:label></TD>
								<TD width="75%" colspan="4"><asp:label id="lblDealerName" runat="server" Width="224px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 15px"><asp:label id="lblPeriodTitle" runat="server" Font-Bold="True">Sisa Deposit</asp:label></TD>
								<TD style="HEIGHT: 15px"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 15px"><asp:label id="lblPeriod" runat="server" Width="224px"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblBegBalc" runat="server" Font-Bold="True">Saldo Awal Bulan (Rp)</asp:label></TD>
								<TD></TD>
								<TD><asp:label id="lblDr" runat="server" Font-Bold="True">Total Debet (Rp)</asp:label></TD>
								<TD class="titleField"><asp:label id="lblCr" runat="server" Font-Bold="True">Total Kredit (Rp)</asp:label></TD>
								<TD></TD>
								<TD><asp:label id="lblEndBalc" runat="server" Font-Bold="True">Saldo Akhir (Rp)</asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="lblBeginBalance" runat="server"></asp:label></TD>
								<TD></TD>
								<TD><asp:label id="lblDebit" runat="server"></asp:label></TD>
								<TD><asp:label id="lblCredit" runat="server"></asp:label></TD>
								<TD></TD>
								<TD><asp:label id="lblEndingBalance" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label2" runat="server" Font-Bold="True">Available Deposit (Rp)</asp:label></TD>
								<TD></TD>
								<TD><asp:label id="Label3" runat="server" Font-Bold="True">RO (Rp)</asp:label></TD>
								<TD class="titleField"><asp:label id="Label4" runat="server" Font-Bold="True">Giro (Rp)</asp:label></TD>
								<TD></TD>
								<TD><asp:label id="Label5" runat="server" Font-Bold="True">Service (Rp)</asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="lblDepositAwal" runat="server"></asp:label></TD>
								<TD></TD>
								<TD><asp:label id="lblRO" runat="server"></asp:label></TD>
								<TD><asp:label id="lblGiroService" runat="server"></asp:label></TD>
								<TD></TD>
								<TD><asp:label id="lblService" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6"><asp:datagrid id="dgDepositList" runat="server" Width="100%" AllowSorting="True" CellPadding="3"
										BorderWidth="1px" BorderColor="Gray" BackColor="#CDCDCD" AutoGenerateColumns="False" PageSize="25">
										<AlternatingItemStyle BackColor="#EFEFEF"></AlternatingItemStyle>
										<ItemStyle BackColor="White"></ItemStyle>
										<HeaderStyle Font-Bold="True" HorizontalAlign="Center" BackColor="Blue" ForeColor="Black"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="No">
												<HeaderStyle Width="4%" CssClass="titleTablePrint"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:Label id="lblNo" runat="server" Text=""></asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
												<HeaderStyle Width="4%" CssClass="titleTablePrint"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="PostDateText" SortExpression="PostingDate" ReadOnly="True" HeaderText="Tgl Transaksi">
												<HeaderStyle Width="8%" CssClass="titleTablePrint"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Debit" SortExpression="Debit" ReadOnly="True" HeaderText="Debet (Rp)"
												DataFormatString="{0:#,##0}">
												<HeaderStyle Width="10%" CssClass="titleTablePrint"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Credit" SortExpression="Credit" ReadOnly="True" HeaderText="Kredit (Rp)"
												DataFormatString="{0:#,##0}">
												<HeaderStyle Width="10%" CssClass="titleTablePrint"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="DocumentNo" SortExpression="DocumentNo" ReadOnly="True" HeaderText="No Dokumen">
												<HeaderStyle Width="8%" CssClass="titleTablePrint"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="ReferenceNo" SortExpression="ReferenceNo" ReadOnly="True" HeaderText="Referensi">
												<HeaderStyle Width="12%" CssClass="titleTablePrint"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="InvoiceNo" SortExpression="InvoiceNo" ReadOnly="True" HeaderText="No Faktur">
												<HeaderStyle Width="8%" CssClass="titleTablePrint"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Remark" SortExpression="Remark" ReadOnly="True" HeaderText="Keterangan">
												<HeaderStyle Width="22%" CssClass="titleTablePrint"></HeaderStyle>
											</asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
							<TR>
								<TD vAlign="top" align="center" colSpan="6"><INPUT class="hideButtonOnPrint" id="btnPrint" style="WIDTH: 74px; HEIGHT: 21px" onclick="window.print()"
										type="button" value="Cetak" name="btnPrint"> <INPUT class="hideButtonOnPrint" id="btnClose" style="WIDTH: 74px; HEIGHT: 21px" onclick="window.close()"
										type="button" value="Tutup" name="btnClose">
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
