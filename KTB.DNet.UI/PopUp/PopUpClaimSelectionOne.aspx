<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpClaimSelectionOne.aspx.vb" Inherits="PopUpClaimSelectionOne" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpClaimSelectionOne</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory','',500,760,DealerSelection);
			}
						
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer.split(';');
				var txtDealerSelection = document.getElementById("txtDealerCode");
				txtDealerSelection.value = tempParam[0];
			}
			
			function getQueryVariable(variable)
			{
				var query = window.location.search.substring(1);
				var vars = query.split("&");
				for (var i=0;i<vars.length;i++)
				{	
					var pair = vars[i].split("=");
					if (pair[0] == variable)
					{
						return pair[1];
					}
				}
				return "nothing";
				
			}
		
			function GetSelectedClaim()
			{
				var table;
				var bcheck =false;
				table = document.getElementById("dtgListClaim");
				var Claim ='';
				for (i = 1; i < table.rows.length; i++)
				{
			
				var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (radioBtn != null && radioBtn.checked)			
					{
						if(navigator.appName == "Microsoft Internet Explorer")
						{	
					
							if(getQueryVariable("x") == "Territory")
							{	
								Claim = replace(table.rows[i].cells[1].innerText,' ','') + ';' + table.rows[i].cells[2].innerText + ';' + 
										replace(table.rows[i].cells[5].innerText,' ','')+ ';' + replace(table.rows[i].cells[6].innerText,' ','')+ ';' +
										table.rows[i].cells[6].getElementsByTagName("INPUT")[0].value + ';' +
										table.rows[i].cells[6].getElementsByTagName("INPUT")[1].value;
							}
							else
							{	Claim = replace(table.rows[i].cells[1].innerText,' ','') + ';' + table.rows[i].cells[2].innerText + ';' +
										table.rows[i].cells[6].getElementsByTagName("INPUT")[0].value + ';' +
										table.rows[i].cells[6].getElementsByTagName("INPUT")[1].value ;
							}
							
						window.returnValue = Claim;
						//alert(Claim);
						bcheck=true;
						}
						else if (navigator.appName == "Netscape") {

						    if (getQueryVariable("x") == "Territory") {
						        Claim = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText + ';' +
										replace(table.rows[i].cells[5].innerText, ' ', '') + ';' + replace(table.rows[i].cells[6].innerText, ' ', '') + ';' +
										table.rows[i].cells[6].getElementsByTagName("INPUT")[0].value + ';' +
										table.rows[i].cells[6].getElementsByTagName("INPUT")[1].value;
						    }
						    else {
						        Claim = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText + ';' +
										table.rows[i].cells[6].getElementsByTagName("INPUT")[0].value + ';' +
										table.rows[i].cells[6].getElementsByTagName("INPUT")[1].value;
						    }

						    opener.dialogWin.returnFunc(Claim);
						    //alert(Claim);
						    bcheck = true;
						}
						else
						{
							if(getQueryVariable("x") == "Territory")
							{	Claim = replace(table.rows[i].cells[1].innerHTML,' ','') + ';' + replace(table.rows[i].cells[2].innerHTML,' ','') + ';' + 
										replace(table.rows[i].cells[5].innerHTML,' ','') + ';' + table.rows[i].cells[6].getElementsByTagName("INPUT")[0].value + ';' +
										table.rows[i].cells[6].getElementsByTagName("INPUT")[1].value ;
							}
							else
							{	Claim = replace(table.rows[i].cells[1].innerHTML,' ','') + ';' + 
										replace(table.rows[i].cells[2].innerHTML,' ','') + ';' + 
										table.rows[i].cells[6].getElementsByTagName("INPUT")[0].value + ';' +
										table.rows[i].cells[6].getElementsByTagName("INPUT")[1].value ;
							}						
							opener.dialogWin.returnFunc(Claim);
							bcheck=true;
						}
						break;
					}
				}
			
				if (bcheck)
				{
					window.close();
				}
				else
				{
					alert("Silahkan Pilih Claim ");	
				}
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD class="titleField" width="20%"><asp:label id="Label1" runat="server">Kode</asp:label>Dealer</TD>
					<td width="1%">:</td>
					<TD width="40%"><asp:textbox id="txtDealerCode" runat="server" Width="152px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
					<td class="titleField" width="15%">Nomor Claim</td>
					<td width="1%">:</td>
					<td width="30%"><asp:textbox id="txtClaimNo" runat="server" Width="152px"></asp:textbox></td>
				</TR>
				<TR>
					<TD class="titleField" width="20%">&nbsp;
						<asp:checkbox id="chkClaimDate" runat="server" Text="Tanggal Claim" Checked="True"></asp:checkbox></TD>
					<td>:</td>
					<TD style="WIDTH: 344px">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td>
									<cc1:inticalendar id="icClaimDateFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
								<td>&nbsp;s/d&nbsp;</td>
								<td>
									<cc1:inticalendar id="icClaimDateUntil" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
							</tr>
						</table>
					<td class="titleField" style="WIDTH: 96px">Status</td>
					<td style="WIDTH: 7px">:</td>
					<td><asp:dropdownlist id="ddlStatus" runat="server" Width="140px"></asp:dropdownlist></td>
				</TR>
				<TR>
					<TD class="titleField" width="20%"></TD>
					<td></td>
					<TD style="WIDTH: 344px"><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button></TD>
					<td style="WIDTH: 96px"></td>
					<td style="WIDTH: 7px"></td>
					<td></td>
				</TR>
				<tr>
					<td align="center" colSpan="6">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dtgListClaim" runat="server" Width="100%" BorderColor="Gainsboro" CellPadding="3"
								BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" BorderWidth="0px" CellSpacing="1" DataKeyField="ID" AllowPaging="True"
								AllowCustomPaging="True">
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											&nbsp;Pilih
										</HeaderTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ClaimNo" SortExpression="ClaimNo" HeaderText="No. Claim">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ClaimDate" SortExpression="ClaimDate" HeaderText="Tgl. Claim" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Total Claim">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblTotal" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartPOStatus.BillingNumber" HeaderText="No. Faktur">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNoFaktur" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPOStatus.BillingNumber") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartPOStatus.BillingDate" HeaderText="Tgl. Faktur">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblTglFaktur" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.SparePartPOStatus.BillingDate"),"dd/MM/yyyy") %>'>
											</asp:Label>
											<asp:Textbox ID="txtDealerCode1" style="display:none" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Textbox>
											<asp:Textbox ID="txtDealerName1" style="display:none" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
											</asp:Textbox>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</td>
				</tr>
				<tr>
					<td colSpan="7" align="center">&nbsp;<INPUT id="btnChoose" style="WIDTH:60px; HEIGHT:21px" disabled onclick="GetSelectedClaim()"
							type="button" value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px; HEIGHT: 21px" onclick="window.close()" type="button"
							value="Tutup" name="btnCancel"></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
