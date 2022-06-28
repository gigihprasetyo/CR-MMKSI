<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpESRUTDownloadLog.aspx.vb" Inherits=".PopUpESRUTDownloadLog" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>ESRUT Download Log</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <base target="_self">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="6" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" style="height: 18px" colspan="7">ESRUT Download Log </td>
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
                            <td class="titleField" style="height: 15px">
                                <asp:Label ID="lblChassisNumberDesc" runat="server" Text="Nomor Rangka"></asp:Label>
                                &nbsp;
                                      <asp:Label ID="lblColon" runat="server" Text=":"></asp:Label>
                                &nbsp;
                                    <asp:Label ID="lblChassisNumber" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="width: 17px; height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                        </tr>

                        <tr>
                            <td class="titleField" style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="width: 17px; height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <div id="div1" style="overflow: auto; height: 350px">
                                    <asp:DataGrid ID="dgList" runat="server" Width="100%" AutoGenerateColumns="False" GridLines="Horizontal"
                                        CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowPaging="True" AllowCustomPaging="True"
                                        AllowSorting="True" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="10%" HorizontalAlign="Center" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="CreatedTime" HeaderText="Tanggal Download">
                                                <HeaderStyle Width="45%" HorizontalAlign="Center" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedTime" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="CreatedBy" HeaderText="User">
                                                <HeaderStyle Width="45%" HorizontalAlign="Center" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedBy" runat="server">
                                                    </asp:Label>
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
                                <input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                                    name="btnCancel"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
