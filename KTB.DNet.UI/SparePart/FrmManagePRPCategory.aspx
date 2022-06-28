<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmManagePRPCategory.aspx.vb" Inherits="KTB.DNet.UI.SparePart.FrmManagePRPCategory" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmManagePRPCategory</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 8px">PARTSHOP REWARD 
						PROGRAM&nbsp;-&nbsp;Pengelolaan Kategori PRP</td>
				</tr>
				<tr>
					<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 8px" height="8"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 14px" width="24%">Nama Kategori</TD>
								<TD style="HEIGHT: 14px" width="1%">:</TD>
								<TD noWrap><asp:textbox id="txtCategoryName" runat="server" MaxLength="30" Width="272px" onkeypress="return HtmlCharUniv(event)"
										onblur="HtmlCharBlur(txtCategoryName)"></asp:textbox><asp:requiredfieldvalidator id="rfv1" runat="server" EnableClientScript="False" Display="Dynamic" ControlToValidate="txtCategoryName"
										ErrorMessage="* masih kosong."></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" Display="Dynamic" ControlToValidate="txtCategoryName"
										ErrorMessage="< dan > bukan karakter valid" ValidationExpression="[^\<\>]+"></asp:regularexpressionvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 27px" width="24%">Deskripsi</TD>
								<TD style="HEIGHT: 27px" width="1%">:</TD>
								<TD style="HEIGHT: 27px" noWrap><asp:textbox id="txtDescription" runat="server" MaxLength="30" Width="272px" onkeypress="return HtmlCharUniv(event)"
										onblur="HtmlCharBlur(txtDescription)"></asp:textbox><asp:requiredfieldvalidator id="rfv2" runat="server" EnableClientScript="False" ControlToValidate="txtDescription"
										ErrorMessage="* masih kosong." Display="Dynamic"></asp:requiredfieldvalidator>
									<asp:RegularExpressionValidator id="RegularExpressionValidator2" runat="server" Display="Dynamic" ControlToValidate="txtDescription"
										ErrorMessage="< dan > bukan karakter valid" ValidationExpression="[^\<\>]+"></asp:RegularExpressionValidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 19px" width="24%">
									<P>Status</P>
								</TD>
								<TD style="HEIGHT: 19px" width="1%">:</TD>
								<TD style="HEIGHT: 19px" width="261"><asp:dropdownlist id="ddlStatus" runat="server" Width="206px"></asp:dropdownlist><asp:requiredfieldvalidator id="rfv3" runat="server" EnableClientScript="False" ControlToValidate="ddlStatus"
										ErrorMessage="*"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px"></TD>
								<TD style="HEIGHT: 20px"></TD>
								<TD style="WIDTH: 261px; HEIGHT: 20px"><asp:button id="btnSimpan" runat="server" Width="70px" Text="Simpan"></asp:button><asp:button id="btnBatal" runat="server" Width="70px" Text="Batal" CausesValidation="False"></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="WIDTH: 261px; HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 360px"><asp:datagrid id="dtgPRPCategory" runat="server" Width="100%" AllowSorting="True" CellPadding="3"
											BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowCustomPaging="True" AllowPaging="True"
											PageSize="25">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
													<HeaderStyle Width="0%" CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblNo" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="CategoryName" SortExpression="CategoryName" HeaderText="Kategori">
													<HeaderStyle Width="38%" CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
													<HeaderStyle Width="40%" CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblStatus" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="7px" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:LinkButton id="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Edit">
														</asp:LinkButton>
														<asp:LinkButton id="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Edit">
														</asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Mode="NumericPages" HorizontalAlign="Right"></PagerStyle>
										</asp:datagrid></DIV>
								</TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6"></TD>
							</TR>
						</TABLE>
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
