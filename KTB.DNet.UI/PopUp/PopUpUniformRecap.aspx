<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpUniformRecap.aspx.vb" Inherits="PopUpUniformRecap" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpUniformRecap</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<base target="_self">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">
						Salesman Uniform&nbsp;- Rekap</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>
								<td>
									<asp:Label id="Label1" runat="server">Grand Total : Rp</asp:Label>
									<asp:Label id="lblGrandTotal" runat="server"></asp:Label>
								</td>
							</tr>
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 330px">
										<asp:datagrid id="dtgUniform" runat="server" Width="100%" PageSize="25" BorderColor="Gainsboro"
											CellSpacing="1" BorderWidth="0px" CellPadding="3" BackColor="#CDCDCD" AutoGenerateColumns="False">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblNo" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Kode Pesanan">
													<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblSalesmanUnifDistributionCode" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Keterangan">
													<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblDescription" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="SRecap" HeaderText="S">
													<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="MRecap" HeaderText="M">
													<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="LRecap" HeaderText="L">
													<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="XLRecap" HeaderText="XL">
													<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="XXLRecap" HeaderText="XXL">
													<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Harga Satuan">
													<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblUnitPrice" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Harga">
													<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblPrice" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<tr>
								<td align="center">
									<INPUT id="btnBack" onclick="window.close();" style="WIDTH: 60px" type="button" value="Kembali"
										name="btnBack">
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 8px"></TD>
				</TR>
			</TABLE>
			<INPUT id="Hidden1" type="hidden" name="Hidden1" runat="server">
		</form>
	</body>
</HTML>
