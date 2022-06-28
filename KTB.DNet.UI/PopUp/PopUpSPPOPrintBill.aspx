<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpSPPOPrintBill.aspx.vb" Inherits="PopUpSPPOPrintBill"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>PopUpSPPOPrintBill</title>
        <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
        <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
        <meta name="vs_defaultClientScript" content="JavaScript">
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    </HEAD>
    <body>
        <form id="Form1" method="post" runat="server">
            <div style="padding:5px;">
                <TABLE id="Table1" cellSpacing="1" cellPadding="1">
                    <TR>
                        <TD colspan="2">
                            No. Kwitansi
                        </TD>
                        <td>
                            :
                            <asp:TextBox id="txtNoBill" runat="server"></asp:TextBox>
                        </td>
                    </TR>
                    <tr>
                        <td colspan="2" valign="top">
                            Kode Dealer
                        </td>
                        <td valign="top">
                            :
                            <asp:Label id="lblDealerCode" runat="server"></asp:Label>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <TR>
                        <TD>Telah terima dari</TD>
                        <TD>:</TD>
                        <TD>PT Mitsubishi Motors Krama Yudha Sales Indonesia</TD>
                    </TR>
                    <TR>
                        <TD>Uang sejumlah</TD>
                        <TD>:</TD>
                        <TD>
                            <asp:Label id="lblTerbilang" runat="server" Font-Underline="True"></asp:Label></TD>
                    </TR>
                    <TR>
                        <TD>Untuk pembayaran</TD>
                        <TD>:</TD>
                        <TD>Pemindahan saldo dari deposit B ke deposit C1, atas pembelian Equipment No. 
                            Referensi :
                            <asp:Label id="lblPoNo" runat="server" Font-Underline="True"></asp:Label></TD>
                    </TR>
                    <TR>
                        <TD vAlign="bottom">Jumlah :
                            <asp:Label id="lblJumlah" runat="server" Font-Bold="True"></asp:Label></TD>
                        <TD></TD>
                        <TD align="right">
                            <br>
                            <asp:Label id="lblCityDate" runat="server"></asp:Label>
                            <br>
                            <br>
                            <br>
                            <br>
                            (TTD &amp; Nama Pejabat Berwenang, Stempel Dealer &amp; Materai)
                        </TD>
                    </TR>
                    <TR>
                        <TD vAlign="bottom"></TD>
                        <TD></TD>
                        <TD align="right">
                            <br />
                            <br />
                            <INPUT class="hideButtonOnPrint" id="btnCetak" style="WIDTH: 48px; HEIGHT: 21px" onclick="window.print();"
                                type="button" value="Cetak" name="btnCetak" runat="server"> <INPUT class="hideButtonOnPrint" id="btnClose" style="WIDTH: 48px; HEIGHT: 21px" onclick="window.close();"
                                type="button" value="Close" name="btnClose" runat="server">
                        </TD>
                    </TR>
                </TABLE>
            </div>
        </form>
    </body>
</HTML>
