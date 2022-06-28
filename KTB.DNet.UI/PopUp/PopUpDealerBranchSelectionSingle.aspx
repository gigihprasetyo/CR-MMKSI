<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpDealerBranchSelectionSingle.aspx.vb" Inherits=".PopUpDealerBranchSelectionSingle" %>

<!DOCTYPE html>

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

        function GetSelectedDealer() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgDealerSelection");
            var Dealer = '';
            for (i = 1; i < table.rows.length; i++) {

                var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (radioBtn != null && radioBtn.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        Dealer = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText + ';' + replace(table.rows[i].cells[3].innerText, ' ', '');
                        window.returnValue = Dealer;
                        bcheck = true;
                    }
                    else if (navigator.appName == "Netscape") {
                        Dealer = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText + ';' + replace(table.rows[i].cells[3].innerText, ' ', '');

                        window.opener.dialogWin.returnFunc(Dealer);
                        bcheck = true;
                    }
                    else {
                        Dealer = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '') + ';' + table.rows[i].cells[2].innerHTML + ';' + table.rows[i].cells[3].innerHTML;
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
                alert("Silahkan pilih cabang");
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
                            <td class="titlePage" colspan="7">Cabang DEALER -&nbsp;Pencarian Cabang Dealer &nbsp;</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor_sales.gif" colspan="7" height="1" style="height: 1px !important;"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField" style="height: 13px">Kode Cabang</td>
                            <td style="height: 13px">:</td>
                            <td style="height: 13px">
                                <asp:TextBox ID="txtBranchCode"
                                    runat="server" Width="152px"></asp:TextBox>&nbsp;</td>
                            <td style="width: 17px; height: 13px"></td>
                            <td class="titleField" style="height: 13px">Term Cari 1</td>
                            <td style="height: 13px">:</td>
                            <td style="width: 225px; height: 13px">
                                <asp:TextBox ID="txtSearch1"
                                    runat="server"></asp:TextBox><asp:Button ID="btnSearch" runat="server" Text=" Cari "></asp:Button></td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField" width="20%">Nama Cabang Dealer</td>
                            <td width="1%">:</td>
                            <td width="25%">
                                <asp:TextBox ID="txtBranchName"
                                    runat="server" Width="152px"></asp:TextBox></td>
                            <td style="width: 17px" width="2%"></td>
                            <td class="titleField" width="20%">Term Cari 2</td>
                            <td width="1%">:</td>
                            <td style="width: 225px" width="225">
                                <asp:TextBox ID="txtSearch2"
                                    runat="server"></asp:TextBox></td>
                        </tr>


                        <tr>
                            <td class="titleField" style="height: 15px">Kode Dealer</td>
                            <td style="height: 15px">:</td>
                            <td style="height: 15px">
                                <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                            </td>
                            <td style="width: 17px; height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="width: 225px; height: 15px"></td>
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
                                    <asp:DataGrid ID="dtgDealerSelection" runat="server" Width="100%" AutoGenerateColumns="False"
                                        AllowSorting="True">
                                        <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                                        <HeaderStyle ForeColor="White" BackColor="#CC3333" Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="2%" CssClass="titleTableGeneral"></HeaderStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="DealerCode" HeaderText="Kode Cabang">
                                                <HeaderStyle Width="7%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerBranchCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="DealerName" SortExpression="DealerName" HeaderText="Nama Cabang">
                                                <HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
                                            </asp:BoundColumn>


                                            <asp:BoundColumn DataField="SearchTerm1" SortExpression="SearchTerm1" HeaderText="Term Cari 1">
                                                <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="SearchTerm2" SortExpression="SearchTerm1" HeaderText="Term Cari 2">
                                                <HeaderStyle Width="12%" CssClass="titleTableGeneral"></HeaderStyle>
                                            </asp:BoundColumn>

                                            <asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="Dealer.DealerCode">
                                                <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ParentDealer.DealerCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="7">
                                <input id="btnChoose" style="width: 60px" disabled onclick="GetSelectedDealer()" type="button"
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
