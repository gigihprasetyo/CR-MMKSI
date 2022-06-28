<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpEventProposalHistoryView.aspx.vb" Inherits="PopUpEventProposalHistoryView" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpEventProposalHistoryView</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<base target="_self">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tr>
					<td colspan="4">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 240px" DESIGNTIMEDRAGDROP="1988">
							<asp:datagrid id="dtgHistory" runat="server" Width="100%" AllowPaging="True" PageSize="50" AllowCustomPaging="True"
								AllowSorting="True" CellPadding="3" BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro"
								BackColor="#CDCDCD" AutoGenerateColumns="False">
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="EventProposal.Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblDealerCode" Text='<%# DataBinder.Eval(Container, "DataItem.EventProposal.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="EventProposal.Dealer.DealerName" HeaderText="Nama Dealer">
										<HeaderStyle ForeColor="White" Width="17%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblDealerName" Text='<%# DataBinder.Eval(Container, "DataItem.EventProposal.Dealer.DealerName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ActivityPlaceOld" SortExpression="ActivityPlaceOld" HeaderText="Tempat Keg. Lama">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="ActivityPlaceOld" HeaderText="Jadwal Lama">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# Iif(DataBinder.Eval(Container, "DataItem.ActivityScheduleOld", "{0:dd/MM/yyyy}") = "01/01/1753", "" , DataBinder.Eval(Container, "DataItem.ActivityScheduleOld", "{0:dd/MM/yyyy}")) %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ActivityPlaceNew" SortExpression="ActivityPlaceNew" HeaderText="Tempat Keg. Terbaru">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ActivityScheduleNew" SortExpression="ActivityScheduleNew" HeaderText="Jadwal Terbaru"
										DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="UpdateBy" SortExpression="UpdateBy" HeaderText="Update by">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
							<asp:datagrid id="dtgHistoryAgreement" runat="server" Width="100%" AllowPaging="True" PageSize="50"
								AllowCustomPaging="True" AllowSorting="True" CellPadding="3" BorderWidth="0px" CellSpacing="1"
								BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False">
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="EventProposal.Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="Label1" Text='<%# DataBinder.Eval(Container, "DataItem.EventProposal.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="EventProposal.Dealer.DealerName" HeaderText="Nama Dealer">
										<HeaderStyle ForeColor="White" Width="17%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="Label2" Text='<%# DataBinder.Eval(Container, "DataItem.EventProposal.Dealer.DealerName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="EventName" SortExpression="EventName" HeaderText="Nama Kegiatan">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ActivitySchedule" SortExpression="ActivitySchedule" 
										HeaderText="Tanggal Acara" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ProposedCost" SortExpression="ProposedCost" 
										HeaderText="Biaya Diajukan" DataFormatString="{0:#,##0}">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ApprovedCost" SortExpression="ApprovedCost" 
										HeaderText="Biaya Disetujui" DataFormatString="{0:#,##0}">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="UpdateBy" SortExpression="UpdateBy" HeaderText="Update by">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
						</DIV>
					</td>
				</tr>
				<tr>
					<td colspan="4" style="PADDING-TOP: 10px" align="center">
						<input type="button" onclick="javascript:window.close();" value="Tutup">
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
