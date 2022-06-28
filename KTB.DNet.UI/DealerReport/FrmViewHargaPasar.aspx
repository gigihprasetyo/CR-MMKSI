<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmViewHargaPasar.aspx.vb" Inherits="FrmViewHargaPasar" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmViewHargaPasar</title>
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
					<TD class="titlePage">DEALER REPORT - Harga Pasar
					</TD>
				</TR>
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
								<TD class="titleField">Kelas</TD>
								<TD>:</TD>
								<TD class="titleField"><asp:dropdownlist id="ddlClass" runat="server"></asp:dropdownlist></TD>
								<TD class="titleField">Area</TD>
								<TD>:</TD>
								<TD class="titleField"><asp:dropdownlist id="ddlArea" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField">Merk</TD>
								<TD>:</TD>
								<TD class="titleField"><asp:dropdownlist id="ddlMerk" runat="server"></asp:dropdownlist></TD>
								<TD class="titleField">Harga Berlaku</TD>
								<TD>:</TD>
								<TD class="titleField"><table border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td><cc1:inticalendar id="icDateFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>&nbsp;s.d&nbsp;</td>
											<td>
												<cc1:inticalendar id="icDateUntil" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD class="titleField"></TD>
								<TD class="titleField"></TD>
								<TD class="titleField"></TD>
								<TD class="titleField"><asp:button id="btnSearch" runat="server" Width="64px" Text="Cari"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><div id="div1" style="OVERFLOW: auto; HEIGHT: 340px"><asp:datagrid id="dgCompetitor" runat="server" AutoGenerateColumns="False" CellSpacing="1" CellPadding="3"
								BorderColor="Gainsboro" BackColor="#CDCDCD" BorderWidth="0px" Width="100%" AllowCustomPaging="True" AllowSorting="True" AllowPaging="True"
								PageSize="20">
								<AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:BoundColumn ReadOnly="True" HeaderText="No">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="CompetitorType.CompetitorBrand.Description" HeaderText="Merk">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblMerk" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CompetitorType.CompetitorBrand.Description") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CompetitorType.VehicleClass.Description" HeaderText="Kelas">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblClass" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CompetitorType.VehicleClass.Description") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CompetitorType.Description" HeaderText="Tipe">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CompetitorType.Description") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ValidDate" HeaderText="Tanggal">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDate" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.ValidDate"),"dd/MM/yyyy") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.Area1.Description" HeaderText="Area">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblArea" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.Area1.Description") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Pengirim">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblDealer runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Off The Road (Rp)">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblOffTheRoad" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="BBN" SortExpression="BBN" HeaderText="BBN (Rp)" DataFormatString="{0:#,###}">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="OnTheRoadPrice" SortExpression="OnTheRoadPrice" HeaderText="On The Road (Rp)"
										DataFormatString="{0:#,###}">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:Button id="btnDownload" Visible="False" runat="server" Text="Download"></asp:Button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
