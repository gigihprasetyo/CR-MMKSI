<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInvoiceRevisionRemark.aspx.vb" Inherits=".FrmInvoiceRevisionRemark" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Remark</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" language="javascript">
        function GetSelectedText() {
            var txt = document.getElementById("txtComment");
            if (navigator.appName == "Microsoft Internet Explorer") {
                window.returnValue = txt.innerText;
            }
            else {
                //alert(txt.value);
                opener.dialogWin.returnFunc(txt.value);
            }
            window.close();
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" method="post" runat="server">
        <table id="Table1" style="z-index: 101; left: 3px; width: 98%; position: absolute; top: 3px; height: 98%"
            cellspacing="0" cellpadding="0" width="100%" border="0" height="100%">
            <tr>
                <td colspan="2" class="titleTableSales">
                    <asp:Label ID="lblTitle" runat="server" BackColor="#F28625">Invoice Revision Remark</asp:Label></td>
            </tr>
            <tr>
                <td colspan="2" height="1" background="../images/bg_hor_sales.gif">
                    <img src="../images/bg_hor_sales.gif" height="1" border="0"></td>
            </tr>
            <tr>
                <td colspan="2" height="10">
                    <img src="../images/dot.gif" height="1" border="0"></td>
            </tr>
            <tr valign="top">
                <td width="35%" class="titleField">
                    <asp:Label ID="lblKomentar" runat="server">Detail Penjelasan Pesanan: </asp:Label></td>
                <td width="65%">
                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="18" Cols="50"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="lblCreatedBy" runat="server" Visible="False">Diajukan Oleh :</asp:Label></td>
                <td>
                    <asp:Label ID="lblCreatorName" runat="server" Visible="False"></asp:Label></td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="lblCreatedDate" runat="server" Visible="False">Pada Tanggal :</asp:Label></td>
                <td>
                    <asp:Label ID="lblDate" runat="server" Visible="False"></asp:Label></td>
            </tr>
            <tr height="40">
                <td></td>
                <td>
                    <input id="btnChoose" style="width: 60px" onclick="GetSelectedText()" type="button" value="Simpan"
                        name="btnChoose" runat="server">&nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Batal"
                            name="btnCancel"></td>
            </tr>
        </table>
    </form>
</body>
</html>
