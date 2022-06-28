<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmSummarySPPO.aspx.vb" Inherits="frmSummarySPPO" smartNavigation="False" %>
<%@ Import Namespace="KTB.DNet.Domain"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Pembatalan Pemesanan</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			function newLocation(loc)
			{
			window.location=loc
			}		
			
		
		
		function ShowPPDealerSelection()
		{
		  //  showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
		    showPopUp('../General/../PopUp/PopUpDealerSelectionArea.aspx', '', 500, 760, DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
			var txtDealerSelection = document.getElementById("txtDealerCode");
			txtDealerSelection.value = selectedDealer;			
		}
		
		function SparePartPO(selectedCode)
		{
				alert("PO tidak ditemukan")
		}
		
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">
						PEMESANAN - Monitoring Sparepart</TD>
				</TR>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD valign="top">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField">Kode Dealer</TD>
								<TD>:</TD>
								<TD><asp:textbox id="txtDealerCode" runat="server" Width="144px" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
										onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Jenis Pesanan</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:dropdownlist id="ddlOrderType" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField">Status</TD>
								<TD>:</TD>
								<TD><asp:dropdownlist id="ddlProcessCode" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField">Tanggal Pesanan</TD>
								<TD>:</TD>
								<TD>
									<table cellSpacing="0" cellPadding="2" border="0">
										<tr>
											<td>
												<asp:CheckBox id="cbTanggalPesanan" runat="server" Checked="True"></asp:CheckBox>
											</td>
											<td>
												<cc1:inticalendar id="icPODateStart" runat="server" TextBoxWidth="70"></cc1:inticalendar>
											</td>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="icPODateEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<td></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" vAlign="top">Tanggal Pembuatan</TD>
								<TD vAlign="top">:</TD>
								<TD>
									<table cellSpacing="0" cellPadding="2" border="0">
										<tr>
											<td>
												<asp:CheckBox id="cbTanggalPembuatan" runat="server"></asp:CheckBox>
											</td>
											<td>
												<cc1:inticalendar id="icStartTanggalPembuatan" runat="server" TextBoxWidth="70"></cc1:inticalendar>
											</td>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="icEndTanggalPembuatan" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<td></td>
										</tr>
									</table>
									<asp:Button id="btnDownload" runat="server" Width="76px" Text="Download"></asp:Button>
									<asp:Button id="btnCari" runat="server" Text="Cari" Width="56px"></asp:Button>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="lblTotalAmount" runat="server" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;
						<asp:Label id="lblTotalQuantity" runat="server" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;
						<asp:Label id="lblGrandTotal" runat="server" Font-Bold="True"></asp:Label>
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 260px"><asp:datagrid id="dtgSPPO" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="25"
								AllowPaging="True" AllowSorting="True" AllowCustomPaging="True" CellSpacing="1" CellPadding="3" BorderColor="Gainsboro" BackColor="Gainsboro"
								BorderWidth="0px">
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle Font-Bold="True" Height="20px" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="ID">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server" Text='<%# CType(Container.DataItem, V_SparePartPOSummary).DealerCode %>'>Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DealerName" HeaderText="Nama Dealer">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerName" runat="server" Text='<%# CType(Container.DataItem, V_SparePartPOSummary).DealerName %>'>Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PONumber" HeaderText="Nomor Pesanan">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPONumber" runat="server" Text='<%# CType(Container.DataItem, V_SparePartPOSummary).PONumber %>'>Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PODate" HeaderText="Tanggal Pesanan">
										<HeaderStyle HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="Label1" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", CType(Container.DataItem, V_SparePartPOSummary).PODate) %>'>Label</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="OrderType" HeaderText="Jenis Pesanan">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label2" runat="server" Text='<%# CType(Container.DataItem, V_SparePartPOSummary).OrderTypeDesc %>'>Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ProcessCode" HeaderText="Status">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblProcessCode" runat="server" Text='<%# CType(Container.DataItem, V_SparePartPOSummary).ProcessCodeDesc %>'>Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ItemCount" HeaderText="Total Item" DataFormatString="{0:#,###}">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ItemAmount" HeaderText="Total Amount" DataFormatString="{0:#,###}">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			if (document.parentWindow.name != "frMain")
				{
				  self.opener = null;
				  self.close();
				}
		</script>
	</body>
</HTML>
