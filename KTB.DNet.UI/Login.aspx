<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="Login" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
<!-- Global site tag (gtag.js) - Google Analytics -->
<script async src="https://www.googletagmanager.com/gtag/js?id=UA-166094357-1"></script>
<script>
    window.dataLayer = window.dataLayer || [];
    function gtag() { dataLayer.push(arguments); }
    gtag('js', new Date());

    gtag('config', 'UA-166094357-1');
</script>
    <title>Halaman Login</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <link href="WebResources/BasicStyle.css" type="text/css" rel="stylesheet">



    <script language="javascript">

        if (getParameter(document.URL, "Msg") == "GetOut") {
            window.open("Login.aspx?msg=Expired", "_top");
        }

        function getParameter(queryString, parameterName) {
            var parameterName = parameterName + "=";
            if (queryString.length > 0) {
                begin = queryString.indexOf(parameterName);
                if (begin != -1) {
                    begin += parameterName.length;
                    end = queryString.indexOf("&", begin);
                    if (end == -1) {
                        end = queryString.length
                    }
                    return unescape(queryString.substring(begin, end));
                }
                return "null";
            }
        }


        function OpenFullScreenWindow(Address) {
            newwin = window.open(Address, "_blank", "fullscreen=no,titlebar=no,personalbar=no,toolbar=no,status=1,menubar=no,scrollbars=yes,resizable=yes,directories=no,location=no");
            this.name = "origWin";
            origWin = window.open("login.aspx", "origWin");
            window.opener = top;
            window.close();
        }

        function HandleValidator() {
            var DealerValidator = document.getElementById("DealerValidator");
            var UnameValidator = document.getElementById("UnameValidator");
            var PwdValidator = document.getElementById("PwdValidator");
            var Result = true;

            if (trim(document.FormLogin.DealerTextBox.value) == "")
            { DealerValidator.innerHTML = "*"; Result = false; }
            else
            { DealerValidator.innerHTML = ""; }

            if (trim(document.FormLogin.NameTextBox.value) == "")
            { UnameValidator.innerHTML = "*"; Result = false; }
            else
            { UnameValidator.innerHTML = ""; }

            if (trim(document.FormLogin.PasswordTextBox.value) == "")
            { PwdValidator.innerHTML = "*"; Result = false; }
            else
            { PwdValidator.innerHTML = ""; }

            return Result;
        }

        function trim(input) {
            var hsl = "";
            hsl = ltrim(input);
            hsl = rtrim(hsl);
            return hsl;
        }

        function ltrim(input) {
            isiinput = input;
            buang = 0;
            for (i = 0; i < input.length; i++)
                if (input.charAt(i) == " ") buang++;
                else i = input.length + 1;
            if (buang > 0 & input.length > buang) {
                isiinput = input.slice(buang, input.length);
                buang = 0;
            }
            if (buang == input.length) isiinput = "";
            return isiinput;
        }

        function rtrim(input) {
            isiinput = input;
            buang = 0;
            for (i = isiinput.length - 1; i >= 0; i--)
                if (isiinput.charAt(i) == " ") buang++;
                else i = -1;
            if (buang > 0 & isiinput.length > buang) {
                isiinput = isiinput.slice(0, isiinput.length - buang);
                buang = 0;
            }
            if (buang == isiinput.length) isiinput = "";
            return isiinput;
        }

        function HtmlCharUniv(event) {
            if (navigator.appName == "Microsoft Internet Explorer")
                pressedKey = event.keyCode;
            else
                pressedKey = event.which

            if (pressedKey != 60 && pressedKey != 62 && pressedKey != 39) {
                return true;
            }
            else {
                return false;
            }
        }

        document.oncontextmenu = new Function("return false");

    </script>
    <script>
        function CloseMe() {
            window.location.href = "a.html";
            //alert('silahkan menggunakan aplikasi yg sdh terbuka');window.opener=self;window.close();
        }
    </script>
</head>
<body leftmargin="0" topmargin="0" onload="document.FormLogin.DealerTextBox.focus();">
    <form id="FormLogin" method="post" runat="server" width="100%" autocomplete="off">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0" valign="middle">
            <tr>
                <td valign="middle">
                    <table height="380" cellspacing="0" cellpadding="0" width="100%" bgcolor="#cc0001" border="0" style="background-image: url(images/bg-diamond-red.jpg); background-position: center top;">
                        <tr>
                            <td width="40%">&nbsp;</td>
                            <td width="20%" background="images/login_bg.gif">
                                <table height="386" cellspacing="0" cellpadding="0" width="789" border="0">
                                    <tr height="74">
                                        <td width="612" height="74">
                                            <img src="images/login_r1_c1.gif" border="0"></td>
                                        <td width="177">
                                            <img src="images/new_login_r1_c2.gif" border="0"></td>
                                    </tr>
                                    <tr height="81">
                                        <td align="center" background="images/login_bg.gif" colspan="2" height="95">
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tr>
                                                    <%--	<td align="left" width="25%"><IMG hspace="10" src="images/new_mits.gif" border="0"></td>--%>
                                                    <td align="left" width="75%"><span style="padding-left: 20px;"><strong><font face="Verdana" size="5">Selamat Datang</font></strong></span></td>
                                                    <td align="right" width="25%">
                                                        <img hspace="10" src="images/new_mits.gif" border="0"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr height="144">
                                        <td valign="middle" align="center" background="images/login_bg.gif" colspan="2" height="144">
                                            <table height="144" cellspacing="0" cellpadding="0" width="787" align="center" border="0">
                                                <tr>
                                                    <td width="25%" bgcolor="#cccccc">&nbsp;</td>
                                                    <td align="center" width="50%" bgcolor="#cccccc">
                                                        <table height="160" cellspacing="1" width="460" align="center" border="0">
                                                            <tr>
                                                                <td colspan="3"></td>
                                                            </tr>
                                                            <tr>
                                                                <td height="100">
                                                                    <table cellspacing="1" width="460" align="center" border="0">
                                                                        <tr>
                                                                            <td class="welcomeLogin" width="33%">Kode Organisasi</td>
                                                                            <td class="welcomeLogin" width="33%">Nama</td>
                                                                            <td class="welcomeLogin" width="33%">Kata Kunci</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="DealerTextBox" runat="server" CssClass="welcomeLogin"
                                                                                    Width="130px" autocomplete="off" onselect="this.value='';"></asp:TextBox><font color="red"><span id="DealerValidator"></span></font></td>
                                                                            <td>
                                                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="NameTextBox" runat="server" CssClass="welcomeLogin"
                                                                                    Width="130" autocomplete="off" onselect="this.value='';"></asp:TextBox><font color="red"><span id="UnameValidator"></span></font></td>
                                                                            <td>
                                                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="PasswordTextBox" runat="server" CssClass="welcomeLogin"
                                                                                    Width="130" TextMode="Password" autocomplete="off" onselect="this.value='';"></asp:TextBox><font color="red"><span id="PwdValidator"></span></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" height="40">&nbsp;<asp:Label class="welcomeLogin" ID="lblMessage" runat="server" Font-Bold="True"></asp:Label></td>
                                                                        </tr>
                                                                    </table>
                                                                    <asp:Panel ID="pnlAnnouncement" runat="server" Height="31px">
                                                                        <asp:Label ID="lblAnnouncement" runat="server"></asp:Label>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top" align="center" width="25%" bgcolor="#cccccc">
                                                        <table cellpadding="10" width="100%" border="0">
                                                            <tr>
                                                                <td valign="top" align="center">
                                                                    <br>
                                                                    <asp:Literal ID="LtrTime" runat="server"></asp:Literal><asp:Image ID="PCImage" runat="server" Width="70px" Height="70px"></asp:Image><br>
                                                                    <asp:LinkButton ID="lnkImage" runat="server" CausesValidation="False">Upload Gambar Pilihan 1</asp:LinkButton></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr height="23">
                                        <td background="images/login_bg.gif" colspan="2" height="23">
                                            <img height="1" src="images/dot.gif" width="1" border="0"></td>
                                    </tr>
                                    <tr height="44">
                                        <td align="right" background="images/login_bg.gif">
                                            <table cellspacing="1" width="70%" border="0">
                                                <tr>
                                                    <td align="center">
                                                        <%--  <div style="text-align:center">
                                                                  <span style="font-family:Arial; font-size:12px; font-weight:bold;">Mitsubishi Motors authorized distributor<br/></span>
                                                                 <span style="font-family:Arial; font-size:16px; font-weight:bold;">PT Mitsubishi Motors Krama Yudha Sales Indonesia</span>
                                                            </div>--%>

                                                        <img src="images/new_logo_login2.gif" border="0">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="right">
                                            <table cellspacing="1" width="80%" border="0">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Button ID="LoginButton" runat="server" Width="56px" Text="Login"></asp:Button><br>
                                                        <asp:LinkButton ID="lbtnForget" runat="server" CausesValidation="False">Lupa Kata Kunci?</asp:LinkButton></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr height="20">
                                        <td colspan="2" height="20">
                                            <img src="images/login_r4.gif" border="0"></td>
                                    </tr>
                                </table>
                            </td>
                            <td width="40%">&nbsp;</td>
                        </tr>
                    </table>
                    <table height="80" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr valign="top">
                            <td width="40%">&nbsp;</td>
                            <td align="center" width="20%">
                                <p>
                                    <b>Browser yang dapat digunakan adalah Internet Explorer (versi 11 atau kurang) dan Mozilla Firefox
                                        <br>
                                        Pop Up Blocker harap dalam posisi non-aktif
                                        <br>
                                    </b>
                                    <asp:Label ID="lblDonwloadPath" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lbtnDownloadUserManualLogin" runat="server">Download User Manual Login D-Net</asp:LinkButton>
                                </p>
								<span style="color:white">web 1</span>
                                <table height="60" cellspacing="0" cellpadding="0" width="789" border="0">
                                    <tr>
                                        <td align="center">PT Mitsubishi Motors Krama Yudha Sales Indonesia, Hak Cipta 
												Dilindungi Undang-Undang.<br>
                                            Jl. Jend A. Yani Proyek Pulomas, Jakarta.
												<br>
                                            Help Desk D-NET : (021) 4786-7575 atau <a class="menuLeft" href="mailto:admin.d-net@mitsubishi-motors.co.id">admin.d-net@mitsubishi-motors.co.id</a>
                                            <br>
                                            <a href="https://wa.me/628121034312" target="_blank">D-Net Whatsapp Support : 0812-1034-312</a>
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
        <div style="display: none">
            <asp:Label ID="lblScript" runat="server" Text="alert('Browser Anda tidak support untuk membuka dua jendela aplikasi DNet.');window.location.href='FrmAccessDenied.aspx?mess=Browser Anda tidak support untuk membuka dua jendela aplikasi DNet.';"></asp:Label>
        </div>
    </form>
</body>
</html>

                                        