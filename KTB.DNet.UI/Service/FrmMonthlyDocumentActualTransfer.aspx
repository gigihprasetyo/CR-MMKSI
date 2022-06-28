<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMonthlyDocumentActualTransfer.aspx.vb" Inherits=".FrmMonthlyDocumentActualTransfer" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Actual Transfer</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/jquery-1.10.2.js" type="text/javascript"></script>
    <script language="javascript">

    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="titlePage">Umum - Actual Transfer </td>
            </tr>
            <tr>
                <td height="1" background="../images/bg_hor.gif">
                    <img border="0" src="../images/bg_hor.gif" height="1"></td>
            </tr>
            <tr>
                <td height="10">
                    <img border="0" src="../images/dot.gif" height="1"></td>
            </tr>
            <tr>
                <td valign="top" align="left">
                    <table id="Table2" border="0" cellspacing="2" cellpadding="1" width="100%">
                        <tbody>
                            <tr>
                                <td class="titleField" width="25%">Kode Dealer</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdID" runat="server" />
                                </td>                                
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>

                            <tr>
                                <td class="titleField" width="25%">Nomor Accounting</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblNoAccounting" runat="server"></asp:Label></td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>

                            <tr>
                                <td class="titleField" width="25%">Tgl Transfer</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblTglTransfer" runat="server"></asp:Label></td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>

                            <tr>
                                <td class="titleField" width="25%">Bank</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblBank" runat="server"></asp:Label></td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>

                            <tr>
                                <td class="titleField" width="25%">No Rekening</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblNoRekening" runat="server"></asp:Label></td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="25%">Total Transfer</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblTotalTransfer" runat="server"></asp:Label></td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>
                            <tr>
                                <td colspan="2"></td>
                                <td width="75%">&nbsp; &nbsp;
                                        <asp:Button ID="btnBack" runat="server" Width="64px" Text="Tutup" CausesValidation="False"></asp:Button>
                                </td>
                            </tr>
                        </tbody>

                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
