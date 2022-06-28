<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmImageSetup.aspx.vb" Inherits="frmImageSetup" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Personal Security Profile Registration</title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<LINK href="WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="/WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
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
		<form id="FormLogin" method="post" width="100%" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" valign="middle">
				<tr>
					<td vAlign="middle">
						<table height="380" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cc0001" border="0">
							<tr>
								<td width="40%">&nbsp;</td>
								<td width="20%" background="images/login_bg.gif">
									<table height="386" cellSpacing="0" cellPadding="0" width="789" border="0">
										<tr height="74">
											<td width="612" height="74"><IMG src="images/login_r1_c1.gif" border="0"></td>
											<td width="177"><IMG src="images/new_login_r1_c2.gif" border="0"></td>
										</tr>
										<tr height="81">
											<td align="center" background="images/login_bg.gif" colSpan="2" height="69">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<%--<td align="left" width="25%"><IMG hspace="10" src="images/new_mits.gif" border="0"></td>--%>
														<td align="left" width="75%" style="padding-left:14px;">
															<h1><STRONG>Personal Security&nbsp;Profile </STRONG>
															</h1>
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
																	<td height="100">
																		<table cellSpacing="1" width="460" align="center" border="0">
																			<TR>
																				<TD width="183" height="57"><asp:label id="Label8" runat="server" BackColor="Silver" BorderColor="#E0E0E0">GambarPribadi</asp:label></TD>
																				<TD height="57"><asp:button id="btnPrevious" runat="server" Text="<<"></asp:button><asp:image id="photoView" runat="server" Height="70px" Width="70px"></asp:image><asp:button id="btnNext" runat="server" Text=">>"></asp:button></TD>
																			</TR>
																			<TR>
																				<td width="183" height="3"><asp:label id="Label9" runat="server" BackColor="Silver" BorderColor="#E0E0E0">Upload Gambar</asp:label></td>
																				<td height="3">&nbsp;<INPUT id="photoSrc" onkeydown="return false" type="file" size="15" name="photoSrc" runat="server">
																					<asp:button id="btnUpload" runat="server" Text="Upload"></asp:button></td>
																			</TR>
																			<TR>
																				<TD width="183"><asp:label id="Label1" runat="server" BackColor="Silver" BorderColor="#E0E0E0">Deskripsi Gambar</asp:label></TD>
																				<TD><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtDeskripsiGambar" runat="server" Width="206px"
																						CssClass="welcomeLogin"></asp:textbox></TD>
																			</TR>
																			<TR>
																				<TD width="183" height="23"><asp:label id="Label2" runat="server" BackColor="Silver" BorderColor="#E0E0E0">HP Pribadi</asp:label></TD>
																				<TD height="23"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtHP" runat="server" Width="206px"
																						CssClass="welcomeLogin"></asp:textbox></TD>
																			</TR>
																			<TR>
																				<TD width="183" height="23"><asp:label id="Label3" runat="server" BackColor="Silver" BorderColor="#E0E0E0">Email Pribadi</asp:label></TD>
																				<TD height="23"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtEmail" runat="server" Width="206px"
																						CssClass="welcomeLogin"></asp:textbox></TD>
																			</TR>
																			<TR>
																				<TD width="183"><asp:label id="Label4" runat="server" BackColor="Silver" BorderColor="#E0E0E0">Tgl Lahir</asp:label></TD>
																				<TD><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtTanggalLahir" runat="server" Width="206px"
																						CssClass="welcomeLogin"></asp:textbox></TD>
																			</TR>
																			<TR>
																				<TD width="183"><asp:label id="Label5" runat="server" BackColor="Silver" BorderColor="#E0E0E0">Nama Depan Ibu</asp:label></TD>
																				<TD><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtNamaIbu" runat="server" Width="206px"
																						CssClass="welcomeLogin"></asp:textbox></TD>
																			</TR>
																			<TR>
																				<TD width="183"><asp:label id="Label6" runat="server" BackColor="Silver" BorderColor="#E0E0E0">Pertanyaan Favorite</asp:label></TD>
																				<TD><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtPertanyaan" runat="server" Width="206px"
																						CssClass="welcomeLogin"></asp:textbox></TD>
																			</TR>
																			<TR>
																				<TD width="183" height="22"><asp:label id="Label7" runat="server" BackColor="Silver" BorderColor="#E0E0E0">Jawaban</asp:label></TD>
																				<TD height="22"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtJawaban" runat="server" Width="206px"
																						CssClass="welcomeLogin"></asp:textbox></TD>
																			</TR>
																			<TR>
																				<TD width="183" height="15"><asp:label id="lblPassword" runat="server" BackColor="Silver" BorderColor="#E0E0E0">kata kunci</asp:label></TD>
																				<TD height="15"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtKataKunci" runat="server" Width="206px"
																						CssClass="welcomeLogin" TextMode="Password"></asp:textbox></TD>
																			</TR>
																			<TR>
																				<td align="center" colSpan="2"><IMG height="62" src="JpegImage.aspx" width="200">&nbsp;<br>
																					<p><asp:label id="MessageLabel" runat="server"></asp:label><br>
																						<asp:textbox id="CodeNumberTextBox" runat="server"></asp:textbox></p>
																					<P><em class="notice">(Note: jika captcha tidak terlihat silahkan refresh)</em>
																					</P>
																				</td>
																			</TR>
																		</table>
																	</td>
																</tr>
																<TR>
																	<TD align="center" height="60">&nbsp;<asp:button id="btnDaftar" runat="server" Text="Daftarkan" Width="56px"></asp:button>
																		<asp:button id="btnNewToken" runat="server" Text="Token Baru" Width="88px"></asp:button><asp:button id="btnKirimKodeAktivasi" runat="server" Text="Kirim Kode Aktivasi" Width="112px"></asp:button><asp:button id="Button2" runat="server" Text="Next" Width="76px"></asp:button><asp:button id="btnLogout" runat="server" Text="Logout" Width="87px"></asp:button></TD>
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
														<td align="center">  <div style="text-align:center">
                                                                  <span style="font-family:Arial; font-size:12px; font-weight:bold;">Mitsubishi Motors authorize distributor<br/></span>
                                                                 <span style="font-family:Arial; font-size:16px; font-weight:bold;">PT Mitsubishi Motors Krama Yudha Sales Indonesia</span>
                                                            </div></td>
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
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
