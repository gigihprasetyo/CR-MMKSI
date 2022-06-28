<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpInfoKendaraan.aspx.vb" Inherits="PopUpInfoKendaraan" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
	
		<title>Informasi Kendaraan</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<base target=_self >
	    <style type="text/css">
            .auto-style1 {
                font-family: Sans-Serif, Arial;
                font-size: 11px;
                color: #000000;
                margin: 0px;
                font-weight: bold;
                text-align: left;
                width: 117px;
            }
            .auto-style2 {
                font-family: Sans-Serif, Arial;
                font-size: 11px;
                color: #000000;
                margin: 0px;
                font-weight: bold;
                text-align: left;
                height: 18px;
                width: 117px;
            }
            .auto-style3 {
                width: 179px;
            }
            .auto-style4 {
                height: 18px;
                width: 179px;
            }
            .auto-style5 {
                font-family: Sans-Serif, Arial;
                font-size: 11px;
                color: #000000;
                margin: 0px;
                font-weight: bold;
                text-align: left;
                width: 140px;
            }
            .auto-style6 {
                font-family: Sans-Serif, Arial;
                font-size: 11px;
                color: #000000;
                margin: 0px;
                font-weight: bold;
                text-align: left;
                height: 18px;
                width: 140px;
            }
        </style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD colSpan="6">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">UMUM - Informasi Kendaraan</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD class="auto-style1">Nomor Rangka</TD>
					<TD>:</TD>
					<TD class="auto-style3"><asp:label id="lblChassisNumber" runat="server"></asp:label></TD>
					<TD class="auto-style5">No Serial</TD>
					<TD>:</TD>
					<TD><asp:label id="lblNoSerial" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="auto-style1">Model / Tipe / Warna</TD>
					<TD>:</TD>
					<TD class="auto-style3"><asp:label id="lblMaterial" runat="server"></asp:label></TD>
					<TD class="auto-style5">Tanggal Buka Faktur</TD>
					<TD>:</TD>
					<TD><asp:label id="lblFakturOpenDate" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="auto-style1">No Rangka</TD>
					<TD>:</TD>
					<TD class="auto-style3"><asp:label id="lblNoChassis" runat="server"></asp:label></TD>
					<TD class="auto-style5" style="display:none">No Mesin</TD>
					<TD style="display:none">:</TD>
					<TD style="display:none"><asp:label id="lblNoEngine" runat="server"></asp:label></TD>
					<TD class="titleField">Tanggal Cetak DO</TD>
					<TD>:</TD>
					<TD><asp:label id="lblDOPrintDate" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="auto-style1">Dealer Alokasi</TD>
					<TD>:</TD>
					<TD class="auto-style3"><asp:label id="lblDealerSold" runat="server"></asp:label></TD>
					<TD class="auto-style5">Tanggal Unit Keluar MKS</TD>
					<TD>:</TD>
					<TD><asp:label id="lblStockOutDate" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="auto-style2">Dealer Pelaksana PDI</TD>
					<TD style="HEIGHT: 18px">:</TD>
					<TD class="auto-style4"><asp:label id="lblDealerPDI" runat="server"></asp:label></TD>
					<TD class="auto-style6">Tanggal PDI</TD>
					<TD style="HEIGHT: 18px">:</TD>
					<TD style="HEIGHT: 18px"><asp:label id="lblTglPDI" runat="server"></asp:label><asp:label id="lblPDIIndicator" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 10px" colSpan="6"></TD>
				</TR>
				<TR>
					<TD colSpan="6"><EM><STRONG><FONT size="2">Free Service Data</FONT></STRONG></EM></TD>
				</TR>
				<TR>
					<TD colSpan="6">
						<div id="div1" style="OVERFLOW: auto; WIDTH: 771px"><asp:datagrid id="dtgServiceData" runat="server" Width="768px" BackColor="#CDCDCD" CellPadding="3"
								BorderWidth="0px" CellSpacing="1" BorderColor="#CDCDCD" PageSize="50" AutoGenerateColumns="False" AllowSorting="True">
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle ForeColor="White"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="FSKind.KindCode" HeaderText="Kind">
										<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblKind runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FSKind.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="MileAge" SortExpression="MileAge" HeaderText="Jarak Tempuh" DataFormatString="{0:#,###}">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Tanggal FS">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblFS runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CreatedTime" HeaderText="Tanggal Proses">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblTglPro runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer Pelaksana">
										<HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblDealer runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Status">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDelete" Runat="server" CommandName="Delete" text="Hapus" CausesValidation="False">
												<img border="0" src="../images/trash.gif" alt="Hapus" style="cursor:hand"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<tr>
					<TD colSpan="6"><EM><STRONG><FONT size="2">Periodical Maintenance&nbsp;Data</FONT></STRONG></EM></TD>
					</TD></tr>
				<tr>
					<TD colSpan="6"><asp:datagrid id="dgPMStatus" runat="server" Width="100%" BackColor="#CDCDCD" CellPadding="3"
							BorderWidth="0px" CellSpacing="1" BorderColor="#E7E7FF" AutoGenerateColumns="False" AllowSorting="True"
							AllowPaging="True" GridLines="Vertical" BorderStyle="None" ShowFooter="True">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="StandKM" HeaderText="Kind">
									<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblJenis" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PMKindCode") %>' Tooltip='<%# DataBinder.Eval(Container, "DataItem.PMKindDesc") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="StandKM" SortExpression="StandKM" HeaderText="Jarak Tempuh" DataFormatString="{0:#,###}">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Tanggal PM">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblTglPM" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate","{0:dd/MM/yyyy}") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="ReleaseDate" HeaderText="Tanggal Proses">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblTglRilis" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReleaseDate","{0:dd/MM/yyyy}") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer Pelaksana">
									<HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblKodeDealer" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1") %>' Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' >
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="PMStatus" HeaderText="Status">
									<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblStatusPM" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False">
									<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblPopUpDetail" runat="server">
											<img src="../images/detail.gif" border="0" alt="Replacement Part"></asp:Label>
										<asp:LinkButton id="Linkbutton1" Runat="server" CommandName="DeletePM" text="Hapus" CausesValidation="False">
											<img border="0" src="../images/trash.gif" alt="Hapus" style="cursor:hand"></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderText="ID">
									<ItemTemplate>
										<asp:Label id=lblPMID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id=TextBox2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</tr>
				<tr>
					<td align="center" colSpan="6"><INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel">
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
