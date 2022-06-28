<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmFlatRate.aspx.vb" Inherits="FrmFlatRate" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Master Flatrate</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script type="text/javascript" language="javascript">
        
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td>
                    <asp:TextBox ID="temp" Visible="False" runat="server"></asp:TextBox>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" style="height: 17px">Stall Management&nbsp;- Master Flat Rate</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor.gif" colspan="3" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
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
                    <table id="Table3" cellspacing="1" cellpadding="2" width="100%">
                        <tr>
                            <td class="titleField" style="width: 24%">Jenis Kendaraan</td>
                            <td style="width: 1%">:</td>
                            <td style="width: 75%">
                                <asp:TextBox ID="txtVariant" runat="server" Width="160px"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td class="titleField">Tipe</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlTipe" Width="120">
                                    <asp:ListItem Value="0">Silahkan Pilih</asp:ListItem>
                                    <asp:ListItem Value="Free Service">Free Service</asp:ListItem>
                                    <asp:ListItem Value="Periodical Maintenance">Periodical Maintenance</asp:ListItem>
                                    <asp:ListItem Value="Field Fix Campaign">Field Fix Campaign</asp:ListItem>
                                    <asp:ListItem Value="General Repair">Lain - Lain</asp:ListItem>

                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td class="titleField" style="width: 24%">Kode</td>
                            <td style="width: 1%">:</td>
                            <td style="width: 75%">
                                <asp:TextBox ID="txtKode" runat="server" Width="160px"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td class="titleField" style="width: 24%"></td>
                            <td style="width: 1%"></td>
                            <td style="width: 75%">
                                <%--<asp:Button ID="btnSave" runat="server" Width="88px" Text="Simpan" OnClick="btnSave_Click"></asp:Button>&nbsp;--%>
                                <asp:Button ID="btnCari" runat="server" Width="88px" Text="Cari" OnClick="btnCari_Click"></asp:Button>
                                <asp:Button ID="btnDownload" runat="server" Width="88px" Text="Download" OnClick="btnDownload_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td>

                    <asp:DataGrid ID="dtgFlatRate" runat="server" Width="100%" BackColor="#E0E0E0" AutoGenerateColumns="False"
                        BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="false" AllowSorting="True" AllowCustomPaging="True" PageSize="25"
                        AllowPaging="True">
                        <SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
                        <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle VerticalAlign="Top"></HeaderStyle>
                        <FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbNo" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Jenis Kendaraan">
                                <HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbVariant" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Varian")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Tipe">
                                <HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbTipe" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Type")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Code">
                                <HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.KindCode")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Flat Rate(Jam)">
                                <HeaderStyle Width="35%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbFlatRate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FlatRate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                        </Columns>
                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>

                </td>
            </tr>

        </table>
    </form>
</body>
</html>
