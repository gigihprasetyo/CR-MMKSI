<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpSalesDocumentList.aspx.vb" Inherits="PopUpSalesDocumentList" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpSalesDocumentList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" align="left" colSpan="3">SALES - Daftar Dokumen Sales History</td>
				</tr>
				<tr>
					<td align="center" background="../images/bg_hor_sales.gif" colSpan="3" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="WIDTH: 197px" colSpan="3" height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD style="WIDTH: 500%" align="left" colSpan="5">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dtgDocumentHistory" runat="server" Width="100%" AutoGenerateColumns="False"
								AllowCustomPaging="True" PageSize="2">
								<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="LastDownloadBy" HeaderText="Terakhir Didownload Oleh">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblLastDownloadBy" Runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DownloadBy") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="LastDownloadDate" HeaderText="Terakhir Didownload Pada">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblLastDownloadDate" Runat="server" text='<%# format(DataBinder.Eval(Container, "DataItem.DownloadDate"),"dd/MM/yyyy") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" BackColor="#CCCCCC" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<td align=center><INPUT onclick="window.close();" type="button" value="Tutup"></td>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
