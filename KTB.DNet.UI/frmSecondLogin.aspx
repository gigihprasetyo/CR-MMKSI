<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmSecondLogin.aspx.vb" Inherits="frmSecondLogin"  smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Personal Security Profile Registration</title> 
		<!-- saved from url=(0047)http://www.walterzorn.com/tooltip/tooltip_e.htm -->
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<LINK href="WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="/WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
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
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" valign="middle"
				id="Table1">
				<tr>
					<td vAlign="middle">
						<table height="380" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cc0001" border="0" style=" background-image:url(images/bg-diamond-red.jpg);   background-position:center top;  "
							id="Table2">
							<tr>
								<td width="40%">&nbsp;</td>
								<td width="20%" background="images/login_bg.gif">
									<table height="386" cellSpacing="0" cellPadding="0" width="789" border="0" id="Table3">
										<tr height="74">
											<td width="612" height="74"><IMG src="images/login_r1_c1.gif" border="0"></td>
											<td width="177"><IMG src="images/new_login_r1_c2.gif" border="0"></td>
										</tr>
										<tr height="81">
											<td align="center" background="images/login_bg.gif" colSpan="2" height="95">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" id="Table4">
													<tr>
														<%--<td align="left" width="25%"><IMG hspace="10" src="images/new_mits.gif" border="0"></td>--%>
														<td align="left" width="75%">
															<span style="padding-left:14px"><h1><STRONG>Profil Keamanan </STRONG>
															</h1></span>
														</td>
														<td align="right" width="25%"><IMG hspace="10" src="images/new_mits.gif" border="0"></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr height="144">
											<td vAlign="middle" align="center" background="images/login_bg.gif" colSpan="2" height="144">
												<table height="144" cellSpacing="0" cellPadding="0" width="787" align="center" border="0"
													id="Table5">
													<tr>
														<td align="center" bgColor="#cccccc">
															<table height="160" cellSpacing="1" width="500" align="center" border="0" id="Table6">
																<tr>
																	<td height="100">
																	<br>
																		<table cellSpacing="1" width="420" align="center" border="0" id="Table7">
																			<TR>
																				<TD width="183" height="23"><A href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b> <br>No HP yang akan digunakan untuk pengiriman informasi login anda. Mohon dicek 2 kali sebab jika salah anda tidak akan bisa masuk kedalam sistem <BR><b>Format:</b> <br>Numerik 0-9 sesuai nomor HP anda.<br>Tanpa tanda pemisah + atau - <BR><b>Contoh:</b> <BR>0812555666 (GSM umum)<BR>021777888 (flexi) ')"><asp:label id="Label2" runat="server">Nomor HP </asp:label></a></TD>
																				<TD height="23"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtHP" runat="server" Width="206px"
																						CssClass="welcomeLogin"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" ControlToValidate="txtHP" ErrorMessage="*"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" ControlToValidate="txtHP" ErrorMessage="format salah"
																						ValidationExpression="[0-9]*"></asp:regularexpressionvalidator></TD>
																			</TR>
																			<TR>
																				<TD width="183" height="23"><a href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b> Alamat email yang akan digunakan sebagai sarana alternatif pengiriman kode otentikasi. <br><b>Format:</b> namauser@namadomain.tld<br> <b>Contoh:</b> user@ktb.co.id')"><asp:label id="Label3" runat="server">Email</asp:label></a></TD>
																				<TD height="23" nowrap><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtEmail" runat="server" Width="206px"
																						CssClass="welcomeLogin"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmail" ErrorMessage="*"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail" ErrorMessage="format salah"
																						ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:regularexpressionvalidator></TD>
																			</TR>

																			<tr>
																				<td colspan="2" height="1" align=left><hr style="HEIGHT:1px; width:352px;"></td>
																			</tr>
																			<TR>
																				<TD width="183"><asp:label id="Label4" runat="server"><a href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b> <br>Tanggal kelahiran anda atau tanggal yang anda ingat dengan baik <BR>sebab kelak akan ditanyakan jika anda lupa kata kunci. <br><b>Format:</b> <br>DD/MM/YYYY<br><b>Contoh:</b><br>08/02/2001')">Tanggal Lahir</a></asp:label></TD>
																				<TD>
																					<P><cc1:inticalendar id="icTglLahir" runat="server" UrlImage="./Images/calendar.gif"></cc1:inticalendar></P>
																				</TD>
																			</TR>
																			<TR>
																				<TD width="183"><asp:label id="Label5" runat="server"><a href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b><br> Nama ibu anda atau nama yang anda ingat dengan baik. <BR>Sebab kelak akan ditanyakan jika anda lupa kata kunci. <BR><b>Format:</b> Alfabet <BR><b>Contoh:</b> Sri Rezeki')">Nama Depan Ibu</a></asp:label></TD>
																				<TD><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtNamaIbu" runat="server" Width="206px"
																						CssClass="welcomeLogin"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator7" runat="server" ControlToValidate="txtNamaIbu" ErrorMessage="*"></asp:requiredfieldvalidator></TD>
																			</TR>
																			<TR>
																				<TD width="183"><asp:label id="Label6" runat="server"><a href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b> <br>Pertanyaan yang anda buat sendiri. <BR><b>Format:</b> Alfabet<br> <b>Contoh:</b> Siapa Jagoanmu?')">Pertanyaan Favorit</a></asp:label></TD>
																				<TD><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtPertanyaan" runat="server" Width="206px"
																						CssClass="welcomeLogin"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator8" runat="server" ControlToValidate="txtPertanyaan" ErrorMessage="*"></asp:requiredfieldvalidator></TD>
																			</TR>
																			<TR>
																				<TD width="183" height="22"><asp:label id="Label7" runat="server"><a href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b> <Br>Jawaban terhadap pertanyaan favorit anda sebelumnya. <br><b>Format:</b> Alfabet<br> <b>Contoh:</b> Gundam')"> Jawaban Favorit</a></asp:label></TD>
																				<TD height="22"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtJawaban" runat="server" Width="206px"
																						CssClass="welcomeLogin"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator9" runat="server" ControlToValidate="txtJawaban" ErrorMessage="*"></asp:requiredfieldvalidator></TD>
																			</TR>
																			<TR>
																				<TD width="183" height="15"><a href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b> <br>Sejumlah pertanyaan spesifik dari sistem yang anda sebaiknya anda jawab.<br>Sebab kelak akan ditanyakan jika anda lupa kata kunci. <BR><b>Format:</b> Alfabet<br>')"><asp:label id="lblPertanyaan1" runat="server">Pertanyaan 1</asp:label></a></TD>
																				<TD height="15"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtPertanyaan1" runat="server" MaxLength="12"
																						Width="206px" CssClass="welcomeLogin"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator10" runat="server" ControlToValidate="txtPertanyaan1"
																						ErrorMessage="*"></asp:requiredfieldvalidator></TD>
																			</TR>
																			<TR>
																				<TD width="183" height="15"><a href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b> <br>Sejumlah pertanyaan spesifik dari sistem yang anda sebaiknya anda jawab.<br>Sebab kelak akan ditanyakan jika anda lupa kata kunci. <BR><b>Format:</b> Alfabet<br>')"><asp:label id="lblPertanyaan2" runat="server">Pertanyaan 2</asp:label></a></TD>
																				<TD height="15"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtPertanyaan2" runat="server" MaxLength="25"
																						Width="206px" CssClass="welcomeLogin"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator11" runat="server" ControlToValidate="txtPertanyaan2"
																						ErrorMessage="*"></asp:requiredfieldvalidator></TD>
																			</TR>
																			<TR>
																				<TD width="183" height="15"><a href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b> <br>Sejumlah pertanyaan spesifik dari sistem yang anda sebaiknya anda jawab.<br>Sebab kelak akan ditanyakan jika anda lupa kata kunci. <BR><b>Format:</b> Alfabet<br>')"><asp:label id="lblPertanyaan3" runat="server">Pertanyaan 3</asp:label></a></TD>
																				<TD height="15"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtPertanyaan3" runat="server" MaxLength="25"
																						Width="206px" CssClass="welcomeLogin"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator12" runat="server" ControlToValidate="txtPertanyaan3"
																						ErrorMessage="*"></asp:requiredfieldvalidator></TD>
																			</TR>
																			<TR>
																				<TD width="183" height="15"><a href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b> <br>Sejumlah pertanyaan spesifik dari sistem yang anda sebaiknya anda jawab.<br>Sebab kelak akan ditanyakan jika anda lupa kata kunci. <BR><b>Format:</b> Alfabet<br>')"><asp:label id="lblPertanyaan4" runat="server">Pertanyaan 4</asp:label></a></TD>
																				<TD height="15"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtPertanyaan4" runat="server" MaxLength="25"
																						Width="206px" CssClass="welcomeLogin"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator13" runat="server" ControlToValidate="txtPertanyaan4"
																						ErrorMessage="*"></asp:requiredfieldvalidator></TD>
																			</TR>
																			<TR>
																				<TD width="183" height="15"><a href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b> <br>Sejumlah pertanyaan spesifik dari sistem yang anda sebaiknya anda jawab.<br>Sebab kelak akan ditanyakan jika anda lupa kata kunci. <BR><b>Format:</b> Alfabet<br>')"><asp:label id="lblPertanyaan5" runat="server">Pertanyaan 5</asp:label></a></TD>
																				<TD height="15"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtPertanyaan5" runat="server" MaxLength="25"
																						Width="206px" CssClass="welcomeLogin"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator14" runat="server" ControlToValidate="txtPertanyaan5"
																						ErrorMessage="*"></asp:requiredfieldvalidator></TD>
																			</TR>
																			<tr>
																				<td colspan="2" align=left height="1"><hr style="HEIGHT:1px; width:352px;">
																				</td>
																			</tr>
																			<TR>
																				<TD width="183" height="3"><asp:label id="Label9" runat="server"><a href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b> <br>Silahkan upload gambar dari komputer anda . <BR><b>Format:</b> <br>70 x 70 pixel. Ukuran maksimum 30 Kb. <br>')">Upload Gambar Pilihan 2</a></asp:label></TD>
																				<td height="3"><INPUT id="photoSrc" onkeydown="return false" type="file" size="15" name="photoSrc" runat="server"><asp:button id="btnUpload" runat="server" CausesValidation="False" Height="18px" Text="Upload"></asp:button></td>
																			</TR>
																			<TR>
																				<TD width="183"><a href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b> <Br>Tulisan yang akan disertakan  pada gambar pilihan anda <br><b>Format:</b> <Br>Gabungan alfabet-numerik sepanjang 8 karakter <br><b>Contoh:</b><br> fluffy ')"><asp:label id="Label1" runat="server">Deskripsi Gambar Pilihan 2</asp:label></a></TD>
																				<TD><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtDeskripsiGambar" runat="server" Width="206px"
																						CssClass="welcomeLogin" MaxLength="8"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ControlToValidate="txtDeskripsiGambar"
																						ErrorMessage="*"></asp:requiredfieldvalidator></TD>
																			</TR>
																			<tr>
																				<td colspan="2" align=left height="14">&nbsp;</td>
																			</tr>
																			
																			<TR align="center">
																				<TD width="180" height="83"><table border="0" cellspacing="0" cellpadding="0" id="Table8">
																						<tr>
																							<td><asp:button id="btnPrevious" runat="server" Width="20px" CausesValidation="False" Text="<"
																									Visible="true"></asp:button>
																								<asp:ImageButton id="imgBack" runat="server" ImageUrl="./images/page_prev.gif" Visible="False"></asp:ImageButton></td>
																							<td align=center><asp:image id="photoView" runat="server" Width="70px" Height="70px"></asp:image><br>Gambar Pilihan 2</td>
																							<td><asp:button id="btnNext" runat="server" Width="20px" CausesValidation="False" Text=">" Visible="true"></asp:button>
																								<asp:ImageButton id="imgNext" runat="server" ImageUrl="./images/page_next.gif" Visible="False"></asp:ImageButton></td>
																						</tr>
																					</table>
																				</TD>
																				<TD width="240" height="83"><asp:image id="ImgCaptcha" runat="server" Width="120px" Height="32px"></asp:image><br>
																					<asp:textbox id="CodeNumberTextBox" onmouseover="return escape('<b>Deskripsi:</b> <br>Huruf sepanjang 5 karakter.<br>Semua karakter akan dianggap huruf kapital.<br><b>Contoh:</b><br> SHRDLU <BR>EtAoIn')" 
																						runat="server" Width="88px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="CodeNumberTextBox"
																						ErrorMessage="*"></asp:requiredfieldvalidator></TD>
																			</TR>
																		</table>
															<SCRIPT src="./WebResources/wz_tooltip.js" type=text/javascript></SCRIPT>		
																		
																		<br>
																	</td>
																</tr>
																<TR>
																	<TD align="center" height="60">&nbsp;<asp:button id="btnLogout" runat="server" Width="56px" CausesValidation="False" Text="Keluar"></asp:button>&nbsp;&nbsp;&nbsp;<asp:button id="btnDaftar" runat="server" Width="56px" Text="Proses"></asp:button></TD>
																</TR>
															</table>
															<br>
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
											<td align="right">
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
											<td align="center"> PT Mitsubishi Motors Krama Yudha Sales Indonesia, Hak Cipta 
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
