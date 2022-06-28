<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmBabitKuitansi.aspx.vb" Inherits="FrmBabitKuitansi" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmBabitKuitansi</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		    function PrintDocument() {
		        window.print();
		    }
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 640px; POSITION: absolute; TOP: 8px; HEIGHT: 400px"
				cellSpacing="1" cellPadding="1" width="640" border="0">
				<TR>
					<TD align="right" colSpan="3"><asp:label id="lblDealerCode" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD align="right" colSpan="3"><asp:label id="lblDealerName" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD colSpan="3"><asp:label id="lblTgl" runat="server"></asp:label>
						<br>
						<br>
						<br>
					</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="3"><u><STRONG>KWITANSI</STRONG></u></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="3"><STRONG><asp:literal id="ltrNoKwitansi" runat="server"></asp:literal></STRONG></TD>
                </TR>
				<TR>
					<TD align="left" colSpan="3"><STRONG><asp:literal id="ltrBabitRegNumber" runat="server"></asp:literal></STRONG></TD>
                </TR>
				<TR>
					<TD style="WIDTH: 147px; HEIGHT: 28px">Telah Terima Dari</TD>
					<TD style="WIDTH: 6px; HEIGHT: 28px"><STRONG>:</STRONG></TD>
					<TD style="HEIGHT: 28px"><u>PT Mitsubishi Motors Krama Yudha Sales Indonesia</u></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 147px; HEIGHT: 28px"></TD>
					<TD style="WIDTH: 6px; HEIGHT: 28px"></TD>
					<TD style="HEIGHT: 28px">Jl. Jend. Ahmad Yani Jakarta<br>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 147px; HEIGHT: 25px">Uang Sejumlah</TD>
					<TD style="WIDTH: 6px; HEIGHT: 25px"><STRONG>:</STRONG></TD>
					<TD style="HEIGHT: 25px"><u><asp:literal id="ltrlTotalTerbilang" runat="server"></asp:literal></u></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 147px; HEIGHT: 27px">Untuk Pembayaran</TD>
					<TD style="WIDTH: 6px; HEIGHT: 27px"><STRONG>:</STRONG></TD>
					<TD style="HEIGHT: 27px"><u><asp:literal id="ltrlDescription" runat="server"></asp:literal></u></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 147px; HEIGHT: 27px">Nomor Rekening</TD>
					<TD style="WIDTH: 6px; HEIGHT: 27px">:</TD>
					<TD style="HEIGHT: 27px">
						<asp:literal id="ltrlNoRekening" runat="server"></asp:literal></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 147px; HEIGHT: 13px" colSpan="3"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 147px; HEIGHT: 50px" colSpan="3">
						<TABLE id="Table2" style="WIDTH: 300px; HEIGHT: 41px" cellSpacing="1" cellPadding="1" width="300"
							border="0">
							<TR>
								<TD vAlign="top"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 147px; HEIGHT: 116px">
						<TABLE id="Table3" style="WIDTH: 216px; HEIGHT: 48px" cellSpacing="1" cellPadding="1" width="216"
							border="0">
							<TR>
								<TD vAlign="top">Claim</TD>
								<td>
									<asp:label id="lblTitikClaim" runat="server">:</asp:label>
								</td>
								<td>
									<asp:literal id="ltrlClaimAmount" runat="server"></asp:literal><br>
								</td>
							</TR>
							<TR>
								<TD>PPN&nbsp;
								</TD>
								<td>
									<asp:label id="lblTitikPPN" runat="server">:</asp:label>
								</td>
								<td>
									<asp:literal id="ltrlVATTotal" runat="server" ></asp:literal>
								</td>
							</TR>
							<TR>
								<TD>PPh</TD>
								<td>
									<asp:label id="lblTitikPPh" runat="server">:</asp:label>&nbsp;
								</td>
								<td>
									<asp:literal id="ltrlPPHTotal" runat="server"></asp:literal><br>
								</td>
							</TR>
                            <TR>
								<TD>Total</TD>
								<td>
									<asp:label id="lblTitikTotal" runat="server">:</asp:label>&nbsp;
								</td>
								<td>
									<asp:literal id="ltrlTotalReceiptAmount" runat="server"></asp:literal><br>
								</td>
							</TR>
						</TABLE>
					</TD>
					<TD style="WIDTH: 6px; HEIGHT: 116px"></TD>
					<TD style="HEIGHT: 116px" vAlign="top" align="center">
						<asp:literal id="ltrlDealerPlace" Runat="server"></asp:literal><br>
						<asp:literal id="ltrlDealerName" Runat="server"></asp:literal><br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<asp:literal id="ltrlNameTTD" runat="server"></asp:literal><br>
						<hr width="150">
						<asp:literal id="ltrlJabatan" runat="server"></asp:literal><br>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 147px" align="left" colSpan="3"></TD>
				</TR>
				<TR>
					<TD align="left" colSpan="3">PERHATIAN :<BR>
						Kwitansi ini harap dikembalikan selambat-lambatnya 2 minggu setelah tanggal 
						cetak
						<asp:Label id="lblDateCetak" runat="server"></asp:Label></TD>
				</TR>
				<tr>
					<td align="center" colSpan="3"><asp:button class="hideButtonOnPrint" id="btnPrint" Runat="server" Text="Print" Width="72px"></asp:button><asp:button class="hideButtonOnPrint" id="btnKembali" runat="server" Text="Kembali"></asp:button></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
