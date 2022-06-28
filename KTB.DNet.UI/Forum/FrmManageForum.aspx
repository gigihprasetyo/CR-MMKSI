<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmManageForum.aspx.vb" Inherits="FrmManageForum" smartNavigation="False"%>
<%@ Register TagPrefix="FCKeditorV2" Namespace="CKEditor.NET" Assembly="CKEditor.NET" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmManageForum</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript">
		function ShowPopUpForumMember()
		{
			showPopUp('../PopUp/PopUpForumMember.aspx','',500,760,GetIdMemberSelection);
			//Code below Error on mozilla
			//if(navigator.appName != "Microsoft Internet Explorer")
			//{
			//	var sURL = unescape(window.location.pathname);
			//	window.location.href = sURL;
			//}
			
			
		}
		
		function GetIdMemberSelection(selectedUserId)
		{
			var txtKode = document.getElementById("txtUserID");			
			txtKode.value = selectedUserId;	
			__doPostBack('__Page', 'AddMember');	
			
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 19px">FORUM&nbsp;-
						<asp:Label id="lblTitle" runat="server">Forum Baru</asp:Label></td>
				</tr>
				<tr>
					<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Kategori</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:dropdownlist id="ddlKategori" runat="server"></asp:dropdownlist></td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 27px" width="24%">Title Forum</TD>
								<TD style="HEIGHT: 27px" width="1%">:</TD>
								<td style="HEIGHT: 27px" width="75%"><asp:textbox id="txtTitle" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtTitle','<>?*%$;')"
										runat="server" Width="448px" MaxLength="40"></asp:textbox></td>
							</TR>
							<TR valign="top">
								<TD class="titleField" style="HEIGHT: 27px" width="24%">Forum Deskripsi</TD>
								<TD style="HEIGHT: 27px" width="1%">:</TD>
								<TD style="HEIGHT: 27px" width="75%"><FCKeditorV2:CKEditorControl id="txtDescription" BasePath="../UserControl/fckeditor/" runat="server" /></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 16px" width="24%">Sifat Forum</TD>
								<TD style="HEIGHT: 16px" width="1%">:</TD>
								<TD style="HEIGHT: 16px" width="75%"><asp:dropdownlist id="ddlForum" runat="server">
										<asp:ListItem Value="1">Terbuka</asp:ListItem>
										<asp:ListItem Value="0">Tidak Terbuka</asp:ListItem>
									</asp:dropdownlist>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px" width="24%">Status</TD>
								<TD style="HEIGHT: 26px" width="1%">:</TD>
								<TD style="HEIGHT: 26px" width="75%"><asp:dropdownlist id="ddlStatus" runat="server">
										<asp:ListItem Value="1">Aktif</asp:ListItem>
										<asp:ListItem Value="0">Tidak aktif</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 27px" width="24%"></TD>
								<TD style="HEIGHT: 27px" width="1%"></TD>
								<td style="HEIGHT: 27px" width="75%"><asp:button id="btnSimpan" runat="server" Text="Simpan" width="60px"></asp:button><asp:button id="btnBatal" runat="server" Text="Batal" width="60px" CausesValidation="False"></asp:button><asp:textbox id="txtUserID" runat="server" Width="0px" ForeColor="White" BorderStyle="None"></asp:textbox>
									<INPUT id="btnChoose" style="WIDTH: 96px; HEIGHT: 21px" onclick="ShowPopUpForumMember()"
										type="button" value="Tambah Member" name="btnChoose" runat="server">
									<asp:button id="Button1" runat="server" Text="Back to List Forum" Visible="False"></asp:button>
									<asp:Button id="btnBack" runat="server" Text="Kembali"></asp:Button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 390px"><asp:datagrid id="dtgForumMember" runat="server" Width="100%" PageSize="25" BorderColor="#E0E0E0"
								CellPadding="3" BackColor="Gainsboro" AutoGenerateColumns="False" AllowCustomPaging="True" AllowPaging="True" AllowSorting="True">
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
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="UserInfo.Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label1" Text='<%# DataBinder.Eval(Container, "DataItem.UserInfo.Dealer.DealerCode") %>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="UserInfo.UserName" HeaderText="User ID">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label2" Text='<%# DataBinder.Eval(Container, "DataItem.UserInfo.UserName") %>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="UserInfo.JobPosition.Description" HeaderText="Posisi">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label3" Text='<%# DataBinder.Eval(Container, "DataItem.UserInfo.JobPosition.Description") %>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" Width="16px" CausesValidation="False"
												CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
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
