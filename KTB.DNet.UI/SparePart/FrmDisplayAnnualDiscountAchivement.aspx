<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDisplayAnnualDiscountAchivement.aspx.vb" Inherits="FrmDisplayAnnualDiscountAchivement" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Daftar Annual Discount</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" type="text/javascript">
		function ShowPPDealerSelection()
		{			
			showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',520,780,DealerSelection);
		}		
		function DealerSelection(selectedDealer)
		{
			var txtDealerSelection = document.getElementById("txtDealerCode");
			txtDealerSelection.value = selectedDealer;			
		}
		function ShowPPDealerSelectionOne()
			{
				showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory','',520,780,DealerSelectionOne);
			}
		function DealerSelectionOne(selectedDealer)
		{
			selectedDealer = selectedDealer + ';';
			var tempParam= selectedDealer.split(';');
			var txtDealerSelection = document.getElementById("txtDealerCode");
			var lblDealerName = document.getElementById("lblNamaDealer");
			var lblSearchTerm2 = document.getElementById("lblSearchTerm2");
			txtDealerSelection.value = tempParam[0];
			lblDealerName.value	= tempParam[1];
			lblSearchTerm2.value = tempParam[3];			
			document.getElementById("btnSetDropDown").click();			
		}
		</script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">ANNUAL DISCOUNT -&nbsp;Daftar Pencapaian Annual Discount</td>
				</tr>
				<tr>
					<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 8px" height="8"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%"><asp:label id="lblKodeDealer" runat="server"> Kode Dealer</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:label id="lblKodeDealerValue" runat="server"></asp:label><asp:textbox id="txtDealerCode" runat="server" Width="70px" ></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label><asp:button id="btnSetDropDown" runat="server" Text="SetDropDown"></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 19px"><asp:label id="Label5" runat="server">Nama Dealer</asp:label></TD>
								<TD style="HEIGHT: 19px">:</TD>
								<TD style="HEIGHT: 19px"><asp:label id="lblNamaDealer" runat="server" EnableViewState="True"></asp:label>&nbsp; 
									/&nbsp;
									<asp:label id="lblSearchTerm2" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label2" runat="server">Periode</asp:label></TD>
								<TD>:</TD>
								<TD>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><asp:dropdownlist id="ddlPeriode" runat="server" Width="128px" AutoPostBack="True"></asp:dropdownlist></td>
											<TD>&nbsp;s/d&nbsp;</TD>
											<TD><asp:textbox id="txtPeriode" runat="server" ForeColor="Black"></asp:textbox></TD>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblTotalAmount" runat="server">Grand Total Annual Disc</asp:label></TD>
								<TD>:</TD>
								<TD><b>Rp
										<asp:label id="lblTotalAmountValue" runat="server"></asp:label></b></TD>
							</TR>
							<tr>
								<td><asp:label id="Label3" runat="server" Font-Bold="True">Total Point</asp:label></td>
								<td>:</td>
								<td><b>
										<TABLE cellSpacing="0" cellPadding="0" width="340" border="0">
											<TR>
												<TD width="160"><B><asp:label id="lblTotalPoint" runat="server"></asp:label></B></TD>
												<TD width="180"><asp:button id="btnCari" runat="server" Width="60px" Text="Cari"></asp:button><asp:button id="btnDownload" runat="server" Text="Download" Enabled="False"></asp:button></TD>
											</TR>
										</TABLE>
									</b>
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 310px"><asp:datagrid id="dtgAnnualDiscountAchivement" runat="server" Width="100%" AllowSorting="True"
								AllowPaging="True" PageSize="50" AllowCustomPaging="True" AutoGenerateColumns="False" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px"
								CellPadding="3" BackColor="#CDCDCD">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Silver"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="Id" HeaderText="Id">
										<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label1" runat="server"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="TextBox1" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Dealer">
										<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="MaterialCode" SortExpression="MaterialCode" HeaderText="Nomor Barang">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="MaterialDescription" SortExpression="MaterialDescription" HeaderText="Nama Barang">
										<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Point" SortExpression="Point" HeaderText="Point" DataFormatString="{0:#,###}">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="MinimumQty" SortExpression="MinimumQty" HeaderText="Minimum Qty" DataFormatString="{0:#,###}">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BillQtyThisMonth" SortExpression="BillQtyThisMonth" HeaderText="Jumlah Pembelian Bulan Ini"
										DataFormatString="{0:#,###}">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BillQtyThisPeriod" SortExpression="BillQtyThisPeriod" HeaderText="Jumlah Pembelian"
										DataFormatString="{0:#,###}">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RebateQtyThisPeriod" SortExpression="RebateQtyThisPeriod" HeaderText="Jumlah Point Annual"
										DataFormatString="{0:#,###}">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RebateAmountThisPeriod" SortExpression="RebateAmountThisPeriod" HeaderText="Total Annual Discount (Rp)"
										DataFormatString="{0:#,###}">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RemainQty" SortExpression="RemainQty" HeaderText="Kekurangan">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 483px">&nbsp;&nbsp;&nbsp;
						<asp:label id="Label4" runat="server" ForeColor="Red">* Petunjuk perhitungan Annual Discount dapat dilihat pada Daftar file Annual Discount</asp:label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 483px">&nbsp;&nbsp;&nbsp;
						<asp:label id="Label6" runat="server" ForeColor="Red">* Data yang ditampilkan hanya sampai periode satu tahun kebelakang </asp:label></TD>
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
	</body>
</HTML>
