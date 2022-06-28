<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpTrClassMany.aspx.vb" Inherits="PopUpTrClassMany" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>

    <title>PopUpClassSelectionMany</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <base target="_self">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">

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

        function GetSelectedClassMany() {
            var table;
            table = document.getElementById("dtgClassCourse");
            var Class = '';
            for (i = 1; i < table.rows.length; i++) {
                var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (CheckBox != null && CheckBox.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        if (Class == '') {
                            Class = replace(table.rows[i].cells[1].innerText, ' ', '');
                        }
                        else {
                            Class = Class + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
                        }
                        window.returnValue = Class;
                    }
                    else if (navigator.appName == "Netscape") {
                        if (Class == '') {
                            Class = replace(table.rows[i].cells[1].innerText, ' ', '');
                        }
                        else {
                            Class = Class + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
                        }
                        opener.dialogWin.returnFunc(Class);
                    }
                    else {
                        if (Class == '') {
                            Class = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        else {
                            Class = Class + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        opener.dialogWin.returnFunc(Class);
                    }
                }
            }
            window.close();
        }

        function GetSelectedPart() {

            var table, count = 0;
            table = document.getElementById("dtgClassCourse");
            for (i = 1; i < table.rows.length; i++) {
                var radioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];

                if (radioButton != null && radioButton.checked) {
                    count += 1;
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        var ClassCode = table.rows[i].cells[1].innerText;
                        var ClassName = table.rows[i].cells[2].innerText;
                        var Cap = table.rows[i].cells[5].innerText;
                        window.returnValue = ClassCode + ";" + ClassName + ";" + Cap;
                        break;
                    }
                    else if (navigator.appName == "Netscape") {
                        var ClassCode = table.rows[i].cells[1].innerText;
                        var ClassName = table.rows[i].cells[2].innerText;
                        var Cap = table.rows[i].cells[5].innerText;
                        opener.dialogWin.returnFunc(ClassCode + ";" + ClassName + ";" + Cap);
                        break;
                    }
                    else {
                        var ClassCode = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML;

                        var ClassName = table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML;

                        var Cap = table.rows[i].cells[5].innerHTML;

                        opener.dialogWin.returnFunc(ClassCode + ";" + ClassName + ";" + Cap);

                        break;
                    }
                }
            }

            if (count == 0) {

                window.alert("Silahkan Pilih Kelas");
            } else {

                window.close();
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="10" cellpadding="0" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" style="height: 18px" colspan="7">TRAINING -&nbsp;Pencarian 
									Kelas</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor_parts.gif" height="1">
                                <img height="1" src="../images/bg_hor_parts.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                        <tr id="rowCategory" runat="server">
                            <td>
                                <table id="tablebaru" cellspacing="10" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td class="titleField" style="height: 21px" width="20%">Pilih Kategory</td>
                                        <td style="height: 21px" width="1%">:</td>
                                        <td style="height: 21px" width="25%">
                                            <asp:DropDownList ID="ddlJobPositionCategory" runat="server" Width="152px"></asp:DropDownList></td>
                                        <td style="width: 17px; height: 21px" width="2%"></td>

                                    </tr>
                                </table>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <cc1:compositefilter id="cfClassCourse" runat="server" DataGridSouce="dtgClassCourse"></cc1:compositefilter></td>
                        </tr>
                        
                        <tr>
                            <td>
                                <div id="div1" style="overflow: auto; height: 370px">
                                    <asp:DataGrid ID="dtgClassCourse" runat="server" PageSize="25" AllowSorting="True" AutoGenerateColumns="False"
                                        BorderColor="#CDCDCD" BackColor="Gainsboro" CellPadding="3" CellSpacing="1" BorderWidth="0px" Width="100%" AllowCustomPaging="True" AllowPaging="True"
                                        Font-Names="MS Reference Sans Serif" ForeColor="GhostWhite" BorderStyle="None" GridLines="Horizontal">
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <HeaderTemplate>
                                                    <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="ClassCode" HeaderText="Kode Kelas">
                                                <HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClassCode") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="ClassName" HeaderText="Nama Kelas">
                                                <HeaderStyle Width="40%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClassName") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn SortExpression="StartDate" DataField="StartDate" DataFormatString="{0:dd/MM/yyyy}"
                                                HeaderText="Tgl Mulai" HeaderStyle-CssClass="titleTableService"></asp:BoundColumn>
                                            <asp:BoundColumn SortExpression="FinishDate" DataField="FinishDate" DataFormatString="{0:dd/MM/yyyy}"
                                                HeaderText="Tgl Selesai" HeaderStyle-CssClass="titleTableService"></asp:BoundColumn>
                                            <asp:BoundColumn SortExpression="Capacity" DataField="Capacity" HeaderText="Kapasitas" HeaderStyle-CssClass="titleTableService"></asp:BoundColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <input id="btnChoose" style="width: 60px" onclick="GetSelectedClassMany()" type="button"
                                    value="Pilih" name="btnChoose">&nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                                        name="btnCancel"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
