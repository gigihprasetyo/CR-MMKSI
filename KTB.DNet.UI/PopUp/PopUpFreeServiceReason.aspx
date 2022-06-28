<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpFreeServiceReason.aspx.vb" Inherits=".PopUpFreeServiceReason" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>PopUp Warranty Claim Reason</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <base target="_self">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" language="javascript">

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table border="0" width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="titlePage" style="height: 21px">Warranty Claim Canceled Reason</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0" alt="">
                </td>
            </tr>
            <tr>
                <td>
                    <br>
                        <font class="titleField">
                        <asp:Label runat="server" ID="CaptionNotes" Text="Penjelasan MMKSI" Visible="true"></asp:Label></font>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <div id="div1" style="overflow: auto; height: 180px">
                        <br>
                        <asp:TextBox ID="txtNotes" TabIndex="0" runat="server" Width="400px" TextMode="MultiLine" Visible="true"
                            Height="160px"></asp:TextBox><br>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="7">
                    <input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                        name="btnCancel">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
