<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpCity.aspx.vb" Inherits="PopUpCity" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>

    <title>PopUpCity</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <base target="_self">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">

        function getSelectedCourse() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgCity");
            for (i = 1; i < table.rows.length; i++) {
                var RadioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (RadioButton != null && RadioButton.checked) {
                    
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        var CityCode = replace(table.rows[i].cells[1].innerText, ' ', '');
                        var CityName = table.rows[i].cells[2].innerText
                        window.returnValue = CityCode + ";" + CityName;
                    }
                    else if (navigator.appName == "Netscape") {
                        var CityCode = replace(table.rows[i].cells[1].innerText, ' ', '');
                        var CityName = table.rows[i].cells[2].innerText
                        opener.dialogWin.returnFunc(CityCode + ";" + CityName);
                    }
                    else {
                        var CityCode = table.rows[i].cells[1].getElementsByTagName("SPAN")[0].innerHTML;
                        var CityName = table.rows[i].cells[2].getElementsByTagName("SPAN")[0].innerHTML;
                        opener.dialogWin.returnFunc(CityCode + ";" + CityName);
                    }
                    //break;
                    //window.returnValue = AreaCode+";"+AreaDesc+";"+retailPrice;
                    bcheck = true;
                    break;
                }
            }
            if (bcheck) {
                window.close();
            }
            else {
                alert("Silahkan pilih Kota");
            }
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
                            <td class="titlePage" style="height: 18px" colspan="7">KOTA&nbsp;- Pencarian Kota</td>
                        </tr>
                        <tr>
                            <td style="height: 1px" background="../images/bg_hor_sales.gif" colspan="7" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 22px" width="20%">Kode Kota</td>
                            <td style="height: 22px" width="1%">:</td>
                            <td style="height: 22px" width="25%">
                                <asp:TextBox ID="txtKode" runat="server" Width="152px"></asp:TextBox></td>
                            <td style="width: 17px; height: 22px" width="2%"></td>
                            <td class="titleField" style="height: 22px" width="20%">
                                <asp:Button ID="btnSearch" runat="server" Text=" Cari " Width="80px"></asp:Button></td>
                            <td style="height: 22px" width="1%"></td>
                            <td style="height: 22px" width="33%"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 13px">Nama Kota</td>
                            <td style="height: 13px">:</td>
                            <td style="height: 13px">
                                <asp:TextBox ID="txtDeskripsi" runat="server" Width="152px"></asp:TextBox></td>
                            <td style="width: 17px; height: 13px"></td>
                            <td class="titleField" style="height: 13px"></td>
                            <td style="height: 13px"></td>
                            <td style="height: 13px"></td>
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
                                <div id="div1" style="overflow: auto; height: 350px">
                                    <asp:DataGrid ID="dtgCity" runat="server" Width="100%" AutoGenerateColumns="False" GridLines="Horizontal"
                                        CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowPaging="True" AllowCustomPaging="True"
                                        AllowSorting="True" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="CityCode" HeaderText="Kode">
                                                <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblCityCode" Text='<%# DataBinder.Eval(Container, "DataItem.CityCode")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="CityName" HeaderText="Nama">
                                                <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblName" Text='<%# DataBinder.Eval(Container, "DataItem.CityName")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                           <%-- <asp:BoundColumn DataField="CityCode" SortExpression="CityCode" HeaderText="Kode">
                                                <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="CityName" SortExpression="CityName" HeaderText="Nama">
                                                <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                            </asp:BoundColumn>--%>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="7">
                                <input id="btnChoose" style="width: 60px" disabled onclick="getSelectedCourse();" type="button"
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
