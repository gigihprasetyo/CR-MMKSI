<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmMasterJobPosition.aspx.vb" Inherits="FrmMasterJobPosition" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmMasterJobPosition</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" width="100%" cellSpacing="1" cellPadding="1" border="0">
				<TR>
					<TD class="titlePage" colSpan="3">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">MAINTENANCE - Posisi Jabatan</td>
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
					<TD>
						<asp:Label id="Label1" runat="server">Posisi</asp:Label></TD>
					<TD>
						<asp:TextBox id="txtCode" runat="server" Width="180px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
							onblur="omitSomeCharacter('txtCode','<>?\/*%$');"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label2" runat="server">Deskripsi</asp:Label></TD>
					<TD>
						<asp:TextBox id="txtDescription" runat="server" Width="180px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
							onblur="omitSomeCharacter('txtDescription','<>?\/*%$');"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD><asp:Label id="Label3" runat="server">Kategori</asp:Label></TD>
					</TD>
					<td><asp:dropdownlist id="ddlCategory" runat="server" Width="180px" AutoPostBack="True"></asp:dropdownlist></td>
				</TR>
				<TR>
					<TD><asp:Label id="lblMenu" runat="server">Menu</asp:Label></TD>
					</TD>
					<td>
						<asp:listbox id="lstBoxMenu" runat="server" SelectionMode="Multiple" Rows="3" Width="180px"></asp:listbox>
						<!--<asp:CheckBoxList id="cblMenu" runat="server"></asp:checkboxlist>-->
					</td>
				</TR>
				<TR>
					<TD></TD>
					<TD>
						<asp:Button id="btnSave" runat="server" Text="Simpan" Width="70px"></asp:Button>
						<asp:Button id="btnCancel" runat="server" Text="Batal" Width="70px"></asp:Button>
						<asp:Button id="btnSearch" runat="server" Text="Cari" Width="70px"></asp:Button></TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<div id="div1" style="HEIGHT: 370px; OVERFLOW: auto">
							<asp:DataGrid id="dgJobPosition" Width="100%" runat="server" BorderColor="#E0E0E0" BorderStyle="None"
								BackColor="Gainsboro" CellPadding="3" AllowSorting="True" AutoGenerateColumns="False" AllowCustomPaging="True"
								AllowPaging="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Code" SortExpression="Code" HeaderText="Posisi">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
										<HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Category" SortExpression="Category" HeaderText="Kategori">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" Text="Lihat" CommandName="View" CausesValidation="False"
												Width="20px">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" CommandName="Edit" CausesValidation="False"
												Width="20px">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" CommandName="Delete" CausesValidation="False"
												Width="16px">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:DataGrid>
						</div>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
