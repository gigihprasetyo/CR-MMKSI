<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListPartIncidentalDetail.aspx.vb" Inherits="FrmListPartIncidentalDetail" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmListPartIncidentalDetail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
        function Back()
		{
		var hidden = document.getElementById("HiddenField")
		var i = hidden.value * -1
		window.history.go(i);
		}
					
		//function DummyFunction()
		//{
			
		//}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">PERMINTAAN KHUSUS - Rincian Permintaan Khusus</TD>
				</TR>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%"><asp:label id="lblNoPermintaan" runat="server">Nomor Permintaan</asp:label></TD>
								<TD width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD width="25%"><asp:label id="lblNomorPermintaanValue" runat="server"></asp:label></TD>
								<TD width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblKodeDealer" runat="server">Kode Dealer</asp:label></TD>
								<TD><asp:label id="Label8" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblKodeDealerValue" runat="server"></asp:label></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblTanggalinput" runat="server">Tanggal Input</asp:label></TD>
								<TD><asp:label id="Label9" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblTanggalValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblNoSurat" runat="server">No Surat</asp:label></TD>
								<TD><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblNoSuratValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblnoPolisi" runat="server">Nomor Polisi</asp:label></TD>
								<TD><asp:label id="Label10" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblNomorPolisiValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="Label2" runat="server">W/O </asp:label></TD>
								<TD><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblWOvalue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">
									<asp:label id="lblNoRangka" runat="server" Width="112px">No Rangka</asp:label></TD>
								<TD>
									<asp:label id="Label13" runat="server">:</asp:label></TD>
								<TD>
									<asp:label id="lblNoRangkaValue" runat="server"></asp:label></TD>
								<TD>
									<asp:label id="lblPIC" runat="server" Font-Bold="True">PIC</asp:label></TD>
								<TD>
									<asp:label id="lblId" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblPICValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">
									<asp:label id="lblTipe" runat="server" Width="112px">Tipe</asp:label></TD>
								<TD>
									<asp:label id="Label12" runat="server">:</asp:label></TD>
								<TD>
									<asp:label id="lblTipeValue" runat="server"></asp:label></TD>
								<TD>
									<asp:label id="lblTelp" runat="server" Font-Bold="True">Telp</asp:label></TD>
								<TD>
									<asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD>
									<asp:label id="lblTelpValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">
									<P>
										<asp:label id="lblTahunProduksi" runat="server" Width="112px">Tahun Produksi</asp:label></P>
								</TD>
								<TD>
									<asp:label id="Label7" runat="server">:</asp:label></TD>
								<TD>
									<asp:label id="lblTahunProduksiValue" runat="server"></asp:label></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblStatus" runat="server" Width="112px">Status MKS</asp:label></TD>
								<TD><asp:label id="Label11" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblStatusValue" runat="server"></asp:label></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:datagrid id="dtgPartDetail" runat="server" Width="100%" BackColor="#CDCDCD" CellPadding="3"
							BorderWidth="0px" CellSpacing="1" BorderColor="#CDCDCD" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn Visible="False" HeaderText="id">
									<ItemTemplate>
										<asp:Label id=lblIDDetail runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										&nbsp;
									</HeaderTemplate>
									<ItemTemplate>
										<asp:CheckBox id="ChkExport" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="4%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" text="<%# container.itemindex+1 %>">> </asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox runat="server"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn HeaderText="Nomor Barang">
									<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Nama Barang">
									<HeaderStyle Width="25%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Model">
									<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Quantity" HeaderText="Jumlah">
									<HeaderStyle Width="4%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Jumlah Dibatalkan">
									<HeaderStyle Width="4%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Keterangan">
									<HeaderStyle Width="16%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD>&nbsp;
						<asp:button id="btnBack" runat="server" Width="72px" Text="Kembali"></asp:button>
						<asp:button id="btnHapus" runat="server" Width="88px" Text="Hapus"></asp:button>&nbsp;
						<INPUT id="btnSendEmail" style="WIDTH: 88px; HEIGHT: 21px" type="button" value="Email"
							name="btnSendEmail" runat="server"></TD>
				</TR>
			</TABLE>
		</form>
		<SCRIPT language="javascript">
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
		</SCRIPT>
	</body>
</HTML>
