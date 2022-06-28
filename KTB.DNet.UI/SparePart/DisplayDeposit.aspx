<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DisplayDeposit.aspx.vb" Inherits="DisplayDeposit" smartNavigation="False"%>
<%@ Register TagPrefix="dbwc" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ListContract</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">WSC - Daftar Status WSC</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%"><asp:label id="lblDealerCode" runat="server">Kode Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="lblColon1" runat="server">:</asp:label></TD>
								<TD width="25%"><asp:dropdownlist id="ddlDealerCode" runat="server" Width="140px"></asp:dropdownlist></TD>
								<TD class="titleField" width="20%"><asp:label id="lblClaimNo" runat="server">Nomor Klaim</asp:label></TD>
								<TD width="1%"><asp:label id="lblColon4" runat="server">:</asp:label></TD>
								<TD width="29%"><asp:textbox id="txtClaimNo" runat="server" MaxLength="6" size="22"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblCreatePeriod" runat="server">Periode Create</asp:label></TD>
								<TD><asp:label id="lblColon2" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlCreateMonth" runat="server" Width="87"></asp:dropdownlist>
									<asp:dropdownlist id="ddlCreateYear" runat="server" Width="50px"></asp:dropdownlist></TD>
								<TD class="titleField"><asp:label id="lblVehicleType" runat="server">Tipe Kendaraan</asp:label></TD>
								<TD><asp:label id="lblColon5" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlVehicleType" runat="server" Width="140"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblDecidePeriod" runat="server">Periode Decide</asp:label></TD>
								<TD><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlDecideMonth" runat="server" Width="87"></asp:dropdownlist>
									<asp:dropdownlist id="ddlDecideYear" runat="server" Width="50px"></asp:dropdownlist></TD>
								<TD class="titleField"><asp:label id="lblStatus" runat="server">Status</asp:label></TD>
								<TD><asp:label id="lblColon6" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlStatus" runat="server" Width="140"></asp:dropdownlist></TD>
							</TR>
							<tr>
								<td></td>
								<td></td>
								<td>
									<asp:button id="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:button></td>
							</tr>
							<TR>
								<TD valign="top" colSpan="6"><div id="div1" style="OVERFLOW: auto; HEIGHT: 400px"><asp:datagrid id="dgStatusList" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="#CDCDCD"
											BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px" CellPadding="3">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
													<HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="ClaimStatus" HeaderText="Status">
													<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Alasan">
													<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblReason runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Reason.ReasonCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="NotificationNumber" HeaderText="Notifikasi">
													<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="ClaimType" HeaderText="Jenis WSC">
													<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Nomor WSC">
													<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id=lnkClaimNumber runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClaimNumber") %>' CommandName="lnkClaimNumber">
														</asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Nomor Rangka">
													<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber") %>' ID="Label2">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="ServiceDate" HeaderText="Tgl Servis" DataFormatString="{0:dd/MM/yyyy}">
													<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="DecideDate" HeaderText="Tgl Proses" DataFormatString="{0:dd/MM/yyyy}">
													<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="PartAmount" HeaderText="Material" DataFormatString="{0:#,###}">
													<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="LaborAmount" HeaderText="Ongkos Kerja" DataFormatString="{0:#,###}">
													<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TotalAmount" HeaderText="Total" DataFormatString="{0:#,###}">
													<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Email">
													<HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lnkEmail" runat="server" CommandName="lnkEmail">
															<img src="../images/detail.gif" border="0" style="cursor:hand" alt="Send email">
														</asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
