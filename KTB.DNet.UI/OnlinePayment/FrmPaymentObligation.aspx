<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPaymentObligation.aspx.vb" Inherits="FrmPaymentObligation" smartNavigation="False"%>
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
		function getNextSibling(startBrother){
 				endBrother=startBrother.nextSibling;
 				while(endBrother.nodeType!=1){
   					endBrother = endBrother.nextSibling;
 				} 				
 				return endBrother;
			}
		var isshown = false;
		function toggleDetail(elm){
				if (getNextSibling(elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode).style.display =="none")
				{
					isshown = false;
				}
				if (getNextSibling(elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode).style.display =="")
				{
					isshown = true;
				}
				if(!isshown){										
					getNextSibling(elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode).style.display = "block";
					getNextSibling(elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode).style.display = "";					
					isshown = true;
				}
				else{							
					getNextSibling(elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode).style.display = "none";										
					getNextSibling(elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode).style.display = "";
					getNextSibling(elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode).style.display = "none";
					isshown = false;
				}
								
				if(elm.childNodes[2].tagName == 'IMG'){
					if(elm.childNodes[2].style.display == 'none'){
						elm.childNodes[2].style.display = 'block';										
					}
					else{
						elm.childNodes[2].style.display = 'none';					
						
					}
				}
				else{
					if(elm.childNodes[3].style.display == 'none'){
						elm.childNodes[3].style.display = 'block';										
					}
					else{
						elm.childNodes[3].style.display = 'none';					
						
					}				
				}
				if(elm.childNodes[0].tagName == 'IMG'){
					if(elm.childNodes[0].style.display == 'none'){
						elm.childNodes[0].style.display = 'block';
					}
					else{					
						elm.childNodes[0].style.display = 'none';
					}
				}
				else{
					if(elm.childNodes[1].style.display == 'none'){
						elm.childNodes[1].style.display = 'block';
					}
					else{					
						elm.childNodes[1].style.display = 'none';
					}				
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
		
		function CalculateTotal(chk){
			/*var rows = chk.parentNode.parentNode.parentNode.rows;
			var total = 0;
			for(var i = 1; i < rows.length; i++){
				if(rows[i].cells[0].childNodes.length > 0){
					if (rows[i].cells[0].childNodes[0].checked == true)
					{
						total += parseFloat(rows[i].cells[6].childNodes[0].innerText.replace(".", "").replace(",", "."));
					}
				}
			}

			var lblTotal = document.getElementById("lblTotal");
			lblTotal.innerHTML = total.toLocaleString();
			lblTotal.innerHTML = replace(lblTotal.innerHTML.substring(0,lblTotal.innerHTML.length-3),',','.');		
			*/
		}
		
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><asp:label id="lblPageTitle" CssClass="titlePage" Runat="server"></asp:label></td>
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
								<TD width="20%"><asp:label id="lblKodeDealer" Runat="server"></asp:label><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$()_-|\/#,{}=+^`~');" id="txtKodeDealer"
										onblur="omitSomeCharacter('txtKodeDealer','<>?\/*%$');" runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label></TD>
								<TD class="titleField" width="15%">Tanggal Kirim</TD>
								<TD class="titleField" width="1%">:</TD>
								<TD width="20%">
									<TABLE id="tblDocDate" cellSpacing="0" cellPadding="0">
										<TR>
											<TD><cc1:inticalendar id="icFromDocDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<TD class="titleField" width="5%">&nbsp;s/d&nbsp;
											</TD>
											<TD><cc1:inticalendar id="icToDocDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" width="15%">Tipe Assignment</TD>
								<TD class="titleField" width="1%">:</TD>
								<TD width="20%"><asp:dropdownlist id="ddlAssignmentType" runat="server" Width="120px"></asp:dropdownlist></TD>
								<TD class="titleField" width="15%">Pembayaran</TD>
								<TD class="titleField" width="1%">:</TD>
								<TD width="20%"><asp:dropdownlist id="ddlPaymentType" runat="server" Width="120px"></asp:dropdownlist></TD>
							</TR>
							<TR style="DISPLAY: none">
								<TD class="titleField" width="15%">Status</TD>
								<TD class="titleField" width="1%">:</TD>
								<TD width="20%"><asp:dropdownlist id="ddlStatus" runat="server" Width="120px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" width="15%">Assignment</TD>
								<TD class="titleField" width="1%">:</TD>
								<TD width="20%"><asp:textbox id="txtAssignment" runat="server"></asp:textbox></TD>
								<TD class="titleField" width="15%">Total</TD>
								<TD class="titleField" width="1%">:</TD>
								<TD width="20%"><asp:label id="lblTotal" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD colSpan="6"></TD>
							</TR>
							<TR>
								<TD colSpan="2"></TD>
								<TD colSpan="4"><asp:button id="btnCari" Runat="server" Width="60px" Text="Cari"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" colSpan="2">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 320px"><asp:datalist id="listParrent" Runat="server" Width="100%" CellSpacing="1" CellPadding="1" BorderColor="Gainsboro"
								BackColor="#ffffff" BorderWidth="0px" DataKeyField="ID">
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" Font-Bold="True" ForeColor="#FFFFFF" BackColor="#F28625"></HeaderStyle>
								<HeaderTemplate>
									<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
															document.forms[0].chkAllItems.checked);CalculateTotal(this)" />
					</TD>
					<td class="titleTableSales" align="center" valign="top">
						No
					</td>
					<td class="titleTableSales" align="center" valign="top">
						Status
					</td>
					<td class="titleTableSales" align="center" valign="top">
						Dealer
					</td>
					<td class="titleTableSales" align="center" valign="top">
						Assignment
					</td>
					<td class="titleTableSales" align="center" valign="top">
						Tgl Kirim
					</td>
					<td class="titleTableSales" align="center" valign="top">
						Tipe Pembayaran
					</td>
					<td class="titleTableSales" align="center" valign="top">
						Jumlah Tagihan (Rp)
					</td>
					<td class="titleTableSales" align="center" valign="top">
						Jumlah Item
					</td>
					</HeaderTemplate>
					<ItemTemplate>
								<asp:CheckBox id="chkItemChecked" runat="server" onclick="CalculateTotal(this)"></asp:CheckBox>
								</td>
								<td class="bottomLine" align="left">
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
								<td class="bottomLine" align="center">
							<asp:Label id="lblStatusP" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StatusDesc") %>'>
							</asp:Label>
						</td>
								<td class="bottomLine" align="left">
							<asp:Label id="lblDealerP" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' tooltip ='<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1") %>'>
							</asp:Label>
						</td>
								<td class="bottomLine" align="left">
							<asp:Label id="lblAssignmentP" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Assignment") %>'>
							</asp:Label>
						</td>
								<td class="bottomLine" align="left">
							<asp:Label id="lblTglKirim" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.DocDate"),"dd/MM/yyyy") %>'>
							</asp:Label>
						</td>
								<td class="bottomLine" align="center">
							<asp:Label id="lblDescriptionP" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.isTOPDesc") %>'>
							</asp:Label>
						</td>
								<td class="bottomLine" align="right">
							<asp:Label id="lblAmountP" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.TotalAmount"),"###,##0") %>'>
							</asp:Label>
						</td>
								<td class="bottomLine" align="center">
							<asp:Label id="Label2" runat="server" Text='<%#  DataBinder.Eval(Container, "DataItem.TotalItem")  %>'>
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
									<HeaderStyle Width="2%" CssClass="titleTableSales2"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="lblNoD" Runat="server" text='<%# DataBinder.Eval(Container, "ItemIndex") + 1 %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Type">
									<HeaderStyle CssClass="titleTableSales2"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblDescriptionD" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PaymentObligationType.Description") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tanggal Jatuh Tempo">
									<HeaderStyle CssClass="titleTableSales2"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblDueDateD" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.DueDate"),"dd/MM/yyyy") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tagihan (Rp)">
									<HeaderStyle CssClass="titleTableSales2"></HeaderStyle>
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
					<TD class="titleField" vAlign="top"><asp:button id="btnProcess" runat="server" Width="56px" Text="Validasi" Visible="False"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
