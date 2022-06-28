<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanPartPerformanceList.aspx.vb" Inherits="FrmSalesmanPartPerformanceList" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSalesmanList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function ShowPPDealerSelection()
		{
			showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
				function DealerSelection(selectedDealer)
		{
			var txtDealer = document.getElementById("txtDealerCode");
			txtDealer.value = selectedDealer;				
		}
		
		function TxtBlur(objTxt)
		{
			omitSomeCharacter(objTxt,'<>?*%$;');
		}
		/* ini untuk handle char yg tdk diperbolehkan, saat keypress */
		function TxtKeypress()
		{
			return alphaNumericExcept(event,'<>?*%$;')
		}
			
		function ShowSalesmanSelection()
		{	
			var lblSalesmanCode = document.getElementById("lblShowSalesman");
			//showPopUp('../PopUp/PopUpSalesmanPart.aspx?SSCode=' + lblSalesmanCode.innerText,'',600,600,SalesmanSelection);
			showPopUp('../PopUp/PopUpSalesmanPart.aspx?IsGroupDealer=0&IsSales=1','',470,600,SalesmanSelection);
		}
			
		function SalesmanSelection(SelectedSalesman)
		{
			var tempParam = SelectedSalesman.split(';');
			var txtSalesmanCode = document.getElementById("txtSalesmanCode");
			txtSalesmanCode.value = tempParam[0]
		}
		
		function ShowPopUpHistory(id)
		{
			showPopUp('../PopUp/PopUpSalesmanPartPerformanceHist.aspx?id=' + id, '', 340, 560);
		}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage"><asp:label id="lblPageTitle" runat="server">PART EMPLOYEE - Daftar Sales Performance</asp:label></td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" width="20%">Kode Dealer</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD width="79%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
										runat="server" Width="191px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img style="cursor:hand" alt="Klik Disini untuk memilih Nomor Permintaan" src="../images/popup.gif"
											border="0"></asp:label></TD>
							</TR>
							<TR>
								<TD TD class="titleField" style="WIDTH: 180px; HEIGHT: 24px">Periode
								</TD>
								<TD style="WIDTH: 2px; HEIGHT: 24px">:</TD>
								<TD style="HEIGHT: 24px" width="180" valign="middle">
									<table id="tblPeriode" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td><asp:dropdownlist id="ddlPeriodMonth1" runat="server" Width="80px"></asp:dropdownlist></td>
											<td><asp:dropdownlist id="ddlPeriodYear1" runat="server" Width="50px"></asp:dropdownlist></td>
											<td>s/d</td>
											<td><asp:dropdownlist id="ddlPeriodMonth2" runat="server" Width="80px"></asp:dropdownlist></td>
											<td><asp:dropdownlist id="ddlPeriodYear2" runat="server" Width="50px"></asp:dropdownlist></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%">Employee ID</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD width="25%">
									<asp:textbox id="txtSalesmanCode" runat="server" ReadOnly="True" Width="104px"></asp:textbox>
									<asp:label id="lblShowSalesman" runat="server" width="16px" >
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									</asp:label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD width="1%"></TD>
								<TD>
									<asp:button id="btnCari" runat="server" width="60px" Text="Cari"></asp:button>
									<asp:button id="btnDownloadExcel" Text="Download Excel" Runat="server"></asp:button>
								</TD>
							</TR>
							<tr>
								<td colspan="3" class="titleField">
									&nbsp;
								</td>
							</tr>
							<tr>
								<td colspan="3" class="titleField">
									*) Harga Cost dan Retail Penjualan belum termasuk PPN
								</td>
							</tr>
							<TR>
								<TD class="titleField" colSpan="3">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 240px"><asp:datagrid id="dgSalesmanPerformance" runat="server" Width="100%" CellPadding="3" BackColor="#CDCDCD"
											PageSize="25" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False" CellSpacing="1"
											AllowSorting="True" DESIGNTIMEDRAGDROP="57">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblNo" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.Dealer.SearchTerm2" HeaderText="Search Term 2">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblSearchTerm2" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.Dealer.DealerCode" HeaderText="Kode Dealer">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblKodeDealer" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.SalesmanCode" HeaderText="Kode">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.SalesmanCode") %>' ID="Label1">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.Name" HeaderText="Nama">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.Name") %>' ID="Label2"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Year" HeaderText="Tahun">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblYear" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Year") %>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Month" HeaderText="Bulan">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblMonth" Runat="server" ></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="HargaPokok" HeaderText="Harga Cost Penjualan">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblHargaPokok" runat="server" Text='<%# String.Format("{0:#,###}",DataBinder.Eval(Container, "DataItem.HargaPokok")) %>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="HargaJual" HeaderText="Harga Retail Penjualan">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblHargaJual" runat="server" Text='<%# String.Format("{0:#,###}",DataBinder.Eval(Container, "DataItem.HargaJual")) %>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Profit" HeaderText="Profit">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblIncentive" runat="server" Text='<%# String.Format("{0:#,###}",DataBinder.Eval(Container, "DataItem.Profit")) %>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Percentage" HeaderText="Profit (%)">
													<HeaderStyle Width="8%"  CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblPersentase" runat="server" Text='<%# String.Format("{0:0.00}",DataBinder.Eval(Container, "DataItem.Percentage")) %>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Histori">
													<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnHistory" tabIndex="50" Runat="server" text="Histori" CommandName="history" CausesValidation="True">
															<img src="../images/popup.gif" border="0" alt="Histori"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
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
