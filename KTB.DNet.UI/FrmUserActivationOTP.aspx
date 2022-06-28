<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmUserActivationOTP.aspx.vb" Inherits="FrmUserActivationOTP" smartNavigation="False" %>
<%@ Register  TagPrefix = "OTPPage" TagName = "OTPPage" Src = "OTP.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Form OTP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link href="/WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="./WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="./WebResources/InputValidation.js"></script>
      <script language="javascript" type="text/javascript" src="WebResources/GlobalVar.js"></script>
    <script language="javascript">
      
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

        function EmptyControl()
        {
            document.getElementById("OTPPage_txtKodeOTP").value = '';
            document.getElementById("OTPPage_lblAlert").innerHTML = '';
        }

    </script>
</head>
<body onload="f1()">
    <form id="Form1" method="post" runat="server">
        <input type="hidden" id="HdTime" runat="server" value="" />
        <input type="hidden" id="HdMin" runat="server" value="" />
        <input type="hidden" id="HdSec" runat="server" value="" />
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0" valign="middle">
            <tr>
                <td valign="middle">
                    <table height="380" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cc0001" border="0" style=" background-image:url(images/bg-diamond-red.jpg);   background-position:center top;  "
							id="Table2">
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
                                        <td align="center" background="images/login_bg.gif" colspan="2" height="94">
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tr>
                                                    <td align="left" width="25%">
                                                        <img hspace="10" src="images/new_mits.gif" border="0"></td>
                                                    <td align="center" width="50%">
                                                        <h2><strong>
                                                            <asp:Label ID="lblKonfirmasiHeader" runat="server"></asp:Label></strong></h2>
                                                    </td>
                                                    <td align="right" width="25%">
                                                        <img hspace="10" src="images/new_fuso.gif" border="0"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr height="144">
                                        <td valign="middle" align="center" background="images/login_bg.gif" colspan="2" height="144">
                                            <table height="144" cellspacing="0" cellpadding="0" width="787" align="center" border="0">
                                                <tr>
                                                    <td width="25%" bgcolor="#cccccc">&nbsp;</td>
                                                    <td width="50%" align="center" bgcolor="#cccccc">
                                                        <br>
                                                        <table height="160" cellspacing="1" width="460" align="center" border="0">
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td height="100">
                                                                    <table border="0" align="center">
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:Label ID="lblConfirmationMessage" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <OTPPage:OTPPAGE id="OTPPage" runat="server"></OTPPage:OTPPAGE>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txtTimer" onblur="omitSomeCharacter('txtTimer','<>?*%$;')" runat="server" Style="display: none" AutoPostBack="true"></asp:TextBox>
                                                                                <asp:Label ID="showtime" runat="server" Text="showtime"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:Button ID="btnReset" runat="server" Width="180px" Text="Kirim Ulang Kode OTP" CausesValidation="False" OnClientClick="EmptyControl();"></asp:Button>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                 <asp:LinkButton runat="server"><label id="lblHome" tabindex="0" onclick="GotoHome();" style="color:blue">Kembali Ke Halaman Login</label></asp:LinkButton>
                                                                                <asp:TextBox ID="txtHome" Style="display: none" runat="server"></asp:TextBox>
                                                                                <%--<asp:LinkButton ID="lbtnHome" runat="server" CausesValidation="false" OnClientClick>Kembali Ke Halaman Login</asp:LinkButton>--%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="60">&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="center" valign="top" width="25%" bgcolor="#cccccc">
                                                        <br>
                                                        <br>
                                                        &nbsp;</td>
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
                                                        <img src="images/new_logo_login3.gif" border="0"></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="right">&nbsp;
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
                            <td align="center" width="20%"><b>
                            Tampilan Terbaik IE 6.0+ : 1024 x 768th="100%" border="0">
							<tr valign="top">
                                <td width="40%">&nbsp;</td>
                                <td align="center" width="20%"><b>Tampilan Terbaik IE 6.0+ : 1024 x 768</b>
                                    <br>
                                    <table height="60" cellspacing="0" cellpadding="0" width="789" border="0">
                                        <tr>
                                            <td align="center">© 2006, PT Krama Yudha Tiga Berlian Motors, Hak Cipta 
												Dilindungi Undang-Undang.<br>
                                                Jl. Jend A. Yani Proyek Pulomas, Jakarta.
												<br>
                                                Help Desk D-NET : (021) 4786-7575 atau <a class="menuLeft" href="mailto:admin.d-net@ktb.co.id">admin.d-net@ktb.co.id</a>
                                                <br>
                                                <a onclick="window.open('eula2.html','disclaimer','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=yes,width=700,height=500'); return false;"
                                                    href="#javascript">Syarat dan Ketentuan</a>
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
</html>
