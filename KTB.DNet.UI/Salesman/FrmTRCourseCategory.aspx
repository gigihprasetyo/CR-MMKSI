<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTRCourseCategory.aspx.vb" Inherits=".FrmTRCourseCategory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td colspan="2" class="titlePage" style="height: 8px"></td>
            </tr>
            <tr>
                <td colspan="2" class="titlePage" style="height: 8px"></td>
            </tr>
            <tr>
                <td colspan="2" class="titlePage" style="height: 8px">Kategori Training</td>
            </tr>
            <tr>
                <td colspan="2" style="height: 1px" background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td colspan="2" style="height: 8px" height="8">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="24%">Kode </td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox ID="txtKode" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">Deskripsi </td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox ID="txtDeskripsi" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 29px" width="24%">Level</td>
                            <td style="height: 29px" width="1%">:</td>
                            <td style="width: 75%; height: 29px">
                                <asp:DropDownList ID="ddlLevel" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 12px" width="24%">Training Wajib</td>
                            <td style="height: 12px" width="1%">:</td>
                            <td style="width: 261px; height: 12px" width="261">
                                <asp:CheckBox ID="chkWajib" runat="server" Checked="true"></asp:CheckBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 29px" width="24%">Area Karyawan</td>
                            <td style="height: 29px" width="1%">:</td>
                            <td style="width: 75%; height: 29px">
                                <asp:DropDownList ID="DdlArea" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                
            </tr>
            <tr><td colspan="2"></td></tr>
            <tr>
                <td colspan="2">
                    <table cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td valign="top" colspan="6">
                                <div id="div1" style="overflow: auto; height: 280px">
                                    <asp:DataGrid ID="dtgEntryClaim" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" BorderWidth="0px" CellPadding="3" CellSpacing="1" DataKeyField="ID" ShowFooter="True" Width="100%">
                                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <AlternatingItemStyle BackColor="White" ForeColor="Black" />
                                        <ItemStyle BackColor="#F1F6FB" ForeColor="Black" />
                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle CssClass="titleTableParts" Width="10%" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server" NAME="lblNo" Text="1"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Size="Small" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Kode Job Posisi">
                                                <HeaderStyle CssClass="titleTableParts" Width="20%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodeJobPos" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Code")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtKodeJobPos" runat="server" BackColor="White" MaxLength="18" Width="80px"></asp:TextBox>
                                                    <asp:Label ID="lblFooterKodeJobPos" runat="server">
                                                        <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                                </FooterTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtKodeJobPos" runat="server" BackColor="White" MaxLength="18" Text='<%# DataBinder.Eval(Container.DataItem, "DataItem.Code")%>' Width="95px">
                                                    </asp:TextBox>
                                                    <asp:Label ID="lblEditKodeJobPos" runat="server">
									<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>                                          
                                            <asp:TemplateColumn HeaderText="Deskripsi">
                                                <HeaderStyle CssClass="titleTableParts" Width="10%" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeskripsi" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtDeskripsiEntry" runat="server" MaxLength="280" />
                                                </FooterTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDeskripsiEntryEdit" runat="server" MaxLength="280" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>' />
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                            <asp:TemplateColumn>
                                                <HeaderStyle CssClass="titleTableParts" Width="2%" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete">
									<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="Add">
									<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button runat="server" ID="btnKembali" CausesValidation="false" Text="Kembali" />
                    <asp:Button runat="server" ID="btnSimpan" CausesValidation="true" Text="Simpan" />
                    <asp:Button runat="server" ID="btnBaru" CausesValidation="false" Text="Baru" />
                    <asp:Button runat="server" ID="btnValidasi" CausesValidation="false" Text="Validasi" />
                    <asp:Button runat="server" ID="btnPercepatan" CausesValidation="false" Text="Percepatan" />
                    <asp:Button runat="server" ID="btnBatalPembayaran" CausesValidation="false" Text="Batal Pembayaran" />
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
