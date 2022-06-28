<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDownloadCourseRegistrationCS.aspx.vb" Inherits=".FrmDownloadCourseRegistrationCS" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Download Daftar Kelas</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
</head>
<body ms_positioning="GridLayout" topmargin="0" leftmargin="0">
    <form id="Form1" method="post" runat="server">
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <asp:DataGrid ID="dtgDwnload" runat="server" Width="904px" AutoGenerateColumns="False" CellPadding="3"
                        BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="#CCCCCC">
                        <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#FFFFFF"></HeaderStyle>
                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
                                    ForeColor="White" BackColor="#666666"></HeaderStyle>
                                <ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ClassCode" HeaderText="Kode Kelas">
                                <HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
                                    ForeColor="White" BackColor="#666666"></HeaderStyle>
                                <ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
                            </asp:BoundColumn>

                            <asp:TemplateColumn HeaderText="Kategori Kursus" SortExpression="TrCouse.CourseCode">
                                <HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
                                    ForeColor="White" BackColor="#666666"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblCourseCategory" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Tanggal Mulai" SortExpression="StartDate">
                                <HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
                                    ForeColor="White" BackColor="#666666"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTanggalMulai" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Tanggal Selesai" SortExpression="FinishDate">
                                <HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
                                    ForeColor="White" BackColor="#666666"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTanggalSelesai" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Lokasi" SortExpression="">
                                <HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
                                    ForeColor="White" BackColor="#666666"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblLokasi" Text='<%# DataBinder.Eval(Container, "DataItem.Location")%>' runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Tahun Fiskal" SortExpression="FiscalYear">
                                <HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
                                    ForeColor="White" BackColor="#666666"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblFiscalYear" Text='<%# DataBinder.Eval(Container, "DataItem.FiscalYear")%>' runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>

                             <asp:TemplateColumn HeaderText="Kapasitas" SortExpression="">
                                <HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
                                    ForeColor="White" BackColor="#666666"></HeaderStyle>
                               <ItemTemplate>
                                        <asp:Label ID="lblCapacity" Text='<%# DataBinder.Eval(Container, "DataItem.Capacity")%>' runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                <ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>

                             <asp:TemplateColumn HeaderText="Siswa Terundang" SortExpression="">
                                <HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
                                    ForeColor="White" BackColor="#666666"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSiswaTerundang"  runat="server">
                                        </asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>

                             <asp:TemplateColumn HeaderText="Siswa Terdaftar" SortExpression="">
                                <HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
                                    ForeColor="White" BackColor="#666666"></HeaderStyle>
                                <ItemTemplate>
                                        <asp:Label ID="lblSiswaTerdaftar"  runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                <ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>

                        </Columns>
                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
