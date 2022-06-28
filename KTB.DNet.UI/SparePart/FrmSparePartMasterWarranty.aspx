<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmSparePartMasterWarranty.aspx.vb" Inherits=".FrmSparePartMasterWarranty" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSparePartMasterWarranty</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">MASTER BARANG&nbsp;- Barang Pengganti</TD>
				</TR>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD><STRONG><FONT size="2">Nomor Barang Baru</FONT> &nbsp;</STRONG>
						<asp:Label id="lblSparePartAlt" runat="server" Font-Bold="True" Font-Size="Larger"></asp:Label></TD>
				</TR>
				<TR>
					<TD><div id="div1" style="OVERFLOW: auto; HEIGHT: 400px">
						<asp:DataGrid id="dtgSparePartAlt" runat="server" AutoGenerateColumns="False" AllowSorting="True"
							AllowCustomPaging="True" AllowPaging="True" Width="100%" BorderColor="#CDCDCD" CellSpacing="1"
							BorderWidth="0px" CellPadding="3" BackColor="#CDCDCD">
							<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
									<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="PartNumber" SortExpression="PartNumber" HeaderText="Nomor Barang">
									<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTableService"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PartName" SortExpression="PartName" HeaderText="Nama Barang">
									<HeaderStyle ForeColor="White" Width="25%" CssClass="titleTableService"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ModelCode" SortExpression="ModelCode" HeaderText="Model">
									<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableService"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="Stock" HeaderText="Stock" Visible="False">
									<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="RetalPrice" SortExpression="RetalPrice" HeaderText="Harga Eceran (Rp)"
									DataFormatString="{0:#,##0}">
									<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></div></TD>
				</TR>
				<TR>
					<TD align=center><INPUT id="btnClose" type="button" value="Tutup" onclick="window.close()"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
