<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpVechileColorMultipleSelection.aspx.vb" Inherits=".PopUpVechileColorMultipleSelection" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Pilihan Kode Warna</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <base target="_self">
    <script language="javascript">


        function GetSelectedstrMaterialNumber() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgMaterialSelection");
            var strMaterialNumber = '';
            for (i = 1; i < table.rows.length; i++) {
                var CheckBox = table.rows[i].cells[1].getElementsByTagName("INPUT")[0];
                if (CheckBox != null && CheckBox.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        if (strMaterialNumber == '') {
                            strMaterialNumber = replace(table.rows[i].cells[2].innerText, ' ', '');
                        }
                        else {
                            strMaterialNumber = strMaterialNumber + ';' + replace(table.rows[i].cells[2].innerText, ' ', '');
                        }
                        window.returnValue = strMaterialNumber;
                        bcheck = true;
                    }
                    else if (navigator.appName == "Netscape") {
                        if (strMaterialNumber == '') {
                            strMaterialNumber = replace(table.rows[i].cells[2].innerText, ' ', '');
                        }
                        else {
                            strMaterialNumber = strMaterialNumber + ';' + replace(table.rows[i].cells[2].innerText, ' ', '');
                        }
                        bcheck = true;
                    }
                    else {
                        if (strMaterialNumber == '') {
                            strMaterialNumber = replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        else {
                            strMaterialNumber = strMaterialNumber + ';' + replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        bcheck = true;
                    }
                }
            }
            if (bcheck) {
                window.close();
                if (navigator.appName != "Microsoft Internet Explorer")
                { opener.dialogWin.returnFunc(strMaterialNumber); }
            }
            else {
                alert("Silahkan Pilih Material Number terlebih dahulu");
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
        <asp:HiddenField ID="hdnMaterialNumber" runat="server" />
        <table id="Table1" cellspacing="0" cellpadding="4" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage">Pesanan Kendaraan -&nbsp;Kode Warna &nbsp;</td>
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
                            <td>
                                <table>
                                    <tr>
                                        <td class="titleField" width="20%">Material Number</td>
                                        <td width="1%">:</td>
                                        <td width="25%">
                                            <asp:TextBox ID="txtMaterialNumber" Width="120px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtMaterialNumber','<>?*%$;')" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="height: 13px">Deskripsi Kendaraan</td>
                                        <td style="height: 13px">:</td>
                                        <td style="height: 13px">
                                            <asp:TextBox ID="txtDeskripsi" Width="90%" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtDeskripsi','<>?*%$;')" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="height: 15px"></td>
                                        <td style="height: 15px"></td>
                                        <td style="height: 15px">
                                            <asp:Button ID="btnSearch" runat="server" Text=" Cari "></asp:Button></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td><br /></td>
                        </tr>
                        <tr>
                            <td>
                                <div id="div1" style="overflow: auto; height: 280px">
                                    <asp:DataGrid ID="dtgMaterialSelection" runat="server" Width="100%" AutoGenerateColumns="False"
                                        AllowSorting="True">
                                        <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                                        <HeaderStyle ForeColor="White" BackColor="#CC3333" Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <HeaderTemplate>
                                                    <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
													document.forms[0].chkAllItems.checked)" />
                                                </HeaderTemplate>
                                                <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn SortExpression="test" HeaderText="Kode">
											    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
											    <ItemTemplate>
												    <asp:Label id="lblVechileTypeCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MaterialNumber") %>'>
												    </asp:Label>
											    </ItemTemplate>
											</asp:TemplateColumn>

                                            <%--<asp:BoundColumn DataField="MaterialNumber" HeaderText="Material Number">
                                                <HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle Width="30%" HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>--%>
                                            <asp:BoundColumn DataField="MaterialDescription" HeaderText="Deskripsi">
                                                <HeaderStyle Width="60%" CssClass="titleTableSales"></HeaderStyle>
                                            </asp:BoundColumn>

                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <input id="btnChoose" style="width: 60px" disabled onclick="GetSelectedstrMaterialNumber()" type="button"
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
