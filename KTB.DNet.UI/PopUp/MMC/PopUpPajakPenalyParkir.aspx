<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpPajakPenalyParkir.aspx.vb" Inherits="PopUpPajakPenalyParkir" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
		<title>Penalty Parkir - Bukti Pemotongan PPh 23</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript">				
			function PrintDocument()
			{			
				var kantor = document.getElementById('lblKantorPajak');
				var kota = document.getElementById('lblKota');
				var pejabat = document.getElementById('lblPejabat');
				var jabatan = document.getElementById('lblJabatan');
				if (kantor.innerHTML == "")
				{	
					alert('Kantor Pajak harap diisi!');
					return;
				}
				if (kota.innerHTML == "")
				{	
					alert('Kota dealer harap diisi!');
					return;
				}
				if (pejabat.innerHTML == "")
				{	
					alert('Pejabat harap diisi!');
					return;
				}
				if (jabatan.innerHTML == "")
				{	
					alert('Jabatan harap diisi!');
					return;
				}
				document.getElementById('btnSimpan').click();
				document.getElementById('divpajak').style.display='none';
				window.print();
			}
			
			function Kantor(obj) {
			obj.value=obj.value.toUpperCase();
			var ktr = document.getElementById('lblKantorPajak');
			ktr.innerHTML = document.getElementById('txtKtrPajak').value;
			}
			
			function Kota(obj) {
			//obj.value=obj.value.toUpperCase();
			var kota = document.getElementById('lblKota');
			kota.innerHTML = document.getElementById('txtKota').value;
			}
			
			function Pejabat(obj) {
			//obj.value=obj.value.toUpperCase();
			var ktr = document.getElementById('lblPejabat');
			ktr.innerHTML = document.getElementById('txtPejabat').value;
			}
			
			function Jabatan(obj) {
			//obj.value=obj.value.toUpperCase();
			var ktr = document.getElementById('lblJabatan');
			ktr.innerHTML = document.getElementById('txtJabatan').value;
			}
			
		</script>
	</HEAD>
	<body>
		<form id="form1" runat="server">
			<div id="divpajak" align="center">
				<table border="0">
					<tr class="hideTrOnPrint">
						<td>Kantor Pajak</td>
						<td>:</td>
						<td noWrap align="left"><span style="FONT-WEIGHT: bold; FONT-SIZE: 10pt"><input id="txtKtrPajak" onkeyup="Kantor(this)" style="align: center" type="text" runat="server">
							</span>
						</td>
					</tr>
					<tr class="hideTrOnPrint">
						<td>Nama Kota</td>
						<td>:</td>
						<td noWrap align="left"><span style="FONT-WEIGHT: bold; FONT-SIZE: 10pt"><input id="txtKota" onkeyup="Kota(this)" style="align: center" type="text" runat="server">
							</span>
						</td>
					</tr>
					<tr class="hideTrOnPrint">
						<td>Pejabat</td>
						<td>:</td>
						<td noWrap align="left"><span style="FONT-WEIGHT: bold; FONT-SIZE: 10pt"><input id="txtPejabat" onkeyup="Pejabat(this)" style="align: center" type="text" runat="server">
							</span>
						</td>
					</tr>
					<tr class="hideTrOnPrint">
						<td>Jabatan</td>
						<td>:</td>
						<td noWrap align="left"><span style="FONT-WEIGHT: bold; FONT-SIZE: 10pt"><input id="txtJabatan" onkeyup="Jabatan(this)" style="align: center" type="text" runat="server">
							</span>
						</td>
					</tr>
					<tr class="hideTrOnPrint">
						<td align="center" colSpan="3">
							<input class="hideButtonOnPrint" id="btnSimpan" type="button" value="Simpan" runat="server"
								style="DISPLAY:none"> <input class="hideButtonOnPrint" id="btnCetak" onclick="PrintDocument()" type="button"
								value="Cetak" runat="server"> <INPUT class="hideButtonOnPrint" id="btnClose" onclick="window.close()" type="button" value="Tutup"
								name="btnClose">
						</td>
					</tr>
				</table>
			</div>
			<div align="center">
				<table cellSpacing="0" cellPadding="0">
					<tr>
						<td colSpan="40">
							<table cellSpacing="0" cellPadding="0">
								<tr>
									<td style="WIDTH: 70%"></td>
									<td style="WIDTH: 30%" noWrap align="left"><span style="FONT-SIZE: 10pt; FONT-STYLE: normal">Lembar 
											ke-1 untuk : yang menyewakan</span>
									</td>
								</tr>
								<tr>
									<td style="WIDTH: 70%"></td>
									<td style="WIDTH: 30%" noWrap align="left"><span style="FONT-SIZE: 10pt; FONT-STYLE: normal">Lembar 
											ke-2 untuk : Kantor Pelayanan Pajak</span>
									</td>
								</tr>
								<tr>
									<td style="WIDTH: 70%"></td>
									<td style="WIDTH: 30%" noWrap align="left"><span style="FONT-SIZE: 10pt; FONT-STYLE: normal">Lembar 
											ke-3 untuk : Penyewa</span>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
						<td style="WIDTH: 0.45cm">&nbsp;</td>
					</tr>
					<tr align="center">
						<td align="center" colSpan="40">
							<table cellSpacing="0" cellPadding="0" width="100%">
								<tr>
									<td style="WIDTH: 8%" vAlign="middle" align="center"><IMG src="../images/logo_pajak.jpg">
									</td>
									<td style="WIDTH: 92%" align="left">
										<table cellSpacing="0" cellPadding="0" width="50%">
											<tr>
												<td noWrap align="center"><span style="FONT-WEIGHT: bold; FONT-SIZE: 10pt">DEPARTEMEN 
														KEUANGAN REPUBLIK INDONESIA</span></td>
											</tr>
											<tr>
												<td noWrap align="center"><span style="FONT-WEIGHT: bold; FONT-SIZE: 10pt">DIREKTORAT 
														JENDERAL PAJAK</span></td>
											</tr>
											<tr>
												<td noWrap align="center"><span style="FONT-WEIGHT: bold; FONT-SIZE: 10pt">KANTOR 
														PELAYANAN PAJAK</span></td>
											</tr>
											<tr>
												<td noWrap align="center"><span style="FONT-WEIGHT: bold; FONT-SIZE: 10pt"><asp:label id="lblKantorPajak" runat="server"></asp:label></span></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td colSpan="40">&nbsp;</td>
					</tr>
					<tr height="25">
						<td colSpan="40">
							<table cellSpacing="0" cellPadding="0" width="100%">
								<tr>
									<td style="WIDTH: 20%"></td>
									<td style="WIDTH: 60%" align="center">
										<table cellSpacing="0" cellPadding="0" width="100%">
											<tr>
												<td style="BORDER-RIGHT: #000000 thin solid; BORDER-TOP: #000000 thin solid; BORDER-BOTTOM-WIDTH: thin; BORDER-BOTTOM-COLOR: #000000; BORDER-LEFT: #000000 thin solid; BACKGROUND-COLOR: gray"
													noWrap align="center"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">BUKTI 
														PEMOTONGAN PPh FINAL PASAL 4 AYAT (2)</span>
												</td>
											</tr>
											<tr>
												<td style="BORDER-TOP-WIDTH: thin; BORDER-RIGHT: #000000 thin solid; BORDER-BOTTOM-WIDTH: thin; BORDER-BOTTOM-COLOR: #000000; BORDER-LEFT: #000000 thin solid; BORDER-TOP-COLOR: #000000; BACKGROUND-COLOR: gray"
													noWrap align="center"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">ATAS 
														PENGHASILAN DARI PERSEWAAN TANAH</span>
												</td>
											</tr>
											<tr>
												<td style="BORDER-TOP-WIDTH: thin; BORDER-RIGHT: #000000 thin solid; BORDER-BOTTOM-WIDTH: thin; BORDER-BOTTOM-COLOR: #000000; BORDER-LEFT: #000000 thin solid; BORDER-TOP-COLOR: #000000; BACKGROUND-COLOR: gray"
													noWrap align="center"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">DAN/ATAU 
														BANGUNAN</span>
												</td>
											</tr>
											<tr>
												<td style="BORDER-RIGHT: #000000 thin solid; BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid; BACKGROUND-COLOR: gray"
													noWrap align="center"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt"><asp:label id="lblNomor" runat="server"></asp:label></span></td>
											</tr>
										</table>
									</td>
									<td style="WIDTH: 20%"></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td colSpan="40">&nbsp;</td>
					</tr>
					<tr height="25">
						<td align="left" colSpan="9"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">&nbsp;&nbsp;NPWP</span></td>
						<td><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">:&nbsp;</span></td>
						<td style="BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000"
							align="center">0</td>
						<td style="BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000"
							align="center">1</td>
						<td style="BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000"
							align="center">.</td>
						<td style="BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000"
							align="center">3</td>
						<td style="BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000"
							align="center">0</td>
						<td style="BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000"
							align="center">0</td>
						<td style="BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000"
							align="center">.</td>
						<td style="BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000"
							align="center">6</td>
						<td style="BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000"
							align="center">5</td>
						<td style="BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000"
							align="center">7</td>
						<td style="BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000"
							align="center">.</td>
						<td style="BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000"
							align="center">2</td>
						<td style="BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000"
							align="center">-</td>
						<td style="BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000"
							align="center">0</td>
						<td style="BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000"
							align="center">9</td>
						<td style="BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000"
							align="center">2</td>
						<td style="BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000"
							align="center">.</td>
						<td style="BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000"
							align="center">0</td>
						<td style="BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000"
							align="center">0</td>
						<td style="BORDER-RIGHT: #000000 thin solid; BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid"
							align="center">0</td>
						<td colSpan="11"></td>
					</tr>
					<tr height="25">
						<td align="left" colSpan="9"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">&nbsp;&nbsp;Nama</span></td>
						<td><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">:&nbsp;</span></td>
						<td align="left" colSpan="30">PT. MITSUBISHI MOTORS KRAMA YUDHA SALES INDONESIA</td>
					</tr>
					<tr height="25">
						<td align="left" colSpan="9"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">&nbsp;&nbsp;Alamat</span></td>
						<td><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">:&nbsp;</span></td>
						<td align="left" colSpan="30">JL. JEND. A. YANI PROYEK PULOMAS JAKARTA TIMUR</td>
					</tr>
					<tr height="25">
						<td noWrap align="left" colSpan="9"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">&nbsp;&nbsp;Lokasi 
								Tanah dan</span></td>
						<td><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">:&nbsp;</span></td>
						<td align="left" colSpan="30">JL. JEND. A. YANI PROYEK PULOMAS JAKARTA TIMUR</td>
					</tr>
					<tr height="25">
						<td align="left" colSpan="9"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">&nbsp;&nbsp;Bangunan</span></td>
						<td>&nbsp;</td>
						<td align="left" colSpan="30"></td>
					</tr>
					<tr>
						<td colSpan="40">&nbsp;</td>
					</tr>
					<tr>
						<td colSpan="40">
							<table cellSpacing="0" cellPadding="0" width="100%"> <!--bordercolorlight="black" bordercolor="black"-->
								<tr height="30">
									<td style="BORDER-RIGHT: #000000 thin solid; BORDER-TOP: #000000 thin solid; BORDER-BOTTOM-WIDTH: thin; BORDER-BOTTOM-COLOR: #000000; BORDER-LEFT: #000000 thin solid; BACKGROUND-COLOR: gray"
										align="center" colSpan="18"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">Jumlah 
											Bruto Nilai Sewa</span></td>
									<td style="BORDER-RIGHT: #000000 thin solid; BORDER-TOP: #000000 thin solid; BORDER-LEFT-WIDTH: thin; BORDER-LEFT-COLOR: #000000; BORDER-BOTTOM-WIDTH: thin; BORDER-BOTTOM-COLOR: #000000; BACKGROUND-COLOR: gray"
										align="center" colSpan="6"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">Tarif</span></td>
									<td style="BORDER-RIGHT: #000000 thin solid; BORDER-TOP: #000000 thin solid; BORDER-LEFT-WIDTH: thin; BORDER-LEFT-COLOR: #000000; BORDER-BOTTOM-WIDTH: thin; BORDER-BOTTOM-COLOR: #000000; BACKGROUND-COLOR: gray"
										align="center" colSpan="16"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">PPh 
											yang dipotong</span></td>
								</tr>
								<tr height="30">
									<td style="BORDER-TOP-WIDTH: thin; BORDER-RIGHT: #000000 thin solid; BORDER-BOTTOM-WIDTH: thin; BORDER-BOTTOM-COLOR: #000000; BORDER-LEFT: #000000 thin solid; BORDER-TOP-COLOR: #000000; BACKGROUND-COLOR: gray"
										align="center" colSpan="18"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">(Rp)</span></td>
									<td style="BORDER-TOP-WIDTH: thin; BORDER-RIGHT: #000000 thin solid; BORDER-LEFT-WIDTH: thin; BORDER-LEFT-COLOR: #000000; BORDER-BOTTOM-WIDTH: thin; BORDER-BOTTOM-COLOR: #000000; BORDER-TOP-COLOR: #000000; BACKGROUND-COLOR: gray"
										align="center" colSpan="6"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">(%)</span></td>
									<td style="BORDER-TOP-WIDTH: thin; BORDER-RIGHT: #000000 thin solid; BORDER-LEFT-WIDTH: thin; BORDER-LEFT-COLOR: #000000; BORDER-BOTTOM-WIDTH: thin; BORDER-BOTTOM-COLOR: #000000; BORDER-TOP-COLOR: #000000; BACKGROUND-COLOR: gray"
										align="center" colSpan="16"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">(Rp)</span></td>
								</tr>
								<tr height="30">
									<td style="BORDER-RIGHT: #000000 thin solid; BORDER-TOP: #000000 thin solid; BORDER-BOTTOM-WIDTH: thin; BORDER-BOTTOM-COLOR: #000000; BORDER-LEFT: #000000 thin solid; BACKGROUND-COLOR: gray"
										align="center" colSpan="18"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">(1)</span></td>
									<td style="BORDER-RIGHT: #000000 thin solid; BORDER-TOP: #000000 thin solid; BORDER-LEFT-WIDTH: thin; BORDER-LEFT-COLOR: #000000; BORDER-BOTTOM-WIDTH: thin; BORDER-BOTTOM-COLOR: #000000; BACKGROUND-COLOR: gray"
										align="center" colSpan="6"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">(2)</span></td>
									<td style="BORDER-RIGHT: #000000 thin solid; BORDER-TOP: #000000 thin solid; BORDER-LEFT-WIDTH: thin; BORDER-LEFT-COLOR: #000000; BORDER-BOTTOM-WIDTH: thin; BORDER-BOTTOM-COLOR: #000000; BACKGROUND-COLOR: gray"
										align="center" colSpan="16"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">(3)</span></td>
								</tr>
								<tr height="80">
									<td style="BORDER-RIGHT: #000000 thin solid; BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid"
										align="center" colSpan="18"><asp:label id="lblNilai1" runat="server"></asp:label></td>
									<td style="BORDER-RIGHT: #000000 thin solid; BORDER-TOP: #000000 thin solid; BORDER-LEFT-WIDTH: thin; BORDER-LEFT-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid"
										align="center" colSpan="6">10%</td>
									<td style="BORDER-RIGHT: #000000 thin solid; BORDER-TOP: #000000 thin solid; BORDER-LEFT-WIDTH: thin; BORDER-LEFT-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid"
										align="center" colSpan="16"><asp:label id="lblNilai2" runat="server"></asp:label></td>
								</tr>
								<tr height="30">
									<td style="BORDER-TOP-WIDTH: thin; BORDER-RIGHT: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid"
										align="left" colSpan="40"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">&nbsp;&nbsp;Terbilang 
											:</span><asp:label id="lblTerbilang" runat="server"></asp:label></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td colSpan="40">&nbsp;</td>
					</tr>
					<tr>
						<td colSpan="18"></td>
						<td align="center" colSpan="22"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt"><asp:label id="lblKota" runat="server"></asp:label><asp:label id="lblTanggal" runat="server"></asp:label></span></td>
					</tr>
					<tr>
						<td colSpan="40">&nbsp;</td>
					</tr>
					<tr height="25">
						<td colSpan="18"></td>
						<td align="center" colSpan="22"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">Pemotong 
								Pajak</span></td>
					</tr>
					<tr height="25">
						<td colSpan="16"></td>
						<td align="left" colSpan="3"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">NPWP</span></td>
						<td><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">:</span></td>
						<asp:literal id="ltlNPWP2" runat="server"></asp:literal></tr>
					<tr height="25">
						<td align="left" colSpan="16"></td>
						<td vAlign="middle" align="left" colSpan="3"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">Nama</span></td>
						<td vAlign="middle"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">:</span></td>
						<td vAlign="middle" align="left" colSpan="19"><asp:label id="lblNama2" runat="server"></asp:label></td>
					</tr>
					<tr height="25">
						<td colSpan="40"></td>
					</tr>
					<tr>
						<td style="BORDER-RIGHT: #000000 thin solid; BORDER-TOP: #000000 thin solid; BORDER-BOTTOM-WIDTH: thin; BORDER-BOTTOM-COLOR: #000000; BORDER-LEFT: #000000 thin solid"
							align="left" colSpan="18"><span style="FONT-WEIGHT: bold; FONT-SIZE: 9pt">&nbsp;&nbsp;Perhatian 
								:</span></td>
						<td colSpan="22"></td>
					</tr>
					<tr>
						<td style="BORDER-TOP-WIDTH: thin; BORDER-RIGHT: #000000 thin solid; BORDER-BOTTOM-WIDTH: thin; BORDER-BOTTOM-COLOR: #000000; BORDER-LEFT: #000000 thin solid; BORDER-TOP-COLOR: #000000"
							align="left" colSpan="18"><span style="FONT-WEIGHT: bold; FONT-SIZE: 9pt">&nbsp;&nbsp;1. 
								Jumlah Pajak Penghasilan atas</span></td>
						<td align="center" colSpan="22"><span style="FONT-WEIGHT: bold; FONT-SIZE: 10pt"></span></td>
					</tr>
					<tr>
						<td style="BORDER-TOP-WIDTH: thin; BORDER-RIGHT: #000000 thin solid; BORDER-BOTTOM-WIDTH: thin; BORDER-BOTTOM-COLOR: #000000; BORDER-LEFT: #000000 thin solid; BORDER-TOP-COLOR: #000000"
							align="left" colSpan="18"><span style="FONT-WEIGHT: bold; FONT-SIZE: 9pt">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Persewaan 
								Tanah dan/atau Bangunan</span></td>
						<td colSpan="22"></td>
					</tr>
					<tr>
						<td style="BORDER-TOP-WIDTH: thin; BORDER-RIGHT: #000000 thin solid; BORDER-BOTTOM-WIDTH: thin; BORDER-BOTTOM-COLOR: #000000; BORDER-LEFT: #000000 thin solid; BORDER-TOP-COLOR: #000000"
							align="left" colSpan="18"><span style="FONT-WEIGHT: bold; FONT-SIZE: 9pt">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;yang 
								dipotong di atas bukan merupakan</span></td>
						<td colSpan="22"></td>
					</tr>
					<tr>
						<td style="BORDER-TOP-WIDTH: thin; BORDER-RIGHT: #000000 thin solid; BORDER-BOTTOM-WIDTH: thin; BORDER-BOTTOM-COLOR: #000000; BORDER-LEFT: #000000 thin solid; BORDER-TOP-COLOR: #000000"
							align="left" colSpan="18"><span style="FONT-WEIGHT: bold; FONT-SIZE: 9pt">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;kredit 
								pajak dalam Surat Pemberitahuan</span></td>
						<td colSpan="22"></td>
					</tr>
					<tr>
						<td style="BORDER-TOP-WIDTH: thin; BORDER-RIGHT: #000000 thin solid; BORDER-BOTTOM-WIDTH: thin; BORDER-BOTTOM-COLOR: #000000; BORDER-LEFT: #000000 thin solid; BORDER-TOP-COLOR: #000000"
							align="left" colSpan="18"><span style="FONT-WEIGHT: bold; FONT-SIZE: 9pt">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(SPT) 
								Tahunan</span></td>
						<td colSpan="22"></td>
					</tr>
					<tr>
						<td style="BORDER-TOP-WIDTH: thin; BORDER-RIGHT: #000000 thin solid; BORDER-BOTTOM-WIDTH: thin; BORDER-BOTTOM-COLOR: #000000; BORDER-LEFT: #000000 thin solid; BORDER-TOP-COLOR: #000000"
							align="left" colSpan="18"><span style="FONT-WEIGHT: bold; FONT-SIZE: 9pt">&nbsp;&nbsp;2. 
								Bukti Pemotongan ini dianggap sah apabila </span>
						</td>
						<td colSpan="22"></td>
					</tr>
					<tr>
						<td style="BORDER-TOP-WIDTH: thin; BORDER-RIGHT: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid"
							align="left" colSpan="18"><span style="FONT-WEIGHT: bold; FONT-SIZE: 9pt">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;diisi 
								dengan lengkap dan benar.</span></td>
						<td align="center" colSpan="22"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt"><asp:label id="lblPejabat" runat="server"></asp:label></span></td>
					</tr>
					<tr>
						<td colSpan="18"></td>
						<td align="center" colSpan="22"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt"><asp:label id="lblJabatan" runat="server"></asp:label></span></td>
					</tr>
					<tr>
						<td colSpan="40">&nbsp;</td>
					</tr>
					<tr>
						<td align="left" colSpan="18"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt">&nbsp;&nbsp;F.1.1.33.12</span></td>
						<td colSpan="22"></td>
					</tr>
				</table>
			</div>
		</form>
	</body>
</HTML>
