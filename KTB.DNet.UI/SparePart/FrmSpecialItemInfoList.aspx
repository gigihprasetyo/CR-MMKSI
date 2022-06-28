<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSpecialItemInfoList.aspx.vb" Inherits="FrmSpecialItemInfoList" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSpecialItemInfoList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" style="HEIGHT: 8px">SPECIAL ITEM - Daftar Spesial Item</td>
							</tr>
							<tr>
								<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td style="HEIGHT: 8px" height="8"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Periode</TD>
								<td width="1%">:</td>
								<TD width="75%"><asp:dropdownlist id="ddlPeriodMonth" runat="server" AutoPostBack="True"></asp:dropdownlist>&nbsp;/&nbsp;
									<asp:dropdownlist id="ddlPeriodYear" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField">Group</TD>
								<td>:</td>
								<TD><asp:dropdownlist id="ddlExtMaterialGroup" runat="server" AutoPostBack="True" Width="222px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField">Nomor Barang</TD>
								<td>:</td>
								<TD><asp:dropdownlist id="ddlSparePart" runat="server" Width="222px" Visible="False"></asp:dropdownlist>
									<asp:TextBox id="txtSparePart" runat="server"></asp:TextBox>
									<asp:button id="btnSearch" runat="server" Width="64px" Text="Cari"></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField">Referensi</TD>
								<TD>:</TD>
								<TD><asp:listbox id="lbNoReff" runat="server" Width="144px" Height="48px"></asp:listbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Catatan</TD>
								<TD>:</TD>
								<TD><asp:label id="lblCatatan" runat="server" Width="100%"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 260px"><asp:datagrid id="dtgSpecialItem" runat="server" Width="490px" GridLines="Vertical" PageSize="50"
								AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" CellPadding="3" BorderWidth="1px" CellSpacing="0" BackColor="Gainsboro" BorderColor="Gainsboro"
								AutoGenerateColumns="False">
								<AlternatingItemStyle BackColor="#F1F6FB" Font-Bold="True"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Height="20px" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label1" runat="server">Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle CssClass="titleTableParts" Width="20px"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label2" runat="server">Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.PartNumber" HeaderText="Nomor Barang">
										<HeaderStyle CssClass="titleTableParts" Width="150px"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label3" runat="server">Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.PartName" HeaderText="Nama Barang">
										<HeaderStyle CssClass="titleTableParts" Width="250px"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label4" runat="server">Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.ModelCode" HeaderText="Model">
										<HeaderStyle CssClass="titleTableParts" Width="70px"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label5" runat="server">Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
			</TABLE>
			<table width="100%">
				<TR>
					<TD><asp:button id="btnDownload" runat="server" Text="Download"></asp:button></TD>
					<TD align="right"><asp:label id="Label8" runat="server" BackColor="#FFFFC0" ForeColor="#FFFFC0">_____</asp:label>&nbsp;
						<asp:label id="Label9" runat="server">Barang Baru</asp:label><asp:label id="Label10" runat="server" BackColor="#C0FFC0" ForeColor="#C0FFC0">_____</asp:label><asp:label id="Label11" runat="server">Harga Baru</asp:label></TD>
				</TR>
			</table>
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
