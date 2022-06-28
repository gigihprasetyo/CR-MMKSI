<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListPO.aspx.vb" Inherits="FrmListPO"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		function ShowPODetail(POID)
		{
			showPopUp('../PopUp/PopUpSPPODetail.aspx?poid=' + POID, '', 510, 700, null);
		}
		
		

		function ShowPPDealerSelection()
		{
			showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			//var tempParam = selectedDealer.split(';');
			var txtDealer = document.getElementById("txtDealerCode");
			txtDealer.value = selectedDealer;				
		}
		
		
		function PONOSelection(POSelection)
		{
			//alert(POSelection);
			//var tempParam = selectedDealer.split(';');
			var txtNoPO= document.getElementById("txtNoPO");
			txtNoPO.value = POSelection;				
		}
		
		function CheckAll(aspCheckBoxID, checkVal) 
			{
				re = new RegExp(':' + aspCheckBoxID + '$')  
				for(i = 0; i < document.forms[0].elements.length; i++) {
					elm = document.forms[0].elements[i]
					if (elm.type == 'checkbox') {
						if (re.test(elm.name)) {
						elm.checked = checkVal
						}
					}
				}
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table11" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td colSpan="6">
						<TABLE id="Table01" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" colSpan="6">INDENT PART - Daftar PO Indent Part</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" colSpan="6"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td colSpan="6"><IMG height="10" src="../images/blank.gif" border="0"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD class="titleField" vAlign="top" width="15%">Kode Dealer</TD>
					<TD vAlign="top" width="1%">:</TD>
					<td vAlign="top" width="25%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
							runat="server" Width="160px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup" onclick="ShowPPDealerSelection();"></asp:label></td>
					<td class="titleField" vAlign="top" width="15%">Tanggal PO</td>
					<td vAlign="top" width="1%">:</td>
					<td width="50%">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td><cc1:inticalendar id="icPODateFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
								<td>&nbsp;s/d&nbsp;</td>
								<td><cc1:inticalendar id="icPODateUntil" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
							</tr>
						</table>
					</td>
				</TR>
				<TR>
					<!-- remark by Ery
					<TD class="titleField" style="HEIGHT: 16px" width="15%">Tipe Barang</TD>
					<TD style="HEIGHT: 16px" width="1%">:</TD>
					<td style="HEIGHT: 16px" width="25%"><asp:dropdownlist id="ddlTipeBarang" runat="server" Width="96px"></asp:dropdownlist></td>
					 <td class="titleField" width="15%">Tipe Barang</td>
					<td width="1%">:</td>
					<td width="50%"><asp:dropdownlist id="ddlMaterialType" runat="server" Width="160px"></asp:dropdownlist></td> 
					<td class="titleField" style="HEIGHT: 16px" width="15%">Nomor PO</td>
					<td style="HEIGHT: 16px" width="1%">:</td>
					<td style="HEIGHT: 16px" width="50%"><asp:textbox id="txtNoPO" runat="server" Width="184px"></asp:textbox></td>--></TR>
				<TR>
					<TD class="titleField" vAlign="top" width="15%">Nomor PO</TD>
					<TD vAlign="top" width="1%">:</TD>
					<td width="25%">
						<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtPONumber" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
							runat="server" Width="160px" MaxLength="15"></asp:textbox></td>
					<td class="titleField" vAlign="top" width="15%">Status Transfer</td>
					<td vAlign="top" width="1%">:</td>
					<td class="titleField" vAlign="top" width="50%">
						<asp:dropdownlist id="ddlTransferStatus" runat="server" Width="160px"></asp:dropdownlist></td>
				</TR>
				<TR>
					<TD class="titleField" vAlign="top" width="15%">Nomor Pengajuan</TD>
					<TD vAlign="top" width="1%">:</TD>
					<td width="25%">
						<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="TxtRequestNo" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
							runat="server" Width="160px" MaxLength="15"></asp:textbox></td>
					<td class="titleField" vAlign="top" width="15%"></td>
					<td vAlign="top" width="1%"></td>
					<td class="titleField" vAlign="top" width="50%"></td>
				</TR>
                <tr>
                    <td class="titleField">Cara Pembayaran</td>
                    <td width="1%">:</td>
                    <td>
                        <asp:DropDownList ID="ddlTermOfPayment" runat="server" Width="160px" Visible="True"></asp:DropDownList>
                    </td>
                </tr>
				<TR>
					<TD class="titleField" width="15%"></TD>
					<TD width="1%"></TD>
					<td colSpan="4"><asp:button id="btnSearch" runat="server" Text="Cari" width="60px"></asp:button></td>
				</TR>
				<TR>
					<TD vAlign="top" colSpan="6">
						<div id="div1" style="HEIGHT: 340px; OVERFLOW: auto"><asp:datagrid id="dgPO" runat="server" Width="100%" AllowSorting="True" CellSpacing="1" AutoGenerateColumns="False"
								BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" AllowPaging="True" AllowCustomPaging="True" BackColor="#CDCDCD" CellPadding="3"
								GridLines="None" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
															document.forms[0].chkAllItems.checked)" />
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No.">
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label1" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode") %>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DealerName" HeaderText="Nama Dealer">
										<HeaderStyle Width="40%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label2" Text='<%# DataBinder.Eval(Container, "DataItem.DealerName") %>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="RequestNo" HeaderText="Nomor Pengajuan">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblRequestNo" style="cursor:hand" Font-Underline="True" Text='<%# DataBinder.Eval(Container, "DataItem.RequestNo") %>' runat="server" >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PONumber" HeaderText="Nomor PO">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNoPO" Font-Underline="True" Text='<%# DataBinder.Eval(Container, "DataItem.PONumber") %>' runat="server" style="cursor:hand">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="PODate" SortExpression="PODate" HeaderText="Tanggal PO" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="TOPDescription" HeaderText="Cara Pembayaran">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblTOPDescription" Text='<%# DataBinder.Eval(Container, "DataItem.TOPDescription")%>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="IndentTransferDesc" SortExpression="IndentTransfer" HeaderText="Status Transfer">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<td align="left" colSpan="6"><asp:button id="btnSubmit" runat="server" Width="133px" Text="Transfer ke SAP"></asp:button>
						<asp:button id="Button1" runat="server" Width="133px" Text="Transfer Ulang ke SAP"></asp:button></td>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
