<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDealerLeadTime.aspx.vb" Inherits="FrmDealerLeadTime" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>frmDealerLeadTime</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		function ShowPPDealerSelectionOne()
		{
			showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory','',500,760,DealerSelectionOne);
		}
		function DealerSelectionOne(selectedDealer)
		{
			selectedDealer = selectedDealer + ";";
			var tempParam = selectedDealer.split(';');
			var txtDealerCode = document.getElementById("txtDealerCode");
			var lblDealerName = document.getElementById("lblDealerName");
			var lblDealerTerm = document.getElementById("lblDealerTerm");
			txtDealerCode.value = tempParam[0];
			lblDealerName.innerHTML = tempParam[1];
			lblDealerTerm.innerHTML = tempParam[3];
			
		}
		function ShowPPDealerSelection()
		{
			showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
			var txtDealerSelection = document.getElementById("txtDealerCode");
			txtDealerSelection.value = selectedDealer;

			var btnGetDealer = document.getElementById("btnGetDealer");
			btnGetDealer.form.submit();
		}
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server" onfocus="return checkModal()" onclick="checkModal()">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage" colSpan="3">PEMESANAN&nbsp;- Lead Time</TD>
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
								<TD class="titleField" width="24%">Kode Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:label id="lblDealerCode" runat="server" Width="140px">Label</asp:label>
									<asp:textbox id="txtDealerCode" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')" runat="server" Width="144px"></asp:textbox>
									<asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
									<asp:button id="btnGetDealer" runat="server" Width="60px" Text="GetDealer"></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Dealer</TD>
								<TD>:</TD>
								<TD><asp:label id="lblDealerName" runat="server">Label</asp:label>&nbsp;/
									<asp:label id="lblDealerTerm" runat="server">Label</asp:label></TD>
							</TR>
                            <TR>
								<TD class="titleField">Lead Time RO</TD>
								<TD>:</TD>
								<TD><asp:textbox id="txtLeadTimeRO" runat="server" Width="140px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										onblur="omitSomeCharacter('txtLeadTimeRO','<>?*%$;')"></asp:textbox></TD>
							</TR>
                            <TR>
								<TD class="titleField">Lead Time EO</TD>
								<TD>:</TD>
								<TD><asp:textbox id="txtLeadTimeEO" runat="server" Width="140px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										onblur="omitSomeCharacter('txtLeadTimeEO','<>?*%$;')"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD><table border="0" cellpadding="2" cellspacing="0">
										<tr>
											<td><asp:button id="btnFind" runat="server" Width="60px" Text="Cari"></asp:button>
                                                <asp:Button ID="btnDownload" runat="server" Text="Download" />
                                            </td>
										</tr>
									</table>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD colSpan="3"><div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dtgDealerLeadTime" runat="server" Width="100%" BorderColor="Gainsboro" BackColor="Gainsboro"
								CellSpacing="1" CellPadding="3" OnItemDataBound="dtgDealerLeadTime_ItemDataBound" BorderWidth="0px" AutoGenerateColumns="False" PageSize="50" AllowPaging="True"
								AllowCustomPaging="True" AllowSorting="True">
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle Font-Bold="True" Height="20px" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="ID">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="DealerCode">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nama Dealer" SortExpression="DealerName">
										<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kota" SortExpression="CityName">
										<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Area" SortExpression="Area">
										<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="RO" SortExpression="RO">
										<HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
										<ItemTemplate>
                                            <asp:TextBox ID="txtRO" runat="server" Width="30"></asp:TextBox>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="EO" SortExpression="EO">
										<HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
										<ItemTemplate>
                                            <asp:TextBox ID="txtEO" runat="server" Width="30"></asp:TextBox>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnkSave" runat="server" CausesValidation="False" CommandName="Save">
												<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript" type="text/javascript">
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
