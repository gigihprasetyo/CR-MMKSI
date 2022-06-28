<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpPaymentInformation.aspx.vb" Inherits=".PopUpPaymentInformation" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>PopUpPaymentInformation</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>

    <script type="text/javascript">
        function RestrictSpace() {
            if (event.keyCode == 32) {
                event.returnValue = false;
                return false;
            }
        }
    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server">
        <div>
            <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <img id="imageInformasi" runat="server" src="../images/ManualTransferASS.jpg" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
