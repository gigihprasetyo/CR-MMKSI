<%@ Register  TagPrefix = "OTPPage" TagName = "OTPPage" Src = "OTP.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmUserChangePhoneNoOTP.aspx.vb" Inherits="FrmUserChangePhoneNoOTP" smartNavigation="False" %>
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
    <script src="WebResources/jquery-1.10.2.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

        });
        $(function () {
            $("#txtPhoneNo1").keyup(function () {
                if ($('#txtPhoneNo1').val().length == 4) {
                    $('#txtPhoneNo2').focus();
                }
            });
            $("#txtPhoneNo2").keyup(function () {
                if ($('#txtPhoneNo2').val().length == 4) {
                    $('#txtPhoneNo3').focus();
                }
            }
            );
        });
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

    </script>

    <style type="text/css">
        .auto-style1 {
            width: 3px;
        }
    </style>
</head>
<body onload="f1()" id="test">
    <form id="Form1" method="post" runat="server">
        <input type="hidden" id="HdTime" runat="server" value="" />
        <input type="hidden" id="HdMin" runat="server" value="" />
        <input type="hidden" id="HdSec" runat="server" value="" />
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0" valign="middle">
            <tr>
                <td valign="middle">
                    <table height="380" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>

                            <td width="20%">
                                <table height="386" cellspacing="0" cellpadding="0" width="789" border="0">


                                    <tr height="144">
                                        <td valign="middle" align="center" colspan="2" height="144">
                                            <table height="144" cellspacing="0" cellpadding="0" width="787" align="center" border="0">
                                                <tr>

                                                    <td width="50%" align="center">

                                                        <table height="160" cellspacing="1" width="460" align="center" border="0">

                                                            <tr>
                                                                <td height="100">
                                                                    <table border="0" align="center">
                                                                        <tr>
                                                                            <td align="center">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td align="center">
                                                                                            <div runat="server" id="divPhoneNumber">
                                                                                                <table>
                                                                                                    <tr>
                                                                                                        <td>Nomor HP Lama</td>
                                                                                                        <td class="auto-style1">:</td>
                                                                                                        <td valign="top">
                                                                                                            <asp:Label ID="lblNomorHPLama" runat="server"></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>Nomor HP Baru </td>
                                                                                                        <td class="auto-style1">:</td>
                                                                                                        <td valign="top">
                                                                                                            <asp:TextBox ID="txtPhoneNo1" NAME="txtPhoneNo1" runat="server" onkeypress="return numericOnlyUniv(event)" onblur="numericOnlyBlur(txtPhoneNo1)" Width="40px" MaxLength="4" ClientIDMode="static"></asp:TextBox>
                                                                                                            <asp:Label ID="Label1" Text="-" runat="server"></asp:Label>
                                                                                                            <asp:TextBox ID="txtPhoneNo2" runat="server" onkeypress="return numericOnlyUniv(event)" onblur="numericOnlyBlur(txtPhoneNo2)" Width="40px" MaxLength="4" ClientIDMode="static"></asp:TextBox>
                                                                                                            <asp:Label ID="Label2" Text="-" runat="server"></asp:Label>
                                                                                                            <asp:TextBox ID="txtPhoneNo3" runat="server" onkeypress="return numericOnlyUniv(event)" onblur="numericOnlyBlur(txtPhoneNo3)" Width="60px" MaxLength="6"></asp:TextBox>
                                                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPhoneNo1" ErrorMessage="**"></asp:RequiredFieldValidator>
                                                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Format" ValidationExpression="^[0-9]$" ControlToValidate="txtPhoneNo1"></asp:RegularExpressionValidator>
                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPhoneNo2" ErrorMessage="**"></asp:RequiredFieldValidator>
                                                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Format" ValidationExpression="^[0-9]$" ControlToValidate="txtPhoneNo2"></asp:RegularExpressionValidator>
                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPhoneNo3" ErrorMessage="**"></asp:RequiredFieldValidator>
                                                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Format" ValidationExpression="^[0-9]$" ControlToValidate="txtPhoneNo3"></asp:RegularExpressionValidator>--%>

                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="3" align="center">
                                                                                            <asp:Button ID="btnSimpan" runat="server" Text="Simpan"></asp:Button></td>
                                                                                    </tr>
                                                                                </table>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <div id="otpdiv" runat="server" visible="false">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <OTPPage:OTPPAGE id="OTPPage" runat="server" ActivityType="ChangePhoneNumber"></OTPPage:OTPPAGE>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <asp:TextBox ID="txtTimer" onblur="omitSomeCharacter('txtTimer','<>?*%$')" runat="server" Style="display: none" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                                                                                                <asp:Label ID="showtime" runat="server" Text="showtime"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <asp:Button ID="btnReset" runat="server" Width="180px" Text="Kirim Ulang Kode OTP" CausesValidation="False"></asp:Button>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <asp:Button ID="btnBack" runat="server" Width="60px" Text="Kembali" CausesValidation="False"></asp:Button>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>

                                                                            </td>
                                                                        </tr>


                                                                    </table>
                                                                </td>
                                                            </tr>

                                                        </table>
                                                    </td>

                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
                    </table>

                </td>
            </tr>
        </table>
    </form>

</body>
</html>
