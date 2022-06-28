<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmActivityPlanBabit.aspx.vb" Inherits="FrmActivityPlanBabit" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>FrmActivityPlanBabit</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',540,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var txtDealerSelection = document.getElementById("txtDealerCode");
				txtDealerSelection.value = selectedDealer;			
			}
        </script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage" style="HEIGHT: 19px">BABIT - Upload Rencana Aktivitas</td>
                </tr>
                <tr>
                    <td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
                </tr>
                <tr>
                    <td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
                </tr>
                <TR>
                    <TD align="left">
                        <TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
                            <TR>
                                <TD class="titleField" width="24%">Kode Dealer</TD>
                                <TD width="1%">:</TD>
                                <td width="75%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
                                        runat="server" Width="152px"></asp:textbox><asp:label id="lblDealers" runat="server">
                                        <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:label></td>
                            </TR>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 27px" width="24%">Aktivitas BABIT</TD>
                                <TD style="HEIGHT: 27px" width="1%">:</TD>
                                <td style="HEIGHT: 27px" width="75%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtBabitActivity" onblur="omitSomeCharacter('txtBabitActivity','<>?*%$')"
                                        runat="server" Width="288px"></asp:textbox></td>
                            </TR>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 27px" width="24%">Rencana Aktivitas</TD>
                                <TD style="HEIGHT: 27px" width="1%">:</TD>
                                <TD style="HEIGHT: 27px" width="75%"><INPUT onkeypress="return false;" id="fileUploadActivityPlan" style="WIDTH: 368px; HEIGHT: 20px"
                                        type="file" size="42" name="fileUpload" runat="server"></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 16px" width="24%">Periode</TD>
                                <TD style="HEIGHT: 16px" width="1%">:</TD>
                                <TD style="HEIGHT: 16px" width="75%"><asp:dropdownlist id="ddlStartPeriod" runat="server"></asp:dropdownlist><asp:dropdownlist id="ddlYear" runat="server" Width="88px"></asp:dropdownlist>&nbsp;s/d 
                                    &nbsp;
                                    <asp:dropdownlist id="ddlEndPeriod" runat="server"></asp:dropdownlist>
                                    <asp:dropdownlist id="ddlTahunTo" runat="server"></asp:dropdownlist></TD>
                            </TR>
                            <TR vAlign="top">
                                <TD class="titleField" style="HEIGHT: 27px" width="24%">Keterangan</TD>
                                <TD style="HEIGHT: 27px" width="1%">:</TD>
                                <TD style="HEIGHT: 27px" width="75%"><asp:textbox id="txtDescription" runat="server" Width="392px" TextMode="MultiLine" Height="48px"></asp:textbox></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 27px" width="24%"></TD>
                                <TD style="HEIGHT: 27px" width="1%"></TD>
                                <td style="HEIGHT: 27px" width="75%">
                                    <asp:button id="btnUpload" runat="server" Text="Upload"></asp:button>
                                    <input runat="server" type="button" style="WIDTH:80px" value="Kembali" id="btnBack" onclick="window.history.back();return false;"
                                        NAME="btnBack">
                                </td>
                            </TR>
                        </TABLE>
                    </TD>
                </TR>
            </TABLE>
        </form>
    </body>
</HTML>
