<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmCRFPrint.aspx.vb" Inherits="FrmCRFPrint"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmCRFPrint</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script>
			function PrintDocument(){
				document.getElementById("btnCetak").style.visibility="hidden"
				document.getElementById("btnKembali").style.visibility="hidden"
				document.body.style.zoom=1.25;
				window.print();
				document.body.style.zoom=1.;
				document.getElementById("btnCetak").style.visibility="visible"
				document.getElementById("btnKembali").style.visibility="visible"
			}			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE style="WIDTH: 784px; HEIGHT: 564px" id="Table1" border="1" cellSpacing="2" cellPadding="2"
				width="784">
				<tr>
					<td colSpan="3">&nbsp;&nbsp;
						<asp:label style="Z-INDEX: 0" id="Label1" runat="server" Width="688px"></asp:label><IMG src="../images/ktb_logo_full.jpg" runat="server">
					</td>
				</tr>
				<tr>
					<td colSpan="3" align="center">
						<h4><u>TANDA TERIMA SEMENTARA</u><br>
							TEMPORARY RECEIPT
						</h4>
					</td>
				</tr>
				<tr>
					<td style="HEIGHT: 313px" borderColor="black" colSpan="3"><table border="0" width="100%">
							<TBODY>
								<tr>
									<td style="WIDTH: 191px"><STRONG>Receipt No.</STRONG></td>
									<td style="WIDTH: 1px">:</td>
									<td><asp:label style="Z-INDEX: 0" id="lblReceiptNo" runat="server" Width="552px"></asp:label></td>
								</tr>
								<tr>
									<td style="WIDTH: 191px"><STRONG>Received From</STRONG></td>
									<td style="WIDTH: 1px">:</td>
									<td><asp:label style="Z-INDEX: 0" id="lblReceivedFrom" runat="server" Width="552px"></asp:label></td>
								</tr>
								<tr>
									<td style="WIDTH: 191px; HEIGHT: 15px"><STRONG>The Sum Of</STRONG></td>
									<td style="WIDTH: 1px; HEIGHT: 15px">:</td>
									<td style="HEIGHT: 15px"><asp:label style="Z-INDEX: 0" id="lblSumOf" runat="server" Width="552px"></asp:label></td>
								</tr>
								<tr>
									<td style="WIDTH: 191px"><STRONG>Being Payment Of</STRONG></td>
									<td style="WIDTH: 1px">:</td>
									<td><asp:label style="Z-INDEX: 0" id="lblPaymentOf" runat="server" Width="552px"></asp:label></td>
								</tr>
								<TR>
									<TD style="WIDTH: 191px"><STRONG>Amount/Currency</STRONG></TD>
									<TD style="WIDTH: 1px">:</TD>
									<TD><asp:label style="Z-INDEX: 0" id="lblAmount" runat="server" Width="552px"></asp:label></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 191px"><STRONG>Giro Number</STRONG></TD>
									<TD style="WIDTH: 1px">:</TD>
									<TD><asp:label style="Z-INDEX: 0" id="lblGyroNo" runat="server" Width="552px"></asp:label></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 191px"><STRONG>Value Date&nbsp;</STRONG></TD>
									<TD style="WIDTH: 1px">:</TD>
									<TD><asp:label style="Z-INDEX: 0" id="lblValueDate" runat="server" Width="552px"></asp:label></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 191px; HEIGHT: 16px" valign="top"><STRONG>Sales Order</STRONG></TD>
									<TD style="WIDTH: 1px; HEIGHT: 16px" valign="top">:</TD>
									<TD style="HEIGHT: 16px"><asp:label style="Z-INDEX: 0" id="lblSONumber" runat="server" Width="552px"></asp:label></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 191px"></TD>
									<TD style="WIDTH: 1px"></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 191px">Created By</TD>
									<TD style="WIDTH: 1px">:</TD>
									<TD><asp:label style="Z-INDEX: 0" id="lblCreatedBy" runat="server" Width="552px"></asp:label></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 191px">Date</TD>
									<TD style="WIDTH: 1px">:</TD>
									<TD><asp:label style="Z-INDEX: 0" id="lblCreatedDate" runat="server" Width="552px"></asp:label></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 191px"></TD>
									<TD style="WIDTH: 1px"></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 191px"><EM><FONT color="#0000ff">Catatan:</FONT></EM></TD>
									<TD style="WIDTH: 1px"></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD colSpan="3">
										<table style="FONT-STYLE: italic" border="0" cellSpacing="0" cellPadding="0" width="100%">
											<tr>
												<td style="WIDTH: 15px; HEIGHT: 15px; COLOR: #0000ff" vAlign="top">1.</td>
												<td style="HEIGHT: 15px; COLOR: #0000ff"><asp:label style="Z-INDEX: 0" id="lblRemark1" runat="server" Width="608px"></asp:label></td>
											</tr>
											<tr>
												<td style="WIDTH: 15px; HEIGHT: 14px; COLOR: #0000ff" vAlign="top">2.</td>
												<td style="HEIGHT: 14px; COLOR: #0000ff"><asp:label style="Z-INDEX: 0" id="lblRemark2" runat="server" Width="608px"></asp:label></td>
											</tr>
											<tr>
												<td colSpan="2" align="center">
													<table border="1" cellSpacing="2" cellPadding="2" width="100%">
														<tr>
															<td style="WIDTH: 397px" bgColor="white" borderColor="black" colspan="2">"Payment by cheques or 
																gyro only valid upon clearence"
															</td>
														</tr>
													</table>
													<input style="WIDTH: 74px; HEIGHT: 21px" id="btnCetak" onclick="PrintDocument();" value="Cetak"
														type="button">
													<asp:button id="btnKembali" Width="60px" Text="Kembali" Runat="server"></asp:button></td>
											</tr>
										</table>
									</TD>
								</TR>
							</TBODY>
						</table>
					</td>
				</tr>
				<TR>
					<TD colSpan="3" align="center"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 191px"></TD>
					<TD style="WIDTH: 1px"></TD>
					<TD></TD>
				</TR>
			</TABLE>
			</TD></TR><TR>
				<TD></TD>
				<TD></TD>
				<TD></TD>
			</TR>
			<TR>
				<TD></TD>
				<TD></TD>
				<TD></TD>
			</TR>
			<TR>
				<TD></TD>
				<TD></TD>
				<TD></TD>
			</TR>
			</TBODY></TABLE></form>
		<SCRIPT language="javascript">
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
		</SCRIPT>
	</body>
</HTML>
