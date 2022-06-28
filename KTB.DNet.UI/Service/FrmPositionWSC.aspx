<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPositionWSC.aspx.vb" Inherits="FrmPositionWSC" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPositionWSC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">WSC - Daftar Kode ABC</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD widht="24%" class="titleField"><asp:label id="Label1" runat="server">Kategori</asp:label></TD>
								<TD widht="1%"><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD widht="75%"><asp:dropdownlist id="ddlCategory" runat="server" Width="90px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label2" runat="server">Kode</asp:label></TD>
								<TD><asp:label id="Label5" runat="server">:</asp:label></TD>
								<TD><asp:textbox id="txtCode" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharUniv(txtCode)"
										runat="server" Width="90px" MaxLength="4"></asp:textbox><asp:requiredfieldvalidator id="ValidCode" runat="server" ErrorMessage="*" ControlToValidate="txtCode"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label3" runat="server">Deskripsi</asp:label></TD>
								<TD><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD><asp:textbox id="txtDescription" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharUniv(txtDescription)"
										runat="server" Width="250px" MaxLength="30"></asp:textbox><asp:requiredfieldvalidator id="ValidDescription" runat="server" ErrorMessage="*" ControlToValidate="txtDescription"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD><asp:button id="btnSave" runat="server" Text="Simpan"></asp:button>
									<asp:button id="btnCancel" runat="server" Width="64px" Text="Batal" CausesValidation="False"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD valign="top"><div id="div1" style="OVERFLOW: auto; HEIGHT: 370px"><asp:datagrid id="dgPosWSC" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
								AllowSorting="True" AllowCustomPaging="True" pagesize="50" BackColor="#CDCDCD" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3">
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="PositionCategory" SortExpression="PositionCategory" HeaderText="Kategori">
										<HeaderStyle ForeColor="White" Width="25%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PositionCode" SortExpression="PositionCode" HeaderText="Kode">
										<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
										<HeaderStyle ForeColor="White" Width="35%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnkView" runat="server" CommandName="View" CausesValidation="False">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnkEdit" runat="server" CommandName="Edit" CausesValidation="False">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnkDelete" runat="server" CommandName="Delete" CausesValidation="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
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
