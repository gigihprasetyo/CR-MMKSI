<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpDetailInOutMP.aspx.vb" Inherits="PopUpDetailInOutMP" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpDetailInOutMP</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<base target="_self">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%" border=0 cellpadding=0 cellspacing=0>
				<tr>
					<td class="titlePage" style="HEIGHT: 21px">Material Promotion - Detail In-Out</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td><asp:datagrid id="dtgMPStock" runat="server" AllowCustomPaging="True" ShowFooter="True" CellPadding="3"
							BackColor="#CDCDCD" BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False"
							CellSpacing="1" AllowSorting="True" Width="100%">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="WhiteSmoke"></HeaderStyle>
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
								<asp:TemplateColumn HeaderText="Adjustment">
								<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblAdjustment" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AdjustType") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<strong>Stock Akhir</strong>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Jumlah">
								<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblJumlah" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Qty") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label ID="lblStockAkhir" Runat="server"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Keterangan">
								<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblKeterangan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Keterangan") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td align=center><asp:button id="btnClose" Runat="server" Text="Tutup"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
