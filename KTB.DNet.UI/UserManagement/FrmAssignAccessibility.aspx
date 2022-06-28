<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmAssignAccessibility.aspx.vb" Inherits="FrmAssignAccessibility" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ORGANISASI - Hak Akses Organisasi</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
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
					<td class="titlePage">ADMIN SISTEM - Hak Akses Organisasi</td>
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
								<TD class="titleField" width="24%">Tipe Organisasi</TD>
								<td width="1%">:</td>
								<TD width="75%"><asp:label id="lblType" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" width="24%">Kode Organisasi</TD>
								<td width="1%">:</td>
								<TD width="76%"><asp:label id="lblCode" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px">Nama Organisasi</TD>
								<td width="1%">:</td>
								<TD><asp:label id="lblName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Kota</TD>
								<td width="1%">:</td>
								<TD><asp:label id="lblCity" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Search&nbsp;1/2</TD>
								<td width="1%">:</td>
								<TD><asp:label id="lblSearch1" runat="server"></asp:label>&nbsp;/
									<asp:label id="lblSearch2" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px">Pencarian atas Deskripsi</TD>
								<TD style="HEIGHT: 18px">:</TD>
								<td>
									<asp:TextBox id="txtDescription" runat="server" Width="232px"></asp:TextBox>
									<asp:Button id="btnSearch" runat="server" Width="72px" Text="Cari"></asp:Button></td>
							</TR>
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
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 336px"><asp:datagrid id="dtgPrivilege" runat="server" PageSize="25" CellSpacing="1" AllowSorting="True"
											GridLines="Horizontal" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AutoGenerateColumns="False"
											Width="100%">
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
														<asp:CheckBox id="cbAll" onclick="CheckAll('cbItem',this.checked)" runat="server"></asp:CheckBox>
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="cbItem" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="ID" SortExpression="ID" HeaderText="ID">
													<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama Hak Akses">
													<HeaderStyle Width="45%" CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
													<HeaderStyle Width="45%" CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>
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
						<asp:Button id="btnBack" runat="server" Width="64px" Text="Kembali"></asp:Button><asp:button id="btnSave" runat="server" Width="60px" Text="Simpan" Enabled="False"></asp:button></td>
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
