<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpSalesmanSubOrdinate.aspx.vb" Inherits=".PopUpSalesmanSubOrdinate" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmAplikasiHeader</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <base target="_self">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">Daftar Sub Ordinate</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
        </table>
        <table id="Table2" cellspacing="0" cellpadding="0" width="40%" border="0">
            <tr>
                <td class="titleField">Kode Salesman</td>
                <td >:</td>
                <td >
                    <asp:Label ID="lblSalesmanCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleField">Nama Salesman</td>
                <td >:</td>
                <td >
                    <asp:Label ID="lblNama" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleField"></td>
                <td ></td>
                <td >&nbsp;</td>
            </tr>

        </table>
        <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td valign="top" colspan="6">
                    <div id="div1" style="overflow: auto; height: 330px">
                        <asp:DataGrid ID="dgSalesman" runat="server" CellPadding="3" BorderWidth="0px" CellSpacing="1"
                            BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True" PageSize="25" AllowPaging="True"
                            Width="100%">
                            <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="Kode Salesman">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:TextBox Width="95%" runat="server" ID="txtSalesmanCode" ReadOnly="true"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanCode") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nama Salesman">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblName" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Posisi">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblPosisi" Text='<%# DataBinder.Eval(Container, "DataItem.JobPosition.Description") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Atasan Pengganti">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <%--<asp:Label runat="server" ID="lblAtasan"></asp:Label>--%>
                                        <asp:DropDownList runat="server" ID="ddlAtasan"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="7">
                    <input id="btnProses" style="width: 60px; height: 22px;" onserverclick="btnProses_ServerClick" runat="server" type="button" value="Proses"
                        name="btnProses">
                    &nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                        name="btnCancel">
                </td>
            </tr>
        </table>
        <input id="Hidden1" type="hidden" name="Hidden1" runat="server">
    </form>
</body>
</html>

