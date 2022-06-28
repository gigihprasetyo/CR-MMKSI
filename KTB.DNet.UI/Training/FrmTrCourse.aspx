<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTrCourse.aspx.vb" Inherits="FrmTrCourse" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmCourse</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>

    <script type="text/javascript">
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
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
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
            <tr>
                <td valign="top">
                    <table id="Table2" runat="server" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="height: 29px" width="24%">Pilih Kategori</td>
                            <td style="height: 29px" width="1%">:</td>
                            <td style="width: 75%; height: 29px">
                                <asp:DropDownList ID="ddlKategory" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">Kode Kategori</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox onkeypress="return RestrictSpace()" ID="txtCourseCode" runat="server" MaxLength="20"></asp:TextBox>&nbsp;&nbsp;
									<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtCourseCode"
                                        Display="Dynamic"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField">Nama Kategori</td>
                            <td>:</td>
                            <td>
                                <p>
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtCourseName" runat="server" MaxLength="50"
                                        Width="300"></asp:TextBox>&nbsp;&nbsp;
										<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtCourseName"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                </p>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField">Deskripsi</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtDescCourse" AccessKey="M" runat="server"
                                    MaxLength="200" Width="300" TextMode="MultiLine" Rows="3" Columns="250"></asp:TextBox>&nbsp;&nbsp;
									<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtDescCourse"
                                        Display="Dynamic" ValidationExpression="^[\s\S]{0,250}$"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField">Prasyarat Lama Kerja</td>
                            <td>:</td>
                            <td>
                                <asp:CheckBox ID="chkRequireWorkDate" runat="server" Checked="False" AutoPostBack="true"></asp:CheckBox>
                                <asp:TextBox onkeypress="return numericOnlyUniv(event)" ID="txtRequireWorkDate" Enabled="false" runat="server" MaxLength="20"></asp:TextBox>&nbsp;<asp:Label ID="lblbln" runat="server" Text="dalam bulan"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Kode Kelas</td>
                            <td>:</td>
                            <td width="75%">
                                <asp:CheckBox ID="chkCodeKelas" runat="server" Checked="False" AutoPostBack="true"></asp:CheckBox>
                                <asp:TextBox ID="txCodeKelas" onkeypress="return HtmlCharUniv(event)" runat="server" Enabled="false"></asp:TextBox><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txCodeKelas"></asp:RequiredFieldValidator>--%>
                        </tr>
                        <tr>
                            <td class="titleField">Batas Nilai Kelulusan</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox onkeypress="return numericOnlyUniv(event)" ID="txtBtsLulus" runat="server" MaxLength="3"
                                    Width="24px">70</asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtBtsLulus"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Nilai 0-100" ControlToValidate="txtBtsLulus"
                                        MaximumValue="100" MinimumValue="0" Type="Integer"></asp:RangeValidator>

                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Status</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField">Tipe Pembayaran</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlTipePembayaran" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField">Kategori Kursus</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" CausesValidation="false"></asp:DropDownList></td>
                            <asp:HiddenField ID="hdnCategory" runat="server" />
                        </tr>
                        <tr>
                            <td class="titleField">Level</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlLevel" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField">Notes</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtNotes" AccessKey="M" runat="server"
                                    MaxLength="200" Width="300" TextMode="MultiLine" Rows="3" Columns="250"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField"></td>
                            <td></td>
                            <td>
                                <p>
                                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan" Width="60px"></asp:Button>&nbsp;
										<asp:Button ID="btnBatal" runat="server" Text="Batal" Width="60px" CausesValidation="False"></asp:Button>&nbsp;
										<asp:Button ID="btnCari" runat="server" Text="Cari" Width="60px" CausesValidation="False"></asp:Button>
                                </p>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 280px">
                        <asp:DataGrid ID="dtgCourse" runat="server" Width="100%" Font-Names="MS Reference Sans Serif"
                            CellSpacing="1" ForeColor="GhostWhite" PageSize="25" AllowSorting="True" AllowPaging="True"
                            AllowCustomPaging="True" BorderColor="#CDCDCD"
                            BorderStyle="None" BorderWidth="0px" BackColor="Gainsboro" CellPadding="3" GridLines="Horizontal"
                            AutoGenerateColumns="False">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="CourseCode" SortExpression="CourseCode" HeaderText="Kode Kategori">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CourseName" SortExpression="CourseName" HeaderText="Nama Kategori">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="JobPositionCategory.Description" HeaderText="Kategori">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="RequireWorkDate" HeaderText="Prasyarat Lama Kerja">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# IIf(CType(DataBinder.Eval(Container, "DataItem.RequireWorkDate"),Boolean), "Ya", "Tidak") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="WorkDate" HeaderText="Periode Lama Waktu">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorkDate" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# IIf(CType(DataBinder.Eval(Container, "DataItem.Status"),String) = "1", "Aktif", "Tidak Aktif") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kategori Kursus">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Level">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLevel" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnLihat" runat="server" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
                                        <asp:LinkButton ID="btnUbah" runat="server" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="btnHapus" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
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
