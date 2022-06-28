<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpInputATA.aspx.vb" Inherits=".PopUpInputATA" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>PopUp Input ATA</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">	
        function CloseWindow() {
            var Dealer = '';
            window.close();
            if (navigator.appName != "Microsoft Internet Explorer")
            { opener.dialogWin.returnFunc(Dealer); }
        }
    </script>
    <base target="_self">
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server">

        <table id="Table1" style="width: 551px; height: 100px" cellspacing="0" cellpadding="0" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="Label2" runat="server">Pop Up - Input ATA</asp:Label></td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="/images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td valign="top" align="left">
                    <table id="Table2" style="width: 551px;" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td>Tanggal Datang Aktual</td>
                            <td width="1%">:</td>
                            <td width="70%"><cc1:inticalendar id="ICATA" runat="server" TextBoxWidth="80"></cc1:inticalendar></td>
                        </tr>
                        <tr>
                            <td>Kondisi Kendaraan</td>
                            <td width="1%">:</td>
                            <td width="70%"><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" MaxLength="140" Height="100px" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td width="1%"></td>
                            <td width="70%"><asp:button id="btnSave" runat="server" Width="50px" Text="Simpan" /><span style="margin-left: 10px"></span>
                                <asp:button id="btnBatal" runat="server" Width="50px" Text="Batal" OnClientClick="window.close();"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
