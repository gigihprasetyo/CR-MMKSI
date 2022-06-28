<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDisplayPersonalEvaluation.aspx.vb" Inherits="FrmDisplayPersonalEvaluation" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDisplayPersonalEvaluation</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		function cetak()
		{
		window.print();
		}
		
		
			
		</script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; POSITION: absolute; WIDTH: 625px; HEIGHT: 344px; TOP: 0px; LEFT: 0px"
				cellSpacing="0" cellPadding="0" width="625" border="0">
				<TBODY>
					<tr>
						<td class="titlePage" style="HEIGHT: 18px" bgColor="white"><img src="../images/header_email.gif"></td>
					</tr>
					<tr>
						<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
					</tr>
					<tr>
						<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
					</tr>
					<tr>
						<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
					</tr>
					<tr>
						<td style="HEIGHT: 3px" height="3"></td>
					</tr>
					<TR>
						<TD style="HEIGHT: 19px" align="center">
							<P><b style="Z-INDEX: 0">MMKSI Marketing Division</b></P>
						</TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 19px" align="center"><b style="Z-INDEX: 0">MMKSI Network Department</b></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 8px" align="center">&nbsp;<FONT size="3"></FONT></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 20px" bgColor="lightgrey" align="center" vAlign="middle">&nbsp;&nbsp;<B>EVALUASI 
								HASIL TRAINING ( E H T )</B></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 19px" align="center"></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 57px" align="center">
							<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
								<tr>
									<TD class="titleField" width="30">&nbsp;</TD>
									<td align="center">
										<table width="100%" bordercolor="#666666" border="1" cellpadding="2" cellspacing="0">
											<tr>
												<TD class="titleField" width="15%">Nama</TD>
												<TD width="50%" align="center"><asp:label id="lblTraineeName" runat="server"></asp:label></TD>
												<TD class="titleField" width="15%">No. Reg</TD>
												<TD width="20%" align="center"><asp:label id="lblRegNo" runat="server"></asp:label></TD>
											</tr>
											<tr valign="top">
												<TD class="titleField">Dealer</TD>
												<TD align="center"><asp:label id="lblDealerName" runat="server"></asp:label></TD>
												<TD class="titleField">Kota</TD>
												<td align="center"><asp:label id="lblCityName" runat="server"></asp:label></td>
											</tr>
											<tr>
												<TD class="titleField">Kelas</TD>
												<TD align="center"><asp:label id="lblClassName" runat="server"></asp:label></TD>
												<TD class="titleField">Kode Kelas</TD>
												<TD align="center"><asp:label id="lblClassCode" runat="server"></asp:label></TD>
											</tr>
											<TR>
												<TD class="titleField">Periode</TD>
												<TD align="center">
													<asp:Label id="lblStartPeriod" runat="server"></asp:Label></TD>
												<TD class="titleField">Sampai</TD>
												<TD align="center">
													<asp:Label id="lblEndPeriod" runat="server"></asp:Label></TD>
											</TR>
										</table>
									</td>
									<TD class="titleField" width="30">&nbsp;</TD>
								</tr>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 15px" vAlign="top" align="center"></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 20px" vAlign="middle" bgColor="lightgrey" align="center"><b>&nbsp;&nbsp;1. 
								NILAI/STATUS AKHIR</b></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 14px" vAlign="top"></TD>
					</TR>
					<TR>
						<TD>
							<table width="100%" cellpadding="2" border="0">
								<tr>
									<td width="30">&nbsp;</td>
									<td WIDTH="21%"><b>Test Initial</b></td>
									<td width="100" align="right"><asp:label id="lblInitial" Runat="server"></asp:label></td>
									<td align="center">&nbsp;</td>
									<td width="5%">&nbsp;</td>
								</tr>
								<asp:repeater id="rptTest" Runat="server">
									<ItemTemplate>
										<tr>
											<td width="30"></td>
											<td><b>
													<asp:Label Runat="server" ID="lblKeteranganTest"></asp:Label></b></td>
											<td align="right">
												<asp:Label Runat="server" ID="lblNilaiTest"></asp:Label></td>
											<td align="center">&nbsp;</td>
											<td width="30">&nbsp</td>
										</tr>
									</ItemTemplate>
								</asp:repeater>
								<tr>
									<td width="30"></td>
									<td><b>Tes Final</b></td>
									<td align="right"><asp:label id="lblFinal" Runat="server"></asp:label></td>
									<td align="center">&nbsp;</td>
									<td width="30"></td>
								</tr>
								<tr>
									<td width="30" style="HEIGHT: 18px"></td>
									<td style="HEIGHT: 18px"><b>Average</b></td>
									<td align="right" style="HEIGHT: 18px"><asp:label id="lblAverage" Runat="server"></asp:label></td>
									<td align="left" style="HEIGHT: 18px"><EM>( diluar initial tes )</EM>&nbsp;</td>
									<td width="30" style="HEIGHT: 18px"></td>
								</tr>
								<tr>
									<td width="30"></td>
									<td><b>Ranking ke / dari</b></td>
									<td align="right"><asp:label id="lblRangking" Runat="server"></asp:label></td>
									<td align="center">&nbsp;</td>
									<td width="30"></td>
								</tr>
								<tr>
									<td width="30"></td>
									<td><b>Status</b></td>
									<td align="right"><asp:label id="lblStatus" Runat="server"></asp:label></td>
									<td align="center">&nbsp;</td>
									<td width="30"></td>
								</tr>
							</table>
						</TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 16px" align="center"></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 20px" vAlign="middle" bgColor="lightgrey" align="center"><b>&nbsp;&nbsp;2. 
								SIKAP SELAMA TRAINING DAN LAIN-LAIN</b></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 2px"></TD>
					</TR>
					<tr>
						<td width="625">
							<table style="WIDTH: 100%" border="0">
								<TBODY>
									<tr>
										<td width="30">&nbsp;
										</td>
										<td vAlign="top">
											<table width="100%" border="0">
												<asp:repeater id="rptPenilaianSikap" Runat="server">
													<ItemTemplate>
														<tr>
															<td WIDTH="38%">
																<b>
																	<asp:Label Runat="server" ID="lblKeterangan"></asp:Label></b>
															</td>
															<td width="62%" align="center">
																<asp:Label Runat="server" ID="lblNilai"></asp:Label>
															</td>
														</tr>
													</ItemTemplate>
												</asp:repeater></table>
										</td>
										<td vAlign="top" width="100"><STRONG>Skala Penilaian : </STRONG>
										</td>
										<TD vAlign="top" width="100">
											A: Sempurna<br>
											B: Baik<br>
											C: Cukup<br>
											D: Kurang<br>
											E:&nbsp;Buruk<br>
										</TD>
										<td width="30"></td>
									</tr>
								</TBODY>
							</table>
						</td>
					</tr>
					<TR>
						<TD style="HEIGHT: 10px" align="center"></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 20px" bgColor="lightgrey" vAlign="middle" align="center"><b>&nbsp;&nbsp;3. 
								REKOMENDASI</b></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 32px">
							<table style="WIDTH: 100%">
								<TBODY>
									<tr>
										<td width="30">&nbsp;
										</td>
										<td><b><asp:label id="lblRekomendasi" runat="server"></asp:label></b></td>
										<td width="30">&nbsp;</td>
									</tr>
								</TBODY>
							</table>
						</TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 21px" align="center"></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 20px" bgColor="lightgrey" vAlign="middle" align="center"><b>&nbsp;&nbsp;4. 
								CATATAN</b></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 22px">
							<table style="WIDTH: 100%">
								<TBODY>
									<tr>
										<td width="30">&nbsp;
										</td>
										<td><b><asp:label id="lblCatatan" runat="server"></asp:label></b></td>
										<td width="30">&nbsp;</td>
									</tr>
								</TBODY>
							</table>
						</TD>
					</TR>
					<TR>
						<td></td>
					</TR>
					<TR>
						<TD style="HEIGHT: 22px"></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 22px"></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 20px">
							<table style="WIDTH: 625px" border="0">
								<TBODY>
									<tr>
										<td width="30" style="HEIGHT: 16px">&nbsp;
										</td>
										<td style="HEIGHT: 16px"><b><asp:label id="lblLokasi" runat="server"></asp:label>,&nbsp;<asp:label id="lblTanggal" runat="server"></asp:label></b></td>
										<td style="HEIGHT: 16px">&nbsp;</td>
										<td width="30" style="HEIGHT: 16px">&nbsp;</td>
									</tr>
									<tr>
										<td width="30">&nbsp;
										</td>
										<td><b>Mengetahui,</b></td>
										<td align="right"><b>Dilaporkan oleh,</b></td>
										<td width="30">&nbsp;</td>
									</tr>
									<tr>
										<td width="30">&nbsp;
										</td>
										<td><asp:label id="lblManagerName" runat="server"></asp:label></td>
										<td align="right"><asp:Label Runat="server" ID="lblReporter"></asp:Label></td>
										<td width="30">&nbsp;</td>
									</tr>
									<tr>
										<td width="30">&nbsp;
										</td>
										<td></td>
										<td align="right"></td>
										<td width="30">&nbsp;</td>
									</tr>
								</TBODY>
							</table>
						</TD>
					<TR>
						<TD style="HEIGHT: 22px"><asp:button class="hideButtonOnPrint" id="btnBack" runat="server" Text="Kembali"></asp:button><INPUT class="hideButtonOnPrint" id="btnCetak" style="WIDTH: 56px; HEIGHT: 21px" onclick="cetak()"
								type="button" value="Cetak"></TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
	</body>
</HTML>
