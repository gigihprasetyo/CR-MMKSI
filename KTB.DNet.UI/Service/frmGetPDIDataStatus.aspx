<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmGetPDIDataStatus.aspx.vb" Inherits="frmGetPDIDataStatus" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Daftar Status PDI</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script type="text/javascript">
		
			function showReasonDescription(reasonID,description)
			{
				var URL = "FrmFSReasonDescription.aspx?ID=" + reasonID + "&description=" + description;
				window.showModalDialog(URL);
				return false;
			}
			
			function ShowPPDealerSelection()
			{
				showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}

			function ShowPDILog()
			{
			    showPopUp('../PopUp/PopUpPDILog.aspx', '', 500, 760, '');
			}
			
			function DealerSelection(selectedDealer)
			{
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = selectedDealer;			
			}

			function ShowPPDealerBranchSelection() {
			    var txtDealerSelection = document.getElementById("txtKodeDealer");
			    showPopUp('../General/../PopUp/PopUpDealerBranchMultipleSelection.aspx?DealerCode=' + txtDealerSelection.value, '', 500, 760, DealerBranchSelection);
			}

			function DealerBranchSelection(selectedDealerBranch) {
			    var txtDealerBranchSelection = document.getElementById("txtKodeDealerBranch");
			    txtDealerBranchSelection.value = selectedDealerBranch;
			}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">&nbsp;PRE DELIVERY INSPECTION - Daftar Status PDI</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="773" border="0" style="WIDTH: 773px; HEIGHT: 64px">
							<TR>
								<TD class="titleField" width="24%" style="HEIGHT: 21px"><asp:label id="lblDealer" runat="server">Kode Dealer </asp:label></TD>
								<TD width="1%" style="HEIGHT: 21px">:</TD>
								<td width="55%" style="HEIGHT: 21px">
									<asp:textbox id="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server" Width="300px"></asp:textbox>
									<asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
                                <td class="titleField" width="300px" style="HEIGHT: 21px">PDI Kadaluarsa</td>
								<td style="HEIGHT: 21px">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup" onclick="ShowPDILog();"></td>
							</TR>
                            <TR>
								<TD class="titleField" width="24%" style="HEIGHT: 21px"><asp:label id="lblDealerBranch" runat="server">Kode Cabang </asp:label></TD>
								<TD width="1%" style="HEIGHT: 21px">:</TD>
								<TD width="55%" style="HEIGHT: 21px">
									<asp:textbox id="txtKodeDealerBranch" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtKodeDealerBranch','<>?*%$')"
										runat="server" Width="300px"></asp:textbox>
									<asp:label id="lblSearchDealerBranch" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
                                <td colspan="2"></td>
							</TR>
							<TR>
								<TD class="titleField" width="24%" style="HEIGHT: 1px">Status</TD>
								<TD width="1%" style="HEIGHT: 1px">:</TD>
								<TD width="55%" style="HEIGHT: 1px"><asp:dropdownlist id="ddlStatus" runat="server"></asp:dropdownlist></TD>
                                <td colspan="2"></td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" vAlign="top">Periode Rilis</TD>
								<TD style="HEIGHT: 20px" vAlign="top">:</TD>
								<TD style="HEIGHT: 20px" vAlign="top">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><cc1:inticalendar id="ICDari" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>&nbsp;s.d&nbsp;</td>
											<td><cc1:inticalendar id="ICSampai" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td></td>
										</tr>
									</table>
								</TD>
                                <td colspan="2"></td>
							</TR>
							<TR>
								<TD style="HEIGHT: 19px"><STRONG><asp:Label Runat="server" ID="lblCategori">Kategori</asp:Label>
									</STRONG>
								</TD>
								<TD style="HEIGHT: 19px"><asp:Label Runat="server" ID="lblCategori2">:</asp:Label></TD>
								<TD style="HEIGHT: 19px">
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlCategory" runat="server" Width="140px"></asp:dropdownlist></TD>
                                <td colspan="2"></td>
							</TR>
							<tr>
								<td></td>
								<td></td>
								<td><asp:button id="btnRefresh" runat="server" Width="60px" Text="Cari"></asp:button></td>
                                <td colspan="2"></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="HEIGHT: 320px; OVERFLOW: auto">
							<asp:datagrid id="dgPDI" runat="server" Width="100%" AllowSorting="True" ShowFooter="True" AutoGenerateColumns="False"
								BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3"
								CellSpacing="1" GridLines="Vertical" AllowPaging="True" PageSize="50" OnItemCommand="dgPDI_ItemCommand">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdID" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ID")%>' />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PDIStatus" HeaderText="Status">
										<HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPDIStatus" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label id="lblTotalDealer" runat="server">Total Dealer :</asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
										<HeaderStyle Width="13%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblKodeDealer" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1") %>' Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label id="lblTotalDealerValue" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="DealerBranch.DealerBranchCode" HeaderText="Cabang">
										<HeaderStyle Width="13%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblKodeCabang" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.DealerBranch.Name")%>' Text='<%# DataBinder.Eval(Container, "DataItem.DealerBranch.DealerBranchCode")%>'>
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label id="lblTotalDealerBranchValue" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ChassisMaster.ChassisNumber" HeaderText="Nomor Rangka">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblChassisNumber runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label id="lblTotalUnit" runat="server">Total Unit :</asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ChassisMaster.Category.CategoryCode" HeaderText="Kategori" Visible="true">
										<HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblKategori" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.Category.CategoryCode") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label id="lblKategoriF" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Kind" HeaderText="Jenis PDI">
										<HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblJenisPDI" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Kind") %>' >
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label id="lblTotalUnitValue" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="PDIDate" SortExpression="PDIDate" HeaderText="Tgl PDI" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ReleaseDate" SortExpression="ReleaseDate" HeaderText="Tgl Rilis" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
								    <asp:BoundColumn DataField="WorkOrderNumber" HeaderText="WO Number" SortExpression="WorkOrderNumber">
                                        <HeaderStyle Width="13%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
								    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="">
	                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
	                                    <ItemTemplate>
		                                     <asp:LinkButton id="lbDownload" runat="server" CommandName="download">
                                                 <img src="../images/download.gif" border="0" alt="Download File">
		                                     </asp:LinkButton>
	                                    </ItemTemplate>
                                    </asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 11px" align="left">&nbsp;
						<asp:button id="btnDownload" runat="server" Width="60px" Text="Download" Enabled="False"></asp:button></TD>
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
