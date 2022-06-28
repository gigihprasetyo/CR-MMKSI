<%@ Import Namespace="KTB.DNet.Domain"%>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPurchaseOrderEstimate.aspx.vb" Inherits="FrmPurchaseOrderEstimate" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPurchaseOrderEstimate</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript">
		function Estimate()
		{
		alert ('Detail tidak ditemukan');
		}
		
		function ShowPPDealerSelection()
		{			
			showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
			var txtDealerSelection = document.getElementById("txtDealerCode");
			txtDealerSelection.value = selectedDealer;			
		}
		function ShowPPDealerSelectionOne()
		{
			showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory','',500,760,DealerSelectionOne);
		}
		function DealerSelectionOne(selectedDealer)
		{
			selectedDealer = selectedDealer + ";";
			var tempParam = selectedDealer.split(';');
			var txtDealerCode = document.getElementById("txtDealerCode");
			var lblDealerName = document.getElementById("lblDealerName");
			var lblDealerTerm = document.getElementById("lblDealerTerm");
			txtDealerCode.value = tempParam[0];	
			lblDealerName.innerHTML = tempParam[1];
			lblDealerTerm.innerHTML = tempParam[3];
		}
		</script>
	    <style type="text/css">
            .auto-style1 {
                height: 23px;
                width: 42%;
            }
            .auto-style2 {
                width: 42%;
            }
            .auto-style3 {
                height: 23px;
                width: 17%;
            }
            .auto-style4 {
                width: 17%;
            }
            .auto-style5 {
                width: 17%;
                font-weight: bold;
            }
        </style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">PEMESANAN - Estimasi Pesanan</TD>
				</TR>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 23px" width="24%"><asp:label id="Label1" runat="server"> Kode Dealer</asp:label></TD>
								<TD style="HEIGHT: 23px" width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD class="auto-style1"><asp:label id="lblDealerCode" runat="server"></asp:label><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$;')"
										runat="server" Width="144px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label><asp:button id="btnGetDealer" runat="server" Width="60px" Text="GetDealer"></asp:button></TD>
								<TD class="auto-style3">&nbsp;</TD>
								<TD style="HEIGHT: 23px" width="30%">&nbsp;</TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label7" runat="server">Nama Dealer</asp:label></TD>
								<TD>:</TD>
								<TD class="auto-style2"><asp:label id="lblDealerName" runat="server"></asp:label>&nbsp;/
									<asp:label id="lblDealerTerm" runat="server"></asp:label></TD>
								<TD class="auto-style4">&nbsp;</TD>
								<TD>&nbsp;</TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label3" runat="server">Jenis Order</asp:label></TD>
								<TD><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="cmbOrderTye" runat="server" Width="140px" AutoPostBack="True" OnSelectedIndexChanged="cmbOrderType_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
                            <TR>
								<TD class="titleField">Tipe Dokumen</TD>
								<TD><asp:label id="Label8" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="cmbDocumentType" runat="server" Width="140px"></asp:dropdownlist></TD>
                                
							</TR>
							<TR>
								<TD class="titleField">Nomor Pesanan</TD>
								<TD>:</TD>
								<TD class="auto-style2"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtNomorPesanan" onblur="omitSomeCharacter('txtNomorPesanan','<>?*%$;')"
										runat="server"></asp:textbox></TD>
                                <TD class="auto-style5"></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="titleField">
                                    <asp:label id="Label5" runat="server">Tanggal Pesanan</asp:label>
								</TD>
								<TD><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD class="auto-style2">
									<table cellSpacing="0" cellPadding="2" border="0">
										<tr>
                                            <td><asp:CheckBox ID="chkPO" runat="server" /></td>
											<td><cc1:inticalendar id="ccPODateStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="ccPODateEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<td></td>
										</tr>
									</table>
								</TD>
								<TD class="auto-style5">Jumlah Pesanan</TD>
								<TD>:&nbsp;<asp:Label ID="lblTotalQuantity" runat="server"></asp:Label>
                                </TD>
							</TR>
                            <TR>
								<TD class="titleField"><asp:label id="Label9" runat="server">Tanggal Penjualan MKS</asp:label></TD>
								<TD><asp:label id="Label10" runat="server">:</asp:label></TD>
								<TD class="auto-style2">
									<table cellSpacing="0" cellPadding="2" border="0">
										<tr>
                                            <td><asp:CheckBox ID="chkSO" runat="server" /></td>
											<td><cc1:inticalendar id="ccSODateStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="ccSODateEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
										</tr>
									</table>
								</TD>
								<TD class="auto-style5">Total Tagihan</TD>
								<TD>:&nbsp;<asp:Label ID="lblTotalTagihan" runat="server"></asp:Label>
                                </TD>
							</TR>
                        <tr>
                            <td class="titleField">Cara Pembayaran</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:DropDownList ID="ddlTermOfPayment" runat="server" Width="160px" Visible="True"></asp:DropDownList>
                            </td>
                        </tr>
                            <tr>
                            <td class="titleField"></td>
                            <td width="1%"></td>
							<td><asp:button id="cmdSearch" runat="server" Width="60px" Text="Cari"></asp:button></td>
                        </tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dgPOEstimate" runat="server" Width="100%" AllowCustomPaging="True" CellSpacing="1"
								CellPadding="3" AllowPaging="True" PageSize="50" AutoGenerateColumns="False" BorderWidth="0px" BorderColor="Gainsboro" BackColor="Gainsboro"
								AllowSorting="True">
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle Font-Bold="True" Height="30px" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="SparePartPO.Dealer.DealerCode">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Jenis order" SortExpression="SparePartPO.OrderType">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tipe Dokumen" SortExpression="DocumentType">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nomor Pesanan" SortExpression="SparePartPO.PONumber">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPONo" runat="server" Text='<%# CType(Container.DataItem, SparePartPOEstimate).SparePartPO.PONumber %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tanggal Pesanan" SortExpression="SparePartPO.PODate">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPODate" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", CType(Container.DataItem, SparePartPOEstimate).SparePartPO.PODate) %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="SONumber" HeaderText="Nomor Penjualan MMKSI" SortExpression="SONumber">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Tanggal Penjualan MMKSI" SortExpression="SODate">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblSODate" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", CType(Container.DataItem, SparePartPOEstimate).SODate) %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Jadwal Pengiriman / Pembayaran" SortExpression="DeliveryDate">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblSchedule" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", CType(Container.DataItem, SparePartPOEstimate).DeliveryDate) %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartPO.TermOfPayment.ID" HeaderText="Cara Pembayaran">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<%--<asp:Label id="Label1" runat="server" Text='<%# CType(Container.DataItem, SparePartPO).TermOfPayment.Description%>'>Label</asp:Label>--%>
											<asp:Label id="LabelTOP" runat="server" Text='<%# IIf(CType(DataBinder.Eval(Container, "DataItem.SparePartPO.TermOfPayment.Description"), String) = "", "", CType(DataBinder.Eval(Container, "DataItem.SparePartPO.TermOfPayment.Description"), String))%>'>Label</asp:Label>
                                            
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nilai Tagihan (Rp)">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblTotalAmount" runat="server" Text='<%# String.Format("{0:#,##0}", CType(Container.DataItem, SparePartPOEstimate).POEstimateAmount) %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="PPN (Rp)">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
                                            <asp:Label ID="lblVAT" runat="server" ></asp:Label>
											<%-- %><asp:Label id="lblVAT" runat="server" Text='<%# String.Format("{0:#,##0}", CType(Container.DataItem, SparePartPOEstimate).POEstimateAmount * 0.1) %>'> 
											</asp:Label>--%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Total Tagihan (Rp)">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
                                            <asp:Label id="lblNetAmount" runat="server">
											</asp:Label>
											<%-- %><asp:Label id="lblNetAmount" runat="server" Text='<%# String.Format("{0:#,##0}", CType(Container.DataItem, SparePartPOEstimate).POEstimateAmount + (CType(Container.DataItem, SparePartPOEstimate).POEstimateAmount * 0.1))%>'>
											</asp:Label>--%>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <%--Add by reza Req by Mai 11072018--%>
									<asp:BoundColumn HeaderText="Picking Ticket" SortExpression="SparePartPO.PickingTicket">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
                                    <%--end--%>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDetail" runat="server">
												<img style="cursor:hand" alt="Detail" src="../images/detail.gif">
											</asp:Label>
											<asp:LinkButton id="lblPrint" runat="server" CommandName="Print" text="Cetak" CausesValidation="False">
												<img src="../images/print.gif" border="0" alt="Cetak" height="17px" width="17px"></asp:LinkButton>
											<asp:LinkButton id="lnkDownload" runat="server" CommandName="Download" CausesValidation="False">
												<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
						<br>
						<asp:button id="cmdDownload" runat="server" Width="64px" Text="Download" ToolTip="Download File Terakhir"></asp:button>&nbsp;
						<asp:button id="btnDownloadFaktur" runat="server" Width="104px" Text="Download Faktur" ToolTip="Download File Terakhir"></asp:button></TD>
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
