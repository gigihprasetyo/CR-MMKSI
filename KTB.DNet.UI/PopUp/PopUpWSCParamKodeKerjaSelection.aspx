<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpWSCParamKodeKerjaSelection.aspx.vb" Inherits=".PopUpWSCParamKodeKerjaSelection" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PopUpWSCParamKodeKerjaSelection</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/BasicStyle.css" type="text/css" rel="stylesheet">
    <base target="_self">
    <script language="javascript">
        function GetSelectedWorkCode() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgWorkCodeSelection");
            var tmpPos = document.getElementById("hdnWorkCode");
            var WorkCode = '';
            for (i = 1; i < table.rows.length; i++) {
                var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (CheckBox != null && CheckBox.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        if (WorkCode == '') {
                            WorkCode = replace(table.rows[i].cells[1].innerText, ' ', '');
                        }
                        else {
                            WorkCode = WorkCode + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
                        }
                        if (tmpPos.value != '') {
                            window.returnValue = tmpPos.value;
                        }
                        else {
                            window.returnValue = WorkCode
                        }
                        bcheck = true;
                    }
                    else if (navigator.appName == "Netscape") {
                        if (WorkCode == '') {
                            WorkCode = replace(table.rows[i].cells[1].innerText, ' ', '');
                        }
                        else {
                            WorkCode = WorkCode + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
                        }
                        bcheck = true;
                    }
                    else {
                        if (WorkCode == '') {
                            WorkCode = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        else {
                            WorkCode = WorkCode + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        bcheck = true;
                    }
                }
            }
            if (bcheck) {
                window.close();
                if (navigator.appName != "Microsoft Internet Explorer")
                { opener.dialogWin.returnFunc(WorkCode); }
            }
            else {
                alert("Silahkan Pilih Nomor Buletin terlebih dahulu");
            }
        }

        function tmpCB(val) {

            var index = val.parentNode.parentNode.rowIndex;
            var tmpPos = document.getElementById("hdnWorkCode");
            var table = document.getElementById("dtgWorkCodeSelection");
            var WorkCode = tmpPos.value;
            var CheckBox = table.rows[index].cells[0].getElementsByTagName("INPUT")[0];
            var newVal = replace(table.rows[index].cells[1].innerText, ' ', '')
            if (CheckBox != null && CheckBox.checked) {
                if (WorkCode == '') {
                    tmpPos.value = newVal;
                }
                else {
                    var PCode = WorkCode.split(';')
                    for (var i = 0; i <= PCode.length; i++) {
                        if (pcode[i] != newVal) {
                            WorkCode = WorkCode + ';' + newVal;
                        }
                    }
                }
                tmpPos.value = WorkCode;
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
                            <td class="titlePage" colspan="7">Daftar - Kode Posisi</td>
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
                            <td class="titleField" width="20%" style="height: 20px">Kode Kerja</td>
                            <td width="1%" style="height: 20px">:</td>
                            <td width="25%" style="height: 20px">
                                <asp:TextBox ID="txtRecallRegNo" runat="server" Width="152px"></asp:TextBox></td>
                            <td style="width: 17px; height: 20px" width="2%"></td>
                            <td class="titleField" width="20%" style="height: 20px">&nbsp;</td>
                            <td width="1%" style="height: 20px">&nbsp;</td>
                            <td width="33%" style="height: 20px">&nbsp;</td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" style="height: 13px">Deskripsi</td>
                            <td style="height: 13px">:</td>
                            <td style="height: 13px">
                                <asp:TextBox ID="txtDEscription" runat="server" Width="152px"></asp:TextBox></td>
                            <td style="width: 17px; height: 13px"></td>
                            <td class="titleField" style="height: 13px">&nbsp;</td>
                            <td style="height: 13px">&nbsp;</td>
                            <td style="height: 13px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px">
                                <asp:Button ID="btnSearch" runat="server" Text=" Cari "></asp:Button></td>
                            <td style="width: 17px; height: 15px"><asp:HiddenField ID="hdnWorkCode" runat="server" /></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <div id="div1" style="overflow: auto; height: 260px">
                                    <asp:DataGrid ID="dtgWorkCodeSelection" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="2"
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
                                            <asp:TemplateColumn SortExpression="KodeKerja" HeaderText="Kode Kerja">
                                                <HeaderStyle Width="15%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWorkCodeCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.KodeKerja")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
                                                <HeaderStyle Width="40%"></HeaderStyle>
                                            </asp:BoundColumn>

                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="7">
                                <input id="btnChoose" style="width: 60px" disabled onclick="GetSelectedWorkCode()" type="button"
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
