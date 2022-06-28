<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpPaymentStatus.aspx.vb" Inherits="PopUpPaymentStatus" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Status Payment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function firstFocus()
		{
			if (document.all.txtlblNoReqPO != null)
			{
			document.all.txtlblNoReqPO.focus();
			}
		}
		
		</script>
	</HEAD>
	<body onload="firstFocus()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="titlePage" colSpan="6">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">EQUIPMENT SALES - Status Pembayaran</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="/images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD class="titleField" width="20%"><asp:label id="lblNoReqPO" runat="server">No Reg PO</asp:label></TD>
					<TD width="1%">:</TD>
					<TD width="30%"><asp:label id="lblNoReqPOValue" runat="server"></asp:label><asp:textbox id="txtlblNoReqPO" runat="server"></asp:textbox><asp:button id="btnCari" runat="server" Width="50px" Text="Cari"></asp:button></TD>
					<TD class="titleField" width="20%"><asp:label id="lblKodeDealer" runat="server">Dealer</asp:label></TD>
					<TD width="1%">:</TD>
					<TD width="30%"><asp:label id="lblDealerCode" runat="server"></asp:label>&nbsp;/
						<asp:label id="lblSearchTerm1" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblNoP3B" runat="server">Nomor P3B</asp:label></TD>
					<TD>:</TD>
					<TD><asp:label id="lblP3BNumber" runat="server"></asp:label></TD>
					<TD class="titleField"><asp:label id="lblTotal" runat="server">Total</asp:label></TD>
					<TD>:</TD>
					<TD>
						<table cellPadding="0" width="180" border="0">
							<tr>
								<td noWrap width="20">Rp</td>
								<td noWrap align="right" width="100"><asp:label id="lblTotalValue" runat="server"></asp:label></td>
								<td width="60">&nbsp;</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 5px"><asp:label id="Label1" runat="server">Tanggal P3B</asp:label></TD>
					<TD style="HEIGHT: 5px">:</TD>
					<TD style="HEIGHT: 5px"><asp:label id="lblP3BDate" runat="server"></asp:label></TD>
					<TD class="titleField" style="HEIGHT: 5px"><asp:label id="lblTotalPembayaran" runat="server">Total Pembayaran</asp:label></TD>
					<TD style="HEIGHT: 5px">:</TD>
					<TD>
						<table cellPadding="0" width="180" border="0">
							<tr>
								<td noWrap width="20">Rp</td>
								<td noWrap align="right" width="100"><asp:label id="lblTotalPembayaranValue" runat="server"></asp:label></td>
								<td width="60"><asp:label id="lblTotalPayPCT" runat="server"></asp:label></td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 5px"><asp:label id="lblJenis" runat="server">Jenis</asp:label></TD>
					<TD style="HEIGHT: 5px">:</TD>
					<TD style="HEIGHT: 5px"><asp:label id="lblJenisValue" runat="server"></asp:label></TD>
					<TD class="titleField" style="HEIGHT: 5px"><asp:label id="lblSisaPembayaran" runat="server">Sisa Pembayaran</asp:label></TD>
					<TD style="HEIGHT: 5px">:</TD>
					<TD>
						<table cellPadding="0" width="180" border="0">
							<tr>
								<td noWrap width="20">Rp</td>
								<td noWrap align="right" width="100"><asp:label id="lblSisaPembayaranValue" runat="server"></asp:label></td>
								<td width="60"><asp:label id="lblSisaPCT" runat="server"></asp:label></td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 2px"><asp:label id="Label2" runat="server">Status</asp:label></TD>
					<TD style="HEIGHT: 2px">:</TD>
					<TD style="HEIGHT: 2px"><asp:label id="lblStatus" runat="server"></asp:label></TD>
					<TD class="titleField" style="HEIGHT: 2px"></TD>
					<TD style="HEIGHT: 2px"></TD>
					<TD style="HEIGHT: 2px"></TD>
				</TR>
				<TR>
					<TD vAlign="top" colSpan="6"><asp:datagrid id="dtgEquipmentPayment" runat="server" Width="100%" BorderWidth="0px" CellSpacing="1"
							BorderColor="#CDCDCD" OnItemDataBound="dtgEquipmentPayment_ItemDataBound" OnItemCommand="dtgEquipmentPayment_ItemCommand"
							ShowFooter="True" AutoGenerateColumns="False" CellPadding="3" BackColor="#E0E0E0">
							<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
							<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="id" HeaderText="id"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblNo runat="server" NAME="lblNo" text="<%# container.itemindex+1 %>">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nomor Kwitansi">
									<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblNomorKwitansi runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "KwitansiNumber") %>' NAME="lblNomorKwitansi">
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="txtNomorKwitansi" runat="server" MaxLength="40"></asp:TextBox>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Jumlah (Rp)">
									<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblJumlah runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Amount", "{0:#,###}" ) %>' NAME="lblJumlah" CssClass="TextRight">
										</asp:Label>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
									<FooterTemplate>
										<asp:TextBox id="txtJumlah" onkeypress="return numericOnlyUniv(event)" onKeyUp="pic(this,this.value,'9999999999','N')" runat="server" MaxLength="12" CssClass="TextRight"></asp:TextBox>										
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="CreatedTime" HeaderText="Tanggal Proses" DataFormatString="{0:dd/MM/yyyy}">
									<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Diproses Oleh">
									<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="0%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete">
											<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="0%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<FooterTemplate>
										<asp:LinkButton id="lbtnAdd" runat="server" CommandName="Add">
											<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Mode="NumericPages"></PagerStyle>
						</asp:datagrid><asp:label id="lblError" runat="server" ForeColor="Red" EnableViewState="False"></asp:label></TD>
				</TR>
				<TR id="OpClient1" runat="server">
					<TD vAlign="middle" colSpan="6" align=center><INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel"></TD>
				</TR>
				<TR id="OpClient2" runat="server" align=center>
					<TD vAlign="middle" colSpan="6"><asp:button id="btnBack" Text="Kembali" Runat="server"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
