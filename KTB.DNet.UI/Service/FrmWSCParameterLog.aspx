<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmWSCParameterLog.aspx.vb" Inherits=".FrmWSCParameterLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FrmWSCParameterLog</title>
    <style>
        .HiddenColumn {
            DISPLAY: none;
            FONT-WEIGHT: bold;
            FONT-SIZE: 11px;
            BACKGROUND: #666666;
            MARGIN: 0px;
            COLOR: #ffffff;
            FONT-FAMILY: Sans-Serif, Arial;
            TEXT-ALIGN: center;
        }
    </style>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="6" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" style="height: 18px" colspan="7">WSC Parameter Error Log</td>
                        </tr>
                        <tr>
                            <td style="height: 1px" background="../images/bg_hor_sales.gif" colspan="7" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 22px" width="20%">Parameter Error Log</td>
                        </tr>
                        <tr style="height: 350px">
                            <td width="100%" height="350px">
                                <asp:ListBox ID="lbLog" runat="server" Width="100%" Height="100%"></asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
