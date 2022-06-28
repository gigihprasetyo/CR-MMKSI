<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSparePartCeiling.aspx.vb" Inherits="FrmSparePartCeiling"  smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DEPOSIT - Daftar Deposit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			function ShowPPCreditAcctSelection()
			{
				//showPopUp('../SparePart/../PopUp/PopUpCreditAccount.aspx','',500,760,CreditAcctSelection);
				//showPopUp('../PopUp/PopUpCreditAccountSelection.aspx','',500,760,CreditAcctSelection);
				showPopUp('../General/../PopUp/PopUpCreditAccountSelection2.aspx','',500,760,AccountSelection2);
			}
			
			function AccountSelection2(selectedAccount)
			{
				var tempParam= selectedAccount;
				var txtCreditAcct = document.getElementById("txtCreditAcct");
				txtCreditAcct.value = selectedAccount;	
			}
			
			function CreditAcctSelection(SelectedAcct)
			{
				var txtCreditAcct = document.getElementById("txtCreditAcct");
				var lblDealerName = document.getElementById("lblDealerName");
				var txtDealerName = document.getElementById("txtDealerName");
				var str = SelectedAcct.split(";");
				txtCreditAcct.value = str[0];			
				lblDealerName.innerHTML=str[1];
				txtDealerName.value=str[1];
			}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 8px">DEPOSIT&nbsp;- Spare Part Ceiling</td>
				</tr>
				<tr>
					<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 8px" height="8"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="25%"><asp:label id="lblCreditAcct" runat="server">Credit Account</asp:label></TD>
								<TD width="1%">:</TD>
								<TD><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtCreditAcct"  
										onblur="omitSomeCharacter('txtCreditAcct','<>?*%$')" runat="server"></asp:textbox><asp:label id="lblSearchCreditAcct" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="25%"><asp:label id="lblName" runat="server" Font-Bold="True">Nama Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="lblColon2" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblDealerName" runat="server" Width="224px"></asp:label><INPUT 
            id=txtDealerName type=hidden name=txtDealerName value="<%= lblDealerName.Text %>"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="25%"></TD>
								<TD width="1%"></TD>
								<TD><asp:button id="btnSearch" runat="server" Width="60px" Text=" Cari " Height="22px"></asp:button></TD>
							</TR>
							<tr>
								<td colspan="3">&nbsp;</td>
							</tr>
							<TR>
								<TD vAlign="top" colSpan="6">
									<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 320px"><asp:datagrid id="dgDepositList" runat="server" Width="100%" PageSize="25" AllowPaging="True"
											AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px" CellPadding="3"
											AllowSorting="True">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblNo" runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
													<HeaderStyle Width="4%" CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CreditAccount" ReadOnly="True" HeaderText="Credit Account" SortExpression="CreditAccount">
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Nama Dealer">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblDealerGrid" runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="Ceiling" SortExpression="Ceiling" ReadOnly="True" HeaderText="Spare Part Ceiling"
													DataFormatString="{0:#,##0}">
													<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CeilingBalance" SortExpression="CeilingBalance" ReadOnly="True" HeaderText="Ceiling Balance"
													DataFormatString="{0:#,##0}">
													<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></DIV>
								</TD>
							</TR>
						</TABLE>
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
