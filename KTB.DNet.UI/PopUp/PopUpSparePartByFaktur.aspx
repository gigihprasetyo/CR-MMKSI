<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpSparePartByFaktur.aspx.vb" Inherits="PopUpSparePartByFaktur" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>PopUpSparePart</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <base target="_self">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">

        function GetSelectedPart() {
            var Hidden1 = document.getElementById("Hidden1");
            var table;
            table = document.getElementById("dtgSparePart");
            var find = false;
            for (i = 1; i < table.rows.length; i++) {
                var radioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (radioButton != null && radioButton.checked) {

                    var partNumber = "";
                    var partName = "";                  
                    var retailPrice = "";
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        partNumber = replace(table.rows[i].cells[1].innerText, ' ', '');
                        partName = replace(table.rows[i].cells[2].innerText, ' ', '');
                        retailPrice = replace(table.rows[i].cells[3].innerText, '.', '');
                        partName = replace(partName, ';', ',');
                        window.returnValue = partNumber + ";" + partName + ";" + retailPrice + ";" + Hidden1.value;
                    }
                    else if (navigator.appName == "Netscape") {
                        partNumber = replace(table.rows[i].cells[1].innerText, ' ', '');
                        partName = replace(table.rows[i].cells[2].innerText, ' ', '');
                        retailPrice = replace(table.rows[i].cells[3].innerText, '.', '');
                        partName = replace(partName, ';', ',');
                        opener.dialogWin.returnFunc(partNumber + ";" + partName + ";" + retailPrice + ";" + Hidden1.value);
                    }
                    else {
                        partNumber = table.rows[i].cells[1].getElementsByTagName("SPAN")[0].innerHTML;
                        partName = table.rows[i].cells[2].getElementsByTagName("SPAN")[0].innerHTML;
                        retailPrice = replace(table.rows[i].cells[3].getElementsByTagName("SPAN")[0].innerHTML, '.', '');
                        partName = replace(partName, ';', ',');
                        opener.dialogWin.returnFunc(partNumber + ";" + partName + ";" + retailPrice + ";" + Hidden1.value);
                    }
                    find = true;
                    break;
                }
            }
            if (find)
                window.close();
            else
                alert("Silahkan pilih barang");
        }
    </script>
</head>
<body bottommargin="10" topmargin="10">
    <form id="Form2" method="post" runat="server">
        <table id="Table1" cellspacing="10" cellpadding="0" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" height="20">&nbsp;&nbsp;SPAREPART - Daftar Barang</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor_parts.gif" height="1">
                                <img height="1" src="../images/bg_hor_parts.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:compositefilter id="cfSparePart" runat="server" DataGridSouce="dtgSparePart"></cc1:compositefilter></td>
                        </tr>
                        <tr>
                            <td>
                                <div id="div1" style="overflow: auto; height: 310px">
                                    <asp:DataGrid ID="dtgSparePart" runat="server" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"
                                        BorderColor="Gainsboro" BackColor="Gainsboro" CellPadding="3" CellSpacing="1" BorderWidth="0px" Width="100%" AllowCustomPaging="True" AllowPaging="True">
                                        <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                                        <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableParts"></HeaderStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="SparePartMaster.PartNumber" HeaderText="Nomor Barang">
                                                <HeaderStyle ForeColor="White" Width="30%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="SparePartMaster.PartName" HeaderText="Nama Barang">
                                                <HeaderStyle ForeColor="White" Width="40%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Harga Eceran (Rp)">
                                                <HeaderStyle ForeColor="White" Width="25%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRetailPrice" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.RetalPrice","{0:#,##0}") %>'>
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
                                <input id="btnChoose" style="width: 60px" onclick="GetSelectedPart()" type="button" value="Pilih"
                                    name="btnChoose">&nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                                        name="btnCancel"></td>
                        </tr>
                    </table>
                    <input id="Hidden1" type="hidden" name="Hidden1" runat="server">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
