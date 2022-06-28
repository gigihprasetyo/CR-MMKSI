<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpCcCalculationHistory.aspx.vb" Inherits=".PopUpCcCalculationHistory" %>

<!DOCTYPE html>

<html>
<head>

    <title>Recalculate History</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <base target="_self">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Recalculate History</td>
            </tr>
            <tr>
                <td height="1" background="../images/bg_hor.gif">
                    <img src="../images/bg_hor.gif" height="1" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img src="../images/dot.gif" height="1" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="width:100px">Periode</td>
                            <td>:</td>
                            <td><asp:DropDownList ID="ddlPeriode" runat="server"></asp:DropDownList></td>
                            <td><asp:Button ID="btnSearch" runat="server" Text="Cari" Width="50px" /></td>
                        </tr>
                       
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div1" style="overflow: auto; height: 390px">
                        <asp:DataGrid ID="dtgHistory" runat="server" Width="100%" GridLines="None" CellPadding="3" BackColor="#E0E0E0"
                            BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AutoGenerateColumns="False" CellSpacing="1"
                            AllowSorting="True" AllowPaging="True" AllowCustomPaging="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                 <asp:TemplateColumn HeaderStyle-CssClass="titleTableGeneral" HeaderText="No" HeaderStyle-Width="3%">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderStyle-CssClass="titleTableGeneral" SortExpression="CcCSPerformanceMaster.Description" HeaderText="Formula" HeaderStyle-Width="25%">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblFormula" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderStyle-CssClass="titleTableGeneral" SortExpression="CcPeriod.YearMonth" HeaderText="Periode" HeaderStyle-Width="25%">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPeriod" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderStyle-CssClass="titleTableGeneral" SortExpression="CcCSPerformanceCluster.ClusterName" HeaderText="Cluster" HeaderStyle-Width="5%">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCluster" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderStyle-CssClass="titleTableGeneral" SortExpression="RequestedDate" HeaderText="Tanggal Request" HeaderStyle-Width="25%">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequest" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderStyle-CssClass="titleTableGeneral" SortExpression="ProcessedDate" HeaderText="Tanggal Proses" HeaderStyle-Width="25%">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblProcess" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>

        </table>
    </form>
</body>
</html>
