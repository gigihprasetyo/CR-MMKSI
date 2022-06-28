<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmSecurityPage.aspx.vb" Inherits="frmSecurityPage" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
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
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
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
					<td class="titlePage">Security - Profil Keamanan</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td vAlign="middle">
						<table height="380" cellSpacing="0" cellPadding="0" width="99%" border="0">
							<tr>
								<td width="20%">
									<table height="386" cellSpacing="0" cellPadding="0" border="0" width="100%">
										<tr height="144">
											<td vAlign="middle" align="center" background="images/login_bg.gif" colSpan="2" height="144">
												<table height="144" cellSpacing="0" cellPadding="0" align="center" border="0" width="100%">
													<tr>
														<td align="center">
															<table cellSpacing="1" width="100%" align="center" border="0">
																<TBODY>
																	<tr>
																		<td height="100" width="480">
																			<table cellSpacing="1" width="480" align="center" border="0">
																				<TBODY>
																					<TR>
																						<TD width="183" height="23"><asp:label id="Label2" runat="server" BorderColor="#E0E0E0"><A href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b> <br>No HP yang akan digunakan untuk pengiriman informasi login anda. Mohon dicek 2 kali sebab jika salah anda tidak akan bisa masuk kedalam sistem <BR><b>Format:</b> <br>Numerik 0-9 sesuai nomor HP anda.<br>Tanpa tanda pemisah + atau - <BR><b>Contoh:</b> <BR>0812555666 (GSM umum)<BR>021777888 (flexi) ')">Nomor HP </A></asp:label>
																						&nbsp;&nbsp;&nbsp;<b><asp:label id="lblChangeHP" runat="server" BorderColor="#E0E0E0" Font-Size =10></asp:Label> </b>
																						</TD>
																						<TD height="23" nowrap>
                                                                                            <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtHP" runat="server" Width="206px"
                                                                                                CssClass="welcomeLogin" ReadOnly="True"></asp:TextBox>
                                                                                            <%--<asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" ControlToValidate="txtHP" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                                                            <%--<asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" ControlToValidate="txtHP" ErrorMessage="format salah" ValidationExpression="[0-9]*"></asp:regularexpressionvalidator>--%>
                                                                                            <asp:LinkButton ID="lbtOTP" runat="server" Text="Ubah" CommandName="Ganti No HP" CausesValidation="False">
                                                                                                <img src="../images/Edit.gif" border="0" alt="Ubah">
                                                                                            </asp:LinkButton>
																							<asp:label id="lblKodeActivate" runat="server" BorderColor="#E0E0E0" BackColor="Transparent"
																								Visible="False" Width="80px">
																								<b>Kode Aktivasi</b></asp:label>
																							<asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtKodeAktivasi" runat="server" Width="104px"
																								CssClass="welcomeLogin" Visible="False"></asp:textbox></TD>
																		</td>
																	</tr>
																	<TR>
																		<TD width="183" height="22"><asp:label id="Label3" runat="server" BorderColor="#E0E0E0"><a href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b> Alamat email yang akan digunakan sebagai sarana alternatif pengiriman kode otentikasi. <br><b>Format:</b> namauser@namadomain.tld<br> <b>Contoh:</b> user@ktb.co.id')">Email</A></asp:label></TD>
																		<TD height="22"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtEmail" runat="server" Width="206px"
																				CssClass="welcomeLogin"></asp:textbox>
																			<asp:RequiredFieldValidator id="RequiredFieldValidator7" runat="server" ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator></TD>
																	</TR>
																	<tr>
																		<td colspan="2" align="left" height="1"><hr style="WIDTH:393px; HEIGHT:1px">
																		</td>
																	</tr>
																	<TR>
																		<TD width="183"><asp:label id="Label4" runat="server" BorderColor="#E0E0E0"><a href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b> <br>Tanggal kelahiran Anda atau tanggal yang anda ingat dengan baik <BR>sebab kelak akan ditanyakan jika anda lupa kata kunci. <br><b>Format:</b> <br>DD/MM/YYYY<br><b>Contoh:</b><br>08/02/2001')">Tanggal Lahir</a></asp:label></TD>
																		<TD>
																			<cc1:inticalendar id="icTglLahir" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
																	</TR>
																	<TR>
																		<TD width="183"><asp:label id="Label5" runat="server" BorderColor="#E0E0E0"><a href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b><br> Nama ibu anda atau nama yang Anda ingat dengan baik. <BR>Sebab kelak akan ditanyakan jika anda lupa kata kunci. <BR><b>Format:</b> Alfabet <BR><b>Contoh:</b> Sri Rezeki')">Nama Depan Ibu</a></asp:label></TD>
																		<TD><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtNamaIbu" runat="server" Width="206px"
																				CssClass="welcomeLogin"></asp:textbox>
																			<asp:RequiredFieldValidator id="RequiredFieldValidator8" runat="server" ControlToValidate="txtNamaIbu" ErrorMessage="*"></asp:RequiredFieldValidator></TD>
																	</TR>
																	<TR>
																		<TD width="183"><asp:label id="Label6" runat="server" BorderColor="#E0E0E0"><a href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b> <br>Pertanyaan yang anda buat sendiri. <BR><b>Format:</b> Alfabet<br> <b>Contoh:</b> Siapa Jagoanmu?')">Pertanyaan Favorite</a></asp:label></TD>
																		<TD><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtPertanyaan" runat="server" Width="206px"
																				CssClass="welcomeLogin"></asp:textbox>
																			<asp:RequiredFieldValidator id="RequiredFieldValidator9" runat="server" ControlToValidate="txtPertanyaan" ErrorMessage="*"></asp:RequiredFieldValidator></TD>
																	</TR>
																	<TR>
																		<TD width="183" height="22"><asp:label id="Label7" runat="server" BorderColor="#E0E0E0"><a href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b> <Br>Jawaban terhadap pertanyaan favorit anda sebelumnya. <br><b>Format:</b> Alfabet<br> <b>Contoh:</b> Gundam')">Jawaban</a></asp:label></TD>
																		<TD height="22"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtJawaban" runat="server" Width="206px"
																				CssClass="welcomeLogin"></asp:textbox>
																			<asp:RequiredFieldValidator id="RequiredFieldValidator10" runat="server" ControlToValidate="txtJawaban" ErrorMessage="*"></asp:RequiredFieldValidator></TD>
																	</TR>
																	<TR>
																		<TD width="183" height="15"><A onmouseover="return escape('<b>Deskripsi:</b> <br>Sejumlah pertanyaan spesifik dari sistem yang anda sebaiknya anda jawab.<br>Sebab kelak akan ditanyakan jika anda lupa kata kunci. <BR><b>Format:</b> Alfabet');" href="#" style="COLOR:black"><asp:label id="lblPertanyaan1" runat="server" BorderColor="#E0E0E0">Pertanyaan 1</asp:label></a></TD>
																		<TD height="15"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtPertanyaan1" runat="server" MaxLength="25"
																				Width="120px" CssClass="welcomeLogin"></asp:textbox>
																			<asp:RequiredFieldValidator id="RequiredFieldValidator11" runat="server" ControlToValidate="txtPertanyaan1"
																				ErrorMessage="*"></asp:RequiredFieldValidator></TD>
																	</TR>
																	<TR>
																		<TD width="183" height="15"><A onmouseover="return escape('<b>Deskripsi:</b> <br>Sejumlah pertanyaan spesifik dari sistem yang anda sebaiknya anda jawab.<br>Sebab kelak akan ditanyakan jika anda lupa kata kunci. <BR><b>Format:</b> Alfabet');" href="#" style="COLOR:black"><asp:label id="lblPertanyaan2" runat="server" BorderColor="#E0E0E0">Pertanyaan 2</asp:label></a></TD>
																		<TD height="15"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtPertanyaan2" runat="server" MaxLength="25"
																				Width="120px" CssClass="welcomeLogin"></asp:textbox>
																			<asp:RequiredFieldValidator id="RequiredFieldValidator12" runat="server" ControlToValidate="txtPertanyaan2"
																				ErrorMessage="*"></asp:RequiredFieldValidator></TD>
																	</TR>
																	<TR>
																		<TD width="183" height="15"><A onmouseover="return escape('<b>Deskripsi:</b> <br>Sejumlah pertanyaan spesifik dari sistem yang anda sebaiknya anda jawab.<br>Sebab kelak akan ditanyakan jika anda lupa kata kunci. <BR><b>Format:</b> Alfabet');" href="#" style="COLOR:black"><asp:label id="lblPertanyaan3" runat="server" BorderColor="#E0E0E0">Pertanyaan 3</asp:label></a></TD>
																		<TD height="15"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtPertanyaan3" runat="server" MaxLength="25"
																				Width="120px" CssClass="welcomeLogin"></asp:textbox>
																			<asp:RequiredFieldValidator id="RequiredFieldValidator13" runat="server" ControlToValidate="txtPertanyaan3"
																				ErrorMessage="*"></asp:RequiredFieldValidator></TD>
																	</TR>
																	<TR>
																		<TD width="183" height="15"><A onmouseover="return escape('<b>Deskripsi:</b> <br>Sejumlah pertanyaan spesifik dari sistem yang anda sebaiknya anda jawab.<br>Sebab kelak akan ditanyakan jika anda lupa kata kunci. <BR><b>Format:</b> Alfabet');" href="#"  style="COLOR:black" "><asp:label id="lblPertanyaan4" runat="server" BorderColor="#E0E0E0">Pertanyaan 4</asp:label></a></TD>
																		<TD height="15"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtPertanyaan4" runat="server" MaxLength="25"
																				Width="120px" CssClass="welcomeLogin"></asp:textbox>
																			<asp:RequiredFieldValidator id="RequiredFieldValidator14" runat="server" ControlToValidate="txtPertanyaan4"
																				ErrorMessage="*"></asp:RequiredFieldValidator></TD>
																	</TR>
																	<TR>
																		<TD width="183" height="15"><a onmouseover="return escape('<b>Deskripsi:</b> <br>Sejumlah pertanyaan spesifik dari sistem yang anda sebaiknya anda jawab.<br>Sebab kelak akan ditanyakan jika anda lupa kata kunci. <BR><b>Format:</b> Alfabet<br>');" href="#" style="COLOR:black"><asp:label id="lblPertanyaan5" runat="server" BorderColor="#E0E0E0">Pertanyaan 5</asp:label></a></TD>
																		<TD height="15"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtPertanyaan5" runat="server" MaxLength="25"
																				Width="120px" CssClass="welcomeLogin"></asp:textbox>
																			<asp:RequiredFieldValidator id="RequiredFieldValidator15" runat="server" ControlToValidate="txtPertanyaan5"
																				ErrorMessage="*"></asp:RequiredFieldValidator></TD>
																	</TR>
																	<TR id="rowKataKunciLama" runat="server">
																		<TD class="titleField"><asp:label id="lblKataKunciLama" runat="server">Kata Kunci Lama</asp:label><asp:label id="Label22" runat="server" ForeColor="red" Visible="False">*</asp:label></TD>
																		<TD><asp:label id="Label12" runat="server"></asp:label><asp:textbox id="txtKataKunciLama" onkeypress="return HtmlCharUniv(event)" runat="server" TextMode="Password"></asp:textbox></TD>
																	</TR>
																	<TR>
																		<TD class="titleField"><asp:label id="lblKataKunciBaru" runat="server">Kata Kunci Baru</asp:label><asp:label id="Label13" runat="server" ForeColor="red" Visible="False">*</asp:label></TD>
																		<TD nowrap><asp:label id="Label14" runat="server"></asp:label>
                                                                            <asp:textbox id="txtKataKunciBaru" onkeypress="return HtmlCharUniv(event)" runat="server" TextMode="Password"></asp:textbox>
                                                                            <%--<asp:regularexpressionvalidator id="RegularExpressionValidator2" runat="server" ErrorMessage="Minimum 6 Karakter, Maximum 20 Karakter"
																				ControlToValidate="txtKataKunciBaru" ValidationExpression="\w{0,20}|w{0}" Width="160px"  EnableClientScript="false" Enabled="false">Max 20 Karakter</asp:regularexpressionvalidator>--%>
                                                                            <%--<br /><asp:label id="Label24" Font-Size="Smaller" runat="server" ForeColor="red">[Kata Kunci Baru harus mengandung Huruf besar, Huruf kecil, Tanda baca dan Angka]</asp:label>--%>
																		</TD>
																	</TR>
																	<TR>
																		<TD nowrap class="titleField" style="HEIGHT: 23px"><asp:label id="lblKonfirmasiKataKunciBaru" runat="server">Konfirmasi Kata Kunci Baru</asp:label><asp:label id="Label15" runat="server" ForeColor="red" Visible="False">*</asp:label></TD>
																		<TD style="HEIGHT: 23px">
																			<P><asp:label id="Label16" onkeypress="return HtmlCharUniv(event)" runat="server"></asp:label><asp:textbox id="txtKonfirmasiKataKunciBaru" runat="server" TextMode="Password"></asp:textbox></P>
																		</TD>
																	</TR>
																	<tr>
																		<td colspan="2" align="left" height="1"><hr style="WIDTH:393px; HEIGHT:1px">
																		</td>
																	</tr>
													<TR>
														<TD height="3"><asp:label id="Label9" runat="server" BorderColor="#E0E0E0"><a href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b> <br>Silahkan upload gambar dari komputer anda . <BR><b>Format:</b> <br>70 x 70 pixel. Ukuran maksimum 30 Kb. <br>')">Upload Gambar Pilihan 2</a></asp:label></TD>
														<td height="3"><INPUT id="photoSrc" onkeydown="return false" type="file" size="15" name="photoSrc" runat="server">
															<asp:button id="btnUpload" runat="server" Text="Upload" Height="18px" CausesValidation="False" ></asp:button></td>
													</TR>
													<TR>
														<TD><asp:label id="Label1" runat="server" BorderColor="#E0E0E0"><a href="#" style="COLOR:black" onmouseover="return escape('<b>Deskripsi:</b> <Br>Tulisan yang akan disertakan  pada gambar pilihan anda <br><b>Format:</b> <Br>Gabungan alfabet-numerik sepanjang 8 karakter <br><b>Contoh:</b><br> fluffy ')">Deskripsi 
																	Gambar Pilihan 2</A></asp:label></TD>
														<TD><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtDeskripsiGambar" runat="server" Width="206px"
																CssClass="welcomeLogin"></asp:textbox>
															<asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" ControlToValidate="txtDeskripsiGambar"
																ErrorMessage="*"></asp:RequiredFieldValidator></TD>
													</TR>
																	
																	<tr>
																		<td>&nbsp;</td></tr>
																	<TR>
																		<TD height="82" width="280" valign="bottom">
																			<table border="0" cellpadding="0" cellspacing="0">
																				<tr>
																					<td><asp:button id="btnPrevious" Width="20px" runat="server" Text="<" Visible="True" CausesValidation="False" ></asp:button><asp:ImageButton id="imgBack" runat="server" Visible="False"></asp:ImageButton></td>
																					<td align=center><asp:image id="photoView" runat="server" Height="70px" Width="70px"></asp:image><br>Gambar Pilihan 2</td>
																					<td><asp:button id="btnNext" Width="20px" runat="server" Text=">" Visible="True" CausesValidation="False"></asp:button><asp:ImageButton id="imgNext" runat="server" Visible="False"></asp:ImageButton></td>
																					<td style="WIDTH: 55px"></td>
																				</tr>
																			</table>
																		</TD>
																		<TD width="200" height="82">
																			<asp:Image id="captchaImg" runat="server" Width="120px" Height="32px"></asp:Image>
																			<asp:TextBox id="CodeNumberTextBox" onmouseover="return escape('<b>Deskripsi:</b> <br>Huruf sepanjang 5 karakter.<br>Semua karakter akan dianggap huruf kapital.<br><b>Contoh:</b><br> SHRDLU <BR>EtAoIn')" runat="server" Width="120px"></asp:TextBox></TD>
																		</td>
																		
													</tr>
													<tr>
														<td>&nbsp;</td>
													</tr>
													<tr height="40">
														<td colspan=2 align=center><br>
															<asp:button id="btnDaftar" runat="server"  onmouseover="return escape('Klik tombol ini untuk menyimpan perubahan data Anda')" Width="105px" Text="Simpan Data"></asp:button>&nbsp;
															<asp:button id="btnCancelReg" runat="server" Width="105px" onmouseover="return escape('Klik tombol ini untuk membatalkan perubahan HP')" Text="Batalkan HP Baru"></asp:button>&nbsp;
															<asp:button id="btnNewToken" runat="server" Width="105px" onmouseover="return escape('Dapatkan Token terbaru Anda dengan menekan tombol ini')" Text="Token Baru"></asp:button><IMG src="../images/star2.gif" border="0" id="imgStar" runat="server">&nbsp;
															<asp:button id="btnKirimKodeAktivasi" runat="server" Width="105px" style="display:none"
                                                                onmouseover="return escape('Jika kode aktivasi Anda hilang, tekan tombol ini agar dikirim ulang')" Text="Kirim Kode Aktivasi"></asp:button></td>
													</tr>
												</table>
												<SCRIPT src="../WebResources/wz_tooltip.js" type="text/javascript"></script>
											</td>
											<td width="30%" valign="top"><br>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table></TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE>
		</form>
	</body>
</HTML>

