<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpEquipmentSelection.aspx.vb" Inherits="PopUpEquipmentSelection" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>PopUp Equipment Selection</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <base target="_self">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">

        function GetSelectedEquipment() {
            var table1;
            var table;
            table1 = document.getElementById("dgEquipmentList");
            for (i = 1; i < table1.rows.length; i++) {
                table = document.getElementById("dgEquipmentList__ctl" + (i + 2) + "_dtgHeader");
                if (i == 1) {
                    var radioButton = table.rows[1].cells[0].getElementsByTagName("INPUT")[0];
                }
                else {
                    var radioButton = table.rows[0].cells[0].getElementsByTagName("INPUT")[0];
                }
                if (radioButton != null && radioButton.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        if (i == 1) {
                            var Kode = table.rows[1].cells[1].innerText + ';' + table.rows[1].cells[2].innerText;
                        }
                        else {
                            //var Kode = table.rows[0].cells[1].innerText + ";" + table.rows[1].cells[1].innerText;
                            var partNumber = table.rows[i].cells[1].getElementsByTagName("SPAN")[0].innerHTML;
                            var partName = table.rows[i].cells[2].getElementsByTagName("SPAN")[0].innerHTML;
                            var Kode = partNumber + ';' + partName;
                        }
                        window.returnValue = Kode;
                    }
                    else if (navigator.appName == "Netscape") {
                        if (i == 1) {
                            var Kode = table.rows[1].cells[1].innerText + ';' + table.rows[1].cells[2].innerText;
                        }
                        else {
                            //var Kode = table.rows[0].cells[1].innerText + ";" + table.rows[1].cells[1].innerText;
                            var partNumber = table.rows[i].cells[1].getElementsByTagName("SPAN")[0].innerHTML;
                            var partName = table.rows[i].cells[2].getElementsByTagName("SPAN")[0].innerHTML;
                            var Kode = partNumber + ';' + partName;
                        }
                        opener.dialogWin.returnFunc(Kode);
                    }
                    else {
                        //alert(table.rows[0].cells[1].innerHTML)					
                        if (i == 1) {
                            var Kode = table.rows[1].cells[1].innerHTML;
                        }
                        else {
                            var Kode = table.rows[0].cells[1].innerHTML;
                        }
                        opener.dialogWin.returnFunc(Kode);
                    }
                    window.close();
                }
                if (navigator.appName == "Microsoft Internet Explorer") {
                    if (i == table1.rows.length - 1) {
                        alert('Silahkan Pilih Equipment');
                    }
                }
            }

        }
    </script>
    <style type="text/css">
        .HiddenCol {
            display: none;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table01" cellspacing="10" cellpadding="0" width="100%" border="0">
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="titlePage">EQUIPMENT SALES - Kode Equipment</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor.gif" height="1">
                                <img height="1" src="/images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td style="width: 20%; height: 4px" class="titleField">
                                <asp:Label ID="Label1" runat="server">Kode Equipment</asp:Label></td>
                            <td style="width: 1%; height: 4px">
                                <asp:Label ID="Label4" runat="server">:</asp:Label></td>
                            <td style="width: 29%; height: 4px">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtEquipmentNumber" runat="server"
                                    onblur="omitSomeCharacter('txtEquipmentNumber','<>?*%$;')"></asp:TextBox></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="width: 20%; height: 1px" class="titleField">
                                <asp:Label ID="Label2" runat="server">Spesifikasi</asp:Label></td>
                            <td style="width: 1%; height: 1px">
                                <asp:Label ID="Label5" runat="server">:</asp:Label></td>
                            <td style="width: 29%; height: 1px">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtSpecification" runat="server"
                                    onblur="omitSomeCharacter('txtSpecification','<>?*%$;')"></asp:TextBox></td>
                            <td style="height: 1px"></td>
                        </tr>
                        <tr>
                            <td style="width: 20%; height: 1px" class="titleField">
                                <asp:Label ID="Label3" runat="server">Nama Equipment</asp:Label></td>
                            <td style="width: 1%; height: 1px">
                                <asp:Label ID="Label6" runat="server">:</asp:Label></td>
                            <td style="width: 29%; height: 1px">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtDescription" runat="server"
                                    onblur="omitSomeCharacter('txtDescription','<>?*%$;')"></asp:TextBox></td>
                            <td style="height: 1px">
                                <asp:Button ID="btnSearch" runat="server" Text="Cari" Width="76px"></asp:Button></td>
                        </tr>
                        <tr>
                            <td style="width: 100%; height: 1px" valign="top" colspan="4">
                                <div id="div1" style="overflow: auto; height: 300px">
                                    <asp:DataGrid ID="dgEquipmentList" runat="server" Width="100%" HorizontalAlign="Right" AllowPaging="True"
                                        PageSize="25" AllowCustomPaging="True" AutoGenerateColumns="False" ShowHeader="False">
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" BackColor="Teal"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                                <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Equipment List">
                                                <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:DataGrid ID="dtgHeader" runat="server" AutoGenerateColumns="False" Width="100%" OnItemDataBound="dtgMaster_ItemDataBound">
                                                        <Columns>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                                                <ItemStyle Width="10%" HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn DataField="EquipmentNumber" HeaderText="Kode Equipment">
                                                                <HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
                                                                <ItemStyle Width="30%" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="Description" HeaderText="Nama Equipment">
                                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="Price" HeaderText="Harga">
                                                                <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                                                <ItemStyle Width="15%" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="Price" HeaderText="Harga">
                                                                <HeaderStyle CssClass="HiddenCol"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" CssClass="HiddenCol"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:TemplateColumn SortExpression="ID" HeaderText="ID">
                                                                <HeaderStyle ForeColor="White" Width="0%" CssClass="HiddenCol"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="server" CssClass="HiddenCol" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td valign="middle" align="center" colspan="4" height="40">
                                <input id="btnChoose" style="width: 60px" onclick="GetSelectedEquipment()" type="button"
                                    value="Pilih" name="btnChoose" disabled runat="server">&nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                                        name="btnCancel"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
