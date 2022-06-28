<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmSalesmanRekapSalesPerGroupDealer.aspx.vb" Inherits="frmSalesmanRekapSalesPerGroupDealer" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAplikasiHeader</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<base target="_self">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			/* Deddy H	validasi value *********************************** */
			/* ini untuk handle char yg tdk diperbolehkan, saat paste */
			function TxtBlur(objTxt)
			{
				omitSomeCharacter(objTxt,'<>?*%$;');
			}
			/* ini untuk handle char yg tdk diperbolehkan, saat keypress */
			function TxtKeypress()
			{
				return alphaNumericExcept(event,'<>?*%$;')
			}
			function ShowPPDealerCodeSelection()
			{
				showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',540,760,DealerCodeSelection);
			}
			function DealerCodeSelection(selectedDealer)
			{
				var txtDealerSelection = document.getElementById("txtDealerCode");
				txtDealerSelection.value = selectedDealer;			
			}	
		</script>
		<script language="javascript">
		function ShowPPDealerSelection()
		{
			showPopUp('../SparePart/../PopUp/PopUpGroupDealerSelection.aspx?x=Territory','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var txtGroupDealer= document.getElementById("txtGroupDealer");
			txtGroupDealer.value = selectedDealer;				
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">TENAGA PENJUAL- Rekap Tenaga Penjual Per 
						Group Dealer</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 17px" width="24%">
									Unit</TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 329px; HEIGHT: 17px" width="329"><asp:dropdownlist id="ddlSalesmanUnit" runat="server" Width="152px" AutoPostBack="True"></asp:dropdownlist></TD>
								<TD class="titleField" style="WIDTH: 157px; HEIGHT: 17px" width="157"></TD>
								<TD style="HEIGHT: 17px" width="1%"></TD>
								<TD style="HEIGHT: 17px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px" width="24%">Kode Dealer</TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 329px; HEIGHT: 26px" width="329">
									<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
										runat="server" Width="152px"></asp:textbox>
									<asp:label id="lblDealers" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label></TD>
								<TD class="titleField" style="WIDTH: 157px; HEIGHT: 17px" width="157"></TD>
								<TD style="HEIGHT: 17px" width="1%"></TD>
								<TD style="HEIGHT: 17px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px" width="24%">Group Dealer</TD>
								<TD style="HEIGHT: 26px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 329px; HEIGHT: 26px" width="329"><asp:textbox onkeypress="TxtKeypress();" id="txtGroupDealer" onblur="TxtBlur('txtGroupDealer');"
										runat="server" Width="248px"></asp:textbox>
									<asp:label id="lblSearchGroupDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label></TD>
								<TD class="titleField" style="WIDTH: 157px; HEIGHT: 17px" width="157"></TD>
								<TD style="HEIGHT: 26px" width="1%"></TD>
								<TD style="HEIGHT: 26px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="WIDTH: 329px; HEIGHT: 11px"><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button>
									<asp:Button id="btnBack" runat="server" Text="Kembali"></asp:Button></TD>
								<TD class="titleField" style="WIDTH: 157px; HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 330px"><asp:datagrid id="dgResult" runat="server" Width="100%" PageSize="25" AllowSorting="True" CellSpacing="1"
											AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" AllowPaging="True" AllowCustomPaging="True" BackColor="#CDCDCD"
											CellPadding="3" GridLines="None" ShowFooter="True">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn SortExpression="Dealer.DealerGroup.GroupName" HeaderText="Group Dealer">
													<HeaderStyle Width="20%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblGroupDealer"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="TotalBM" HeaderText="SL">
													<HeaderStyle Width="8%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblBM"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalBM" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="TotalMgr" HeaderText="SC">
													<HeaderStyle Width="8%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblManajer"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalMGR" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="TotalAMGR" HeaderText="SPV">
													<HeaderStyle Width="8%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblAssManajer"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalAMGR" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Totalspv1" HeaderText="SM">
													<HeaderStyle Width="8%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblspv1"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalspv1" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Totalspv2" HeaderText="BM">
													<HeaderStyle Width="8%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblspv2"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalSpv2" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False" SortExpression="Totalspv3" HeaderText="Spv3">
													<HeaderStyle Width="8%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblspv3"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalSPV3" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False" SortExpression="Totalsl1" HeaderText="SL1">
													<HeaderStyle Width="8%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblsl1"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotals11" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False" SortExpression="Totalsl2" HeaderText="SL2">
													<HeaderStyle Width="8%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblsl2"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalS12" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False" SortExpression="TotalTR" HeaderText="TR">
													<HeaderStyle Width="8%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lbltr"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalTR" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Total">
													<HeaderStyle Width="8%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblTotal"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalTotal" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 8px"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
