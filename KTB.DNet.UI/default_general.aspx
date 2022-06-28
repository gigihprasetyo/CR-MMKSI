<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.Menu" Assembly="KTB.DNet.Menu" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default_general.aspx.vb" Inherits="default_general" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>PT Mitsubishi Motors Krama Yudha Sales Indonesia - General</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <link href="WebResources/BasicStyle.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="./WebResources/AjaxDelegate.js"></script>
    <script type="text/javascript">
        function OpenNewWindow(Address) {
            newwin = window.open(Address, null, "width=700,height=600,fullscreen=no,toolbar=yes,status=yes,menubar=yes,scrollbars=yes,resizable=yes,directories=yes,location=yes");
            window.opener = "";
            window.close();
        }
        //   document.oncontextmenu = new Function("return false");
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
<body bottommargin="0" leftmargin="0" topmargin="0" onload="updateClock(); setInterval('updateClock()', 1000 );pageLoad();"
    rightmargin="0">
    <form id="Form1" method="post" runat="server">
        <table height="99%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="74">
                                <table height="74" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="153" height="30">
                                            <img src="images/general_r1_c1.gif"></td>
                                        <td width="163" height="30">
                                            <img src="images/general_r1_c2.gif"></td>
                                        <td width="152" height="30" background="images/general_r1_c3_bg.gif">
                                            <img src="images/general_r1_c3_bg.gif"></td>
                                        <td width="115" height="30" background="images/general_r1_c3_bg.gif">
                                            <img src="images/general_r1_c3_bg.gif"></td>
                                        <td width="442" height="30" align="left">
                                            <img src="images/04general_r1_c4.gif"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img src="images/general_r2_c1.jpg"></td>
                                        <td>
                                            <img src="images/general_r2_c2.jpg"></td>
                                        <td>
                                            <img src="images/general_r2_c3.jpg"></td>
                                        <td background="images/04mrk_r2_c3_bg.gif">
                                            <img src="images/04mrk_r2_c3_bg.gif"></td>
                                        <td align="right">
                                            <img src="images/04mrk_r2_c4.gif"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="53">
                                <table width="100%" height="53" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="200" height="53" background="images/04mrk_r3_c1_bg.gif">
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tr>
                                                    <td colspan="2" height="30">
                                                        <img height="30" src="images/blank.gif" width="1" border="0"></td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" nowrap width="130">
                                                        <asp:Label ID="CalenderLabel" runat="server" Width="140px">Kalender</asp:Label></td>
                                                    <td valign="top" nowrap align="right" width="70"><span id="ClockTime"><%=clockTimeString(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)%></span></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="42" height="53" background="images/04mrk_r3_c2.gif" align="right">
                                            <img height="53" width="42" src="images/04mrk_r3_c2.gif" border="0"></td>
                                        <td background="images/04mrk_r3_c3_bg.gif" height="53">
                                            <table width="740" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="29" style="padding-left: 11px;">
                                                        <img src="images/04icon_general.gif" border="0"></td>
                                                    <td class="menuHor">
                                                        <asp:LinkButton class="menuHor2" ID="GeneralLinkButton" runat="server"><b>General</b></asp:LinkButton></td>
                                                    <td width="29" style="padding-left: 11px;">
                                                        <img src="images/04icon_sales.gif" border="0"></td>
                                                    <td class="menuHor">
                                                        <asp:LinkButton class="menuHor" ID="SalesLinkButton" runat="server" Enabled="False">Sales</asp:LinkButton></td>
                                                    <td width="34" style="padding-left: 6px;">
                                                        <img src="images/04icon_service.gif" border="0"></td>
                                                    <td class="menuHor">
                                                        <asp:LinkButton class="menuHor" ID="ServiceLinkButton" runat="server" Enabled="False">Service</asp:LinkButton></td>
                                                    <td width="29" style="padding-left: 13px;">
                                                        <img src="images/04icon_parts_02.gif" border="0"></td>
                                                    <td class="menuHor">
                                                        <asp:LinkButton class="menuHor" ID="SparepartLinkButton" runat="server" Enabled="False">Parts</asp:LinkButton></td>
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
                                                        <asp:LinkButton class="menuHor" ID="MrkLinkButton" runat="server">Marketing</asp:LinkButton></td>

                                                    <td width="34" style="padding-left: 11px;">
                                                        <img src="images/iconCC.gif" border="0"></td>
                                                    <td nowrap class="menuHor">
                                                        <asp:LinkButton class="menuHor" ID="lbtnCallCenter" runat="server" Enabled="True" Width="68px">Customer Satisfaction</asp:LinkButton></td>

                                                    <td width="34" style="padding-left: 11px;">
                                                        <img id="imgTAF" src="images/icon_taf.gif" border="0" runat="server"></td>
                                                    <td class="menuHor">
                                                        <asp:LinkButton class="menuHor" ID="TAFLinkButton" runat="server">&nbsp;&nbsp;Leasing</asp:LinkButton></td>

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
                </td>
            </tr>
            <tr>
                <td valign="top" width="90%" height="97%">
                    <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr valign="top" height="99%">
                            <td rowspan="2" valign="top" align="center" width="200" bgcolor="#e6e6e6" height="601">
                                <!-- start Left -->
                                <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                    <tr align="center">
                                        <td align="center" height="50">
                                            <table height="50" cellspacing="6" cellpadding="0" width="191" align="center" border="0">
                                                <tr>
                                                    <td height="50">Selamat Datang,&nbsp;
															<br>
                                                        <asp:Label ID="labelUser" runat="server" Font-Bold="True"></asp:Label><br>
                                                        <asp:Label ID="LabelNamaDealer" runat="server" Font-Bold="True"></asp:Label><br>
                                                        <asp:Label ID="LabelSearchTerm" runat="server" Font-Bold="True"></asp:Label><br>
                                                        <asp:Label ID="LabelDealer" runat="server" Font-Bold="True"></asp:Label>
                                                        <asp:Label ID="lblTokenText" runat="server" Font-Bold="True">
																<br />
																<br />
																<b>Token Anda valid sampai</b></asp:Label><br>
                                                        <asp:Label ID="lblValidUntil" runat="server" Font-Bold="True"></asp:Label>
                                                        <asp:Panel ID="pnlEmailValidator" runat="server">
                                                            <br>
                                                        </asp:Panel>
                                                        <asp:Label ID="lblStatus" runat="server" Font-Bold="True"></asp:Label><br>
                                                        <br>
                                                        <asp:LinkButton ID="UbahDataLinkButton" runat="server" CssClass="red">Ubah Data</asp:LinkButton>&nbsp;
															<asp:LinkButton ID="LogoutLinkButton" runat="server" CssClass="red">Keluar</asp:LinkButton>&nbsp;
															<asp:LinkButton ID="lnkSecurity" runat="server" CssClass="red">Security</asp:LinkButton>&nbsp;
															<asp:LinkButton ID="lnkMaintenance" runat="server" CssClass="red">Checklist</asp:LinkButton>
                                                        <img src="images/star2.gif" border="0" id="imgStar" runat="server">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr height="1">
                                        <td bgcolor="#666666" height="1">
                                            <img height="1" src="images/dot.gif" width="1" border="0"></td>
                                    </tr>
                                    <tr valign="top">
                                        <td valign="top" align="center" height="80">
                                            <table cellspacing="2" cellpadding="0" width="98%" border="0">
                                                <tr>
                                                    <td valign="top">
                                                        <div id="div1" style="height: 380px; overflow: auto">
                                                            <cc1:treemenu id="MyMenu" runat="server" ScreenId="2300" XMLPath="TreeMenu.XML.PathFromRootFU"></cc1:treemenu>
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
                            <td valign="top" width="100%" height="601">
                                <!-- start Content -->
                                <table height="500" cellspacing="6" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td valign="top">
                                            <iframe id="frMain"
                                                style="width: 100%; height: 100%" name="frMain"
                                                src="<%= MainFrameSrc%>" frameborder="no" width="100%"
                                                height="100%"></iframe>
                                        </td>
                                    </tr>
                                </table>
                                <!-- end Content -->
                            </td>
                        </tr>
                        <tr valign="top" height="20">
                            <!--<td width="200" bgColor="#e6e6e6" height="20">&nbsp;</td>-->
                            <td valign="top" width="100%" height="20">
                                <!-- start Footer -->
                                <table cellspacing="1" cellpadding="0" width="100%" border="0">
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
                    <!-- end Footer -->
                </td>
            </tr>
        </table>
        </TD></TR></TBODY></TABLE>
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
