<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDownloadCourseRegistrationDetailCS.aspx.vb" Inherits=".FrmDownloadCourseRegistrationDetailCS" %>

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
                             <asp:TemplateColumn HeaderText="No. Reg" SortExpression="ID">
                                <HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
                                    ForeColor="White" BackColor="#666666"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNoReg" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Kode Kelas">
                                <HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
                                    ForeColor="White" BackColor="#666666"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblClassCode" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Nama Siswa">
                                <HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
                                    ForeColor="White" BackColor="#666666"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNamaSiswa" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Kode Dealer" >
                                <HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
                                    ForeColor="White" BackColor="#666666"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Mulai Bekerja">
                                <HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
                                    ForeColor="White" BackColor="#666666"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblMulaiKerja" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Posisi">
                                <HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
                                    ForeColor="White" BackColor="#666666"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPosisi" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Tanggal Validasi">
                                <HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
                                    ForeColor="White" BackColor="#666666"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblValidasi" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>

                             <asp:TemplateColumn HeaderText="Status">
                                <HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
                                    ForeColor="White" BackColor="#666666"></HeaderStyle>
                               <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server">
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
