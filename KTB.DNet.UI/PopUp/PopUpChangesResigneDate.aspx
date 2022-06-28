<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpChangesResigneDate.aspx.vb" Inherits=".PopUpChangesResigneDate" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title>Pencarian Kelas</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <base target="_self">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript">
        
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">
                    <asp:Label ID="lblPageTitle" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="height: 5px" width="20%">
                                <asp:Label ID="lblText" runat="server" Visible="False">Kode Dealer</asp:Label></td>
                            <td style="width: 14px; height: 5px" width="14">
                                <asp:Label ID="lblsemicolon" runat="server" Width="3px" Visible="False">:</asp:Label></td>
                            <td class="titleField" style="height: 5px" width="20%">
                                <asp:Label ID="lblKodeDealer" runat="server" Width="152px" Visible="False"></asp:Label></td>
                            <td class="titleField" style="height: 5px" width="20%"></td>
                            <td style="height: 5px" width="1%"></td>
                            <td style="height: 5px" width="29%"></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="20%">Employee ID</td>
                            <td style="height: 17px" width="1%">:</td>
                            <td class="titleField" width="25%">
                                <asp:Label ID="lblEmpID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="20%">Nama</td>
                            <td width="14" style="width: 14px">
                                <asp:Label ID="Label1" runat="server">:</asp:Label></td>
                            <td class="titleField" width="20%">
                                <asp:Label ID="lblName" runat="server"></asp:Label></td>
                            <td class="titleField" width="20%"></td>
                            <td width="1%"></td>
                            <td width="40%"></td>
                        </tr>

                        <tr>
                            <td class="titleField" width="20%">Posisi</td>
                            <td width="14" style="width: 14px">
                                <asp:Label ID="Label2" runat="server">:</asp:Label></td>
                            <td class="titleField" width="20%">
                                <asp:Label ID="lblPosition" runat="server"></asp:Label>

                            </td>
                            <td class="titleField" width="20%"></td>
                            <td width="1%"></td>
                            <td width="29%"></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="20%" style="height: 29px">Tgl Keluar</td>
                            <td style="width: 14px; height: 29px" width="14">
                                <asp:Label ID="Label5" runat="server">:</asp:Label></td>
                            <td style="width: 80px; height: 29px" nowrap width="80">
                                <table border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td></td>
                                        <td>
                                            <cc1:inticalendar id="icResignDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                    </tr>
                                </table>
                            </td>
                            <td class="titleField" style="height: 29px" width="20%"></td>
                            <td width="1%" style="height: 29px"></td>
                            <td style="height: 29px" width="29%"></td>
                        </tr>

                        <tr>
                            <td class="titleField">Alasan</td>
                            <td style="width: 14px; height: 11px">
                                <asp:Label ID="Label6" runat="server">:</asp:Label></td>
                            <td class="titleField" style="height: 11px">
                                <asp:TextBox onkeypress="TxtKeypress();" ID="txtResignReason" onblur="TxtBlur('txtResignReason');"
                                    runat="server" Width="208px" MaxLength="700" size="22" TextMode="MultiLine"></asp:TextBox></td>
                            <td class="titleField" style="height: 11px"></td>
                            <td width="1%"></td>
                            <td style="height: 11px"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 141px; height: 24px"></td>
                            <td style="width: 14px; height: 24px"></td>
                            <td class="titleField" style="height: 24px">
                                <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
                            <td class="titleField" style="height: 24px"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 141px; height: 24px"></td>
                            <td style="width: 14px; height: 24px"></td>
                            <td class="titleField" style="height: 24px">
                                <asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:Button>
                                &nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                                    name="btnCancel">
                            <td class="titleField" style="height: 24px"></td>
                            <td style="height: 24px"></td>
                        </tr>

                        <tr>
                            <td class="titleField" style="width: 141px; height: 24px" colspan="6"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 8px"></td>
            </tr>
        </table>
    </form>

</body>

</html>
