<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpSparepartSelectionOne.aspx.vb" Inherits=".PopUpSparepartSelectionOne" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<%--<html xmlns="http://www.w3.org/1999/xhtml">--%>
<html>
<head>
    <title>FrmSparepartSelection</title>
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

        function GetSelectedSparepart() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgSparePart");
            var Sparepart = '';
            for (i = 1; i < table.rows.length; i++) {

                var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (radioBtn != null && radioBtn.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        if (getQueryVariable("x") == "Territory") {
                            Sparepart = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText;
                        }
                        else {
                            Sparepart = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText;
                        }
                        window.returnValue = Sparepart;
                        bcheck = true;
                    }
                    else if (navigator.appName == "Netscape") {
                        if (getQueryVariable("x") == "Territory") {
                            Sparepart = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText;
                        }
                        else {
                            Sparepart = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText;
                        }
                        window.opener.dialogWin.returnFunc(Sparepart);
                        bcheck = true;
                    }
                    else {
                        if (getQueryVariable("x") == "Territory") {
                            Sparepart = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '') + ';' + table.rows[i].cells[2].innerHTML;
                        }
                        else {
                            Sparepart = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '') + ';' + table.rows[i].cells[2].innerHTML;
                        }
                        window.opener.dialogWin.returnFunc(Sparepart);
                        bcheck = true;
                    }
                    break;
                }
            }
            if (bcheck) {
                window.close();
            }
            else {
                alert("Silahkan pilih Sparepart");
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
<body bottommargin="10" topmargin="10">
    <form id="Form2" method="post" runat="server">
        <table id="Table1" cellspacing="10" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 21px">Sparepart - Daftar Sparepart Field Fix</td>
            </tr>
            <tr>
                <td background="../images/bg_hor_parts.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td align="left">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="91" style="width: 91px">Nomor&nbsp;Sparepart</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox ID="txtNoSparepart" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtNoRangka','<>?*%$;')"
                                    runat="server" Width="230px"></asp:TextBox><input id="hdnIndent" type="hidden" runat="server"></td>
                        </tr>
                        <asp:Panel Visible="False">
                            <tr>
                                <td class="titleField" width="91" style="width: 91px">Nama Sparepart</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:TextBox ID="txtNamaSparepart" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtMoMesin','<>?*%$;')"
                                        runat="server" Width="228px"></asp:TextBox></td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td class="titleField" width="91" style="width: 91px"></td>
                            <td width="1%"></td>
                            <td width="75%">
                                <asp:Button ID="btnSearch" runat="server" Width="64px" Text="Cari"></asp:Button></td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td>
                    <div id="div1" style="height: 350px; overflow: auto">
                        <asp:DataGrid ID="dtgSparePart" runat="server" Width="100%" CellPadding="3" BackColor="#CDCDCD"
                            BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True"
                            AllowCustomPaging="True" AllowPaging="True" PageSize="25">
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableParts"></HeaderStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="PartNumber" HeaderText="Nomor Barang">
                                    <HeaderStyle ForeColor="White" Width="30%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartNumber") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="PartName" HeaderText="Nama Barang">
                                    <HeaderStyle ForeColor="White" Width="40%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ModelCode" HeaderText="Model">
                                    <HeaderStyle ForeColor="White" Width="40%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblModelCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ModelCode") %>'>
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
                <td align="center">
                    <br>
                    <input id="btnChoose" style="width: 60px" onclick="GetSelectedSparepart()" type="button" value="Pilih"
                        name="btnChoose">&nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                            name="btnCancel"></td>
            </tr>
        </table>
        <input id="Hidden1" type="hidden" name="Hidden1" runat="server">
    </form>
</body>
</html>
