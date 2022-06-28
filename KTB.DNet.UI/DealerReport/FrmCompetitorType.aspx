<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmCompetitorType.aspx.vb" Inherits="FrmCompetitorType" smartNavigation="False"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Maintenance Tipe Kompetitor</title>
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
					<td class="titlePage">DEALER REPORT - Tipe Kompetitor</td>
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
								<TD class="titleField" style="WIDTH: 88px" width="88">Kelas</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 262px" width="262"><asp:dropdownlist id="ddlKelas" runat="server" Width="200px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 88px" width="88">Merek</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 262px" width="262"><asp:dropdownlist id="ddlMerek" runat="server" Width="200px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 88px" width="88">Kode</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 262px" width="262"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;+=`~{}');" id="txtKode" onblur="omitSomeCharacter('txtKode','<>?*%$;');"
										runat="server" Width="200px"></asp:textbox></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" style="WIDTH: 88px" width="88">Deskripsi</TD>
								<TD width="1%">:</TD>
								<td style="WIDTH: 262px" width="262"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;=+`~{}');" id="txtDeskripsi"
										onblur="omitSomeCharacter('txtDeskripsi','<>?*%$;');" Width="200px" Runat="server"></asp:textbox></td>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 88px" width="88"></TD>
								<TD width="1%"></TD>
								<td style="WIDTH: 262px" width="262"><asp:button id="btnSave" runat="server" width="70px" Text="Simpan"></asp:button><asp:button id="btnBatal" runat="server" width="70px" Text="Batal"></asp:button>&nbsp;
									<asp:button id="btnCari" runat="server" width="70px" Text="Cari"></asp:button><asp:button id="btnImport" runat="server" width="70px" Text="Import"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 320px"><asp:datagrid id="dgCompetitorType" runat="server" Width="100%" PageSize="25" CellPadding="3"
								BackColor="#CDCDCD" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False" CellSpacing="1"
								AllowSorting="True">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="2%" CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="VehicleClass.Description" HeaderText="Kelas">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblClassCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VehicleClass.Description") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CompetitorBrand.Description" HeaderText="Merek">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label2" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CompetitorBrand.Description") %>'>
											</asp:Label>
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
											<asp:LinkButton id="Linkbutton1" runat="server" CommandName="Activation"></asp:LinkButton>
											<asp:LinkButton id="lbtnInactivation" runat="server" CommandName="Activation" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
												<img src="../images/in-aktif.gif" border="0" alt="Klik Untuk Meng-aktifkan"></asp:LinkButton>
											<asp:LinkButton id="lbtnActivation" runat="server" CommandName="Activation">
												<img src="../images/aktif.gif" border="0" alt="Klik Untuk Tidak meng-aktifkan"></asp:LinkButton>
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
