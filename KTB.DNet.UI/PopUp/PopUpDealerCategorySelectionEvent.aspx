<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpDealerCategorySelectionEvent.aspx.vb" Inherits=".PopUpDealerCategorySelectionEvent" %>

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

        function GetSelectedDealer() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgDealerSelection");
            var Dealer = '';
            for (i = 1; i < table.rows.length; i++) {
                var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (CheckBox != null && CheckBox.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        if (Dealer == '') {
                            var ddlCat = document.getElementById("ddlCategory");
                            Dealer = ddlCat.value + ';'+ replace(table.rows[i].cells[1].innerText, ' ', '');
                            //Dealer = replace(table.rows[i].cells[1].innerText, ' ', '') + '|' + table.rows[i].cells[2].innerText + '|' + replace(table.rows[i].cells[6].innerText, ' ', '');
                        }
                        else {
                            Dealer = Dealer + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
                        }
                        window.returnValue = Dealer;
                        bcheck = true;
                    }
                    else if (navigator.appName == "Netscape") {
                        if (Dealer == '') {
                            var ddlCat = document.getElementById("ddlCategory");
                            Dealer = ddlCat.value + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
                            //Dealer = replace(table.rows[i].cells[1].innerText, ' ', '') + '|' + table.rows[i].cells[2].innerText + '|' + replace(table.rows[i].cells[6].innerText, ' ', '');
                        }
                        else {
                            Dealer = Dealer + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
                        }
                        bcheck = true;
                    }
                    else {
                        if (Dealer == '') {
                            Dealer = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                            //Dealer = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '') + '|' + table.rows[i].cells[2].innerHTML + '|' + table.rows[i].cells[6].innerHTML;
                        }
                        else {
                            Dealer = Dealer + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        bcheck = true;
                    }
                }
            }
            if (bcheck) {
                window.close();
                if (navigator.appName != "Microsoft Internet Explorer")
                { opener.dialogWin.returnFunc(Dealer); }
            }
            else {
                alert("Silahkan Pilih Dealer terlebih dahulu");
            }
        }

        function CheckAll(aspCheckBoxID, checkVal) {
            re = new RegExp(':' + aspCheckBoxID + '$')
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox') {
                    if (re.test(elm.name)) {
                        elm.checked = checkVal
                    }
                }
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
        <table id="Table1" cellspacing="0" cellpadding="6" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" colspan="7">DEALER -&nbsp;Pencarian Dealer</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor_sales.gif" colspan="7" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                    </table>
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr valign="top">
                            <td class="titleField" style="height: 13px">Dealer Category</td>
                            <td style="height: 13px">:</td>
                            <td style="height: 13px">
                                <asp:DropDownList ID="ddlCategory" runat="server">
                                    <asp:ListItem Text="Silahkan Pilih" Value="-1"></asp:ListItem>
                                </asp:DropDownList></td>
                            <td style="width: 17px; height: 20px" width="2%"></td>
                            <td class="titleField" width="20%" style="height: 20px">Term Cari 1</td>
                            <td width="1%" style="height: 20px">:</td>
                            <td width="30%" style="height: 20px">
                                <asp:TextBox ID="txtSearch1" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" width="10%" style="height: 20px">Dealer Group</td>
                            <td width="1%" style="height: 20px">:</td>
                            <td width="38%" style="height: 20px">
                                <asp:TextBox ID="txtDealerName" runat="server" Width="152px"></asp:TextBox></td>
                            <td style="width: 17px; height: 13px"></td>
                            <td class="titleField" style="height: 13px">Term Cari 2</td>
                            <td style="height: 13px">:</td>
                            <td style="height: 13px">
                                <asp:TextBox ID="txtSearch2" runat="server"></asp:TextBox><asp:Button ID="btnSearch" runat="server" Text=" Cari "></asp:Button></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="width: 17px; height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <div id="div1" style="overflow: auto; height: 260px">
                                    <asp:DataGrid ID="dtgDealerSelection" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="2"
                                        AllowSorting="True">
                                        <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                                        <HeaderStyle ForeColor="white" BackColor="#CC3333" Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="2%"></HeaderStyle>
                                                <HeaderTemplate>
                                                    <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                                <HeaderStyle Width="7%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                            <asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Dealer Name">
                                                <HeaderStyle Width="21%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                            <asp:TemplateColumn SortExpression="Dealer.City.CityName" HeaderText="Kode Dealer">
                                                <HeaderStyle Width="10%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCityName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.City.CityName")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                            <asp:TemplateColumn SortExpression="Dealer.City.CityName" HeaderText="Dealer Group">
                                                <HeaderStyle Width="10%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGroupName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerGroup.GroupName")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                            <asp:TemplateColumn SortExpression="Dealer.City.CityName" HeaderText="Term Cari 1">
                                                <HeaderStyle Width="10%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSearchTerm1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                            <asp:TemplateColumn SortExpression="Dealer.City.CityName" HeaderText="Term Cari 2">
                                                <HeaderStyle Width="10%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSearchTerm2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm2")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <%--<asp:BoundColumn DataField="Dealer.DealerName" SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
                                                <HeaderStyle Width="21%"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn SortExpression="Dealer.City.CityName" HeaderText="Kota">
                                                <HeaderStyle Width="10%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCity" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Dealer.DealerGroup.GroupName" HeaderText="Grup">
                                                <HeaderStyle Width="10%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGroup" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Dealer.SearchTerm1" SortExpression="Dealer.SearchTerm1" HeaderText="Term Cari 1">
                                                <HeaderStyle Width="12%"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Dealer.SearchTerm2" SortExpression="Dealer.SearchTerm2" HeaderText="Term Cari 2">
                                                <HeaderStyle Width="15%"></HeaderStyle>
                                            </asp:BoundColumn>--%>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="7">
                                <input id="btnChoose" style="width: 60px" disabled onclick="GetSelectedDealer()" type="button"
                                    value="Pilih" name="btnChoose" runat="server">&nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Batal"
                                        name="btnCancel"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
