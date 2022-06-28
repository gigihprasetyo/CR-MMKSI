<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDaftarInvoice.aspx.vb" Inherits="FrmDaftarInvoice" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ListPO</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
          <script language="javascript" src="../WebResources/FormFunctions.js"></script>

		<script language="javascript" src="../WebResources/InputValidation.js"></script>
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
		
		function ViewDailyPKFlow()
		{}
		function ShowPPAccountSelection()
		{
			showPopUp('../PopUp/PopUpCreditAccountSelection.aspx','',500,760,AccountSelection);
		}
		
		function AccountSelection(selectedAccount)
		{
			var txtDealerSelection = document.getElementById("txtKodeDealer");
			var str = selectedAccount.split(";");
			txtDealerSelection.value = str[0];			
		}
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			 <TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">INVOICE&nbsp;&nbsp;- Daftar Invoice</td>
				</tr>
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
								<TD class="titleField" style="HEIGHT: 17px" width="22%"><asp:label id="lblDealer" runat="server">Kode Dealer</asp:label></TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD style="WIDTH: 328px; HEIGHT: 17px" width="328"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 17px" width="20%"><asp:label id="Label6" runat="server"> Nomor Invoice</asp:label></TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD style="HEIGHT: 17px" width="20%"><asp:textbox onkeypress="return alphaNumericPlusUniv(event)" id="txtNoInvoice" onblur="return alphaNumericPlusBlur(txtNoInvoice)"
										runat="server" Width="140px" MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField"><asp:label id="Label5" runat="server" Width="88px">Tanggal Invoice</asp:label></TD>
								<TD>:</TD>
								<TD style="WIDTH: 328px">
									<TABLE id="Table3" cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD><cc1:inticalendar id="icListDI1" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="icListDI2" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>
								<td class="titleField"><asp:label id="lblDealerPO" runat="server"> Nomor PO</asp:label></td>
								<TD>
									<asp:Label id="lblDealerPOColon" runat="server">:</asp:Label></TD>
								<TD><asp:textbox id="txtDealerPO" runat="server" Width="140px" MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 17px">Produk</TD>
								<TD style="HEIGHT: 17px">
									<asp:Label id="lblOrderTypeColon" runat="server">:</asp:Label></TD>
								<TD style="WIDTH: 328px; HEIGHT: 17px">
									<asp:dropdownlisT style="Z-INDEX: 0" id="ddlProductCategory" runat="server" width="130px" AutoPostBack="True"></asp:dropdownlisT></TD>
								<TD class="titleField" style="HEIGHT: 17px"><asp:label id="Label2" runat="server"> Nomor S/O</asp:label></TD>
								<TD style="HEIGHT: 17px">:</TD>
								<TD style="HEIGHT: 17px"><asp:textbox onkeypress="return alphaNumericPlusUniv(event)" id="txtNoSO" onblur="alphaNumericPlusBlur(txtNoSO)"
										runat="server" Width="140px" MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblOrdertype" runat="server" style="Z-INDEX: 0"> Kategori</asp:label></TD>
								<TD>
									<asp:Label id="Label3" runat="server">:</asp:Label></TD>
								<TD style="WIDTH: 328px"><B><B><asp:dropdownlist id="ddlSalesOrg" runat="server" Width="130px" style="Z-INDEX: 0"></asp:dropdownlist></B></B></TD>
								<TD class="titleField"><asp:label id="lblNoMO" runat="server"> Nomor O/C</asp:label></TD>
								<TD>
									<asp:Label id="lblNoMOColon" runat="server">:</asp:Label></TD>
								<TD><asp:textbox onkeypress="return alphaNumericPlusUniv(event)" id="txtNoOC" onblur="alphaNumericPlusBlur(txtNoOC)"
										runat="server" Width="140px" MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>
									<asp:label id="lblTotalTebus" runat="server" Font-Bold="True" style="Z-INDEX: 0">Total Harga Tebus</asp:label></TD>
								<TD>:</TD>
								<TD style="WIDTH: 328px"><b>
										<asp:label id="lblJmlHargaTebus" runat="server" Font-Bold="True" style="Z-INDEX: 0"></asp:label></b></TD>
								<TD class="titleField"><asp:label id="lblNoDebitCharge" runat="server"> Nomor Debit Charge</asp:label></TD>
								<TD>
									<asp:Label id="lblNoDebitChargeColon" runat="server">:</asp:Label></TD>
								<TD><asp:textbox onkeypress="return alphaNumericPlusUniv(event)" id="txtNoDebitCharge" onblur="alphaNumericPlusBlur(txtNoDebitCharge)"
										runat="server" Width="140px" MaxLength="20"></asp:textbox></TD>
							</TR>
                            <TR>
								<TD><b>Jenis Invoice</b></TD>
								<TD>:</TD>
								<TD style="WIDTH: 328px"><asp:DropDownList ID="ddlInvoiceKind" runat="server"></asp:DropDownList></TD>
								<TD class="titleField"><asp:label id="Label1" runat="server"> Nomor Debit Memo</asp:label></TD>
								<TD>
									<asp:Label id="lblNoDebitMemoColon" runat="server">:</asp:Label></TD>
								<TD><asp:textbox onkeypress="return alphaNumericPlusUniv(event)" id="txtNoDebitMemo" onblur="alphaNumericPlusBlur(txtNoDebitMemo)"
										runat="server" Width="140px" MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD style="WIDTH: 328px"></TD>
								<TD></TD>
								<TD></TD>
								<TD><asp:button id="btnCari" runat="server" Width="60px" Text="Cari"></asp:button>&nbsp;
									<asp:button id="btnDownload" runat="server" Width="75px" Text="Download"></asp:button></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD style="WIDTH: 328px">
									
								</TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<DIV id="div1" style="HEIGHT: 320px; OVERFLOW: auto"><asp:datagrid id="dtgDaftarInv" runat="server" Width="100%" CellPadding="3" BorderWidth="0px"
								CellSpacing="1" BorderColor="#CDCDCD" BackColor="#CDCDCD" AllowPaging="True" OnItemDataBound="dtgDaftarInv_itemdataBound" OnItemCommand="dtgDaftarInv_Edit"
								PageSize="50" AllowCustomPaging="True" AutoGenerateColumns="False" AllowSorting="True">
								<SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
								<EditItemStyle VerticalAlign="Top"></EditItemStyle>
								<AlternatingItemStyle VerticalAlign="Top" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle VerticalAlign="Top" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" BackColor="Teal"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
                                      <asp:TemplateColumn Visible="false">
													<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														<INPUT id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('ChkExport', document.all.chkAllItems.checked)"
															type="checkbox">
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="ChkExport" runat="server"></asp:CheckBox>
                                                           <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>' Visible="false">
                                                               </asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>

									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesOrder.POHeader.ContractHeader.Dealer.DealerCode" HeaderText="Dealer">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="TextBox1" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesOrder.POHeader.ContractHeader.Dealer.CreditAccount" HeaderText="Credit Account">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblCreditAccount" runat="server"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="Textbox2" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn SortExpression="InvoiceNumber" HeaderText="Nomor Invoice">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>

                                    <asp:TemplateColumn   HeaderText="Jenis Invoice">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblInvoiceKind" runat="server"></asp:Label>
										</ItemTemplate>
										 
									</asp:TemplateColumn>


                                    <asp:BoundColumn SortExpression="LogisticDN.DebitMemoNo" HeaderText="Nomor Debit Memo">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn SortExpression="SalesOrder.LogisticDCHeader.DebitChargeNo" HeaderText="Nomor Debit Charge">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="SalesOrder.SONumber" HeaderText="Nomor SO">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="SalesOrder.POHeader.PONumber" HeaderText="No Reg PO">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="SalesOrder.POHeader.DealerPONumber" HeaderText="Nomor PO">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="InvoiceDate" HeaderText="Tanggal Invoice">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Nama Pesanan Khusus" SortExpression="SalesOrder.POHeader.ContractHeader.ProjectName">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="SalesOrder.POHeader.ContractHeader.Category.CategoryCode" HeaderText="Kategori">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn SortExpression="SalesOrder.Amount" HeaderText="Harga SO (Rp)">
										<HeaderStyle HorizontalAlign="Center" ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn SortExpression="LogisticDN.TotalAmount" HeaderText="Total Biaya Kirim Incl PPN (Rp)">
										<HeaderStyle HorizontalAlign="Center" ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Harga Tebus (Rp)">
										<HeaderStyle HorizontalAlign="Center" ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn Visible="False">
										<HeaderStyle ForeColor="White" Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnFileName" runat="server" CommandName="Download">
												<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
											<asp:Label id="lblString" runat="server" Visible="False"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Download Invoice">
										<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnLihat" runat="server" CommandName="Download">
												<img src="../images/detail.gif" alt="Lihat Detil" border="0" style="cursor:hand"></asp:LinkButton>
											<asp:Label id="lblText" runat="server" Visible="False"></asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Download Debit Memo">
										<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnLihatDebitMemo" runat="server" CommandName="DownloadDebitMemo">
												<img src="../images/detail.gif" alt="Lihat Detil" border="0" style="cursor:hand"></asp:LinkButton>
											<asp:Label id="lblTextDebitMemo" runat="server" Visible="False"></asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False">
										<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnUbah" runat="server" CommandName="Edit">
												<img src="../images/edit.gif" alt="Ubah" border="0" style="cursor:hand"></asp:LinkButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblHistoryStatus" runat="server">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Lihat Perubahan Status"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblFlow" runat="server">
												<img src="../images/alur_flow2.gif" style="cursor:hand" border="0" alt="Lihat Alur PO"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

                                      <asp:TemplateColumn Visible="true">
                                        <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnDelete" runat="server" Text="Del" CommandName="Delete">
												<img src="../images/trash.gif" alt="Hapus" border="0"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
                <tr><td><label> &nbsp;</label></td></tr>
                <tr>
                    <td>
                        <asp:button   id="btnDelete" runat="server" Width="136px" Text="Hapus" visible="false"></asp:button>

                    </td>

                </tr>
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
