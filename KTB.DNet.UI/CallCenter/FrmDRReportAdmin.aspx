<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" EnableViewState="True" Codebehind="FrmDRReportAdmin.aspx.vb" Inherits="FrmDRReportAdmin" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Maintenance Tipe Kompetitor</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
		
			function DownloadCcReport(fullPath)
			{
				//window.open("../PopUp/PopUpDownloadCcReport.aspx?file=" + fullPath, "","top=0,location=0,status=0, scrollbars=0,width=1px,height=1px");
				document.getElementById("fraDownload").src="../PopUp/PopUpDownloadCcReport.aspx?file=" + fullPath;
			}
			
			/* ini untuk handle char yg tdk diperbolehkan, saat paste */
			function TxtBlur(objTxt)
			{
				omitSomeCharacter(objTxt,'<>?*%$;');
			}
			/* ini untuk handle char yg tdk diperbolehkan, saat keypress */
			function TxtKeypress()
			{
				return alphaNumericExcept(event,'<>?*%$;')
			}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="FrmReportDealerRanking" method="post" runat="server">
			<iframe id="fraDownload" style="DISPLAY: none" src="" width="0" height="0" runat="server">
			</iframe>
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">REPORT - Mekanisme Open Close</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField">Report</TD>
								<TD width="1%">:</TD>
								<TD width="80%"><asp:dropdownlist id="ddlReport" runat="server" Width="230px">
										<asp:ListItem Value="0" Selected="True">Rangking Pelayanan Dealer</asp:ListItem>
										<asp:ListItem Value="1">Customer Voice</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField">Periode</TD>
								<TD width="1%">:</TD>
								<td width="60%">
									<asp:dropdownlist id="ddlMonth" runat="server" Width="115px"></asp:dropdownlist><asp:dropdownlist id="ddlYear1" runat="server" Width="80px"></asp:dropdownlist>&nbsp;&nbsp; 
									s.d. &nbsp;&nbsp;
									<asp:dropdownlist id="ddlMonth2" runat="server" Width="115px"></asp:dropdownlist><asp:dropdownlist id="ddlYear2" runat="server" Width="80px"></asp:dropdownlist>
								</td>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD width="1%"></TD>
								<td width="60%">
									<asp:button id="btnSave" runat="server" width="70px" Text="Simpan"></asp:button>&nbsp;
									<asp:button id="btnCancel" runat="server" width="70px" Text="Batal"></asp:button>&nbsp;
									<asp:button id="btnShow" runat="server" width="70px" Text="Cari" Visible="False"></asp:button>&nbsp;
								</td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px">
							<asp:datagrid id="dgReports" runat="server" Width="100%" PageSize="25" CellPadding="3" BackColor="#CDCDCD"
								AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False"
								CellSpacing="1" AllowSorting="True">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="2%" CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CcReportMaster.RptDesc" HeaderText="Report">
										<HeaderStyle Width="50%" CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CcReportMaster.RptDesc") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PeriodFrom" HeaderText="Bulan" ItemStyle-HorizontalAlign="Center">
										<HeaderStyle Width="26%" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPeriod" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ReportStatus" HeaderText="Status" ItemStyle-HorizontalAlign="Center">
										<HeaderStyle Width="12%" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Aksi">
										<HeaderStyle Width="10%" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" CausesValidation="False" Runat="server" text="Ubah" CommandName="Edit"
												ToolTip="Ubah">
												<img border="0" src="../images/edit.gif" alt="Ubah" style="cursor:hand"></asp:LinkButton>
											<asp:LinkButton id="lbtnOpen" CausesValidation="False" Runat="server" text="Aktif" CommandName="Open"
												ToolTip="Open">
												<img border="0" src="../images/aktif.gif" alt="Open" style="cursor:hand"></asp:LinkButton>
											<asp:LinkButton id="lbtnClosed" CausesValidation="False" Runat="server" text="Non-aktif" CommandName="Closed"
												ToolTip="Closed">
												<img border="0" src="../images/in-aktif.gif" alt="Closed" style="cursor:hand"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" CausesValidation="False" Runat="server" text="Hapus" CommandName="Delete"
												Visible="False" ToolTip="Hapus">
												<img border="0" src="../images/trash.gif" alt="Hapus" style="cursor:hand"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
