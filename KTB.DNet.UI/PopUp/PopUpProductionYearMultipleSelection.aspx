<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpProductionYearMultipleSelection.aspx.vb" Inherits=".PopUpProductionYearMultipleSelection" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Pilihan Tahun Perakitan</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <base target="_self">
    <script language="javascript">


        function GetSelectedProductionYear() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgProductionYear");
            var strProductionYear = '';
            for (i = 1; i < table.rows.length; i++) {
                var CheckBox = table.rows[i].cells[1].getElementsByTagName("INPUT")[0];
                if (CheckBox != null && CheckBox.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        if (strProductionYear == '') {
                            strProductionYear = replace(table.rows[i].cells[2].innerText, ' ', '');
                        }
                        else {
                            strProductionYear = strProductionYear + ';' + replace(table.rows[i].cells[2].innerText, ' ', '');
                        }
                        window.returnValue = strProductionYear;
                        bcheck = true;
                    }
                    else if (navigator.appName == "Netscape") {
                        if (strProductionYear == '') {
                            strProductionYear = replace(table.rows[i].cells[2].innerText, ' ', '');
                        }
                        else {
                            strProductionYear = strProductionYear + ';' + replace(table.rows[i].cells[2].innerText, ' ', '');
                        }
                        bcheck = true;
                    }
                    else {
                        if (strProductionYear == '') {
                            strProductionYear = replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        else {
                            strProductionYear = strProductionYear + ';' + replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        bcheck = true;
                    }
                }
            }
            if (bcheck) {
                window.close();
                if (navigator.appName != "Microsoft Internet Explorer")
                { opener.dialogWin.returnFunc(strProductionYear); }
            }
            else {
                alert("Silahkan Pilih Tahun Perakitan terlebih dahulu");
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
        <asp:HiddenField ID="hdnProductionYear" runat="server" />
        <table id="Table1" cellspacing="0" cellpadding="4" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage">
								PROGRAM DISKON REGULER&nbsp;- Tahun Perakitan</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor_sales.gif" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td><br /></td>
                        </tr>
                        <tr>
                            <td>
                                <div id="div1" style="overflow: auto; height: 370px">
                                    <asp:DataGrid ID="dtgProductionYear" runat="server" Width="100%" AutoGenerateColumns="False"
                                        AllowSorting="True">
                                        <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                                        <HeaderStyle ForeColor="White" BackColor="#CC3333" Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle Width="2%" HorizontalAlign="Center"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <HeaderTemplate>
                                                    <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
													document.forms[0].chkAllItems.checked)" />
                                                </HeaderTemplate>
                                                <ItemStyle Width="2%" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Tahun Perakitan">
											    <HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle Width="20%" HorizontalAlign="Center"></ItemStyle>
											    <ItemTemplate>
												    <asp:Label id="lblProductionYear" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProductionYear")%>'>
												    </asp:Label>
											    </ItemTemplate>
											</asp:TemplateColumn>

                                            <%--<asp:BoundColumn DataField="ProductionYear" HeaderText="Tahun Perakitan">
                                                <HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle Width="20%" HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>--%>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <input id="btnChoose" style="width: 60px" disabled onclick="GetSelectedProductionYear()" type="button"
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
