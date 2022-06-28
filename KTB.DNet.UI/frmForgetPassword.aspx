<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmForgetPassword.aspx.vb" Inherits="frmForgetPassword" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Lupa Kata Kunci</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="/WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="./WebResources/PreventNewWindow.js"></script>
		<script language="javascript">
		function OpenFullScreenWindow(Address)
			{
				//if(document.location.search=='')  {
						
				newwin=window.open(Address,"_blank", "fullscreen=no,titlebar=no,personalbar=no,toolbar=no,status=1,menubar=no,scrollbars=yes,resizable=yes,directories=no,location=no");
				self.opener = null;
				self.close();
					
				//}
			}
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="FormLogin" method="post" width="100%" runat="server">
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
											<td align="center" background="images/login_bg.gif" colSpan="2" height="81">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<%--<td align="left" width="25%"><IMG hspace="10" src="images/new_mits.gif" border="0"></td>--%>
														<td align="left" width="75%" style="padding:14px"><h2><strong>Verifikasi Kata Kunci</strong></h2></td>
														<td align="right" width="25%"><IMG hspace="10" src="images/new_fuso.gif" border="0"></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr height="144">
											<td vAlign="middle" align="center" background="images/login_bg.gif" colSpan="2" height="144">
												<table height="144" cellSpacing="0" cellPadding="0" width="787" align="center" border="0">
													<tr>
														<td width="25%" bgColor="#cccccc">&nbsp;</td>
														<td width="50%" align="center" bgColor="#cccccc">
															<table height="160" cellSpacing="1" width="460" align="center" border="0">
																<tr>
																	<td class="titlePage" colSpan="3">&nbsp;</td>
																</tr>
																<tr>
																	<td height="100">
																		<table cellSpacing="1" width="450" align="center" border="0">
																			<tr>
																				<td class="welcomeLogin" width="160">Kode Organisasi</td>
																				<td class="welcomeLogin" width="240">:
																					<asp:Label id="lblKodeDealer" runat="server"></asp:Label></td>
																				<td class="welcomeLogin" width="50"></td>
																			</tr>
																			<tr>
																				<td class="welcomeLogin">Nama</td>
																				<td style="HEIGHT: 16px">:
																					<asp:Label id="lblUserID" runat="server"></asp:Label></td>
																				<td style="HEIGHT: 16px"></td>
																			</tr>
																			<TR>
																				<TD align="center" colSpan="3">&nbsp;</TD>
																			</TR>
																			<TR>
																				<TD class="welcomeLogin" width="160"><asp:label id="lblQuesFavorit" runat="server" Width="240px">Pertanyaan Favorit</asp:label></TD>
																				<TD style="HEIGHT: 17px" colSpan="2">:
																					<asp:textbox id="txtAnsFavorit" runat="server" Width="185px" TextMode="Password" MaxLength="100"></asp:textbox>
																					<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtAnsFavorit" ErrorMessage="*"></asp:RequiredFieldValidator></TD>
																			</TR>
																			<TR>
																				<TD class="welcomeLogin" nowrap>Kata Kunci  baru akan dikirim ke</TD>
																				<TD>:<asp:checkbox id="chkHandPhone" runat="server" Text="HandPhone" Checked="True"></asp:checkbox></TD>
																				<TD><asp:checkbox id="chkEmail" runat="server" Text="Email"></asp:checkbox></TD>
																			</TR>
																		</table>
																	</td>
																</tr>
																<TR>
																	<TD height="60">&nbsp;</TD>
																</TR>
															</table>
														</td>
														<td align="center" valign="top" width="25%" bgColor="#cccccc">
														<br>
														<table border="0" width="100%" cellpadding="10">
																<tr>
																	<td align="center" valign="top"><asp:Image id="imgPhisingGuard" runat="server" Width="70px" Height="70px"></asp:Image><Br>Pilihan Gambar 2</td>
																</tr>																
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
														<td align="center"> &nbsp;</td>
													</tr>
												</table>
											</td>
											<td align="right">
												<table cellSpacing="1" width="80%" border="0">
													<tr>
														<td align="center"><asp:button id="btnVerify" runat="server" Text="Verifikasi" Width="76px" Enabled="False"></asp:button><br>
															<asp:linkbutton id="HyperLink1" runat="server" CausesValidation="False">Kembali ke halaman Login</asp:linkbutton></td>
													</tr>
												</table>
											</td>
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
