<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpHistorySPAFStatus.aspx.vb" Inherits="PopUpHistorySPAFStatus" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpHistorySPAFStatus</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<base target="_self">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tr>
					<TD class="titleField" width="22%">
						Kode Leasing
					</TD>
					<td width="1%">:</td>
					<td width="35%">
						<asp:Label ID="lblDealerCode" Runat="server"></asp:Label>
					</td>
					<td></td>
				</tr>
				<tr>
					<TD class="titleField">
						Kode Transaksi
					</TD>
					<td width="1%">:</td>
					<td>
						<asp:Label ID="lblDocType" Runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<td colspan="4">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 240px" DESIGNTIMEDRAGDROP="1988">
							<asp:datagrid id="dtgSPAF" runat="server" Width="100%" AllowPaging="True" PageSize="50" AllowCustomPaging="True"
								AllowSorting="True" CellPadding="3" BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro"
								BackColor="#CDCDCD" AutoGenerateColumns="False">
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="OldStatus" HeaderText="Status Lama">
										<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblStatusLama" Text='<%# DataBinder.Eval(Container, "DataItem.OldStatus") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="StatusDesc" HeaderText="Status Baru">
										<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblStatusBaru" Text='<%# DataBinder.Eval(Container, "DataItem.StatusDesc") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ProcessDate" SortExpression="ProcessDate" HeaderText="Diproses Tanggal"
										DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ProcessBy" SortExpression="ProcessBy" HeaderText="Diproses Oleh">
										<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
						</div>
					</td>
				</tr>
				<tr>
					<td colspan="4" style="PADDING-TOP: 10px" align="center">
						<asp:Button id="btnClose" runat="server" Text="Close"></asp:Button>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
