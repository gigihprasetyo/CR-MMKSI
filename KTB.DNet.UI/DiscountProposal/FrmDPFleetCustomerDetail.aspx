<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDPFleetCustomerDetail.aspx.vb" Inherits=".FrmDPFleetCustomerDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Daftar Pembayaran Transfer</title>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>

    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet" />
    <script>
        function ShowPPDealerSelection() {
            showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer.split(';');
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = tempParam[0];
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
                                <td><asp:Label runat="server" ID="lblKodeFleet"></asp:Label></td>
                                <td><strong>Fleet Group</strong></td>
                                <td><strong>:</strong></td>
                                <td><asp:Label runat="server" ID="lblFleetGroup" Visible="false"></asp:Label>
                                    <asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtFleetGroup" 
                                        onblur="omitSomeCharacter('txtFleetGroup','<>?*%$;')" runat="server" Visible="false"></asp:textbox></td>
                            </tr>
                            <tr>
                                <td><strong>Tipe</strong></td>
                                <td><strong>:</strong></td>
                                <td><asp:Label runat="server" ID="lblTipe" Visible="false"></asp:Label>
                                    <asp:DropDownList ID="ddlTipe" runat="server" Width="140px" Visible="false"></asp:DropDownList></td>
                                <td><strong>Jenis Usaha</strong></td>
                                <td><strong>:</strong></td>
                                <td><asp:Label runat="server" ID="lblJenisUsaha" Visible="false"></asp:Label>
                                    <asp:DropDownList ID="ddlJenisUsaha" runat="server" Width="140px" Visible="false"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td><strong>Ketegori</strong></td>
                                <td><strong>:</strong></td>
                                <td><asp:Label runat="server" ID="lblKategori" Visible="false"></asp:Label>
                                    <asp:DropDownList ID="ddlKategori" runat="server" Width="100px" Visible="false"></asp:DropDownList></td>
                                <td><strong>Tanggal Pengajuan Fleet</strong></td>
                                <td><strong>:</strong></td>
                                <td><asp:Label runat="server" ID="lblTglPengajualFleet"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><strong>Nama</strong></td>
                                <td><strong>:</strong></td>
                                <td><asp:Label runat="server" ID="lblNama" Visible="false"></asp:Label>
                                    <asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtNama" 
                                        onblur="omitSomeCharacter('txtNama','<>?*%$;')" runat="server" Visible="false" Width="200px"></asp:textbox></td>
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
                            <tr id="trFilter1" runat="server">
                                <td width="10px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                                <td width="10px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                            </tr>
                            <tr id="trFilter2" runat="server">
                                <td style="color: red"><strong>Dealer</strong></td>
                                <td></td>
                                <td></td>
                                <td width="10px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                            </tr>
                            <tr id="trFilter3" runat="server">
                                <td><strong>Kode Dealer</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')"
										runat="server" MaxLength="10"></asp:textbox><asp:label id="lblKodeDealer" runat="server"></asp:label><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
                                </td>
                                <td width="10px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                            </tr>
                            <tr id="trFilter4" runat="server">
                                <td><strong>Provinsi</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:DropDownList ID="ddlProvinsi" runat="server" Width="140px"></asp:DropDownList>
                                </td>
                                <td width="10px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnCari" runat="server" Text="Cari" Width="100px" Visible="false"/>
                                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan" Width="100px" Visible="false"/> &nbsp;
                                    <asp:Button ID="btnBack" runat="server" Text="Kembali" Width="100px" />
                                </td>
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
                                    <EditItemTemplate>
                                        <asp:Label runat="server" ID="lblNoEdit" Text='<%# Container.ItemIndex + 1 + (dtgMain.CurrentPageIndex * dtgMain.PageSize)%>'></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Detail">
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridFleetDetailCode" Text='<%# DataBinder.Eval(Container, "DataItem.FleetDetailCode")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label runat="server" ID="lblGridFleetDetailCodeEdit" Text='<%# DataBinder.Eval(Container, "DataItem.FleetDetailCode")%>'></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Dealer">
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridKodeDealer" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label runat="server" ID="lblGridKodeDealerEdit" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>'></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Term1">
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridTerm1" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label runat="server" ID="lblGridTerm1Edit" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1")%>'></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Alamat">
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridAlamat" Text='<%# DataBinder.Eval(Container, "DataItem.Address")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtGridAlamat" TextMode="MultiLine"
                                            onblur="omitSomeCharacter('txtGridAlamat','<>?*%$;')" runat="server" MaxLength="250" Text='<%# DataBinder.Eval(Container, "DataItem.Address")%>'></asp:textbox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kota">
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridKota" Text='<%# DataBinder.Eval(Container, "DataItem.City.CityName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlGridKota" runat="server" Width="140px"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Provinsi">
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridProvinsi" Text='<%# DataBinder.Eval(Container, "DataItem.City.Province.ProvinceName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label runat="server" ID="lblGridProvinsiEdit" Text='<%# DataBinder.Eval(Container, "DataItem.City.Province.ProvinceName")%>'></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tipe Identitas">
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridTipeIdentitas" ></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlGridTipeIdentitas" runat="server" Width="140px"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No Identitas">
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridNoIdentitas" Text='<%# DataBinder.Eval(Container, "DataItem.IdentityNumber")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtGridNoIdentitas" TextMode="MultiLine"
                                            onblur="omitSomeCharacter('txtGridNoIdentitas','<>?*%$;')" runat="server" MaxLength="20"  Text='<%# DataBinder.Eval(Container, "DataItem.IdentityNumber")%>'></asp:textbox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No NPWP">
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridNoNPWP" Text='<%# DataBinder.Eval(Container, "DataItem.NPWPNo")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtGridNoNPWP" TextMode="MultiLine"
                                            onblur="omitSomeCharacter('txtGridNoNPWP','<>?*%$;')" runat="server" MaxLength="20" Text='<%# DataBinder.Eval(Container, "DataItem.NPWPNo")%>'></asp:textbox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Status">
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGridStatus" ></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlGridStatus" runat="server" Width="140px"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="Edit" CausesValidation="False">
												            <img src="../images/edit.gif"  border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnDelete" runat="server" CommandName="Delete" CausesValidation="False">
												            <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lnkbtnSave" runat="server" CommandName="Save" CausesValidation="False">
												            <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnCancel" runat="server" CommandName="Cancel" CausesValidation="False">
												            <img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                    </EditItemTemplate>
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
