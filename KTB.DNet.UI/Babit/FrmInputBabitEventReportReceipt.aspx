<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputBabitEventReportReceipt.aspx.vb" Inherits="FrmInputBabitEventReportReceipt" SmartNavigation="False" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmInputBabitEventReportReceipt</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>

    <script language="javascript">

        function toCommas(value) {
            return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
        }
        function calculatePrice(txtPrice) {
            var txtPrices = txtPrice.value.replace(".", "").replace(".", "").replace(".", "").replace(".", "");
            var index = txtPrice.parentNode.parentNode.rowIndex;
            var dtg = document.getElementById("dgBabitEventProposal");
            var txtFQty = dtg.rows[index].getElementsByTagName("INPUT")[1];
            var txtFQty = txtFQty.value.replace(".", "").replace(".", "").replace(".", "");
            var txtFTotalPrice = dtg.rows[index].getElementsByTagName("INPUT")[3];

            if (trim(txtPrices) != "" || trim(txtPrices) != "0") {
                txtFTotalPrice.value = txtFQty * txtPrices
                txtFTotalPrice.value = toCommas(txtFTotalPrice.value)
            }
        }
        function calculateQty(txtQty) {
            var txtQtys = txtQty.value.replace(".", "").replace(".", "").replace(".", "").replace(".", "");
            var index = txtQty.parentNode.parentNode.rowIndex;
            var dtg = document.getElementById("dgBabitEventProposal");
            var txtFPrice = dtg.rows[index].getElementsByTagName("INPUT")[2];
            var txtFPrice = txtFPrice.value.replace(".", "").replace(".", "").replace(".", "").replace(".", "");
            var txtFTotalPrice = dtg.rows[index].getElementsByTagName("INPUT")[3];

            if (trim(txtQtys) != "" || trim(txtQtys) != "0") {
                txtFTotalPrice.value = txtQtys * txtFPrice
                txtFTotalPrice.value = toCommas(txtFTotalPrice.value)
            }
        }
        function trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }
        function keyUP(txt) {
            if (txt.value.length == txt.maxLength) {
                if (txt.id == "txtNomorFaktur1") {
                    document.getElementById("txtNomorFaktur2").focus()
                } else if (txt.id == "txtNomorFaktur2") {
                    document.getElementById("txtNomorFaktur3").focus()
                } else if (txt.id == "txtNomorFaktur3") {
                    document.getElementById("txtNomorFaktur4").focus()
                }
            }
        }
    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <input id="hdnBabitEventReportReceiptID" type="hidden" value="0" runat="server">
        <input id="hdnBabitEventReportHeaderID" type="hidden" value="0" runat="server">
        
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" colspan="2">EVENT - INPUT KUITANSI</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1" colspan="2">
                    <img height="1" src="../images/bg_hor.gif" alt="" border="0" /></td>
            </tr>
            <tr>
                <td height="10" colspan="2">
                    <img height="1" src="../images/dot.gif" alt="" border="0" /></td>
            </tr>
            <tr>
                <td valign="top">
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="width: 146px">Kode Dealer</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Nama Dealer</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Nomor Reg Babit</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="lblNoRegEvent" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Nomor Kuitansi</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:TextBox ID="txtReceiptNo" runat="server" Width="275px" Maxlength="25"/></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Nomor Faktur Pajak</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <%--<asp:TextBox ID="txtFakturPajakNo" runat="server" Width="275px" Maxlength="18" />--%>
                                <asp:TextBox onkeypress="return NumericOnlyWith(event,',');" ID="txtNomorFaktur1" onblur="omitSomeCharacter('txtNomorFaktur1','<>?*%$;')" onkeyup="keyUP(this)"
                                    runat="server" Width="25px" MaxLength="2"></asp:TextBox><asp:Label ID="lblNomorFaktur1" runat="server" Text="." />
                                <asp:TextBox onkeypress="return NumericOnlyWith(event,',');" ID="txtNomorFaktur2" onblur="omitSomeCharacter('txtNomorFaktur2','<>?*%$;')" onkeyup="keyUP(this)"
                                    runat="server" Width="25px" MaxLength="3"></asp:TextBox><asp:Label ID="lblNomorFaktur2" runat="server" Text="-" />
                                <asp:TextBox onkeypress="return NumericOnlyWith(event,',');" ID="txtNomorFaktur3" onblur="omitSomeCharacter('txtNomorFaktur3','<>?*%$;')" onkeyup="keyUP(this)"
                                    runat="server" Width="20px" MaxLength="2"></asp:TextBox><asp:Label ID="lblNomorFaktur3" runat="server" Text="." />
                                <asp:TextBox onkeypress="return NumericOnlyWith(event,',');" ID="txtNomorFaktur4" onblur="omitSomeCharacter('txtNomorFaktur4','<>?*%$;')" onkeyup="keyUP(this)"
                                    runat="server" Width="55px" MaxLength="8"></asp:TextBox>
                                <asp:Label ID="lblNomorFakturOld" runat="server" Visible="false"></asp:Label>
                               <span style="color: red; font-size: 10px"> 1 Digit pertama diabaikan, contoh: 10.XXX-YY.ZZZZZZZZZ</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Tanggal Faktur Pajak</td>
                            <td style="width: 2px">:</td>
                            <td>
                               <cc1:IntiCalendar ID="icFakturPajakDate" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Amount Claim</td>
                            <td style="width: 2px">:</td>
                            <td>
                               <asp:TextBox ID="txtClaimAmount" Style="text-align: right" runat="server" onkeypress="return NumericOnlyWith(event,'');"
                                    Enabled="false" onkeyup="pic(this,this.value,'9999999999','N')" Width="120px" /></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Amount PPN</td>
                            <td style="width: 2px">:</td>
                            <td>
                               <asp:TextBox ID="txtVATTotal" Style="text-align: right" runat="server" onkeypress="return NumericOnlyWith(event,'');"
                                    Enabled="false" onkeyup="pic(this,this.value,'9999999999','N')" Width="120px" /></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Amount Pph</td>
                            <td style="width: 2px">:</td>
                            <td>
                               <asp:TextBox ID="txtPPHTotal" Style="text-align: right" runat="server" onkeypress="return NumericOnlyWith(event,'');"
                                    Enabled="false" onkeyup="pic(this,this.value,'9999999999','N')" Width="120px" /></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Total Amount Kuitansi</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <input id="hdnTotalReceiptAmount" type="hidden" value="0" runat="server">
                               <asp:TextBox ID="txtTotalReceiptAmount" Style="text-align: right" runat="server" onkeypress="return NumericOnlyWith(event,'');"
                                    Enabled="false" onkeyup="pic(this,this.value,'9999999999','N')" Width="120px" /></td>
                        </tr>                            
                        <tr>
                            <td class="titleField" style="width: 146px">Tanggal Kuitansi</td>
                            <td style="width: 2px">:</td>
                            <td>
                               <cc1:IntiCalendar ID="icReceiptDate" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Nomor Rekening</td>
                            <td style="width: 2px">:</td>
                            <td><asp:DropDownList runat="server" ID="ddlNoRek"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Telah Terima Dari</td>
                            <td style="width: 2px">:</td>
                            <td>
                               <asp:Label ID="lblTerimaDari" runat="server">
                                   MITSUBISHI MOTORS KRAMA YUDHA SALES INDONESIA, PT</asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Untuk Pembayaran</td>
                            <td style="width: 2px">:</td>
                            <td>
                               <asp:Label ID="lblTujuanPembayaran" runat="server" >Untuk Pembayaran EVENT</asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Nama Tanda Tangan</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:TextBox ID="txtSignName" runat="server" Width="275px" /></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Jabatan Tanda Tangan</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:TextBox ID="txtSignPosition" runat="server" Width="275px" /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSave" Width="70px" OnClientClick="return confirm('Anda yakin mau simpan?');" runat="server" Text="Simpan"></asp:Button>&nbsp;
                                <asp:Button ID="btnCetak" Width="70px" runat="server" Text="Cetak" Visible="false"></asp:Button>&nbsp;
                                <asp:Button ID="btnBack" Width="70px" runat="server" Text="Kembali" Visible="false"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
