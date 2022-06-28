<%@ Register  TagPrefix = "OTPPage" TagName = "OTPPage" Src = "OTP.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmForgetToken.aspx.vb" Inherits="FrmForgetToken" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Lupa Token</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="/WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="./WebResources/PreventNewWindow.js"></script>
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
          <script language="javascript" type="text/javascript" src="WebResources/GlobalVar.js"></script>
        <script language="javascript">
           
            var lblAlert = document.getElementById("OTPPage_lblAlert");
            function kosong() {
                var lblAlert = document.getElementById("OTPPage_lblAlert");
                lblAlert.innerHTML = "";
            }
            var f = new Date();
            function f1() {
                try {
                    if (document.getElementById("HdSec").value == 0 || document.getElementById("HdSec").value == "")
                    { f2(); }
                    else
                    {
                        sec = document.getElementById("HdSec").value;
                        min = document.getElementById("HdMin").value;
                        time = document.getElementById("HdTime").value;
                        f2();
                    }
                } catch (e) {

                }
            }

            function f2() {
                if (parseInt(sec) > 0) {
                    sec = parseInt(sec) - 1;
                    document.getElementById("showtime").innerHTML = "Berlaku Selama : " + min + " : " + sec + " ";
                    //document.getElementById("showtime").innerHTML = "Berlaku Selama 5 Menit";
                    tim = setTimeout("f2()", 1000);

                    document.getElementById("HdSec").value = sec;
                    document.getElementById("HdMin").value = min;
                    document.getElementById("HdTime").value = time;

                    func_txtTimer_hidden();
                }
                else {
                    if (parseInt(sec) == 0) {
                        if (parseInt(min) > 0) {
                            min = parseInt(min) - 1;
                        }
                        time = parseInt(time) - 1;
                        if (parseInt(min) == 0) {
                            if (parseInt(time) == 0) {
                                clearTimeout(tim);
                                document.getElementById("showtime").innerHTML = "Waktu Anda Sudah Habis Silahkan Untuk Mengirimkan Ulang Kode OTP";

                                document.getElementById("HdSec").value = sec;
                                document.getElementById("HdMin").value = min;
                                document.getElementById("HdTime").value = time;

                                func_txtTimer_visible();
                            }
                            else {

                                sec = 60;
                                min = 0
                                document.getElementById("showtime").innerHTML = "Berlaku Selama : " + min + " : " + sec + " ";

                                document.getElementById("HdSec").value = sec;
                                document.getElementById("HdMin").value = min;
                                document.getElementById("HdTime").value = time;

                                func_txtTimer_hidden();

                                tim = setTimeout("f2()", 1000);
                            }
                        }
                        else {
                            sec = 60;
                            document.getElementById("showtime").innerHTML = "Berlaku Selama : " + min + " : " + sec + " ";
                            //document.getElementById("showtime").innerHTML = "Berlaku Selama 5 Menit";
                            tim = setTimeout("f2()", 1000);

                            func_txtTimer_hidden();
                        }
                    }
                }
            }

            function func_txtTimer_visible() {

                var btnReset = document.getElementById("btnReset");

                document.getElementById("btnReset").style.visibility = "visible";
                document.getElementById("OTPPage_btnSimpan").disabled = true;
                document.getElementById("OTPPage_txtKodeOTP").readOnly = true;
                btnReset.style.visibility = "visible";
            }

            function func_txtTimer_hidden() {

                var btnReset = document.getElementById("btnReset");
                document.getElementById("OTPPage_btnSimpan").disabled = false;
                document.getElementById("OTPPage_txtKodeOTP").readOnly = false;
                btnReset.style.visibility = "hidden";
            }

            function GotoHome() {
                var txtHome = document.getElementById("txtHome");
                location.replace(txtHome.value);
            }

    </script>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="f1();"">
        <form id="FormLogin" method="post" runat="server" width="100%">
            <input type="hidden" id="HdTime" runat="server" value="" />
            <input type="hidden" id="HdMin" runat="server" value="" />
            <input type="hidden" id="HdSec" runat="server" value="" />
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" valign="middle">
				<tr>
					<td vAlign="middle">
						<table height="380" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cc0001" style=" background-image:url(images/bg-diamond-red.jpg);   background-position:center top;  "
							border="0" >
							<tr>
								<td width="40%">&nbsp;</td>
								<td width="20%" background="images/login_bg.gif">
									<table height="386" cellSpacing="0" cellPadding="0" width="789" border="0">
										<tr height="74">
											<td width="612" height="74"><IMG src="images/login_r1_c1.gif" border="0"></td>
                                            <td width="177">
                                                <img src="images/new_login_r1_c2.gif" border="0"></td>
										</tr>
										<tr height="81">
											<td align="center" background="images/login_bg.gif" colSpan="2" height="81">
												<table width="100%" border="0" cellspacing="0" cellpadding="0">
													<tr>
														<td align="left" width="25%"><img src="images/new_mits.gif" border="0" hspace="10"></td>
														<td align="center" width="50%"><h2><strong>Verifikasi Kata Kunci</strong></h2>
														</td>
														<td width="25%" align="right"><img src="images/new_fuso.gif" border="0" hspace="10"></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr height="144">
											<td vAlign="middle" align="center" background="images/login_bg.gif" colSpan="2" height="144">
												<table height="144" cellSpacing="0" cellPadding="0" width="787" align="center" border="0">
													<tr>
														<td align="center" bgColor="#cccccc">
															<table cellSpacing="1" width="500" align="center" border="0" height="160">
																<tr>
																	<td Colspan="3" height="20" width="400">&nbsp;</td>
																	<TD class="titlePage" width="100"></TD>
																</tr>
																<tr valign="top">
																	<td height="140">
																		<table cellSpacing="1" width="400" align="center" border="0">
																			<tr>
																				<td class="welcomeLogin" width="160">Kode Organisasi</td>
                                                                                <td width="5">:</td>
																				<td class="welcomeLogin" width="240">
																					<asp:textbox id="DealerTextBox" runat="server" CssClass="welcomeLogin"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ControlToValidate="DealerTextBox" ErrorMessage="*"></asp:requiredfieldvalidator></td>
																			</tr>
																			<tr>
																				<td width="160" class="welcomeLogin">Nama</td>
                                                                                <td width="5">:</td>
																				<td width="240">
																					<asp:textbox id="NameTextBox" runat="server" CssClass="welcomeLogin"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="NameTextBox" ErrorMessage="*"></asp:requiredfieldvalidator></td>
																			</tr>
																			<TR>
																				<td width="160"></td>
																				<td width="240" colspan="2">&nbsp;&nbsp;<asp:button id="btnFind" runat="server" Width="56px" Text="Cari"></asp:button></td>
																			</TR>
                                                                            
                                                                            <tr id="question2" runat="server">
                                                                                <td class="welcomeLogin" width="160">
                                                                                    <asp:Label ID="lblQuestion2" runat="server">Nomor HP</asp:Label></td>
                                                                                <td width="5">:</td>
                                                                                <td width="240">
                                                                                    <asp:Label ID="lblPhoneNo" runat="server" Width="100px" MaxLength="50"></asp:Label></td>
                                                                            </tr>
																			
                                                                            <tr id="otpdiv" runat="server">
                                                                                <td colspan="3" align="center">
                                                                                    <%--<div id="otpdiv" runat="server" visible="false">--%>
                                                                                    <table>

                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <OTPPage:OTPPAGE id="OTPPage" runat="server" ActivityType="ResetToken"></OTPPage:OTPPAGE>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <asp:Label ID="showtime" runat="server" Text="showtime"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <asp:Button ID="btnReset" runat="server" Width="200px" Text="Kirim Ulang Kode OTP" CausesValidation="false" OnClientClick="kosong();"></asp:Button></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <%--</div>--%>
                                                                                </td>
                                                                            </tr>
																			<%--<TR id="question1" runat="server">
																				<TD class="welcomeLogin" width="160">
																					<asp:label id="lblQuestion1" runat="server">Pertanyaan 1</asp:label></TD>
																				<TD width="240" nowrap>:
																					<asp:textbox id="txtAnswer1" runat="server" Width="200px" MaxLength="50" TextMode="Password"></asp:textbox>
																					<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtAnswer1"></asp:RequiredFieldValidator></TD>
																			</TR>
																			<TR id="question2" runat="server">
																				<TD class="welcomeLogin" width="160">
																					<asp:label id="lblQuestion2" runat="server">Pertanyaan 2</asp:label></TD>
																				<TD width="240">:
																					<asp:textbox id="txtAnswer2" runat="server" Width="200px" TextMode="Password" MaxLength="50"></asp:textbox>
																					<asp:RequiredFieldValidator id="Requiredfieldvalidator4" runat="server" ControlToValidate="txtAnswer2" ErrorMessage="*"></asp:RequiredFieldValidator></TD>
																			</TR>--%>
																		</table>
																	</td>
																	<TD height="100" align="center"><br>
																		<br>
																		<asp:Image id="ImgCaptcha" runat="server" Width="120px" Height="32px"></asp:Image><br>
																		<asp:textbox id="CodeNumberTextBox" onmouseover="return escape('Silahkan tulis ulang huruf-huruf yang tertera pada gambar ke textbox ini.')"
																			runat="server" Width="88px"></asp:textbox>
																		<asp:requiredfieldvalidator id="Requiredfieldvalidator5" runat="server" ErrorMessage="*" ControlToValidate="CodeNumberTextBox"></asp:requiredfieldvalidator></TD>
																</tr>
																<TR>
																	<TD height="60">&nbsp;</TD>
																	<TD height="60"></TD>
																</TR>
															</table>
															<SCRIPT src="./WebResources/wz_tooltip.js" type="text/javascript"></SCRIPT>
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr height="23">
											<td background="images/login_bg.gif" colSpan="2" height="23"><IMG height="1" src="images/dot.gif" width="1" border="0"></td>
										</tr>
										<tr height="44">
											<td background="images/login_bg.gif" align="right">
												<table cellSpacing="1" width="70%" border="0">
													<tr>
														<td align="center"><IMG src="images/new_logo_login2.gif" border="0"></td>
													</tr>
												</table>
											</td>
											<td align="right">
												<table cellSpacing="1" width="80%" border="0">
													<tr>
														<td align="center">
															<asp:LinkButton runat="server"><label id="lblHome" tabindex="0" onclick="GotoHome();" style="color:blue">Kembali Ke Halaman Login</label></asp:LinkButton>
                                                                                <asp:TextBox ID="txtHome" Style="display: none" runat="server"></asp:TextBox>
														</td>
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
								<td width="20%" align="center">
									<B>Tampilan Terbaik IE 6.0+ : 1024 x 768</B>
									<br>
									<table height="60" cellSpacing="0" cellPadding="0" width="789" border="0">
										<tr>
											<td align="center">PT Mitsubishi Motors Krama Yudha Sales Indonesia, Hak Cipta 
												Dilindungi Undang-Undang.<br>
												Jl. Jend A. Yani Proyek Pulomas, Jakarta.
												<br>
												Help Desk D-NET : (021) 4786-7575 atau <a class="menuLeft" href="mailto:admin.d-net@ktb.co.id">
													admin.d-net@ktb.co.id</a>
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
