<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpSalesmanArea.aspx.vb" Inherits="PopUpSalesmanArea" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmAplikasiHeader</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <base target="_self">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">

        function GetSelectedArea() {
            var Hidden1 = document.getElementById("Hidden1");
            var table;
            table = document.getElementById("dgSalesmanArea");
            var find = false;
            for (i = 1; i < table.rows.length; i++) {
                var radioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (radioButton != null && radioButton.checked) {
                    var AreaCode = "";
                    var AreaDesc = "";
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        AreaCode = replace(table.rows[i].cells[1].innerText,' ','');
                        AreaDesc = replace(table.rows[i].cells[2].innerText, ' ', '');
                        window.returnValue = AreaCode + ";" + AreaDesc;
                    }
                    else if (navigator.appName == "Netscape") {
                        AreaCode = replace(table.rows[i].cells[1].innerText, ' ', '');
                        AreaDesc = replace(table.rows[i].cells[2].innerText, ' ', '');
                        opener.dialogWin.returnFunc(AreaCode + ";" + AreaDesc);
                    }
                    else {
                        AreaCode = table.rows[i].cells[1].getElementsByTagName("SPAN")[0].innerHTML;
                        AreaDesc = table.rows[i].cells[2].getElementsByTagName("SPAN")[0].innerHTML;
                        opener.dialogWin.returnFunc(AreaCode + ";" + AreaDesc);
                    }
                    //break;
                    //window.returnValue = AreaCode+";"+AreaDesc+";"+retailPrice;
                    find = true;
                    break;
                }
            }
            if (find)
                window.close();
            else
                alert("Silahkan pilih Area");
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">UMUM&nbsp;- Salesman Area</td>
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
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="height: 17px" width="24%">Kode Area</td>
                            <td style="height: 17px" width="1%">
                                <asp:Label ID="lblColon3" runat="server">:</asp:Label></td>
                            <td style="height: 17px" width="25%">
                                <asp:TextBox ID="txtAreaCode" runat="server" size="22" MaxLength="20"></asp:TextBox></td>
                            <td class="titleField" style="height: 17px" width="20%">Kota</td>
                            <td style="height: 17px" width="1%">
                                <asp:Label ID="Label3" runat="server">:</asp:Label></td>
                            <td style="height: 17px" width="29%">
                                <asp:TextBox ID="txtCity" runat="server" size="22" MaxLength="20"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 10px" width="24%">Deskripsi Area</td>
                            <td style="height: 10px" width="1%">
                                <asp:Label ID="Label1" runat="server">:</asp:Label></td>
                            <td style="height: 10px" width="25%">
                                <asp:TextBox ID="txtAreaDesc" runat="server" size="22" MaxLength="20"></asp:TextBox></td>
                            <td class="titleField" style="height: 10px" width="20%"></td>
                            <td style="height: 10px" width="1%"></td>
                            <td style="height: 10px" width="29%">
                                <asp:Button ID="btnSearch" runat="server" Width="60px" Text="Cari"></asp:Button><asp:Button ID="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:Button></td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="6">
                                <div id="div1" style="overflow: auto; height: 330px">
                                    <asp:DataGrid ID="dgSalesmanArea" runat="server" Width="100%" AllowPaging="True" PageSize="25"
                                        AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
                                        CellPadding="3">
                                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <input type="radio" name="radio">
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="AreaCode" HeaderText="Kode Area">
                                                <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblKodeArea"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="AreaDesc" HeaderText="Desc Area">
                                                <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblDescArea"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="City" SortExpression="City" HeaderText="Kota">
                                                <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                            </asp:BoundColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="7">
                                <input id="btnChoose" style="width: 60px" onclick="GetSelectedArea()" type="button" value="Pilih"
                                    name="btnChoose">&nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                                        name="btnCancel">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 8px"></td>
            </tr>
        </table>
        <input id="Hidden1" type="hidden" name="Hidden1" runat="server">
    </form>
</body>
</html>
