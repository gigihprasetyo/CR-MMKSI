<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpUploadBuktiPembayaranInvRev.aspx.vb" Inherits="PopUpUploadBuktiPembayaranInvRev" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Upload Bukti Dokumen Transfer</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <base target="_self">
    <script language="javascript">

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="FrmSalesmanLevel" method="post" runat="server">
        <table id="Table1" cellspacing="1" cellpadding="6" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage">DAFTAR PEMBAYARAN - UPLOAD BUKTI</td>
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
                <td width="100%">

                    <table cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titlefield" width="20%" nowrap>No. Registrasi Pembayaran</td>
                            <td width="1%">:</td>
                            <td width="79%">
                                <asp:Label ID="lblRegNumber" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titlefield" width="20%" nowrap>Nama File</td>
                            <td width="1%">:</td>
                            <td width="79%">
                                <asp:Label ID="lblFileName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titlefield">Upload Bukti Pembayaran</td>
                            <td>:</td>
                            <td>
                                <INPUT id="UploadFile" style="WIDTH: 264px; HEIGHT: 20px" type="file" size="24" name="fileUpload" runat="server">&nbsp;&nbsp;
                                    <asp:Label ID="lblFileUpload" runat="server"></asp:Label><br />
                                <asp:Label ID="lblEvidencePath" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnUpload" runat="server" Text="Proses"></asp:Button>&nbsp;
                                <input id="btnTutup" onclick="window.close();" type="button" value="Tutup">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlRunCloseWindow" runat="server" Visible="False">
            <script language="javascript">
                var isUpload = '1';
                alert('Upload file berhasil');
                window.returnValue = isUpload;
                window.close();
                if (navigator.appName != "Microsoft Internet Explorer")
                { opener.dialogWin.returnFunc(isUpload); }
            </script>
        </asp:Panel>
    </form>
</body>
</html>
