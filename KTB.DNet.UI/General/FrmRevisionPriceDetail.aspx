<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmRevisionPriceDetail.aspx.vb" Inherits=".FrmRevisionPriceDetail" smartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">


<html>
<head>
    <title>FrmAplikasiHeader</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	<script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
	<script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
</head>
<body MS_POSITIONING="GridLayout">
    <form id="form1" method="post" runat="server">
        <table id="Table2" cellspacing="5" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px" colspan="2">UMUM - Master Revisi Harga Detail</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" colspan="2" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" colspan="2" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>

            <tr>
                <td colspan="2">
                    <asp:Panel ID="formLeasing" runat="server">
                        <table>
                            <tr>
                                <td class="titleField" width="10%">Category&nbsp;</td>
                                <td width="3%" align="center">:</td>
                                <td>
                                    <asp:Label ID="lblCategory" runat="server" Width="242px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Tipe Revisi&nbsp;</td>
                                <td width="3%" align="center">:</td>
                                <td>
                                    <asp:Label ID="lblRevisi" runat="server" Width="242px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Amount&nbsp;</td>
                                <td width="3%" align="center">:</td>
                                <td>
                                    <asp:Label ID="lblAmount" runat="server" Width="242px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="15%">Valid From&nbsp;</td>
                                <td width="3%" align="center">:</td>
                                <td>
                                    <asp:Label ID="lblValid"
                                        runat="server" Width="242px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">&nbsp;</td>
                                <td width="3%" align="center"></td>
                                <td>
                                    <br />
                                    <input id="btnKembali" type="button" value="Kembali" onclick="javascript: history.back()" />&nbsp;          
                                </td>
                            </tr>

                        </table>

                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2"></td>
            </tr>
        </table>
    </form>
</body>
</html>
