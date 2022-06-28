<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMDPMasterVehicle.aspx.vb" Inherits=".FrmMDPMasterVehicle" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
    <title>MDPVehicleMaster</title>
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
                    <td class="titlePage">Monthly Delivery Planning - MDP Master Kendaraan</td>
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
                    <td valign="top" align="left">
                        <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tr>
                                <td class="titleField" width="20%"></td>
                                <td width="1%"></td>
                                <td width="79%"></td>
                            </tr>
                            <tr>
                                <td class="titleField">Status</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField">Kategori</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    <asp:DropDownList Style="z-index: 0" ID="ddlSubCategory" runat="server" AutoPostBack="True"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField">Tipe</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlType" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <div id="div1" style="overflow: auto; height: 320px">
                            <asp:DataGrid ID="dtgMasterVehicle" runat="server" Width="100%" PageSize="25" Height="1px" AutoGenerateColumns="False"
                                BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Both" CellSpacing="1"
                                BackColor="#CDCDCD" AllowSorting="True">
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
                                            <asp:Label ID="lblNo" runat="server" BorderColor="Transparent"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                        <HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn SortExpression="VehicleColor.VechileType.VechileTypeCode" HeaderText="Kode Tipe">
                                        <HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblKodeTipe" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VehicleColor.VechileType.VechileTypeCode")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="VehicleColor.ColorCode" HeaderText="Kode Warna">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblKodeWarna" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VehicleColor.ColorCode")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="VehicleColor.MaterialDescription" HeaderText="Nama Kendaraan">
                                        <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNamaKendaraan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VehicleColor.MaterialDescription")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Status">
                                        <HeaderStyle Width="9%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Tgl. Update">
                                        <HeaderStyle ForeColor="White" Width="10%" BackColor="#C22C32"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTglUpdate" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnPopUp" runat="server" CommandName="Detail">
												<img src="../images/detail.gif" border="0" alt="Lihat Detail">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#CDCDCD" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td height="40" align="left"></td>
                </tr>
            </table>
        </div>
    </form>

    <script type="text/javascript">
        if (window.parent == window) {
            if (!navigator.appName == "Microsoft Internet Explorer") {
                self.opener = null;
                self.close();
            }
            else {
                this.name = "origWin";
                origWin = window.open(window.location, "origWin");
                window.opener = top;
                window.close();
            }
        }
    </script>
</body>
</html>
