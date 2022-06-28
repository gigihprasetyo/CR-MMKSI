<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpSalesman.aspx.vb" Inherits="PopUpSalesman" SmartNavigation="False" %>

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

        function GetSelectedSalesman() {
            var Hidden1 = document.getElementById("Hidden1");
            var table;
            table = document.getElementById("dgSalesman");
            var find = false;
            for (i = 1; i < table.rows.length; i++) {
                var radioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (radioButton != null && radioButton.checked) {
                    var SalesmanCode = "";
                    var SalesmanName = "";
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        var SalesmanCode = replace(table.rows[i].cells[1].innerText, ' ', '');
                        var SalesmanName = replace(table.rows[i].cells[2].innerText, ' ', '');
                        window.returnValue = SalesmanCode + ";" + SalesmanName;
                    }
                    else if (navigator.appName == "Netscape") {
                        var SalesmanCode = replace(table.rows[i].cells[1].innerText, ' ', '');
                        var SalesmanName = replace(table.rows[i].cells[2].innerText, ' ', '');
                        opener.dialogWin.returnFunc(SalesmanCode + ";" + SalesmanName);
                    }
                    else {
                        SalesmanCode = table.rows[i].cells[1].getElementsByTagName("SPAN")[0].innerHTML;
                        SalesmanName = table.rows[i].cells[2].getElementsByTagName("SPAN")[0].innerHTML;
                        opener.dialogWin.returnFunc(SalesmanCode + ";" + SalesmanName);
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
                <td class="titlePage" style="height: 17px">UMUM&nbsp;- Salesman</td>
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
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 22px" width="20%">No KTP</td>
                <td style="height: 22px" width="1%">:</td>
                <td style="height: 22px" width="25%">
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtNoKTP" onblur="omitSomeCharacter('txtSalesmanCode','<>?*%$;')"
                        runat="server" Width="152px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 22px" width="20%">Kode Salesman</td>
                <td style="height: 22px" width="1%">:</td>
                <td style="height: 22px" width="25%">
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtSalesmanCode" onblur="omitSomeCharacter('txtSalesmanCode','<>?*%$;')"
                        runat="server" Width="152px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 13px">Nama Salesman</td>
                <td style="height: 13px">:</td>
                <td style="height: 13px">
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtName" onblur="omitSomeCharacter('txtName','<>?*%$;')"
                        runat="server" Width="152px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 15px"></td>
                <td style="height: 15px"></td>
                <td style="height: 15px">
                    <asp:Button ID="btnSearch" runat="server" Width="80px" Text=" Cari "></asp:Button></td>
            </tr>
            <tr>
                <td colspan="4">
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td valign="top" colspan="6">
                                <div id="div1" style="overflow: auto; height: 330px">
                                    <asp:DataGrid ID="dgSalesman" runat="server" CellPadding="3" BorderWidth="0px" CellSpacing="1"
                                        BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True" PageSize="25" AllowPaging="True"
                                        Width="100%">
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
                                            <asp:TemplateColumn SortExpression="SalesmanCode" HeaderText="Kode Salesman">
                                                <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblKodeSalesman" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanCode") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Name" HeaderText="Nama Salesman">
                                                <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblName" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="JobPosition.ID" HeaderText="Kategori">
                                                <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Label1" Text='<%# DataBinder.Eval(Container, "DataItem.JobPosition.Description") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                                <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblDealerCode" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Dealer.SearchTerm1" HeaderText="Search Term 1">
                                                <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblSearchTerm1" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1")%>'>
                                                    </asp:Label>
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
                                <input id="btnChoose" style="width: 60px" onclick="GetSelectedSalesman()" type="button"
                                    value="Pilih" name="btnChoose">&nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
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
