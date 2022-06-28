<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEditViewRole.aspx.vb" Inherits="FrmEditViewRole" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmEditViewRole</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
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
		
		function keyDownRoleName(event)
		{
			var txtRoleName = document.getElementById("txtRoleName");
			if (txtRoleName.disabled)
				return false;
			var nResult = HtmlCharUniv(event);
			if (nResult)
			{
				var pressedKey;
				if(navigator.appName == "Microsoft Internet Explorer")	
					pressedKey = event.keyCode;
				else
					pressedKey = event.which
				if (pressedKey == 32)
					return false;
			}
			return nResult;
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">ADMIN SISTEM - Role</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" width="150" style="WIDTH: 150px">Kode Organisasi</TD>
								<td style="WIDTH: 11px">:</td>
								<TD><asp:label id="lblCode" runat="server"></asp:label>&nbsp;/
									<asp:label id="lblSearchTerm1" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="150" style="WIDTH: 150px">Nama Organisasi</TD>
								<td style="WIDTH: 11px">:</td>
								<TD><asp:label id="lblName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="150" style="WIDTH: 150px"></TD>
								<td style="WIDTH: 11px">:</td>
								<TD></TD>
							</TR>
							<TR>
								<TD class="titleField" width="150" style="WIDTH: 150px">Nama Role</TD>
								<td style="WIDTH: 11px">:</td>
								<TD><asp:textbox onkeypress="return keyDownRoleName(event)" id="txtRoleName" runat="server" MaxLength="50"
										Width="500px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtRoleName" Display="Dynamic"
										ErrorMessage="* harus diisi. "></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" ControlToValidate="txtRoleName"
										Display="Dynamic" ErrorMessage="< dan > bukan karakter valid" ValidationExpression="[^\<\>]+"></asp:regularexpressionvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" width="150" style="WIDTH: 150px">Deskripsi</TD>
								<td style="WIDTH: 11px">:</td>
								<TD><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtDescription" runat="server" Width="500px"
										MaxLength="255"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="* harus diisi. " Display="Dynamic"
										ControlToValidate="txtDescription"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="Regularexpressionvalidator2" runat="server" ErrorMessage="< dan > bukan karakter valid"
										Display="Dynamic" ValidationExpression="[^\<\>]+" ControlToValidate="txtDescription"></asp:regularexpressionvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" width="150" style="WIDTH: 150px">Status</TD>
								<td style="WIDTH: 11px">:</td>
								<TD><asp:dropdownlist id="ddlStatus" runat="server" Width="80px">
										<asp:ListItem Value="0">Non Aktif</asp:ListItem>
										<asp:ListItem Value="1">Aktif</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<tr>
								<td colspan="3"></td>
							</tr>
							<tr>
								<TD class="titleField" width="150" style="WIDTH: 150px">
									Pencarian atas Deskripsi
								</TD>
								<td style="WIDTH: 11px">:</td>
								<td>
									<asp:TextBox id="txtFilterDescription" runat="server" Width="304px"></asp:TextBox>
									<asp:Button id="btnCari" runat="server" Width="56px" Text="Cari" CausesValidation="False"></asp:Button>
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 18px"><EM>Silahkan tick hak akses yang akan 
										diberikan</EM></TD>
							</TR>
							<TR>
								<TD>
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 250px"><asp:datagrid id="dtgOrgPrivilege" runat="server" CellSpacing="1" GridLines="Horizontal" CellPadding="3"
											BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" Width="100%" AutoGenerateColumns="False" PageSize="25" AllowSorting="True">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														<asp:CheckBox id="cbAll" runat="server" onclick="CheckAll('cbItem',this.checked)"></asp:CheckBox>
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="cbItem" runat="server"></asp:CheckBox>
														<asp:CheckBox id="cbInitial" style="display:none" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
													<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="Privilege.ID" HeaderText="ID">
													<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Privilege.ID") %>' ID="Label1" NAME="Label1">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Privilege.Name" HeaderText="Nama Hak Akses">
													<HeaderStyle Width="45%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Privilege.Name") %>' ID="lblPrivilegeName">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Privilege.Description" HeaderText="Deskripsi">
													<HeaderStyle Width="45%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Privilege.Description") %>' ID="Label3">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td>
						<asp:Button id="btnBack" runat="server" Width="64px" Text="Kembali" CausesValidation="False"></asp:Button><asp:button id="btnSave" runat="server" Width="60px" Text="Simpan" Enabled="False"></asp:button></td>
				</tr>
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
