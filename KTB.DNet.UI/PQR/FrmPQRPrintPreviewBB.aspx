<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPQRPrintPreviewBB.aspx.vb" Inherits="FrmPQRPrintPreviewBB" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>FrmPQRHeaderBB</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			function BackButton()
			{
				//var ret = (parseInt(document.getElementById("hid_History").value) + 1)* (-1)
				//document.getElementById("btnBack").disabled=true
				//history.go(ret)
				document.location.href="../SparePart/FrmPQRList.aspx";
			}
			function focusSave()
			{
				document.getElementById("btnSimpan").focus();			
			}
		</script>
</HEAD>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" align="center">PRODUCT QUALITY REPORT SPECIAL&nbsp;"CONFIDENTIAL"</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
			</TABLE>
			<table cellSpacing="1" cellPadding="4" width="100%" border="0">
				<tr vAlign="top">
					<td width="0">
						<table  cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>

								<TD colspan=6 align=center><b>Model : </b><asp:label id="lbModel" Runat="server"></asp:label></TD>
							</tr>
							<tr>
								<td colSpan="6">&nbsp;</td>
							</tr>
							<TR valign=top>
								<td class="titleField" width="25%"><asp:label id="lblTglPembuatan" Runat="server">Tgl Pembuatan</asp:label></td>
								<td width="1%">:</td>
								<td width="24%"><asp:label id="lblTglPembuatanVal" Runat="server"></asp:label></td>
								<TD width="20%" class="titleField"><asp:label id="lblDealer" Runat="server">Dealer : </asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="29%"><asp:label id="lblDealerVal" Runat="server"></asp:label></TD>
							</TR>
							<tr valign=top>
								<td class="titleField"><asp:label id="lblTglKerusakan" Runat="server">Tanggal Kerusakan</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblTglKerusakanVal" Runat="server"></asp:label></td>
								<td class="titleField"><asp:label id="lblOdometer" Runat="server">Odometer</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblOdometerVal" Runat="server"></asp:label>&nbsp;<span style="FONT-SIZE: 8pt">Km</span>
								</td>
							</tr>
							<tr valign=top>
								<td class="titleField"><asp:label id="lblTglDelivery" Runat="server">Tanggal Delivery</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblTglDeliveryVal"  Runat="server"></asp:label></td>
								<td class="titleField">No PQR</td>
								<td>:</td>
								<td><asp:label id="lblPQRNoVal" Runat="server"></asp:label></td>
							</tr>
							<tr valign=top>
          <TD class="titleField>
			<asp:label id="lblTglFaktur" Runat="server" ><B>Tanggal Buka Faktur</B> </asp:label></TD>
          <TD>:</TD>
          <TD>
<asp:label id=lblTglFakturVal Runat="server"></asp:label></TD>
          <TD class=titleField>
<asp:label id=lblRefPQRNo Runat="server">No PQR Ref</asp:label></TD>
          <TD>:</TD>
          <TD>
<asp:label id=lblRefPQRNoVal Runat="server"></asp:label></TD></tr>
        <TR vAlign=top>
          <TD class=titleField>
<asp:label id=lblThnProduksi Runat="server">Tahun Produksi</asp:label></TD>
          <TD>:</TD>
          <TD>
<asp:label id=lblThnProduksiVal Runat="server"></asp:label></TD>
          <TD class=titleField></TD>
          <TD></TD>
          <TD></TD></TR>
        <TR vAlign=top>
          <TD class=titleField>
<asp:label id=lblNoChasis Runat="server">No Rangka</asp:label></TD>
          <TD>:</TD>
          <TD>
<asp:label id=lblNoChasisVal Runat="server"></asp:label></TD>
          <TD class=titleField>
<asp:label id=lblTypeColor Runat="server">Tipe / Warna</asp:label></TD>
          <TD>:</TD>
          <TD>
<asp:label id=lblTypeColorVal Runat="server"></asp:label></TD></TR>
        <TR vAlign=top>
          <TD class=titleField>
<asp:label id=lblNoMesin Runat="server">No Mesin</asp:label></TD>
          <TD style="HEIGHT: 16px">:</TD>
								<td style="HEIGHT: 16px"><asp:label id="lblNoMesinVal" Runat="server"></asp:label></td>
								<td class="titleField"><asp:label id="lblNama" Runat="server">Nama Pemilik</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblNamaVal" Runat="server"></asp:label></td></TR>
							<TR>
								<td colSpan="6"></td>
							</TR>
							<tr valign=top>
								<td colSpan="3" width="50%"><table border=0 cellpadding=0 cellspacing=0 width="100%"><tr><td><asp:panel id="pnlProfile1" Runat="server"></asp:panel></td><td width="10%"><img src="images/blank.gif" border="0" width="53" height="1"></td></tr></table></td>
								<td colSpan="3" width="50%" valign=bottom><asp:panel id="pnlProfile2" Runat="server"></asp:panel></td>
							</tr>
							<tr>
								<td colSpan="6"></td>
							</tr>
							<tr valign=top>
								<td class="titleField"><asp:label id="lblKodePosisi" Runat="server">Kode Posisi</asp:label></td>
								<td>:</td>
								<td colspan=4><asp:label id="lblKodePosisiVal"  Runat="server"></asp:label></td>
							</tr>
							<tr valign=top >
								<td class="titleField"><asp:label id="lblPart" Runat="server">Part Name/No</asp:label></td>
								<td>:</td>
								<td colspan=4><asp:label id="lblPartVal" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td colSpan="6"></td>
							</tr>
							<tr valign=top>
								<td class="titleField"><asp:label id="lblKecepatan" Runat="server">Kecepatan</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblKecepatanVal" Runat="server"></asp:label>&nbsp;<SPAN style="FONT-SIZE: 8pt">Km 
										/ Jam</SPAN>
								</td>
								<td class="titleField" align=right>Kode 
          Kerusakan:</td>
								<td></td>
								<td></td>
							</tr>
							<tr valign=top>
								<td class="titleField"><asp:label id="lblSubject" Runat="server">Subject</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblSubjectVal" Runat="server"></asp:label></td>
								<td >A : 
<asp:label id=lblCodeA Runat="server"></asp:label></td>
								<td></td>
								<td></td>
							</tr>
							<tr vAlign="top">
								<td class="titleField"><asp:label id="lblGejala" Runat="server">Gejala</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblGejalaVal" Runat="server"></asp:label></td>
								<td >B : 
<asp:label id=lblCodeB Runat="server"></asp:label></td>
								<td></td>
								<td></td>
							</tr>
							<tr vAlign="top">
								<td class="titleField"><asp:label id="lblPenyebab" Runat="server">Penyebab</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblPenyebabVal" Runat="server"></asp:label></td>
								<td >C : 
<asp:label id=lblCodeC Runat="server"></asp:label></td>
								<td></td>
								<td></td>
							</tr>
							<tr vAlign="top">
								<td class="titleField"><asp:label id="lblHasil" Runat="server">Perbaikan</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblHasilVal" Runat="server"></asp:label></td>
								<td class="titleField"></td>
								<td></td>
								<td></td>
							</tr>
							<tr vAlign="top">
								<td class="titleField"><asp:label id="lblCatatan" Runat="server">Catatan</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblCatatanVal" Runat="server"></asp:label></td>
								<td class="titleField"></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td class="titleField"><asp:label id="lblLampiranAtas" Runat="server">Lampiran</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblLampiranAtasVal" Runat="server"></asp:label></td>
								<td class="titleField"></td>
								<td></td>
								<td></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="titlePage" align="center">================== Penjelasan MMKSI 
						==================</td>
				</tr>
				<tr>
					<td>
						<table width="100%">
							<tr>
								<td><asp:label id="lblTglRilis" Runat="server"></asp:label></td>
								<td colSpan="2"><asp:label id="lblUserRilis" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td colSpan="3"><asp:label id="lblSolution" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="titleField" style="WIDTH: 180px"><asp:label id="lblLampiranBawah" Runat="server">Lampiran</asp:label></td>
								<td width="1">:</td>
								<td><asp:label id="lblLampiranBawahVal" Runat="server"></asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="titlePage" align="center">================== Proses Komunikasi 
						==================</td>
				</tr>
				<tr>
					<td>
						<div id="divKomunikasi" runat="server"></div>
					</td>
				</tr>
				<TR>
					<td align="center" colSpan="2"><INPUT class="hideButtonOnPrint" id="btnCetak" style="WIDTH: 48px; HEIGHT: 21px" onclick="window.print()"
							type="button" value="Cetak" name="btnCetak"> 
						<asp:button id="btnBatal" Runat="server" Text="Kembali" CausesValidation="False" class="hideButtonOnPrint"></asp:button></td>
				</TR>
			</table>
		</form></SPAN>
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
	</BODY>
</HTML>
