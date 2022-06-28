<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDPFleetCustomerMapping.aspx.vb" Inherits=".FrmDPFleetCustomerMapping" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Daftar Pembayaran Transfer</title>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>

    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet" />
    <script language="javascript">
        function ShowPPCustomerSelection() {
            showPopUp('../PopUp/PopUpCustomerSelection.aspx', '', 500, 760, CustomerSelection);
        }

        function CustomerSelection(selectedCustomer) {
            var fleetID = document.getElementById("hdnFleetCustomerID");
            fleetID.value = selectedCustomer;
            var btnclick = document.getElementById("hdnButton");
            btnclick.click();
            __doPostBack('hdnButton', '');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage" style="height: 16px">&nbsp;
						DISCOUNT PROPOSAL - Fleet Customer Detail</td>
                </tr>
                <tr style="height: 1px;">
                    <td style="height: 1px; background-image: url('../images/bg_hor.gif'); background-size: auto;"></td>
                </tr>
                <tr>
                    <td>
                        <table cellspacing="8" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td width="10px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                                <td width="10px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                            </tr>
                            <tr>
                                <td><strong>Kode Fleet</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:Label runat="server" ID="lblKodeFleet"></asp:Label></td>
                                <td><strong>Fleet Group</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:Label runat="server" ID="lblFleetGroup" ></asp:Label>
                                    </td>
                            </tr>
                            <tr>
                                <td><strong>Tipe</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:Label runat="server" ID="lblTipe" ></asp:Label>
                                    </td>
                                <td><strong>Jenis Usaha</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:Label runat="server" ID="lblJenisUsaha" ></asp:Label>
                                    </td>
                            </tr>
                            <tr>
                                <td><strong>Ketegori</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:Label runat="server" ID="lblKategori" ></asp:Label>
                                    </td>
                                <td><strong>Tanggal Pengajuan Fleet</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:Label runat="server" ID="lblTglPengajualFleet"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><strong>Nama</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:Label runat="server" ID="lblNama" ></asp:Label>
                                    </td>
                                <td width="10px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                            </tr>
                            <tr>
                                <td width="10px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                                <td width="10px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                            </tr>
                            <tr>
                                <td width="10px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                                <td width="10px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                            </tr>
                            <tr style="display:none">
                                <td style="color: red"><strong>Customer</strong></td>
                                <td></td>
                                <td></td>
                                <td width="10px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                            </tr>
                            <tr style="display:none">
                                <td><strong>Nama Customer</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtNamaCustomer" onblur="omitSomeCharacter('txtNamaCustomer','<>?*%$;')"
                                        runat="server" MaxLength="100"></asp:TextBox>
                                </td>
                                <td><strong>Kode Customer</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeCustomer" onblur="omitSomeCharacter('txtKodeCustomer','<>?*%$;')"
                                        runat="server" MaxLength="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="hdnButton" runat="server" Text="Cari" Width="100px" style="display:none"/>
                                    <asp:Button ID="btnCari" runat="server" Text="Cari" Width="100px" Visible="false" />
                                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan" Width="100px" />
                                    &nbsp;
                                    <asp:Button ID="btnBack" runat="server" Text="Kembali" Width="100px" />
                                </td>
                                <td width="10px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                            </tr>
                            <tr>
                                <td width="10px">
                                        <asp:HiddenField runat="server" ID="hdnFleetCustomerID" />
                                </td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                                <td width="10px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                            </tr>
                            <tr>
                                <td><strong>Tambahkan Customer : </strong><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"  onclick="ShowPPCustomerSelection();" ></asp:label></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                                <td width="10px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
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
                                <asp:TemplateColumn HeaderText="Kode Customer">
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridKodeCustomer" Text='<%# DataBinder.Eval(Container, "DataItem.Customer.Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nama">
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridNama" Text='<%# DataBinder.Eval(Container, "DataItem.Customer.Name1")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Alamat">
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridAlamat" Text='<%# DataBinder.Eval(Container, "DataItem.Customer.Alamat")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kota">
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridKota" Text='<%# DataBinder.Eval(Container, "DataItem.Customer.City.CityName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Provinsi">
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridProvinsi" Text='<%# DataBinder.Eval(Container, "DataItem.Customer.City.Province.ProvinceName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No Identitas">
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridNoIdentitas" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnDelete" runat="server" CommandName="Delete" CausesValidation="False">
												            <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
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
