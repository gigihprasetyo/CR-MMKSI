<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmSecondAuth.aspx.vb" Inherits="frmSecondAuth" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Halaman Login Kedua</title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<LINK href="WebResources/BasicStyle.css" type="text/css" rel="stylesheet">
			<script language="javascript">

			function alphaNumericWith(event, addKey)
			{
				var pressedKey
				
				if(navigator.appName == "Microsoft Internet Explorer")	
					pressedKey = event.keyCode;
				else
				{
					pressedKey = event.charCode;
				}					
				if ( (isAccepted(pressedKey,addKey)) ||(pressedKey >=48 && pressedKey<=57) || (pressedKey >=97 && pressedKey <=122) || (pressedKey >=65 && pressedKey <=90) || (pressedKey == 0) )
				{
					return true;
				}
				else
					return false;
			}

			document.oncontextmenu=new Function("return false");
			
			</script>
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="FormLogin" method="post" width="100%" runat="server">
			<table id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0"
				valign="middle">
				<tr>
					<td vAlign="middle">
						<table id="Table2" height="380" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cc0001" style=" background-image:url(images/bg-diamond-red.jpg);   background-position:center top;  "
							border="0">
							<tr>
								<td width="40%">&nbsp;</td>
								<td width="20%" background="images/login_bg.gif">
									<table id="Table3" height="386" cellSpacing="0" cellPadding="0" width="789" border="0">
										<tr height="74">
											<td width="612" height="74"><IMG src="images/login_r1_c1.gif" border="0"></td>
											<td width="177"><IMG src="images/new_login_r1_c2.gif" border="0"></td>
										</tr>
										<tr height="81">
											<td align="center" background="images/login_bg.gif" colSpan="2" height="81">
												<table id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<%--<td align="left" width="25%"><IMG hspace="10" src="images/new_mits.gif" border="0"></td>--%>
														<td vAlign="middle" align="left" width="75%" style="margin-top:14px;padding-top:10px;padding-left:14px;">
													 	<h2><STRONG>Otentikasi Kedua</STRONG></h2>
														</td>
														<td align="right" width="25%"><IMG hspace="10" src="images/new_mits.gif" border="0"></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr height="144">
											<td vAlign="middle" align="center" background="images/login_bg.gif" colSpan="2" height="144">
												<table id="Table5" height="144" cellSpacing="0" cellPadding="0" width="787" align="center"
													border="0">
													<tr>
														<td align="center" bgColor="#cccccc"><asp:panel id="Panel1" runat="server"></asp:panel>
															<table id="Table6" height="160" cellSpacing="1" width="600" align="center" border="0">
																<tr>
																	<td height="20">&nbsp;</td>
																</tr>
																<tr>
																	<td width="100" rowSpan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
																	<td height="67">
																		<table id="Table7" cellSpacing="1" width="400" align="center" border="0">
																			<tr>
																				<td width="200"><b>Login Terakhir </b>
																				</td>
																				<td width="200">:
																					<asp:label id="lblLastLogin" Runat="server"></asp:label></td>
																			</tr>
																			<TR>
																				<TD width="200"><STRONG>Jumlah Login</STRONG></TD>
																				<TD width="200">:
																					<asp:label id="lblJumlahLogin" Runat="server"></asp:label></TD>
																			</TR>
																			<tr>
																				<td width="200" height="10"><b>Nomor Serial </b>
																				</td>
																				<td width="200" height="10">:
																					<asp:label id="lblSerial" Runat="server"></asp:label></td>
																			</tr>
																			<TR>
																				<TD align="center" colSpan="2"><asp:label id="lblValidUntil" runat="server" Font-Bold="True"></asp:label><IMG id="imgStar" src="images/star2.gif" border="0" runat="server">
																					<asp:label id="lblValiduntil2" runat="server" Font-Bold="True"></asp:label></TD>
																			</TR>
																		</table>
																	</td>
																	<td>
																		<table id="Table8" cellSpacing="1" width="100" align="center" border="0">
																			<TR>
																				<TD align="center" colSpan="2" height="47"><asp:image id="photoView" runat="server" Height="70px" Width="70px"></asp:image><br>
																					Gambar Pilihan 2</TD>
																			</TR>
																		</table>
																	</td>
																</tr>
																<tr>
																	<td height="100">
																		<table id="Table9" cellSpacing="1" width="360" align="center" border="0">
																			<TR>
																				<TD align="center" colSpan="2" height="22"><asp:panel id="PanelBingo" runat="server" Height="200px" Width="255px" BackColor="#cccccc"></asp:panel></TD>
																			</TR>
																		</table>
																	</td>
																	<td></td>
																</tr>
																<TR>
																	<TD align="center" height="60">&nbsp;
																		<asp:button id="btnLogout" runat="server" Text="Keluar"></asp:button>&nbsp;&nbsp;<asp:button id="btnProsess" runat="server" Text="Proses"></asp:button></TD>
																	<td></td>
																</TR>
                                                                <tr>
                                                                    <td colspan="3" align="center">
                                                                        <asp:linkbutton id="lbtnForget" runat="server" CausesValidation="False">Lupa Token?</asp:linkbutton>
                                                                    </td>
                                                                </tr>
															</table>
															<asp:label id="lblAlertBingo" runat="server" Font-Bold="True" Visible="False" ForeColor="Red"
																Font-Size="Medium"></asp:label><br>
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
												<table id="Table10" cellSpacing="1" width="70%" border="0">
													<tr>
														<td align="center">

                                                             &nbsp;</td>
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
						<table id="Table11" height="80" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr vAlign="top">
								<td width="40%">&nbsp;</td>
								<td align="center" width="20%"><B>Tampilan Terbaik IE 6.0+ : 1024 x 768</B>
									<br>
									<table id="Table12" height="60" cellSpacing="0" cellPadding="0" width="789" border="0">
										<tr>
											<td align="center">PT Mitsubishi Motors Krama Yudha Sales Indonesia, Hak Cipta 
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
