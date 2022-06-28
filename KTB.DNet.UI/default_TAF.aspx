<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.Menu" Assembly="KTB.DNet.Menu" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="default_TAF.aspx.vb" Inherits="default_TAF" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PT Tiga Berlian Auto Finance - TAF</title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<LINK href="js/stylesheet.css" rel="stylesheet">
			<LINK href="WebResources/stylesheet.css" type="text/css" rel="stylesheet">
				<script language="javascript" src="./WebResources/PreventNewWindow.js"></script>
				<script language="javascript" src="./WebResources/AjaxDelegate.js"></script>
				<script type="text/javascript">
			function OpenNewWindow(Address)
			{
				//if(document.location.search=='')  {
				newwin=window.open(Address,null, "width=700,height=600,fullscreen=no,toolbar=yes,status=yes,menubar=yes,scrollbars=yes,resizable=yes,directories=yes,location=yes");
				window.opener = "";
				window.close();	
				//}
			}
				</script>
				<script language="JavaScript" type="text/javascript">

var clockLocalStartTime = new Date();
var clockServerStartTime = new Date(<%=getServerDateItems()%>);


function simpleFindObj(name, inLayer) {
	return document[name] || (document.all && document.all[name])
		|| (document.getElementById && document.getElementById(name))
		|| (document.layers && inLayer && document.layers[inLayer].document[name]);
}

var clockIncrementMillis = 60000;
var localTime;
var clockOffset;
var clockExpirationLocal;
var clockShowsSeconds = false;
var clockTimerID = null;

function clockInit(localDateObject, serverDateObject)
{
    var origRemoteClock = parseInt(clockGetCookieData("remoteClock"));
    var origLocalClock = parseInt(clockGetCookieData("localClock"));
    var newRemoteClock = serverDateObject.getTime();
   
    var newLocalClock = localDateObject.getTime();
    var maxClockAge = 60 * 60 * 1000;   
    
    if (newRemoteClock != origRemoteClock) {
        
        document.cookie = "remoteClock=" + newRemoteClock;
        document.cookie = "localClock=" + newLocalClock;
        clockOffset = newRemoteClock - newLocalClock;
        clockExpirationLocal = newLocalClock + maxClockAge;
        localTime = newLocalClock;  
    }
    else if (origLocalClock != origLocalClock) {
        
        clockOffset = null;
        clockExpirationLocal = null;
    }
    else {
        
        clockOffset = origRemoteClock - origLocalClock;
        clockExpirationLocal = origLocalClock + maxClockAge;
        localTime = origLocalClock;
        
    }
    
    var nextDayLocal = (new Date(serverDateObject.getFullYear(),
            serverDateObject.getMonth(),
            serverDateObject.getDate() + 1)).getTime() - clockOffset;
    if (nextDayLocal < clockExpirationLocal) {
        clockExpirationLocal = nextDayLocal;
    }
}

function clockOnLoad()
{
    clockToggleSeconds();
}

function clockOnUnload() {
    clockClearTimeout();
}

function clockClearTimeout() {
    if (clockTimerID) {
        clearTimeout(clockTimerID);
        clockTimerID = null;
    }
}

function clockToggleSeconds()
{
    clockClearTimeout();
    if (clockShowsSeconds) {
        clockShowsSeconds = false;
        clockIncrementMillis = 60000;
    }
    else {
        clockShowsSeconds = true;
        clockIncrementMillis = 1000;
    }
    clockUpdate();
}

function clockTimeString(inHours, inMinutes, inSeconds) {
    return inHours == null ? "-:--" : ((inHours == 0
                   ? "12" : (inHours <= 12 ? inHours : inHours - 12))
                + (inMinutes < 10 ? ":0" : ":") + inMinutes
                + (clockShowsSeconds
                   ? ((inSeconds < 10 ? ":0" : ":") + inSeconds) : "")
                + (inHours < 12 ? " AM" : " PM"));
}

function clockDisplayTime(inHours, inMinutes, inSeconds) {
    
    clockWriteToDiv("ClockTime", clockTimeString(inHours, inMinutes, inSeconds));
}

function clockWriteToDiv(divName, newValue) 
{
    var divObject = simpleFindObj(divName);
    /*newValue = '<p>' + newValue + '<' + '/p>';*/
    if (divObject && divObject.innerHTML) {
        divObject.innerHTML = newValue;
    }
    else if (divObject && divObject.document) {
        divObject.document.writeln(newValue);
        divObject.document.close();
    }
    
}

function clockGetCookieData(label) {
    
    var c = document.cookie;
    if (c) {
        var labelLen = label.length, cEnd = c.length;
        while (cEnd > 0) {
            var cStart = c.lastIndexOf(';',cEnd-1) + 1;
            
            while (cStart < cEnd && c.charAt(cStart)==" ") cStart++;
            if (cStart + labelLen <= cEnd && c.substr(cStart,labelLen) == label) {
                if (cStart + labelLen == cEnd) {                
                    return ""; 
                }
                else if (c.charAt(cStart+labelLen) == "=") {
                    
                    return unescape(c.substring(cStart + labelLen + 1,cEnd));
                }
            }
            cEnd = cStart - 1;  
        }
    }
    return null;
}


function clockUpdate()
{
    var lastLocalTime = localTime;
    localTime = (new Date()).getTime();
    
    
    if (clockOffset == null) {
        clockDisplayTime(null, null, null);
    }
    else if (localTime < lastLocalTime || clockExpirationLocal < localTime) {
        
        document.cookie = 'remoteClock=-';
        document.cookie = 'localClock=-';
        location.reload();      
    }
    else {
       
        var serverTime = new Date(localTime + clockOffset);
        clockDisplayTime(serverTime.getHours(), serverTime.getMinutes(),
            serverTime.getSeconds());
        
        
        clockTimerID = setTimeout("clockUpdate()",
            clockIncrementMillis - (serverTime.getTime() % clockIncrementMillis));
    }
}
				</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" onload="clockInit(clockLocalStartTime, clockServerStartTime);clockOnLoad();pageLoad();"
		rightMargin="0" onunload="clockOnUnload()">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="2" cellPadding="0" width="999" bgColor="#003399" border="0"
				valign="middle">
				<tr>
					<td vAlign="top" bgColor="white">
						<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" valign="middle">
							<tr>
								<td vAlign="top" bgColor="white" height="133">
									<!-- start Header -->
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td width="200" height="82"><IMG src="images/lheader_r1_c1_new.jpg" border="0"></td>
											<td width="98%">
												<table height="82" cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<td width="295" background="images/lheader_r1_c3_bg.jpg" height="82"><IMG src="images/lheader_r1_c2.jpg" border="0"></td>
														<td align="right" width="500" background="images/lheader_r1_c3_bg.jpg" height="82"><IMG src="images/lheader_r1_c3_02.gif" width="500" border="0"></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td width="200" background="images/lheader_r2_c1_new.jpg" height="51">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<td colSpan="2" height="25"><IMG height="25" src="images/blank.gif" width="1" border="0"></td>
													</tr>
													<tr>
														<td vAlign="top" noWrap width="120">&nbsp;<asp:label id="CalenderLabel" runat="server" Width="125px">Kalender</asp:label></td>
														<td vAlign="top" noWrap align="right" width="70"><span id="ClockTime"><%=clockTimeString(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)%></span></td>
													</tr>
												</table>
											</td>
											<td align="right" width="98%" background="images/lheader_r2_c8_bg.jpg">
												<table height="51" cellSpacing="0" cellPadding="0" width="120" border="0">
													<tr>
														<td width="5%" height="51"><IMG src="images/lheader_r2_c2.jpg" border="0"></td>
														<td width="90%" background="images/lheader_r2_c2_bg.jpg" height="51">
															<table height="51" cellSpacing="0" cellPadding="0" width="120" border="0">
																<tr>
																	<!--td background="images/lheader_nav_red.jpg" width="30" height="51">&nbsp;</td-->
																	<td class="menuTaf" align="center" width="13%" height="51"><asp:linkbutton class="menuTaf" id="GeneralLinkButton" runat="server">D-NET</asp:linkbutton>&nbsp;&nbsp;<font color="red">l</font>&nbsp;&nbsp;<asp:linkbutton class="menuTaf" id="TAFLinkButton" runat="server"><b>T 
																				A F</b></asp:linkbutton></td>
																	<!--
																	<td background="images/lheader_nav_yellow.jpg" width="30" height="51">&nbsp;</td>
																	<td width="16%" height="51"><a href="" class="menuTaf" border="0"><b>CASHIER</b></a></td>
																	<td background="images/lheader_nav_blue.jpg" width="30" height="51">&nbsp;</td>
																	<td width="19%" height="51"><a href="" class="menuTaf" border="0"><b>COLLECTOR</b></a></td>
																	<td background="images/lheader_nav_green.jpg" width="30" height="51">&nbsp;</td>
																	<td width="24%" height="51"><a href="" class="menuTaf" border="0"><b>DEALER MANAGEMENT</b></a></td>
																	<td background="images/lheader_nav_orange.jpg" width="30" height="51">&nbsp;</td>
																	//-->
																	<!--td width="10%" class="menuHor"></td>
														<td width="10%" class="menuHor"><asp:linkbutton class="menuHor" id="SalesLinkButton" runat="server" Enabled="False"><b>SALES</b></asp:linkbutton></td>
														<td width="10%" class="menuHor"><asp:linkbutton class="menuHor" id="ServiceLinkButton" runat="server" Enabled="False">Service</asp:linkbutton></td>
														<td width="9%" class="menuHor"><asp:linkbutton class="menuHor" id="SparepartLinkButton" runat="server" Enabled="False">Parts</asp:linkbutton></td>
														<td width="9%" class="menuHor"><asp:linkbutton class="menuHor" id="RsdLinkButton" runat="server">RSD</asp:linkbutton></td>
														<td width="11%" class="menuHor"><asp:linkbutton class="menuHor" id="PromoLinkButton" runat="server">Promotion</asp:linkbutton></td>
														<td width="11%" class="menuHor2"><asp:linkbutton class="menuHor" id="MrkLinkButton" runat="server">Marketing</asp:linkbutton></td--></tr>
															</table>
														</td>
														<td width="5%" height="51"><asp:linkbutton id="LogoutLinkButton" runat="server" CssClass="red"><img src="images/lheader_r2_c8.jpg" border="0"></asp:linkbutton></td>
													</tr>
												</table>
											</td>
										</tr>
									</table> <!-- end Header //--></td>
							</tr>
							<tr bgColor="#003399">
								<td vAlign="top" width="90%">
									<table height="93%" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr valign="top">
											<td width="200" background="images/lheader_r3_c1.jpg" height="35">
												<table cellSpacing="6" width="100%" border="0">
													<tr>
														<td class="copyRight">Selamat Datang,&nbsp;<br>
															<asp:label id="labelUser" runat="server" Font-Bold="True"></asp:label></TRD></td>
													</tr>
												</table>
											</td>
											<td width="98%" bgColor="white" rowSpan="2" valign="top">
												<!-- start Content -->
												<table height="515" cellSpacing="6" cellPadding="0" width="780" border="0">
													<tr>
														<td>&nbsp;</td>
													</tr>
													<tr>
														<td vAlign="top"><IFRAME id=frMain 
                        style="WIDTH: 100%; HEIGHT: 480px" name=frMain 
                        src="<%= MainFrameSrc%>" frameBorder=no width="100%" 
                        height="100%" 
                        >
															</IFRAME></td>
													</tr>
												</table>
												<!-- end Content --></td>
										</tr>
										<tr vAlign="top" height="99%">
											<td vAlign="top" width="200" background="images/lheader_r4_c1_bg.jpg"><IMG height="6" src="images/blank.gif" width="200" border="0"><br>
												<!-- left //-->
												<table cellSpacing="6" width="100%" border="0">
													<tr>
														<td><asp:label id="LabelNamaDealer" runat="server" Font-Bold="True"></asp:label><br>
															<asp:label id="LabelSearchTerm" runat="server" Font-Bold="True"></asp:label><br>
															<asp:label id="LabelDealer" runat="server" Font-Bold="True"></asp:label><asp:label id="Label1" runat="server" Font-Bold="True">
																<br />
																<br />
																<b>Token Anda valid sampai</b></asp:label><BR>
															<asp:label id="lblvaliduntil" runat="server" Font-Bold="True"></asp:label><asp:panel id="pnlEmailValidator" runat="server"><BR>
															</asp:panel><asp:label id="lblStatus" runat="server" Font-Bold="True"></asp:label><br>
															<br>
															<asp:linkbutton id="UbahDataLinkButton" runat="server" CssClass="red">Ubah Data</asp:linkbutton>&nbsp;&nbsp; 
															<!--<asp:linkbutton id="Linkbutton1" runat="server" CssClass="red">Keluar</asp:linkbutton>//--> 
															l&nbsp;&nbsp;
															<asp:linkbutton id="lnkSecurity" runat="server" CssClass="red">Security</asp:linkbutton>&nbsp;<IMG id="imgStar" src="images/star2.gif" border="0" runat="server">
															<br>
															<table cellSpacing="2" cellPadding="0" width="98%" border="0">
																<tr>
																	<td vAlign="top">
																		<div class="scroll_service" id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><cc1:treemenu id="MyMenu" runat="server" ScreenId="100100" XMLPath="TreeMenu.XML.PathFromRootFU"></cc1:treemenu></div>
																		<div id="divMusic" style="DISPLAY: none"></div>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
												<!-- end Left --></td>
										</tr>
										 <tr valign="top" height="20">
                            <!--<td width="200" bgColor="#e6e6e6" height="20">&nbsp;</td>-->
                            <td valign="top" width="100%" height="20">
                                <!-- start Footer -->
                                <table cellspacing="1" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td valign="top" align="left" width="20%" style="padding-left:14px;">
                                            <img src="images/oim.png" border="0"></td>
                                        <td valign="bottom" nowrap align="center" width="70%"> <b><a style="color: #000000; text-decoration: none" href="http://www.mitsubishi-motors.co.id" target="_blank">PT Mitsubishi Motors Krama Yudha Sales Indonesia,</a></b> Hak Cipta Dilindungi 
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
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
				
//-----------------TAMBAHAN DARI ANDREE
		function handleServerResponse(url, response)
		{
			if (response!='' && response!='[]' && response.length <1500)
			{
			alert(response);
			}
			
		}

	function sendRequestToServer(){
						var url = "AlertManagement.ashx";
						var ajax = new AjaxDelegate(url, handleServerResponse);
						ajax.Fetch();						
					}
					
				function pageLoad(){
						sendRequestToServer();
						window.setInterval(sendRequestToServer, 120000);
					}	

//------------------END OF TAMBAHAN DARI ANDREE
		</script>
	</body>
</HTML>
