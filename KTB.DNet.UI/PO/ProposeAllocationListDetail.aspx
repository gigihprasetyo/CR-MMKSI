<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProposeAllocationListDetail.aspx.vb" Inherits="ProposeAllocationListDetail" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ProposeAllocationListDetail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
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
					<td class="titlePage" colSpan="4">PO HARIAN - Unit Usulan SAP Detail</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" colSpan="4" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td colSpan="4" height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD style="WIDTH: 154px; HEIGHT: 16px"><asp:label id="Label1" runat="server">Tanggal Alokasi</asp:label></TD>
					<TD style="WIDTH: 17px; HEIGHT: 16px"><asp:label id="Label2" runat="server">:</asp:label></TD>
					<TD style="WIDTH: 756px; HEIGHT: 16px"><asp:label id="lblTanggalAlokasi" runat="server"></asp:label></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 154px; HEIGHT: 12px"><asp:label id="Label3" runat="server">Tipe / Warna</asp:label></TD>
					<TD style="WIDTH: 17px; HEIGHT: 12px"><asp:label id="Label5" runat="server">:</asp:label></TD>
					<TD style="WIDTH: 756px; HEIGHT: 12px"><asp:label id="lblTipeWarna" runat="server"></asp:label></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 154px"><asp:label id="Label4" runat="server">Model/Tipe/Warna</asp:label></TD>
					<TD style="WIDTH: 17px"><asp:label id="Label6" runat="server">:</asp:label></TD>
					<TD style="WIDTH: 756px"><asp:label id="lblModelTipeWarna" runat="server"></asp:label></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 154px">
						<asp:Label id="Label10" runat="server">Tahun Perakitan / Impor</asp:Label></TD>
					<TD style="WIDTH: 17px">
						<asp:label id="Label12" runat="server">:</asp:label></TD>
					<TD style="WIDTH: 756px">
						<asp:Label id="lblTahunPerakitan" runat="server"></asp:Label></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 154px">
						<asp:Label id="Label11" runat="server">Stok ATP</asp:Label></TD>
					<TD style="WIDTH: 17px">
						<asp:label id="Label13" runat="server">:</asp:label></TD>
					<TD style="WIDTH: 756px">
						<asp:Label id="lblStokAtp" runat="server"></asp:Label></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 154px"></TD>
					<TD style="WIDTH: 17px"></TD>
					<TD style="WIDTH: 756px"></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD colspan="4">
						<div id="div1" style="HEIGHT: 340px; OVERFLOW: auto"><asp:DataGrid id="dgAllocationDetail" runat="server" Width="50%" AutoGenerateColumns="False" BorderColor="#CDCDCD"
								BackColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3">
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn HeaderText="DealerCode">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Nama">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Tgl Permintaan Kirim">
										<HeaderStyle Width="9%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Tgl Alokasi PO">
										<HeaderStyle Width="9%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Nomor PO">
										<HeaderStyle Width="9%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Nomor PO Dealer">
										<HeaderStyle Width="9%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Order (Unit)">
										<HeaderStyle Width="9%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Alokasi (Unit)">
										<HeaderStyle Width="9%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Sisa (Unit)">
										<HeaderStyle Width="9%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Sisa Sebelum">
										<HeaderStyle Width="9%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Sisa Setelah">
										<HeaderStyle Width="9%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Status">
										<HeaderStyle Width="9%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:DataGrid></div>
					</TD>
				</TR>
				<TR>
					<TD colspan="4" height="40">
						<asp:Button ID="btnBack" Runat="server" Text="Kembali"></asp:Button>&nbsp;
						<asp:Button style="Z-INDEX: 0" id="btnDownload" Text="Download" Runat="server"></asp:Button></TD>
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
