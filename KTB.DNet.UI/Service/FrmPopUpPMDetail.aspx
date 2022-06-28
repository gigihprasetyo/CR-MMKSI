<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPopUpPMDetail.aspx.vb" Inherits="FrmPopUpPMDetail" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PM Detail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PM Detail</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="0" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD style="HEIGHT: 40px" vAlign="top" align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%"><asp:label id="lblDealer" runat="server">Dealer </asp:label></TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:label id="lblDealerVal" runat="server"></asp:label></td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 19px">No Chassis</TD>
								<TD style="HEIGHT: 19px">:</TD>
								<td style="HEIGHT: 19px"><asp:label id="lblChassisNoVal" runat="server"></asp:label></td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 19px">Stand KM</TD>
								<TD style="HEIGHT: 19px">:</TD>
								<td style="HEIGHT: 19px"><asp:label id="lblStandKMVal" runat="server"></asp:label></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="left"></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto">
							<asp:datagrid id="dgPMDetail" runat="server" Width="100%" CellSpacing="1" PageSize="50" GridLines="Vertical"
								CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="Gainsboro"
								AutoGenerateColumns="False">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Part">
										<HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblReplacementPartCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReplecementPartMaster.Code") %>' Width="53px">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Description ">
										<HeaderStyle Width="60%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblReplacementPartDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReplecementPartMaster.PartDescription") %>' >
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Left"></FooterStyle>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD>
						<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel">
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
