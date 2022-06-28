<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmModifySpecialItemSubmit.aspx.vb" Inherits="FrmModifySpecialItemSubmit" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmModifySpecialItemSubmit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">SPECIAL ITEM - Modifikasi Special Item</td>
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
								<TD class="titleField" width="24%">Periode</TD>
								<td width="1%">:</td>
								<TD width="75%"><asp:textbox id="txtBulan" runat="server" ReadOnly="True"></asp:textbox>&nbsp;/ 
									&nbsp;
									<asp:textbox id="txtTahun" runat="server" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Group</TD>
								<td>:</td>
								<TD><asp:textbox id="txtGroup" runat="server" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Nomor Barang</TD>
								<td>:</td>
								<TD><asp:textbox id="txtPartNumber" runat="server" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Barang</TD>
								<td>:</td>
								<TD><asp:textbox id="txtPartName" runat="server" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Model</TD>
								<td>:</td>
								<TD><asp:textbox id="txtModel" runat="server" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Referensi</TD>
								<td>:</td>
								<TD><asp:textbox id="txtReference" runat="server" ReadOnly="True" MaxLength="50" Width="224px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Remark</TD>
								<td>:</td>
								<TD><asp:textbox id="txtRemark" runat="server" MaxLength="40" Width="224px"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><asp:datagrid id="dtgSpecialItem" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#CDCDCD"
							CellSpacing="0" BorderWidth="1px" CellPadding="3" BackColor="#CDCDCD" GridLines="Vertical" BorderStyle="Solid">
							<AlternatingItemStyle BackColor="#F1F6FB" Font-Bold="True"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<Columns>
								<asp:TemplateColumn Visible="False" HeaderText="ID">
									<ItemTemplate>
										<asp:Label id="Label1" runat="server">Label</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="20px" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label2" runat="server">Label</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nomor Barang">
									<HeaderStyle Width="150px" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server">Label</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nama Barang">
									<HeaderStyle Width="250px" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label4" runat="server">Label</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Model">
									<HeaderStyle Width="70px" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label5" runat="server">Label</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD><asp:button id="btnDelete" runat="server" Text="Hapus"></asp:button><asp:button id="btnSubmit" runat="server" Text="Simpan"></asp:button><asp:button id="btnKembali" runat="server" Text="Kembali"></asp:button></TD>
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
