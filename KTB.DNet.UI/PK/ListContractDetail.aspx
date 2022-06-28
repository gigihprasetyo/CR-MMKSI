	<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ListContractDetail.aspx.vb" Inherits="ListContractDetail" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ListContractDetail</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		//function Back()
		//{
		//window.history.go(-1);
		//}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PESANAN KENDARAAN -&nbsp;O/C Detail</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<P>
							<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
								<TR>
									<TD width="24%" class="titleField"><asp:Label id="Label1" runat="server">Kode Dealer</asp:Label></TD>
									<TD width="1%"><asp:Label id="Label5" runat="server">:</asp:Label></TD>
									<TD width="25%"><asp:Label id="lblDealerCode" runat="server"></asp:Label></TD>
									<TD width="20%" class="titleField"><asp:Label id="Label9" runat="server">Kota Dealer</asp:Label></TD>
									<TD width="1%"><asp:Label id="Label13" runat="server">:</asp:Label></TD>
									<TD width="29%"><asp:Label id="lblCityDealerValue" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD class="titleField"><asp:Label id="Label2" runat="server">Nama Dealer</asp:Label></TD>
									<TD><asp:Label id="Label6" runat="server">:</asp:Label></TD>
									<TD><asp:Label id="lblDealerName" runat="server"></asp:Label></TD>
									<TD class="titleField"><asp:Label id="Label10" runat="server">Nomor O/C</asp:Label></TD>
									<TD><asp:Label id="Label14" runat="server">:</asp:Label></TD>
									<TD><asp:Label id="lblNomorKontrakValue" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD class="titleField"><asp:Label id="Label3" runat="server">Kondisi Pesanan</asp:Label></TD>
									<TD><asp:Label id="Label7" runat="server">:</asp:Label></TD>
									<TD><asp:Label id="lblJenisMoValue" runat="server"></asp:Label></TD>
									<TD class="titleField"><asp:Label id="Label11" runat="server">Kategori</asp:Label></TD>
									<TD><asp:Label id="Label15" runat="server">:</asp:Label></TD>
									<TD><asp:Label id="lblKategoriValue" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD class="titleField"><asp:Label id="Label4" runat="server">Periode O/C</asp:Label></TD>
									<TD><asp:Label id="Label8" runat="server">:</asp:Label></TD>
									<TD><asp:Label id="lblPeriodeKontrakValue" runat="server"></asp:Label></TD>
									<TD class="titleField"><asp:Label id="Label12" runat="server">Jenis Pesanan</asp:Label></TD>
									<TD><asp:Label id="Label16" runat="server">:</asp:Label></TD>
									<TD><asp:Label id="lblContractType" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:Label id="lblNamaPesananKhusus" runat="server">Nama Pesanan Khusus</asp:Label></TD>
									<TD>
										<asp:Label id="Label21" runat="server">:</asp:Label></TD>
									<TD>
										<asp:Label id="lblNamaPesananKhususValue" runat="server"></asp:Label></TD>
									<TD class="titleField">
										<asp:Label id="lblTotalHargatebus" runat="server">Total Harga Tebus</asp:Label></TD>
									<TD><asp:Label id="Label18" runat="server">:</asp:Label></TD>
									<TD>
										<asp:Label id="Label25" runat="server" Font-Bold="True">Rp</asp:Label>
										<asp:Label id="lblTotal" runat="server" Font-Bold="True"></asp:Label></TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:Label id="Label17" runat="server">Free PPh22 Indicator</asp:Label></TD>
									<TD>
										<asp:Label id="Label19" runat="server">:</asp:Label></TD>
									<TD>
										<asp:CheckBox id="chbxFreePPh" runat="server"></asp:CheckBox></TD>
									<TD class="titleField"><asp:Label id="Label22" runat="server">Nomor Applikasi</asp:Label></TD>
									<TD><asp:Label id="Label20" runat="server">:</asp:Label></TD>
									<TD><asp:Label id="lblNomorSPL" runat="server" Font-Bold="True"></asp:Label>
										<asp:ImageButton id="ibtnDownload" runat="server" ImageUrl="../images/download.gif" ToolTip="Download SPL"></asp:ImageButton></TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:Label id="lblPPhLastUpdateBy" runat="server">Perubahan Terakhir Oleh</asp:Label></TD>
									<TD>
										<asp:Label id="Label23" runat="server">:</asp:Label></TD>
									<TD>
										<asp:Label id="lblPPhLastUpdateByValue" runat="server"></asp:Label></TD>
									<TD></TD>
									<TD></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:Label id="lblPPhLastUpateTime" runat="server">Perubahan Terakhir Tanggal</asp:Label></TD>
									<TD>
										<asp:Label id="Label24" runat="server">:</asp:Label></TD>
									<TD>
										<asp:Label id="lblPPhLastUpdateTimeValue" runat="server"></asp:Label></TD>
									<TD></TD>
									<TD></TD>
									<TD></TD>
								</TR>
							</TABLE>
						</P>
					</TD>
				</TR>
				<TR>
					<TD valign="top"><div id="div1" style="OVERFLOW: auto; HEIGHT: 290px">
							<asp:datagrid id="dgContractDetail" runat="server" AutoGenerateColumns="False" ForeColor="Black"
								GridLines="None" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="#CDCDCD"
								Width="100%" CellSpacing="1" ShowFooter="True">
								<SelectedItemStyle ForeColor="GhostWhite" BackColor="DarkSlateBlue"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" BackColor="Tan"></HeaderStyle>
								<FooterStyle BackColor="#cdcdcd" Font-Bold=True></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<%# container.itemindex+1 %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Tipe/Warna">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblMaterialNumber" runat="server"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="TextBox1" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Model/Tipe/Warna">
										<HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblMaterialDescription" runat="server"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="TextBox2" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="TargetQty" HeaderText="O/C Unit" FooterStyle-HorizontalAlign="Right">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Harga Unit (Rp)" FooterStyle-HorizontalAlign="Right">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblAmount runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Amount") %>' Visible="False">
											</asp:Label>
											<asp:Label id="lblAmountString" runat="server"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Amount") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="PPh-22 Unit (Rp)" FooterStyle-HorizontalAlign="Right">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblPPh22 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PPh22") %>' Visible="False">
											</asp:Label>
											<asp:Label id="lblPPh22String" runat="server"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox4 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PPh22") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="PaleGoldenrod"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table3" style="WIDTH: 16px; HEIGHT: 29px" cellSpacing="1" cellPadding="1" width="16"
							border="0">
							<TR>
								<TD>
									<asp:Button ID="btnBack" Runat="server" Text="Kembali"></asp:Button></TD>
								<TD>
									<asp:Button ID="btnSave" Runat="server" Text="Simpan"></asp:Button></TD>
							</TR>
						</TABLE>
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
