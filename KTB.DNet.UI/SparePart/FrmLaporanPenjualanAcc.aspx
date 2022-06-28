<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmLaporanPenjualanAcc.aspx.vb" Inherits="FrmLaporanPenjualanAcc" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Laporan Penjualan Accessories</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script>			
			function ShowPPDealerSelection()
			{
				showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
			
			function DealerSelection(selectedDealer)
			{
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = selectedDealer;			
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:textbox style="DISPLAY: none" id="txtEnumAccessories" Text="" Runat="server"></asp:textbox>
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="titlePage">PENJUALAN&nbsp;- Laporan Penjualan Accessories</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
				</tr>
				<tr>
					<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="2" width="100%">
							<TR>
								<TD class="titleField" width="24%"><asp:label id="lblKodeDealer" runat="server">Kode Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD width="25%"><asp:textbox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" style="Z-INDEX: 0" id="txtKodeDealer"
										onkeypress="return alphaNumericExcept(event,'<>?*%$')" runat="server"></asp:textbox><asp:label style="Z-INDEX: 0" id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label style="Z-INDEX: 0" id="Label8" runat="server">Kategori Input</asp:label></TD>
								<TD>:</TD>
								<TD><asp:dropdownlist id="ddlKategori" runat="server" Width="128px"></asp:dropdownlist></TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 26px" class="titleField"><asp:label style="Z-INDEX: 0" id="lblNomorLaporan" runat="server">Nomor Laporan</asp:label></TD>
								<TD style="HEIGHT: 26px"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 26px"><asp:textbox id="txtReportNumber" Text="" Runat="server" Width="128px"></asp:textbox></TD>
								<TD style="HEIGHT: 26px" class="titleField"></TD>
								<TD style="HEIGHT: 26px"></TD>
								<TD style="HEIGHT: 26px"></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label style="Z-INDEX: 0" id="lblNomorPermintaan" runat="server">Nomor Referensi</asp:label></TD>
								<TD><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD><asp:textbox onblur="alphaNumericPlusSpaceBlur(txtRefNum)" id="txtRefNum" onkeypress="alphaNumericPlusSpaceUniv(event)"
										runat="server" MaxLength="15"></asp:textbox></TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR vAlign="top">
								<TD style="HEIGHT: 29px" class="titleField"><asp:label id="lblNoRangka" runat="server">Nomor Rangka</asp:label></TD>
								<TD style="HEIGHT: 29px"><asp:label id="Label9" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 29px">
									<table cellSpacing="0" cellPadding="0">
										<tr>
											<td><asp:textbox onblur="alphaNumericPlusSpaceBlur(txtNomorPolisi)" id="txtNoRangka" onkeypress="return alphaNumeric(event)"
													runat="server" MaxLength="20"></asp:textbox></td>
											<td style="DISPLAY: none"><asp:linkbutton id="lnkbtnCheckChassis" Runat="server" ToolTip="Validate Chassis" CausesValidation="False">
													<img style="cursor:hand" alt="Check Chassis" src="../images/tanya.gif" border="0">
												</asp:linkbutton></td>
											<td style="DISPLAYx: none"></td>
										</tr>
									</table>
								</TD>
								<TD style="HEIGHT: 29px" class="titleField"></TD>
								<TD style="HEIGHT: 29px"></TD>
								<TD style="HEIGHT: 29px"></TD>
							</TR>
							<TR>
								<TD class="titleField">
									<table border="0" cellSpacing="0" cellPadding="0">
										<tr>
											<td>
												<asp:CheckBox Runat="server" ID="chkPenjualan" Text="" Checked="True"></asp:CheckBox>
											</td>
											<td>
												<asp:label id="lblTanggalInput" runat="server">Tanggal Penjualan</asp:label>
											</td>
										</tr>
									</table>
								</TD>
								<TD>
									<asp:label id="Label2" runat="server">:</asp:label>
								</TD>
								<TD>
									<table border="0" cellSpacing="0" cellPadding="0">
										<tr>
											<td><cc1:inticalendar id="calStart" runat="server" Enabled="True"></cc1:inticalendar></td>
											<td width="1">s/d
											</td>
											<td><cc1:inticalendar id="calEnd" runat="server" Enabled="True"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="titleField">
									<table border="0" cellSpacing="0" cellPadding="0">
										<tr>
											<td>
												<asp:CheckBox Runat="server" ID="chkPembuatan" Text="" Checked="False"></asp:CheckBox>
											</td>
											<td>
												<asp:label style="Z-INDEX: 0" id="Label15" runat="server">Tanggal Pembuatan</asp:label>
											</td>
										</tr>
									</table>
								</TD>
								<TD>:</TD>
								<TD>
									<table border="0" cellSpacing="0" cellPadding="0">
										<tr>
											<td><cc1:inticalendar id="calCreatedStart" runat="server" Enabled="True"></cc1:inticalendar></td>
											<td width="1">s/d
											</td>
											<td><cc1:inticalendar id="calCreatedEnd" runat="server" Enabled="True"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
								<TD class="titleField"></TD>
								<TD><asp:button id="btnFind" runat="server" Text="Cari" Width="80px" style="Z-INDEX: 0"></asp:button></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD colSpan="6"><asp:datagrid id="dtgMain" runat="server" Width="100%" BackColor="#E0E0E0" AutoGenerateColumns="False"
										BorderColor="#E0E0E0" BorderStyle="None" BorderWidth="0px" CellPadding="3" ShowFooter="False" Font-Size="Small"
										CellSpacing="1">
										<FooterStyle Font-Size="Small" ForeColor="#003399" BackColor="White"></FooterStyle>
										<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
										<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
										<ItemStyle BackColor="White"></ItemStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="No">
												<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
												<ItemTemplate>
													<asp:Label id="lblNo" runat="server" NAME="lblNo" text= '<%# container.itemindex+1 %>'>
													</asp:Label>
												</ItemTemplate>
												<FooterStyle Font-Size="Small"></FooterStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Kategori Input">
												<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<asp:Label Runat="server" ID="lblKategoriInput" Text='<%# DataBinder.Eval(Container.DataItem, "AccessoriesCategory.Name" )  %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="No Laporan">
												<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<asp:Label Runat="server" ID="Label14" Text='<%# DataBinder.Eval(Container.DataItem, "ReportNumber" )  %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Kode Dealer">
												<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<asp:Label Runat="server" ID="Label4" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode" )  %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Nama Dealer">
												<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<asp:Label Runat="server" ID="Label17" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName" )  %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Kota">
												<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<asp:Label Runat="server" ID="Label18" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.City.CityName" )  %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Tanggal Pengajuan">
												<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<asp:Label Runat="server" ID="Label5" Text='<%# DataBinder.Eval(Container.DataItem, "SoldDate" )  %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Tanggal Pembuatan">
												<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<asp:Label Runat="server" ID="Label16" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedTime" )  %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="No Referensi">
												<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<asp:Label Runat="server" ID="Label7" Text='<%# DataBinder.Eval(Container.DataItem, "RefNumber" )  %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="No.Rangka">
												<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<asp:Label Runat="server" ID="Label10" Text='<%# DataBinder.Eval(Container.DataItem, "ChassisMaster.ChassisNumber" )  %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Nama Customer">
												<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<asp:Label Runat="server" ID="Label11" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName" )  %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Nomor Telp">
												<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<asp:Label Runat="server" ID="Label12" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerPhone" )  %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Comments">
												<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<asp:Label Runat="server" ID="lblComment" Text=''>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn>
												<HeaderStyle Width="1%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:LinkButton id="lbtnDetail" runat="server" CommandName="Detail">
														<img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn>
												<HeaderStyle Width="1%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:LinkButton id="lbtnEdit" runat="server" CommandName="Edit">
														<img src="../images/edit.gif" border="0" alt="Edit"></asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><br>
								</TD>
							</TR>
							<TR>
								<TD colSpan="6"><asp:button id="btnDownload" runat="server" Text="Download"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 151px" colSpan="2"></TD>
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
