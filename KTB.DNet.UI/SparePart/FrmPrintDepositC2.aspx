<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPrintDepositC2.aspx.vb" Inherits="FrmPrintDepositC2" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ListContract</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="600" border="0">
				<tr>
					<td class="titlePage">DEPOSIT&nbsp;- Daftar Deposit C2</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="4" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 13px" width="24%"><asp:label id="lblCode" runat="server" Font-Bold="True">Kode Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="lblColon1" runat="server">:</asp:label></TD>
								<TD width="219" style="WIDTH: 219px"><asp:label id="lblDealerCode" runat="server" Width="200px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblName" runat="server" Font-Bold="True">Nama Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="lblColon2" runat="server">:</asp:label></TD>
								<TD colspan="4"><asp:label id="lblDealerName" runat="server" Width="200px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px"></TD>
								<TD></TD>
								<TD class="titleField" style="WIDTH: 52px; HEIGHT: 10px"></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblDocDate" runat="server" Font-Bold="True">Tgl Dokumen</asp:label></TD>
								<TD>:</TD>
								<TD style="WIDTH: 220px">
									<asp:label id="lblRangeDate" runat="server" Width="320px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 2px"><asp:label id="lblTotDepC2" runat="server" Font-Bold="True">Total Deposit C2 (Rp)</asp:label></TD>
								<TD>:</TD>
								<TD style="WIDTH: 220px"><asp:label id="lblTotDepositC2" runat="server"></asp:label></TD>
							<TR>
								<TD colSpan="6"></TD>
							<TR>
								<TD colSpan="6">
									<asp:label id="lblNotes" runat="server" Font-Bold="True">* Nilai ini tidak termasuk bunga</asp:label></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
										<asp:datagrid id="dgDepositC2List" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="#CDCDCD"
											BorderColor="#CDCDCD" BorderWidth="1px" CellPadding="3" AllowSorting="True" PageSize="25">
											<AlternatingItemStyle BackColor="#EFEFEF"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
													<HeaderStyle Width="4%" CssClass="titleTablePrint"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle ForeColor="Black" Width="10%" CssClass="titleTablePrint"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" ForeColor="Black" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblNo" runat="server" Text=""></asp:Label>
													</ItemTemplate>
													<FooterStyle ForeColor="Black"></FooterStyle>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="DocDateText" SortExpression="DocDateText" ReadOnly="True" HeaderText="Tgl Dokumen">
													<HeaderStyle ForeColor="Black" Width="30%" CssClass="titleTablePrint"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" ForeColor="Black" VerticalAlign="Top"></ItemStyle>
													<FooterStyle ForeColor="Black"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="DocumentNo" SortExpression="DocumentNo" ReadOnly="True" HeaderText="No Dokumen">
													<HeaderStyle ForeColor="Black" Width="30%" CssClass="titleTablePrint"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" ForeColor="Black" VerticalAlign="Top"></ItemStyle>
													<FooterStyle ForeColor="Black"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="DepositC2Amnt" SortExpression="DepositC2Amnt" ReadOnly="True" HeaderText="Deposit C2 (Rp) *"
													DataFormatString="{0:#,##0}">
													<HeaderStyle ForeColor="Black" Width="30%" CssClass="titleTablePrint"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" ForeColor="Black" VerticalAlign="Top"></ItemStyle>
													<FooterStyle ForeColor="Black"></FooterStyle>
												</asp:BoundColumn>
											</Columns>
										</asp:datagrid>
								</TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6" align="center">
									<INPUT class="hideButtonOnPrint" id="btnPrint" style="WIDTH: 74px; HEIGHT: 21px" onclick="window.print()"
										type="button" value="Cetak" name="btnPrint" runat="server"> <INPUT class="hideButtonOnPrint" id="btnClose" style="WIDTH: 74px; HEIGHT: 21px" onclick="window.close()"
										type="button" value="Tutup" name="btnClose" runat="server">
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
