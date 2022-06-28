<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmClaimProcess.aspx.vb" Inherits="FrmClaimProcess" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Claim Process</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<tr>
					<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 8px" height="8"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%"></TD>
								<TD width="1%"></TD>
								<TD width="25%"></TD>
								<TD class="titleField" width="24%"></TD>
								<TD width="1%"></TD>
								<TD width="25%">
									<P>Keterangan Permasalahan/
										<asp:label id="lblIncharge" runat="server"></asp:label></P>
									<P>&nbsp;</P>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%"><asp:label id="lblNo" runat="server" Font-Bold="True">Nomor Claim</asp:label></TD>
								<TD width="1%"><asp:label id="lblColon1" runat="server">:</asp:label></TD>
								<TD width="25%"><asp:label id="lblClaimNo" runat="server" Width="224px"></asp:label></TD>
								<TD class="titleField" width="24%"><asp:label id="Label7" runat="server" Font-Bold="True">No Faktur</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:label id="lblNoFaktur" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%"><asp:label id="lblClaimDate2" runat="server" Font-Bold="True">Tgl Pengajuan Claim</asp:label></TD>
								<TD width="1%"><asp:label id="lblColon2" runat="server">:</asp:label></TD>
								<TD width="25%"><asp:label id="lblClaimDate" runat="server" Width="224px"></asp:label></TD>
								<TD class="titleField" width="20%"><asp:label id="Label8" runat="server" Font-Bold="True">Tgl Faktur</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="29%"><asp:label id="lblFakturDate" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 15px"><asp:label id="lblD" runat="server" Font-Bold="True">Dealer</asp:label></TD>
								<TD style="HEIGHT: 15px"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 15px"><asp:label id="lblDealer" runat="server" Width="224px"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 15px"><asp:label id="Label9" runat="server" Font-Bold="True">Nomor DO</asp:label></TD>
								<TD style="HEIGHT: 15px">:</TD>
								<TD style="HEIGHT: 15px"><asp:label id="lblNoDO" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblKota" runat="server" Font-Bold="True"> Kota</asp:label></TD>
								<TD><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblCity" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblCr" runat="server" Font-Bold="True">Tgl Delivery</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblDeliveryDate" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label4" runat="server" Font-Bold="True">Kategori Claim</asp:label></TD>
								<TD><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblCategoryClaim" runat="server"></asp:label></TD>
								<TD><asp:label id="Label10" runat="server" Font-Bold="True">Tgl Kedatangan</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblDateCome" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD></TD>
								<TD><asp:label id="Label11" runat="server" Font-Bold="True">Status</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblStatus" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6"><asp:datagrid id="dgClaimDetail" runat="server" Width="100%" CellPadding="3" BorderWidth="1px"
										AutoGenerateColumns="False">
										<AlternatingItemStyle BackColor="#EFEFEF"></AlternatingItemStyle>
										<ItemStyle BackColor="White"></ItemStyle>
										<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="Nomor Barang">
												<HeaderStyle CssClass="titleTablePrint"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:Literal ID="ltrNoBarang" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPOStatusDetail.SparePartMaster.PartNumber") %>'>
													</asp:Literal>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Nama Barang">
												<HeaderStyle CssClass="titleTablePrint"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:Literal ID="ltrNamaBarang" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPOStatusDetail.SparePartMaster.PartName") %>'>
													</asp:Literal>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Qty Faktur">
												<HeaderStyle CssClass="titleTablePrint"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:Literal ID="ltrQtyClaim" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPOStatusDetail.BillingQuantity") %>'>
													</asp:Literal>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Qty Claim">
												<HeaderStyle CssClass="titleTablePrint"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:Literal ID="Literal1" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Qty") %>'>
													</asp:Literal>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Harga">
												<HeaderStyle CssClass="titleTablePrint"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:Literal ID="ltrHarga" Runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.SparePartPOStatusDetail.ClaimPriceUnit"),"#,###") %>'>
													</asp:Literal>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
									</asp:datagrid>
									Keterangan :
									<asp:datagrid id="dtgClaimKeterangan" runat="server" Width="100%" CellPadding="3" BorderWidth="0px"
										AutoGenerateColumns="False" ShowHeader="False">
										<AlternatingItemStyle BackColor="#EFEFEF"></AlternatingItemStyle>
										<ItemStyle BackColor="White"></ItemStyle>
										<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="Nomor Barang">
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<asp:Literal ID="ltrNoBarangKeterangan" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPOStatusDetail.SparePartMaster.PartNumber") & " : " & DataBinder.Eval(Container, "DataItem.Keterangan")%>'>
													</asp:Literal>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
									</asp:datagrid>
									<br>
									<asp:Literal Runat="server" ID="LtrPenjelasan"></asp:Literal>
									<br>
								</TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<TABLE id="Table3" borderColor="#cdcdcd" cellSpacing="0" cellPadding="2" width="100%" border="1">
										<asp:panel id="Panel1" Visible="False" Runat="server">
											<TBODY>
												<TR>
													<TD>Penjelasan Dealer<BR>
														<asp:Label id="lblPenjelasanDealer" runat="server"></asp:Label>
														<P>&nbsp;</P>
														<P>&nbsp;</P>
													</TD>
												</TR>
										</asp:panel>
										<TR>
											<TD>Penjelasan MKS<br>
												<asp:label id="lblPenjelasanKTB" runat="server"></asp:label>
												<p>&nbsp;</p>
												<p>&nbsp;</p>
												<p>&nbsp;</p>
												<p>&nbsp;</p>
												<p>&nbsp;</p>
												<p>&nbsp;</p>
											</TD>
										</TR>
										<TR>
											<TD>
												<P>Rekomendasi</P>
												<P>&nbsp;</P>
												<p>&nbsp;</p>
												<p>&nbsp;</p>
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<TABLE id="Table4" borderColor="#cdcdcd" cellSpacing="0" cellPadding="2" width="100%" border="1">
										<TR>
											<TD align="center" width="33%"><b>Claim Diterima</b></TD>
											<TD align="center" width="33%"><b>Tgl Diselesaikan</b></TD>
											<TD align="center" width="33%"><b>Manajer</b></TD>
										</TR>
										<TR>
											<TD>
												<P>Incharge (Berdasarkan Tabel)</P>
												<P>Tgl</P>
												<P>Sign</P>
											</TD>
											<TD>&nbsp;</TD>
											<TD>&nbsp;</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD vAlign="top" align="center" colSpan="6">&nbsp;
									<asp:button class="hideButtonOnPrint" id="Button1" runat="server" Width="74px" Text="Cetak"></asp:button><INPUT class="hideButtonOnPrint" id="btnClose" style="WIDTH: 74px; HEIGHT: 21px" onclick="window.close()"
										type="button" value="Tutup" name="btnClose">
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				</TBODY></TABLE>
		</form>
	</body>
</HTML>
