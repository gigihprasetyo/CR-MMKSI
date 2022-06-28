<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmUploadItemAnnualDiscount.aspx.vb" Inherits="frmUploadItemAnnualDiscount" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>UploadItemAnnualDiscount</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" colSpan="3">ANNUAL DISCOUNT - Upload Item Annual Discount</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" colSpan="3" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td colSpan="3" height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD class="titleField" width="20%"><asp:label id="lblPeriode" runat="server">Periode</asp:label></TD>
					<TD width="1%">:</TD>
					<TD width="79%">
						<table id="Table2" height="100%" cellSpacing="0" cellPadding="0"  border="0">
							<tr>
								<td width="20%"><cc1:inticalendar id="icPeriodeAwal" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
								<td width="5%" align=center>s/d</td>
								<td width="25%"><cc1:inticalendar id="icPeriodeAkhir" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
								<td width="50%"><asp:button id="btnCari" runat="server" Text="Cari" Width="60"></asp:button></td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblPilihLokasiFile" runat="server"> Lokasi File</asp:label></TD>
					<TD><asp:label id="Label2" runat="server">:</asp:label></TD>
					<TD><INPUT onkeypress="return false;" id="DataFile" style="WIDTH: 300px" type="file" name="File1"
							runat="server">
						<asp:button id="btnUpload" runat="server" Text="Upload"></asp:button>&nbsp;<asp:button id="btnSimpan" runat="server" Text="Simpan" Enabled="False"></asp:button>
						<asp:button id="btnClear" runat="server" Text="Baru" Enabled="False"></asp:button>
						<asp:button id="btnHapus" runat="server" Text="Hapus Semua" Enabled="False"></asp:button></TD>
				</TR>
				<TR>
					<TD colSpan="3"></TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 410px" DESIGNTIMEDRAGDROP="95"><asp:datagrid id="dtgItemAnnualDiscount" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
								BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0" CellPadding="3" OnPageIndexChanged="paging_grid" GridLines="Horizontal" OnItemDataBound="dtgAnnualDiscount_ItemDataBound"
								CellSpacing="1" AllowPaging="True" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="1%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" NAME="lblNo"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="PartNo" HeaderText="Nomor Barang">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="partName" HeaderText="Nama Barang">
										<HeaderStyle Width="7%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Model" HeaderText="Model">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="MinimumQty" HeaderText="Jumlah Minimum">
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Point" HeaderText="Point">
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ErrorMessage" HeaderText="Keterangan">
										<HeaderStyle Width="7%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#404040" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
					</tr>
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
