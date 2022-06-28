<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpPQRDetails.aspx.vb" Inherits="PopUpPQRDetails" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPQRHeader</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<base target =_self>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			function ShowPopUp(selectedCode)
			{
			}
		</script>
	</HEAD>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PQR - Detil Dokumen</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</TABLE>
			<asp:panel id="pnlResult" Runat="server">
				<TABLE cellSpacing="1" cellPadding="4" width="764" border="0">
					<TR vAlign="top">
						<TD style="WIDTH: 361px" width="0">
							<TABLE cellSpacing="1" cellPadding="2" width="360" border="0">
								<TR>
									<TD class="titleField">
										<asp:label id="lblPQRNo" Runat="server">PQR No</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:label id="lblPQRNoVal" Runat="server">Value Of PQR Number</asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:label id="lblRefPQRNo" Runat="server">Ref PQR No</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:label id="lblRefPQRNoVal" Runat="server" Font-Size="8" Width="196px"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:label id="lblTglPembuatan" Runat="server">Tgl Pembuatan</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:label id="lblTglPembuatanVal" Runat="server">Value Of Tgl Pembuatan</asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:label id="lblNoChasis" Runat="server">No Chasis</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:label id="lblNoChasisVal" Runat="server" Font-Size="8" Width="197px"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:label id="lblNoMesin" Runat="server">No Mesin</asp:label></TD>
									<TD style="HEIGHT: 16px">:</TD>
									<TD style="HEIGHT: 16px">
										<asp:label id="lblNoMesinVal" Runat="server" Width="213">Value Of No Mesin</asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:label id="lblTypeColor" Runat="server">Type / Color</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:label id="lblTypeColorVal" Runat="server" Width="213">Value Of Type / Color</asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:label id="lblNama" Runat="server">Nama</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:label id="lblNamaVal" Runat="server" Width="213">Value Of Nama</asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:label id="lblThnProduksi" Runat="server">Tahun Produksi/Perakitan</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:label id="lblThnProduksiVal" Runat="server" Width="213">Value Of Tahun Produksi/Perakitan</asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:label id="lblTglDelivery" Runat="server">Tanggal Delivery</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:label id="lblTglDeliveryVal" Runat="server" Width="213">Tanggal Delivery</asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:label id="lblTglFaktur" Runat="server">Tanggal Buka Faktur</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:label id="lblTglFakturVal" Runat="server" Width="213">Tanggal Buka Faktur</asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:label id="lblTglKerusakan" Runat="server">Tanggal Kerusakan</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:label id="lblTglKerusakanVal" Runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:label id="lblOdometer" Runat="server">Odometer</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:label id="lblOdometerVal" Runat="server"></asp:label>&nbsp;<SPAN style="FONT-SIZE: 8pt">Km</SPAN>
									</TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:label id="lblKecepatan" Runat="server">Kecepatan</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:label id="lblKecepatanVal" Runat="server"></asp:label>&nbsp;<SPAN style="FONT-SIZE: 8pt">Km 
											/ Jam</SPAN>
									</TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:label id="lblSubject" Runat="server">Subject</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:label id="lblSubjectVal" Runat="server"></asp:label></TD>
								</TR>
								<TR vAlign="top">
									<TD class="titleField">
										<asp:label id="lblGejala" Runat="server">Gejala</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:textbox id="txtGejala" Runat="server" Width="213px" Height="60px" TextMode="MultiLine" ReadOnly="True"></asp:textbox></TD>
								</TR>
								<TR vAlign="top">
									<TD class="titleField">
										<asp:label id="lblPenyebab" Runat="server">Penyebab</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:textbox id="txtPenyebab" Runat="server" Width="213px" Height="60px" TextMode="MultiLine"
											ReadOnly="True"></asp:textbox></TD>
								</TR>
								<TR vAlign="top">
									<TD class="titleField">
										<asp:label id="lblHasil" Runat="server">Hasil</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:textbox id="txtHasil" Runat="server" Width="213px" Height="60px" TextMode="MultiLine" ReadOnly="True"></asp:textbox></TD>
								</TR>
								<TR vAlign="top">
									<TD class="titleField">
										<asp:label id="lblCatatan" Runat="server">Catatan</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:textbox id="txtCatatan" Runat="server" Width="213px" Height="60px" TextMode="MultiLine"
											ReadOnly="True"></asp:textbox></TD>
								</TR>
							</TABLE>
						</TD>
						<TD>
							<TABLE id="titlePanel1" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD class="titlePanel"><B>
											<asp:label id="lblDealer" Runat="server">Dealer : </asp:label>
											<asp:label id="lblDealerVal" Runat="server" Font-Size="8">Value Of Dealer</asp:label></B></TD>
								</TR>
								<TR>
									<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
								</TR>
							</TABLE>
							<BR>
							<asp:panel id="Panel1" runat="server" BorderStyle="Solid" BorderWidth="0" Height="120px"></asp:panel><BR>
							<FONT class="titleField">Kerusakan</FONT>
							<BR>
							<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 100px">
								<asp:datagrid id="dgKerusakan" runat="server" Width="381" BorderWidth="1px" BackColor="White"
									ShowFooter="False" AutoGenerateColumns="False" CellPadding="3" BorderColor="#CDCDCD">
									<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
									<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
									<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
									<FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
									<Columns>
										<asp:TemplateColumn HeaderText="No">
											<HeaderStyle Width="25" CssClass="titleTableService"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Kode ">
											<HeaderStyle Width="108" CssClass="titleTableService"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:label id="lblKodeDamage" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DeskripsiKodePosisi.KodePosition") %>'>
												</asp:label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Deskripsi">
											<HeaderStyle Width="300" CssClass="titleTableService"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblDescDamage" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DeskripsiKodePosisi.Description") %>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
								</asp:datagrid></DIV>
							<BR>
							<FONT class="titleField">Parts</FONT>
							<BR>
							<DIV id="div2" style="OVERFLOW: auto; HEIGHT: 100px">
								<asp:datagrid id="dgParts" runat="server" Width="381px" BorderWidth="1px" BackColor="White" ShowFooter="False"
									AutoGenerateColumns="False" CellPadding="3" BorderColor="#CDCDCD">
									<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
									<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
									<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
									<FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
									<Columns>
										<asp:TemplateColumn HeaderText="No">
											<HeaderStyle Width="25" CssClass="titleTableService"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Kode ">
											<HeaderStyle Width="108" CssClass="titleTableService"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:label id="lblKodeParts" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>'>
												</asp:label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Deskripsi">
											<HeaderStyle Width="300" CssClass="titleTableService"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblDescParts" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName") %>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
								</asp:datagrid></DIV>
							<BR>
							<FONT class="titleField">Attachment</FONT>
							<BR>
							<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 100px">
								<asp:datagrid id="dgFileAttachmentTop" runat="server" Width="381px" BorderWidth="1px" BackColor="White"
									ShowFooter="False" AutoGenerateColumns="False" CellPadding="3" BorderColor="#CDCDCD">
									<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
									<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
									<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
									<FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
									<Columns>
										<asp:TemplateColumn HeaderText="No">
											<HeaderStyle Width="25" CssClass="titleTableService"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Literal id="ltrFileAttachmentTopNo" runat="server"></asp:Literal>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="File">
											<HeaderStyle Width="400" CssClass="titleTableService"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:LinkButton id="lnkbtnFileAttachmentTop" runat="server" CommandName="Download" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Attachment") %>' >
													<%# DataBinder.Eval(Container, "DataItem.FileName") %>
												</asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
								</asp:datagrid></DIV>
							<BR>
							<TABLE style="MARGIN: 5% 0px" align="center" border="0">
								<TR>
									<TD class="titleField">
										<asp:label id="lblStatus" Runat="server" Font-Size="8">Status</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:label id="lblStatusVal" Runat="server" Font-Size="8"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:label id="lblAppliedBy" Runat="server" Font-Size="8">Diajukan Oleh</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:label id="lblAppliedByVal" Runat="server" Font-Size="8"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:label id="lblTglJam" Runat="server" Font-Size="8">Tanggal/Jam</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:label id="lblTglJamVal" Runat="server" Font-Size="8"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:label id="lblProcessBy" Runat="server" Font-Size="8">Diproses Oleh</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:label id="lblProcessByVal" Runat="server" Font-Size="8"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">
										<asp:label id="lblTglJamProcess" Runat="server" Font-Size="8">Waktu Proses</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:label id="lblTglJamProcessVal" Runat="server" Font-Size="8"></asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD style="WIDTH: 361px"><FONT class="titleField">Tambahan Info :</FONT>
							<asp:literal id="ltrStatusAdditionalInfo" runat="server"></asp:literal>
							<asp:label id="lblLastPostedInfo" runat="server">
								<img src="../images/icon_mail.gif" border="0" runat="server" ID="img">
							</asp:label>
							<asp:linkbutton id="lnkbtnAdditionalInfoPopUp" runat="server" CausesValidation="False">
								<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
							</asp:linkbutton><BR>
							<FONT class="titleField">Solusi :</FONT>
							<BR>
							<asp:textbox id="txtSolution" runat="server" Width="360px" Height="130px" TextMode="MultiLine"
								ReadOnly="True"></asp:textbox><BR>
						</TD>
						<TD>
							<asp:label class="titleField" id="lblBobot" Runat="server">Bobot :  </asp:label>
							<asp:label id="lblBobotVal" Runat="server"></asp:label><BR>
							<FONT class="titleField">Attachment :</FONT>
							<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 100px">
								<asp:datagrid id="dgFileAttachmentBottom" runat="server" Width="381px" BorderWidth="1px" BackColor="White"
									ShowFooter="False" AutoGenerateColumns="False" CellPadding="3" BorderColor="#CDCDCD">
									<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
									<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
									<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
									<FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
									<Columns>
										<asp:TemplateColumn HeaderText="No">
											<HeaderStyle Width="25" CssClass="titleTableService"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Literal id="ltrFileAttachmentBottomNo" runat="server"></asp:Literal>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="File">
											<HeaderStyle Width="400" CssClass="titleTableService"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:LinkButton id="lnkbtnFileAttachmentBottom" CommandName="Download" runat="server" CommandArgument ='<%# DataBinder.Eval(Container, "DataItem.Attachment") %>' >
													<%# DataBinder.Eval(Container, "DataItem.FileName") %>
												</asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
								</asp:datagrid></DIV>
						</TD>
					<TR>
						<TD align="center" colSpan="2"><INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
								name="btnCancel">
						</TD>
					</TR>
				</TABLE>
			</asp:panel></form>
		</SPAN>
<!--		<script language="javascript">
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
		</script>-->
	</BODY>
</HTML>
