<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDownloadDoc.aspx.vb" Inherits="FrmDownloadDoc" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDownloadDoc</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<h4><asp:Label ID="lblTitle" Runat="server"></asp:Label></h4>
			<asp:DataGrid id="dtgDownload" runat="server" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
				BackColor="White" CellPadding="3" AutoGenerateColumns="False" ShowFooter ="True">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<ItemStyle ForeColor="#000066"></ItemStyle>
				<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#006699"></HeaderStyle>
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<Columns>
					<asp:BoundColumn HeaderText="No"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Kode Vendor/Customer" DataField="KodeDealer"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Nama Vendor/Customer" DataField="NamaDealer"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Nomor Jurnal Voucher (JV)"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Tanggal Pembayaran"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Keterangan Transaksi"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Account GL *)"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Cost Center *)"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Family/Model" DataField="Family"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Variant" DataField="Variants"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Business Area *)"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Unit" DataField="Unit"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Jumlah DPP" DataField="JumlahDpp" DataFormatString="{0:#,##0.00}" ></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Jumlah PPN" DataField="JumlahPpn" DataFormatString="{0:#,##0.00}"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Jumlah (PPN + DPP )" DataField="JumlahDppPpn" DataFormatString="{0:#,##0.00}" ></asp:BoundColumn>
					
				</Columns>
				
				<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
			
			</asp:DataGrid>
		</form>
	</body>
</HTML>
