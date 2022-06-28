<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopupSalesTrainingParticipant.aspx.vb" Inherits="PopupSalesTrainingParticipant" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAplikasiHeader</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">SALESMAN TRAINING - Daftar Peserta</td>
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
							<TR>
								<TD class="titleField" style="HEIGHT: 17px" width="24%"></TD>
								<TD style="HEIGHT: 17px" width="1%"></TD>
								<TD style="HEIGHT: 17px" width="25%"></TD>
								<TD class="titleField" style="HEIGHT: 17px" width="20%"></TD>
								<TD style="HEIGHT: 17px" width="1%"></TD>
								<TD style="HEIGHT: 17px" width="29%"></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 330px"><asp:datagrid id="dgParticipant" runat="server" Width="100%" AllowPaging="True" PageSize="25"
											AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
											CellPadding="3">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.SalesmanCode" HeaderText="Salesman ID">
													<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblSalesmanID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.SalesmanCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.Dealer.DealerCode" HeaderText="Dealer Code">
													<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblDealerCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.Dealer.DealerCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.Dealer.DealerName" HeaderText="Dealer Name">
													<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblDealerName" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.Dealer.DealerName") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.Dealer.City.CityName" HeaderText="Kota">
													<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblKota" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.Dealer.City.CityName") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.Name" HeaderText="Nama">
													<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblSalesmanName" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.Name") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
									</td>
								</tr>
								<tr>
								<td colspan=7 align=center>
									<asp:button id="btnClose" runat="server" Text="Tutup" Width="60px"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 8px"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
