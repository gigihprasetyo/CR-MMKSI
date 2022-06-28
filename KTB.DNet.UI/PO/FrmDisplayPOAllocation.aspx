<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDisplayPOAllocation.aspx.vb" Inherits="FrmDisplayPOAllocation" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDisplayPOAllocation</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PO HARIAN&nbsp;- Alokasi PO</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TBODY>
								<TR>
									<TD class="titleField" width="20%"><asp:label id="Label2" runat="server">Tahun Perakitan/Impor</asp:label></TD>
									<TD width="1%">:</TD>
									<TD width="30%"><asp:dropdownlist id="ddlTahunPerakitan" runat="server"></asp:dropdownlist></TD>
									<TD class="titleField" width="15%"><asp:label id="Label3" runat="server">Kategori</asp:label></TD>
									<TD width="1%">:</TD>
									<TD width="35%"><asp:dropdownlist id="ddlKategori" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="titleField"><asp:label id="Label6" runat="server">Jenis Order</asp:label></TD>
									<TD>:</TD>
									<TD><asp:dropdownlist id="ddlJenisOrder" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
									<TD class="titleField" style="HEIGHT: 19px"><asp:label id="Label4" runat="server">Tipe</asp:label></TD>
									<TD style="HEIGHT: 19px">:</TD>
									<TD style="HEIGHT: 19px"><asp:dropdownlist id="ddlTipe" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
								</TR>
								<tr>
									<TD class="titleField"><asp:label id="Label1" runat="server">Permintaan Kirim</asp:label></TD>
									<TD width="1%">:</TD>
									<TD noWrap width="43%">
										<table cellSpacing="0" cellPadding="0" border="0">
											<tr>
												<td><cc1:inticalendar id="icPermintaanKirim1" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
												<td>&nbsp;s.d&nbsp;</td>
												<td><cc1:inticalendar id="icPermintaanKirim2" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											</tr>
										</table>
									</TD>
									<TD class="titleField"><asp:label id="Label5" runat="server">Tipe / Warna</asp:label></TD>
									<TD>:</TD>
									<TD noWrap><asp:dropdownlist id="ddlTipeWarna" runat="server"></asp:dropdownlist></TD>
								</tr>
								<TR>
									<TD height="10"><asp:checkbox id="CheckBoxDS" runat="server" Font-Size="Medium" Font-Bold="True" Text="W/O Download Stock"></asp:checkbox></TD>
									<td></td>
									<TD height="10"></TD>
									<td></td>
									<td></td>
									<td><asp:button id="btnCari" runat="server" Text="Cari" Width="55px"></asp:button><asp:button id="btnDownload1" runat="server" Text="Data" Width="60px" Visible="True"></asp:button><asp:button id="btnDownload2" runat="server" Text="Report" Width="60px" Visible="True"></asp:button></td>
								</TR>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD colSpan="4">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 340px"><asp:datagrid id="dtgPOAllocation" runat="server" Width="100%" ShowFooter="True" AutoGenerateColumns="False"
								BackColor="#CDCDCD" OnItemDataBound="dtgPOAllocation_itemdataBound" OnItemCommand="dtgPOAllocation_itemCommand" BorderColor="#CDCDCD" CellSpacing="1"
								BorderWidth="0px" CellPadding="3">
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle" BackColor="Blue"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn HeaderText="Model / Tipe / Warna">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Kode Tipe / Warna">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" HeaderText="Sisa O/C (unit)">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="ATP Stok (unit)">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Total Order (unit)">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Total Alokasi (unit)">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Total Order Tidak Terpenuhi (unit)">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableSales" VerticalAlign="Top"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnAlokasi" runat="server" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Alokasi"></asp:LinkButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn>
										<HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></div>
						<asp:label id="Label11" runat="server" Font-Size="Smaller" Font-Bold="True">Button W/O Download Stock is left blank if MMKSI re-download ZFUO0002 programme to allocate PO with order type ‘Tambahan’.</asp:label></TD>
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
