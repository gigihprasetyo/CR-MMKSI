<%@ Page Language="vb" AutoEventWireup="false" Codebehind="OpenDailyPO.aspx.vb" Inherits="OpenDailyPO" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>OpenDailyPO</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%" border="0">
				<tr>
					<td colSpan="7">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">PO HARIAN - Pengajuan PO</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" colSpan="6" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td colSpan="6" height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD class="titleField" width="24%"><asp:label id="lblDealerCode" runat="server">Kode Dealer</asp:label></TD>
					<TD width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
					<TD width="25%"><asp:label id="lblDealerCodeValue" runat="server"></asp:label></TD>
					<TD class="titleField" width="20%"><asp:label id="lblCity" runat="server">Kota</asp:label></TD>
					<TD width="1%"><asp:label id="Label9" runat="server">:</asp:label></TD>
					<TD width="29%"><asp:label id="lblCityValue" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblNamaDealer" runat="server">Nama Dealer</asp:label></TD>
					<TD><asp:label id="Label3" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblNamaDealerValue" runat="server"></asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblJenisPesanan" runat="server">Jenis O/C</asp:label></TD>
					<TD><asp:label id="Label1" runat="server">:</asp:label></TD>
					<TD><asp:dropdownlist id="ddlJenisPesanan" runat="server" Width="140px"></asp:dropdownlist></TD>
					<TD class="titleField"><asp:label id="lblNomorKontrak" runat="server">Nomor O/C</asp:label></TD>
					<TD><asp:label id="Label11" runat="server">:</asp:label></TD>
					<TD><asp:textbox onkeypress="return alphaNumericPlusUniv(event)" id="txtNomorKOntrak" onblur="alphaNumericPlusBlur(txtNomorKOntrak)"
							runat="server" Width="140px" MaxLength="10"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 15px"><asp:label id="Label5" runat="server">Periode O/C</asp:label></TD>
					<TD style="HEIGHT: 15px"><asp:label id="Label6" runat="server">:</asp:label></TD>
					<TD style="HEIGHT: 15px"><asp:dropdownlist id="ddlPeriodeKontrak" runat="server" Width="140px"></asp:dropdownlist></TD>
					<TD class="titleField" style="HEIGHT: 15px"><asp:label id="lblNomerPk" runat="server">Nomor PK</asp:label></TD>
					<TD style="HEIGHT: 15px"><asp:label id="Label12" runat="server">:</asp:label></TD>
					<TD style="HEIGHT: 15px"><asp:textbox onkeypress="return alphaNumericPlusSpaceUniv(event)" id="txtDealerPKNumber" onblur="alphaNumericPlusSpaceBlur(txtDealerPKNumber)"
							runat="server" Width="140px" MaxLength="40"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 14px"><asp:label id="lblKondisiPesanan" runat="server">Kondisi Pesanan</asp:label></TD>
					<TD style="HEIGHT: 14px"><asp:label id="Label13" runat="server">:</asp:label></TD>
					<TD style="HEIGHT: 14px"><asp:dropdownlist id="ddlKondisiPesanan" runat="server" Width="140px"></asp:dropdownlist></TD>
					<TD class="titleField" style="HEIGHT: 14px"><asp:label id="lblKategori" runat="server">Kategori</asp:label></TD>
					<TD style="HEIGHT: 14px">:</TD>
					<TD style="HEIGHT: 14px"><asp:dropdownlist id="ddlKategori" runat="server" Width="140px"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label4" runat="server">Total Sisa Tebus (Rp.)</asp:label></TD>
					<TD>
						<asp:label id="Label20" runat="server">:</asp:label></TD>
					<TD><STRONG>
							<asp:label id="Label19" runat="server" Font-Bold="True">Rp</asp:label></STRONG>
						<asp:label id="lblTotalSisaTebus" runat="server" Font-Bold="True"></asp:label></TD>
					<TD class="titleField">
						<asp:label id="Label17" runat="server"> Total Quantity</asp:label></TD>
					<TD>:</TD>
					<TD>
						<asp:label id="lblQuantity" runat="server" Font-Bold="True"></asp:label></TD>
				</TR>
				<tr>
					<td><STRONG>Kondisi Bebas PPh</STRONG></td>
					<td>:</td>
					<td>
						<asp:dropdownlist style="Z-INDEX: 0" id="ddlFreePPh" runat="server" Width="140px"></asp:dropdownlist></td>
					<td></td>
					<td></td>
					<td><asp:button id="btnCari" runat="server" Width="60px" Text="Cari"></asp:button></td>
				</tr>
				<TR>
					<TD colSpan="6">
						<div id="div1" style="HEIGHT: 280px; OVERFLOW: auto"><asp:datagrid id="dtgContract" runat="server" Width="100%" CellSpacing="1" OnItemCommand="dtgContract_ItemCommand"
								OnItemDataBound="dtgContract_itemdataBound" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD"
								CellPadding="3" AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#CCCCFF" BackColor="#003399"></HeaderStyle>
								<FooterStyle ForeColor="#003399" BackColor="#99CCCC"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" SortExpression="id" HeaderText="id">
										<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" SortExpression="status" HeaderText="Status">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ContractNumber" SortExpression="ContractNumber" HeaderText="Nomor O/C">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RefContractNumber" SortExpression="RefContractNumber" HeaderText="O/C Reference">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="DealerPKNumber" HeaderText="Nomor PK">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="Category.ID" HeaderText="Kategori">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="ContractType" HeaderText="Jenis O/C">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="ProductionYear" HeaderText="Tahun Perakitan / Impor">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="ProjectName" HeaderText="Nama Pesanan Khusus">
										<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Jumlah Sisa Tebus (Rp.)">
										<HeaderStyle ForeColor="White" Width="12%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat Detil O/C"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnCreatePO" runat="server" CommandName="Create">
												<img src="../images/edit.gif" border="0" alt="Buat PO"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD colSpan="7"><asp:label id="lblPerhatian" runat="server" Font-Bold="True" Visible="False">Perhatian :</asp:label><asp:label id="lblDokumen" runat="server" Visible="False">Dokumen ini merupakan bagian yang tidak terpisahkan dari Perjanjian Jual Beli No.</asp:label><asp:label id="lblspaNumber" runat="server" Visible="False"></asp:label>&nbsp;
						<asp:label id="lblTanggal" runat="server" Visible="False">Tanggal</asp:label>&nbsp;
						<asp:label id="lblspaDate" runat="server" Visible="False"></asp:label></TD>
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
