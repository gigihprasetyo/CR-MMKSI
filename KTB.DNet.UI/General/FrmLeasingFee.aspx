<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmLeasingFee.aspx.vb" Inherits="FrmLeasingFee"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>FrmLeasingFee</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript">
        </script>
    </HEAD>
    <body>
        <form id="Form1" method="post" runat="server">
            <div class="titlePage">
                GENERAL - Leasing Fee
            </div>
            <br>
            <table>
                <tr>
                    <td>
                        Periode&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <cc1:inticalendar id="dtmFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar>
                    </td>
                    <td>
                        &nbsp;s/d&nbsp;
                    </td>
                    <td>
                        <cc1:inticalendar id="dtmTo" runat="server" TextBoxWidth="70"></cc1:inticalendar>
                    </td>
                </tr>
                <TR>
                    <TD>Variant</TD>
                    <TD>:</TD>
                    <TD><asp:dropdownlist id="ddlVehicleCAtegory" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
                    <TD></TD>
                    <TD><asp:dropdownlist id="ddlVehicleType" runat="server"></asp:dropdownlist></TD>
                </TR>
                <TR>
                    <TD>Fee</TD>
                    <TD>:</TD>
                    <TD><asp:textbox onkeypress="return NumericOnlyWith(event,',');" id="txtFee" runat="server" MaxLength="5"
                            Columns="5"></asp:textbox>&nbsp;%</TD>
                    <TD>&nbsp;
                        <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Masukkan Fee" ControlToValidate="txtFee"
                            Display="Dynamic">*</asp:RequiredFieldValidator></TD>
                    <TD></TD>
                </TR>
                <TR>
                    <TD></TD>
                    <TD></TD>
                    <TD>
                        <asp:button id="btnSave" runat="server" Text="Simpan"></asp:button>
                        <INPUT id="btnBack" type="button" value="Kembali" runat="server">
                    </TD>
                    <TD></TD>
                    <TD></TD>
                </TR>
            </table>
            <asp:ValidationSummary id="ValidationSummary1" runat="server"></asp:ValidationSummary>
        </form>
    </body>
</HTML>
