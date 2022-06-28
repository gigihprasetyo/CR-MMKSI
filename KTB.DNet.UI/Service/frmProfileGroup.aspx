<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmProfileGroup.aspx.vb" Inherits="frmProfileGroup" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>frmProfileGroup</title>
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
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TBODY>
					<TR>
						<td colspan="2">
							<TABLE id="titlePage" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td class="titlePage">UMUM &nbsp;- Profile Group</td>
								</tr>
								<tr>
									<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
								</tr>
								<tr>
									<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
								</tr>
							</TABLE>
						</td>
					</TR>
					<TR>
						<TD class="titleField"><asp:label id="Label1" runat="server">Kode</asp:label></TD>
						<TD colspan="2"><asp:textbox id="txtCode" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$; ')"
								onblur="omitSomeCharacter('txtCode','<>?*%$; ')"></asp:textbox>
							<asp:RequiredFieldValidator id="rfvKode" runat="server" ErrorMessage="Kode Harus Diisi" ControlToValidate="txtCode">*</asp:RequiredFieldValidator></TD>
					</TR>
					<TR>
						<TD class="titleField"><asp:label id="Label2" runat="server">Deskripsi</asp:label></TD>
						<TD colspan="2"><asp:textbox id="txtDescription" runat="server" Width="214px"></asp:textbox>
							<asp:RequiredFieldValidator id="rfvDeskripsi" runat="server" ErrorMessage="Deskripsi harus diisi" ControlToValidate="txtDescription">*</asp:RequiredFieldValidator></TD>
					</TR>
					<TR>
						<TD></TD>
						<TD><asp:button id="btnSave" runat="server" Text="Simpan"></asp:button><asp:button id="btnCancel" runat="server" Text="Batal"></asp:button>
							<!--<asp:button id="btnSearch" runat="server" Text="Cari"></asp:button> -->
						</TD>
					</TR>
					<TR>
						<TD colSpan="2"><div id="div1" style="OVERFLOW: auto; HEIGHT: 360px"><asp:datagrid id="dgProfileGroup" runat="server" Width="100%" BorderColor="#CCCCCC" BorderStyle="None"
									BorderWidth="1px" BackColor="White" CellPadding="3" AllowSorting="True" AutoGenerateColumns="False" AllowCustomPaging="True" AllowPaging="True" PageSize="100">
									<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
									<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
									<ItemStyle ForeColor="#000066"></ItemStyle>
									<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#006699"></HeaderStyle>
									<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
									<Columns>
										<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
										<asp:BoundColumn HeaderText="No">
											<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Code" SortExpression="Code" HeaderText="Posisi">
											<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
											<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
										</asp:BoundColumn>
										<asp:TemplateColumn>
											<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
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
												<asp:LinkButton id="titleTableGeneral" runat="server" Text="Ubah" Width="20px" CausesValidation="False"
													CommandName="ProfileHeader" ToolTip="ProfileHeader">
													<img src="../images/set.gif" border="0" alt="Set Header"></asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Right" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
								</asp:datagrid></div>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
		</TD></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
