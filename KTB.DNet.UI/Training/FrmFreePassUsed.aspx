<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmFreePassUsed.aspx.vb" Inherits=".FrmFreePassUsed" %>

<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmCourse</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 12px;
        }

        .auto-style2 {
            width: 17px;
            height: 12px;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="4" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" colspan="7">Training -&nbsp;Free Pass Detail &nbsp;</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor_sales.gif" colspan="7" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>

                        <tr valign="top">
                            <td class="auto-style1" width="20%">Kode Dealer</td>
                            <td width="1%" class="auto-style1">:</td>
                            <td width="25%" class="auto-style1">
                                <asp:Label ID="lblDealerCode"
                                    runat="server"></asp:Label></td>
                            <td width="2%" class="auto-style2"></td>

                        </tr>


                        <tr valign="top">
                            <td class="titleField" width="20%">Tahun Fiskal</td>
                            <td width="1%">:</td>
                            <td width="25%">
                                <asp:Label ID="lblFiscalYear"
                                    runat="server" Width="152px"></asp:Label></td>
                            <td style="width: 17px" width="2%"></td>

                        </tr>


                        <tr valign="top">
                            <td class="titleField" width="20%">Free Pass Terpakai</td>
                            <td width="1%">:</td>
                            <td width="25%">
                                <asp:Label ID="lblQtyUsed"
                                    runat="server" Width="152px"></asp:Label></td>
                            <td style="width: 17px" width="2%"></td>

                        </tr>

                        <tr style="height: 40px">
                            <td></td>
                        </tr>

                        <tr>
                            <td colspan="7">
                                <div id="div1" style="overflow: auto; height: 280px">
                                    <asp:DataGrid ID="dtgFreePass" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                        CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowPaging="True" AllowSorting="True" PageSize="25"
                                        Width="100%" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif" AllowCustomPaging="True">
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>

                                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>

                                        <Columns>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="TrTrainee.ID" HeaderText="ID Trainee">
                                                <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTraineeID" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="TrTrainee.Name" HeaderText="Nama Trainee">
                                                <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTraineeName" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="TrClassRegistration.TrClass.ClassCode" HeaderText="Kelas">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblClass" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="TrClassRegistration.TrClass.PaidDay" HeaderText="Free Pass Terpakai">
                                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQtyUsed" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="TrClassRegistration.TrClass.StartDate" HeaderText="Tgl Training">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTrainingDate" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="TrClassRegistration.TrClass.TrMRTC.Code" HeaderText="Lokasi Training">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAddress" runat="server">
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
                            <td align="center" colspan="7">
                                <asp:Button ID="btnKembali" runat="server" Text="Kembali" />
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
