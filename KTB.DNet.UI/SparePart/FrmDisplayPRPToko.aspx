<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDisplayPRPToko.aspx.vb" Inherits="KTB.DNet.UI.SparePart.FrmDisplayPRPToko" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDisplayPRPToko</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 8px">PARTSHOP REWARD 
						PROGRAM&nbsp;-&nbsp;Daftar PRP PerToko</td>
				</tr>
				<tr>
					<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 8px" height="8"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 183px; HEIGHT: 18px">Kode Dealer</TD>
								<TD style="WIDTH: 17px; HEIGHT: 18px">:</TD>
								<TD style="HEIGHT: 18px"><asp:label id="lblKodeDealerValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 183px">Nama Dealer</TD>
								<TD style="WIDTH: 17px">:</TD>
								<TD><asp:label id="lblNamaDealerValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="3"><asp:button id="btnBack" runat="server" Width="72px" Text="Kembali"></asp:button><asp:button id="btnDownload" text="Download" runat="server"></asp:button></TD>
							</TR>
						</TABLE>
						<table id="Table3" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD vAlign="top" colSpan="3">
									<DIV id="div1" style="OVERFLOW: scroll; WIDTH: 100%; POSITION: absolute; HEIGHT: 480px"><asp:datagrid id="dtgExcel" runat="server" Width="1490px" AllowSorting="True" CellPadding="3"
											BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowPaging="True" PageSize="50" ShowFooter="True">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<FooterStyle Font-Bold="True" HorizontalAlign="Right" CssClass="titleTableParts"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="40px" CssClass="titleTableParts"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="PSCode" SortExpression="PSCode" HeaderText="Kode PS">
													<HeaderStyle Width="75px" CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="PartShopName" SortExpression="PartShopName" HeaderText="Nama PS">
													<HeaderStyle Width="200px" CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Kota" SortExpression="Kota" HeaderText="Kota">
													<HeaderStyle Width="200px" CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Jan" SortExpression="Jan" HeaderText="Januari" DataFormatString="{0:#,###}">
													<HeaderStyle Width="75px" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Width="75px"></ItemStyle>
													<FooterStyle HorizontalAlign="Right" ForeColor="#FFFFFF"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Feb" SortExpression="Feb" HeaderText="Februari" DataFormatString="{0:#,###}">
													<HeaderStyle Width="75px" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Width="75px"></ItemStyle>
													<FooterStyle HorizontalAlign="Right" ForeColor="#FFFFFF"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Mar" SortExpression="Mar" HeaderText="Maret" DataFormatString="{0:#,###}">
													<HeaderStyle Width="75px" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Width="75px"></ItemStyle>
													<FooterStyle HorizontalAlign="Right" ForeColor="#FFFFFF"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Apr" SortExpression="Apr" HeaderText="April" DataFormatString="{0:#,###}">
													<HeaderStyle Width="75px" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Width="75px"></ItemStyle>
													<FooterStyle HorizontalAlign="Right" ForeColor="#FFFFFF"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="May" SortExpression="May" HeaderText="Mei" DataFormatString="{0:#,###}">
													<HeaderStyle Width="75px" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Width="75px"></ItemStyle>
													<FooterStyle HorizontalAlign="Right" ForeColor="#FFFFFF"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Jun" SortExpression="Jun" HeaderText="Juni" DataFormatString="{0:#,###}">
													<HeaderStyle Width="75px" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Width="75px"></ItemStyle>
													<FooterStyle HorizontalAlign="Right" ForeColor="#FFFFFF"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Jul" SortExpression="Jul" HeaderText="Juli" DataFormatString="{0:#,###}">
													<HeaderStyle Width="75px" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Width="75px"></ItemStyle>
													<FooterStyle HorizontalAlign="Right" ForeColor="#FFFFFF"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Aug" SortExpression="Aug" HeaderText="Agustus" DataFormatString="{0:#,###}">
													<HeaderStyle Width="75px" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Width="75px"></ItemStyle>
													<FooterStyle HorizontalAlign="Right" ForeColor="#FFFFFF"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Sep" SortExpression="Sep" HeaderText="September" DataFormatString="{0:#,###}">
													<HeaderStyle Width="75px" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Width="75px"></ItemStyle>
													<FooterStyle HorizontalAlign="Right" ForeColor="#FFFFFF"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Oct" SortExpression="Oct" HeaderText="Oktober" DataFormatString="{0:#,###}">
													<HeaderStyle Width="75px" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Width="75px"></ItemStyle>
													<FooterStyle HorizontalAlign="Right" ForeColor="#FFFFFF"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Nov" SortExpression="Nov" HeaderText="November" DataFormatString="{0:#,###}">
													<HeaderStyle Width="75px" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Width="75px"></ItemStyle>
													<FooterStyle HorizontalAlign="Right" ForeColor="#FFFFFF"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Dec" SortExpression="Dec" HeaderText="Desember" DataFormatString="{0:#,###}">
													<HeaderStyle Width="75px" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Width="75px"></ItemStyle>
													<FooterStyle HorizontalAlign="Right" ForeColor="#FFFFFF"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Total" SortExpression="Total" HeaderText="Total" DataFormatString="{0:#,###}">
													<HeaderStyle Width="75px" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Width="75px"></ItemStyle>
													<FooterStyle HorizontalAlign="Right" ForeColor="#FFFFFF"></FooterStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle PageButtonCount="20" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></DIV>
								</TD>
							</TR>
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
