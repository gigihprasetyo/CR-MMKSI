<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpUploadDocumentCBUReturn.aspx.vb" Inherits=".PopUpUploadDocumentCBUReturn" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Upload Document CBU Return</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <base target="_self">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="4" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" colspan="7">Attachment Claim</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor_sales.gif" colspan="7" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid ID="dgUploadFile" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1"
                                    AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                                    <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                                    <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                                    <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                    <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="No.">
                                            <HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdDocID" runat="server" />
                                                <asp:Label ID="lblNo" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Remark">
                                            <HeaderStyle CssClass="titleTablePromo" Width="350"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeskripsi" runat="server" Style="word-wrap: normal; word-break: break-all;"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtDeskripsiNew" runat="server" Width="350"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="File Name">
                                            <HeaderStyle CssClass="titleTablePromo" Width="240"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnDownload" runat="server" CommandName="download" CausesValidation="False" Style="word-wrap: normal; word-break: break-all;"></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:FileUpload ID="fuUploadNew" runat="server" Width="240"></asp:FileUpload>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle CssClass="titleTablePromo" Width="80"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <div style="width: 80px">
                                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" OnClientClick="return confirm('Anda yakin mau hapus?');" CommandName="delete" CausesValidation="False">
                                                            <img src="../images/trash.gif" border="0" alt="Hapus">
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnAdd" runat="server" CommandName="add" CausesValidation="False" TabIndex="0">
								                        <img src="../images/add.gif" border="0" alt="Tambah">
                                                </asp:LinkButton>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSimpan" OnClientClick="return confirm('Anda yakin mau simpan?');" runat="server" Text="Simpan" Visible="false"></asp:Button>
                                <input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup" name="btnCancel">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
