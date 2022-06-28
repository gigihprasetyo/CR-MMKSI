<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpSalesmanMultiple.aspx.vb" Inherits=".PopUpSalesmanMultiple" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmSalesmanSelection</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <base target="_self">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function GetSelectedSalesman() {
            var table;
            var bcheck = false;
            table = document.getElementById("dgSalesman");
            var Salesman = '';
            for (i = 1; i < table.rows.length; i++) {
                var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (CheckBox != null && CheckBox.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        if (Salesman == '') {
                            Salesman = replace(table.rows[i].cells[1].innerText, ' ', '');
                        }
                        else {
                            Salesman = Salesman + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
                        }
                        bcheck = true;
                    }
                    else if (navigator.appName == "Netscape") {
                        if (Salesman == '') {
                            Salesman = replace(table.rows[i].cells[1].innerText, ' ', '');
                        }
                        else {
                            Salesman = Salesman + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
                        }
                        bcheck = true;
                    }
                    else {
                        if (Salesman == '') {
                            Salesman = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        else {
                            Salesman = Salesman + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        bcheck = true;
                    }
                }
            }
            if (bcheck) {
                window.close();
                if (navigator.appName != "Microsoft Internet Explorer")
                { opener.dialogWin.returnFunc(Salesman); }
                else {
                    window.returnValue = Salesman;
                }
            }
            else {
                alert("Silahkan Pilih Salesman terlebih dahulu");
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
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">Daftar Event&nbsp;- Salesman</td>
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
                                                <HeaderTemplate>
                                                    <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
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
                                            <asp:TemplateColumn SortExpression="JobPosition.ID" HeaderText="Kode Posisi">
                                                <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Label0" Text='<%# DataBinder.Eval(Container, "DataItem.JobPosition.Code") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="JobPosition.ID" HeaderText="Deskripsi Posisi">
                                                <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Label1" Text='<%# DataBinder.Eval(Container, "DataItem.JobPosition.Description") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Dealer.ID" HeaderText="Cabang Dealer">
                                                <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Label2" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <%--<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>--%>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="7">
                                <INPUT id="btnChoose" style="width: 60px" onclick="GetSelectedSalesman()" type="button"
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
