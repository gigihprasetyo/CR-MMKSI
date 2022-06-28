<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDisplayDepositC2.aspx.vb" Inherits="FrmDisplayDepositC2" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ListContract</title>
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
				showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer.split(';');
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				var lblDealerName = document.getElementById("lblDealerName");
				var TextDealerName = document.getElementById("txtDealerName");
				txtDealerSelection.value = tempParam[0];
				lblDealerName.innerHTML	=tempParam[1] + " - " + tempParam[3];		
				TextDealerName.value=tempParam[1] + " - " + tempParam[3];	
			}
			function ShowPPDealerSelectionKTB()
			{			
				showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelectionKTB);
			}
			function DealerSelectionKTB(selectedDealer)
			{
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = selectedDealer;			
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">DEPOSIT&nbsp;- Daftar Deposit C2</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 13px" width="24%"><asp:label id="lblCode" runat="server" Font-Bold="True">Kode Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="lblColon1" runat="server">:</asp:label></TD>
								<TD width="170"><asp:label id="lblDealerCode" runat="server" Width="48px" Visible="False"></asp:label>
									<asp:textbox id="txtKodeDealer" runat="server"  Width="80px" ></asp:textbox>
									<asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD width="50"></TD>
								<TD width="170"></TD>
								<TD width="50"></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblName" runat="server" Font-Bold="True">Nama Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="lblColon2" runat="server">:</asp:label></TD>
								<TD colspan="4"><asp:label id="lblDealerName" runat="server" Width="400px"></asp:label><INPUT id=txtDealerName type=hidden value="<%= lblDealerName.Text %>" name=txtDealerName></TD>
							</TR>

							<TR>
								<TD class="titleField"><asp:label id="lblDocDate" runat="server" Font-Bold="True">Tgl Dokumen</asp:label></TD>
								<TD>:</TD>
								<TD colspan=3><table border=0 cellpadding=0 cellspacing=0><tr><td><cc1:inticalendar id="icDocDateStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="icDocDateEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></td></tr></table>
								<TD><asp:button id="btnSearch" runat="server" Text=" Cari " Height="24px" Width="60px"></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 2px"><asp:label id="Label1" runat="server" Font-Bold="True">Nomor Dokumen</asp:label></TD>
								<TD>:</TD>
								<TD><asp:textbox id="txtNoDoc" runat="server"  Width="80px"></asp:textbox></TD>
								<td></td>
								<td></td>
								<td></td>
							<TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 2px"><asp:label id="Label2" runat="server" Font-Bold="True">Nomor Billing</asp:label></TD>
								<TD>:</TD>
								<TD><asp:textbox id="txtBillingNumber" runat="server"  Width="80px"></asp:textbox></TD>
								<td></td>
								<td></td>
								<td></td>
							<TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 2px"><asp:label id="lblTotDepC2" runat="server" Font-Bold="True">Total Deposit C2 (Rp)</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblTotDepositC2" runat="server"></asp:label></TD>
								<td></td>
								<td></td>
								<td></td>
							<TR>
								<TD colSpan="6"></TD>
							<TR>
								<TD colSpan="6">
									<asp:label id="lblNotes" runat="server" Font-Bold="True">* Nilai ini tidak termasuk bunga</asp:label></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<DIV id="div1" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 310px">
										<asp:datagrid id="dgDepositC2List" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="#CDCDCD"
											BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px" CellPadding="3" AllowSorting="True">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
													<HeaderStyle Width="4%" CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblNo" runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Kode Dealer">
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="DocDateText" SortExpression="DocDateText" ReadOnly="True" HeaderText="Tgl Dokumen">
													<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="DocumentNo" SortExpression="DocumentNo" ReadOnly="True" HeaderText="No Dokumen">
													<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="DepositC2Amnt" SortExpression="DepositC2Amnt" ReadOnly="True" HeaderText="Deposit C2 (Rp) *"
													DataFormatString="{0:#,##0}">
													<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
                                                <asp:BoundColumn DataField="BillingNumber" SortExpression="BillingNumber" ReadOnly="True" HeaderText="No Billing">
													<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Download Tanda Terima Titipan">
                                                <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDownload" runat="server" CommandName="Download" Text="Download">
												<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>  
											</Columns>
										</asp:datagrid></DIV>
								</TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6"><asp:button id="btnDnLoad" runat="server" Text="Download" Width="72px" Enabled="False" ></asp:button>
									<INPUT id="btnPrint" type="button" runat="server" value="Cetak Tampilan" name="btnPrint"></TD>
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
