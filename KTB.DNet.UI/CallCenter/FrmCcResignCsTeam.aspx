<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmCcResignCsTeam.aspx.vb" Inherits=".FrmCcResignCsTeam" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
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
		    /* ini untuk handle char yg tdk diperbolehkan, saat paste */
		    function TxtBlur(objTxt) {
		        omitSomeCharacter(objTxt, '<>?*%$;');
		    }
		    /* ini untuk handle char yg tdk diperbolehkan, saat keypress */
		    function TxtKeypress() {
		        return alphaNumericExcept(event, '<>?*%$;')
		    }
		    function ShowSalesmanSelection() {
		        var lblSalesmanCode = document.getElementById("lblShowSalesman");
		        //showPopUp('../PopUp/PopUpSalesmanPart.aspx?SSCode=' + lblSalesmanCode.innerText,'',600,600,SalesmanSelection);
		        
		        showPopUp('../PopUp/PopUpCsTeam.aspx?IsGroupDealer=0&IsSales=4&IsResign=0', '', 470, 600, SalesmanSelection);
		    }

		    function SalesmanSelection(SelectedSalesman) {
		        var tempParam = SelectedSalesman.split(';');
		        var txtSalesmanCode = document.getElementById("txtSalesmanCode");
		        var lblName = document.getElementById("lblName");
		        var lblPosition = document.getElementById("lblPosition");
		        txtSalesmanCode.value = tempParam[0];
		        lblName.innerHTML = tempParam[1];
		        lblPosition.innerHTML = tempParam[2];
		    }

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">
						<asp:label id="lblPageTitle" runat="server"></asp:label></td>
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
								<TD class="titleField" style="HEIGHT: 5px" width="20%">
									<asp:Label id="lblText" runat="server" Visible="False">Kode Dealer</asp:Label></TD>
								<TD style="WIDTH: 14px; HEIGHT: 5px" width="14">
									<asp:Label id="lblsemicolon" runat="server" Width="3px" Visible="False">:</asp:Label></TD>
								<TD class="titleField" style="HEIGHT: 5px" width="20%">
									<asp:Label id="lblKodeDealer" runat="server" Width="152px" Visible="False"></asp:Label></TD>
								<TD class="titleField" style="HEIGHT: 5px" width="20%"></TD>
								<TD style="HEIGHT: 5px" width="1%"></TD>
								<TD style="HEIGHT: 5px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%">Employee ID</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD width="25%">
									<asp:textbox id="txtSalesmanCode" runat="server"   Width="104px"></asp:textbox>
									<asp:label id="lblShowSalesman" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									</asp:label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%">Nama</TD>
								<TD width="14" style="WIDTH: 14px"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD class="titleField" width="20%">
									<asp:Label id="lblName" runat="server"></asp:Label></TD>
								<TD class="titleField" width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="40%"></TD>
							</TR>
							
							<TR>
								<TD class="titleField" width="20%">Posisi</TD>
								<TD width="14" style="WIDTH: 14px"><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD class="titleField" width="20%">
									<asp:Label id="lblPosition" runat="server"></asp:Label>
                                    <asp:Label id="lbllevel" runat="server" Visible="false" ></asp:Label>
                                    <asp:dropdownlist id="ddlResign" runat="server" Width="208px" Visible="false" ></asp:dropdownlist>
                                    <asp:dropdownlist id="ddlSalesIndicator" Visible="false"  runat="server" Width="208px"></asp:dropdownlist>
									<asp:label id="lblResign" runat="server" Visible="false"></asp:label>
                                    <asp:requiredfieldvalidator Visible="false"  id="Requiredfieldvalidator7" runat="server" ControlToValidate="ddlResign" ErrorMessage="Tipe Resign Harus dipilih">*</asp:requiredfieldvalidator>
								</TD>
								<TD class="titleField" width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="29%"></TD>
							</TR>
                            <%--<TR>
								<TD class="titleField" width="20%">Level</TD>
								<TD width="14" style="WIDTH: 14px"><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD class="titleField" width="20%">
									<asp:Label id="lbllevel" runat="server"></asp:Label></TD>
								<TD class="titleField" width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="29%"></TD>
							</TR>--%>
							<TR>
								<TD class="titleField" width="20%" style="HEIGHT: 29px">Tgl Keluar</TD>
								<TD style="WIDTH: 14px; HEIGHT: 29px" width="14"><asp:label id="Label5" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 80px; HEIGHT: 29px" noWrap width="80"><table border="0" cellspacing="0" cellpadding="0">
										<tr>
											<td></td>
											<td><cc1:inticalendar id="icResignDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
								<TD class="titleField" style="HEIGHT: 29px" width="20%"></TD>
								<TD width="1%" style="HEIGHT: 29px"></TD>
								<TD style="HEIGHT: 29px" width="29%"></TD>
							</TR>
							<%--<TR>
								<TD class="titleField">Tipe Resign</TD>
								<TD style="WIDTH: 14px; HEIGHT: 11px"><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD noWrap width="20%"><asp:dropdownlist id="ddlResign" runat="server" Width="208px"></asp:dropdownlist>
                                    <asp:dropdownlist id="ddlSalesIndicator" Visible="false"  runat="server" Width="208px"></asp:dropdownlist>
									<asp:label id="lblResign" runat="server"></asp:label><asp:requiredfieldvalidator id="Requiredfieldvalidator7" runat="server" ControlToValidate="ddlResign" ErrorMessage="Tipe Resign Harus dipilih">*</asp:requiredfieldvalidator>
								</TD>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD width="1%"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>--%>
							<TR>
								<TD class="titleField">Alasan</TD>
								<TD style="WIDTH: 14px; HEIGHT: 11px"><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD class="titleField" style="HEIGHT: 11px"><asp:textbox onkeypress="TxtKeypress();" id="txtResignReason" onblur="TxtBlur('txtResignReason');"
										runat="server" Width="208px" MaxLength="700" size="22" TextMode="MultiLine"></asp:textbox></TD>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD width="1%"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 141px; HEIGHT: 24px"></TD>
								<TD style="WIDTH: 14px; HEIGHT: 24px"></TD>
								<TD class="titleField" style="HEIGHT: 24px">
                                    <asp:button id="btnSimpan" runat="server" Width="60px" OnClientClick="return confirm('Apakah yakin simpan? Data yang disimpan tidak dapat diubah kembali, silahkan hubungi mmksi jika ada kesalah data.');" Text="Simpan">
                                    </asp:button><asp:button id="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button>
									<asp:Button id="btnBack" runat="server" Text="Kembali"></asp:Button></TD>
								<TD class="titleField" style="HEIGHT: 24px"></TD>
								<TD style="HEIGHT: 24px"></TD>
							</TR>
							<TR>
								<TD class="titleField" colSpan="6"><div id="div1" style="OVERFLOW: auto; HEIGHT: 250px">
                                    <asp:datagrid id="dgSalesmanHeader" runat="server" Width="100%" CellPadding="3" BorderWidth="0px"
											CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True" PageSize="25" AllowPaging="True">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="SalesmanCode" SortExpression="SalesmanCode" HeaderText="Kode">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama">
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Posisi">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblPosisi" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="ResignDate" HeaderText="Tgl Keluar">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblTglKeluar" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<%--<asp:TemplateColumn SortExpression="ResignType" HeaderText="Tipe Resign">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblResignType" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="ResignReason" HeaderText="Alasan">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>--%>
												<asp:TemplateColumn>
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
															CommandName="View">
															<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
														<asp:LinkButton id="lbtnEdit" Visible="false" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 141px; HEIGHT: 24px" colSpan="6"></TD>
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
