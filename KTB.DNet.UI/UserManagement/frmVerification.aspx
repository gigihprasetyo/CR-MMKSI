<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmVerification.aspx.vb" Inherits="frmVerification" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>frmVerification</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="./WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="./WebResources/InputValidation.js"></script>
		<script language="javascript" src="./WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="/WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" valign="middle">
				<tr>
					<td vAlign="middle">
						<table height="380" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cc0001" border="0">
							<tr>
								<td width="40%">&nbsp;</td>
								<td width="20%" background="../images/login_bg.gif">
									<table height="386" cellSpacing="0" cellPadding="0" width="789" border="0">
										<tr height="74">
											<td width="612" height="74"><IMG src="../images/login_r1_c1.gif" border="0"></td>
											<td width="177"><IMG src="../images/new_login_r1_c2.gif" border="0"></td>
										</tr>
										<tr height="81">
											<td align="center" background="../images/login_bg.gif" colSpan="2" height="81">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<td align="left" width="25%"><IMG hspace="10" src="../images/new_mits.gif" border="0"></td>
														<td align="center" width="50%"><IMG src="../images/login_r2_wel.gif" border="0"></td>
														<td align="right" width="25%"><IMG hspace="10" src="../images/new_fuso.gif" border="0"></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr height="144">
											<td vAlign="middle" align="center" background="../images/login_bg.gif" colSpan="2" height="144">
												<table height="144" cellSpacing="0" cellPadding="0" width="787" align="center" border="0">
													<tr>
														<td width="25%" bgColor="#cccccc">&nbsp;</td>
														<td align="center" width="50%" bgColor="#cccccc">
															<TABLE id="Table1" cellSpacing="2" cellPadding="3" width="400" align="center" border="0">
																<tr>
																	<td align="center" class="titlePage" width="33%" height="16" colspan="2">Konfirmasi 
																		Verifikasi Kode Aktivasi</td>
																</tr>
																<TR>
																	<TD align="left" width="40%"><asp:label id="Label2" runat="server">User Name</asp:label></TD>
																	<TD align="left" width="60%">:
																		<asp:label id="lblUserName" runat="server">User Name</asp:label></TD>
																</TR>
																<TR>
																	<TD align="left"><asp:label id="Label3" runat="server">Dealer</asp:label></TD>
																	<TD align="left">:
																		<asp:label id="lblDealer" runat="server">Dealer</asp:label></TD>
																</TR>
																<TR>
																	<TD align="left"><asp:label id="Label1" runat="server">Registration Status</asp:label></TD>
																	<TD align="left">:
																		<asp:label id="lblRegStatus" runat="server">Registration Status</asp:label></TD>
																</TR>
																<TR>
																	<TD align="left"><asp:label id="Label4" runat="server">Activation Code</asp:label></TD>
																	<TD align="left">:
																		<asp:label id="lblActiveCode" runat="server">Activation Code</asp:label></TD>
																</TR>
																<TR>
																	<TD align="left"><asp:label id="Label5" runat="server">Activation Status</asp:label></TD>
																	<TD align="left">:
																		<asp:label id="lblActivStatus" runat="server">Activation Status</asp:label></TD>
																</TR>
																<TR>
																	<TD align="left"><asp:label id="Label6" runat="server">Serial Number</asp:label></TD>
																	<TD align="left">:
																		<asp:label id="lblSerialNumb" runat="server">Serial Number</asp:label></TD>
																</TR>
																<TR height="40">
																	<TD align="left"></TD>
																	<TD align="left"><asp:button id="btnMainPage" runat="server" Text="Halaman Utama"></asp:button></TD>
																</TR>
															</TABLE>
														<td vAlign="top" align="center" width="25%" bgColor="#cccccc">
															<table cellPadding="10" width="100%" border="0">
																<tr>
																	<td vAlign="top" align="center"></td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr height="23">
											<td background="../images/login_bg.gif" colSpan="2" height="23"><IMG height="1" src="../images/dot.gif" width="1" border="0"></td>
										</tr>
										<tr height="44">
											<td align="right" background="../images/login_bg.gif">
												<table cellSpacing="1" width="70%" border="0">
													<tr>
														<td align="center"><IMG src="../images/new_logo_login3.gif" border="0"></td>
													</tr>
												</table>
											</td>
											<td align="right"></td>
										</tr>
										<tr height="20">
											<td colSpan="2" height="20"><IMG src="../images/login_r4.gif" border="0"></td>
										</tr>
									</table>
								</td>
								<td width="40%">&nbsp;</td>
							</tr>
						</table>
						<table height="80" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr vAlign="top">
								<td width="40%">&nbsp;</td>
								<td align="center" width="20%"><B>Tampilan Terbaik IE 6.0+ : 1024 x 768</B>
									<br>
									<table height="60" cellSpacing="0" cellPadding="0" width="789" border="0">
										<tr>
											<td align="center">© 2006, PT Mitsubishi Motors Krama Yudha Sales Indonesia, Hak Cipta 
												Dilindungi Undang-Undang.<br>
												Jl. Jend A. Yani Proyek Pulomas, Jakarta.
												<br>
												Help Desk D-NET : (021) 4786-7575 atau <A class="menuLeft" href="mailto:admin.d-net@mitsubishi-motors.co.id">
													admin.d-net@mitsubishi-motors.co.id</A>
												<br>
												<asp:LinkButton ID="LnkTerms" runat="server">Syarat dan Ketentuan</asp:LinkButton>
											</td>
										</tr>
									</table>
								</td>
								<td width="40%">&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
