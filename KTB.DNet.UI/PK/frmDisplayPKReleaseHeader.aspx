<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmDisplayPKReleaseHeader.aspx.vb" Inherits="frmDisplayPKReleaseHeader" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DisplayPKReleaseHeader</title>
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
					<td class="titlePage" colSpan="3">PESANAN KENDARAAN - Daftar Status Rilis</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" colSpan="3" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td colSpan="3" height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD colSpan="3">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%"><asp:label id="lblJenisPesanan" runat="server">Jenis Pesanan</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:dropdownlist id="ddlJenisPesanan" runat="server" Width="104px"></asp:dropdownlist></TD>
								<TD class="titleField" width="20%"><asp:label id="lblKategori" runat="server">Kategori</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="29%"><asp:dropdownlist id="ddlKategori" runat="server" Width="104px" AutoPostBack="True"></asp:dropdownlist>
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlSubCategory" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px"><asp:label id="lblPeriode" runat="server">Periode Alokasi</asp:label></TD>
								<TD style="HEIGHT: 20px">:</TD>
								<TD style="HEIGHT: 20px">
									<asp:DropDownList id="ddlPeriode" runat="server" Width="104px"></asp:DropDownList></TD>
								<TD class="titleField" style="HEIGHT: 20px"><asp:label id="lblTipe" runat="server">Tipe</asp:label></TD>
								<TD style="HEIGHT: 20px">:</TD>
								<TD style="HEIGHT: 20px"><asp:dropdownlist id="ddlTipe" runat="server" Width="104px" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD>
									<asp:button id="btnCari" runat="server" Width="56px" Text="Cari"></asp:button></TD>
								<TD class="titleField"><asp:label id="lblTipeWarna" runat="server">Tipe/Warna</asp:label></TD>
								<TD>:</TD>
								<TD><asp:dropdownlist id="ddlTipeWarna" runat="server" Width="105px"></asp:dropdownlist>&nbsp;&nbsp;</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD valign="top" colSpan="3"><div id="div1" style="HEIGHT: 400px; OVERFLOW: auto"><asp:datagrid id="dtgProduction" runat="server" Width="100%" BorderWidth="0px" CellPadding="3"
								CellSpacing="1" AutoGenerateColumns="False" OnItemDataBound="dtgProduction_ItemDataBound" BackColor="Gainsboro" OnEditCommand="dtgProduction_Edit" ShowFooter="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F5F1EE"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn HeaderText="Model / Tipe / Warna">
										<HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Tipe / Warna">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Tahun Perakitan / Impor">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Total Pesanan (unit)">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Total Rilis (unit)">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Sisa Stok (unit)">
										<HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" runat="server" Text="" CommandName="Edit" CausesValidation="false"></asp:LinkButton>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:LinkButton id="LinkButton3" runat="server" Text="" CommandName="Update"></asp:LinkButton>&nbsp;
											<asp:LinkButton id="LinkButton2" runat="server" Text="" CommandName="Cancel" CausesValidation="false"></asp:LinkButton>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" HeaderText="Total Produksi">
										<HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD colSpan="3"></TD>
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
