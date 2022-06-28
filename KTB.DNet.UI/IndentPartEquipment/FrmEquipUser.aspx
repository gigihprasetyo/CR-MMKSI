<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEquipUser.aspx.vb" Inherits="FrmEquipUser" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmEquipUser</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="titlePage">Indent Part Equipment - Daftar Penerima Email
					</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
				</tr>
				<tr>
					<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
				</tr>
				<TR>
					<TD vAlign="top">
						<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="1" width="100%">
							<TR>
								<TD class="titleField" width="24%">Nama User</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:textbox onblur="HtmlCharBlur(txtUserName)" id="txtUserName" onkeypress="return HtmlCharUniv(event)"
										runat="server" Width="288px" MaxLength="50"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtUserName"></asp:requiredfieldvalidator></td>
							</TR>
							<TR>
								<TD style="HEIGHT: 24px" class="titleField">Alamat Email</TD>
								<TD style="HEIGHT: 24px">:</TD>
								<td style="HEIGHT: 24px">
									<P><asp:textbox onblur="HtmlCharBlur(txtEmail)" id="txtEmail" onkeypress="return HtmlCharUniv(event)"
											runat="server" Width="288px" MaxLength="50"></asp:textbox>(Format email: 
										:xxx@xxx.xxx"&gt;)
										<asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" ErrorMessage="* Format Email Salah, Format Email Yang Benar: xxx@xxx.xxx"
											ControlToValidate="txtEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:regularexpressionvalidator><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtEmail"></asp:requiredfieldvalidator></P>
								</td>
							</TR>
							<TR>
								<TD style="HEIGHT: 24px" class="titleField">Jabatan</TD>
								<TD style="HEIGHT: 24px">:</TD>
								<TD style="HEIGHT: 24px"><asp:textbox id="txtPositionCC" runat="server" Width="288px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 24px" class="titleField">Tipe</TD>
								<TD style="HEIGHT: 24px">:</TD>
								<TD style="HEIGHT: 24px">
									<P><asp:dropdownlist id="ddlTipe" runat="server">
											<asp:ListItem Value="0" Selected="True">TO</asp:ListItem>
											<asp:ListItem Value="1">CC</asp:ListItem>
										</asp:dropdownlist></P>
								</TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 24px" class="titleField">Group</TD>
								<TD style="HEIGHT: 24px">:</TD>
								<TD style="HEIGHT: 24px"><asp:dropdownlist id="ddlGroup" runat="server"></asp:dropdownlist></TD>
							</TR>
                            <tr>
                                <td class="titleField">Upload Email</td>
                                <td>:</td>
                                <td>
                                    <INPUT onkeypress="return false;" id="dfPartShop" style="WIDTH: 320px; HEIGHT: 20px" type="file"
										    size="34" name="File1" runat="server">
									    <asp:button id="btnUpload" runat="server" Width="70px" Height="19px" Text="Upload" CausesValidation="false"></asp:button></td>
                            </tr>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<td>
									<P><asp:button id="btnSimpan" runat="server" Text="Simpan" width="60px"></asp:button>&nbsp;
                                        <asp:button id="btnSimpanUpload" runat="server" Text="Simpan" width="60px" CausesValidation="False" Visible="false"></asp:button>&nbsp;
										<asp:button id="btnBatal" runat="server" Text="Batal" width="60px" CausesValidation="False"></asp:button>&nbsp;
										<asp:button id="btnCari" runat="server" width="60px" Text="Cari" CausesValidation="False"></asp:button></P>
								</td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div style="OVERFLOW: auto; HEIGHT: 380px" id="div1"><asp:datagrid id="dtgPIUser" runat="server" Width="100%" Font-Names="MS Reference Sans Serif"
								CellSpacing="1" ForeColor="GhostWhite" PageSize="25" AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" BorderColor="#CDCDCD"
								BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal" AutoGenerateColumns="False">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="UserName" SortExpression="UserName" HeaderText="Nama User">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Email" SortExpression="Email" HeaderText="Email">
										<HeaderStyle Width="6%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PositionCC" SortExpression="PositionCC" HeaderText="Position">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="Tipe" HeaderText="Tipe">
										<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblTipe" Text='<%# DataBinder.Eval(Container, "DataItem.EquipUserTipeDesc") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="GroupType" HeaderText="Group">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblGroupType" Text='<%# DataBinder.Eval(Container, "DataItem.EquipUserGroupDesc") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="4%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="btnLihat" runat="server" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="btnUbah" runat="server" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="btnHapus" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
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
