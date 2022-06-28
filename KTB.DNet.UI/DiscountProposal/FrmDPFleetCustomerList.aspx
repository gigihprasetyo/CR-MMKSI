<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDPFleetCustomerList.aspx.vb" Inherits=".FrmDPFleetCustomerList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
    <title>Daftar Fleet Customer</title>
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
    <form id="Form1" runat="server">
        <div>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">DISCOUNT PROPOSAL - Daftar Fleet Customer</td>
				</tr>
                <tr>
                    <td background="../images/bg_hor.gif" height="1" colspan="2">
                        <img height="1" src="../images/bg_hor.gif" border="0"></td>
                </tr>
                <tr>
                    <td height="10" colspan="2">
                        <img height="1" src="../images/dot.gif" border="0"></td>
                </tr>
                <tr>
                    <td>
                        <table cellspacing="8" cellpadding="0" width="50%" border="0">
                            <tr>
                                <td><strong>Tipe</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlTipe" Width="30%"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td><strong>Kode</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:TextBox onblur="omitSomeCharacter('txtKode','<>?*%$')" ID="txtKode" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        runat="server" Width="50%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><strong>Nama</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:TextBox onblur="omitSomeCharacter('txtNama','<>?*%$')" ID="txtNama" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        runat="server" Width="50%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnCari" runat="server" Text="Cari" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DataGrid ID="dtgMain" runat="server"
                            Width="100%" CellSpacing="1" GridLines="Horizontal"
                            CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0"
                            AutoGenerateColumns="False" AllowCustomPaging="True" PageSize="25" AllowPaging="True" AllowSorting="true"
                            DataKeyField="ID" ShowFooter="false">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                            <ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle ForeColor="White" Width="2%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnID" Value='<%# DataBinder.Eval(Container, "DataItem.ID")%>' />
                                        <asp:Label runat="server" ID="lblNo" Text='<%# Container.ItemIndex + 1 + (dtgMain.CurrentPageIndex * dtgMain.PageSize)%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode">
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridKode" Text='<%# DataBinder.Eval(Container, "DataItem.FleetCode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tipe">
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridTipe" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kategori">
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridKategori"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nama">
                                    <HeaderStyle ForeColor="White" Width="16%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridNama" Text='<%# DataBinder.Eval(Container, "DataItem.FleetCustomerName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Group">
                                    <HeaderStyle ForeColor="White" Width="16%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridGroup" Text='<%# DataBinder.Eval(Container, "DataItem.FleetCustomerGroupCompany")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jenis Usaha">
                                    <HeaderStyle ForeColor="White" Width="16%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridJenisUsaha" Text='<%# DataBinder.Eval(Container, "DataItem.BusinessSectorDetail.BusinessDomain")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnDetail" runat="server" CommandName="Detail" CausesValidation="False">
												            <img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="Edit" CausesValidation="False">
												            <img src="../images/edit.gif"  border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnDelete" runat="server" CommandName="Delete" CausesValidation="False">
												            <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnMapping" runat="server" CommandName="Mapping" CausesValidation="False">
												            <img src="../images/assigntocustomer.png" border="0" alt="Mapping"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
