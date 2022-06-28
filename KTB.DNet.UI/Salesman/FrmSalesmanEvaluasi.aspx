<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanEvaluasi.aspx.vb" Inherits="FrmSalesmanEvaluasi" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ListContract</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			function ShowPPDealerSelection()
		{
			showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
			var txtDealerSelection = document.getElementById("txtKodeDealer");
			txtDealerSelection.value = selectedDealer;			
		}
		
		function ShowSalesmanSelection()
		{
			showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx?FilterIndicator=Unit&IsGroupDealer=1','',500,760,SalemanSelection);
		}
		function SalemanSelection(selectedSales)
		{
			var temp = selectedSales.split(";")
			var txtSalesman = document.getElementById('txtSalesman');
			txtSalesman.value = temp[0];
		}
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" onsubmit="return (SomeChecked == true);" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">Tenaga Penjual&nbsp;-&nbsp;Evaluasi 
						Tenaga Penjual</td>
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
							<TR>
								<TD class="titleField" style="HEIGHT: 11px" width="20%"><asp:label id="lblDealerCode" runat="server">Kode Dealer</asp:label></TD>
								<TD style="HEIGHT: 11px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 11px" width="24%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')"
										runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 11px" width="20%"><asp:label id="lblCategory" runat="server"> Kategori</asp:label></TD>
								<TD style="HEIGHT: 11px" width="1%"><asp:label id="lblColon4" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 11px"><asp:dropdownlist id="ddlCategory" runat="server" Width="140px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblChassisNo" runat="server"> Salesman</asp:label></TD>
								<TD><asp:label id="Label5" runat="server">:</asp:label></TD>
								<TD><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtSalesman" onblur="omitSomeCharacter('txtChassisNo','<>?*%$;')"
										runat="server" Width="140px" MaxLength="20" size="22"></asp:textbox><asp:label id="lblSalesman" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD class="titleField"><asp:checkbox id="chkValidPeriod" runat="server" Text="Tanggal Faktur" Checked="True"></asp:checkbox></TD>
								<TD><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD noWrap width="34%">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><cc1:inticalendar id="icStartValid" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>&nbsp;s/d&nbsp;</td>
											<td><cc1:inticalendar id="icEndValid" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblInvoiceNo" runat="server"> Status</asp:label></TD>
								<TD><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD><asp:listbox id="lboxStatus" runat="server" width="140px" Rows="3" SelectionMode="Multiple"></asp:listbox></TD>
								<TD class="titleField"><asp:checkbox id="chkValidation" runat="server" Text="Tanggal Validasi Faktur"></asp:checkbox></TD>
								<TD><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD noWrap width="34%">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><cc1:inticalendar id="icStartValidation" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>&nbsp;s/d&nbsp;</td>
											<td><cc1:inticalendar id="icEndValidation" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</TR>
							<tr>
								<TD class="titleField"><asp:checkbox id="chkSPK" runat="server" Text="Tanggal SPK"></asp:checkbox></TD>
								<TD><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD noWrap>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><cc1:inticalendar id="icStartConfirm" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>&nbsp;s/d&nbsp;</td>
											<td><cc1:inticalendar id="icEndConfirm" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
								<TD class="titleField"><asp:checkbox id="chkOpenFaktur" runat="server" Text="Tanggal Buka Faktur"></asp:checkbox></TD>
								<TD><asp:label id="Label7" runat="server">:</asp:label></TD>
								<TD noWrap width="34%">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><cc1:inticalendar id="icStartOpen" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>&nbsp;s/d&nbsp;</td>
											<td><cc1:inticalendar id="icEndOpen" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</tr>
							<tr>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD noWrap>
								</TD>
								<TD class="titleField"><asp:checkbox id="chkConfirm" runat="server" Text="Tanggal Konfirmasi Faktur"></asp:checkbox></TD>
								<TD><asp:label id="Label9" runat="server">:</asp:label></TD>
								<TD noWrap width="34%">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><cc1:inticalendar id="icStartConfirmTime" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>&nbsp;s/d&nbsp;</td>
											<td><cc1:inticalendar id="icEndConfirmTime" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</tr>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button></TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblJumRecord" runat="server"></asp:label></TD>
								<TD></TD>
								<TD></TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dgInvoiceList" runat="server" Width="100%" AllowCustomPaging="True" AllowPaging="True"
											PageSize="50" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px" CellPadding="3" AllowSorting="True">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="SPKFakturID" ReadOnly="True" HeaderText="ID">
													<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblNo" runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="DealerCode" SortExpression="DealerCode" ReadOnly="True" HeaderText="Kode Dealer">
													<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="SalesmanCode" SortExpression="SalesmanCode" ReadOnly="True" HeaderText="Kode Salesman">
													<HeaderStyle Width="6%" CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="SalesmanName" SortExpression="SalesmanName" ReadOnly="True" HeaderText="Salesman">
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="LeaderName" SortExpression="LeaderName" ReadOnly="True" HeaderText="Supervisor">
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="CategoryTeam" HeaderText="Kategori Tim">
													<HeaderStyle width="5%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblCategoryTeam" runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="FakturNumber" SortExpression="FakturNumber" ReadOnly="True" HeaderText="No. Faktur">
													<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="FakturDate" HeaderText="Tanggal Faktur">
													<HeaderStyle width="8%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="FakturStatus" HeaderText="Status">
													<HeaderStyle width="5%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="SPKNumber" SortExpression="SPKNumber" ReadOnly="True" HeaderText="No. SPK">
													<HeaderStyle Width="6%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="SPKDate" HeaderText="Tanggal SPK">
													<HeaderStyle width="8%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="ChassisNumber" SortExpression="ChassisNumber" ReadOnly="True" HeaderText="No. Rangka">
													<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CategoryCode" SortExpression="CategoryCode" ReadOnly="True" HeaderText="Kategori">
													<HeaderStyle Width="4%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="MaterialDescription" SortExpression="MaterialDescription" ReadOnly="True"
													HeaderText="Nama Kendaraan">
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CustomerName" SortExpression="CustomerName" ReadOnly="True" HeaderText="Nama Customer">
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle VerticalAlign="Middle" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="btnDownload" Width="110px" Text="Download" Runat="server"></asp:button></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 8px"></TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			if (window.parent==window)
			{
				if (!navigator.appName=="Microsoft Internet Explorer")
				{
				  self.opener = null;
				  self.close();
				}
				else
				{
				   this.name = "origWin";
				   origWin= window.open(window.location, "origWin");
				   window.opener = top;
                   window.close();
				}
			}	
		</script>
		<!-- 
-->
	</body>
</HTML>
