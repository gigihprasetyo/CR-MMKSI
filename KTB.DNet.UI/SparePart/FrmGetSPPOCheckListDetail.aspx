<%@ Import Namespace="KTB.DNet.Domain"%>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmGetSPPOCheckListDetail.aspx.vb" Inherits="FrmGetSPPOCheckListDetail" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmGetSPPOCheckListDetail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="0"
				cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titleTableParts" height="20">
						PEMESANAN&nbsp;-&nbsp;Rincian Barang yang Tidak Terpenuhi</td>
				</tr>
				<tr>
					<td background="../images/bg_hor_parts.gif" height="1"><IMG height="1" src="../images/bg_hor_parts.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD width="24%" class="titleField"><asp:Label id="Label3" runat="server">Jenis Order</asp:Label></TD>
								<TD width="1%"><asp:Label id="Label1" runat="server">:</asp:Label></TD>
								<TD width="75%"><asp:label id="lblOrderType" runat="server">lblOrderType</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:Label id="Label4" runat="server">Nomor Pesanan / Tanggal</asp:Label></TD>
								<TD><asp:Label id="Label2" runat="server">:</asp:Label></TD>
								<TD nowrap><asp:label id="lblPONumber" runat="server">lblPONumber</asp:label>
									<asp:label id="Label6" runat="server">/</asp:label>
									<asp:label id="lblDate" runat="server">lblDate</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top"><asp:datagrid id="dtgCheckListDetail" runat="server" Width="100%" BorderColor="#CDCDCD" BorderStyle="None"
							BorderWidth="0px" BackColor="#cdcdcd" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
							AllowPaging="True" AllowCustomPaging="True" CellSpacing="1">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
							<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
							<Columns>
								<asp:BoundColumn HeaderText="No">
									<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Nomor Barang">
									<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblPartNo" runat="server" Text='<%# CType(Container.DataItem, SparePartPODetail).SparePartMaster.PartNumber %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nama Barang">
									<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblPartName" runat="server" Text='<%# CType(Container.DataItem, SparePartPODetail).SparePartMaster.PartName %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Status Barang">
									<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblPartStatus" runat="server" Text='<%# EnumStopMark.GetStringValue(CType(Container.DataItem, SparePartPODetail).StopMark) %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="#999999" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD vAlign="top"><INPUT type="button" value="Tutup" onclick="window.close()"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
