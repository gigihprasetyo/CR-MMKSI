<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmWSCDetail.aspx.vb" Inherits="FrmWSCDetail" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ListContract</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
				
		function DummyFunction()
		{
			
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">WSC - Rincian Status WSC</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%" border="0">
							<TR valign="top">
								<TD class="titleField" width="15%"><asp:label id="lbl1" runat="server">Dealer Pelaksana</asp:label></TD>
								<TD width="1%"><asp:label id="lblColon1" runat="server">:</asp:label></TD>
								<TD width="34%"><asp:label id="lblServiceDealer" runat="server"></asp:label></TD>
								<TD class="titleField" width="15%"><asp:label id="lbl10" runat="server">Dealer Penjual</asp:label></TD>
								<TD width="1%"><asp:label id="lblColon4" runat="server">:</asp:label></TD>
								<TD width="34%"><asp:label id="lblSoldDealer" runat="server"></asp:label></TD>
							</TR>
							<TR valign="top">
								<TD class="titleField"><asp:label id="lbl2" runat="server">Jenis WSC</asp:label></TD>
								<TD><asp:label id="lblColon2" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblClaimType" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lbl11" runat="server">Kode Model</asp:label></TD>
								<TD><asp:label id="lblColon5" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblVehicleType" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lbl3" runat="server">Nomor WSC</asp:label></TD>
								<TD><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblClaimNumber" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lbl12" runat="server">Nomor Rangka</asp:label></TD>
								<TD><asp:label id="lblColon6" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblChassisNumber" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lbl4" runat="server">No WSC Referensi</asp:label></TD>
								<TD><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblRefClaimNumber" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lbl13" runat="server">Nomor Mesin</asp:label></TD>
								<TD><asp:label id="Label19" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblEngineNumber" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lbl5" runat="server">Tanggal Service</asp:label></TD>
								<TD><asp:label id="Label14" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblServiceDate" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lbl14" runat="server">Nomor Seri</asp:label></TD>
								<TD><asp:label id="Label20" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblSerialNumber" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lbl6" runat="server">Tanggal Proses</asp:label></TD>
								<TD><asp:label id="Label15" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblDecideDate" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lbl15" runat="server">Tanggal Pengiriman</asp:label></TD>
								<TD><asp:label id="Label21" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblDeliveryDate" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lbl7" runat="server">Jarak Tempuh</asp:label></TD>
								<TD><asp:label id="Label16" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblMileage" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lbl16" runat="server">Notifikasi</asp:label></TD>
								<TD><asp:label id="Label22" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblNotification" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lbl8" runat="server">Nomor PQR</asp:label></TD>
								<TD><asp:label id="Label17" runat="server">:</asp:label></TD>
								<TD>
									<asp:LinkButton id="lblPQR" runat="server"></asp:LinkButton></TD>
								<TD class="titleField"><asp:label id="lbl17" runat="server">Status</asp:label></TD>
								<TD><asp:label id="Label23" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblStatus" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 7px"><asp:label id="lbl9" runat="server">Keterangan</asp:label></TD>
								<TD style="HEIGHT: 7px"><asp:label id="Label18" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 7px"><asp:label id="lblDescription" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 7px"><asp:label id="lbl18" runat="server">Alasan</asp:label></TD>
								<TD style="HEIGHT: 7px"><asp:label id="lblColo18" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 7px"><asp:label id="lblReason" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD colSpan="6">
									<P>
										<TABLE id="Table4" cellSpacing="1" cellPadding="3" width="100%" border="0">
											<TR>
												<TD><b><asp:label id="lbl19" runat="server" Width="48px">Kode A</asp:label>:</b>
													<asp:label id="lblKodeA" runat="server">Kode A</asp:label></TD>
												<TD><b><asp:label id="lbl20" runat="server" Width="46px">Kode B</asp:label>
														<asp:label id="Label1" runat="server">:&nbsp;</asp:label></b>
													<asp:label id="lblKodeB" runat="server" Width="39px">Kode B</asp:label></TD>
												<TD><b><asp:label id="lbl21" runat="server">Kode C</asp:label><asp:label id="Label3" runat="server">&nbsp;&nbsp;:&nbsp;</asp:label></b><asp:label id="lblKodeC" runat="server">Kode C</asp:label></TD>
												<TD class="titleField"><asp:checkbox id="chkDamagePart" runat="server" Height="8px" Text="Damage Part"></asp:checkbox></TD>
												<TD class="titleField"><asp:checkbox id="chkKwitansi" runat="server" Height="8px" Text="Kuitansi" Enabled="False"></asp:checkbox></TD>
												<TD class="titleField"><asp:checkbox id="chkPhoto" runat="server" Height="8px" Text="Foto" Enabled="False"></asp:checkbox></TD>
											</TR>
										</TABLE>
									</P>
								</TD>
							</TR>
						</TABLE>
						<TABLE id="Table3" cellSpacing="1" cellPadding="4" width="100%" border="0">
							<TR vAlign="top">
								<TD width="50%">
									<TABLE id="Table5" cellSpacing="2" cellPadding="2" width="100%" border="0">
										<tr>
											<td colspan="4"><STRONG>Daftar Labor / Part</STRONG>
											</td>
										</tr>
									</TABLE>
									<div id="div1" style="OVERFLOW: auto; WIDTH: 90%; HEIGHT: 200px">
										<asp:datagrid id="dgWSCDetail" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="#CDCDCD"
											BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3">
											<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Main" ItemStyle-HorizontalAlign="Center">
													<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Tipe">
													<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblType runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Type") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Kode">
													<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Code") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Kerja">
													<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblKerja" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="Quantity" HeaderText="Jumlah">
													<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Amount">
													<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblLaborAmount" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.PartPrice"),"#,##0") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Jumlah Diterima">
													<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblQuantityReceived" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Diterima Oleh">
													<HeaderStyle CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblReceivedBy" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Tanggal Diterima">
													<HeaderStyle CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblReceivedDate" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></div>
								</TD>
								<TD width="50%">
									<TABLE id="Table5" cellSpacing="1" cellPadding="2" width="100%" border="0">
										<tr>
											<td colspan="4"><STRONG>Daftar Bukti WSC</STRONG>
											</td>
										</tr>
										<tr valign="top">
											<td colspan="2" valign="top">
												<div id="div1" style="OVERFLOW: auto; HEIGHT: 200px">
													<asp:DataGrid id="dbBukti" runat="server" AutoGenerateColumns="False" Width="130px" CellPadding="3"
														BorderWidth="0px" CellSpacing="1" BorderColor="#999999" BackColor="#cdcdcd" BorderStyle="None"
														GridLines="Vertical">
														<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
														<ItemStyle ForeColor="Black" BackColor="#ffffff"></ItemStyle>
														<AlternatingItemStyle BackColor="#f6f6f6"></AlternatingItemStyle>
														<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
														<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
														<Columns>
															<asp:TemplateColumn HeaderText="Bukti">
																<HeaderStyle ForeColor="White" CssClass="titleTableService"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:Label id="lblBukti" runat="server"></asp:Label>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox id="TextBox4" runat="server"></asp:TextBox>
																</EditItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Download">
																<HeaderStyle ForeColor="White" CssClass="titleTableService"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:LinkButton id="lblLihat" runat="server" CommandArgument="Lihat">Lihat</asp:LinkButton>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox id="TextBox5" runat="server"></asp:TextBox>
																</EditItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn Visible="False" HeaderText="File">
																<ItemTemplate>
																	<asp:Label id=lblPath runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PathFile") %>'>
																	</asp:Label>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox id=TextBox6 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PathFile") %>'>
																	</asp:TextBox>
																</EditItemTemplate>
															</asp:TemplateColumn>
														</Columns>
														<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Mode="NumericPages"></PagerStyle>
													</asp:DataGrid>
												</div>
											</td>
											<td colspan="2" valign="top">
												<table width="100%" border="0" cellpadding="2">
													<TR>
														<TD class="titleField" width="38%"><asp:label id="lbl22" runat="server">Dikirim Oleh</asp:label></TD>
														<TD width="2%"><asp:label id="Label4" runat="server">:</asp:label></TD>
														<TD width="60%"><asp:label id="lblCreateBy" runat="server"></asp:label></TD>
													</TR>
													<TR>
														<TD class="titleField" width="38%"><asp:label id="lbl23" runat="server">Tanggal Kirim</asp:label></TD>
														<TD><asp:label id="Label5" runat="server">:</asp:label></TD>
														<TD><asp:label id="lblCreateDate" runat="server"></asp:label></TD>
													</TR>
													<TR>
														<TD class="titleField" nowrap><asp:label id="lbl24" runat="server" Visible="False">Barang Diterima Oleh</asp:label></TD>
														<TD><asp:label id="Label6" runat="server" Visible="False">:</asp:label></TD>
														<TD><asp:label id="lblPartReceiveBy" runat="server" Visible="False"></asp:label></TD>
													</TR>
													<TR>
														<TD class="titleField" nowrap><asp:label id="lbl25" runat="server" Visible="False">Tgl Terima Barang</asp:label></TD>
														<TD><asp:label id="Label7" runat="server" Visible="False">:</asp:label></TD>
														<TD><asp:label id="lblPartReceiveDate" runat="server" Visible="False"></asp:label></TD>
													</TR>
												</table>
											</td>
										</tr>
										<tr>
											<td></td>
											<td></td>
											<td></td>
											<td></td>
										</tr>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 945px; HEIGHT: 13px" colSpan="2"><asp:button id="btnSave" runat="server" Text="Simpan" Width="60px"></asp:button>&nbsp;
						<asp:button id="btnClose" runat="server" Text="Kembali" Width="60px"></asp:button>&nbsp;
						<INPUT id="btnSendEmail" style="WIDTH: 120px; HEIGHT: 21px" type="button" value="Permintaan Bukti"
							runat="server" NAME="btnSendEmail"></TD>
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
