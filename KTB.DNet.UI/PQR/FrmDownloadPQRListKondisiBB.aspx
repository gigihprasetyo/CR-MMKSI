<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDownloadPQRListKondisiBB.aspx.vb" Inherits="FrmDownloadPQRListKondisiBB" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Download Data Status Siswa</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 11pt; MARGIN: 0px; COLOR: #990000; FONT-FAMILY: Sans-Serif, Arial">PQR 
						- Daftar Kondisi</TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD vAlign="top"><asp:datagrid id="dgListPQR" runat="server" CellSpacing="1" ForeColor="Gray" BorderColor="#CDCDCD"
							BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal" AutoGenerateColumns="False"
							Width="100%" DataKeyField="ID" AllowPaging="false" PageSize="50" AllowCustomPaging="True" AllowSorting="True">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Status" SortExpression="RowStatus">
									<HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblStatus" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
									<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") + "-" + DataBinder.Eval(Container, "DataItem.Dealer.DealerName")  %>' ID="lblDealer" NAME="lblDealer">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="true" DataField="PQRNo" HeaderText="PQR No" SortExpression="PQRNo">
									<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="ChassisMasterBB.ChassisNumber" HeaderText="No Rangka">
									<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMasterBB.ChassisNumber") %>' ID="lblNoChassis" NAME="lblNoChassis">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="true" DataField="ValidationTime" HeaderText="Tgl Validasi" SortExpression="ValidationTime">
									<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="true" DataField="RealeseTime" HeaderText="Tgl Rilis" SortExpression="RealeseTime">
									<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="true" DataField="Subject" HeaderText="Subject" SortExpression="Subject">
									<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="true" DataField="ConfirmBy" HeaderText="Proses Oleh" SortExpression="ConfirmBy">
									<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="Bobot" HeaderText="Bobot">
									<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Bobot") %>' ID="lblBobot" NAME="lblBobot">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="IntervalProcess" HeaderText="Interval">
									<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblInterval" NAME="lblInterval"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
