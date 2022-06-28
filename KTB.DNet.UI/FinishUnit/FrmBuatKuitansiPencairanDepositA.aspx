<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmBuatKuitansiPencairanDepositA.aspx.vb" Inherits="FrmBuatKuitansiPencairanDepositA"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmBuatKuitansiPencairanDepositA</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
		<LINK rel="stylesheet" type="text/css" href="../WebResources/printstyle.css" media="print">
		<script language="javascript" type="text/javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);				
			}
					
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer.split(';');
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				var lblDealerName = document.getElementById("lblNamaDealer");
				txtDealerSelection.value = tempParam[0];
				lblDealerName.innerHTML	=tempParam[1] + " - " + tempParam[3];
				//return false;
				var btnImgDealer = document.getElementById("btnImgDealer");				
				btnImgDealer.click();
			}
			
			function PrintDocument()
			{
			   // var divGrid = document.getElementById("divGrid");
				//divGrid.style.overflow='visible';
				window.print();
				//if(navigator.appName == "Microsoft Internet Explorer")
				//{
				//	divGrid.style.overflow='auto';
				//}				
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="titlePage">Sales - DepositA - Pembuatan Kuitansi Pencairan</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG border="0" src="../images/dot.gif" height="1"></td>
				</tr>
				<tr>
					<td>
						<table id="Table2" border="0" cellSpacing="1" cellPadding="2" width="100%">
							<tr>
								<td style="WIDTH: 22%; HEIGHT: 16px" class="titleField"><asp:label id="lblCode" Runat="server">Kode Dealer</asp:label></td>
								<td style="WIDTH: 1%; HEIGHT: 16px">:</td>
								<td style="HEIGHT: 16px">
									<!--<asp:label id="lblKodeDealer" runat="server"></asp:label>-->
									<asp:TextBox ID="txtKodeDealer" runat="server" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"></asp:TextBox>
									<img id="imgDealerS" src="../images/popup.gif" style="CURSOR:hand" onclick="ShowPPDealerSelection();">
									<asp:ImageButton style="DISPLAY:none" ID="imgDealer" Runat="server" ImageUrl="../images/popup.gif"></asp:ImageButton>
									<asp:Button ID="btnImgDealer" Runat="server" Text="" style="DISPLAY:none"></asp:Button>
								</td>
							</tr>
							<tr>
								<td style="WIDTH: 22%" class="titleField"><asp:label id="Label1" Runat="server">Nama Dealer</asp:label></td>
								<td style="WIDTH: 1%">:</td>
								<td><asp:label id="lblNamaDealer" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td style="WIDTH: 22%; HEIGHT: 11px" class="titleField">Tipe Pengajuan</td>
								<td style="WIDTH: 1%; HEIGHT: 11px">:</td>
								<td style="HEIGHT: 11px" class="titleField"><asp:dropdownlist id="ddlTipePengajuan" Runat="server" AutoPostBack="True" Width="136px"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td style="WIDTH: 22%; HEIGHT: 3px" class="titleField"><asp:label id="Label2" Runat="server" Width="160px"> Nomor Reg. Pengajuan</asp:label></td>
								<td style="WIDTH: 1%; HEIGHT: 3px">:</td>
								<td style="HEIGHT: 3px" class="titleField">
									<asp:dropdownlist id="ddlNoRegPengajuan" Runat="server" AutoPostBack="True" Width="136px"></asp:dropdownlist>
								</td>
							</tr>
                            <tr>
                                <TD style="WIDTH: 22%; HEIGHT: 3px" class="titleField">Produk</TD>
								<TD style="WIDTH: 1%; HEIGHT: 3px">:</TD>
                                <TD style="HEIGHT: 3px"><asp:label id="lblProduk" runat="server"></asp:label></TD>
                            </tr>
							<TR>
								<TD style="WIDTH: 22%; HEIGHT: 3px" class="titleField">Nomor Ref. Surat Pengajuan</TD>
								<TD style="WIDTH: 1%; HEIGHT: 3px">:</TD>
								<TD style="HEIGHT: 3px"><asp:label id="lblNoRefSuratPengajuan" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 22%; HEIGHT: 3px" class="titleField">Nomor Rekening</TD>
								<TD style="WIDTH: 1%; HEIGHT: 3px">:</TD>
								<TD style="HEIGHT: 3px"><asp:label id="lblNoRekening" runat="server"></asp:label></TD>
							</TR>
							<tr>
								<td style="WIDTH: 22%" class="titleField"><asp:label id="Label3" Runat="server" Width="112px">Tanggal Pengajuan</asp:label></td>
								<td style="WIDTH: 1%">:</td>
								<td><asp:label id="lblTglPengajuan" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td style="WIDTH: 22%" class="titleField"><asp:label id="Label5" Runat="server" Width="88px">Nomor Kuitansi</asp:label></td>
								<td style="WIDTH: 1%">:</td>
								<td><asp:textbox id="txtNomorKuitansi" runat="server" MaxLength="40"></asp:textbox></td>
							</tr>
							<tr>
								<td style="WIDTH: 22%" class="titleField"><asp:label id="Label7" Runat="server" Width="96px">Tanggal Kwitansi</asp:label></td>
								<td style="WIDTH: 1%">:</td>
								<td><asp:label id="lblTanggalKwitansi" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td style="WIDTH: 22%" class="titleField"><asp:label id="Label9" Runat="server" Width="96px">Telah Terima dari</asp:label></td>
								<td style="WIDTH: 1%">:</td>
								<td><asp:label id="lblTelahTerimaDari" runat="server"></asp:label></td>
							</tr>
							<TR>
								<TD style="WIDTH: 22%" class="titleField"><asp:label id="Label6" Runat="server" Width="96px">Uang Sejumlah</asp:label></TD>
								<TD style="WIDTH: 1%">:</TD>
								<TD><asp:label id="lblUangSejumlah" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 22%" class="titleField"><asp:label id="Label12" Runat="server" Width="112px">Untuk Pembayaran</asp:label></TD>
								<TD style="WIDTH: 1%">:</TD>
								<TD><asp:label id="lblUangPembayaran" runat="server"></asp:label></TD>
							</TR>
							<tr>
								<td colSpan="3"></td>
							</tr>
							<TR>
								<TD class="titleField" colSpan="3"><br>
									<br>
									<br>
									<asp:label id="lblFooter" runat="server"></asp:label><br>
									<br>
									<br>
									<br>
									<br>
									<br>
									<br>
									<br>
									<asp:textbox id="txtSign" runat="server"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" colSpan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:label id="Label4" Runat="server" Width="80px">( Nama Jelas )</asp:label></TD>
							</TR>
							<tr>
								<TD class="titleField" colSpan="3">
									<asp:textbox id="txtJabatan" runat="server"></asp:textbox>
								</TD>
							</tr>
							<TR>
								<TD class="titleField" colSpan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:label id="Label8" Runat="server" Width="80px">( Jabatan )</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" colSpan="3">*) Nama yang bertanda tangan harus sesuai dengan 
									nama yang berhak atau diberi kuasa menandatangani kuitansi.</TD>
							</TR>
							<tr>
								<td colSpan="3">
									<table>
										<tr>
											<td><asp:button id="btnSimpan" class="hideButtonOnPrint" runat="server" Width="72px" Text="Simpan"></asp:button></td>
											<td><asp:button id="btnCetak" class="hideButtonOnPrint" runat="server" Width="72px" Text="Cetak"
													Visible="False"></asp:button></td>
											<td><asp:button id="btnNew" class="hideButtonOnPrint" runat="server" Width="72px" Text="Baru"></asp:button></td>
											<td><asp:button id="btnKembali" class="hideButtonOnPrint" runat="server" Width="72px" Text="Kembali"
													Visible="False"></asp:button></td>
											<td><asp:button id="btnValidasi" class="hideButtonOnPrint" runat="server" Width="72px" Text="Validasi"></asp:button></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
