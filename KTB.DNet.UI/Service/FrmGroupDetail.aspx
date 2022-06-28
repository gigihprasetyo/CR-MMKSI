<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmGroupDetail.aspx.vb" Inherits="FrmGroupDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Form User Info</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">ADMIN USER - Ubah Role User
					</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<table cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TBODY>
								<tr>
									<td class="titleField" width="24%">Kode Organisasi</td>
									<td width="1%">:</td>
									<td width="75%"><asp:label id="LblKodeOrg" runat="server" Width="192px">ORG</asp:label></td>
								</tr>
								<tr>
									<td class="titleField">Nama Organisasi</td>
									<td width="1%">:</td>
									<td><asp:label id="LblNamaOrg" runat="server" Width="192px">ORG</asp:label></td>
								</tr>
								<tr>
									<td class="titleField">ID</td>
									<td width="1%">:</td>
									<td><asp:label id="LblID" runat="server" Width="192px">ORG</asp:label></td>
								</tr>
								<tr>
									<td class="titleField">Nama Login</td>
									<td width="1%">:</td>
									<td><asp:label id="LblLogin" runat="server" Width="192px">ORG</asp:label></td>
								</tr>
								<tr>
									<td></td>
									<td width="1%"></td>
									<td><asp:label id="lblerror" runat="server"></asp:label></td>
								</tr>
							</TBODY>
						</table>
						<table width="100%">
							<tr>
								<td width="30%"><asp:datagrid id="dtgUserRole1" runat="server" Width="100%" CellSpacing="1" GridLines="Horizontal"
										CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False"
										AllowSorting="True">
										<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
										<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
										<ItemStyle BackColor="White"></ItemStyle>
										<HeaderStyle Font-Bold="True" ForeColor="White"></HeaderStyle>
										<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
												<HeaderStyle Width="0%" CssClass="titleTableGeneral"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="No">
												<HeaderStyle Width="0%" CssClass="titleTableGeneral"></HeaderStyle>
											</asp:TemplateColumn>
											<asp:TemplateColumn>
												<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<HeaderTemplate>
													<asp:CheckBox id="cbAll" onclick="CheckAll('cbItem',this.checked)" runat="server"></asp:CheckBox>
												</HeaderTemplate>
												<ItemTemplate>
													<asp:CheckBox id="cbItem" runat="server"></asp:CheckBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="RoleName" SortExpression="RoleName" HeaderText="Nama Role">
												<HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
												<HeaderStyle Width="50%" CssClass="titleTableGeneral"></HeaderStyle>
											</asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Right" ForeColor="#404040" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></td>
							</tr>
							<tr>
								<td><asp:button id="btnBatal" runat="server" Width="60px" CausesValidation="False" Text="Kembali"></asp:button><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button></td>
							</tr>
						</table>
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
