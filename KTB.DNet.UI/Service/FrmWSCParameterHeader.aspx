<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmWSCParameterHeader.aspx.vb" Inherits=".FrmWSCParameterHeader" %>


<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmWSCParameterHeader</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Waranty Service Claim - Input Parameter WSC</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td align="left">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="14%">Tipe Claim</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:DropDownList ID="ddlClaimType" runat="server" Width="136px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" style="height: 15px" width="24%" valign="top">Kategori / 
									Tipe Kendaraan</td>
                            <td style="height: 15px" width="1%" valign="top">:</td>
                            <td style="height: 15px" width="75%" valign="top">
                                <asp:ListBox ID="lboxCategory" runat="server" Width="183px" Rows="5" SelectionMode="Multiple"></asp:ListBox>
                                <asp:Button ID="btnSerchCode" runat="server" Width="40px" Text="Cari" Style="margin-left: 5px; margin-bottom: 5px"></asp:Button>
                                <asp:Label ID="lblCategory" runat="server" style="font-weight:bold;display:none" valign="top"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%" valign="top"></td>
                            <td width="1%"></td>
                            <td width="75%" valign="top">
                                <asp:ListBox ID="lboxVehicleType" runat="server" Width="183px" Rows="5" SelectionMode="Multiple"></asp:ListBox>
                                <asp:Label ID="lblVehicleType" runat="server" style="font-weight:bold;display:none" onchange="alert('test');setFocus(this.value);"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="14%">Deskripsi</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox ID="txtDescription" Width="250px" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="14%">Status</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="136px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField"></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Simpan" CausesValidation="False"></asp:Button>
                                <asp:Button ID="btnBatal" runat="server" Width="60px" Text="Batal"></asp:Button>
                                <asp:Button ID="btnCari" runat="server" Width="60px" Text="Cari" CausesValidation="False" Visible="True"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 350px">
                        <asp:DataGrid ID="dtgWscParam" runat="server" Width="100%" BorderStyle="None" AllowPaging="True"
                            PageSize="50" AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" BackColor="#CDCDCD"
                            CellPadding="3" GridLines="Horizontal" ForeColor="Gray" CellSpacing="1">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="" HeaderText="Deskripsi">
                                    <HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>' ID="Label5"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="" HeaderText="Tipe Claim">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipeClaim" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClaimType")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="" HeaderText="Tipe Kendaraan">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVehicleType" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Aksi">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDetail" runat="server" Text="Ubah" CommandName="View" CausesValidation="False"
                                            ToolTip="Detail">
												<img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnEdit" CausesValidation="False" runat="server" Text="Ubah" CommandName="Edit"
                                            ToolTip="Ubah">
												<img border="0" src="../images/edit.gif" alt="Ubah" style="cursor:hand"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnActive" CausesValidation="False" runat="server" Text="Aktif" CommandName="Active"
                                            ToolTip="Aktivkan">
												<img border="0" src="../images/aktif.gif" alt="Aktifkan" style="cursor:hand"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnInactive" CausesValidation="False" runat="server" Text="Non-aktif" CommandName="Inactive"
                                            ToolTip="Non Aktivkan">
												<img border="0" src="../images/in-aktif.gif" alt="Non-aktifkan" style="cursor:hand"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDelete" CausesValidation="False" runat="server" Text="Hapus" CommandName="Delete"
                                            ToolTip="Hapus">
												<img border="0" src="../images/trash.gif" alt="Hapus" style="cursor:hand"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnParam" CausesValidation="False" runat="server" Text="Detail" CommandName="Detail"
                                            ToolTip="Detail">
													<img src="../images/set.gif" border="0" alt="Parameter Detail"></asp:LinkButton>
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
    <script language="javascript">
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
