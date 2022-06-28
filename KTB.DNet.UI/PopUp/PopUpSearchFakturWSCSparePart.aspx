<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpSearchFakturWSCSparePart.aspx.vb" Inherits=".PopUpSearchFakturWSCSparePart" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>PopUpPositionCode</title>
    <style>
        .HiddenColumn {
            DISPLAY: none;
            FONT-WEIGHT: bold;
            FONT-SIZE: 11px;
            BACKGROUND: #666666;
            MARGIN: 0px;
            COLOR: #ffffff;
            FONT-FAMILY: Sans-Serif, Arial;
            TEXT-ALIGN: center;
        }
    </style>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <base target="_self">
    <script language="javascript">

        function getSelectedCourse() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgParts");
            for (i = 1; i < table.rows.length; i++) {
                var RadioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (RadioButton != null && RadioButton.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        var Course = table.rows[i].cells[1].innerText + ";" + table.rows[i].cells[2].innerText
                                        + ";" + table.rows[i].cells[3].innerText + ";" + table.rows[i].cells[4].innerText;
                        window.returnValue = Course;
                        bcheck = true;
                        break;
                    }
                    else if (navigator.appName == "Netscape") {
                        var Course = table.rows[i].cells[1].innerText + ";" + table.rows[i].cells[2].innerText
                                        + ";" + table.rows[i].cells[3].innerText + ";" + table.rows[i].cells[4].innerText;
                        window.opener.dialogWin.returnFunc(Course);
                        bcheck = true;
                        break;
                    }

                    else {
                        var Course = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML + ";" + table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML;
                        window.opener.dialogWin.returnFunc(Course);
                        bcheck = true;
                        break;
                    }
                }
            }
            if (bcheck) {
                window.close();
            }
            else {
                alert("Silahkan pilih Kode Parts");
            }
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
                            <td class="titlePage" style="height: 18px" colspan="7">SERVICE&nbsp;-&nbsp;Daftar Faktur Number</td>
                        </tr>
                        <tr>
                            <td style="height: 1px" background="../images/bg_hor_sales.gif" colspan="7" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <div id="div1" style="overflow: auto; height: 350px">
                                    <asp:DataGrid ID="dtgParts" runat="server" Width="100%" AutoGenerateColumns="False" GridLines="Horizontal"
                                        CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD"
                                        AllowPaging="True" AllowCustomPaging="True" AllowSorting="True" PageSize="250" ForeColor="GhostWhite"
                                        CellSpacing="1" Font-Names="MS Reference Sans Serif">
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <input type="radio" name="radio">
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Kode Parts">
                                                <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartNumber") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Deskripsi">
                                                <HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartName") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nomor Faktur">
                                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BillingNumber")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tanggal Faktur">
                                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%#Format(DataBinder.Eval(Container, "DataItem.BillingDate"), "dd/MM/yyyy")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Status">
                                                <HeaderStyle Width="10%" CssClass="HiddenColumn"></HeaderStyle>
                                                <ItemStyle CssClass="HiddenColumn" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7" align="center">
                                <input id="btnChoose" style="width: 60px" onclick="getSelectedCourse()" type="button"
                                    value="Pilih" name="btnChoose" runat="server">&nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()"
                                        type="button" value="Tutup" name="btnCancel">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
