<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmCustomerDetail.aspx.vb" Inherits="FrmCustomerDetail"  smartNavigation="False"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmCustomerRequest</title>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function ShowPPDealerSelection()
		{
			showPopUp('../PopUp/PopUpDealerSelectionOne.aspx?x=Territory','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var tempParam = selectedDealer.split(';');
			var txtDealer = document.getElementById("txtDealer");
			txtDealer.value = tempParam[0];				
		}
		function autofocus(field,next) 
		{
			if (field.value.length == field.maxLength) 
			{
				field.form.elements[next].focus();
			}
		}
		
		function ShowCustomerList()
		{
			showPopUp('../PopUp/PopUpCustomerList.aspx','',500,760,CustomerSelection);
		}
		function ShowNoPengajuanList()
		{
			showPopUp('../PopUp/PopUpCustomerList.aspx','',500,760,PengajuanSelection);
		}
		function PengajuanSelection(selectedCustomer)
		{
			var arr= selectedCustomer.split(';');
			var txtRefNoPengajuan = document.getElementById("txtRefNoPengajuan");
			txtRefNoPengajuan.value = arr[0];
		}
		function CustomerSelection(selectedCustomer)
		{
			var arr= selectedCustomer.split(';');
			var txtRefKodePelanggan = document.getElementById("txtRefKodePelanggan");
			txtRefKodePelanggan.value = arr[1];
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="740" border="0">
				<tr>
					<td class="titlePage">
						KONSUMEN&nbsp;- Konsumen Detail
					</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top">
						<TABLE id="titleUmum" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td class="titlePanel"><b>INFORMASI PELANGGAN : UMUM</b></td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
						</TABLE>
						<TABLE cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>
								<TD class="titleField" width="30%">Kode Pelanggan</TD>
								<TD width="1%">:</TD>
								<TD width="28%"><asp:label id="lblKodePelanggan" runat="server"></asp:label></TD>
								<TD style="WIDTH: 1px" width="1"></TD>
								<TD class="titleField" width="15%"></TD>
								<TD width="1%"></TD>
								<TD width="25%">&nbsp;</TD>
							</tr>
							<TR>
								<TD class="titleField" width="20%">Nama 1</TD>
								<TD width="1%">:</TD>
								<td colSpan="4">
									<asp:label id="lblNama1" runat="server"></asp:label>
								</td>
							</TR>
							<TR>
								<TD class="titleField" width="20%">Nama 2</TD>
								<TD width="1%">:</TD>
								<td colSpan="4">
									<asp:label id="lblNama2" runat="server"></asp:label>
								</td>
							</TR>
							<TR>
								<TD class="titleField" width="20%">Gedung</TD>
								<TD width="1%">:</TD>
								<td width="28%"><asp:label id="lblGedung" runat="server"></asp:label>&nbsp;</td>
								<TD width="25"></TD>
								<TD class="titleField" width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="28%"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%">Alamat</TD>
								<TD width="1%">:</TD>
								<td width="28%"><asp:label id="lblAlamat" runat="server"></asp:label>&nbsp;</td>
								<TD width="1"></TD>
								<TD class="titleField" width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="28%"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%">Kelurahan</TD>
								<TD width="1%">:</TD>
								<td><asp:label id="lblKelurahan" runat="server"></asp:label>&nbsp;</td>
								<TD style="WIDTH: 1px" width="1"></TD>
								<TD class="titleField"></TD>
								<TD width="1%"></TD>
								<TD width="28%"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%">Kecamatan</TD>
								<TD width="1%">:</TD>
								<td><asp:label id="lblKecamatan" runat="server"></asp:label>&nbsp;</td>
								<TD style="WIDTH: 1px" width="1"></TD>
								<TD class="titleField"></TD>
								<TD width="1%"></TD>
								<TD width="28%">&nbsp;</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px" width="20%">Kota/Kabupaten</TD>
								<TD style="HEIGHT: 18px" width="1%">:</TD>
								<td><asp:label id="lblKota" runat="server"></asp:label></td>
								<TD style="WIDTH: 1px; HEIGHT: 18px" width="1"></TD>
								<TD class="titleField">Kode Pos</TD>
								<TD style="HEIGHT: 18px" width="1%">:</TD>
								<TD style="HEIGHT: 18px" width="28%"><asp:label id="lblKodePos" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%">Propinsi</TD>
								<TD width="1%">:</TD>
								<td style="WIDTH: 293px" width="293"><asp:label id="lblPropinsi" runat="server"></asp:label></td>
								<TD style="WIDTH: 1px" width="1"></TD>
								<TD class="titleField"><asp:label id="lblCetakTitle" runat="server" tooltip="Print Propinsi di Faktur">Cetak</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="28%"><asp:label id="lblCetak" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%"></TD>
								<TD width="1%"></TD>
								<td><asp:label id="lblTelepon" runat="server" Visible="False"></asp:label>&nbsp;</td>
								<TD style="WIDTH: 1px" width="1"></TD>
								<TD class="titleField"></TD>
								<TD width="1%"></TD>
								<TD width="28%">&nbsp;<asp:label id="lblEmail" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%"></TD>
								<TD width="1%"></TD>
								<td colSpan="4">&nbsp;&nbsp;&nbsp;&nbsp;</td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><asp:panel id="Panel1" runat="server" Height="40px" Visible="True">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="600" border="0">
								<TR>
									<TD><BR>
										<asp:Panel id="pnlPerorangan" runat="server">
											<TABLE id="titlePanel1" cellSpacing="0" cellPadding="0" width="100%" border="0">
												<TR>
													<TD class="titlePanel"><B>PERORANGAN :</B></TD>
												</TR>
												<TR>
													<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
												</TR>
											</TABLE>
										</asp:Panel><BR>
										<asp:Panel id="pnlPerusahaan" runat="server">
											<TABLE id="titlePanel2" cellSpacing="0" cellPadding="0" width="100%" border="0">
												<TR>
													<TD class="titlePanel"><B>PERUSAHAAN :</B></TD>
												</TR>
												<TR>
													<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
												</TR>
											</TABLE>
										</asp:Panel><BR>
										<asp:Panel id="PnlBUMN" runat="server">
											<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
												<TR>
													<TD class="titlePanel"><B>BUMN&nbsp;:</B></TD>
												</TR>
												<TR>
													<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
												</TR>
											</TABLE>
										</asp:Panel>
										<asp:panel id="PnlLainnya" runat="server">
											<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
												<TR>
													<TD class="titlePanel"><B>Lainnya&nbsp;:</B></TD>
												</TR>
												<TR>
													<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
												</TR>
											</TABLE>
										</asp:panel>
										<asp:Panel id="pnlTambahan" runat="server">
											<TABLE id="titlePanel3" cellSpacing="0" cellPadding="0" width="100%" border="0">
												<TR>
													<TD class="titlePanel"><B>TAMBAHAN :</B></TD>
												</TR>
												<TR>
													<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
												</TR>
											</TABLE>
										</asp:Panel></TD>
								</TR>
							</TABLE>
						</asp:panel><asp:button id="btnBack" runat="server" Text="Kembali" CausesValidation="False"></asp:button></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
