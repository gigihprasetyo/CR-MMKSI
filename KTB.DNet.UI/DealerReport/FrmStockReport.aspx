<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmStockReport.aspx.vb" Inherits="FrmStockReport" smartNavigation="False"  %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ListContract</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
			//var SomeChecked = false;
			//function MakeValid()
			//{
		///		SomeChecked = true;
		///	}
		//	
		//	function IsChecked() {
		//		if (IsAnyCheckedCheckBox('chkSelect')) {
		//			SomeChecked = true;
		//		}
		//		else {
		//			SomeChecked = false;
		//			alert("Anda belum memilih faktur");
		//		}
		//	}
			
			function ShowPPStockDealerSelection()
			{
				showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,StockDealerSelection);
			}
			function StockDealerSelection(selectedDealer)
			{
				var txtStockKodeDealer = document.getElementById("txtStockKodeDealer");
				txtStockKodeDealer.value = selectedDealer;			
			}
			/*function AllocationDealerSelection(selectedDealer)
			function ShowPPAllocationDealerSelection()
			{
				showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,AllocationDealerSelection);
			}
			{
				var txtAllocationKodeDealer = document.getElementById("txtAllocationKodeDealer");
				txtAllocationKodeDealer.value = selectedDealer;			
			}
			*/
						
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">DEALER REPORT&nbsp;-&nbsp;Stok Pasar</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<!--							
							<TR>
								<TD class="titleField" width="20%"><asp:label id="lblDealerCode" runat="server">Alokasi Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD width="24%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$()_-|\/#,{}=+^`~')" id="txtAllocationKodeDealer"
										onblur="omitSomeCharacter('txtAllocationKodeDealer','<>?*%$')" runat="server"></asp:textbox><asp:label id="lblSearchAllocationDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD class="titleField" width="20%">Tanggal Stock</TD>
								<TD width="1%"><asp:label id="lblColon4" runat="server">:</asp:label></TD>
								<TD>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><asp:checkbox id="chkChoose" Runat="server"></asp:checkbox></td>
											<td><cc1:inticalendar id="icStartValid" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>&nbsp;s/d&nbsp;</td>
											<td><cc1:inticalendar id="icEndValid" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</TR>
							-->
							<TR>
								<TD class="titleField" style="HEIGHT: 30px"><asp:label id="Label8" runat="server">Stock Dealer</asp:label></TD>
								<TD style="HEIGHT: 30px"><asp:label id="Label5" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 30px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$()_-|\/#,{}=+^`~');" id="txtStockKodeDealer"
										onblur="omitSomeCharacter('txtStockKodeDealer','<>?\/*%$');" runat="server"></asp:textbox><asp:label id="lblSearchStockDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 30px"><asp:label id="lblChassisNo" runat="server">Nomor Rangka</asp:label></TD>
								<TD style="HEIGHT: 30px"><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 30px" noWrap width="34%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;,{}=+^`~');" id="txtChassisNo"
										onblur="omitSomeCharacter('txtChassisNo','<>?*%$;');" runat="server" size="22" MaxLength="20" Width="140px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 1px"><asp:label id="lblCategory" runat="server"> Kategori</asp:label></TD>
								<TD style="HEIGHT: 1px">:</TD>
								<TD style="HEIGHT: 1px"><asp:dropdownlist id="ddlCategory" runat="server"></asp:dropdownlist></TD>
								<TD class="titleField" style="HEIGHT: 1px">Status</TD>
								<TD style="HEIGHT: 1px">:</TD>
								<TD style="HEIGHT: 1px" noWrap><asp:dropdownlist id="ddlStatus" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button></TD>
								<TD class="titleField" style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
							</TR>
							<TR>
								<TD align="right" colSpan="6"><asp:label class="titleField" id="lblTotalUnit" Runat="server">Total Unit : </asp:label><asp:label id="lblTotalUnitVal" Runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dgStock" runat="server" Width="100%" PageSize="100" AllowPaging="true" AutoGenerateColumns="False"
											BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px" CellPadding="3" AllowSorting="True" AllowCustomPaging="True">
											<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
													<HeaderStyle Width="3%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblNo" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="ChassisNumber" SortExpression="ChassisNumber" ReadOnly="True" HeaderText="Nomor Rangka">
													<HeaderStyle Width="8%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="VechileColor.MaterialNumber" HeaderText="Tipe/Warna">
													<HeaderStyle Width="8%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileColor.MaterialNumber") %>' ID="Label2">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="EngineNumber" SortExpression="EngineNumber" ReadOnly="True" HeaderText="Nomor Mesin">
													<HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Alokasi Dealer">
													<HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblDealer runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' tooltip = '<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="StockDealer" HeaderText="Stok Dealer">
													<HeaderStyle Width="4%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblStokDealer" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="StockDate" HeaderText="Tgl Stok">
													<HeaderStyle Width="4%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblTglStok" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Status">
													<HeaderStyle Width="4%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblStatus" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lnkDetail" Runat="server">
															<img src="../images/detail.gif" style="cursor:hand" border="0" alt="Lihat detil"></asp:Label>
														<asp:Label id="lnkEdit" Runat="server">
															<img src="../images/edit.gif" style="cursor:hand" border="0" alt="Ubah detil"></asp:Label>
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
				<tr>
					<td><asp:button id="btnDnLoad" runat="server" Width="96px" Text="Download"></asp:button></td>
				</tr>
				<TR>
					<TD style="HEIGHT: 8px"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
