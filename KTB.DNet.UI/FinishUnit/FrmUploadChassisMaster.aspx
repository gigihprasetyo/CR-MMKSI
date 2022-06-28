<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmUploadChassisMaster.aspx.vb" Inherits="FrmUploadChassisMaster" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmVehicleModel</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 16px; WIDTH: 680px; POSITION: absolute; TOP: 16px; cellSpacing: "
				cellPadding="1" width="680" border="0">

				<TR>
					<TD class="titlePage"> Upload Chasis Master</TD>
				</TR>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>				
				<TR>
					<TD style="HEIGHT: 130px" vAlign="top" align="left">
						<TABLE id="Table2" style="WIDTH: 592px; HEIGHT: 16px" cellSpacing="1" cellPadding="1" width="592"
							border="0">
							<TR>
								<TD style="WIDTH: 236px; HEIGHT: 15px">Lokasi File</TD>
								<TD style="WIDTH: 394px; HEIGHT: 15px">: <INPUT id="DataFile" style="WIDTH: 293px; HEIGHT: 24px" type="file" size="29" name="File1"
										runat="server"></TD>
								<TD style="WIDTH: 144px; HEIGHT: 15px"><asp:button id="btnUpload" runat="server" Width="96px" Text="Upload"></asp:button></TD>
								<TD style="HEIGHT: 15px"><asp:button id="btnStore" runat="server" Width="88px" Text="Simpan" Height="24px" Enabled="False"></asp:button></TD>
							</TR>
						</TABLE>
						<TABLE id="Table3" style="WIDTH: 368px; cellSpacing: " cellPadding="1" width="368" border="1">
							<TR>
								<TD style="WIDTH: 69px; HEIGHT: 11px">Status</TD>
								<TD style="WIDTH: 166px; HEIGHT: 11px">:
									<asp:dropdownlist id="ddlStatus" runat="server" Width="128px" Height="24px"></asp:dropdownlist></TD>
								<TD style="HEIGHT: 11px"><asp:button id="btnSearch" runat="server" Width="96px" Text="Cari"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 69px; HEIGHT: 6px">Kategori</TD>
								<TD style="WIDTH: 166px; HEIGHT: 6px">:
									<asp:dropdownlist id="ddlCategory" runat="server" Width="128px" Height="24px"></asp:dropdownlist></TD>
								<TD style="HEIGHT: 6px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 69px">Tipe</TD>
								<TD style="WIDTH: 166px">:
									<asp:dropdownlist id="ddlType" runat="server" Width="128px" Height="24px"></asp:dropdownlist></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top"><asp:datagrid id="dgColorUpload" runat="server" Width="720px" AutoGenerateColumns="False" BorderColor="#E7E7FF"
							BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" GridLines="Horizontal">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F7F7F7"></AlternatingItemStyle>
							<ItemStyle ForeColor="#4A3C8C" BackColor="#E7E7FF"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="RowStatusX" HeaderText="Status" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
								<asp:BoundColumn DataField="SpecialFlag" HeaderText="S/P Color"></asp:BoundColumn>
								<asp:BoundColumn DataField="HeaderBOM" HeaderText="BOM"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Tipe">
									<ItemTemplate>
										<asp:Label id="lblVehicleType" runat="server" Text=""></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="ColorCode" HeaderText="Warna"></asp:BoundColumn>
								<asp:BoundColumn DataField="ColorEngName" HeaderText="Inggris"></asp:BoundColumn>
								<asp:BoundColumn DataField="ColorIndName" HeaderText="Indonesia"></asp:BoundColumn>
								<asp:BoundColumn DataField="ErrorMessage" HeaderText="Error"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="MaterialNumber" HeaderText="Material"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="MaterialDescription" HeaderText="Mat Desc"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="MarketCode" HeaderText="Market"></asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid><asp:datagrid id="dgVehicleColor" runat="server" Width="720px" AutoGenerateColumns="False" BorderColor="#E7E7FF"
							BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" GridLines="Horizontal">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F7F7F7"></AlternatingItemStyle>
							<ItemStyle ForeColor="#4A3C8C" BackColor="#E7E7FF"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="RowStatusX" HeaderText="Status">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SpecialFlag" HeaderText="S/P Color"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Category">
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Category.CategoryCode") %>' ID="Label3">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="HeaderBOM" HeaderText="BOM"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Kode Tipe">
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.VechileTypeCode") %>' ID="Label1">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nama Tipe">
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Description") %>' ID="Label2">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="ColorCode" HeaderText="Warna"></asp:BoundColumn>
								<asp:BoundColumn DataField="ColorEngName" HeaderText="Inggris"></asp:BoundColumn>
								<asp:BoundColumn DataField="ColorIndName" HeaderText="Indonesia"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="MaterialNumber" HeaderText="Material"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="MaterialDescription" HeaderText="Mat Desc"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="MarketCode" HeaderText="Market"></asp:BoundColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:LinkButton id="lbtnDelete" runat="server" Text="Del" CommandName="Delete">Hapus</asp:LinkButton>
										<asp:LinkButton id="lbtnActive" runat="server" Text="Undel" CommandName="Active">Aktifkan</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 11px" align="left"><asp:button id="btnPrint" runat="server" Width="96px" Text="Cetak" Height="24px"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnClose" runat="server" Width="96px" Text="Tutup" Height="24px"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
