<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmEquipmentMaster.aspx.vb" Inherits="frmEquipmentMaster" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EquipmentMaster</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="/WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td colspan="3">
						<TABLE id="Table12" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">EQUIPMENT REPAIR - Master Equipment</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD WIDTH="24%" class="titleField"><asp:label id="Label1" runat="server">Pilih Lokasi File</asp:label></TD>
					<TD WIDTH="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
					<TD WIDTH="75%"><INPUT id="FileText" type="file" name="File1" runat="server" onkeypress="return false;">
						<asp:button id="btnUpload" runat="server" Width="72px" Text="Upload"></asp:button>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 110px; HEIGHT: 10px" colspan="3"></TD>
				</TR>
				<TR>
					<TD colSpan="3" valign="top"><div id="div1" style="OVERFLOW: auto; HEIGHT: 400px"><asp:datagrid id="dgEquipmentUpload" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#CDCDCD"
								BorderWidth="0px" CellPadding="3" BackColor="#CDCDCD" CellSpacing="1">
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle ForeColor=white></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="" Visible="false">
										<HeaderStyle Width="0%" CssClass="titleTableService"></HeaderStyle>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="EquipmentNumber" HeaderText="Kode Equipment">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Group">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblGroup runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Kind") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Kind") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Description" HeaderText="Nama Equipment">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Status">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblStatus runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ErrorMessage" HeaderText="Keterangan">
										<HeaderStyle Width="35%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD colSpan="6"><asp:button id="btnSave" runat="server" Width="72px" Text="Simpan" Enabled="False"></asp:button></TD>
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
