<%@ Page Language="vb" AutoEventWireup="false" Codebehind="UploadProductionPlanning.aspx.vb" Inherits="UploadProductionPlanning" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>UploadProductionPlanning</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td colspan="6">
						<table width="100%" border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td class="titlePage" colSpan="6">PESANAN KENDARAAN - Upload Rencana Produksi</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" colSpan="6" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td colSpan="6" height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD class="titleField" width="20%" height="22"><asp:label id="lblPilihLokasiFile" runat="server">Pilih Lokasi File</asp:label></TD>
					<TD width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
					<TD colspan="4" width="79%"><INPUT id="DataFile" style="WIDTH: 340px" type="file" name="File1" runat="server" onkeypress="return false;">&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="btnUpload" runat="server" Text="Upload"></asp:button>&nbsp;<asp:button id="btnSimpan" runat="server" Text="Simpan" Enabled="False"></asp:button></TD>
				</TR>
                <tr>
                    <td></td>
                    <td></td>
                    <td><asp:LinkButton ID="lbtnDownloadExcel" runat="server" Text="Download Template Excel"  /></td>
                </tr>
				<TR>
					<TD width="20%" class="titleField"><asp:label id="lblPeriod" runat="server">Periode</asp:label></TD>
					<td width="1%"><asp:label id="Label4" runat="server">:</asp:label></td>
					<TD width="29%"><asp:dropdownlist id="ddlPeriode" runat="server" Width="104px"></asp:dropdownlist>&nbsp;</TD>
				</TR>
				<TR valign="top">
					<TD class="titleField" style="HEIGHT: 22px">
						<asp:label id="Label1" runat="server">Kategori</asp:label></TD>
					<TD>
						<asp:label id="Label5" runat="server">:</asp:label></TD>
					<TD valign="bottom">
						<table>
                            <tr valign="top">
                                <td><asp:dropdownlist id="ddlKategori" runat="server" Width="96px" AutoPostBack="True"></asp:dropdownlist></td>
                                <td><asp:dropdownlist style="Z-INDEX: 0" id="ddlSubCategory" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
                            </tr>
                        </table>
					</TD>
				</TR>
				<TR valign="top" >
					<TD class="titleField">Tipe</TD>
					<TD>:</TD>
					<TD>
                        <asp:dropdownlist id="ddlTipe" runat="server" Width="104px"></asp:dropdownlist>
					</TD>
				</TR>
				<TR valign="top" style="display:none">
					<TD class="titleField" style="HEIGHT: 59px">Status</TD>
					<TD style="HEIGHT: 59px">:</TD>
					<TD style="HEIGHT: 59px">
                        <table>
                            <tr valign="top">
                                <td>
                                    <asp:listbox id="lboxStatus" runat="server" Width="136px" SelectionMode="Multiple" Rows="4">
							            <asp:ListItem Value="0">Baru</asp:ListItem>
							            <asp:ListItem Value="2">Validasi</asp:ListItem>
							            <asp:ListItem Value="3">Konfirmasi</asp:ListItem>
							            <asp:ListItem Value="4">Rilis</asp:ListItem>
							            <asp:ListItem Value="9">Selesai</asp:ListItem>
							            <asp:ListItem Value="6">Setuju</asp:ListItem>
						            </asp:listbox>
                                </td>                                
                                <td width="32%">
                                    <asp:ListBox id="lBoxType" runat="server" Width="88px" Rows="4" SelectionMode="Multiple"></asp:ListBox></td>
                            </tr>
                        </table>
					</TD>
				</TR>
				<TR>
					<td></td>
					<td></td>
					<td><asp:button id="btnCari" runat="server" Text="Cari" width="60px"></asp:button>&nbsp;
                        <asp:button id="btnDownLoad" runat="server" Text="DownLoad" Enabled="False" Width="64px"></asp:button>
					</td>
				</TR>
                <TR>
					<TD colSpan="6">
                        <hr />
						<div id="div1" style="HEIGHT: 300px; OVERFLOW: auto">
                            <asp:datagrid id="dtgProductionPlan" runat="server" Width="100%" CellSpacing="1" OnItemDataBound="dtgProductionPlan_ItemDataBound"
								GridLines="Horizontal" CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" 
								AutoGenerateColumns="False" AllowSorting="True" ShowFooter="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle BackColor="White" VerticalAlign="Top"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn Visible="False" HeaderText="id">
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'>
											</asp:Label>
										</ItemTemplate>
										<%--<EditItemTemplate>
											<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'>
											</asp:TextBox>
										</EditItemTemplate>--%>
									</asp:TemplateColumn>

									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
											<asp:Label id="lbLineNo" runat="server"
                                                Text='<%# Container.ItemIndex+1 %>'></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="VechileColor.MaterialNumber" HeaderText="Kode Kendaraan">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblMaterialNumber" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "VechileColor.MaterialNumber")%>'></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="VechileColor.MaterialDescription" HeaderText="Model/Tipe/Warna">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblMaterialDescription" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "VechileColor.MaterialDescription")%>'></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

                                    <asp:TemplateColumn Visible="false" SortExpression="PeriodMonth" HeaderText="PeriodMonth">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPeriodMonth" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "PeriodMonth")%>'></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn Visible="false" SortExpression="PeriodYear" HeaderText="PeriodYear">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPeriodYear" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "PeriodYear")%>'></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="ProductionYear" HeaderText="Tahun Perakitan">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblProductionYear" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "ProductionYear")%>'></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Stok Awal Bulan (Unit)">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPlanQty" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "PlanQty")%>'></asp:Label>
										</ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
									</asp:TemplateColumn>

                                     <asp:TemplateColumn HeaderText="Alokasi (Unit)">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblAllocationQty" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "AllocationQty")%>'></asp:Label>
										</ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
									</asp:TemplateColumn>

                                     <asp:TemplateColumn HeaderText="Reserve (Unit)">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblReserveQty" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "ReserveQty")%>'></asp:Label>
										</ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                         <EditItemTemplate>
											<asp:TextBox 
                                                id="txtEditReserve" 
                                                style="text-align:right;" 
                                                runat="server" 
                                                Width="40px" 
                                                BackColor="White" 
                                                Text='<%# DataBinder.Eval(Container.DataItem, "ReserveQty")%>' 
                                                onkeypress="return numericOnlyUniv(event)" onkeyup="pic(this,this.value,'9999999999','N')">
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Sisa Stok Setelah Alokasi / Reserve (Unit)">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblSisaStok" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "StokSetelahAlokasi")%>'></asp:Label>
										</ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
									</asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Pesan Error">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPesanError" ForeColor="Red" runat="server"></asp:Label>
										</ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
									</asp:TemplateColumn> 

                                    <asp:TemplateColumn HeaderText="Status Baru & Validasi (Unit)">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCountStatusBaruValidasi" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "TotalBaruAndValidasi")%>'></asp:Label>
										</ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
									</asp:TemplateColumn> 
                                    <asp:TemplateColumn HeaderText="Status Konfirmasi & Tunggu Diskon (Unit)">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCountStatusKonfirmasiTungguDiskon" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "TotalKonfirmasiAndTungguDiskon")%>'></asp:Label>
										</ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
									</asp:TemplateColumn> 
                                    <asp:TemplateColumn HeaderText="Status Rilis (Unit)">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCountStatusRilis" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "TotalReleaseAndAgree")%>'></asp:Label>
										</ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
									</asp:TemplateColumn> 
                                    <asp:TemplateColumn HeaderText="Status DiTolak (Unit)">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCountStatusTolak" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "TotalTolak")%>'></asp:Label>
										</ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
									</asp:TemplateColumn> 
                                    <asp:TemplateColumn HeaderText="Status Selesai/OC (Unit)">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCountStatusSelesai" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "TotalSelesai")%>'></asp:Label>
										</ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
									</asp:TemplateColumn> 
                                    <asp:TemplateColumn HeaderText="DO">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCountDO" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "TotalDO")%>'></asp:Label>
										</ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
									</asp:TemplateColumn> 
                                    <asp:TemplateColumn HeaderText="Total Pesanan Kendaraan(Unit)">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblTotal" runat="server" 
                                                Text='<%# DataBinder.Eval(Container.DataItem, "Total")%>'></asp:Label>
										</ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
									</asp:TemplateColumn> 
                                    <asp:TemplateColumn HeaderText="Sisa Stok D-Net(Rilis/Selesai)">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblSisaStokDNet" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "SisaStokDNet")%>'></asp:Label>
										</ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
									</asp:TemplateColumn> 
                                    <asp:TemplateColumn>
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="true" CommandName="Edit"><img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
										</ItemTemplate>
                                        <EditItemTemplate>
											<asp:LinkButton id="lbtnSave" tabIndex="40" Runat="server" CausesValidation="True" CommandName="Update"
															text="Simpan">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
														<asp:LinkButton id="lbtnCancel" tabIndex="50" Runat="server" CausesValidation="True" CommandName="Cancel"
															text="Batal">
															<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
										</EditItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#404040" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
						</div>
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
