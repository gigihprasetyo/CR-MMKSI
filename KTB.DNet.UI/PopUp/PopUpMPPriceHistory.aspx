<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpMPPriceHistory.aspx.vb" Inherits="PopUpMPPriceHistory"  smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpMPPriceHistory</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">&nbsp;&nbsp;&nbsp; Price History</td>
				</tr>
				<tr>
					<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</TABLE>
			<table cellSpacing="1" cellPadding="2" width="100%">
				<tr>
					<td>
						<div id="div1" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 380px"><asp:DataGrid id="dtgPriceHistory" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD"
								Width="100%" BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0" CellPadding="3" GridLines="Horizontal" CellSpacing="1" PageSize="15" AllowPaging="True"
								AllowSorting="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="WhiteSmoke" BackColor="OrangeRed"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="LastUpdateTime" HeaderText="Update Terakhir">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblUpdateTime" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.LastUpdateTime"),"dd/MM/yyyy")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Price" HeaderText="Harga">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblHarga" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Price"),"#,##0") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Description" HeaderText="Keterangan">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblKeterangan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages" HorizontalAlign=Right></PagerStyle>
							</asp:DataGrid></div>
					</td>
				</tr>
			</table>
			<table width="100%">
				<tr>
					<td align="center"><asp:Button ID="btnTutup" Runat="server" Text="Tutup"></asp:Button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
