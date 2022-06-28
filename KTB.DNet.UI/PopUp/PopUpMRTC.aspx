<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpMRTC.aspx.vb" Inherits=".PopUpMRTC" %>

<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmDealerSelection</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/BasicStyle.css" type="text/css" rel="stylesheet">
    <base target="_self">
    <script language="javascript">

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

        function GetSelectedMRTC() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgMRTCSelection");
            var MRTC = '';
            for (i = 1; i < table.rows.length; i++) {

                var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (radioBtn != null && radioBtn.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        if (getQueryVariable("x") == "Territory") {
                            MRTC = table.rows[i].cells[1].innerText + ';' + table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[5].innerText + ';' + table.rows[i].cells[6].innerText;
                        }
                        else {
                            //Dealer = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText;
                            MRTC = table.rows[i].cells[1].innerText + ';' + table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[5].innerText + ';' + table.rows[i].cells[6].innerText;
                        }
                        window.returnValue = MRTC;
                        bcheck = true;
                    }
                    else if (navigator.appName == "Netscape") {
                        if (getQueryVariable("x") == "Territory") {
                            MRTC = table.rows[i].cells[1].innerText + ';' + table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[5].innerText + ';' + table.rows[i].cells[6].innerText;
                        }
                        else {
                            //Dealer = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText;
                            MRTC = table.rows[i].cells[1].innerText + ';' + table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[5].innerText + ';' + table.rows[i].cells[6].innerText;
                        }
                        window.opener.dialogWin.returnFunc(MRTC);
                        bcheck = true;
                    }
                    else {
                        if (getQueryVariable("x") == "Territory") {
                            MRTC = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML + ';' + table.rows[i].cells[2].innerHTML + ';' + table.rows[i].cells[5].innerHTML + ';' + table.rows[i].cells[6].innerHTML;
                        }
                        else {
                            //Dealer = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '') + ';' + replace(table.rows[i].cells[2].innerHTML, ' ', '');
                            MRTC = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML + ';' + table.rows[i].cells[2].innerHTML + ';' + table.rows[i].cells[5].innerHTML + ';' + table.rows[i].cells[6].innerHTML;
                        }
                        window.opener.dialogWin.returnFunc(MRTC);
                        bcheck = true;
                    }
                    break;
                }
            }
            if (bcheck) {
                window.close();
            }
            else {
                alert("Silahkan pilih mrtc");
            }
        }

        function replace(string, text, by) {
            var strLength = string.length, txtLength = text.length;
            if ((strLength == 0) || (txtLength == 0)) return string;

            var i = string.indexOf(text);
            if ((!i) && (text != string.substring(0, txtLength))) return string;
            if (i == -1) return string;

            var newstr = string.substring(0, i) + by;

            if (i + txtLength < strLength)
                newstr += replace(string.substring(i + txtLength, strLength), text, by);

            return newstr;
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
                            <td class="titlePage" colspan="7">MRTC -&nbsp;Pencarian MRTC &nbsp;</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor_sales.gif" colspan="7" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" width="20%">Kode MRTC</td>
                            <td width="1%">:</td>
                            <td width="25%">
                                <asp:TextBox ID="txtMRTCCode"
                                    runat="server" Width="152px"></asp:TextBox></td>
                            <td style="width: 17px" width="2%"></td>

                        </tr>
                        <tr valign="top">
                            <td class="titleField" width="20%">Nama MRTC</td>
                            <td width="1%">:</td>
                            <td width="25%">
                                <asp:TextBox ID="txtMRTCName"
                                    runat="server" Width="152px"></asp:TextBox>
                                &nbsp;
                                <asp:Button ID="btnSearch" runat="server" Text=" Cari "></asp:Button>
                            </td>


                            <td style="width: 17px" width="2%"></td>

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
                                    <asp:DataGrid ID="dtgMRTCSelection" runat="server" Width="100%" AutoGenerateColumns="False"
                                        AllowSorting="True" AllowCustomPaging="true" AllowPaging="true">
                                        <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                                        <HeaderStyle ForeColor="White" BackColor="#CC3333" Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="2%" CssClass="titleTableGeneral"></HeaderStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Code" HeaderText="Kode MRTC">
                                                <HeaderStyle Width="7%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMRTCCode" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Name" HeaderText="Nama MRTC">
                                                <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMRTCName" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
                                                <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealer" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="City.Province.ProvinceName" HeaderText="Provinsi">
                                                <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProvince" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="City.CityName" HeaderText="Kota">
                                                <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCity" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Address" HeaderText="Alamat">
                                                <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAddress" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>


                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="7">
                                <input id="btnChoose" style="width: 60px" disabled onclick="GetSelectedMRTC()" type="button"
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
