<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanPartRegister.aspx.vb" Inherits="FrmSalesmanPartRegister" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAplikasiHeader</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
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
			
		</script>
		<script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../PopUp/PopUpSelectingDealer.aspx?multi=true','',540,880,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var txtDealerCodeSelection = document.getElementById("txtDealerCode");
				var arrValue = selectedDealer.split(';');
				
				txtDealerCodeSelection.value = arrValue[0];
				/* txtDealerNameSelection.value = arrValue[1]; */
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
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px"><asp:label id="lblPageTitle" runat="server"></asp:label></td>
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
								<TD class="titleField" style="HEIGHT: 17px" width="24%"><asp:label id="lblSalesmanUnit" runat="server" Width="208px"></asp:label></TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 17px" width="25%"><asp:dropdownlist id="ddlSalesmanUnit" runat="server" AutoPostBack="True" Width="152px" Enabled="False"></asp:dropdownlist></TD>
								<TD class="titleField" style="HEIGHT: 17px" width="20%"></TD>
								<TD style="HEIGHT: 17px" width="1%"></TD>
								<TD style="HEIGHT: 17px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Kode Dealer</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="25%"><asp:textbox onkeypress="TxtKeypress();" id="txtDealerCode" onblur="TxtBlur('txtDealerCode');"
										runat="server" Width="128px" MaxLength="10" ToolTip="Dealer Search 1"></asp:textbox><asp:label id="lblPopUpDealer" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 10px" width="20%"></TD>
								<TD style="HEIGHT: 10px" width="1%"></TD>
								<TD style="HEIGHT: 10px" width="29%"></TD>
							</TR>
                            <TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Kode Cabang Dealer</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="25%"><asp:textbox onkeypress="TxtKeypress();" id="txtDealerBranchCode" onblur="TxtBlur('txtDealerBranchCode');"
										runat="server" Width="128px" MaxLength="10" ToolTip="Dealer Branch Search 1"></asp:textbox><asp:label id="lblPopUpDealerBranch" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 10px" width="20%"></TD>
								<TD style="HEIGHT: 10px" width="1%"></TD>
								<TD style="HEIGHT: 10px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" width="24%">Status</TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" noWrap width="25%"><asp:dropdownlist id="ddlStatus" runat="server" AutoPostBack="False"></asp:dropdownlist></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="20%"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px" width="24%"><asp:label id="lblNamaSalesman" runat="server"></asp:label></TD>
								<TD style="HEIGHT: 18px" width="1%"><asp:label id="Label5" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 18px" noWrap width="25%"><asp:textbox onkeypress="TxtKeypress();" id="txtName" onblur="TxtBlur('txtName');" runat="server"
										Width="300px" MaxLength="60"></asp:textbox></TD>
								<TD class="titleField" style="HEIGHT: 18px" width="20%"></TD>
								<TD style="HEIGHT: 18px" width="1%"></TD>
								<TD style="HEIGHT: 18px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button></TD>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 280px"><asp:datagrid id="dgResult" runat="server" Width="100%" CellPadding="3" BorderWidth="0px" CellSpacing="1"
											BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True" PageSize="25" AllowPaging="True">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
													<HeaderStyle CssClass="titleTableRsd" Width="5%" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblDealerCode" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblDealerName" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="DealerBranch.DealerBranchCode" HeaderText="Kode Cabang">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
                                                    <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblDealerBranchCode" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Name" HeaderText="Nama">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblName" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="DateOfBirth" HeaderText="Tanggal lahir">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblDateOfBirth" runat="server" Width="51px"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="RegisterStatus" HeaderText="Status">
													<HeaderStyle CssClass="titleTableRsd" Width="10%" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Width="15%" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblRegisterStatus" Runat="server" ></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanCode" HeaderText="Kode">
													<HeaderStyle CssClass="titleTableRsd" Width="10%" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Width="15%" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblSalesmanCode" Runat="server" ></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
															Visible="False" CommandName="View">
															<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
														<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" Visible="False"
															CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:Button id="btnRegister" Text="Register" Runat="server" CausesValidation="False" CommandName="Register"></asp:Button>
														<asp:LinkButton id="lbtnKonfirmasi" ToolTip="Konfirmasi" Runat="server" CausesValidation="False"
															CommandName="Konfirmasi" text="Konfirmasi">
															<img border="0" src="../images/aktif.gif" alt="Konfirmasi" style="cursor:hand"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False"
															CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
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
				<TR>
					<TD style="HEIGHT: 8px"><asp:button id="btnDownload" runat="server" Text="Download"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
