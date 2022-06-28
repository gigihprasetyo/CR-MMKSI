<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpStockDealerDetail.aspx.vb" Inherits="PopUpStockDealerDetail" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>List Stock Dealer</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 146px">
									Dealer</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField">
									<asp:Label id="lblDealer" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">
									Stok Periode</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField">
									<asp:Label id="lblStockPeriode" runat="server"></asp:Label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Tipe/Warna</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField">
									<asp:Label id="lblColor" runat="server"></asp:Label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:datagrid id="dgDetails" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="1"
							CellPadding="3" BorderColor="Gainsboro" BackColor="#CDCDCD" BorderWidth="0px">
							<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
							<Columns>
								<asp:BoundColumn ReadOnly="True" HeaderText="No">
									<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No Rangka">
									<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblChassisNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="No Mesin">
									<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblEngineNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.EngineNumber") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="No Seri">
									<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblSeri" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.SerialNumber") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
				<TR>
					<TD><INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
