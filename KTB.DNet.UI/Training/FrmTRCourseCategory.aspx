<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTRCourseCategory.aspx.vb" Inherits=".FrmTRCourseCategory" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
    <title></title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <base target="_self">
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.getElementById("IdDelete");
            if (confirm("Yakin data ini akan dihapus?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
        }

        function RestrictSpace() {
            if (event.keyCode == 32) {
                event.returnValue = false;
                return false;
            }
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <asp:HiddenField ID="idvalue" runat="server" />
        <asp:HiddenField ID="IdDelete" runat="server" />
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">

            <tr>
                <td colspan="2" class="titlePage" style="height: 8px">
                    <asp:Label ID="lblTitle" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr id="trInput" runat="server">
                <td valign="top">
                    <table id="Table1" runat="server" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td width="150" class="titleField">Pilih Kategori</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlKategory" runat="server" Width="250px" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Kode </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtKode" onkeypress="return RestrictSpace()" runat="server" Width="250px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtKode"></asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Nama </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtDeskripsi" onkeypress="return HtmlCharUniv(event)" runat="server" Width="250px"
                                    MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtDeskripsi"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Tipe Kategori Kursus</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlTipeKategoriKursus" runat="server" Width="250px" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:CheckBox ID="cbxAll" Text="Pilih Semua" runat="server" AutoPostBack="true" Visible="false" />
                            </td>
                        </tr>
                        <tr id="trjobposition" runat="server" visible="false">
                            <td class="titleField">Posisi</td>
                            <td>:</td>
                            <td>
                                <div id="dv1" style="width: 250px; height: 100px; overflow-y: scroll">
                                    <asp:CheckBoxList ID="listCheckJobPosition" runat="server" RepeatColumns="1"></asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                        <tr id="trLevel" runat="server" visible="false">
                            <td class="titleField">Level</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlLevel" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Status</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="DdlStatus" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField"></td>
                            <td></td>
                            <td>
                                <asp:Button runat="server" Width="60px" ID="btnSimpan" CausesValidation="true" Text="Simpan" />&nbsp;
                                <asp:Button runat="server" Width="60px" ID="btnBatal" CausesValidation="false" Text="Batal" />&nbsp;
                                <asp:Button runat="server" Width="60px" ID="btnCari" CausesValidation="false" Text="Cari" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td valign="top">

                    <div id="div1" style="overflow: auto; height: 280px">
                        <asp:DataGrid ID="GridCourseCtg" runat="server" AllowSorting="True"
                            AutoGenerateColumns="False" AllowCustomPaging="True" BackColor="#CDCDCD" BorderColor="Gainsboro" BorderWidth="0px"
                            CellPadding="3" CellSpacing="1"
                            DataKeyField="ID" ShowFooter="False" Width="100%" AllowPaging="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="White" ForeColor="Black" />
                            <ItemStyle BackColor="#F1F6FB" ForeColor="Black" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle CssClass="titleTableService" Width="2%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server" NAME="lblNo" Text="1"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Size="Small" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Code" SortExpression="Code" HeaderText="Kode">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Nama">
                                    <HeaderStyle CssClass="titleTableService" Width="30%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeskripsi" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kategori">
                                    <HeaderStyle CssClass="titleTableService" Width="7%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKategori" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tipe Kategori Kursus">
                                    <HeaderStyle CssClass="titleTableService" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipeKategori" runat="server" NAME="lblTipeKategori"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Status">
                                    <HeaderStyle CssClass="titleTableService" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" NAME="lblStatus"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle CssClass="titleTableService" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
                                            CommandName="View">
															<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
                                            CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Edit"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" OnClientClick="Confirm();" CommandName="Delete">
									                    <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />

                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>

                </td>
            </tr>

        </table>

    </form>
</body>
</html>
