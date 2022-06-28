<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DetailRemainPKProcess.aspx.vb" Inherits="DetailRemainPKProcess" smartNavigation="True"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DetailRemainPKProcess</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		//function Back()
		//{
		//if(navigator.appName == "Microsoft Internet Explorer")
		//{
		//window.history.go(-1);
		//}
		//else
		//{
		//var hidden = document.getElementById("Hidden1")
		//var i = hidden.value * -1
		//window.history.go(i);
		//}
		//}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PESANAN KENDARAAN - Proses Sisa Alokasi</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="0" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%"><asp:label id="lblNomorPK" runat="server">Nomor PK</asp:label></TD>
								<TD width="1%"><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD width="25%"><asp:label id="lblNomorPKValue" runat="server"></asp:label></TD>
								<TD width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblKodeDealer" runat="server">Kode Dealer</asp:label></TD>
								<TD><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblKodeDealerValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblKategori" runat="server">Kategori</asp:label></TD>
								<TD><asp:label id="Label8" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblKategoriValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblNamaDealer" runat="server">Nama Dealer</asp:label></TD>
								<TD><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblNamaDealerValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblJenisPesanan" runat="server">Jenis Pesanan</asp:label></TD>
								<TD><asp:label id="Label9" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblJenisPesananValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblKota" runat="server">Kota</asp:label></TD>
								<TD><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblKotaValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblTahunPerakitanAtauImport" runat="server">Tahun Perakitan / Import</asp:label></TD>
								<TD><asp:label id="Label10" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblTahunPerakitanValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblTanggalPesanan" runat="server">Tanggal Pesanan</asp:label></TD>
								<TD><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblTanggalPesananValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblRencanaPenebusan" runat="server">Rencana Penebusan</asp:label></TD>
								<TD><asp:label id="Label11" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblRencanaPenebusanValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblStatus" runat="server">Status</asp:label></TD>
								<TD><asp:label id="Label7" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblStatusValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblNomorPesanan" runat="server">Nomor Pesanan</asp:label></TD>
								<TD><asp:label id="Label12" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblNomorPesananValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblTotalUnit" runat="server" Width="112px">Total Unit</asp:label></TD>
								<TD><asp:label id="Label16" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblTotalUnitValue" runat="server"></asp:label></TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dtgPesananKendaraan" runat="server" Width="100%" CellSpacing="1" CellPadding="3"
								BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False" OnItemDataBound="dtgPesananKendaraan_ItemDataBound">
								<SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#CCCCFF" BackColor="#003399"></HeaderStyle>
								<FooterStyle ForeColor="#003399" BackColor="#99CCCC"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" NAME = "lblNo" text = '<%# container.itemindex+1 %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn ReadOnly="True" HeaderText="Model / Tipe / Warna">
										<HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="VehicleTypeCode" HeaderText="KodeTipe">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="VehicleColorCode" HeaderText="Kode Warna">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="TargetQty" HeaderText="Pesanan (Unit)">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ResponseQty" HeaderText="Alokasi (Unit)">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Sisa Alokasi (Unit)">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn>
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:TextBox id="TextBox1" runat="server" Width="30px" onkeypress="return numericOnlyUniv(event)">0</asp:TextBox>
											<asp:TextBox id="TextBox2" runat="server" Width="30px" onkeypress="return numericOnlyUniv(event)">0</asp:TextBox>
											<asp:TextBox id="TextBox3" runat="server" Width="30px" onkeypress="return numericOnlyUniv(event)">0</asp:TextBox>
											<asp:TextBox id="TextBox4" runat="server" Width="30px" onkeypress="return numericOnlyUniv(event)">0</asp:TextBox>
											<asp:TextBox id="TextBox5" runat="server" Width="30px" onkeypress="return numericOnlyUniv(event)">0</asp:TextBox>
											<asp:TextBox id="TextBox6" runat="server" Width="30px" onkeypress="return numericOnlyUniv(event)">0</asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
						<asp:label id="lblError" runat="server" Width="624px" Height="7px" EnableViewState="False"
							ForeColor="Red"></asp:label></TD>
				</TR>
				<TR>
				</TR>
			</TABLE>
			<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr vAlign="top">
					<td width="20%"><asp:label id="Label5" runat="server" Visible="False">PK Dilanjutkan Dengan Nomor :</asp:label></td>
					<td><asp:listbox id="lboxPKNumber" runat="server" Visible="False"></asp:listbox></td>
				</tr>
			</TABLE>
			</TR>
			<TR>
				<TD style="HEIGHT: 15px">
					<asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button>
					<asp:Button ID="btnBack" Runat="server" Text="Kembali"></asp:Button>
				</TD>
			<TR>
				<TD></TD>
			</TR>
			</TABLE></form>
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
