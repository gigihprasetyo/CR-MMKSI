<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmVehicleKind.aspx.vb" Inherits="FrmVehicleKind" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmVehicleKind</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script>
			function CheckAll(chk,No)
			{
				var dtg = document.getElementById("dtgMain");
				var i=0;
				var j=0;
				var chkItem;
				
				for(i=1;i<=dtg.rows.length-1;i++)
				{
					chkItem = document.getElementById("dtgMain__ctl"+(i+1)+"_chkVehicleKind"+No);
					if(chk)
					{
						chkItem.checked=chk.checked;
					}
				}
				
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">UMUM - Informasi Jenis Kendaraan</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table2" cellSpacing="2" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 114px">Kategori</TD>
								<TD style="WIDTH: 3px">:</TD>
								<TD style="WIDTH: 278px"><asp:dropdownlist id="ddlCategory" runat="server" Width="96px" AutoPostBack="True"></asp:dropdownlist><asp:dropdownlist id="ddlSubCategory" runat="server" Width="152px" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 114px">Status</TD>
								<TD style="WIDTH: 3px">:</TD>
								<TD style="WIDTH: 278px">
									<asp:dropdownlist id="ddlStatus" runat="server" Width="96px"></asp:dropdownlist></TD>
							<TR>
								<TD class="titleField" style="WIDTH: 114px"></TD>
								<TD style="WIDTH: 3px">:</TD>
								<TD style="WIDTH: 278px">
									<asp:button id="btnFind" runat="server" Width="80px" Text="Cari"></asp:button>
									<asp:button id="btnSave" runat="server" Width="80px" Text="Simpan"></asp:button>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" colSpan="6">
									<asp:datagrid id="dtgMain" runat="server" Width="100%" CellSpacing="1" Visible="True" GridLines="Horizontal"
										CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0"
										AutoGenerateColumns="False">
										<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
										<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
										<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
										<ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
										<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="No">
												<HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:Label id="lblNo" runat="server" Text=''></asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Tipe Kendaraan">
												<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
												<ItemTemplate>
													<asp:Label id="lblVehicleTypeCode" runat="server" Text=''></asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Nama Kendaraan">
												<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
												<ItemTemplate>
													<asp:Label id="lblVehicleName" runat="server" Text=''></asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Mobil Penumpang">
												<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<HeaderTemplate>
													<table cellspacing="0" cellpadding="0" border="0">
														<tr>
															<td align="center">
																<asp:Label Runat="server" ID="lblKind1">Mobil Penumpang</asp:Label>
															</td>
														</tr>
														<tr>
															<td align="center">
																<asp:CheckBox Runat="server" ID="chkKind1" Checked="False" OnClick="CheckAll(this,1)"></asp:CheckBox>
															</td>
														</tr>
													</table>
												</HeaderTemplate>
												<ItemTemplate>
													<asp:CheckBox Runat="server" ID="chkVehicleKind1"></asp:CheckBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Mobil Bus">
												<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<HeaderTemplate>
													<table cellspacing="0" cellpadding="0" border="0">
														<tr>
															<td align="center">
																<asp:Label Runat="server" ID="lblKind2">Mobil <br>Bus</asp:Label>
															</td>
														</tr>
														<tr>
															<td align="center">
																<asp:CheckBox Runat="server" ID="chkKind2" Checked="False" OnClick="CheckAll(this,2)"></asp:CheckBox>
															</td>
														</tr>
													</table>
												</HeaderTemplate>
												<ItemTemplate>
													<asp:CheckBox Runat="server" ID="chkVehicleKind2"></asp:CheckBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Mobil Barang">
												<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<HeaderTemplate>
													<table cellspacing="0" cellpadding="0" border="0">
														<tr>
															<td align="center">
																<asp:Label Runat="server" ID="lblKind3">Mobil Barang</asp:Label>
															</td>
														</tr>
														<tr>
															<td align="center">
																<asp:CheckBox Runat="server" ID="chkKind3" Checked="False" OnClick="CheckAll(this,3)"></asp:CheckBox>
															</td>
														</tr>
													</table>
												</HeaderTemplate>
												<ItemTemplate>
													<asp:CheckBox Runat="server" ID="chkVehicleKind3"></asp:CheckBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Kendaraan Khusus">
												<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<HeaderTemplate>
													<table cellspacing="0" cellpadding="0" border="0">
														<tr>
															<td align="center">
																<asp:Label Runat="server" ID="lblKind4">Kendaraan Khusus</asp:Label>
															</td>
														</tr>
														<tr>
															<td align="center">
																<asp:CheckBox Runat="server" ID="chkKind4" Checked="False" OnClick="CheckAll(this,4)"></asp:CheckBox>
															</td>
														</tr>
													</table>
												</HeaderTemplate>
												<ItemTemplate>
													<asp:CheckBox Runat="server" ID="chkVehicleKind4"></asp:CheckBox>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
