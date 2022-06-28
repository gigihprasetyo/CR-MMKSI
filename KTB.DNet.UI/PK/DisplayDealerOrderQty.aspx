<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DisplayDealerOrderQty.aspx.vb" Inherits="DisplayDealerOrderQty" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DisplayDealerOrderQty</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/jquery-1.10.2.js"></script>
        <script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">

        <script type="text/javascript">

            function CheckGrid() {
                var counter = 0;
                var btnSimpan = $('#btnSimpan');
                $("#<%=dtgProduction.ClientID%> input[id*='ChkExport']:checkbox").each(function (index) {
                    if ($(this).is(':checked'))
                        counter++;
                });
                if (counter > 0) {
                    btnSimpan.show()
                } else {
                    btnSimpan.hide()
                    alert("Tidak ada Tipe Kendaraan yang dipilih")
                    return false
                }
            };

        </script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" colSpan="3">PESANAN KENDARAAN - Proses Alokasi</td>
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
							<TBODY>
								<TR>
									<TD class="titleField" width="120px"><asp:label id="lblKategori" runat="server">Kategori</asp:label></TD>
									<TD>:</TD>
									<TD><asp:dropdownlist id="ddlKategori" runat="server" AutoPostBack="True" Width="104px"></asp:dropdownlist>
										<asp:dropdownlist style="Z-INDEX: 0" id="ddlSubCategory" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
									<TD class="titleField"><asp:label id="lblJenisPesanan" runat="server">Jenis Pesanan</asp:label></TD>
									<TD>:</TD>
									<TD><asp:dropdownlist id="ddlJenisPesanan" runat="server" AutoPostBack="True" Width="104px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="titleField" style="HEIGHT: 20px"><asp:label id="lblTipe" runat="server">Tipe</asp:label></TD>
									<TD style="HEIGHT: 20px">:</TD>
									<TD style="HEIGHT: 20px"><asp:dropdownlist id="ddlTipe" runat="server" AutoPostBack="True" Width="104px"></asp:dropdownlist></TD>
									<TD class="titleField" style="HEIGHT: 20px"><asp:label id="lblPeriode" runat="server">Periode Alokasi</asp:label></TD>
									<TD style="HEIGHT: 20px">:</TD>
									<TD style="HEIGHT: 20px"><asp:dropdownlist id="ddlPeriode" runat="server" Width="104px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="titleField"><asp:label id="lblTipeWarna" runat="server">Tipe/Warna</asp:label></TD>
									<TD>:</TD>
									<TD><asp:dropdownlist id="ddlTipeWarna" runat="server" Width="105px"></asp:dropdownlist></TD>
                                    <td colspan="3"></td>
								<tr>
									<TD class="titleField"><asp:label id="lblTahunPerakitan" runat="server" Width="152px">Tahun Perakitan</asp:label></TD>
									<TD>:</TD>
									<TD><asp:dropdownlist id="ddlTahunPerakitan" runat="server" Width="104px"></asp:dropdownlist></TD>
									<td></td>
									<td></td>
									<td>&nbsp;
                                        </td>
                                <TR>
                                    <td></td>
									<td></td>
									<td><asp:button id="btnCari" runat="server" Width="56px" Text="Cari"></asp:button>&nbsp;
                                        <asp:Button ID="btnHitungAlokasi" runat="server" Text="Alokasi Otomasi" Enabled="True" Visible="False" OnClientClick="CheckGrid()"></asp:Button>&nbsp;
				                        <asp:button id="btnUpload" runat="server" Text="Upload"></asp:button>
									</td>
									<td><asp:Button ID="btnSimpan" runat="server" Text="Simpan" Enabled="True" Visible="False"></asp:Button></td>
									<td></td>
				                    <TD style="padding-top:5px;padding-right:0px">
                                        &nbsp;
                                        </TD>
			                    </TR>
					</TD>
				</TR>
			</TABLE>
			</TD></TR>
			<TR>
				<TD vAlign="top" colSpan="3">
					<div id="div1" style="HEIGHT: 340px; OVERFLOW: auto">
                        <asp:datagrid id="dtgProduction" runat="server" Width="990px" ShowFooter="True" OnEditCommand="dtgProduction_Edit"
							BackColor="Gainsboro" OnItemDataBound="dtgProduction_ItemDataBound" AutoGenerateColumns="False" CellSpacing="1" CellPadding="3" BorderWidth="0px">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="#F5F1EE"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
							<Columns>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderTemplate>
                                        <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('ChkExport', document.all.chkAllItems.checked)"
                                            type="checkbox">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkExport" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
									<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:TemplateColumn>
								<asp:BoundColumn HeaderText="Model / Tipe / Warna">
									<HeaderStyle Width="17%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Tipe / Warna">
									<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Tahun Perakitan">
									<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Total Pesanan (unit)">
									<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Total Alokasi (unit)">
									<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Pesanan Belum Alokasi (unit)">
									<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>

                                <asp:BoundColumn HeaderText="Sisa Stok Setelah Alokasi (unit)">
									<HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>

								<asp:BoundColumn HeaderText="Sisa Stok (unit)">
									<HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>

                                <asp:TemplateColumn HeaderText="Alokasi Otomasi (unit)">
									<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
								</asp:TemplateColumn>
                                
								<asp:TemplateColumn>
									<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnEdit" runat="server" Text="" CommandName="Edit" CausesValidation="false"></asp:LinkButton>
									</ItemTemplate>
									<EditItemTemplate>
										&nbsp;
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn>
									<HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
								</asp:BoundColumn>
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
            <TR>
				<TD colSpan="3" style="padding-top:5px">
                    <%--<asp:Button ID="btnSimpan" runat="server" Text="Simpan" Enabled="True" Visible="False"></asp:Button>
                    <asp:Button ID="btnHitungAlokasi" runat="server" Text="Alokasi Otomasi" Enabled="True" Visible="False" OnClientClick="CheckGrid()"></asp:Button>--%>
				</TD>
			</TR>
			</TBODY></TABLE></form>
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
