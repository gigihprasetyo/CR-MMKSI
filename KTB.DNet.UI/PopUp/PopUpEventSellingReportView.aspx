<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpEventSellingReportView.aspx.vb" Inherits="PopUpEventSellingReportView" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpEventSellingReportView</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<base target="_self">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" cellSpacing="0"
				cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">EVENT&nbsp;-&nbsp;Detail&nbsp;Laporan&nbsp;Penjualan</TD>
				</TR>
				<TR>
					<TD class="titlePage"></TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center">
						<asp:datagrid id="dtgEvent" runat="server" Width="100%" AutoGenerateColumns="False" AllowSorting="True"
							AllowPaging="True" AllowCustomPaging="True" PageSize="20">
							<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle ForeColor="White"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn SortExpression="VechileType.VechileModel.Category.Description" HeaderText="Kategori">
									<HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id=Label12 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.VechileModel.Category.Description") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="VechileType.Description" HeaderText="Tipe Kendaraan">
									<HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="Label2" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Description") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Jumlah" SortExpression="Jumlah" HeaderText="Jumlah" DataFormatString="{0:N0}">
									<HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</TD>
				</TR>
				<tr>
					<td align="center"><INPUT id="btnTutup" style="WIDTH: 77px; HEIGHT: 24px" onclick="window.close()" type="button"
							value="Tutup">
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
