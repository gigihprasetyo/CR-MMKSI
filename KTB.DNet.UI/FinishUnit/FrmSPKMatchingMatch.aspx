<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmSPKMatchingMatch.aspx.vb" Inherits="FrmSPKMatchingMatch" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Register TagPrefix="uc1" TagName="Clock" Src="../UserControl/Clock.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title>FrmSPKMatching</title>
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function TxtBlur(objTxt) {
            omitSomeCharacter(objTxt, '<>?*%$');
        }
        function TxtKeypress() {
            return alphaNumericExcept(event, '<>?*%$;')
        }
        
        //function ShowSPKSelection() {
        //    //showPopUp('../PopUp/PopUpSPKTersedia.aspx?IsGroupDealer=0', '', 500, 800, SPKSelection);
        //    showPopUp('../PopUp/PopUpSPKTersedia.aspx?IsGroupDealer=1', '', 500, 800, SPKSelection);
        //}
        //function SPKSelection(selectedSPK) {
        //    var temp = selectedSPK.split(";")
        //    var txtSPKNumber = document.getElementById('txtNoSPK');
            
        //    txtSPKNumber.value = temp[0];
        //}
        
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" enctype="multipart/form-data" runat="server">
        <table id="Table1" cellspacing="1" cellpadding="2" width="500px" border="0">
            <tr>
                <td colspan="3">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage">Display SPK Matching</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor.gif" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="30%">Nomor Rangka</td>
                <td>:</td>
                <td width="68%">
                    <asp:TextBox ID="txtNoChassis" onkeypress="TxtKeypress();" onblur="TxtBlur('txtNoChassis');"
                        runat="server" Width="152px"></asp:TextBox>
                    <asp:Button ID="btnCheckNoChassis" runat="server" Width="50px" Text="Check"></asp:Button>
                </td>
            </tr>
            <tr>
                <td class="titleField">Deskripsi</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblDescription" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleField">Nomor SPK</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtNoSPK" onkeypress="TxtKeypress();" onblur="TxtBlur('txtNoSPK');" runat="server" Width="152px"></asp:TextBox>
                    <%--<asp:label id="lblSPKNumber" runat="server" width="16px">
						<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
					</asp:label>&nbsp;--%>
                    <asp:Button ID="btnCheckNoSPK" runat="server" Width="50px" Text="Check"></asp:Button>
                </td>
            </tr>
            <tr>
                <td class="titleField">Kode Tipe</td>
                <td>:</td>
                <td>
                    <asp:Label ID="txtVehicleType" runat="server" Width="152px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleField">Kode Warna</td>
                <td>:</td>
                <td>
                    <asp:Label ID="txtKodeWarna" runat="server" Width="152px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleField">Key No.</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtKeyNo" onkeypress="TxtKeypress();" onblur="TxtBlur('txtMatchNo');"
                        runat="server" Width="152px"></asp:TextBox>
                </td>
            </tr>
            <%--<tr>
                <td class="titleField">Match No.</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtMatchNo" onkeypress="TxtKeypress();" onblur="TxtBlur('txtMatchNo');"
                        runat="server" Width="152px"></asp:TextBox>
                </td>
            </tr>--%>
            <tr>
                <td class="titleField">Referensi No.</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtRefNo" onkeypress="TxtKeypress();" onblur="TxtBlur('txtRefNo');"
                        runat="server" Width="152px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField">Tanggal Matching</td>
                <td>:</td>
                <td>
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td style="width: 100px">
                                <cc1:IntiCalendar ID="matchingDate" runat="server" TextBoxWidth="80"></cc1:IntiCalendar></td>
                            <td class="titleField"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Button ID="btnMatch" runat="server" Width="70px" Text="Match"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Width="50px" Text="Cancel"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
