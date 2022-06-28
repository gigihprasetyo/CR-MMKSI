<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmUploadSPPOText.aspx.vb" Inherits="FrmUploadSPPOText" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmVehicleModel</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PEMESANAN&nbsp;- Upload Melalui File Text</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table4" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="30%">Kode Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:label id="lblDealerCode" runat="server">AWFO</asp:label></TD>
								<TD width="44%"><asp:label id="lblError" runat="server" ForeColor="Red" Width="179px">Error message</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Dealer</TD>
								<TD>:</TD>
								<TD><asp:label id="lblDealerName" runat="server">Bumen Redja Abadi</asp:label>&nbsp;/
									<asp:label id="lblSearchTerm2" runat="server">AWF0</asp:label></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="titleField">Tipe Order</TD>
								<TD>:</TD>
								<TD><asp:label id="lblOrderType" runat="server" Width="296px">Emergency</asp:label></TD>
								<TD><asp:label id="lblTypeCode" runat="server" Width="96px" Visible="False">Hidden Code</asp:label></TD>
							</TR>
                            <TR id="trPQRNo" runat="server" visible="false">
								<TD class="titleField">Nomor PQR</TD>
								<td width="1%">:</td>
								<TD>
                                    <asp:label id="lblPQRNo" runat="server" Width="216px">100676/20/PC/1</asp:label>
								</TD>
                                <TD><asp:label id="lblPQRError" runat="server" ForeColor="Red" Width="179px">Error message</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Dealer PO No / Tanggal</TD>
								<TD>:</TD>
								<TD><asp:label id="lblPONo" runat="server" Width="216px">EAB01620000001</asp:label><asp:label id="lblPODate" runat="server" Width="72px">22/03/2005</asp:label></TD>
								<TD>
									<asp:label id="lblPOError" runat="server" ForeColor="Red" Width="179px">Error message</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Nilai Pemesanan</TD>
								<TD>:</TD>
								<TD><b>Rp
										<asp:label id="lblPOTotAmnt" runat="server" Width="216px">25.500.000</asp:label></b></TD>
								<TD></TD>
							</TR>
                            <tr>
                                <td class="titleField">Cara Pembayaran</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:DropDownList ID="ddlTermOfPayment" runat="server" Width="160px" Visible="True"></asp:DropDownList>
                                </td>
                            </tr>
							<TR>
								<TD class="titleField">Upload File</TD>
								<TD>:</TD>
								<TD><INPUT id="DataFile" onkeypress="return false;" type="file" size="29" name="File1" runat="server"></TD>
								<TD><asp:button id="btnUpload" runat="server" Width="70px" Text="Upload"></asp:button> &nbsp;
                                    <asp:button id="btnDownloadSample" runat="server" Width="110px" Text="Download Sample"></asp:button>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top"><div id="div1" style="OVERFLOW: auto; HEIGHT: 310px">
							<asp:datagrid id="dgSPPODetail" runat="server" Width="100%" CellSpacing="1" GridLines="Horizontal"
								CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="Gainsboro"
								AutoGenerateColumns="False" AllowSorting="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nomor Barang" SortExpression="PartNumberTemp">
										<HeaderStyle Width="13%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPartNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartNumberTemp") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nama Barang" SortExpression="PartNameTemp">
										<HeaderStyle Width="25%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPartName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartNameTemp") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Quantity" HeaderText="Jumlah" DataFormatString="{0:#,##0}" SortExpression="Quantity">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RetailPrice" HeaderText="Harga Eceran (Rp)" DataFormatString="{0:#,##0}"
										SortExpression="RetailPrice">
										<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<%--<asp:BoundColumn DataField="Amount" HeaderText="Total Harga (Rp)" DataFormatString="{0:#,##0}" SortExpression="Amount">
										<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>--%>                                    
									<asp:TemplateColumn SortExpression="Amount" HeaderText="Total Harga (Rp)">
										<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblPOAmount runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Amount","{0:#,##0}") %>' CssClass="textRight">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ErrorMessage" HeaderText="Pesan" SortExpression="ErrorMessage">
										<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 11px" align="left">
						<TABLE id="Table5" cellPadding="1" width="100%" border="0">
							<TR>
								<TD width="50%" height="40"><asp:button id="btnNew" runat="server" Width="60px" Text="Baru" CausesValidation="False"></asp:button><asp:button id="btnCancel" runat="server" Width="60px" Text="Batal" Enabled="False"></asp:button><asp:button id="btnSave" runat="server" Width="60px" Text="Simpan" Enabled="False"></asp:button><asp:button id="btnPrint" runat="server" Width="60px" Text="Cetak" Enabled="False"></asp:button><asp:button id="btnSubmit" runat="server" Width="60px" Text="Kirim" Enabled="False"></asp:button></TD>
								<TD align="left" width="50%">* Pemesanan melalui file text tidak dapat diedit atau 
									ditambah</TD>
							</TR>
						</TABLE>
						<INPUT id="hid_f" type="hidden" name="hid_f" runat="server">
					</TD>
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
