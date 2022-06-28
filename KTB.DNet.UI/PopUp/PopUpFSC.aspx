<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpFSC.aspx.vb" Inherits="PopUpFSC" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpFSC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="tblFreeService" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="titlePage" width="100%" colSpan="6">:: Detail Data</td>
				</tr>
				<tr>
					<td colspan="6"><table border="0" cellpadding="0" cellspacing="0" width="100%">
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td width="100%" colSpan="6" height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td width="20%"><asp:label id="Label120" runat="server" CssClass="titleField">Chassis Number </asp:label></td>
					<td align="left" width="1%"><asp:label id="Label121" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="20%"><asp:label id="lblChassisNo" runat="server"></asp:label></td>
					<td width="20%"><asp:label id="Label1" runat="server" CssClass="titleField">Engine Number</asp:label></td>
					<td align="left" width="1%"><asp:label id="Label2" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="30%"><asp:label id="lblEngineNo" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td width="20%"><asp:label id="Label3" runat="server" CssClass="titleField">Item Number</asp:label></td>
					<td align="left" width="1%"><asp:label id="Label4" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="20%"><asp:label id="lblItemNo" runat="server"></asp:label></td>
					<td width="20%"><asp:label id="Label6" runat="server" CssClass="titleField">NIK Number</asp:label></td>
					<td align="left" width="1%"><asp:label id="Label7" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="30%"><asp:label id="lblNIKNo" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td width="20%"><asp:label id="Label9" runat="server" CssClass="titleField">End Customer</asp:label></td>
					<td align="left" width="1%"><asp:label id="Label10" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="20%"><asp:label id="lblEndCust" runat="server"></asp:label></td>
					<td width="20%"><asp:label id="Label12" runat="server" CssClass="titleField">Address</asp:label></td>
					<td align="left" width="1%"><asp:label id="Label13" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="30%"><asp:label id="lblAddress" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td width="20%"><asp:label id="Label15" runat="server" CssClass="titleField">CBU Receipt Date</asp:label></td>
					<td align="left" width="1%"><asp:label id="Label16" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="20%"><asp:label id="lblCBUReceiptDate" runat="server"></asp:label></td>
					<td width="20%"><asp:label id="Label18" runat="server" CssClass="titleField">Facture Date</asp:label></td>
					<td align="left" width="1%"><asp:label id="Label19" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="30%"><asp:label id="LblFactureDate" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td width="20%"><asp:label id="Label21" runat="server" CssClass="titleField">Delivery Date</asp:label></td>
					<td align="left" width="1%"><asp:label id="Label22" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="20%"><asp:label id="lblDeliveryDate" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td width="100%" colSpan="6"></td>
				</tr>
				<tr>
					<td width="100%" colSpan="6">
						<div id="divData" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 300px">
							<asp:datagrid id="dtgData" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
								CellPadding="3" CellSpacing="1" AllowSorting="True" AutoGenerateColumns="False" ShowFooter="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle HorizontalAlign="Center" ForeColor="Black" VerticalAlign="Middle" BackColor="#FFFFFF"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" text= '<%# container.itemindex+1 %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Kind" HeaderText="Kind Milleage">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblKind" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Kind") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ServiceDealer" HeaderText="Service Dealer">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblServiceDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDealer") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Service Date">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblServiceDate" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="EntryDate" HeaderText="Entry Date">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblEntryDate" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Amount" HeaderText="Amount">
										<HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblAmount" runat="server" Text='<%# String.Format("{0:n}", Convert.ToInt32(DataBinder.Eval(Container, "DataItem.Amount"))) %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></div>
					</td>
				</tr>
				<tr>
					<td align="center" width="100%" colSpan="6"><asp:button id="btnClose" runat="server" CausesValidation="False" Text="Tutup" Width="56px"></asp:button></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
