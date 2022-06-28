<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DetailInOutMaterialPromotion.aspx.vb" Inherits="DetailInOutMaterialPromotion"  smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>DetailInOutMaterialPromotion</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">

		function ShowPPDealerSelection()
		{
			showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx?x=Territory','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var txtDealer= document.getElementById("txtKodeDealer");
			txtDealer.value = selectedDealer;				
		}
		function ShowPPMatrialPromotion()
		{
			showPopUp('../PopUp/PopUpMaterialPromotion.aspx','',500,760,MPSelection);
		}
		function MPSelection(selectedMP)
		{
			var txtKodeBarang= document.getElementById("txtKodeBarang");
			txtKodeBarang.value = selectedMP;				
		}
		function ClosePopUp(){
			window.close();
		}
		</script>
</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">MATERIAL PROMOSI - Detail In-Out Material Promosi</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>			</TABLE>
			<table width="100%" border="0" cellpadding="2" cellspacing="1">
				<tr>
					<td class="titleField" style="HEIGHT: 20px" width="24%">Kode Dealer</td>
					<TD style="HEIGHT: 20px" width="1%">:</TD>
					<td style="WIDTH: 223px; HEIGHT: 20px" width="75%"><asp:textbox id="txtKodeDealer" runat="server" Width="192px" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
							onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"></asp:textbox><asp:label id="lblDealer" runat="server" width="16px">
							<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></td>
				</tr>
				<TR>
					<TD class="titleField" style="WIDTH: 24%; HEIGHT: 25px">Kode Barang</TD>
					<TD style="HEIGHT: 25px" width="1%">:</TD>
					<td style="WIDTH: 223px; HEIGHT: 25px" width="75%"><asp:textbox id="txtKodeBarang" runat="server" Width="192px" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
							onblur="omitSomeCharacter('txtKodeBarang','<>?*%$')"></asp:textbox><asp:label id="lblPopUpMPMaster" runat="server" width="16px"><img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></td>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 24%">Tipe Adjustment </TD>
					<TD width="1%">:</TD>
					<td width="75%"><asp:dropdownlist id="ddlAdjustType" runat="server" Width="136px"></asp:dropdownlist></td>
				<TR>
					<TD class="titleField">Tanggal</TD>
					<TD width="1%">:</TD>
					<td width="75%">
						<table border=0 cellpadding=0 cellspacing=0>
							<tr>
								<td><cc1:inticalendar id="icTglStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
								<td>&nbsp;s/d&nbsp;</td>
								<td><cc1:inticalendar id="icTglEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
							</tr>
						</table>
					</td>
				<tr>
					<TD class="titleField"></TD>
					<TD width="1%"></TD>
					<td width="75%"><asp:button id="btnCari" runat="server" Width="60px" Text="Cari"></asp:button></td>
				</tr>
			</table>
			<table cellSpacing="1" cellPadding="0" width="100%">
				<tr>
					<td><div id="div1" style="OVERFLOW: auto; HEIGHT: 280px"><asp:datagrid id="dtgDetail" runat="server" Width="100%" AllowPaging="True" AllowCustomPaging="True"
							CellSpacing="1" GridLines="Horizontal" CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px"
							BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" PageSize="25">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White" VerticalAlign=Top></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="#FFFFFF"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
									<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression = "Dealer.DealerName" HeaderText = "Nama Dealer">
									<HeaderStyle CssClass = "titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label ID="lblDealerName" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'></asp:Label>										
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression = "Dealer.City.CityName" HeaderText = "Kota">
									<HeaderStyle CssClass = "titleTablePromo"/>
									<ItemTemplate>
										<asp:Label ID="lblDealerCity" Runat = "server" Text = '<%#DataBinder.Eval(Container,"DataItem.Dealer.City.CityName")%>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn SortExpression="CreatedTime" HeaderText="Tanggal">
									<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblTanggal" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.CreatedTime"),"dd/MM/yyyy") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="MaterialPromotion.GoodNo" HeaderText="Kode Barang">
									<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblKdBarang" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MaterialPromotion.GoodNo") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="MaterialPromotion.Name" HeaderText="Nama Barang">
									<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblNmBarang" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MaterialPromotion.Name") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="AdjustType" HeaderText="Tipe Adjustment">
									<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:label id="lblAdjType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AdjustType") %>'>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="StockAwal" HeaderText="Stock Awal">
									<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign=right></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblStockAwal" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.StockAwal"),"#,##0") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Qty" HeaderText="Adj. Qty">
									<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign=right></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblAdjQty" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.Qty"),"#,##0") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Sisa Stock">
									<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign=right></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblSisaStock" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Keterangan" HeaderText="Keterangan">
									<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblKeterangan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Keterangan") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#404040" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></div></td>
				</tr>
				<tr height="40">
					<td align="center"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
