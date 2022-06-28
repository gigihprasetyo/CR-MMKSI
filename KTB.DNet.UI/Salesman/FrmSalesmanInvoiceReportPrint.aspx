<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanInvoiceReportPrint.aspx.vb" Inherits="FrmSalesmanInvoiceReportPrint" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSalesmanInvoiceReportPrint</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 640px; POSITION: absolute; TOP: 8px; HEIGHT: 400px"
				cellSpacing="1" cellPadding="1" width="640" border="0">
				<TR>
					<TD colSpan="3" align="center"><STRONG>Kwitansi</STRONG></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 147px; HEIGHT: 28px">No Kwitansi</TD>
					<TD style="WIDTH: 6px; HEIGHT: 28px"><STRONG>:</STRONG></TD>
					<TD style="HEIGHT: 28px"><u>
							<asp:Literal id="ltrNoKwitansi" runat="server"></asp:Literal></u></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 147px; HEIGHT: 28px">Telah Terima Dari</TD>
					<TD style="WIDTH: 6px; HEIGHT: 28px"><STRONG>:</STRONG></TD>
					<TD style="HEIGHT: 28px"><u>PT Mitsubishi Motors Krama Yudha Sales Indonesia</u></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 147px; HEIGHT: 25px">Uang Sejumlah</TD>
					<TD style="WIDTH: 6px; HEIGHT: 25px"><STRONG>:</STRONG></TD>
					<TD style="HEIGHT: 25px">
						<u>
							<asp:Literal id="ltrlTotalTerbilang" runat="server"></asp:Literal></u></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 147px; HEIGHT: 27px">Untuk Pembayaran</TD>
					<TD style="WIDTH: 6px; HEIGHT: 27px"><STRONG>:</STRONG></TD>
					<TD style="HEIGHT: 27px">
						<u>
							<asp:Literal id="ltrlDescription" runat="server"></asp:Literal></u></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 147px; HEIGHT: 13px" colSpan="3">Catatan:</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 147px; HEIGHT: 50px" colSpan="3">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="300" border="0" style="WIDTH: 300px; HEIGHT: 41px">
							<TR>
								<TD valign="top">
									<asp:Literal id="ltrlNote" runat="server"></asp:Literal></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 147px; HEIGHT: 116px">
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="216" border="0" style="WIDTH: 216px; HEIGHT: 48px">
							<TR>
								<TD><hr>
									Rp.
									<asp:Literal id="ltrlTotalHarga" runat="server"></asp:Literal>
									<hr>
								</TD>
							</TR>
						</TABLE>
					</TD>
					<TD style="WIDTH: 6px; HEIGHT: 116px"></TD>
					<TD align="center" valign="top" style="HEIGHT: 116px">
						<asp:Literal id="ltrlDealerPlace" Runat="server"></asp:Literal>,
						<asp:Literal ID="ltrlCurrentDate" Runat="server"></asp:Literal><br>
						<asp:Literal ID="ltrlDealerName" Runat="server"></asp:Literal>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<hr width="150">
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 147px" colSpan="3" align="right"></TD>
				</TR>
				<tr>
					<td colspan="3" align="center"><asp:Button ID="btnPrint" Runat="server" Text="Print" Width="72px"></asp:Button></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
