<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmSPPOStatus.aspx.vb" Inherits="frmSPPOStatus" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>frmSPPOStatus</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">

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
		    function ShowPPDealerSelection() {
		        showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
		    }

		    function DealerSelection(selectedDealer) {
		        var txtDealerSelection = document.getElementById("txtDealerCode");
		        txtDealerSelection.value = selectedDealer;
		    }
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server" onfocus="return checkModal()" onclick="checkModal()">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage" colSpan="3">PEMESANAN&nbsp;- Status Pesanan</TD>
				</TR>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="15%">Kode Dealer</TD>
								<TD width="1%">:</TD>
								<TD><asp:label id="lblDealerCode" runat="server" Width="140px">Label</asp:label>
									<asp:textbox id="txtDealerCode" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')" runat="server" Width="144px"></asp:textbox>
									<asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
									<asp:button id="btnGetDealer" runat="server" Width="60px" Text="GetDealer"></asp:button></TD>
								<TD class="titleField" width="15%">Nomor Pesanan</TD>
								<TD>:</TD>
								<TD><asp:textbox id="txtPONumber" runat="server" Width="140px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										onblur="omitSomeCharacter('txtPONumber','<>?*%$;')"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Dealer</TD>
								<TD>:</TD>
								<TD><asp:label id="lblDealerName" runat="server">Label</asp:label>&nbsp;/
									<asp:label id="lblDealerTerm" runat="server">Label</asp:label></TD>
								<TD class="titleField">Nomor Penjualan MKS</TD>
								<TD>:</TD>
								<TD>
                                    <asp:TextBox ID="txtSONumber" runat="server" Width="140px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										onblur="omitSomeCharacter('txtSONumber','<>?*%$;')"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Jenis Order</TD>
								<TD>:</TD>
								<TD><asp:dropdownlist id="ddlJenisOrder" runat="server" Width="140px"></asp:dropdownlist></TD>
								<TD class="titleField">Nomor Pengiriman MKS</TD>
								<TD>:</TD>
								<TD>
                                    <asp:TextBox ID="txtDONumber" runat="server" Width="140px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										onblur="omitSomeCharacter('txtDONumber','<>?*%$;')"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Status Packing</TD>
								<TD>:</TD>
								<TD><asp:dropdownlist id="ddlPackingStatus" runat="server" Width="140px"></asp:dropdownlist></TD>
								<TD class="titleField">Nomor Faktur</TD>
								<TD>:</TD>
								<TD><asp:textbox id="txtFakturNumber" runat="server" Width="140px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										onblur="omitSomeCharacter('txtFakturNumber','<>?*%$;')"></asp:textbox></TD>
							</TR>
                            <TR>
								<TD class="titleField">Tipe Dokumen</TD>
								<TD><asp:label id="Label8" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="cmbDocumentType" runat="server" Width="140px"></asp:dropdownlist></TD>
								
                                <td class="titleField">Ceiling Status</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:DropDownList ID="ddlCeilingStatus" runat="server" Width="160px" Visible="True"></asp:DropDownList>
                                </td>
							</TR>
							<TR>
								<TD class="titleField">Tanggal Pesanan</TD>
								<TD>:</TD>
								<TD><table border="0" cellpadding="2" cellspacing="0">
										<tr>
                                            <td><asp:CheckBox ID="chkPODate" runat="server" /></td>
											<td><cc1:inticalendar id="icPODate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="icPODateTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<td></td>
										</tr>
									</table>
								</TD>
                                
                                <td class="titleField">Cara Pembayaran</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:DropDownList ID="ddlTermOfPayment" runat="server" Width="160px" Visible="True"></asp:DropDownList>
                                </td>
							</TR>
							<TR>
								<TD class="titleField">Tanggal Penjualan MKS</TD>
								<TD>:</TD>
								<TD><table border="0" cellpadding="2" cellspacing="0">
										<tr>
                                            <td><asp:CheckBox ID="chkSODate" runat="server" /></td>
											<td><cc1:inticalendar id="icSODate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="icSODateTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<td></td>
										</tr>
									</table>
								</TD>
                                <td class="titleField" colspan="3"><asp:button id="btnFind" runat="server" Width="60px" Text="Cari"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD colSpan="3"><div id="div1" style="OVERFLOW: auto; HEIGHT: 300px">
                        <asp:datagrid id="dtgPOStatus" runat="server" Width="100%" BorderColor="Gainsboro" BackColor="Gainsboro"
								CellSpacing="1" CellPadding="3" OnItemDataBound="dtgPOStatus_ItemDataBound" BorderWidth="0px" AutoGenerateColumns="False" PageSize="50" AllowPaging="True"
								AllowCustomPaging="True" AllowSorting="True">
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle Font-Bold="True" Height="20px" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="POID" HeaderText="ID">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="DealerCode">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Jenis order" SortExpression="OrderType">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tipe Dokumen" SortExpression="DocumentType">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn SortExpression="PODate" HeaderText="Tanggal Pesanan">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn SortExpression="POSendDate" HeaderText="Tanggal Kirim PO">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="PONumber" HeaderText="Nomor Pesanan">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
                                            <asp:LinkButton id="lnkPONumber" runat="server" CommandName="PODetail" ></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SONumber" HeaderText="Nomor Penjualan (SO MMKSI)">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
                                            <asp:LinkButton id="lnkSONumber" runat="server" ></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DONumber" HeaderText="Nomor DO MMKSI">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
                                            <asp:LinkButton id="lnkDONumber" runat="server" CommandName="DODetail"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="BillingNumber" HeaderText="Nomor Faktur">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
                                            <asp:LinkButton id="lnkBillingNumber" runat="server" ></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Ceiling Status" SortExpression="TOPCeilingStatus">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblTOPCeilingStatus" runat="server"><%-- Text='<%# DataBinder.Eval(Container, "DataItem.TOPCeilingStatus")%>'>--%>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Cara Pembayaran" SortExpression="TOPDescription">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblTOPDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TOPDescription")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Status">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
                                            <asp:Label id="lblStatus" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Indikator">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
                                            <asp:Image ID="imgStatus" Runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="false">
										<HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDetail" runat="server" Visible ="true">
												<img style="cursor:hand" alt="Rincian" src="../images/detail.gif" border="0" height="17px"
													width="17px">
											</asp:Label>
											<asp:LinkButton id="lnkPrint" runat="server" CausesValidation="False" text="Cetak" CommandName="Print">
												<img src="../images/print.gif" border="0" alt="Cetak" height="17px" width="17px"></asp:LinkButton>
											<asp:LinkButton id="lnkDownload" runat="server" CausesValidation="False" CommandName="Download" Visible="false">
												<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
                                    <asp:BoundColumn Visible="False" DataField="SOID" HeaderText="SOID">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn Visible="False" DataField="DOID" HeaderText="DOID">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn Visible="False" DataField="BillingID" HeaderText="BillingID">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
                <tr>
                    <td align="left">
                        <table>
                            <tr>
                                <td colspan="3"><asp:Label ID="Label22" runat="server">Keterangan indikator :</asp:Label></td>
                            </tr>
                            <tr>
                                <td><img src="../images/whitebox.jpg" border="0" alt="" height="17px" width="17px"></td>
                                <td>:</td>
                                <td><asp:Label ID="Label4" runat="server">Status : PO - Delivery</asp:Label></td>
                            </tr>
                            <tr>
                                <td><img src="../images/yellowbox.jpg" border="0" alt="" height="17px" width="17px"></td>
                                <td>:</td>
                                <td><asp:Label ID="Label1" runat="server">Status Delivery : Hari ini  <= Tanggal ETA</asp:Label></td>
                            </tr>
                            <tr>
                                <td><img src="../images/redbox.jpg" border="0" alt="" height="17px" width="17px"></td>
                                <td>:</td>
                                <td><asp:Label ID="Label2" runat="server">Status Delivery : Hari ini  > Tanggal ETA</asp:Label></td>
                            </tr>
                            <tr>
                                <td><img src="../images/greenbox.jpg" border="0" alt="" height="17px" width="17px"></td>
                                <td>:</td>
                                <td><asp:Label ID="Label3" runat="server">Status Delivery : Diterima oleh Dealer</asp:Label></td>
                            </tr>
                        </table>
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
