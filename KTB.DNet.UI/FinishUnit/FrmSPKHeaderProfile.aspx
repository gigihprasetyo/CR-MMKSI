<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSPKHeaderProfile.aspx.vb" Inherits="FrmSPKHeaderProfile" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SPK Awal</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		var indexRow;
		function BackToPrev()
		{
			var url=document.getElementById("txtUrlToBack").value;
			window.location=url;
		}
		
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">MARKETING - SPK Awal</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" colSpan="3" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 180px; HEIGHT: 24px">Kode Dealer</TD>
								<TD style="WIDTH: 2px; HEIGHT: 24px">:</TD>
								<TD style="HEIGHT: 24px" width="25%"><asp:label id="lblDealer" runat="server"></asp:label></TD>
								<TD class="titleField" style="WIDTH: 149px; HEIGHT: 24px" width="149">Nomor SPK Reference </TD>
								<TD style="HEIGHT: 24px" width="1%"></TD>
								<TD style="WIDTH: 155px; HEIGHT: 24px" width="155"><asp:label id="lblNoSPKReference" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 180px; HEIGHT: 24px">Nomor Reg. SPK</TD>
								<TD style="WIDTH: 2px; HEIGHT: 24px">:</TD>
								<TD><asp:label id="lblNoSPK" runat="server"></asp:label></TD>
								<TD class="titleField" style="WIDTH: 180px; HEIGHT: 24px">Nomor SPK Dealer</TD>
								<TD style="WIDTH: 2px; HEIGHT: 24px">:</TD>
								<TD style="WIDTH: 155px; HEIGHT: 18px"><asp:label id="lblNoSPKDealer" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 180px; HEIGHT: 24px"><asp:label id="Label4" runat="server" Font-Bold="True">Status SPK</asp:label></TD>
								<TD style="WIDTH: 2px; HEIGHT: 24px">:</TD>
								<TD style="HEIGHT: 18px"><asp:label id="lblStatus" runat="server"></asp:label></TD>
								<TD class="titleField" style="WIDTH: 149px"><asp:label id="lblSalesman" runat="server" Font-Bold="True">Salesman</asp:label></TD>
								<TD style="WIDTH: 2px; HEIGHT: 24px">:</TD>
								<TD style="WIDTH: 155px; HEIGHT: 18px"><asp:label id="lblSalesmanCode" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 180px; HEIGHT: 24px"><asp:label id="Label13" runat="server" Font-Bold="True">Total Unit</asp:label></TD>
								<TD style="WIDTH: 2px; HEIGHT: 24px">:</TD>
								<TD style="HEIGHT: 27px"><asp:label id="lblTotalUnit" runat="server"></asp:label>
								</TD>
								<TD class="titleField" style="WIDTH: 180px; HEIGHT: 24px"><asp:label id="txtKondisiPesanan" runat="server" Font-Bold="True">Nama</asp:label></TD>
								<TD style="WIDTH: 2px; HEIGHT: 24px">:</TD>
								<TD style="WIDTH: 155px; HEIGHT: 18px"><asp:label id="lblNamaSalesman" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 180px; HEIGHT: 24px"><asp:label id="Label11" runat="server" Font-Bold="True">Tanggal Buka SPK</asp:label></TD>
								<TD style="WIDTH: 2px; HEIGHT: 24px">:</TD>
								<TD style="WIDTH: 257px; HEIGHT: 25px"><asp:label id="lblSPKOpenDate" runat="server"></asp:label>
								</TD>
								<TD class="titleField" style="WIDTH: 180px; HEIGHT: 24px"><asp:label id="Label9" runat="server"> Level</asp:label></TD>
								<TD style="WIDTH: 2px; HEIGHT: 24px">:</TD>
								<TD style="HEIGHT: 27px"><asp:label id="lblLevelSalesman" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 180px; HEIGHT: 24px"><asp:label id="Label15" runat="server" Font-Bold="True">Dibuat Oleh</asp:label></TD>
								<TD style="WIDTH: 2px; HEIGHT: 24px">:</TD>
								<TD style="HEIGHT: 18px"><asp:label id="lblDibuatOleh" runat="server"></asp:label></TD>
								<TD class="titleField" style="WIDTH: 180px; HEIGHT: 24px"><asp:label id="Label12" runat="server"> Jabatan</asp:label></TD>
								<TD style="WIDTH: 9px; HEIGHT: 25px">:</TD>
								<TD style="HEIGHT: 25px"><asp:label id="lblJabatan" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 180px; HEIGHT: 24px"></TD>
								<TD style="WIDTH: 2px; HEIGHT: 24px"></TD>
								<TD style="HEIGHT: 18px"></TD>
								<TD class="titleField" style="WIDTH: 180px; HEIGHT: 24px"><asp:label id="Label6" runat="server">Dealer Babit & Event</asp:label></TD>
								<TD style="WIDTH: 9px; HEIGHT: 25px">:</TD>
								<TD style="HEIGHT: 25px"><asp:label id="lblCampaignName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 180px; HEIGHT: 24px"><asp:label id="Label7" runat="server" Font-Bold="True" Visible="False">Kategori Kendaraan</asp:label></TD>
								<TD style="WIDTH: 2px; HEIGHT: 24px">&nbsp;</TD>
								<TD style="HEIGHT: 18px"><asp:label id="lblKategoriKendaraan" runat="server"></asp:label></TD>
								<TD class="titleField" style="WIDTH: 180px; HEIGHT: 24px"><asp:label id="Label16" runat="server" Font-Bold="True" Visible="False">Total Harga</asp:label></TD>
								<TD style="WIDTH: 2px; HEIGHT: 24px">&nbsp;</TD>
								<TD style="WIDTH: 155px; HEIGHT: 18px"><asp:label id="lblTotalHarga" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 180px; HEIGHT: 24px"><asp:label id="Label1" runat="server" Font-Bold="True" Visible="False">Rencana Pengiriman Kendaraan</asp:label></TD>
								<TD style="WIDTH: 2px; HEIGHT: 24px">&nbsp;</TD>
								<TD style="HEIGHT: 18px"><asp:label id="lblPengiriman" runat="server" Visible="False"></asp:label></TD>
								<TD class="titleField" style="WIDTH: 180px; HEIGHT: 24px"><asp:label id="Label3" runat="server" Font-Bold="True" Visible="False">Rencana Pengajuan Faktur</asp:label></TD>
								<TD style="WIDTH: 2px; HEIGHT: 24px">&nbsp;</TD>
								<TD style="WIDTH: 155px; HEIGHT: 24px"><asp:label id="lblPengajuan" runat="server" Visible="False"></asp:label>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<table style="WIDTH:100%">
							<tr>
								<td>
									<div id="div1" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 160px">
										<asp:datagrid id="dtgPesananKendaraan" runat="server" Width="100%" AutoGenerateColumns="False"
											OnItemCommand="dtgPesananKendaraan_ItemCommand" OnItemDataBound="dtgPesananKendaraan_ItemDataBound"
											BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="1" ShowFooter="True"
											BackColor="#E0E0E0">
											<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
											<Columns>
												<asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
													<HeaderStyle Width="2%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="30px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblNo" runat="server" NAME="lblNo" text= '<%# container.itemindex+1 %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Model / Tipe / Warna">
													<HeaderStyle Width="150px" HorizontalAlign="Center" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblModel" runat="server" NAME="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "VechileColor.VechileType.Category.CategoryCode" )  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Kategori">
													<HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="40px"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblKategori" runat="server" NAME="lblKategori" Text='<%# DataBinder.Eval(Container.DataItem, "VechileColor.VechileType.Category.CategoryCode" )  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Kode Tipe">
													<HeaderStyle Width="40px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblViewKodeModel runat="server" NAME="lblViewKodeModel" Text='<%# DataBinder.Eval(Container.DataItem, "VehicleTypeCode" )  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Kode Warna">
													<HeaderStyle Width="40px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblViewKodeWarna runat="server" NAME="lblViewKodeWarna" Text='<%# DataBinder.Eval(Container.DataItem, "VehicleColorCode" ) %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Model Body">
													<HeaderStyle Width="40px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblViewModelBody" runat="server" NAME="lblViewModelBody" Text='<%# DataBinder.Eval(Container.DataItem, "ProfileDetail.Code" ) %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Tambahan">
													<HeaderStyle Width="100px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblTambahan" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Remarks" HeaderText="Remarks">
													<HeaderStyle Width="100px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblRemarks" runat="server" NAME="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Unit">
													<HeaderStyle Width="40px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="40px"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblViewUnitPermintaanDealer runat="server" NAME="lblViewUnitPermintaanDealer" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity", "{0:#,###}" ) %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Harga Per Unit (OTR / Deal Price)(Rp)" visible="false">
													<HeaderStyle Width="100px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="100px"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblViewHarga" runat="server" NAME="lblViewHarga" Text='<%# DataBinder.Eval(Container.DataItem, "Amount", "{0:#,###,###,###}" ) %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn ReadOnly="True" HeaderText="Total Harga (Rp)" DataFormatString="{0:#,###,###,###}" visible="false">
													<HeaderStyle Width="120px" CssClass="titleTableMrk" ></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="120px"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="RejectedReason" HeaderText="Alasan Batal">
													<HeaderStyle Width="120px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="120px"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblRej" runat="server" NAME="lblViewHarga">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="CampaignName" HeaderText="Campaign">
													<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="80px"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblCampaignName" runat="server" NAME="lblCampaignName">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="false">
													<HeaderStyle Width="40px" CssClass="titleTableMrk" ></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnProfile" runat="server" CommandName="UpdateProfile">
															<img src="../images/dok.gif" border="0" alt="Update Profile"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                
                                                <asp:TemplateColumn HeaderText="Konsumen Faktur" Visible="false">
													<HeaderStyle Width="40px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
                                                    	<ItemTemplate>
														<asp:LinkButton id="lbtnAddFaktur" runat="server" CommandName="AddFaktur">
															<img src="../images/add.gif" border="0" alt="Tambah" id="imgConsumentFaktur" runat="server"/></asp:LinkButton>
													</ItemTemplate>
													 
												</asp:TemplateColumn>

											</Columns>
											<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
										</asp:datagrid>
									</div>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD height="40">
						<asp:Button id="BtnTutup" runat="server" Text="Kembali" CausesValidation="False"></asp:Button></TD>
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
