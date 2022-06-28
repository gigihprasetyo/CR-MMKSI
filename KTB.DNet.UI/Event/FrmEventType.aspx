<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEventType.aspx.vb" Inherits="FrmEventType" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmEventType</title>
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
								<td class="titlePage">Event&nbsp;- Jenis Event</td>
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
					<TD class="titleField" style="WIDTH: 96px; HEIGHT: 4px">Jenis Event</TD>
					<td style="WIDTH: 5px; HEIGHT: 4px">:</td>
					<TD style="HEIGHT: 4px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtEventType" onblur="omitSomeCharacter('txtEventType','<>?*%$;')"
							runat="server" MaxLength="30" Width="176px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 96px; HEIGHT: 4px"></TD>
					<td style="WIDTH: 5px; HEIGHT: 4px"></td>
					<TD style="HEIGHT: 4px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 96px"></TD>
					<td style="WIDTH: 5px"></td>
					<TD><asp:button id="btnSimpan" runat="server" Text="Simpan"></asp:button>&nbsp;
						<asp:button id="btnBatal" runat="server" Width="56px" Text="Batal" CausesValidation="False"></asp:button></TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 380px" DESIGNTIMEDRAGDROP="245"><asp:datagrid id="dtgEventType" runat="server" Width="100%" BorderColor="#E0E0E0" CellPadding="3"
								BackColor="Gainsboro" AutoGenerateColumns="False" AllowCustomPaging="True" AllowPaging="True" AllowSorting="True" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
										<HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Kegiatan">
										<HeaderStyle Width="30%" CssClass="titleTablePromo"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
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
