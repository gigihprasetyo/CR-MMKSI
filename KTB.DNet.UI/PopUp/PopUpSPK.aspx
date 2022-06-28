<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpSPK.aspx.vb" Inherits=".PopUpSPK" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmDealerSelection</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/BasicStyle.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <base target="_self">
    <script type="text/javascript" language="javascript">

        function getQueryVariable(variable) {
            var query = window.location.search.substring(1);
            var vars = query.split("&");
            for (var i = 0; i < vars.length; i++) {
                var pair = vars[i].split("=");
                if (pair[0] == variable) {
                    return pair[1];
                }
            }
            return "nothing";
        }

        function GetSelectedSPK() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgSpkSelection");
            var Dealer = '';
            for (i = 1; i < table.rows.length; i++) {

                var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (radioBtn != null && radioBtn.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        if (getQueryVariable("x") == "Territory") {
                            Dealer = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText + ';' + replace(table.rows[i].cells[3].innerText, ' ', '') + ';' + replace(table.rows[i].cells[4].innerText, ' ', '');
                        }
                        else {
                            //Dealer = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText;
                            Dealer = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText + ';' + replace(table.rows[i].cells[3].innerText, ' ', '') + ';' + replace(table.rows[i].cells[4].innerText, ' ', '');
                        }
                        window.returnValue = Dealer;
                        bcheck = true;
                    }
                    else if (navigator.appName == "Netscape") {
                        if (getQueryVariable("x") == "Territory") {
                            Dealer = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText + ';' + replace(table.rows[i].cells[3].innerText, ' ', '') + ';' + replace(table.rows[i].cells[4].innerText, ' ', '');
                        }
                        else {
                            //Dealer = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText;
                            Dealer = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText + ';' + replace(table.rows[i].cells[3].innerText, ' ', '') + ';' + replace(table.rows[i].cells[4].innerText, ' ', '');
                        }
                        window.opener.dialogWin.returnFunc(Dealer);
                        bcheck = true;
                    }
                    else {
                        if (getQueryVariable("x") == "Territory") {
                            Dealer = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '') + ';' + table.rows[i].cells[2].innerHTML + ';' + table.rows[i].cells[3].innerHTML + ';' + table.rows[i].cells[4].innerHTML;
                        }
                        else {
                            //Dealer = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '') + ';' + replace(table.rows[i].cells[2].innerHTML, ' ', '');
                            Dealer = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '') + ';' + table.rows[i].cells[2].innerHTML + ';' + table.rows[i].cells[3].innerHTML + ';' + table.rows[i].cells[4].innerHTML;
                        }
                        window.opener.dialogWin.returnFunc(Dealer);
                        bcheck = true;
                    }
                    break;
                }
            }
            if (bcheck) {
                window.close();
            }
            else {
                alert("Silahkan pilih SPK");
            }
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="4" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" colspan="7">Nomor SPK DNet</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor_sales.gif" colspan="7" height="1">
                                <img alt="" height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" alt="" src="../images/dot.gif" border="0" /></td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" width="20%">Nama Peserta</td>
                            <td width="1%">:</td>
                            <td style="width: 225px" width="225">
                                <asp:TextBox ID="txtName" Width="225px" runat="server" AutoPostBack="true" />
                            </td>
                            <td style="width: 17px" width="10%"></td>
                            <td class="titleField" width="10%">No KTP</td>
                            <td style="height: 10px" width="1%">:</td>
                            <td style="width: 215px; height: 10px" width="215">
                                <asp:TextBox ID="txtKTP" Width="125px" runat="server" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" width="20%"></td>
                            <td width="1%"></td>
                            <td valign="bottom" style="width: 225px; height: 13px">
                                <asp:Button ID="btnSearch" runat="server" Text=" Cari "></asp:Button>
                            </td>
                            <td style="width: 17px" width="10%"></td>
                            <td class="titleField" style="height: 13px"></td>
                            <td style="height: 13px">&nbsp;</td>
                            <td valign="bottom" style="width: 225px; height: 13px"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="width: 17px; height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="width: 225px; height: 15px"></td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <div id="div1" style="overflow: auto; height: 280px">
                                    <asp:DataGrid ID="dtgSpkSelection" runat="server" Width="100%" AutoGenerateColumns="False"
                                        AllowSorting="True" AllowCustomPaging="True" PageSize="25" AllowPaging="True" ShowFooter="false">
                                        <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                                        <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                                        <HeaderStyle ForeColor="White" BackColor="#CC3333" Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
                                        <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="No SPK">
                                                <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSPKNumber" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nama Peserta">
                                                <HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="No KTP">
                                                <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProfileValue" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Alamat">
                                                <HeaderStyle Width="25%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="7">
                                <input id="btnChoose" style="width: 60px" disabled onclick="GetSelectedSPK()" type="button"
                                    value="Pilih" name="btnChoose" runat="server">&nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                                        name="btnCancel"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
