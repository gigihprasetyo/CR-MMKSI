<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmSPPODetail.aspx.vb" Inherits="FrmSPPODetail" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Pemesanan - Detail Pesanan </title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <%--<base target="_self">--%>
    <script language="javascript">
        var indexRow;
        function BackToPrev() {
            var url = document.getElementById("txtUrlToBack").value;
            window.location = url;
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">PEMESANAN - Detail Pesanan </td>
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
                <td>
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="24%">Kode Dealer</td>
                            <td width="1%">:</td>
                            <td width="20%">
                                <asp:Label ID="lblDealerCode" runat="server"></asp:Label></td>
                            <td width="50%"></td>
                        </tr>
                        <tr>
                            <td class="titleField">Nama Dealer</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:Label ID="lblDealerName" runat="server"></asp:Label>/
									<asp:Label ID="lblDealerTerm" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">Tipe Order</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:DropDownList ID="ddlOrderType" runat="server" Width="140px" Enabled="False"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">Nomor /Tanggal PO</td>
                            <td width="1%">:</td>
                            <td width="20%">
                                <asp:TextBox ID="txtPONumber" runat="server" size="22" ReadOnly="True" Enabled="False">[Dibuat oleh sistem]</asp:TextBox></td>
                            <td width="55%">
                                <asp:Label ID="LblPODate" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">Nilai Pemesanan</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:Label ID="lblTotPOAmount" runat="server">0</asp:Label></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField">Cara Pembayaran</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:Label ID="lblCaraPembayaran" runat="server"></asp:Label></td>
                            <td></td>
                        </tr>
                    </table>
                    <div id="div1" style="overflow: auto; height: 280px">
                        <asp:DataGrid ID="dtgPODetail" runat="server" Width="100%" AutoGenerateColumns="False" ShowFooter="True"
                            CellPadding="3" CellSpacing="1" BackColor="#CDCDCD" BorderColor="Gainsboro" BorderWidth="0px" AllowSorting="True" EnableViewState="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn ReadOnly="True" HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="SparePartMaster.PartNumber" HeaderText="Nomor Barang">
                                    <HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="SparePartMaster.PartName" HeaderText="Nama Barang">
                                    <HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartname" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Quantity" HeaderText="Jumlah">
                                    <HeaderStyle HorizontalAlign="Right" Width="12%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' CssClass="textRight">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="RetailPrice" HeaderText="Harga Eceran">
                                    <HeaderStyle HorizontalAlign="Right" Width="12%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRetailPrice" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RetailPrice","{0:#,##0}") %>' CssClass="textRight">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Amount" HeaderText="Total Harga">
                                    <HeaderStyle HorizontalAlign="Right" Width="12%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Amount","{0:#,##0}") %>' CssClass="textRight">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnDownload" runat="server" Text="Download" />
                    <INPUT id="btnClose" onclick="window.close()" type="button" value="Tutup"></TD>
                    <input style="display: none" onclick="BackToPrev();" type="button" value="Kembali">
                    <asp:TextBox ID="txtUrlToBack" Style="visibility: hidden" ReadOnly="True" Text="" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>
        &nbsp;&nbsp;
    </form>
</body>
</html>
