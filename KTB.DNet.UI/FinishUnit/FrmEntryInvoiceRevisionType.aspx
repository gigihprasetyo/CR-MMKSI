<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmEntryInvoiceRevisionType.aspx.vb" Inherits=".FrmEntryInvoiceRevisionType" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN>
<html>
<head>
    <title>FrmEntryInvoice</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js" type="text/javascript"></script>
    <script language="javascript" src="../WebResources/InputValidation.js?id=0" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">REVISI FAKTUR&nbsp;- Permohonan Revisi 
						Faktur Kendaraan</td>
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
                    <table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="80" height="24">Tipe Revisi</td>
                            <td width="5">:</td>
                            <td width="230"><asp:DropDownList ID="ddlRevisionType" AutoPostBack="true" runat="server" CausesValidation="false" ></asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlRevisionType"
                            ValueToCompare="Silahkan Pilih" Operator="NotEqual" Enabled="False">*</asp:CompareValidator></td>
                            <td class="titleField" width="80" height="24">.</td>
                            <td width="5">.</td>
                            <td width="230">.</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <p><br />
                        <asp:Button ID="btnCancel" runat="server" Text="Batal" CausesValidation="False"></asp:Button>
                        <asp:Button ID="btnKembali" runat="server" Text="Kembali" CausesValidation="False"></asp:Button>
                    </p>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
