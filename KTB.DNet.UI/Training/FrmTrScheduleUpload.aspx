<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTrScheduleUpload.aspx.vb" Inherits=".FrmTrScheduleUpload" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmTrMasterScheduleUpload</title>
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
                <td class="titlePage">
                    <asp:Label ID="lblPageTitle" runat="server"></asp:Label>
                </td>
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
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="24%">Kategori</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:DropDownList ID="ddlKategory" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">Tahun</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 16px">Nama Jadwal</td>
                            <td style="height: 16px">:</td>
                            <td style="height: 16px">
                                <p>
                                    <asp:TextBox ID="txtScheduleName" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="50"
                                        Width="320px"></asp:TextBox>&nbsp;
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*" ValidationExpression="^[\s\S]{0,250}$"
                                        ControlToValidate="txtScheduleName" Display="Dynamic"></asp:RegularExpressionValidator>
                                </p>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" style="height: 24px">Deskripsi</td>
                            <td style="height: 24px">:</td>
                            <td style="height: 24px">
                                <asp:TextBox ID="txtDesc" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="250"
                                    Width="328px" TextMode="MultiLine" Height="64px"></asp:TextBox>&nbsp;
									<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*" ValidationExpression="^[\s\S]{0,250}$"
                                        ControlToValidate="txtDesc" Display="Dynamic"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr id="trUpload" runat="server">
                            <td class="titleField" style="height: 23px">Lokasi File</td>
                            <td style="height: 23px">:</td>
                            <td style="height: 23px">
                                <input onkeypress="return false" id="File" type="file" size="35" name="File1" runat="server">
                                <asp:Label ID="lblInfofile" runat="server" ForeColor="Red">&nbsp; Pdf, Ms. Excel</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSimpan" runat="server" Text="Simpan" Width="60px"></asp:Button>&nbsp;
									<asp:Button ID="btnBatal" runat="server" Text="Batal" Width="60px"></asp:Button>
                                <asp:Button ID="btnCari" runat="server" Text="Cari" Width="60px"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 300px">
                        <asp:DataGrid ID="dtgSchedule" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                            CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowCustomPaging="True" AllowPaging="True"
                            Width="100%" AllowSorting="True" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ScheduleYear" SortExpression="ScheduleYear" HeaderText="Tahun">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn  SortExpression="JobPositionCategory.Description" HeaderText="Kategori">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKategori" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama Jadwal">
                                    <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="Description" HeaderText="Deskripsi">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDescription" Width="98%" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:BoundColumn DataField="UploadDate" SortExpression="UploadDate" HeaderText="Tanggal Upload" DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDownload" runat="server" CommandName="Download" CausesValidation="False">
                                            <img src="../images/unduh.png" border="0" alt="Download">
                                        </asp:LinkButton>
                                        <asp:Label ID="lblDownload" runat="server" Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
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

