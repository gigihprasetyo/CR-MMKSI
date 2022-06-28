<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmForgetPasswordConfirmation.aspx.vb" Inherits="frmForgetPasswordConfirmation" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>frmForgetPasswordConfirmation</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="/WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="./WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="./WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
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
											<td align="center" background="images/login_bg.gif" colSpan="2" height="94">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<%--<td align="left" width="25%"><IMG hspace="10" src="images/new_mits.gif" border="0"></td>--%>
														<td align="left" width="75%" style="padding-left:14px;"><h2><strong>
																	<asp:Label id="lblKonfirmasiHeader" runat="server"></asp:Label></strong></h2>
														</td>
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
															<br>
															<table height="160" cellSpacing="1" width="460" align="center" border="0">
																<tr>
																	<td>&nbsp;</td>
																</tr>
																<tr>
																	<td height="100">
																		<table border="0" align="center">
																			<tr>
																				<td align="center">
																					<asp:Label id="lblConfirmationMessage" runat="server"></asp:Label>
																				</td>
																			</tr>
																			<tr>
																				<td>
																					&nbsp;
																				</td>
																			</tr>
																			<tr>
																				<td align="center">
																					<asp:LinkButton id="LinkButton1" runat="server">Kembali Ke Halaman Login</asp:LinkButton>
																				</td>
																			</tr>
																		</table>
																	</td>
																</tr>
																<TR>
																	<TD height="60">&nbsp;</TD>
																</TR>
															</table>
														</td>
														<td align="center" valign="top" width="25%" bgColor="#cccccc"><br>
															<br>
															<asp:Image id="ImgPC" runat="server" Width="70px" Height="70px"></asp:Image>&nbsp;</td>
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
														<td align="center">&nbsp;</td>
													</tr>
												</table>
											</td>
											<td align="right">
												&nbsp;
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
