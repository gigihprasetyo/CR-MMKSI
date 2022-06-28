<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpSuratParkir.aspx.vb" Inherits="PopUpSuratParkir" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Surat Penalti Parkir</title>
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
			function PrintDocument()
			{			
				window.print();			
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 640px; POSITION: absolute; TOP: 8px; HEIGHT: 400px"
				cellSpacing="1" cellPadding="1" width="640" border="0">
				<TBODY>
					<TR>
						<TD align="left"><IMG hspace="30" height="90" width="90" src="../images/mits_logo90.jpg" border="0"></TD>
						<TD align="right"><%--<IMG hspace="30" height="90" width="90" src="../images/fuso_logo90.jpg" border="0">--%></TD>
					</TR>
					<tr>
						<td colspan="2" width="100%"><br>
						</td>
					</tr>
					<TR>
						<TD align="left">No.<asp:label id="lblLetterNumber" runat="server"></asp:label></TD>
						<TD align="right"><asp:label id="lblCreatedTime" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD align="left" colSpan="2"><asp:label id="lblDealerName" runat="server" Font-Size="12" Font-Bold="True"></asp:label></TD>
					</TR>
					<TR>
						<TD align="left" colSpan="2"><asp:label id="lblAlamat" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD align="left" colSpan="2"><asp:label id="lblKota" runat="server"></asp:label>
							<br>
							<br>
						</TD>
					</TR>
					<TR>
						<TD align="left" colSpan="2"><asp:label id="lblContactPerson" runat="server" Font-Size="10" Font-Bold="True"></asp:label>
							<br>
							<br>
						</TD>
					</TR>
					<TR>
						<TD align="center" colSpan="2">
							<asp:label id="lblHeader1" runat="server" Font-Size="12" Font-Bold="True" Font-Underline="True">Perihal : Penalti Parkir</asp:label>
							<asp:label id="lblHeader2" runat="server" Font-Size="12" Font-Bold="True" Font-Underline="True"></asp:label>
							<br>
							<br>
						</TD>
					</TR>
					<TR>
						<TD align="left" colSpan="2">Dengan hormat,<BR>
							<BR>
							&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Dengan ini kami beritahukan Penalti parkir 
							di Car Pool PT Mitsubishi Motors Krama Yudha Sales Indonesia ("PT.MMKSI") untuk periode "
							<asp:label ID="lblPeriode" Runat="server"></asp:label>
							" dengan rincian sebagai berikut :<br>
						</TD>
					</TR>
					<tr>
						<td colspan="2" width="100%"><br>
						</td>
					</tr>
					<tr>
						<td colspan="2" align="center">
							<asp:Literal ID="ltrTable" Runat="server"></asp:Literal>
						</td>
					</tr>
					<TR>
						<TD align="left" colSpan="2"><br>
							&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Total pembayaran Penalti Parkir sebesar 
							&nbsp;<asp:label ID="lblBiayaTerbilang" Runat="server"></asp:label>&nbsp; 
							tersebut, telah kami potong dari Deposit A milik Dealer di PT MMKSI.<br>
						</TD>
					</TR>
					<tr>
						<td colspan="2" width="100%"><br>
						</td>
					</tr>
					<TR>
						<TD align="left" colSpan="2" class="titleField">
							&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Perlu diperhatikan bahwa pada saat Dealer 
							melakukan pengisian Bukti Pemotongan PPh Pasal 23 tersebut, agar mencantumkan 
							No. Debit Charge. Setelah di isi, maka diharapkan agar Form Bukti Pemotongan 
							PPh Pasal 23 tersebut dikirimkan segera ke Whole Sales Dept. - Delivery Section 
							PT MMKSI (Up. Ayu Poppy R).<br>
						</TD>
					</TR>
					<tr>
						<td colspan="2" width="100%"><br>
						</td>
					</tr>
					<TR>
						<TD align="left" colSpan="2">
							&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Demikian surat ini kami sampaikan, atas 
							perhatiannya kami ucapkan terima kasih.<br>
							<br>
						</TD>
					</TR>
					<TR>
						<TD align="left" colSpan="2">
							Catatan :<br>
						</TD>
					</TR>
					<TR>
						<TD align="left" colSpan="2">
							<table>
								<tr>
									<td valign="top">a.</td>
									<td>Dokumen ini diproses secara otomatis oleh sistem dan buat dalam bentuk dokumen 
										elektronik serta merupakan alat bukti yang sah, baik yang ditampilkan dalam 
										sistem maupun hasil cetakannya. Oleh karenanya tidak memerlukan tandatangan 
										maupun percetakan secara manual (dokumen hard copy).</td>
								</tr>
							</table>
						</TD>
					</TR>
					<tr>
						<td colspan="2" width="100%" align="center"><br>
						</td>
					</tr>
					<tr>
						<td colspan="2" width="100%" align="center"><br>
						</td>
					</tr>
					<tr>
						<td colspan="2" width="100%" align="center">
							<IMG height="129" width="672" src="../images/ktb_footer.gif" border="0">
						</td>
					</tr>
					<tr>
						<td align="center" colSpan="2">
							<asp:button class="hideButtonOnPrint" id="btnPrint" Runat="server" Text="Print" Width="72px"></asp:button>
							<INPUT id="btnClose" class="hideButtonOnPrint" style="WIDTH: 60px" onclick="window.close()"
								type="button" value="Tutup" name="btnClose"></td>
						</TD>
					</tr>
				</TBODY>
			</TABLE>
		</form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM> 
		<!-- 
					<TR>
						<TD align="left" colSpan="2">
							<table>
								<tr>
									<td valign="top">a.</td>
									<td>Dokumen ini merupakan bagian yang tidak terpisahkan dari Perjanjian Jual Beli 
										Kendaraan antara Dealer dan PT MMKSI, Akta No. 158 yang dibuat oleh dan 
										dihadapan Sri Ismiyati, SH, MKn, Notaris di Jakarta Utara, pada tanggal 14 
										Januari 2011, termasuk setiap perubahan dan/atau pembaharuannya.</td>
								</tr>
							</table>
						</TD>
					</TR>

-->
	</body>
</HTML>
