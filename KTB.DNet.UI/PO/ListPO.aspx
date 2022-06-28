<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ListPO.aspx.vb" Inherits="ListPO"  smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
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
	 

		function Confirm(msg) {
		    var confirm_value = document.createElement("INPUT");
		    confirm_value.type = "hidden";
		    confirm_value.name = "confirm_value";
		    if (confirm(msg)) {
		        confirm_value.value = "Yes";
		    } else {
		        confirm_value.value = "No";
		    }
		    document.forms[0].appendChild(confirm_value);
		}
		
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<!--td class="titlePage">PO HARIAN &nbsp;- Daftar PO</td-->
					<td class="titlePage">Sales Order &nbsp;- Daftar SO</td>
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
								<TD style="WIDTH: 328px; HEIGHT: 17px" width="328">&nbsp;<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 17px" width="20%">
									Produk</TD>
								<TD style="HEIGHT: 17px" width="1%"><STRONG>:</STRONG></TD>
								<TD style="HEIGHT: 17px" width="20%">
									<asp:dropdownlisT style="Z-INDEX: 0" id="ddlProductCategory" runat="server" width="130px" AutoPostBack="True"></asp:dropdownlisT></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblAllocation" runat="server">Permintaan Kirim</asp:label></TD>
								<TD>:</TD>
								<TD style="WIDTH: 328px">
									<table>
										<tr>
											<td><cc1:inticalendar id="icListPO1" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>&nbsp;s.d&nbsp;</td>
											<td><cc1:inticalendar id="icListPO2" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
								<TD class="titleField">
									<asp:label id="lblSalesOrg" runat="server" Font-Bold="True" style="Z-INDEX: 0"> Kategori</asp:label></TD>
								<TD>
									<asp:label id="lblOrdertypeColon" runat="server" Font-Bold="True">:</asp:label></TD>
								<TD>
									<asp:dropdownlist id="ddlSalesOrg" runat="server" Width="140px" style="Z-INDEX: 0"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField">
									<asp:label id="lblTEOP" runat="server" Font-Bold="True">Cara Pembayaran</asp:label></TD>
								<TD>
									<asp:label id="lblTEOPColon" runat="server" Font-Bold="True">:</asp:label></TD>
								<TD style="WIDTH: 328px">
									<asp:dropdownlist id="ddlTermOfPayment" runat="server" Width="88px">
										<asp:ListItem Value="Silahkan Pilih">Silahkan Pilih</asp:ListItem>
										<asp:ListItem Value="TOP">TOP</asp:ListItem>
										<asp:ListItem Value="COD">COD</asp:ListItem>
										<asp:ListItem Value="RTGS">RTGS</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD class="titleField">
									<asp:label id="lblOrdertype" runat="server" Font-Bold="True" style="Z-INDEX: 0">Jenis Order</asp:label></TD>
								<TD>:</TD>
								<TD>
									<asp:dropdownlist id="ddlOrderType" runat="server" Width="140px" style="Z-INDEX: 0"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 28px">
									<asp:label id="Label1" runat="server" style="Z-INDEX: 0"> No Debit Charge</asp:label></TD>
								<TD style="HEIGHT: 28px">:</TD>
								<TD style="WIDTH: 328px; HEIGHT: 28px">
									<asp:textbox onkeypress="return alphaNumericPlusUniv(event)" id="txtNoDebitCharge" onblur="return alphaNumericPlusBlur(txtDealerPO)"
										runat="server" MaxLength="20" Width="140px" style="Z-INDEX: 0"></asp:textbox></TD>
								<TD class="titleField" style="HEIGHT: 28px">
									<asp:label id="Label7" runat="server" style="Z-INDEX: 0"> Nomor SO</asp:label></TD>
								<TD style="HEIGHT: 28px">:</TD>
								<TD style="HEIGHT: 28px">
									<asp:textbox onkeypress="return alphaNumericPlusUniv(event)" id="txtNoSO" onblur="return alphaNumericPlusBlur(txtDealerPO)"
										runat="server" MaxLength="20" Width="140px" style="Z-INDEX: 0"></asp:textbox></TD>
							</TR>
							<TR>
                                <TD class="titleField" style="HEIGHT: 28px">
									<asp:label id="Label3" runat="server" Font-Bold="True">Total Quantity</asp:label></TD>
								<TD style="HEIGHT: 28px">:</TD>
								<TD style="WIDTH: 328px; HEIGHT: 28px">
									<table id="Table3" cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td></td>
											<td></td>
											<td></td>
										</tr>
									</table>
									<B>
										<asp:label id="lblQuantity" runat="server" Font-Bold="True"></asp:label></B></TD>
								<TD style="HEIGHT: 24px">
									<asp:label id="lblDealerPO" runat="server" style="Z-INDEX: 0"> Nomor PO</asp:label></TD>
								<TD style="HEIGHT: 24px">
									<asp:label id="lblNoRegPOColon" runat="server" Font-Bold="True">:</asp:label></TD>
								<TD style="HEIGHT: 24px">
									<asp:textbox onkeypress="return alphaNumericPlusUniv(event)" id="txtDealerPO" onblur="return alphaNumericPlusBlur(txtDealerPO)"
										runat="server" MaxLength="20" Width="140px" style="Z-INDEX: 0"></asp:textbox></TD>
							</TR>
							<TR>
                                <TD style="HEIGHT: 24px">
									<asp:label id="lblTotalHargaTebus" runat="server" Font-Bold="True">Total Harga Tebus</asp:label></TD>
								<TD style="HEIGHT: 24px">:</TD>
								<TD style="WIDTH: 328px; HEIGHT: 24px"><b><B>
											<asp:label id="lblJmlHargaTebus" runat="server" Font-Bold="True"></asp:label></B></b></TD>
								
								<TD style="HEIGHT: 24px">
									<asp:label id="lblNoRegPO" runat="server" Font-Bold="True" style="Z-INDEX: 0"> Nomor Reg PO</asp:label></TD>
								<TD style="HEIGHT: 24px">
									<asp:label id="lblFactoringColon" runat="server" Font-Bold="True">:</asp:label></TD>
								<TD style="HEIGHT: 24px">
									<asp:textbox onkeypress="return alphaNumericPlusUniv(event)" id="txtNoRegPO" onblur="return alphaNumericPlusBlur(txtDealerPO)"
										runat="server" MaxLength="20" Width="140px" style="Z-INDEX: 0"></asp:textbox></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD style="WIDTH: 328px"></TD>
								<TD>
									<asp:label id="lblFactoring" runat="server" Font-Bold="True" style="Z-INDEX: 0">Factoring</asp:label></TD>
								<TD><STRONG>:</STRONG></TD>
								<TD>&nbsp;
									<asp:dropdownlist id="ddlFactoring" runat="server" Width="140px" style="Z-INDEX: 0"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD style="WIDTH: 328px"></TD>
								<TD></TD>
								<TD></TD>
								<TD>
									<asp:button id="btnCari" runat="server" Width="60px" Text="Cari" style="Z-INDEX: 0"></asp:button>
									<asp:button id="btnDownload" runat="server" Width="75px" Text="Download" style="Z-INDEX: 0"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<div id="div1" style="HEIGHT: 320px; OVERFLOW: auto"><asp:datagrid id="dtgListPO" runat="server" Width="100%" AllowSorting="True" OnItemCommand="dtgListPO_Edit"
								AutoGenerateColumns="False" OnItemDataBound="dtgListPO_ItemDataBound" AllowCustomPaging="True" PageSize="50" OnPageIndexChanged="dtgListPO_PageIndexChanged"
								AllowPaging="True" BackColor="#CDCDCD" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3">
								<SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
								<EditItemStyle VerticalAlign="Top"></EditItemStyle>
								<AlternatingItemStyle VerticalAlign="Top" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle VerticalAlign="Top" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" BackColor="Teal"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" SortExpression="Status" HeaderText="Status">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="POHeader.ContractHeader.Dealer.DealerCode" HeaderText="Dealer">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="TextBox1" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" SortExpression="POHeader.Dealer.CreditAccount" HeaderText="Credit Account">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="SONumber" HeaderText="Nomor SO">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn SortExpression="LogisticDCHeader.DebitChargeNo" HeaderText="No Debit Charge">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="POHeader.PONumber" HeaderText="No Reg PO">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="POHeader.DealerPONumber" HeaderText="Nomor PO">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="POHeader.ReqAllocationDateTime" HeaderText="Tanggal Permintaan Kirim">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="POHeader.ContractHeader.ProjectName" HeaderText="Nama Pesanan Khusus">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" SortExpression="CreatedTime" HeaderText="Tanggal Pengajuan">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" SortExpression="POHeader.ContractHeader.ContractNumber" HeaderText="Nomor OC">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="POHeader.ContractHeader.Category.CategoryCode" HeaderText="Kategori">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="POHeader.POType" HeaderText="Jenis Order">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="POHeader.TermOfPayment.ID" HeaderText="Cara Pembayaran">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn HeaderText="Harga SO (Rp)">
										<HeaderStyle HorizontalAlign="Center" ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn HeaderText="Total Biaya Kirim Incl PPN (Rp)">
										<HeaderStyle HorizontalAlign="Center" ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Harga Tebus (Rp)">
										<HeaderStyle HorizontalAlign="Center" ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Factoring">
										<HeaderStyle HorizontalAlign="Center" ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Pembayaran Tunai">
										<HeaderStyle HorizontalAlign="Center" ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Sisa Pembayaran">
										<HeaderStyle HorizontalAlign="Center" ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn Visible="false" HeaderText="Download">
										<HeaderStyle ForeColor="White" Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnFileName" runat="server" CommandName="Download">
												<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
											<asp:Label id="lblString" runat="server" Visible="True"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                   <asp:TemplateColumn HeaderText ="Lihat Debit Charge">
										<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnLihatdc" runat="server" CommandName="ViewDC">
												<img src="../images/detail.gif" alt="Lihat Detail DC" border="0" style="cursor:hand"></asp:LinkButton>&nbsp;&nbsp;
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText ="Lihat SO">
										<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnLihat" runat="server" CommandName="View">
												<img src="../images/detail.gif" alt="Lihat Detail SO" border="0" style="cursor:hand"></asp:LinkButton>&nbsp;&nbsp;
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
											<asp:LinkButton id="lbtnDelete" CausesValidation="False" Runat="server" text="Hapus" CommandName="Delete"
												ToolTip="Hapus"  >
												<img border="0" src="../images/trash.gif" alt="Hapus" style="cursor:hand"></asp:LinkButton>
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
                                    <asp:TemplateColumn Visible="false" HeaderText="DownloadDC">
										<HeaderStyle ForeColor="White" Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnFileNameDC" runat="server" CommandName="DownloadDC">
												<img src="../images/download.gif" border="0" alt="DownloadDC"></asp:LinkButton>
											<asp:Label id="lblStringDC" runat="server" Visible="True"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn Visible="false" HeaderText="Sales Order Interest">
										<HeaderStyle ForeColor="White" Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblSOInterestID" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
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
