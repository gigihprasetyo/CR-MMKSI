<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpMDPMasterVehicleHistory.aspx.vb" Inherits=".PopUpMDPMasterVehicleHistory" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
    <title>Daftar Perubahan Status Kendaraan MDP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" src="../WebResources/InputValidation.js"></script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" method="post" runat="server">
        <div runat="server" id="divPage">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage">Maintenance - Daftar Perubahan Status</td>
                </tr>
                <tr>
                    <td background="../images/bg_hor.gif" height="1">
                        <img alt="" height="1" src="../images/bg_hor.gif" border="0">
                    </td>
                </tr>
                <tr>
                    <td height="10">
                        <img height="1" alt="" src="../images/dot.gif" border="0"></td>
                </tr>
                <tr>
                    <td>
                        <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tr>
                                <td>Nama Kendaraan</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblNamaKendaraan" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <div id="div1" style="overflow: auto; height: 375px">
                            <asp:DataGrid ID="dtgStatusChange" runat="server" Width="100%" PageSize="25" Height="1px" AutoGenerateColumns="False"
                                BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Both" CellSpacing="1" BackColor="#CDCDCD" AllowSorting="True">
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                                <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                <Columns>
                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle Width="2%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoHistory" runat="server" BorderColor="Transparent"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                        <HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="Status Lama">
                                        <HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatusLama" runat="server" Text="">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Status Baru">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatusBaru" runat="server" Text="">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Diproses Tanggal">
                                        <HeaderStyle ForeColor="White" Width="10%" BackColor="#C22C32"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTglProses" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#CDCDCD" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>

</body>
</html>
