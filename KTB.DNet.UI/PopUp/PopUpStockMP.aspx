<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpStockMP.aspx.vb" Inherits="PopUpStockMP" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>

	<HEAD>
		<title>PopUpStockMP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<base target="_self">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table border="0" cellspacing="0" cellpadding=0 width="100%">
				<tr>
					<td class="titlePage">Material Promotion - Stock Adjustment</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td><div id="div1" style="OVERFLOW: auto; HEIGHT: 520px"><asp:datagrid id="dtgMPStock" runat="server" AllowPaging="True" AllowCustomPaging="True" ShowFooter="True"
							CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False"
							CellSpacing="1" AllowSorting="True" Width="100%" PageSize="25">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White" VerticalAlign=Top></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="WhiteSmoke" BackColor="OrangeRed"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
								<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Dealer">
								<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:label id="lblKodeDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="AdjustType" HeaderText="Adjustment">
								<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblAdjustment" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AdjustType") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<strong>Stock Akhir</strong>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Qty" HeaderText="Jumlah">
								<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
								<ItemStyle HorizontalAlign=Right></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblJumlah" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Qty"),"#,##0") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label ID="lblStockAkhir" Runat="server"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Keterangan" HeaderText="Keterangan">
								<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblKeterangan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Keterangan") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="LastUpdateTime" HeaderText="Tanggal Update">
								<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
								<ItemStyle HorizontalAlign=Center></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblTglUpdate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LastUpdateTime") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Description" HeaderText="Penjelasan">
								<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages" HorizontalAlign=Right></PagerStyle>
						</asp:datagrid></div></td>
				</tr>
				<tr>
					<td align=center><br><asp:button id="btnClose" Runat="server" Text="Tutup"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
