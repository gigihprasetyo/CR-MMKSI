<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanUniformInvoice.aspx.vb" Inherits="FrmSalesmanUniformInvoice" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSalesmanUniformInvoice</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			/* Deddy H	validasi value *********************************** */
			function ShowPPSalesmanUniformOrderSelection()
			{
				var passParam="";
				var txtGetDealer = document.getElementById("txtGetDealer");
				if (txtGetDealer.value == "KTB" || txtGetDealer.value == "MKS")
				{
					var txtDealer = document.getElementById("txtDealerCode");
					passParam = txtDealer.value;
				}
				else
				{
					var ltrlDealerCode = document.getElementById("ltrlDealerCode");
					passParam = ltrlDealerCode.innerHTML;
				}
				showPopUp('../PopUp/PopUpSalesmanUniformOrderSelection.aspx?DealerCode='+passParam,'',500,760,SalesmanUniformOrderSelection);
				
			}
			function SalesmanUniformOrderSelection(selectedVal)
			{
				var txtNoOrder = document.getElementById("txtNoOrder");
				//var arrValue = selectedVal.split(';');
				txtNoOrder.value = selectedVal;
			}
			function ShowPPDealerSelection()
			{
				showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var txtDealer = document.getElementById("txtDealerCode");
				txtDealer.value = selectedDealer;
			}
			function CheckAll(aspCheckBoxID, checkVal) 
			{
				re = new RegExp(':' + aspCheckBoxID + '$')  
				for(i = 0; i < document.forms[0].elements.length; i++) {
					elm = document.forms[0].elements[i]
					if (elm.type == 'checkbox' && !elm.disabled){
						if (re.test(elm.name)) {
						elm.checked = checkVal;
						}
					}
				}
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">SERAGAM TENAGA PENJUAL - Rekap Pesanan</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</TABLE>
			<table style="WIDTH: 728px; HEIGHT: 117px" DESIGNTIMEDRAGDROP="867">
				<tr>
					<td class="titleField" style="HEIGHT: 20px" width="24%">Kode Dealer</td>
					<TD style="HEIGHT: 20px" width="1%">:</TD>
					<td style="HEIGHT: 20px" width="75%"><asp:label id="ltrlDealerCode" runat="server"></asp:label><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitCharsOnCompsTxt(this,'<>?*%$')"
							runat="server" Width="191px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
							<img style="cursor:hand" alt="Klik Disini untuk memilih Nomor Permintaan" src="../images/popup.gif"
								border="0"></asp:label></td>
				</tr>
				<tr>
					<td class="titleField" style="HEIGHT: 20px" width="24%">Kode Pesanan</td>
					<TD style="HEIGHT: 20px" width="1%">:</TD>
					<td style="HEIGHT: 20px" width="75%"><asp:dropdownlist id="ddlKodePSeragam" runat="server"></asp:dropdownlist></td>
				</tr>
				<TR>
					<TD class="titleField" style="WIDTH: 130px"><FONT color="crimson"><FONT color="#000000">No. 
								Order</FONT></FONT></TD>
					<TD>:</TD>
					<td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtNoOrder" onblur="omitSomeCharacter('txtProgress','<>?*%$;')"
							runat="server" Width="208px" MaxLength="100"></asp:textbox><asp:label id="lblPopUpOrderNo" runat="server" width="16px">
							<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></td>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 130px">
						<asp:CheckBox id="chkNoInvoice" runat="server" Text="No. Kwitansi"></asp:CheckBox></TD>
					<TD>:</TD>
					<TD>
						<asp:TextBox id="txtNoInvoice" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 16px"><FONT color="crimson"><FONT color="#000000">Grand 
								Total Total Harga</FONT></FONT></TD>
					<TD style="HEIGHT: 16px">:</TD>
					<TD style="HEIGHT: 16px">Rp.
						<asp:label id="lblGrandTotalTHarga" runat="server" CssClass="textRight">0</asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 16px"><FONT color="crimson"><FONT color="#000000"><FONT color="crimson"><FONT color="#000000"><FONT color="crimson"><FONT color="#000000">Grand 
												Total Total Harga + PPN</FONT></FONT></FONT></FONT></FONT></FONT></TD>
					<TD style="HEIGHT: 16px">:</TD>
					<TD style="HEIGHT: 16px">Rp.
						<asp:label id="lblGrandTotalTHargaPPN" runat="server" CssClass="textRight">0</asp:label></TD>
				</TR>
				<tr>
					<td></td>
					<td></td>
					<td><asp:button id="btnCari" runat="server" Width="60px" Text="Cari"></asp:button><asp:button id="btnDownloadExcel" Text="Download Excel" Runat="server"></asp:button><INPUT id="txtGetDealer" type="hidden" runat="server"></td>
				</tr>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD><asp:linkbutton id="lbtnPrintInvoice" runat="server" Width="20px" Text="Generate &amp; Print Invoice"
							CommandName="Print" CausesValidation="False">
							<img src="../images/print.gif" border="0" alt="Print"></asp:linkbutton></TD>
				</TR>
				<TR>
					<TD colSpan="3"><asp:datagrid id="dtgKwitansi" runat="server" Width="100%" ShowFooter="True" PageSize="25" AllowPaging="True"
							AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None"
							BorderWidth="0px" BackColor="#E0E0E0" CellPadding="3" GridLines="Horizontal" CellSpacing="1">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle ForeColor="White" BorderColor="White" BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle ForeColor="White" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="WhiteSmoke"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False">
									<HeaderStyle Width="5%" CssClass="titleTableRsd"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkSelection',
														document.forms[0].chkAllItems.checked)" />
									</HeaderTemplate>
									<ItemTemplate>
										<asp:CheckBox ID="chkSelection" Runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Invoice No.">
									<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblInvoiceNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.InvoiceNo") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="SalesmanUnifDistribution.SalesmanUnifDistributionCode" HeaderText="Kode Pesanan">
									<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblKodePSeragam" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanUnifDistribution.SalesmanUnifDistributionCode") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="SalesmanUnifDistribution.Description" HeaderText="Deskripsi">
									<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblDeskripsi" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanUnifDistribution.Description") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="OrderNumber" HeaderText="No Order">
									<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblNoOrder" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OrderNumber") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Total Harga">
									<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblTotalHarga" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalHarga") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<b>Total:</b>
										<asp:Label ID="lblSumTotalHarga" Runat="server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="PPN">
									<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblPPN" runat="server">10%</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Total Harga + PPN">
									<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblTotalHargaPPN" runat="server"></asp:Label>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<b>Total:</b>
										<asp:Label ID="lblSumTotalHargaPPN" Runat="server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False">
									<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnPrint" runat="server" Width="20px" Text="Print" CausesValidation="False"
											CommandName="Print">
											<img src="../images/print.gif" border="0" alt="Print"></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderText="ID">
									<ItemTemplate>
										<asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#404040" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
			</table>
			<table cellSpacing="1" cellPadding="0" width="100%">
				<tr>
					<td>
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 360px" DESIGNTIMEDRAGDROP="95"></DIV>
					</td>
				</tr>
				<tr height="40">
					<td align="center"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
