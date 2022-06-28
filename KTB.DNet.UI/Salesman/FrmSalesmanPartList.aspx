<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanPartList.aspx.vb" Inherits="FrmSalesmanPartList" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSalesmanList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx','',500,600,DealerSelection);
			}
			
			function DealerSelection(selectedDealer)
			{
				var txtDealer = document.getElementById("txtDealerCode");
				txtDealer.value = selectedDealer;				
			}
		
			/* ini untuk handle char yg tdk diperbolehkan, saat paste */
			function TxtBlurName(objTxt) {
			    omitSomeCharacterExcludeSingleQuote(objTxt, '<>?*%$;');
			}
			function TxtBlur(objTxt)
			{
				omitSomeCharacter(objTxt,'<>?*%$;');
			}
			/* ini untuk handle char yg tdk diperbolehkan, saat keypress */
			function TxtKeypress()
			{
				return alphaNumericExcept(event,'<>?*%$;')
			}
			
			function ShowSalesmanSelection()
			{	
				var lblSalesmanCode = document.getElementById("lblShowSalesman");
				showPopUp('../PopUp/PopUpSalesmanPart.aspx?IsGroupDealer=1&IsSales=0&IsResign=0','',470,600,SalesmanSelection);
			}
				
			function SalesmanSelection(SelectedSalesman)
			{
				var tempParam = SelectedSalesman.split(';');
				var txtSalesmanCode = document.getElementById("txtID");
				var txtNama = document.getElementById("txtNama");
				txtSalesmanCode.value = tempParam[0]
				txtNama.value = tempParam[1];
			}

			function ShowPPDealerBranchSelection() {
			    var txtDealerSelection = document.getElementById("txtDealerCode");
			    showPopUp('../PopUp/PopUpDealerBranchMultipleSelection.aspx?DealerCode=' + txtDealerSelection.value, '', 500, 760, DealerBranchSelection);
			}

			function DealerBranchSelection(selectedDealerBranch) {
			    var txtDealerBranchSelection = document.getElementById("txtDealerBranchCode");
			    txtDealerBranchSelection.value = selectedDealerBranch;
			}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage"><asp:label id="lblPageTitle" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" width="20%">Kode Dealer</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD width="79%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
										runat="server" Width="191px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img style="cursor:hand" alt="Klik Disini untuk memilih Nomor Permintaan" src="../images/popup.gif"
											border="0"></asp:label></TD>
							</TR>
                            <TR>
								<TD class="titleField" width="20%">Cabang Dealer</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD width="79%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerBranchCode" onblur="omitSomeCharacter('txtDealerBranchCode','<>?*%$')"
										runat="server" Width="191px"></asp:textbox><asp:label id="lblSearchDealerBranch" runat="server">
										<img style="cursor:hand" alt="Klik Disini untuk memilih Nomor Permintaan" src="../images/popup.gif"
											border="0"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblSalesmanID" runat="server" Width="220px"></asp:label></TD>
								<TD style="HEIGHT: 28px" width="1%">:</TD>
								<TD>
									<asp:textbox onkeypress="TxtKeypress();" id="txtID" onblur="TxtBlur('txtID');" runat="server"
										MaxLength="15"></asp:textbox>
									<asp:label id="lblShowSalesman" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									</asp:label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Nama</TD>
								<TD style="HEIGHT: 23px" width="1%">:</TD>
								<td><asp:textbox onkeypress="TxtKeypress();" id="txtNama" onblur="TxtBlurName('txtNama');" runat="server"
										MaxLength="60"></asp:textbox>&nbsp;</td>
							</TR>
							<TR>
								<TD class="titleField">Status</TD>
								<TD style="HEIGHT: 28px" width="1%">:</TD>
								<td><asp:dropdownlist id="ddlStatus" runat="server"></asp:dropdownlist></td>
							</TR>
							<TR>
								<TD class="titleField">Kategori</TD>
								<TD style="HEIGHT: 28px" width="1%">:</TD>
								<td><asp:dropdownlist id="ddlKategori" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
							</TR>
							<TR>
								<TD class="titleField">Posisi</TD>
								<TD width="1%">:</TD>
								<TD><asp:dropdownlist id="ddlPosisi" tabIndex="12" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label ID="lblLevelSalesman" Runat="server">Level</asp:label></TD>
								<TD width="1%"><asp:label ID="lblSep" Runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlGrade" tabIndex="12" runat="server"></asp:dropdownlist></TD>
							</TR>
							
							<TR>
								<TD class="titleField"></TD>
								<TD width="1%"></TD>
								<td><asp:button id="btnCari" runat="server" width="60px" Text="Cari"></asp:button><asp:button id="btnCancel" runat="server" width="60px" Text="Batal"></asp:button><asp:button id="btnDownloadExcel" Text="Download Excel" Runat="server"></asp:button></td>
							</TR>
							<TR>
								<TD class="titleField" colSpan="3">
									<div id="div1" style="HEIGHT: 240px; OVERFLOW: auto"><asp:datagrid id="dgSalesmanHeader" runat="server" Width="100%" CellPadding="3" BackColor="#CDCDCD"
											PageSize="25" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False" CellSpacing="1"
											AllowSorting="True" DESIGNTIMEDRAGDROP="57">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
													<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblKodeDealer" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="DealerBranch.DealerBranchCode" HeaderText="Kode Cabang">
													<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblKodeDealerBranch" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="SalesmanCode" SortExpression="SalesmanCode" HeaderText="Kode">
													<HeaderStyle Width="10%" CssClass="titleTableParts" Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
                                               <asp:TemplateColumn  HeaderText="No Registrasi Training">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblSalesmanHeaderID" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama">
													<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Kategori">
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblJobPositionId_Main" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Posisi">
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblPosisi" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Level">
													<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblLevel" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="HireDate" SortExpression="HireDate" HeaderText="Tanggal Masuk" DataFormatString="{0:dd/MM/yyyy}">
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="DateOfBirth" HeaderText="Tanggal Lahir">
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblResignDate Text='<%# Format(DataBinder.Eval(Container, "DataItem.DateOfBirth"),"dd/MM/yyyy") %>' Runat="server">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
													<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblStatus" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
															CommandName="View">
															<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
														<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False"
															CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
														<asp:LinkButton id="lbtnRegister" runat="server" Width="20px" Text="Request ID" CausesValidation="False"
															CommandName="RequestID">
															<img src="../images/aktif.gif" border="0" alt="Request ID"></asp:LinkButton>
														<asp:LinkButton id="lbtnPartshop" runat="server" Width="20px" Text="Partshop" CausesValidation="False"
															CommandName="Partshop">
															<img src="../images/dok.gif" border="0" alt="Partshop"></asp:LinkButton>
														<asp:LinkButton id="lbtnHistory" runat="server" Width="20px" Text="History" CausesValidation="False"
															CommandName="History">
															<img src="../images/alur_flow.gif" border="0" alt="History"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
