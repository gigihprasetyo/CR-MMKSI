<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPQRListKondisiBB.aspx.vb" Inherits="FrmPQRListKondisiBB" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPQRListKondisi</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory','',500,760,DealerSelection);
			}
			
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer.split(';');
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = tempParam[0];
			}

			function ShowPPDealerBranchSelection() {
			    var txtDealerSelection = document.getElementById("txtKodeDealer");
			    showPopUp('../General/../PopUp/PopUpDealerBranchMultipleSelection.aspx?DealerCode=' + txtDealerSelection.value, '', 500, 760, DealerBranchSelection);
			}

			function DealerBranchSelection(selectedDealerBranch) {
			    var txtDealerBranchSelection = document.getElementById("txtKodeDealerBranch");
			    txtDealerBranchSelection.value = selectedDealerBranch;
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
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PRODUCT QUALITY REPORT SPECIAL-&nbsp; Daftar Kondisi</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="20%"><asp:label id="lblDealerSearch" runat="server">Dealer</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="34%"><asp:textbox id="txtKodeDealer" runat="server" MaxLength="10"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label></TD>
								<TD class="titleField" width="20%"><asp:label id="lblNoChasisSearch" runat="server">No Chasis</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="24%"><asp:textbox id="txtNoChasis" runat="server" MaxLength="20"></asp:textbox></TD>
							</TR>
                            <tr>
                                <td class="titleField">Cabang</td>
                                <td>:</td>
                                <td>
                                    <asp:textbox id="txtKodeDealerBranch" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtKodeDealerBranch','<>?*%$')"
										runat="server" Width="156px"></asp:textbox>
									<asp:label id="lblSearchDealerBranch" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
                                </td>
                                <td class="titleField"><asp:label id="lblDamageCode0" runat="server">Kode Kerusakan</asp:label></td>
                                <td>:</td>
                                <td><asp:textbox id="txtKodeKerusakan" runat="server" MaxLength="50"></asp:textbox></td>
                            </tr>
							<TR>
								<TD class="titleField"><asp:label id="lblPQRNoSearch" runat="server">No PQR</asp:label></TD>
								<TD>:</TD>
								<TD><asp:textbox id="txtPQRNo" runat="server" MaxLength="25"></asp:textbox></TD>
								<TD class="titleField"><asp:label id="lblKategoriSearch" runat="server">Kategori</asp:label></TD>
								<TD>:</TD>
								<TD><asp:dropdownlist id="ddlKategori" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblTglApplySearch" runat="server">Periode Validasi</asp:label></TD>
								<TD>:</TD>
								<TD><table border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td><asp:CheckBox id="cbDate" runat="server" Checked="True" Enabled="False" Text="." Visible="False"></asp:CheckBox></td>
											<td>
												<cc1:inticalendar id="icTglValidateDari" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
											<td nowrap>
												&nbsp;s.d&nbsp;
											</td>
											<td><cc1:inticalendar id="icTglValidateSampai" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
								<TD class="titleField"><asp:label id="lblStat" runat="server">Status</asp:label></TD>
								<TD>:</TD>
								<TD><asp:dropdownlist id="ddlStatus" runat="server"></asp:dropdownlist></TD>
							</TR>
							<tr>
								<TD class="titleField"><asp:label id="lblSubject" runat="server">Subject</asp:label></TD>
								<TD>:</TD>
								<TD style="WIDTH: 310px"><asp:textbox id="txtSubject" runat="server" MaxLength="50" Width="156px"></asp:textbox></TD>
								<TD class="titleField"><asp:label id="lblProsesOleh" runat="server">Diproses Oleh</asp:label></TD>
								<TD>:</TD>
								<TD noWrap><asp:textbox id="txtProsesOleh" runat="server" MaxLength="20"></asp:textbox></TD>
							</tr>
							<TR>
								<TD class="titleField"><asp:label id="lblProcessTimeAvg" runat="server">Rata-rata waktu Proses</asp:label></TD>
								<TD>:</TD>
								<TD style="WIDTH: 310px"><asp:label id="lblProcessTimeAvgVal" runat="server"></asp:label></TD>
								<TD></TD>
								<TD></TD>
								<TD><asp:button id="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblBobotAvg" runat="server"> Rata-rata bobot</asp:label></TD>
								<TD>:</TD>
								<TD style="WIDTH: 310px"><asp:label id="lblBobotAvgVal" runat="server"></asp:label></TD>
								<TD colSpan="3"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<div id="div1" style="HEIGHT: 260px; OVERFLOW: auto"><asp:datagrid id="dgListPQR" runat="server" Width="100%" DataKeyField="ID" BorderStyle="None"
					AllowPaging="True" PageSize="50" AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px"
					BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal" ForeColor="Gray" CellSpacing="1">
					<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
					<AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
					<ItemStyle ForeColor="Black" BackColor="White" VerticalAlign="Top"></ItemStyle>
					<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
					<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
					<Columns>
						<asp:TemplateColumn>
							<HeaderStyle ForeColor="White" Width="1%" CssClass="titleTableService"></HeaderStyle>
							<HeaderTemplate>
								<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkSelection',
														document.forms[0].chkAllItems.checked)" />
							</HeaderTemplate>
							<ItemTemplate>
								<asp:CheckBox id="chkSelection" runat="server"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="No">
							<HeaderStyle ForeColor="White" Width="1%" CssClass="titleTableService"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="lblNo" runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="RowStatus" HeaderText="Status">
							<HeaderStyle ForeColor="White" Width="1%" CssClass="titleTableService"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="lblStatus" runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
							<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" tooltip = '<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1")  %>' Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' ID="lblDealer" NAME="lblDealer">
								</asp:Label>
							</ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="DealerBranch.DealerBranchCode" HeaderText="Cabang">
							<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" tooltip = '<%# DataBinder.Eval(Container, "DataItem.DealerBranch.Name")%>' Text='<%# DataBinder.Eval(Container, "DataItem.DealerBranch.DealerBranchCode")%>' ID="lblDealerBranch" NAME="lblDealerBranch">
								</asp:Label>
							</ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="PQRNo" HeaderText="PQR No">
							<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
							<ItemTemplate>
								<asp:LinkButton ID="lnkbtnPQRNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PQRNo") %>' CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="ViewPQR" >
								</asp:LinkButton>
							</ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="ChassisMaster.ChassisNumber" HeaderText="No Rangka">
							<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMasterBB.ChassisNumber") %>' ID="lblNoChassis" NAME="lblNoChassis">
								</asp:Label>
							</ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="ValidationTime" HeaderText="Tgl Validasi">
							<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="lblTglValidasi"></asp:Label>
							</ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="RealeseTime" HeaderText="Tgl Rilis">
							<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="lblTglRilis"></asp:Label>
							</ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="Subject" SortExpression="Subject" HeaderText="Subject">
							<HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn SortExpression="ConfirmBy" HeaderText="Diproses Oleh">
							<HeaderStyle CssClass="titleTableService"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="lblConfirmBy"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Bobot" HeaderText="Bobot">
							<HeaderStyle CssClass="titleTableService"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Bobot") %>' ID="lblBobot" NAME="lblBobot">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="IntervalProcess" HeaderText="Interval">
							<HeaderStyle CssClass="titleTableService" Width="20%"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="lblInterval" NAME="lblInterval"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></div>
			<br>
			<asp:button id="btnDownload" Text="Download" Runat="server"></asp:button></form>
	</body>
</HTML>
