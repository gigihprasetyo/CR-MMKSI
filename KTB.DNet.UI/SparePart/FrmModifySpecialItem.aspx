<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmModifySpecialItem.aspx.vb" Inherits="FrmModifySpecialItem" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmModifySpecialItem</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function disableThem()
		{
			document.getElementById("ddlExtMaterialGroup").disabled = true;
			document.getElementById("ddlPeriodMonth").disabled = true;
			document.getElementById("ddlSparePart").disabled = true;
			//document.getElementById("btnSearch").disabled = true;
		}
		
		function enableThem()
		{
			document.getElementById("ddlExtMaterialGroup").disabled = false;
			document.getElementById("ddlPeriodMonth").disabled = false;
			document.getElementById("ddlSparePart").disabled = false;
			//document.getElementById("btnSearch").disabled = false;
		}		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">SPECIAL ITEM - Pengelolaan Spesial Item</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 8px" width="24%">Periode</TD>
								<td style="HEIGHT: 8px" width="1%">:</td>
								<TD style="HEIGHT: 8px" width="75%"><asp:dropdownlist id="ddlPeriodMonth" runat="server" AutoPostBack="True"></asp:dropdownlist>&nbsp;/&nbsp;&nbsp;
									<asp:dropdownlist id="ddlPeriodYear" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField">Group</TD>
								<td>:</td>
								<TD><asp:dropdownlist id="ddlExtMaterialGroup" runat="server" AutoPostBack="True" Width="200px"></asp:dropdownlist>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
							</TR>
							<TR>
								<TD class="titleField">Nomor Barang</TD>
								<td>:</td>
								<TD noWrap><asp:dropdownlist id="ddlSparePart" runat="server" Width="200px"></asp:dropdownlist><asp:button id="btnCari" runat="server" width="60px" Text="Cari"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 360px;">
							<asp:datagrid id="dtgSpecialItem" runat="server" Width="100%" BackColor="#CDCDCD" BorderStyle="Solid"
								GridLines="Vertical" AllowCustomPaging="True" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
								PageSize="50" BorderColor="#CDCDCD" CellSpacing="0" BorderWidth="1px" CellPadding="3">
								<AlternatingItemStyle Font-Bold="True" BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<Columns>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<ItemTemplate>
											<asp:Label id="Label1" runat="server">Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="20px" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label2" runat="server">Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.PartNumber" HeaderText="Nomor Barang">
										<HeaderStyle ForeColor="White" Width="150px" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnkPartNo" runat="server" CommandName="lnkPartNo"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.PartName" HeaderText="Nama Barang">
										<HeaderStyle ForeColor="White" Width="250px" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label4" runat="server">Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.ModelCode" HeaderText="Model">
										<HeaderStyle ForeColor="White" Width="70px" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label5" runat="server">Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
				<TR>
				</TR>
			</TABLE>
			<table width="500px">
				<TR>
					<TD>
						<asp:button id="btnHapus" runat="server" Text="Hapus Semua"></asp:button></TD>
					<TD align="right">
						<asp:Label id="Label3" runat="server" BackColor="#FFFFC0" ForeColor="#FFFFC0">_____</asp:Label>&nbsp;
						<asp:Label id="lblNewPart" runat="server">Barang Baru</asp:Label>
						<asp:Label id="Label6" runat="server" BackColor="#C0FFC0" ForeColor="#C0FFC0">_____</asp:Label>
						<asp:Label id="lblNewPrice" runat="server">Harga Baru</asp:Label></TD>
				</TR>
			</table>
			</TR></TABLE>
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
