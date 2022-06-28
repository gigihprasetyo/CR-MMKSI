<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmDisplayPKReleaseDetail.aspx.vb" Inherits="frmDisplayPKReleaseDetail" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EntryAllocationQty</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" colSpan="3">PESANAN KENDARAAN - Detil Status Rilis</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" colSpan="3" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td colSpan="3" height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD style="HEIGHT: 59px" colSpan="3">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%"><asp:label id="lblKategori" runat="server">Kategori</asp:label></TD>
								<td width="1%">:</td>
								<TD style="WIDTH: 156px" width="156"><asp:label id="lblKategoriValue" runat="server"></asp:label></TD>
								<TD class="titleField" style="WIDTH: 172px" width="172"><asp:label id="lblPeriodeAlokasi" runat="server">Periode Alokasi</asp:label></TD>
								<td style="WIDTH: 1px" width="1">:</td>
								<TD width="29%"><asp:label id="lblPeriodeAlokasiValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblTipe" runat="server">Tipe</asp:label></TD>
								<td width="1%">:</td>
								<TD style="WIDTH: 156px"><asp:label id="lblTipeValue" runat="server"></asp:label></TD>
								<TD class="titleField" style="WIDTH: 172px"><asp:label id="lblTahunPerakitan" runat="server">Tahun Perakitan</asp:label></TD>
								<td style="WIDTH: 1px" width="1">:</td>
								<TD><asp:label id="lblTahunPerakitanValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblMaterialNumber" runat="server">Tipe / Warna</asp:label></TD>
								<td width="1%">:</td>
								<TD style="WIDTH: 156px"><asp:label id="lblMaterialNumberValue" runat="server">Label</asp:label></TD>
								<TD class="titleField" style="WIDTH: 172px"><asp:label id="lblTotalProduksi" runat="server">Sisa Stok</asp:label></TD>
								<td style="WIDTH: 1px" width="1">:</td>
								<TD><asp:label id="lblTotalProduksiValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblModelTipeWarna" runat="server">Model / Tipe / Warna</asp:label></TD>
								<td width="1%">:</td>
								<TD style="WIDTH: 156px"><asp:label id="lblMaterialDescriptionValue" runat="server"></asp:label></TD>
								<TD class="titleField" style="WIDTH: 172px"></TD>
								<td style="WIDTH: 1px" width="1"></td>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 335px"><asp:datagrid id="dtgEntryAllocation" runat="server" AllowSorting="True" OnItemDataBound="dtgEntryAllocation_ItemDataBound"
								GridLines="None" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" CellSpacing="1" BorderStyle="Ridge" BorderColor="#CDCDCD" AutoGenerateColumns="False"
								Width="100%" OnItemCommand="dtgEntryAllocation_ItemCommand">
								<SelectedItemStyle ForeColor="White" BackColor="#9471DE"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#E7E7FF" VerticalAlign="Middle"
									BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="Black" BackColor="#C6C3C6"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" ReadOnly="True" HeaderText="ID">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PKHeader.PKNumber" HeaderText="No Reg PK">
										<HeaderStyle HorizontalAlign="Center" ForeColor="White" Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" ForeColor="Black"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnNoRegPK" runat="server" CommandName="View" text='<%# DataBinder.Eval(Container.DataItem, "PKHeader.PKNumber" )  %>'>
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn SortExpression="PKHeader.Dealer.DealerCode" HeaderText="Dealer">
										<HeaderStyle HorizontalAlign="Center" ForeColor="White" Width="25%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" ForeColor="Black"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="PKHeader.Dealer.DealerName" HeaderText="Nama Dealer">
										<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle ForeColor="Black"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="PKHeader.Dealer.DealerName" HeaderText="Nama Pesanan Khusus">
										<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="TargetQty" HeaderText="Pesanan (Unit)">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Alokasi (Unit)">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblAlokasiQty" Runat="server" NAME="lblAlokasiQty" text= '<%# DataBinder.Eval(Container.DataItem, "ResponseQty" )  %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" HeaderText="PKType"></asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="#C6C3C6"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 30px" colSpan="3"></TD>
				</TR>
				<TR>
					<TD colSpan="3"><asp:button id="btnKembali" runat="server" CausesValidation="False" Text="Kembali"></asp:button></TD>
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
