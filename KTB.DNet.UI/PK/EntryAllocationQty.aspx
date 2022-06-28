<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EntryAllocationQty.aspx.vb" Inherits="EntryAllocationQty" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>EntryAllocationQty</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td colspan="3" class="titlePage">PESANAN KENDARAAN - Detil Proses Alokasi</td>
            </tr>
            <tr>
                <td height="1" colspan="3" background="../images/bg_hor.gif">
                    <img src="../images/bg_hor.gif" height="1" border="0"></td>
            </tr>
            <tr>
                <td height="10" colspan="3">
                    <img src="../images/dot.gif" height="1" border="0"></td>
            </tr>
            <tr>
                <td style="height: 59px" colspan="3">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td width="24%" class="titleField">
                                <asp:Label ID="lblKategori" runat="server">Kategori</asp:Label></td>
                            <td width="1%">:</td>
                            <td width="156" style="width: 156px">
                                <asp:Label ID="lblKategoriValue" runat="server"></asp:Label></td>
                            <td width="172" class="titleField" style="width: 172px">
                                <asp:Label ID="lblPeriodeAlokasi" runat="server">Periode Alokasi</asp:Label></td>
                            <td width="1" style="width: 1px">:</td>
                            <td width="29%">
                                <asp:Label ID="lblPeriodeAlokasiValue" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblTipe" runat="server">Tipe</asp:Label></td>
                            <td width="1%">:</td>
                            <td style="width: 156px">
                                <asp:Label ID="lblTipeValue" runat="server"></asp:Label></td>
                            <td class="titleField" style="width: 172px">
                                <asp:Label ID="lblTahunPerakitan" runat="server">Tahun Perakitan</asp:Label></td>
                            <td width="1" style="width: 1px">:</td>
                            <td>
                                <asp:Label ID="lblTahunPerakitanValue" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblMaterialNumber" runat="server">Tipe / Warna</asp:Label></td>
                            <td width="1%">:</td>
                            <td style="width: 156px">
                                <asp:Label ID="lblMaterialNumberValue" runat="server">Label</asp:Label></td>
                            <td class="titleField" style="width: 172px">
                                <asp:Label ID="lblTotalProduksi" runat="server">Sisa Stok</asp:Label></td>
                            <td width="1" style="width: 1px">:</td>
                            <td>
                                <asp:Label ID="lblTotalProduksiValue" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblModelTipeWarna" runat="server">Model / Tipe / Warna</asp:Label></td>
                            <td width="1%">:</td>
                            <td style="width: 156px">
                                <asp:Label ID="lblMaterialDescriptionValue" runat="server"></asp:Label></td>
                            <td class="titleField" style="width: 172px">
                                <asp:Label ID="lblSisaProduksi" runat="server">Sisa Stok Setelah Alokasi</asp:Label></td>
                            <td width="1" style="width: 1px">:</td>
                            <td>
                                <asp:Label ID="lblSisaProduksiValue" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <div id="div1" style="overflow: auto; height: 310px">
                        <asp:DataGrid ID="dtgEntryAllocation" runat="server" Width="100%" AutoGenerateColumns="False"
                            BorderColor="#CDCDCD" BorderStyle="Ridge" CellSpacing="1" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="None" OnItemDataBound="dtgEntryAllocation_ItemDataBound"
                            AllowSorting="True" OnItemCommand="dtgEntryAllocation_ItemCommand">
                            <SelectedItemStyle ForeColor="White" BackColor="#9471DE"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White" VerticalAlign="top"></ItemStyle>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#E7E7FF" VerticalAlign="Middle"
                                BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="Black" BackColor="#C6C3C6"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="id" ReadOnly="True" HeaderText="ID">
                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="PKHeader.PKNumber" HeaderText="No Reg PK">
                                    <HeaderStyle HorizontalAlign="Center" ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" ForeColor="Black"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnNoRegPK" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PKHeader.PKNumber" )  %>' CommandName="View">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn SortExpression="PKHeader.Dealer.DealerCode" HeaderText="Dealer">
                                    <HeaderStyle HorizontalAlign="Center" ForeColor="White" Width="20%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" ForeColor="Black"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn SortExpression="PKHeader.Dealer.DealerName" HeaderText="Nama Dealer">
                                    <HeaderStyle ForeColor="White" Width="12%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle ForeColor="Black"></ItemStyle>
                                </asp:BoundColumn>

                                <asp:TemplateColumn SortExpression="DealerBranch.DealerBranchCode" HeaderText="Cabang Dealer">
                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerBranchCode" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>


                                <asp:BoundColumn SortExpression="PKHeader.Dealer.DealerName" HeaderText="Nama Pesanan Khusus">
                                    <HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:BoundColumn>

                                <asp:TemplateColumn HeaderText="Free Days">
                                    <HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrentFreeDays" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Sisa Program (unit)">
                                    <HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSisaProgram" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:BoundColumn DataField="TargetQty" HeaderText="Pesanan (Unit)">
                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Alokasi (Unit)">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox onkeypress="return numericOnlyUniv(event)" ID="TextBox1" runat="server" Width="60px" Text='<%# DataBinder.Eval(Container.DataItem, "ResponseQty" )  %>' CssClass="textRight">
                                        </asp:TextBox>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Input Tidak Valid" ControlToValidate="TextBox1"
                                            MaximumValue="1000000" MinimumValue="0" Type="Integer">*</asp:RangeValidator>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn Visible="False" HeaderText="PKType"></asp:BoundColumn>

                                <asp:TemplateColumn HeaderText="Pesan/Keterangan">
                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPesan" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn Visible="false" HeaderText="AlokasiAwal">
                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAlokasiAwal" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="false" HeaderText="VehicleModel">
                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVehicleModel" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="#C6C3C6"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height: 30px" colspan="3">
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"></asp:ValidationSummary>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan"></asp:Button>
                    <asp:Button ID="btnHitung" runat="server" Text="Hitung" Enabled="False" Visible="False"></asp:Button>
                    <asp:Button ID="btnKembali" runat="server" Text="Kembali" CausesValidation="False"></asp:Button></td>
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
