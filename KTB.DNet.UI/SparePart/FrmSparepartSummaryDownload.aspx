<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSparepartSummaryDownload.aspx.vb" Inherits="FrmSparepartSummaryDownload" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSparepartSummaryDownload</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		 
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table>
				<tr>
					<td>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">PEMESANAN - Monitoring Sparepart</td>
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
				<tr>
					<td>
						<asp:datagrid id="dtgSPPO" runat="server" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True"
							CellSpacing="1" CellPadding="3" BorderColor="Gainsboro" BackColor="Gainsboro" BorderWidth="0px"
							Width="100%">
							<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
							<HeaderStyle Font-Bold="True" Height="20px" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="id" HeaderText="ID">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Kode Dealer">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblDealerCode" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Name Dealer">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblDealerName" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nomor Pesanan">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblPONumber" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tanggal Pesanan">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblTanggalPesanan" runat="server"></asp:Label>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Jenis Pesanan">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblJenisPesanan" runat="server" ></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Status">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblProcessCode" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Total Item">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblTotalQuantity runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemCount") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemCount") %>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Total Amount">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblTotalAmount runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemAmount", "{0:#,###.00}") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id=TextBox2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemAmount", "{0:#,###.00}") %>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
