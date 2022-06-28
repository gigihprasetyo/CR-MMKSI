<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmCompetitorBrand.aspx.vb" Inherits="FrmCompetitorBrand" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Maintenance Merek Kompetitor</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="FrmSalesmanLevel" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">DEALER REPORT - Merek Kompetitor</td>
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
								<TD class="titleField" style="WIDTH: 88px" width="88">Kode</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 262px" width="262"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;+=`~{}');" id="txtKode" onblur="omitSomeCharacter('txtKode','<>?*%$; ');"
										runat="server" MaxLength="10" Width="100px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 88px" width="88">Deskripsi</TD>
								<TD width="1%">:</TD>
								<td style="WIDTH: 262px" width="262"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;=+`~{}');" id="txtDeskripsi"
										onblur="omitSomeCharacter('txtDeskripsi','<>?*%$;');" MaxLength="50" Width="500px" Runat="server" TextMode="SingleLine" Rows="3"></asp:textbox></td>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 88px; HEIGHT: 15px" width="88">Status</TD>
								<TD style="HEIGHT: 15px" width="1%">:</TD>
								<TD style="WIDTH: 262px; HEIGHT: 15px" width="262"><asp:dropdownlist id="ddlStatus" tabIndex="12" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 88px" width="88"></TD>
								<TD width="1%"></TD>
								<td style="WIDTH: 262px" width="262"><asp:button id="btnSave" runat="server" Text="Simpan" width="70px"></asp:button>&nbsp;
									<asp:button id="btnCari" runat="server" Text="Cari" width="70px"></asp:button>&nbsp;
									<asp:button id="btnCancel" runat="server" Text="Batal" width="70px"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 340px"><asp:datagrid id="dgCompetitorType" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
								AutoGenerateColumns="False" BorderColor="Gainsboro" BorderWidth="0px" AllowPaging="True" AllowCustomPaging="True" BackColor="#CDCDCD" CellPadding="3"
								PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle VerticalAlign="Top" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="2%" CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Code" HeaderText="Kode">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Code") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Description" HeaderText="Deskripsi">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblDescription Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableMrk"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>&nbsp;
											<asp:LinkButton id="lbtnActivation" runat="server" CommandName="Activation">
												<img src="../images/aktif.gif" border="0" alt="Klik Untuk Tidak meng-aktifkan"></asp:LinkButton>
											<asp:LinkButton id="lbtnInactivation" runat="server" CommandName="Activation">
												<img src="../images/in-aktif.gif" border="0" alt="Klik Untuk Meng-aktifkan"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD height="40"></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
