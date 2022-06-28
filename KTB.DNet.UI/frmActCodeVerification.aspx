<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmActCodeVerification.aspx.vb" Inherits="frmActCodeVerification" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Halaman Verifikasi Kode Aktivasi</title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<LINK href="WebResources/stylesheet.css" type="text/css" rel="stylesheet">
			<LINK href="/WebResources/stylesheet.css" type="text/css" rel="stylesheet">
				<script language="javascript" src="./WebResources/PreventNewWindow.js"></script>
				<script language="javascript" src="./WebResources/InputValidation.js"></script>
				<script language="javascript">
			
		 
			
			function OpenFullScreenWindow(Address)
			{
			       newwin=window.open(Address,"_blank", "fullscreen=no,titlebar=no,personalbar=no,toolbar=no,status=1,menubar=no,scrollbars=yes,resizable=yes,directories=no,location=no");
			  	   this.name = "origWin";
				   origWin= window.open("login.aspx", "origWin");
				   window.opener = top;
                   window.close();              
			}
			
				</script>
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="FormLogin" method="post" runat="server" width="100%">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" valign="middle">
				<tr>
					<td vAlign="middle">
						<table height="380" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cc0001" border="0" style=" background-image:url(images/bg-diamond-red.jpg);   background-position:center top;  ">
							<tr>
								<td width="40%">&nbsp;</td>
								<td width="20%" background="images/login_bg.gif">
									<table height="386" cellSpacing="0" cellPadding="0" width="789" border="0">
										<tr height="74">
											<td width="612" height="74"><IMG src="images/login_r1_c1.gif" border="0"></td>
											<td width="177"><IMG src="images/new_login_r1_c2.gif" border="0"></td>
										</tr>
										<tr height="81">
											<td align="center" background="images/login_bg.gif" colSpan="2" height="95">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<%--<td align="left" width="25%"><IMG hspace="10" src="images/new_mits.gif" border="0"></td>--%>
														<td align="center" width="75%">
														<span style="padding-left:14px">	<h1><STRONG>Verifikasi Kode Aktivasi</STRONG>
															</h1></span>
														</td>
														<td align="right" width="25%"><IMG hspace="10" src="images/new_mits.gif" border="0"></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr height="144">
											<td vAlign="middle" align="center" background="images/login_bg.gif" colSpan="2" height="144">
												<table height="144" cellSpacing="0" cellPadding="0" width="787" align="center" border="0">
													<tr>
														<td align="center" bgColor="#cccccc">
															<table height="160" cellSpacing="1" width="460" align="center" border="0">
																<tr>
																	<td class="titlePage" align="center" colSpan="3"></td>
																</tr>
																<tr>
																	<td height="100" align="center">
																		<table cellSpacing="1" width="460" align="center" border="0">
																			<tr>
																				<td class="welcomeLogin" width="33%" height="16">&nbsp;</td>
																				<td class="titlePage" align="center" width="33%" height="16">Kode Aktivasi</td>
																				<td class="welcomeLogin" width="33%" height="16"></td>
																			</tr>
																			<TR>
																				<TD colspan="3" class="welcomeLogin" align="center" width="33%" height="15">
																					<asp:Label id="Label1" runat="server">Maaf, no HP Anda sudah terdaftar 
																					di dalam sistem, silahkan masukan kode 
																					aktivasi yang telah Anda miliki, atau tekan 
																					tombol kembali untuk mengubah isian no HP Anda </asp:Label></td>
																			</tr><tr>
																				<td></td>
																				<td><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtKodeAktivasi" runat="server" Width="130px"
																						CssClass="welcomeLogin"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtKodeAktivasi"
																						ErrorMessage="*"></asp:requiredfieldvalidator></TD>
																				<TD class="welcomeLogin" align="center" width="33%" height="15"><asp:image id="MyImage" runat="server" Width="70px" Height="70px"></asp:image></TD>
																			</tr>
																			<tr>
																				<td height="25"></td>
																				<td height="25"></td>
																				<td height="25"></td>
																			<TR>
																				<TD height="25"></TD>
																				<TD align="center" height="25"><asp:button id="btnProsess" runat="server" Width="56px" Text="Proses"></asp:button>&nbsp;
																					<asp:button id="btnKembali" CausesValidation="False" runat="server" Width="56px" Text="Kembali"></asp:button></TD>
																				<TD height="25"></TD>
																			</TR>
																			<TR>
																				<TD height="25"></TD>
																				<TD align="center" height="25"></TD>
																				<TD height="25"></TD>
																			</TR>
																		</table>
																		<asp:linkbutton id="LinkButton1" runat="server" CausesValidation="False">Kembali Ke Halaman Login</asp:linkbutton>
																	</td>
																</tr>
																<TR>
																	<TD height="60">&nbsp;</TD>
																</TR>
															</table>
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr height="23">
											<td background="images/login_bg.gif" colSpan="2" height="23"><IMG height="1" src="images/dot.gif" width="1" border="0"></td>
										</tr>
										<tr height="44">
											<td align="right" background="images/login_bg.gif">
												<table cellSpacing="1" width="70%" border="0">
													<tr>
														<td align="center"> 
                                                            <IMG src="images/new_logo_login3.gif" border="0">


														</td>
													</tr>
												</table>
											</td>
											<td align="right"></td>
										</tr>
										<tr height="20">
											<td colSpan="2" height="20"><IMG src="images/login_r4.gif" border="0"></td>
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
											<td align="center">PT Mitsubishi Motors Krama Yudha Sales Indonesia, Hak Cipta 
												Dilindungi Undang-Undang.<br>
												Jl. Jend A. Yani Proyek Pulomas, Jakarta.
												<br>
												Help Desk D-NET : (021) 4786-7575 atau <A class="menuLeft" href="mailto:admin.d-net@mitsubishi-motors.co.id">
													admin.d-net@mitsubishi-motors.co.id</A>
												<br>
												<A onclick="window.open('eula2.html','disclaimer','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=yes,width=700,height=500'); return false;"
													href="#javascript">Syarat dan Ketentuan</A>
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
