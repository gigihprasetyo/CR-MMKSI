<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpFreeServicePartDetail.aspx.vb" Inherits=".PopUpFreeServicePartDetail" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>PopUp Free Service Part Detail</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <base target="_self">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" language="javascript">

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table border="0" width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="titlePage" style="height: 21px">Free Service Part Detail</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0" alt="">
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div id="div1" style="overflow: auto; height: 310px">
                        <asp:DataGrid ID="dtgFreeServiceDetail" runat="server" Width="100%" CellPadding="3" BackColor="#CDCDCD"
                            BorderWidth="0px" BorderColor="Gainsboro"
                            AllowSorting="True" AllowCustomPaging="True" AllowPaging="True" PageSize="25"
                            AutoGenerateColumns="False" CellSpacing="1">
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn SortExpression="SparePartMaster.PartNumber" HeaderText="Part Number">
                                    <HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="SparePartMaster.PartName" HeaderText="Part Name">
                                    <HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <%--<asp:BoundColumn DataField="PartNo" HeaderText="Part Number">
                                    <HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PartName" SortExpression="PartName" HeaderText="Part Name">
                                    <HeaderStyle Width="35%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:BoundColumn>--%>
                                <asp:BoundColumn DataField="Quantity" SortExpression="Quantity" HeaderText="Quantity">
                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="SparePartMaster.PartPrice" HeaderText="Part Price">
                                    <HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartPrice" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.PartPrice"), "#,##0")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <%--<asp:BoundColumn DataField="PartPrice" SortExpression="PartPrice" HeaderText="Part Price">
                                    <HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:BoundColumn>--%>
                            </Columns>
                            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="7">
                    <input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                        name="btnCancel">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
