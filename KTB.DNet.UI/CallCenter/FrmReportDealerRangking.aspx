<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" EnableViewState="True" Codebehind="FrmReportDealerRangking.aspx.vb" Inherits="FrmReportDealerRangking" smartNavigation="False" %>
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
		<script language="javascript">
			function UpdateAfterDownload(btnUpdateAfter, fullPath){
				DownloadCcReport(fullPath)
				btnUpdateAfter.click();
			}
			function DownloadCcReport(fullPath)
			{
				window.open("../PopUp/PopUpDownloadCcReport.aspx?file=" + fullPath, "","top=0,location=0,status=0, scrollbars=0,width=1px,height=1px");
				//document.getElementById("fraDownload").src="../PopUp/PopUpDownloadCcReport.aspx?file=" + fullPath;
			}

			function DownloadCcReportXLS(fullPath) {
			    window.open("../PopUp/PopUpDownloadCcReport.aspx?file=" + fullPath, "", "top=0,location=0,status=0, scrollbars=0,width=1px,height=1px");
			    //document.getElementById("fraDownload").src="../PopUp/PopUpDownloadCcReport.aspx?file=" + fullPath;
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
			function RefreshGrid(a)
			{	
				var btnRefreshGrid = document.getElementById("btnRefreshGrid");
				btnRefreshGrid.click();				
				//window.location="FrmReportDealerRangking.aspx?AutoRefresh=1";
			}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="FrmReportDealerRanking" method="post" runat="server">
			<iframe id="fraDownload" style="DISPLAY: none" src="" width="0" height="0" runat="server">
			</iframe>
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Download Rangking Dealer</td>
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
							<TBODY>
								<TR>
									<TD class="titleField">Report</TD>
									<TD width="1%">:</TD>
									<TD width="80%"><asp:dropdownlist id="ddlReport" runat="server" Width="230px">
											<asp:ListItem Value="0" Selected="True">Rangking Pelayanan Dealer</asp:ListItem>
											<asp:ListItem Value="1">Customer Voice</asp:ListItem>
										</asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="titleField">Periode</TD>
									<TD width="1%">:</TD>
									<TD width="80%"><asp:dropdownlist id="ddlPeriod" runat="server" AutoPostBack="True" Width="115px"></asp:dropdownlist></TD>
								</TR>
								<TR vAlign="top">
									<TD class="titleField">Bulan / Tahun</TD>
									<TD width="1%">:</TD>
									<td>
										<asp:dropdownlist id="ddlMonth" runat="server" Width="115px"></asp:dropdownlist>&nbsp;&nbsp;
										<asp:dropdownlist id="ddlYear" runat="server" Width="80px"></asp:dropdownlist>&nbsp;&nbsp; 
										s.d. &nbsp;&nbsp;
										<asp:dropdownlist id="ddlMonth2" runat="server" Width="115px"></asp:dropdownlist>&nbsp;&nbsp;
										<asp:dropdownlist id="ddlYear2" runat="server" Width="80px"></asp:dropdownlist>
									</td>
								</TR>
								<TR>
									<TD class="titleField">Kategori Pelayanan</TD>
									<TD width="1%">:</TD>
									<td width="60%"><asp:dropdownlist id="ddlServiceType" runat="server" Width="230px"></asp:dropdownlist></td>
								</TR>
								<TR>
									<TD class="titleField" style="HEIGHT: 18px">Jenis Kendaraan</TD>
									<TD width="1%" style="HEIGHT: 18px">:</TD>
									<td width="60%" style="HEIGHT: 18px"><asp:dropdownlist id="ddlVehicle" runat="server" Width="230px"></asp:dropdownlist></td>
								</TR>
								<TR>
									<TD class="titleField">Group Dealer</TD>
									<TD width="1%">:</TD>
									<td width="60%"><asp:dropdownlist id="ddlDealer" runat="server" Width="230px"></asp:dropdownlist></td>
								</TR>
								<TR>
									<TD class="titleField">Status Dealer</TD>
									<TD width="1%">:</TD>
									<td width="60%"><asp:dropdownlist id="ddlStatusDealer" runat="server" Width="230px"></asp:dropdownlist></td>
								</TR>
								<TR>
									<TD class="titleField"></TD>
									<TD width="1%"></TD>
									<td width="60%"><asp:button id="btnShow" runat="server" width="70px" Text="Tampilkan"></asp:button><asp:button id="btnDownload" runat="server" Text="Download" width="70px" Visible="False"></asp:button>&nbsp;</td>
								</TR>
							</TBODY>
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
										<HeaderStyle Width="20%" CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CcReportMaster.RptDesc") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Group Dealer">
										<HeaderStyle Width="23%" CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerGroup" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PeriodType" HeaderText="Periode" ItemStyle-HorizontalAlign="Center">
										<HeaderStyle Width="18%" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPeriodType" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Period" HeaderText="Periode" ItemStyle-HorizontalAlign="Center">
										<HeaderStyle Width="10%" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPeriod" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CcCustomerCategory.Description" HeaderText="Kategori Pelayanan">
										<HeaderStyle Width="15%" CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblDescription Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CcCustomerCategory.Description") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CcVehicleCategory.Code" HeaderText="Jenis Kendaraan">
										<HeaderStyle Width="10%" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="Label1" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CcVehicleCategory.Code") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DealerType" HeaderText="Status Dealer">
										<HeaderStyle Width="10%" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStatusDealer" Runat="server" ></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="File" Visible="False">
										<HeaderStyle Width="2%" CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblFileName" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PdfFileName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DownloadStatus" HeaderText="Status Download" Visible="True">
										<HeaderStyle Width="10%" CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDownload" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="PDF">
										<HeaderStyle Width="8%" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDownload" runat="server" Text="Download" style="display:none" CommandName="Download"
												CausesValidation="False">
												<img src="../images/simpan.gif" border="0" alt="Download"></asp:LinkButton>
											<asp:Label Runat="server" text="" ID="lblShowDownload" style="cursor:hand">
												<img src="../images/download.gif" border="0" alt="Download">
											</asp:Label>
											<div style="display:none;">
												<asp:Button Runat="server" ID="btn" Text="" CommandName="Download"></asp:Button>
												<asp:Button Runat="server" ID="btnUpdateAfter" Text="" CommandName="UpdateAfterDownload"></asp:Button>
											</div>
										</ItemTemplate>
									</asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="XLS">
										<HeaderStyle Width="8%" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDownloadXLS" runat="server" Text="Download" style="display:none" CommandName="DownloadXLS"
												CausesValidation="False">
												<img src="../images/simpan.gif" border="0" alt="Download"></asp:LinkButton>
											<asp:Label Runat="server" text="" ID="lblShowDownloadXLS" style="cursor:hand">
												<img src="../images/download.gif" border="0" alt="Download">
											</asp:Label>
											<div style="display:none;">
												<asp:Button Runat="server" ID="btnXLS" Text="" CommandName="DownloadXLS"></asp:Button>
												<asp:Button Runat="server" ID="btnUpdateAfterXLS" Text="" CommandName="UpdateAfterDownload"></asp:Button>
											</div>
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
