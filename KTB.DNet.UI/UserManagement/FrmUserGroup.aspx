<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmUserGroup.aspx.vb" Inherits="FrmUserGroup" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmUserGroup</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">


		function ShowPPGroupMember() {
			showPopUp('../PopUp/PopUpGroupMember.aspx','',600,600,BulletinMember);
		}
		
		function BulletinMember(selectedMember)
		{
			var txtGroupMemberSelection = document.getElementById("txtGroupMember");
			txtGroupMemberSelection.value = selectedMember;
			if(navigator.appName == "Microsoft Internet Explorer")
			{
				txtGroupMemberSelection.focus();
				txtGroupMemberSelection.blur();
			}
			else
			{
				txtGroupMemberSelection.onchange();
			}
		}
		
		function CheckAll(aspCheckBoxID, checkVal) 
		{
			re = new RegExp(':' + aspCheckBoxID + '$')  
			for(i = 0; i < document.forms[0].elements.length; i++) {
				elm = document.forms[0].elements[i]
				if (elm.type == 'checkbox') {
					if (re.test(elm.name)) {
						elm.checked = checkVal
					}
				}
			}
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">ADMIN SISTEM - User Group</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td vAlign="top" align="left">
						<table cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>
								<td class="titleField" width="24%">User Group</td>
								<td width="1%">:</td>
								<td width="75%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$; ')" id="txtUserGroup" onblur="omitSomeCharacter('txtUserGroup','<>?*%$; ')"
										runat="server" Width="192px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator10" runat="server" ErrorMessage="User Group harus diisi"
										ControlToValidate="txtUserGroup" Font-Bold="True">*</asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<td class="titleField" width="24%">Deskripsi</td>
								<td width="1%">:</td>
								<td width="75%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtDeskripsi" onblur="omitSomeCharacter('txtDeskripsi','<>?*%$;')"
										runat="server" Width="192px"></asp:textbox></td>
							</tr>
							<tr>
								<td width="24%"></td>
								<td width="1%"></td>
								<td width="75%"><asp:button id="btnSimpan" Text="Simpan" Runat="server"></asp:button><asp:button id="btnBatal" Width="48px" Text="Batal" Runat="server"></asp:button><asp:button id="btnCari" runat="server" Width="48px" Text="Cari" CausesValidation="False"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<table cellSpacing="1" cellPadding="0" width="100%">
							<tr>
								<td>
									<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 360px"><asp:datagrid id="dtgUserRole1" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
											BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0" CellPadding="3" GridLines="Horizontal" CellSpacing="1" AllowCustomPaging="True"
											AllowPaging="True">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="WhiteSmoke" BackColor="OrangeRed"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn Visible="False" HeaderText="ID">
													<ItemTemplate>
														<asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="0%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblNo" runat="server" text= '<%# container.itemindex+1 %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="Code" SortExpression="Code" HeaderText="User Group">
													<HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
													<HeaderStyle Width="55%" CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Aksi">
													<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
															CommandName="View">
															<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
														<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False"
															CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#404040" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></DIV>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
