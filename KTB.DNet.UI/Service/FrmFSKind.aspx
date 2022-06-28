<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmFSKind.aspx.vb" Inherits="FrmFSKind" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Jenis Free Service</title>
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
                <td class="titlePage">FREE SERVICE -&nbsp; Jenis Free Service</td>
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
                            <td class="titleField" width="24%">Kode</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharUniv(txtKindCode)" ID="txtKindCode" runat="server" Width="70px"
                                    MaxLength="3"></asp:TextBox><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Silahkan isi kode Free Service (tidak boleh kosong)"
                                        ControlToValidate="txtKindCode" BorderStyle="Inset" Display="None"></asp:RequiredFieldValidator>--%>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtKindCode" ErrorMessage="*"></asp:RequiredFieldValidator>--%></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">KM</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="TxtKM" runat="server" Width="70px" MaxLength="6" ToolTip="Harus diisi dengan angka"></asp:TextBox><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Silahkan isi KM dengan angka (tidak boleh kosong)"
                                    <%--ControlToValidate="TxtKM" Display="None"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Width="140px" ErrorMessage="Silahkan isi dengan angka (tanpa koma dan titik)"
                                        ControlToValidate="TxtKM" ValidationExpression="\d{1,6}" Display="None"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtKM" ErrorMessage="*"></asp:RequiredFieldValidator>--%></td>
                        </tr>
                        <tr>
                            <td class="titleField">Deskripsi</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtKindDesc" runat="server" Width="304px"
                                    MaxLength="30"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtKindDesc" ErrorMessage="Silahkan isi deskripsi"
                                    Display="None"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtKindDesc" ErrorMessage="*"></asp:RequiredFieldValidator>--%></td>
                        </tr>
                        <tr style="display: none">
                            <td class="titleField">Tipe FS</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlFSType" runat="server"></asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlFSType" ErrorMessage="Silahkan pilih tipe FS"
                                    Display="None" Enabled="false"></asp:RequiredFieldValidator>--%>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlFSType" ErrorMessage="*"></asp:RequiredFieldValidator>--%></td>
                        </tr>
                        <tr>
                                <td class="titleField">Jenis PM</td>
                                <td style="width: 2px" valign="top">:</td>
                                <td valign="top">
                                    <asp:DropDownList ID="ddlPM" runat="server"  Width="180"></asp:DropDownList>
                                </td>
                            </tr>
                        <tr style="display: none">
                            <td class="titleField">Status</td>
                            <td>:</td>
                            <td>
                                <asp:CheckBox ID="cbStatus" runat="server" Text="Aktif" /></td>
                        </tr>
                        <tr>
                            <td class="titleField"></td>
                            <td></td>
                            <td>&nbsp;
									<asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:Button><asp:Button ID="btnCari" runat="server" Width="60px" Text="Cari"></asp:Button><asp:Button ID="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:Button>
                                <%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"
                                    DisplayMode="SingleParagraph"></asp:ValidationSummary>--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 350px">
                        <asp:DataGrid ID="dtgFSKind" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" GridLines="Horizontal"
                            CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True"
                            PageSize="25" AllowPaging="True">
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
                                <asp:BoundColumn DataField="KindCode" SortExpression="KindCode" HeaderText="Kode">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="KM" SortExpression="KM" HeaderText="KM">
                                    <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableService"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="KindDescription" SortExpression="KindDescription" HeaderText="Deskripsi">
                                    <HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="FSType" HeaderText="Tipe FS" Visible="false">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFSType" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="PM Deskripsi" ItemStyle-HorizontalAlign="Center">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPM" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="false">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnActive" CausesValidation="False" runat="server" Text="Aktif" CommandName="Active"
                                            ToolTip="Aktivkan">
												<img border="0" src="../images/aktif.gif" alt="Aktifkan" style="cursor:hand"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnInactive" CausesValidation="False" runat="server" Text="Non-aktif" CommandName="Inactive"
                                            ToolTip="Non Aktivkan">
												<img border="0" src="../images/in-aktif.gif" alt="Non-aktifkan" style="cursor:hand"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="false">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
                                            CommandName="View">
												<img alt="Lihat Detil" src="../images/detail.gif" border="0"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img alt="Hapus" src="../images/trash.gif" border="0"></asp:LinkButton>
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
