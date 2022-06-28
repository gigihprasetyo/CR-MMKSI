<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.Menu" Assembly="KTB.DNet.Menu" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default_cc.aspx.vb" Inherits="default_cc" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>PT Mitsubishi Motors Krama Yudha Sales Indonesia - General</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <link href="WebResources/BasicStyle.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="./WebResources/AjaxDelegate.js"></script>
    <script type='text/javascript'>
        function OpenNewWindow(Address) {
            newwin = window.open(Address, null, "width=700,height=600,fullscreen=no,toolbar=yes,status=yes,menubar=yes,scrollbars=yes,resizable=yes,directories=yes,location=yes");
            window.opener = "";
            window.close();
        }
        document.oncontextmenu = new Function("return false");
    </script>
    <script language="JavaScript" type="text/javascript">

        var currentTime = new Date(<%=getServerDateItems()%>);
        function updateClock() {
            var currentHours = currentTime.getHours();
            var currentMinutes = currentTime.getMinutes();
            var currentSeconds = currentTime.getSeconds() + 1;
            if (currentSeconds == 60) {
                currentSeconds = 0;
                currentMinutes = currentMinutes + 1;
                if (currentMinutes == 60) {
                    currentMinutes = 0;
                    currentHours = currentHours + 1;
                }
            }
            currentTime = new Date(currentTime.getYear(), currentTime.getMonth(), currentTime.getDate(), currentHours, currentMinutes, currentSeconds);
            currentMinutes = (currentMinutes < 10 ? "0" : "") + currentMinutes;
            currentSeconds = (currentSeconds < 10 ? "0" : "") + currentSeconds;
            var timeOfDay = (currentHours < 12) ? "AM" : "PM";
            currentHours = (currentHours > 12) ? currentHours - 12 : currentHours;
            currentHours = (currentHours == 0) ? 12 : currentHours;

            var currentTimeString = currentHours + ":" + currentMinutes + ":" + currentSeconds + " " + timeOfDay;
            document.getElementById("ClockTime").firstChild.nodeValue = currentTimeString;
        }
    </script>
</head>
<body topmargin="0" bottommargin="0" rightmargin="0" leftmargin="0" onload="updateClock(); setInterval('updateClock()', 1000 );pageLoad();">
    <form id="Form1" method="post" runat="server">
        <table width="100%" height="100%" valign="middle" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td valign="top" height="140">
                    <!-- start Header -->
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="74">
                                <table height="74" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="153" height="30">
                                            <img src="images/cc_r1_c1.gif"></td>
                                        <td width="163" height="30">
                                            <img src="images/cc_r1_c2.gif"></td>
                                        <td width="152" height="30" background="images/cc_r1_c3_bg.gif">
                                            <img src="images/cc_r1_c3_bg.gif"></td>
                                        <td width="115" height="30" background="images/cc_r1_c3_bg.gif">
                                            <img src="images/cc_r1_c3_bg.gif"></td>
                                        <td width="442" height="30" align="left">
                                            <img src="images/04cc_r1_c4.gif"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img src="images/cc_r2_c1.jpg"></td>
                                        <td>
                                            <img src="images/cc_r2_c2.jpg"></td>
                                        <td>
                                            <img src="images/cc_r2_c3.jpg"></td>
                                        <td background="images/04mrk_r2_c3_bg.gif">
                                            <img src="images/04mrk_r2_c3_bg.gif"></td>
                                        <td align="right">
                                            <img src="images/04mrk_r2_c4.gif"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" height="53" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="200" height="53" background="images/04mrk_r3_c1_bg.gif">
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tr>
                                                    <td colspan="2" height="30">
                                                        <img src="images/blank.gif" width="1" height="30" border="0"></td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" nowrap width="130">
                                                        <asp:Label ID="CalenderLabel" runat="server" Width="140px">Kalender</asp:Label></td>
                                                    <td valign="top" nowrap width="70" align="right"><span id="ClockTime"><%=clockTimeString(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)%></span></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="42" height="53" background="images/04mrk_r3_c2.gif" align="right">
                                            <img height="53" width="42" src="images/04mrk_r3_c2.gif" border="0"></td>
                                        <td background="images/04mrk_r3_c3_bg.gif" height="53">
                                            <table cellspacing="0" cellpadding="0" width="740" border="0">
                                                <tr>
                                                    <td width="29" style="padding-left: 11px;">
                                                        <img src="images/04icon_general.gif" border="0"></td>
                                                    <td class="menuHor">
                                                        <asp:LinkButton class="menuHor" ID="GeneralLinkButton" runat="server">General</asp:LinkButton></td>
                                                    <td width="29" style="padding-left: 11px;">
                                                        <img src="images/04icon_sales.gif" border="0"></td>
                                                    <td class="menuHor">
                                                        <asp:LinkButton class="menuHor" ID="SalesLinkButton" runat="server" Enabled="False">
																<b>Sales</b></asp:LinkButton></td>
                                                    <td width="34" style="padding-left: 6px;">
                                                        <img src="images/04icon_service.gif" border="0"></td>
                                                    <td class="menuHor">
                                                        <asp:LinkButton class="menuHor" ID="ServiceLinkButton" runat="server" Enabled="False">Service</asp:LinkButton></td>
                                                    <td width="29" style="padding-left: 13px;">
                                                        <img src="images/04icon_parts_02.gif" border="0"></td>
                                                    <td class="menuHor">
                                                        <asp:LinkButton class="menuHor" ID="SparepartLinkButton" runat="server">Parts</asp:LinkButton></td>
                                                    <td width="24" style="padding-left: 14px;">
                                                        <img src="images/04icon_rsd_02.gif" border="0"></td>
                                                    <td class="menuHor">
                                                        <asp:LinkButton class="menuHor" ID="RsdLinkButton" runat="server">SFD</asp:LinkButton></td>
                                                    <td width="25" style="padding-left: 8px;">
                                                        <img src="images/04icon_promo2.gif" border="0"></td>
                                                    <td class="menuHor">
                                                        <asp:LinkButton class="menuHor" ID="PromoLinkButton" runat="server">Promotion</asp:LinkButton></td>
                                                    <td width="34" style="padding-left: 12px;">
                                                        <img src="images/04icon_mrk2.gif" border="0"></td>
                                                    <td class="menuHor">
                                                        <asp:LinkButton class="menuHor" ID="MrkLinkButton" runat="server" Enabled="False">Marketing</asp:LinkButton></td>
                                                    <td width="34" style="padding-left: 11px;">
                                                        <img src="images/iconCC.gif" border="0"></td>
                                                    <td nowrap class="menuHor">
                                                        <asp:LinkButton class="menuHor2" ID="lbtnCallCenter" runat="server" Enabled="True" Width="68px">Customer Satisfaction</asp:LinkButton></td>
                                                    <td width="34" style="padding-left: 8px;">
                                                        <img src="images/04icon_aftersales.gif" border="0"></td>
                                                    <td class="menuHor">
                                                        <asp:LinkButton class="menuHor" ID="AfterSalesLinkButton" runat="server" Enabled="True" Width="68px">After Sales</asp:LinkButton></td>
                                                    <td width="24" style="padding-left: 14px;">
                                                        <img src="images/04icon_rsd_02.gif" border="0"></td>
                                                    <td class="menuHor">
                                                        <asp:LinkButton class="menuHor" ID="lbtnTraining" runat="server">Training</asp:LinkButton></td>



                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <!-- end Header -->
                </td>
            </tr>
            <tr valign="top">
                <td width="90%" valign="top">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" height="100%">
                        <tr valign="top" height="99%">
                            <td rowspan="2" width="200" height="99%" bgcolor="#e6e6e6" valign="top" align="center">
                                <!-- start Left -->
                                <table align="center" width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr align="center">
                                        <td height="50" align="center">
                                            <table width="192" border="0" align="center" cellspacing="6" cellpadding="0" height="50">
                                                <tr>
                                                    <td height="50">Selamat Datang,&nbsp;
															<br>
                                                        <asp:Label ID="labelUser" runat="server" Font-Bold="True"></asp:Label><br>
                                                        <asp:Label ID="LabelNamaDealer" runat="server" Font-Bold="True"></asp:Label><br>
                                                        <asp:Label ID="LabelSearchTerm" runat="server" Font-Bold="True"></asp:Label><br>
                                                        <asp:Label ID="LabelDealer" runat="server" Font-Bold="True"></asp:Label>
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True">
																<br />
																<br />
																<b>Token Anda valid sampai</b></asp:Label><br>
                                                        <asp:Label ID="lblvaliduntil" runat="server" Font-Bold="True"></asp:Label>
                                                        <asp:Panel ID="pnlEmailValidator" runat="server">
                                                            <br>
                                                        </asp:Panel>
                                                        <asp:Label ID="lblStatus" runat="server" Font-Bold="True"></asp:Label>
                                                        <br>
                                                        <br>
                                                        <asp:LinkButton ID="UbahDataLinkButton" runat="server" CssClass="red">Ubah Data</asp:LinkButton>&nbsp;&nbsp;&nbsp;
															<asp:LinkButton ID="LogoutLinkButton" runat="server" CssClass="red">Keluar</asp:LinkButton>&nbsp;&nbsp;&nbsp;
															<asp:LinkButton ID="lnkSecurity" runat="server" CssClass="red">Security</asp:LinkButton>&nbsp;<img src="images/star2.gif" border="0" id="imgStar" runat="server">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr height="1">
                                        <td height="1" bgcolor="#666666">
                                            <img src="images/dot.gif" border="0" height="1" width="1"></td>
                                    </tr>
                                    <tr valign="top">
                                        <td valign="top" height="80" align="center">
                                            <table width="98%" border="0" cellspacing="2" cellpadding="0">
                                                <tr>
                                                    <td valign="top">
                                                        <div id="div1" style="overflow: auto; height: 380px">
                                                            <cc1:TreeMenu id="MyMenu" runat="server" XMLPath="TreeMenu.XML.PathFromRootFU" ScreenId="9000"></cc1:TreeMenu>
                                                        </div>
                                                        <div id="divMusic" style="display: none"></div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <!-- end Left -->
                            </td>
                            <td width="100%" height="99%" valign="top">
                                <!-- start Content -->
                                <table width="100%" height="500" border="0" cellspacing="6" cellpadding="0">
                                    <tr>
                                        <td valign="top">
                                            <iframe id="frMain" style="width: 100%; height: 100%" name="frMain" src="<%= MainFrameSrc%>" frameborder="no" width="100%" height="100%"></iframe>
                                        </td>
                                    </tr>
                                </table>
                                <!-- end Content -->
                            </td>
                        </tr>
                        <tr valign="top" height="20">
                            <!--<td width="200" height="20" bgcolor="#e6e6e6">&nbsp;</td>-->
                            <td width="100%" height="20" valign="top">
                                <!-- start Footer -->
                                <table width="100%" border="0" cellspacing="1" cellpadding="0">
                                    <tr>
                                        <td valign="top" align="left" width="20%" style="padding-left: 14px;">
                                            <img src="images/oim.png" border="0"></td>
                                        <td valign="bottom" nowrap align="center" width="70%"><b><a style="color: #000000; text-decoration: none" href="http://www.mitsubishi-motors.co.id" target="_blank">PT Mitsubishi Motors Krama Yudha Sales Indonesia,</a></b> Hak Cipta Dilindungi 
												Undang-Undang.<br>
                                            <asp:LinkButton ID="LnkTerms" runat="server">Syarat dan Ketentuan</asp:LinkButton>
                                        </td>
                                        <td valign="top" align="right" width="20%">
                                            <img src="images/new_fuso.gif" border="0"></td>
                                    </tr>
                                </table>
                                <!-- end Footer -->
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
    <script language="javascript">
        var IntervalID;
        function handleServerResponse(url, response) {
            if (response.length > 1500) {
                window.clearInterval(intervalID);
            }
            if (response != '' && response != '[]' && response.length < 1500) {
                alert(response);
            }
        }
        function sendRequestToServer() {
            var url = "AlertManagement.ashx";
            var ajax = new AjaxDelegate(url, handleServerResponse);
            ajax.Fetch();
        }
        function pageLoad() {
            sendRequestToServer();
            IntervalID = window.setInterval(sendRequestToServer, 120000);
        }
    </script>
</body>
</html>
