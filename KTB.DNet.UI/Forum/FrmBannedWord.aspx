<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmBannedWord.aspx.vb" Inherits="FrmBannedWord" smartNavigation="False"%>
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
				<script language="javascript">
			
			function SetICPaymentDate()
			{	
				__doPostBack("PaymentDate","");
			}
			
		</script>

	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD class="titlePage" colSpan="3">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">FORUM - Filter Kata</td>
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
					<TD class="titleField" style="HEIGHT: 4px"><asp:label id="Label5" runat="server"> Banned Word</asp:label></TD>
					<td style="HEIGHT: 4px">:</td>
					<TD style="HEIGHT: 4px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtBannedWord" onblur="omitSomeCharacter('txtBannedWord','<>?*%$;')"
							runat="server" MaxLength="20" Width="408px"></asp:textbox><asp:requiredfieldvalidator id="valCondition" runat="server" ErrorMessage="*" ControlToValidate="txtBannedWord"></asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 4px"><asp:label id="Label1" runat="server"> Replacement</asp:label></TD>
					<td style="HEIGHT: 4px">:</td>
					<TD style="HEIGHT: 4px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtReplacement" onblur="omitSomeCharacter('txtReplacement','<>?*%$;')"
							runat="server" MaxLength="20" Width="408px"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" ErrorMessage="*" ControlToValidate="txtBannedWord"></asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD></TD>
					<td></td>
					<TD><asp:button id="btnSimpan" runat="server" Text="Simpan"></asp:button>&nbsp;
						<asp:button id="btnBatal" runat="server" Width="56px" Text="Batal" CausesValidation="False"></asp:button></TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 300px" DESIGNTIMEDRAGDROP="245"><asp:datagrid id="dtgForumBannedWord" runat="server" Width="100%" BorderColor="#E0E0E0" CellPadding="3"
								BackColor="Gainsboro" AutoGenerateColumns="False" AllowCustomPaging="True" AllowPaging="True" AllowSorting="True" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="BannedWord" SortExpression="BannedWord" HeaderText="Banned Word">
										<HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Replacement" SortExpression="Replacement" HeaderText="Replacement">
										<HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
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
