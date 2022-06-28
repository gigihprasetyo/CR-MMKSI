<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEventParameter.aspx.vb" Inherits="FrmEventParameter" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmEventParameter</title>
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
			function TxtKeypressNum()
			{
				return NumericOnlyWith(event,'');
			}
			function TxtBlurNum(objTxtNum)
			{
				NumericOnlyBlurWith(objTxtNum,'');
			}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">EVENT - Parameter EVENT</td>
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
								<TD class="titleField" style="WIDTH: 150px; HEIGHT: 25px" width="150">No Event</TD>
								<TD style="HEIGHT: 25px" width="1%"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 25px" width="75%" colSpan="3"><asp:label id="lblEventNo" runat="server" Width="104px"></asp:label><asp:textbox id="txtEventNo" runat="server"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 10px" width="215">Periode</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="65%" colSpan="3"><asp:dropdownlist id="ddlPeriod" runat="server"></asp:dropdownlist><asp:dropdownlist id="ddlStartMonth" runat="server"></asp:dropdownlist>&nbsp;s/d
									<asp:dropdownlist id="ddlEndMonth" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 19px" width="215">Upload File 
									Material</TD>
								<TD style="HEIGHT: 19px" width="1%">:</TD>
								<TD style="HEIGHT: 19px" width="65%" colSpan="3"><INPUT onkeypress="return false;" id="upFileMaterialName" style="WIDTH: 296px; HEIGHT: 20px"
										type="file" size="30" name="fileUpload" runat="server"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 10px" width="215">Petunjuk 
									Pelaksanaan</TD>
								<TD style="HEIGHT: 10px" width="1%">:</TD>
								<TD style="HEIGHT: 10px" width="65%" colSpan="3"><INPUT onkeypress="return false;" id="upFileDirectionName" style="WIDTH: 296px; HEIGHT: 20px"
										type="file" size="30" name="fileUpload" runat="server"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 26px" width="215">Form 
									Pengajuan Biaya</TD>
								<TD style="HEIGHT: 26px" width="1%">:</TD>
								<TD style="HEIGHT: 26px" width="65%" colSpan="3"><INPUT onkeypress="return false;" id="upFileProposalName" style="WIDTH: 296px; HEIGHT: 20px"
										type="file" size="30" name="fileUpload" runat="server"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnBatal" runat="server" Width="60px" Text="Batal"></asp:button><asp:button id="btnCari" runat="server" Width="56px" Text="Cari"></asp:button></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD colSpan="5">
									<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 280px" DESIGNTIMEDRAGDROP="245"><asp:datagrid id="dtgEventParameter" runat="server" Width="100%" PageSize="25" AllowSorting="True"
											AllowPaging="True" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="Gainsboro" CellPadding="3" BorderColor="#E0E0E0">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="White" VerticalAlign="Top"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
													<HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblNo"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="EventNo" SortExpression="EventNo" HeaderText="Event No">
													<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Period" SortExpression="Period" HeaderText="Periode">
													<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="StartMonth" HeaderText="Mulai">
													<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblStartMonth"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="EndMonth" HeaderText="Akhir">
													<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblEndMonth"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="FileMaterialName" HeaderText="File Material">
													<HeaderStyle Width="25%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblFileMaterialName"></asp:Label>
														<asp:LinkButton id="lbtnPopUp" runat="server" Text="Download" Width="20px" CausesValidation="False"
															CommandName="Download">
															<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="FileDirectionName" HeaderText="File Petunjuk Pelaksana">
													<HeaderStyle Width="25%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblFileDirectionName"></asp:Label>
														<asp:LinkButton id="lbtnFileDirectionName" runat="server" Text="Download" Width="20px" CausesValidation="False"
															CommandName="Download">
															<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="FileProposalName" HeaderText="Form Pengajuan Biaya">
													<HeaderStyle Width="25%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblFileProposalName"></asp:Label>
														<asp:LinkButton id="lbtnView" runat="server" Text="Lihat" Width="20px" CausesValidation="False"
															CommandName="Download2">
															<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" Width="20px" CausesValidation="False" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" Width="16px" CausesValidation="False"
															CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></DIV>
								</TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
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
