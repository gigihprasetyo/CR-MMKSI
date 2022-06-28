<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpDailyFlowPOPK.aspx.vb" Inherits="PopUpDailyFlowPOPK" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpDailyFlowPOPK</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PEMESANAN - Pesanan</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>
								<td class="titleField" width="24%">Nomor Reg PK</td>
								<td width="1%">:</td>
								<TD width="75%"><asp:label id="lblPKNumber" runat="server"></asp:label></TD>
							</tr>
							<TR>
								<TD class="titleField">Nomor O/C</TD>
								<td width="1%">:</td>
								<TD><asp:label id="lblContractNumber" runat="server"></asp:label></TD>
							</TR>
						</TABLE>
						<TABLE id="Table21" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>
								<td class="titleField">Alur PK/PO Harian</td>
							</tr>
						</TABLE>
						<asp:datagrid id="dgPO" runat="server" Width="100%" AutoGenerateColumns="False" ShowFooter="True"
							CellPadding="3" CellSpacing="1" BackColor="#CDCDCD" BorderColor="Gainsboro" BorderWidth="0px"
							AllowSorting="True" EnableViewState="False">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:BoundColumn ReadOnly="True" HeaderText="No">
									<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PONumber" SortExpression="PONumber" HeaderText="Nomor PO">
									<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="70%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:datagrid id="dgChasisMaster" runat="server" AllowSorting="True" BorderWidth="0px" BorderColor="Gainsboro"
											BackColor="#CDCDCD" CellSpacing="1" CellPadding="3" ShowFooter="True" AutoGenerateColumns="False"
											Width="100%" EnableViewState="False">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:BoundColumn DataField="DONumber" HeaderText="Nomor DO">
													<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="SONumber" HeaderText="Nomor SO">
													<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
											</Columns>
										</asp:datagrid>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 24px" align=center><INPUT id="btnBack" onclick="window.close()" type="button" value="Tutup">
					</TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
