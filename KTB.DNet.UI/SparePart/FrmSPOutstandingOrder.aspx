<%@ Import Namespace="KTB.DNet.Domain"%>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSPOutstandingOrder.aspx.vb" Inherits="FrmSPOutstandingOrder" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSPOutstandingOrder</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript">
		    function ShowPopUpSparePart() {
		        showPopUp('../General/../PopUp/PopUpSparePart.aspx', '', 500, 760, SparePartSelection);
		    }

		    function SparePartSelection(selectedSparePart) {
		        selectedSparePart = selectedSparePart + ";";
		        var tempParam = selectedSparePart.split(";");

		        var txtPartNumber = document.getElementById("txtPartNumber");
		        txtPartNumber.value = tempParam[0];
		    }

		    function Outstanding(val) {
		        var cmdSearch = document.getElementById("cmdSearch");
		        cmdSearch.click();
		    }

		    function ShowPPDealerSelection() {
		        showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
		    }

		    function DealerSelection(selectedDealer) {
		        var txtDealerSelection = document.getElementById("txtDealerCode");
		        txtDealerSelection.value = selectedDealer;
		    }
		    function ShowPPDealerSelectionOne() {
		        showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory', '', 500, 760, DealerSelectionOne);
		    }
		    function DealerSelectionOne(selectedDealer) {
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
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">PEMESANAN - Outstanding Order</TD>
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
								<TD style="HEIGHT: 23px" width="75%"><asp:label id="lblDealerCode" runat="server"></asp:label><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$;')"
										runat="server" Width="144px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label><asp:button id="btnGetDealer" runat="server" Width="60px" Text="GetDealer"></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label7" runat="server">Nama Dealer</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblDealerName" runat="server"></asp:label>&nbsp;/
									<asp:label id="lblDealerTerm" runat="server"></asp:label></TD>
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
								<TD><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtNomorPesanan" onblur="omitSomeCharacter('txtNomorPesanan','<>?*%$;')"
										runat="server"></asp:textbox></TD>
							</TR>
                            <tr>
                                <td class="titleField">Cara Pembayaran</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:DropDownList ID="ddlTermOfPayment" runat="server" Width="160px" Visible="True"></asp:DropDownList>
                                </td>
                            </tr>
							<TR>
							<TR>
								<TD class="titleField"><asp:label id="Label5" runat="server">Tanggal Pesanan</asp:label></TD>
								<TD><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD>
									<table cellSpacing="0" cellPadding="2" border="0">
										<tr>
											<td><cc1:inticalendar id="ccPODateStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="ccPODateEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<td></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label9" runat="server">Tanggal Berlaku s/d</asp:label></TD>
								<TD><asp:label id="Label10" runat="server">:</asp:label></TD>
								<TD>
									<table cellSpacing="0" cellPadding="2" border="0">
										<tr>
                                            <td><asp:CheckBox ID="cbValidTo" runat="server" /></td>
											<td><cc1:inticalendar id="ccValidToStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
		                                    <td>&nbsp;s.d&nbsp;</td>
											<TD><cc1:inticalendar id="ccValidToEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<td></td>
										</tr>
									</table>
								</TD>
							</TR>
                            <TR>
								<TD class="titleField">Nomor Barang</TD>
								<TD>:</TD>
								<TD><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtPartNumber" onblur="omitSomeCharacter('txtPartNumber','<>?*%$;')"
										runat="server"></asp:textbox>
                                    <asp:label id=lblSearchSparePart runat="server">
							                        <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:label>
								</TD>
							</TR>
                            <tr>
                                <td></td>
                                <td></td>
                                <td><asp:button id="cmdSearch" runat="server" Width="60px" Text="Cari"></asp:button>&nbsp;
                                    <asp:button id="btnDownloadExcel" runat="server" Width="100px" Text="Download Excel"></asp:button>
                                </td>
                            </tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dgSPOutstanding" runat="server" Width="100%" AllowCustomPaging="True" CellSpacing="1"
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
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="SparePartOutstandingOrder.SparePartPO.Dealer.DealerCode">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Jenis Order" SortExpression="SparePartOutstandingOrder.OrderType">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tipe Dokumen"  SortExpression="SparePartOutstandingOrder.DocumentType">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nomor Pesanan" SortExpression="SparePartOutstandingOrder.SparePartPO.PONumber">
										<HeaderStyle Width="12%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lblPONo" runat="server" Text='<%# CType(Container.DataItem, SparePartOutstandingOrderDetail).SparePartOutstandingOrder.SparePartPO.PONumber%>'>
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Cara Pembayaran"  SortExpression="SparePartOutstandingOrder.SparePartPO.TermOfPayment.ID">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label id="lblCaraPembayaran" runat="server" Text='<%# IIf(CType(DataBinder.Eval(Container, "DataItem.SparePartOutstandingOrder.SparePartPO.TermOfPayment.Description"), String) = "", "", CType(DataBinder.Eval(Container, "DataItem.SparePartOutstandingOrder.SparePartPO.TermOfPayment.Description"), String))%>'>
											</asp:Label>
                                        </ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tanggal Pesanan" SortExpression="SparePartOutstandingOrder.SparePartPO.PODate">
										<HeaderStyle Width="12%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPODate" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", CType(Container.DataItem, SparePartOutstandingOrderDetail).SparePartOutstandingOrder.SparePartPO.PODate)%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								    <asp:BoundColumn DataField="PartNumber" HeaderText="Nomor Barang" SortExpression="PartNumber">
									    <HeaderStyle Width="12%" CssClass="titleTableParts"></HeaderStyle>
								    </asp:BoundColumn>
								    <asp:BoundColumn DataField="PartName" HeaderText="Nama Barang" SortExpression="PartName">
									    <HeaderStyle Width="23%" CssClass="titleTableParts"></HeaderStyle>
								    </asp:BoundColumn>
								    <asp:BoundColumn DataField="OrderQty" HeaderText="Jumlah Pesanan" DataFormatString="{0:#,##0}" SortExpression="OrderQty">
									    <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
									    <ItemStyle HorizontalAlign="Right" ></ItemStyle>
								    </asp:BoundColumn>
								    <asp:BoundColumn DataField="AllocationQty" HeaderText="Jumlah Sudah Dialokasi" DataFormatString="{0:#,##0}" SortExpression="AllocationQty">
									    <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
									    <ItemStyle HorizontalAlign="Right" ></ItemStyle>
								    </asp:BoundColumn>
								    <asp:BoundColumn DataField="OpenQty" HeaderText="Jumlah Belum Dialokasi" DataFormatString="{0:#,##0}" SortExpression="OpenQty">
									    <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
									    <ItemStyle HorizontalAlign="Right" ></ItemStyle>
								    </asp:BoundColumn>
								    <asp:BoundColumn DataField="EstimateFillQty" HeaderText="Qty Estimasi Pemenuhan" DataFormatString="{0:#,##0}" SortExpression="EstimateFillQty">
									    <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
									    <ItemStyle HorizontalAlign="Right" ></ItemStyle>
								    </asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Tanggal Estimasi Pemenuhan" SortExpression="EstimateFillDate">
										<HeaderStyle Width="12%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblEstimateFillDate" runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Keterangan">
										<HeaderStyle Width="12%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblKeterangan" runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Berlaku s/d" SortExpression="SparePartOutstandingOrder.ValidTo">
										<HeaderStyle Width="12%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblValidto" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", CType(Container.DataItem, SparePartOutstandingOrderDetail).SparePartOutstandingOrder.ValidTo)%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
										<HeaderStyle Width="12%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Perpanjang Back Order">
										<HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnExtend" CausesValidation="False" CommandName="PerpanjangBO" text="Perpanjang Back Order" Runat="server">
												<img src="../images/assigntodealer.png" border="0" alt="Perpanjang Back Order"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
						<br>
				</TR>
                <tr>
                    <td><br /><asp:button id="btnSendToSAP" runat="server" Width="75px" Text="Kirim ke SAP" Visible="false"></asp:button>&nbsp;
                    </td>
                </tr>
			</TABLE>
		</form>
		<script language="javascript">
		    if (window.parent == window) {
		        if (!navigator.appName == "Microsoft Internet Explorer") {
		            self.opener = null;
		            self.close();
		        }
		        else {
		            this.name = "origWin";
		            origWin = window.open(window.location, "origWin");
		            window.opener = top;
		            window.close();
		        }
		    }
		</script>
	</body>
</HTML>