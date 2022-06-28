<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmArea2.aspx.vb" Inherits="FrmArea2" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>MAINTENANCE - Area 2</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">MAINTENANCE - Area 2</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><img src="../images/bg_hor.gif" height="1" border="0"></td>
				</tr>
				<tr>
					<td height="10"><img src="../images/dot.gif" height="1" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD width="24%" class="titleField" style="HEIGHT: 25px">Kode Area</TD>
								<TD width="1%" style="HEIGHT: 25px">:</TD>
								<td width="75%" style="HEIGHT: 25px">
									<asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtKodeArea)" id="txtKodeArea" runat="server" MaxLength="10"
										Width="112px"></asp:textbox>
									<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtKodeArea"></asp:requiredfieldvalidator></td>
							</TR>
							<TR>
								<TD class="titleField">Nama Area</TD>
								<TD>:</TD>
								<td>
									<asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtNamaArea)" id="txtNamaArea" runat="server" Width="250px"
										MaxLength="40"></asp:textbox>&nbsp;<asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtNamaArea"></asp:requiredfieldvalidator></td>
							</TR>
							<TR style="display:none">
								<TD class="titleField">ACFinishUnit</TD>
								<TD>:</TD>
								<td>
									<asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtACFinishUnit)" id="txtACFinishUnit" runat="server" Width="250px"
										MaxLength="50"></asp:textbox>&nbsp;
                                    <%--<asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtACFinishUnit"></asp:requiredfieldvalidator>--%></td>
							</TR>
							<TR style="display:none">
								<TD class="titleField">ACSparePart</TD>
								<TD>:</TD>
								<td>
									<asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtACSparePart)" id="txtACSparePart" runat="server" Width="250px"
										MaxLength="50"></asp:textbox>&nbsp;
                                    <%--<asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txtACSparePart"></asp:requiredfieldvalidator>--%></td>
							</TR>
							<TR style="display:none">
								<TD class="titleField">ACService</TD>
								<TD>:</TD>
								<td>
									<asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtACService)" id="txtACService" runat="server" Width="250px"
										MaxLength="50"></asp:textbox>&nbsp;
                                    <%--<asp:requiredfieldvalidator id="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="txtACService"></asp:requiredfieldvalidator>--%></td>
							</TR>
                            <TR>
								<TD class="titleField">Area 1</TD>
								<TD>:</TD>
								<td>
                                    <asp:DropDownList ID="ddlArea1" runat="server"></asp:DropDownList>
								</td>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<td>
									<asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><div id="div1" style="OVERFLOW: auto; HEIGHT: 390px"><asp:datagrid id="dtgArea" runat="server" Width="100%" GridLines="None"  CellPadding="3"
								BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AllowCustomPaging="True" AllowPaging="True" AllowSorting="True"
								PageSize="25" AutoGenerateColumns="False" CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="ID">
										<HeaderStyle Width="2%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="AreaCode" SortExpression="AreaCode" HeaderText="Kode Area">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Nama Area">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="false" DataField="ACFinishUnit" SortExpression="ACFinishUnit" HeaderText="ACFinishUnit">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="false" DataField="ACSparePart" SortExpression="ACSparePart" HeaderText="ACSparePart">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="false" DataField="ACService" SortExpression="ACService" HeaderText="ACService">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Area 1" SortExpression="Area1">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblArea1"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
												CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
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
