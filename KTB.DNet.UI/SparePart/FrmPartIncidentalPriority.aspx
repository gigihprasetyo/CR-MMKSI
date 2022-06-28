<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPartIncidentalPriority.aspx.vb" Inherits="FrmPartIncidentalPriority"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmBannedWord</title>
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
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD class="titlePage" colSpan="3">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">Permintaan Khusus&nbsp;- Daftar Prioritas</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 4px">Prioritas</TD>
					<td style="HEIGHT: 4px">:</td>
					<TD style="HEIGHT: 4px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtPrioritas" onblur="omitSomeCharacter('txtBannedWord','<>?*%$;')"
							runat="server" MaxLength="20" Width="408px"></asp:textbox><asp:requiredfieldvalidator id="ValPrioritas" runat="server" ErrorMessage="*" ControlToValidate="txtPrioritas"></asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 4px">Deskripsi</TD>
					<td style="HEIGHT: 4px">:</td>
					<TD style="HEIGHT: 4px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtDeskripsi" onblur="omitSomeCharacter('txtReplacement','<>?*%$;')"
							runat="server" MaxLength="50" Width="408px"></asp:textbox><asp:requiredfieldvalidator id="ValDeskripsi" runat="server" ErrorMessage="*" ControlToValidate="txtDeskripsi"></asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD></TD>
					<td></td>
					<TD><asp:button id="btnCari" runat="server" Width="56px" CausesValidation="False" Text="Cari"></asp:button><asp:button id="btnSimpan" runat="server" Text="Simpan"></asp:button><asp:button id="btnBatal" runat="server" Width="48px" CausesValidation="False" Text="Batal"></asp:button>&nbsp;
					</TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 300px" DESIGNTIMEDRAGDROP="245"><asp:datagrid id="dgPriority" runat="server" Width="100%" BorderWidth="0px" CellSpacing="1" BorderColor="#CDCDCD"
								CellPadding="3" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowCustomPaging="True" AllowPaging="True" AllowSorting="False" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle ForeColor="White"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Priority" SortExpression="Priority" HeaderText="Prioritas">
										<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
										<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" Text="Lihat" Width="20px" CausesValidation="False"
												CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
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
			</TABLE>
		</form>
	</body>
</HTML>
