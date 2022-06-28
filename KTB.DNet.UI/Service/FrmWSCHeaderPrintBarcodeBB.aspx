<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmWSCHeaderPrintBarcodeBB.aspx.vb" Inherits=".FrmWSCHeaderPrintBarcodeBB" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function PrintDocument() {
            window.print();
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Waranty Service Claim Special - Print Preview Barcode</td>
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

        <table cellspacing="1" cellpadding="4" width="100%" border="0">
            <tr valign="top">
                <td width="100%">
                    <table cellspacing="1" cellpadding="2" width="90%" border="0">
                        <tr>
                            <td colspan="2">
                                <asp:Button class="hideButtonOnPrint" ID="btnPrint" align="center" runat="server" Text="Print" Width="72px"></asp:Button>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button class="hideButtonOnPrint" ID="btnKembali" align="center" runat="server" Text="Kembali" Width="72px"></asp:Button>
                            </td>
                            <td colspan="2" align="right" valign="middle">
                                <b>Page :</b>&nbsp;
                                <asp:Label ID="lblCurrentPage" runat="server"></asp:Label><br />
                                <b>Total Pages :</b>&nbsp;<asp:Label ID="lblTotalPages" runat="server"></asp:Label>
                            </td>
                            <td align="right" valign="bottom">
                                <asp:LinkButton ID="lnkbtnPrev" runat="server" CausesValidation="False" ToolTip="Sebelumnya">
										<img style="cursor:hand" alt="Sebelumnya" src="../images/page_prev.gif" border="0"></asp:LinkButton>&nbsp;&nbsp;||&nbsp;
                                <asp:LinkButton ID="lnkbtnNext" runat="server" CausesValidation="False" ToolTip="Selanjutnya">
										<img style="cursor:hand" alt="Selanjutnya" src="../images/page_next.gif" border="0"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" width="40%">
                                <div runat="server" width="40%">
                                    <asp:Image ID="BarcodeImage1" runat="server" Width="250" Height="100" Style="display: none" />
                                </div>
                            </td>
                            <td>&nbsp;&nbsp;</td>
                            <td align="center" width="40%">
                                <div runat="server" width="40%">
                                    <asp:Image ID="BarcodeImage2" runat="server" Width="250" Height="100" Style="display: none" />
                                </div>
                            </td>
                            <td>&nbsp;&nbsp;</td>
                            <td align="center" width="40%">
                                <div runat="server" width="40%">
                                    <asp:Image ID="BarcodeImage3" runat="server" Width="250" Height="100" Style="display: none" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Image ID="BarcodeImage4" runat="server" Width="250" Height="100" Style="display: none" />
                            </td>
                            <td>&nbsp;&nbsp;</td>
                            <td align="center">
                                <asp:Image ID="BarcodeImage5" runat="server" Width="250" Height="100" Style="display: none" />
                            </td>
                            <td>&nbsp;&nbsp;</td>
                            <td align="center">
                                <asp:Image ID="BarcodeImage6" runat="server" Width="250" Height="100" Style="display: none" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Image ID="BarcodeImage7" runat="server" Width="250" Height="100" Style="display: none" />
                            </td>
                            <td>&nbsp;&nbsp;</td>
                            <td align="center">
                                <asp:Image ID="BarcodeImage8" runat="server" Width="250" Height="100" Style="display: none" />
                            </td>
                            <td>&nbsp;&nbsp;</td>
                            <td align="center">
                                <asp:Image ID="BarcodeImage9" runat="server" Width="250" Height="100" Style="display: none" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Image ID="BarcodeImage10" runat="server" Width="250" Height="100" Style="display: none" />
                            </td>
                            <td>&nbsp;&nbsp;</td>
                            <td align="center">
                                <asp:Image ID="BarcodeImage11" runat="server" Width="250" Height="100" Style="display: none" />
                            </td>
                            <td>&nbsp;&nbsp;</td>
                            <td align="center">
                                <asp:Image ID="BarcodeImage12" runat="server" Width="250" Height="100" Style="display: none" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
