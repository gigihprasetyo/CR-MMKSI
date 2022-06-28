<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanArea.aspx.vb" Inherits="FrmSalesmanArea" smartNavigation="False"%>
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

			//function js untuk handle alphanumeric, dengan menghilangkan karakter numeric
			function alphaNumericNonNumeric(event)
			{	
				if(navigator.appName == "Microsoft Internet Explorer")	
					pressedKey = event.keyCode;
				else
					pressedKey = event.which
				
				if ((pressedKey == 32) || (pressedKey >=97 && pressedKey <=122) || (pressedKey >=65 && pressedKey <=90))
				{
					return true;
				}
				else
				{	
					return false;
				}
			}
			
			function TxtBlurNonNumeric(objTxt)
			{
				omitSomeCharacter(objTxt,'<>?*%$;0123456789');
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">TENAGA PENJUAL&nbsp;- Area Tenaga 
						Penjual</td>
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
								<TD class="titleField" style="HEIGHT: 17px" width="24%">Kode Area</TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 17px" width="25%"><asp:textbox id="txtAreaCode" runat="server" MaxLength="20" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$; ');"
										onblur="omitCharsOnCompsTxt(this,'<>?*%$; ')"></asp:textbox></TD>
								<TD class="titleField" style="WIDTH: 152px; HEIGHT: 17px" width="152"></TD>
								<TD style="HEIGHT: 17px" width="1%"></TD>
								<TD style="HEIGHT: 17px" width="29%"><asp:textbox id="txtCity" runat="server" MaxLength="50" size="22" onkeypress="return alphaNumericNonNumeric(event);"
										onblur="TxtBlurNonNumeric('txtCity');" Visible="False"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Deskripsi Area</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="25%"><asp:textbox id="txtAreaDesc" runat="server" MaxLength="50" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										onblur="omitSomeCharacter('txtAreaDesc','<>?*%$;')"></asp:textbox></TD>
								<TD class="titleField" style="WIDTH: 152px; HEIGHT: 10px" width="152"></TD>
								<TD style="HEIGHT: 10px" width="1%"></TD>
								<TD style="HEIGHT: 10px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" width="24%"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" noWrap width="25%">
									<asp:button id="btnSimpan" runat="server" Text="Simpan" Width="60px"></asp:button>
									<asp:button id="btnBatal" runat="server" Text="Batal" Width="60px" CausesValidation="False"></asp:button>
									<asp:button id="btnSearch" runat="server" Text="Cari" Width="60px"></asp:button></TD>
								<TD class="titleField" style="WIDTH: 152px; HEIGHT: 20px" width="152"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD class="titleField" style="WIDTH: 152px; HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 330px"><asp:datagrid id="dgSalesmanArea" runat="server" Width="100%" CellPadding="3" BorderWidth="0px"
											CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True" PageSize="25"
											AllowPaging="True">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="AreaCode" SortExpression="AreaCode" HeaderText="Kode Area">
													<HeaderStyle Width="10%" CssClass="titleTableRsd"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="AreaDesc" SortExpression="AreaDesc" HeaderText="Deskripsi Area">
													<HeaderStyle Width="23%" CssClass="titleTableRsd"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="City" SortExpression="City" HeaderText="Kota">
													<HeaderStyle Width="20%" CssClass="titleTableRsd"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
															CommandName="View">
															<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
														<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
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
					<TD style="HEIGHT: 8px"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
