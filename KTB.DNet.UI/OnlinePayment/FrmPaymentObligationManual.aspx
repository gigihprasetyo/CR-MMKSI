<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPaymentObligationManual.aspx.vb" Inherits="FrmPaymentObligationManual" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="Intimedia.WebCC" Assembly="Intimedia.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPaymentObligation</title>
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
			showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var tempParam = selectedDealer.split(';');
			var txtDealer = document.getElementById("txtKodeDealer");
			txtDealer.value = tempParam[0];				
		}
		function toggleDetail(elm){
				elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.nextSibling.style.display = 
					elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.nextSibling.style.display == 'none'?'block':'none'												
				
				if(elm.childNodes[2].style.display == 'none'){
					elm.childNodes[2].style.display = 'block';										
				}
				else{
					elm.childNodes[2].style.display = 'none';					
					
				}
				
				if(elm.childNodes[0].style.display == 'none'){
					elm.childNodes[0].style.display = 'block';
				}
				else{					
					elm.childNodes[0].style.display = 'none';
				}
				
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
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Payment Online&nbsp;-&nbsp; Payment Obligation Manual</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" width="15%">Dealer</TD>
								<TD class="titleField" width="1%">:</TD>
								<TD width="20%"><asp:textbox id="txtKodeDealer" runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD class="titleField" width="15%">Tanggal Kirim</TD>
								<TD class="titleField" width="1%">:</TD>
								<TD width="20%">
									<table id="tblDocDate" cellpadding="0" cellspacing="0">
										<tr>
											<td><cc1:inticalendar id="icFromDocDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td class="titleField" width="5%">
												s/d
											</td>
											<td><cc1:inticalendar id="icToDocDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" width="15%">Assignment</TD>
								<TD class="titleField" width="1%">:</TD>
								<TD width="20%"><asp:textbox id="txtAssignment" runat="server"></asp:textbox></TD>
								<TD class="titleField" width="15%"></TD>
								<TD class="titleField" width="1%"></TD>
								<TD width="20%"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="15%">Status</TD>
								<TD class="titleField" width="1%">:</TD>
								<TD width="20%"><asp:dropdownlist id="ddlStatus" runat="server" Width="120px"></asp:dropdownlist></TD>
								<TD class="titleField" width="15%">Pembayaran</TD>
								<TD class="titleField" width="1%">:</TD>
								<TD width="20%">
									<asp:dropdownlist id="ddlPaymentType" runat="server" Width="120px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD colSpan="6"></TD>
							</TR>
							<TR>
								<TD colSpan="2"></TD>
								<TD colSpan="4"><asp:button id="btnCari" Runat="server" Text="Cari" Width="104px"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" colSpan="2">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 240px">
							<asp:datalist id="listParrent" Width="100%" Runat="server" CellSpacing="1" CellPadding="3" BorderColor="Gainsboro"
								BackColor="#CDCDCD" BorderWidth="0px">
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<HeaderTemplate>
									<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
															document.forms[0].chkAllItems.checked)" />
					</TD>
					<td style="color:#F7F7F7; BACKGROUND-color:#4A3C8C" align="center" valign="center">
						No
					</td>
					<td style="color:#F7F7F7; BACKGROUND-color:#4A3C8C" align="center" valign="center">
						Status
					</td>
					<td style="color:#F7F7F7; BACKGROUND-color:#4A3C8C" align="center" valign="center">
						Dealer
					</td>
					<td style="color:#F7F7F7; BACKGROUND-color:#4A3C8C" align="center" valign="center">
						Assignment
					</td>
					<td style="color:#F7F7F7; BACKGROUND-color:#4A3C8C" align="center" valign="center">
						Tipe Pembayaran
					</td>
					<td style="color:#F7F7F7; BACKGROUND-color:#4A3C8C" align="center" valign="center">
						Jumlah Tagihan (Rp)
					</td>
					</HeaderTemplate>
					<ItemTemplate>
										<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
									</td>
									<td style="color:Black; BACKGROUND-color:#F1F6FB" align="left">
							<table cellspacing="0">
								<tr>
									<td>
										<asp:Label ID="Label3" Runat="server" Font-Bold="True" onclick="toggleDetail(this)">
											<img src="../images/plus.gif"> <img style="display:none" src="../images/minus.gif">
										</asp:Label>
									</td>
									<td><%# DataBinder.Eval(Container, "ItemIndex") + 1 %></td>
								</tr>
							</table>
						</td>
									<td style="color:Black; BACKGROUND-color:#F1F6FB" align="center">
							<asp:Label id="lblStatusP" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StatusDesc") %>'>
							</asp:Label>
						</td>
									<td style="color:Black; BACKGROUND-color:#F1F6FB" align="right">
							<asp:Label id="lblDealerP" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
							</asp:Label>
						</td>
									<td style="color:Black; BACKGROUND-color:#F1F6FB" align="right">
							<asp:Label id="lblAssignmentP" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Assignment") %>'>
							</asp:Label>
						</td>		
									<td style="color:Black; BACKGROUND-color:#F1F6FB" align="center">
							<asp:Label id="lblDescriptionP" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.isTOPDesc") %>'>
							</asp:Label>
						</td>
									<td style="color:Black; BACKGROUND-color:#F1F6FB" align="right">
							<asp:Label id="lblAmountP" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.TotalAmount"),"###,##0") %>'>
							</asp:Label>
						</td>
				</TR>
				<tr style="display:none">
					<td></td>
					<td colspan="6">
						<asp:datagrid id="dtgListPaymentObligation" runat="server" Width="100%" AllowCustomPaging="False"
							AllowPaging="False" PageSize="25" DataKeyField="ID" AutoGenerateColumns="False" AllowSorting="True"
							CellSpacing="1" CellPadding="3" BorderColor="Gainsboro" BackColor="#CDCDCD" BorderWidth="0px">
							<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="lblNoD" Runat="server" text='<%# DataBinder.Eval(Container, "ItemIndex") + 1 %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Type">
									<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblDescriptionD" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PaymentObligationType.Code") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tanggal Jatuh Tempo">
									<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblDueDateD" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.DueDate"),"dd/MM/yyyy") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tagihan (Rp)">
									<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblAmountD" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.Amount"),"###,##0") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid>
					</td>
				</tr>
				</ItemTemplate> </asp:datalist></DIV></TD></TR>
				<TR>
					<TD class="titleField" vAlign="top"></TD>
				</TR>
				<TR>
					<TD height="60"><asp:button id="btnProcess" runat="server" Text="Validasi"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
