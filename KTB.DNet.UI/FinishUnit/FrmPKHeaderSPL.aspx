<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPKHeaderSPL.aspx.vb" Inherits="FrmPKHeaderSPL" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPKHeaderSPL</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%">
				<tr>
					<td>
						<asp:DataGrid ID="dg_PKDetail" Runat="server" CellPadding="3" BorderWidth="1px"
							BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False" BackColor="#E0E0E0" AllowSorting="True"
							Width="100%">
							<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle VerticalAlign="Top"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="lblNo" runat="server" text= '<%# container.itemindex+1 %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="PKNumber" HeaderText="Nomor PK">
									<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="lblPKNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PKHeader.PKNumber" )  %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="PKHeader.Dealer.DealerCode" HeaderText="Kode Dealer">
									<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PKHeader.Dealer.DealerCode" )  %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="PKHeader.Dealer.SearchTerm1" HeaderText="TermCari 1">
									<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PKHeader.Dealer.SearchTerm1" )  %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="ResponseQty" HeaderText="Quantity">
									<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ResponseQty" )  %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
						<br><div align="center"><asp:Button id="Button1" runat="server" Text="Tutup"></asp:Button></div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
