<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmMaxTOPDay.aspx.vb" Inherits="FrmMaxTOPDay" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Maksimum Hari TOP</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
              <script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
		<script>			
			function ShowPPDealerSelection()
			{
				showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
			
			function DealerSelection(selectedDealer)
			{
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = selectedDealer;			
			}
		
			function ShowDataHistory(sUrl){
				showPopUp(sUrl,'',500,760);
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="titlePage">MAINTENANCE - Maksimum Hari TOP</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
				</tr>
				<tr>
					<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellPadding="2" width="100%">
							<TR>
								<TD style="HEIGHT: 23px" class="titleField" width="24%">Dealer</TD>
								<TD style="HEIGHT: 23px" width="1%">:</TD>
								<TD style="WIDTH: 147px; HEIGHT: 23px" width="147">
									<table cellspacing="0" cellpadding="0" border="0">
										<tr>
											<td>
												<asp:textbox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" style="Z-INDEX: 0" id="txtKodeDealer"
													onkeypress="return alphaNumericExcept(event,'<>?*%$')" runat="server"></asp:textbox>
											</td>
											<td>
												<asp:label style="Z-INDEX: 0" id="lblSearchDealer" runat="server">
													<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
											</td>
										</tr>
									</table>
								</TD>
								<TD style="HEIGHT: 23px" width="55%"></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 23px" class="titleField" width="24%">Factoring/Non</TD>
								<TD style="HEIGHT: 23px" width="1%">:</TD>
								<TD style="WIDTH: 147px; HEIGHT: 23px" width="147">
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlFactoring" runat="server" Width="140px"></asp:dropdownlist></TD>
								<TD style="HEIGHT: 23px" width="55%"></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 24px" class="titleField" width="24%" vAlign="top">Propinsi</TD>
								<TD style="HEIGHT: 24px" width="1%" vAlign="top">:</TD>
								<TD style="WIDTH: 147px; HEIGHT: 24px" width="147">
									<asp:listbox style="Z-INDEX: 0" id="lsbProvince" runat="server" Width="288px" Rows="3" SelectionMode="Multiple"
										Height="104px"></asp:listbox></TD>
								<TD style="HEIGHT: 24px" width="55%">
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlProvince" runat="server" Width="140px" AutoPostBack="True"
										Visible="False"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 23px" class="titleField" width="24%"><asp:label id="lblCategory" runat="server"> Kategori</asp:label></TD>
								<TD style="HEIGHT: 23px" width="1%">:</TD>
								<td style="WIDTH: 147px; HEIGHT: 23px" width="147">
									<table cellspacing="0" cellpadding="0" border="0">
										<tr>
											<td>
												<asp:dropdownlist id="ddlCategory" runat="server" AutoPostBack="True" Width="140px"></asp:dropdownlist>
											</td>
											<td>
												<asp:dropdownlist style="Z-INDEX: 0" id="ddlSubCategory" runat="server" AutoPostBack="True"></asp:dropdownlist>
											</td>
										</tr>
									</table>
								</td>
								<TD style="HEIGHT: 23px" width="55%"></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblType" runat="server"> Tipe</asp:label></TD>
								<TD width="1%">:</TD>
								<TD  ><asp:dropdownlist id="ddlType" runat="server" Width="228px"></asp:dropdownlist></TD>
								<TD colspan="2">
                                    	<table>
							<tr>
								<td width="200">&nbsp;</td>
								<td width="200"><STRONG>COD</STRONG></td>
								<td width="1">:</td>
								<td> <asp:CheckBox  runat="server" ID="chkCODH" Text=""/> </td>
								<td><asp:button id="btnSaveCOD" Text="Simpan" Runat="server"></asp:button></td>
								<td></td>
							</tr>
						</table>


								</TD>
							</TR>
							<TR>
								<TD class="titleField">Jumlah&nbsp;Max Hari TOP</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 147px">
									<table cellpadding="0" cellspacing="0" border="0" width="100%">
										<TBODY>
											<tr>
												<td>
													<asp:textbox style="Z-INDEX: 0" id="txtMaxTOPDay" onkeypress="return numericOnlyUniv(event)"
														Text="0" CssClass="textRight" Runat="server" Width="50px">0</asp:textbox>
												</td>
												<td><STRONG>
														<asp:CheckBox id="chkMaxTOPDay" runat="server" Text="Kriteria Pencarian" Width="128px"></asp:CheckBox>
													</STRONG>
												</td>
								</TD>
							</TR>
						</TABLE>
						<asp:Label style="Z-INDEX: 0" id="Label3" runat="server" Width="464px" ForeColor="Red" Font-Italic="True"> * Nilai Max TOP 0 (NOL) berarti mengikuti Jumlah Hari di bulan berjalan</asp:Label>
					<TD>
						<table>
							<tr>
								<td width="200"><asp:button style="Z-INDEX: 0" id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button></td>
								<td width="200"><STRONG>Max Hari TOP</STRONG></td>
								<td width="1">:</td>
								<td><asp:textbox id="txtNewMaxTOPDay" onkeypress="return numericOnlyUniv(event)" Text="0" Runat="server"
										CssClass="textRight"></asp:textbox></td>
								<td><asp:button id="btnSimpanAll" Text="Simpan" Runat="server"></asp:button></td>
								<td></td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD height="10" colSpan="4"></TD>
				</TR>
			</TABLE>
			</TD></TR>
			<TR>
				<TD vAlign="top">
					<div style="HEIGHT: 370px; OVERFLOW: auto" id="div1"><asp:datagrid id="dgColor" runat="server" Width="100%" PageSize="25" AllowPaging="True" AllowCustomPaging="True"
							CellSpacing="1" GridLines="None" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AutoGenerateColumns="False">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="DealerCode" SortExpression="DealerCode" ReadOnly="True" HeaderText="Kode Dealer">
									<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Factoring/Non">
									<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label Runat="server" ID="lblFactoring" Text="F"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="ProvinceName" SortExpression="ProvinceName" ReadOnly="True" HeaderText="Propinsi">
									<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CategoryCode" SortExpression="CategoryCode" ReadOnly="True" HeaderText="Kategori">
									<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="VechileTypeCode" SortExpression="VechileTypeCode" ReadOnly="True" HeaderText="Kode/Tipe">
									<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Deskripsi">
									<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
									<ItemTemplate>
										<asp:Label Runat="server" ID="lblDescription"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="MaxTOP">
									<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									<ItemTemplate>
										<asp:TextBox Runat="server" Text="0" Width="50px" id="txtMaxTOP" onkeypress="return numericOnlyUniv(event)"
											CssClass="textRight"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>

                                           <asp:TemplateColumn>
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
													<span>	  COD Allowed</span>
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="chkCOD" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>

								<asp:TemplateColumn>
									<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
									<ItemTemplate>
										<asp:LinkButton Runat="server" ID="lbtnSave" CommandName="Save">
											<img src="../images/download.gif" style="cursor:hand" border="0" alt="Simpan Maks TOP">
										</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblHistoryStatus" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Lihat History Perubahan">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></div>
				</TD>
			</TR>
			<TR>
				<TD style="HEIGHT: 8px" height="8"><asp:button id="btnDnLoad" runat="server" Width="60px" Text="Download" Enabled="False"></asp:button></TD>
			</TR>
			</TBODY></TABLE>
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
